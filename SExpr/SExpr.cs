namespace SExpr
{
    public interface ISExpr
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