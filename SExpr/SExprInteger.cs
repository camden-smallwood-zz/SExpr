namespace SExpr
{
    public struct SExprInteger : ISExpr
    {
        public SExprNodeType NodeType => SExprNodeType.Integer;

        public long Value;
    }
}