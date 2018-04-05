namespace SExpr
{
    public struct SExprCons : ISExprNode
    {
        public SExprNodeType NodeType => SExprNodeType.Cons;

        public ISExprNode Car;
        public ISExprNode Cdr;
    }
}