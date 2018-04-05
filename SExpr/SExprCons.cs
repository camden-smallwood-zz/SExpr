namespace SExpr
{
    public struct SExprCons : ISExpr
    {
        public SExprNodeType NodeType => SExprNodeType.Cons;

        public ISExpr Car;
        public ISExpr Cdr;
    }
}