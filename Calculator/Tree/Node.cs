namespace Calculator;

public class Node
{
    public Operation Operation;
    public Node? Parent;
    public Node? LeftChild;
    public Node? RightChild;

    public Node(Operation operation, Node? leftChild, Node? rightChild)
    {
        Operation = operation;
        LeftChild = leftChild;
        RightChild = rightChild;
    }
    
    public Node(Operation operation, Node? parent, Node? leftChild, Node? rightChild)
    {
        Operation = operation;
        Parent = parent;
        LeftChild = leftChild;
        RightChild = rightChild;
    }

    public Node Clone()
    {
        return new Node(Operation, Parent, LeftChild, RightChild);
    }
}