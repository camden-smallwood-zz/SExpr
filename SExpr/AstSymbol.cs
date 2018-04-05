namespace SExpr
{
    public struct SExprSymbol : ISExprNode
    {
        public SExprNodeType NodeType => SExprNodeType.Symbol;

        public string Value;
    }
}