using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Composite;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.HeadSpecs;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
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

        public SymbolicHeadSpecsFunction Power { get; }

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

            Negative = SymbolicHeadSpecsOperator.CreateNonAssociative(Context, SymbolicFunctionNames.Negative, " -", 1, SymbolicOperatorPosition.Prefix);

            Plus = SymbolicHeadSpecsOperator.CreateAssociative(Context, SymbolicFunctionNames.Plus, " + ", 2, SymbolicOperatorPosition.Infix);
            Subtract = SymbolicHeadSpecsOperator.CreateNonAssociative(Context, SymbolicFunctionNames.Subtract, " - ", 2, SymbolicOperatorPosition.Infix);
            Times = SymbolicHeadSpecsOperator.CreateAssociative(Context, SymbolicFunctionNames.Times, " * ", 3, SymbolicOperatorPosition.Infix);
            Divide = SymbolicHeadSpecsOperator.CreateNonAssociative(Context, SymbolicFunctionNames.Divide, " / ", 3, SymbolicOperatorPosition.Infix);
            
            Inverse = SymbolicHeadSpecsFunction.CreateNonAssociative(Context, SymbolicFunctionNames.Inverse);
            Abs = SymbolicHeadSpecsFunction.CreateNonAssociative(Context, SymbolicFunctionNames.Abs);
            Sqrt = SymbolicHeadSpecsFunction.CreateNonAssociative(Context, SymbolicFunctionNames.Sqrt);
            Exp = SymbolicHeadSpecsFunction.CreateNonAssociative(Context, SymbolicFunctionNames.Exp);
            Power = SymbolicHeadSpecsFunction.CreateNonAssociative(Context, SymbolicFunctionNames.Power);
            Log = SymbolicHeadSpecsFunction.CreateNonAssociative(Context, SymbolicFunctionNames.Log);
            Log2 = SymbolicHeadSpecsFunction.CreateNonAssociative(Context, SymbolicFunctionNames.Log2);
            Log10 = SymbolicHeadSpecsFunction.CreateNonAssociative(Context, SymbolicFunctionNames.Log10);
            Cos = SymbolicHeadSpecsFunction.CreateNonAssociative(Context, SymbolicFunctionNames.Cos);
            Sin = SymbolicHeadSpecsFunction.CreateNonAssociative(Context, SymbolicFunctionNames.Sin);
            Tan = SymbolicHeadSpecsFunction.CreateNonAssociative(Context, SymbolicFunctionNames.Tan);
            ArcCos = SymbolicHeadSpecsFunction.CreateNonAssociative(Context, SymbolicFunctionNames.ArcCos);
            ArcSin = SymbolicHeadSpecsFunction.CreateNonAssociative(Context, SymbolicFunctionNames.ArcSin);
            ArcTan = SymbolicHeadSpecsFunction.CreateNonAssociative(Context, SymbolicFunctionNames.ArcTan);
            ArcTan2 = SymbolicHeadSpecsFunction.CreateNonAssociative(Context, SymbolicFunctionNames.ArcTan2);
            Cosh = SymbolicHeadSpecsFunction.CreateNonAssociative(Context, SymbolicFunctionNames.Cosh);
            Sinh = SymbolicHeadSpecsFunction.CreateNonAssociative(Context, SymbolicFunctionNames.Sinh);
            Tanh = SymbolicHeadSpecsFunction.CreateNonAssociative(Context, SymbolicFunctionNames.Tanh);
        }
    }
}