namespace SExpr
{
    public struct SExprNil : ISExpr
    {
        public SExprNodeType NodeType => SExprNodeType.Nil;
    }
}