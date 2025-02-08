namespace Calculator;

public class Node
{
    public Expression Expression;
    public Node? Parent;
    public Node? LeftChild;
    public Node? RightChild;

    public Node(Expression? expression, Node? leftChild, Node? rightChild)
    {
        Expression = expression;
        LeftChild = leftChild;
        RightChild = rightChild;
    }
    
    public Node(Expression? expression, Node? parent, Node? leftChild, Node? rightChild)
    {
        Expression = expression;
        Parent = parent;
        LeftChild = leftChild;
        RightChild = rightChild;
    }

    public Node Clone()
    {
        return new Node(Expression, Parent, LeftChild, RightChild);
    }
}