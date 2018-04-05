namespace SExpr
{
    public struct SExprBoolean : ISExprNode
    {
        public SExprNodeType NodeType => SExprNodeType.Boolean;

        public bool Value;
    }
}