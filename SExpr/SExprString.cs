namespace SExpr
{
    public struct SExprString : ISExpr
    {
        public SExprNodeType NodeType => SExprNodeType.String;

        public string Value;
    }
}