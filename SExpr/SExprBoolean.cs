namespace SExpr
{
    public struct SExprBoolean : ISExpr
    {
        public SExprNodeType NodeType => SExprNodeType.Boolean;

        public bool Value;
    }
}