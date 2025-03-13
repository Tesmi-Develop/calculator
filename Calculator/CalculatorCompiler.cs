using System.Data;
using Calculator.Expressions;

namespace Calculator;

public static class CalculatorCompiler
{
    public static void ProcessToken(Token token, List<Token> tokens, List<Expression> expressions, ref int index)
    {
        var expression = Expression.FindExpression(token, expressions, tokens, index);
        var oldIndex = index;
            
        expression?.Compile(tokens, ref index, expressions);
        if (expression != null) 
            expressions.Add(expression);

        if (index == oldIndex)
            index++;
    }
    public static List<Expression> Compile(List<Token> tokens)
    {
        Expression.InvokePreCompile(tokens);
        
        var expressions = new List<Expression>();
        var index = 0;
        
        while (index < tokens.Count)
        {
            ProcessToken(tokens[index], tokens, expressions, ref index);
        }
        
        return expressions;
    }

    private static int FindHighestBinaryOperation(List<Expression> expressions)
    {
        var result = -1;
        var priority = BinaryOperationPriority.Lowest;
        
        for (var i = 0; i < expressions.Count; i++)
        {
            var expression = expressions[i];
            
            if (expression is not BinaryOperationExpression operation)
                continue;

            if (operation.PriorityOperation <= priority && result != -1 && operation.PriorityOperation != BinaryOperationPriority.Highest)
                continue;
            
            result = i;
            priority = operation.PriorityOperation;
        }

        return result;
    }

    private static void ExecutePreProcessors(List<Expression> expressions, Dictionary<string, double> variables)
    {
        for (int i = 0; i < expressions.Count; i++)
        {
            var expression = expressions[i];
            if (expression is not PreProcessorExpression preProcessorExpression)
                continue;
            
            preProcessorExpression.Run(variables);
            expressions.RemoveAt(i);
            i--;
        }
    }
    
    public static double Compute(List<Expression> expressions, Dictionary<string, double> variables)
    {
        ExecutePreProcessors(expressions, variables);
        
        while (expressions.Count > 1)
        {
            var expressionIndex = FindHighestBinaryOperation(expressions);
            
            if (expressionIndex == -1 
                || expressionIndex == 0 
                || expressionIndex == expressions.Count - 1
                || expressions[expressionIndex - 1] is not CalculableExpression 
                || expressions[expressionIndex + 1] is not CalculableExpression)
                throw new InvalidExpressionException("Invalid expression");
            
            var expression = (BinaryOperationExpression)expressions[expressionIndex];
            var leftExpression = (CalculableExpression)expressions[expressionIndex - 1];
            var rightExpression = (CalculableExpression)expressions[expressionIndex + 1];
            
            var result = expression.Compute(leftExpression, rightExpression, variables);
            var wrappedResult = new NumericalLiteral();
            wrappedResult.Compile(result);
            
            expressions[expressionIndex] = wrappedResult;
            expressions.RemoveAt(expressionIndex + 1);
            expressions.RemoveAt(expressionIndex - 1);
        }
        
        if (expressions.Count == 1 && expressions[0] is not CalculableExpression)
            throw new InvalidExpressionException("Invalid expression");

        return expressions.Count == 1 ? ((CalculableExpression)expressions[0]).Compute(variables) : 0;
    }
}