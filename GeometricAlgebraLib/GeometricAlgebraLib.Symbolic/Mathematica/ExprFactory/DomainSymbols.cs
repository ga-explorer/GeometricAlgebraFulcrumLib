using Wolfram.NETLink;

namespace GeometricAlgebraLib.Symbolic.Mathematica.ExprFactory
{
    public static class DomainSymbols
    {
        public static readonly Expr Complexes = new Expr(ExpressionType.Symbol, "Complexes");
        public static readonly Expr Reals = new Expr(ExpressionType.Symbol, "Reals");
        public static readonly Expr Integers = new Expr(ExpressionType.Symbol, "Integers");
        public static readonly Expr Algebraics = new Expr(ExpressionType.Symbol, "Algebraics");
        public static readonly Expr Primes = new Expr(ExpressionType.Symbol, "Primes");
        public static readonly Expr Rationals = new Expr(ExpressionType.Symbol, "Rationals");
        public static readonly Expr Booleans = new Expr(ExpressionType.Symbol, "Booleans");
    }
}
