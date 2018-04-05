namespace SExpr
{
    public struct SExprNil : ISExprNode
    {
        public SExprNodeType NodeType => SExprNodeType.Nil;
    }
}