using System.Diagnostics;
using System.Linq;
using System.Text;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Mathematica.Mathematica.ExprFactory
{
    /// <summary>
    /// This class is used to construct mathematica expressions by applying functions to 
    /// sub expressions in an easy way
    /// </summary>
    public sealed class Mfs
    {
        public static Mfs NumericQ { get; } = new Mfs("NumericQ");

        public static Mfs PossibleZeroQ { get; } = new Mfs("PossibleZeroQ");
        public static Mfs Equal { get; } = new Mfs("Equal");
        public static Mfs Unequal { get; } = new Mfs("Unequal");
        public static Mfs Greater { get; } = new Mfs("Greater");
        public static Mfs GreaterEqual { get; } = new Mfs("GreaterEqual");
        public static Mfs Less { get; } = new Mfs("Less");
        public static Mfs LessEqual { get; } = new Mfs("LessEqual");
        public static Mfs Not { get; } = new Mfs("Not");
        public static Mfs And { get; } = new Mfs("And");
        public static Mfs Or { get; } = new Mfs("Or");
        public static Mfs Nand { get; } = new Mfs("Nand");
        public static Mfs Nor { get; } = new Mfs("Nor");
        
        public static Mfs Plus { get; } = new Mfs("Plus");
        public static Mfs Minus { get; } = new Mfs("Minus");
        public static Mfs Subtract { get; } = new Mfs("Subtract");
        public static Mfs Times { get; } = new Mfs("Times");
        public static Mfs Divide { get; } = new Mfs("Divide");
        public static Mfs Power { get; } = new Mfs("Power");
        public static Mfs Dot { get; } = new Mfs("Dot");

        public static Mfs Rational { get; } = new Mfs("Rational");
        public static Mfs Sign { get; } = new Mfs("Sign");
        public static Mfs RealSign { get; } = new Mfs("RealSign");
        public static Mfs UnitStep { get; } = new Mfs("UnitStep");
        public static Mfs UnitBox { get; } = new Mfs("UnitBox");
        public static Mfs Abs { get; } = new Mfs("Abs");
        public static Mfs RealAbs { get; } = new Mfs("RealAbs");
        public static Mfs Sqrt { get; } = new Mfs("Sqrt");
        public static Mfs Sin { get; } = new Mfs("Sin");
        public static Mfs Cos { get; } = new Mfs("Cos");
        public static Mfs Tan { get; } = new Mfs("Tan");
        public static Mfs ArcCos { get; } = new Mfs("ArcCos");
        public static Mfs ArcSin { get; } = new Mfs("ArcSin");
        public static Mfs Sinh { get; } = new Mfs("Sinh");
        public static Mfs Cosh { get; } = new Mfs("Cosh");
        public static Mfs Tanh { get; } = new Mfs("Tanh");
        public static Mfs ArcCosh { get; } = new Mfs("ArcCosh");
        public static Mfs ArcSinh { get; } = new Mfs("ArcSinh");
        public static Mfs Exp { get; } = new Mfs("Exp");
        public static Mfs Log { get; } = new Mfs("Log");
        public static Mfs Log2 { get; } = new Mfs("Log2");
        public static Mfs Log10 { get; } = new Mfs("Log10");
        public static Mfs D { get; } = new Mfs("D");
        public static Mfs Integrate { get; } = new Mfs("Integrate");
        public static Mfs ArcLength { get; } = new Mfs("ArcLength");
        public static Mfs N { get; } = new Mfs("N");
        public static Mfs ArcTan { get; } = new Mfs("ArcTan");
        public static Mfs Re { get; } = new Mfs("Re");
        public static Mfs Im { get; } = new Mfs("Im");
        public static Mfs Round { get; } = new Mfs("Round");

        public static Mfs List { get; } = new Mfs("List");
        public static Mfs IdentityMatrix { get; } = new Mfs("IdentityMatrix");
        public static Mfs ConstantArray { get; } = new Mfs("ConstantArray");
        public static Mfs DiagonalMatrix { get; } = new Mfs("DiagonalMatrix");
        public static Mfs SparseArray { get; } = new Mfs("SparseArray");
        public static Mfs Normal { get; } = new Mfs("Normal");
        public static Mfs Norm { get; } = new Mfs("Norm");
        public static Mfs VectorQ { get; } = new Mfs("VectorQ");
        public static Mfs MatrixQ { get; } = new Mfs("MatrixQ");
        public static Mfs Dimensions { get; } = new Mfs("Dimensions");
        public static Mfs Det { get; } = new Mfs("Det");
        public static Mfs Part { get; } = new Mfs("Part");
        public static Mfs Diagonal { get; } = new Mfs("Diagonal");
        public static Mfs Transpose { get; } = new Mfs("Transpose");
        public static Mfs Inverse { get; } = new Mfs("Inverse");
        public static Mfs SymmetricMatrixQ { get; } = new Mfs("SymmetricMatrixQ");
        public static Mfs Eigenvalues { get; } = new Mfs("Eigenvalues");
        public static Mfs Eigenvectors { get; } = new Mfs("Eigenvectors");
        public static Mfs Eigensystem { get; } = new Mfs("Eigensystem");
        public static Mfs Orthogonalize { get; } = new Mfs("Orthogonalize");

        public static Mfs Apply { get; } = new Mfs("Apply");
        public static Mfs Flatten { get; } = new Mfs("Flatten");
        public static Mfs Rule { get; } = new Mfs("Rule");
        public static Mfs RuleDelayed { get; } = new Mfs("RuleDelayed");
        public static Mfs Element { get; } = new Mfs("Element");
        public static Mfs Ball { get; } = new Mfs("Ball");
        public static Mfs Alternatives { get; } = new Mfs("Alternatives");
        public static Mfs Simplify { get; } = new Mfs("Simplify");
        public static Mfs FactorTerms { get; } = new Mfs("FactorTerms");
        public static Mfs Collect { get; } = new Mfs("Collect");
        public static Mfs Expand { get; } = new Mfs("Expand");
        public static Mfs ExpandAll { get; } = new Mfs("ExpandAll");
        public static Mfs Refine { get; } = new Mfs("Refine");
        public static Mfs FullSimplify { get; } = new Mfs("FullSimplify");
        public static Mfs TrigReduce { get; } = new Mfs("TrigReduce");
        public static Mfs TrigExpand { get; } = new Mfs("TrigExpand");
        public static Mfs FunctionExpand { get; } = new Mfs("FunctionExpand");
        public static Mfs PowerExpand { get; } = new Mfs("PowerExpand");
        public static Mfs ToRadicals { get; } = new Mfs("ToRadicals");
        public static Mfs ReplaceAll { get; } = new Mfs("ReplaceAll");

        public static Mfs MathMlForm { get; } = new Mfs("MathMLForm");
        public static Mfs TeXForm { get; } = new Mfs("TeXForm");
        public static Mfs MatrixForm { get; } = new Mfs("MatrixForm");
        public static Mfs HoldForm { get; } = new Mfs("HoldForm");
        public static Mfs CForm { get; } = new Mfs("CForm");
        public static Mfs EToString { get; } = new Mfs("ToString");

        public static Mfs HilbertTransform { get; } = new Mfs("HilbertTransform");


        public static Expr ProductExpr(params Expr[] argsList)
        {
            if (argsList.Length == 0)
                return Expr.INT_ONE;

            return
                argsList.Length == 1
                ? argsList[0]
                : Times[argsList.Cast<object>().ToArray()];
        }

        public static Expr ProductExpr(bool isNegative, params Expr[] argsList)
        {
            if (argsList.Length == 0)
                return isNegative ? Expr.INT_MINUSONE : Expr.INT_ONE;

            var expr =
                (argsList.Length == 1)
                ? argsList[0]
                : Times[argsList.Cast<object>().ToArray()];

            return isNegative ? Minus[expr] : expr;
        }

        public static Expr SumExpr(params Expr[] argsList)
        {
            if (argsList.Length == 0)
                return Expr.INT_ZERO;

            return 
                argsList.Length == 1 
                ? argsList[0] 
                : Plus[argsList.Cast<object>().ToArray()];
        }

        public static Expr ListExpr(params Expr[] argsList)
        {
            return List[
                argsList
                    .Select(expr => (object)(expr ?? Expr.INT_ZERO))
                    .ToArray()
            ];
        }


        public Expr MathExpr { get; }

        public string FunctionName 
            => MathExpr.ToString();


        public Mfs(string functionName)
        {
            MathExpr = new Expr(ExpressionType.Symbol, functionName);
        }


        public Expr this[params object[] parameters]
        {
            get
            {
                if (parameters.Length == 0)
                    return new Expr(MathExpr);

                Debug.Assert(parameters.All(p => !ReferenceEquals(p, null)));

                return new Expr(MathExpr, parameters);
            }
        }

        public string this[string parametersText]
        {
            get
            {
                var stringBuilder = new StringBuilder(FunctionName.Length + parametersText.Length + 2);

                return 
                    stringBuilder
                    .Append(FunctionName)
                    .Append("[")
                    .Append(parametersText)
                    .Append("]")
                    .ToString();
            }
        }

        public override string ToString()
        {
            return MathExpr.ToString();
        }
    }
}
