using System.Data;
using Calculator.Expressions;

namespace Calculator;

public class CalculatorCompiler
{
    private void ValidateBrackets(List<Token> tokens)
    {
        var depth = 0;

        foreach (var token in tokens)
        {
            if (token.Type == TokenType.BracketOpen)
                depth++;
            
            if (token.Type == TokenType.BracketClose)
                depth--;
            
            if (depth < 0)
                throw new InvalidOperationException("Invalid brackets");
        }

        if (depth != 0)
            throw new InvalidOperationException("Invalid brackets");
    }
    
    private void ValidateModules(List<Token> tokens)
    {
        var count = 0;

        foreach (var token in tokens)
        {
            if (token.Type == TokenType.VerticalBar)
                count++;
        }

        if (count % 2 != 0)
            throw new InvalidOperationException("Invalid modules");
    }
    
    public List<Expression> Compile(List<Token> tokens)
    {
        ValidateBrackets(tokens);
        ValidateModules(tokens);
        
        var expressions = new List<Expression>();
        var index = 0;
        
        while (index < tokens.Count)
        {
            var token = tokens[index];
            var expression = Expression.FindExpression(token, expressions, tokens, index);
            var oldIndex = index;
            
            expression?.Compile(tokens, ref index, expressions);
            if (expression != null) 
                expressions.Add(expression);

            if (index == oldIndex)
                index++;
        }
        
        return expressions;
    }

    private int FindHighestBinaryOperation(List<Expression> expressions)
    {
        var result = -1;
        var priority = BinaryOperationPriority.Lowest;
        
        for (var i = 0; i < expressions.Count; i++)
        {
            var expression = expressions[i];
            if (!(expression is BinaryOperationExpression))
                continue;
            
            var operation = (BinaryOperationExpression)expression;
            if (operation.Priority <= priority && result != -1)
                continue;
            
            result = i;
            priority = operation.Priority;
            
            if (operation.Priority == BinaryOperationPriority.Highest)
                break;
        }

        return result;
    }
    
    public double Compute(List<Expression> expressions, Dictionary<string, double> variables)
    {
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
            var wrappedResult = new NumericalExpression();
            wrappedResult.Compile(result);
            
            expressions[expressionIndex] = wrappedResult;
            expressions.RemoveAt(expressionIndex + 1);
            expressions.RemoveAt(expressionIndex - 1);
        }

        return expressions.Count == 1 ? ((CalculableExpression)expressions[0]).Compute(variables) : 0;
    }
}