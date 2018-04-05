namespace SExpr
{
    public struct SExprInteger : ISExprNode
    {
        public SExprNodeType NodeType => SExprNodeType.Integer;

        public long Value;
    }
}