namespace Calculator;

public class Tree
{
    public readonly Node Root;

    public Tree(Node root)
    {
        if (root.Parent != null)
            throw new Exception("Root cannot have a parent");
        
        Root = root;
    }

    private Node? CloneNode(Node? node, Node? parent)
    {
        if (node == null)
            return null;
        
        var newNode = node.Clone();
        newNode.Parent = parent;
        newNode.LeftChild = CloneNode(node.LeftChild, newNode);
        newNode.RightChild = CloneNode(node.RightChild, newNode);
        
        return newNode;
    }
    
    public Node CloneRoot()
    {
        return CloneNode(Root, null)!;
    }

    private Node GetFreeNode(Node root)
    {
        if (root.LeftChild is null || root.RightChild is null)
            return root;

        return GetFreeNode(root.RightChild ?? root.LeftChild);
    }
    
    private Node GetFreeNode()
    {
        return GetFreeNode(Root);
    }

    public void Add(Node newNode)
    {
        if (newNode.LeftChild is null && newNode.RightChild != null)
            throw new ArgumentException("A node cannot have a right child without a left child");
        
        var foundNode = GetFreeNode(newNode);
        newNode.Parent = foundNode;
        
        if (foundNode.LeftChild != null)
        {
            foundNode.LeftChild = newNode;
            return;
        }
        
        foundNode.RightChild = newNode;
    }

    public void Add(Tree tree)
    {
        Add(tree.CloneRoot());
    } 
}