namespace SExpr
{
    public struct SExprSymbol : ISExpr
    {
        public SExprNodeType NodeType => SExprNodeType.Symbol;

        public string Value;
    }
}