namespace SExpr
{
    public interface ISExprNode
    {
        SExprNodeType NodeType { get; }
    }

    public enum SExprNodeType
    {
        Nil,
        Cons,
        Symbol,
        String,
        Integer,
        Real,
        Boolean
    }
}