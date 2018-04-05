namespace SExpr
{
    public struct SExprString : ISExprNode
    {
        public SExprNodeType NodeType => SExprNodeType.String;

        public string Value;
    }
}