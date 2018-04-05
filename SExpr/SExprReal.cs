namespace SExpr
{
    public struct SExprReal : ISExpr
    {
        public SExprNodeType NodeType => SExprNodeType.Real;

        public double Value;
    }
}