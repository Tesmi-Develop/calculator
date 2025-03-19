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

    private static void ExecutePreProcessors(List<Expression> expressions, Dictionary<string, double> variables)
    {
        for (var i = 0; i < expressions.Count; i++)
        {
            var expression = expressions[i];
            if (expression is not PreProcessorExpression preProcessorExpression)
                continue;
            
            preProcessorExpression.Run(variables);
            expressions.RemoveAt(i);
            i--;
        }
    }

    private static List<Expression> ShuntingYard(List<Expression> expressions)
    {
        var output = new List<Expression>();
        var stack = new Stack<BinaryOperationExpression>();

        foreach (var expression in expressions)
        {
            if (expression is CalculableExpression)
            {
                output.Add(expression);
                continue;
            }
            
            if (expression is not BinaryOperationExpression operation)
                continue;

            while (stack.Count > 0 && 
                   (operation.Associativity == BinaryOperationAssociativity.Left ? 
                       stack.Peek().PriorityOperation >= operation.PriorityOperation : 
                       stack.Peek().PriorityOperation > operation.PriorityOperation))
                output.Add(stack.Pop());
            
            stack.Push(operation);
        }
        
        while (stack.Count > 0)
            output.Add(stack.Pop());

        return output;
    }
    
    public static double Compute(List<Expression> expressions, Dictionary<string, double> variables)
    {
        ExecutePreProcessors(expressions, variables);
        
        var sortedExpressions = ShuntingYard(expressions);
        var stack = new Stack<Expression>();

        foreach (var expression in sortedExpressions)
        {
            if (expression is CalculableExpression)
            {
                stack.Push(expression);
                continue;
            }

            if (expression is not BinaryOperationExpression operation)
                throw new InvalidExpressionException("Invalid expression");

            if (stack.Pop() is not CalculableExpression right || stack.Pop() is not CalculableExpression left)
                throw new InvalidExpressionException("Invalid expression");
            
            var result = new NumericalLiteral();
            result.Compile(operation.Compute(left, right, variables));
            stack.Push(result);
        }
        
        if (stack.Count == 1 && stack.Peek() is not CalculableExpression)
            throw new InvalidExpressionException("Invalid expression");

        return stack.Count == 1 ? ((CalculableExpression)stack.Peek()).Compute(variables) : 0;
    }
}