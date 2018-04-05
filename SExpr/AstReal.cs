namespace SExpr
{
    public struct SExprReal : ISExprNode
    {
        public SExprNodeType NodeType => SExprNodeType.Real;

        public double Value;
    }
}