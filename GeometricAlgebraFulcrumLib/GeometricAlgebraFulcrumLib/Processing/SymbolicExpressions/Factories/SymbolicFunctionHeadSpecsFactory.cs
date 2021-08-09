using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Composite;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.HeadSpecs;

namespace GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Factories
{
    public sealed class SymbolicFunctionHeadSpecsFactory
    {
        public SymbolicContext Context { get; }

        public SymbolicHeadSpecsOperator Plus { get; }

        public SymbolicHeadSpecsOperator Subtract { get; }

        public SymbolicHeadSpecsOperator Times { get; }

        public SymbolicHeadSpecsOperator Divide { get; }

        public SymbolicHeadSpecsOperator Negative { get; }

        public SymbolicHeadSpecsFunction Inverse { get; }

        public SymbolicHeadSpecsFunction Abs { get; }

        public SymbolicHeadSpecsFunction Sqrt { get; }

        public SymbolicHeadSpecsFunction Exp { get; }

        public SymbolicHeadSpecsFunction Log { get; }

        public SymbolicHeadSpecsFunction Log2 { get; }

        public SymbolicHeadSpecsFunction Log10 { get; }

        public SymbolicHeadSpecsFunction Cos { get; }

        public SymbolicHeadSpecsFunction Sin { get; }

        public SymbolicHeadSpecsFunction Tan { get; }

        public SymbolicHeadSpecsFunction ArcCos { get; }

        public SymbolicHeadSpecsFunction ArcSin { get; }

        public SymbolicHeadSpecsFunction ArcTan { get; }

        public SymbolicHeadSpecsFunction ArcTan2 { get; }

        public SymbolicHeadSpecsFunction Cosh { get; }

        public SymbolicHeadSpecsFunction Sinh { get; }

        public SymbolicHeadSpecsFunction Tanh { get; }


        internal SymbolicFunctionHeadSpecsFactory([NotNull] SymbolicContext context)
        {
            Context = context;

            Negative = SymbolicHeadSpecsOperator.CreateNonAssociative(Context, "Minus", " -", 1, SymbolicOperatorPosition.Prefix);

            Plus = SymbolicHeadSpecsOperator.CreateAssociative(Context, "Plus", " + ", 2, SymbolicOperatorPosition.Infix);
            Subtract = SymbolicHeadSpecsOperator.CreateNonAssociative(Context, "Subtract", " - ", 2, SymbolicOperatorPosition.Infix);
            Times = SymbolicHeadSpecsOperator.CreateAssociative(Context, "Times", " * ", 3, SymbolicOperatorPosition.Infix);
            Divide = SymbolicHeadSpecsOperator.CreateNonAssociative(Context, "Divide", " / ", 3, SymbolicOperatorPosition.Infix);
            
            Inverse = SymbolicHeadSpecsFunction.CreateNonAssociative(Context, "Inverse");
            Abs = SymbolicHeadSpecsFunction.CreateNonAssociative(Context, "Abs");
            Sqrt = SymbolicHeadSpecsFunction.CreateNonAssociative(Context, "Sqrt");
            Exp = SymbolicHeadSpecsFunction.CreateNonAssociative(Context, "Exp");
            Log = SymbolicHeadSpecsFunction.CreateNonAssociative(Context, "Log");
            Log2 = SymbolicHeadSpecsFunction.CreateNonAssociative(Context, "Log2");
            Log10 = SymbolicHeadSpecsFunction.CreateNonAssociative(Context, "Log10");
            Cos = SymbolicHeadSpecsFunction.CreateNonAssociative(Context, "Cos");
            Sin = SymbolicHeadSpecsFunction.CreateNonAssociative(Context, "Sin");
            Tan = SymbolicHeadSpecsFunction.CreateNonAssociative(Context, "Tan");
            ArcCos = SymbolicHeadSpecsFunction.CreateNonAssociative(Context, "ArcCos");
            ArcSin = SymbolicHeadSpecsFunction.CreateNonAssociative(Context, "ArcSin");
            ArcTan = SymbolicHeadSpecsFunction.CreateNonAssociative(Context, "ArcTan");
            ArcTan2 = SymbolicHeadSpecsFunction.CreateNonAssociative(Context, "ArcTan");
            Cosh = SymbolicHeadSpecsFunction.CreateNonAssociative(Context, "Cosh");
            Sinh = SymbolicHeadSpecsFunction.CreateNonAssociative(Context, "Sinh");
            Tanh = SymbolicHeadSpecsFunction.CreateNonAssociative(Context, "Tanh");
        }
    }
}