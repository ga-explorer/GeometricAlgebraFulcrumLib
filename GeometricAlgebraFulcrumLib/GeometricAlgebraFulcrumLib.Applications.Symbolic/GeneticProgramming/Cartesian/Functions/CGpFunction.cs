using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.GeneticProgramming.Cartesian.Functions
{
    public abstract class CGpFloat64Function
    {
        public static Pair<int> ZeroArityRange { get; }
            = new Pair<int>(0, 0);

        public static Pair<int> OneArityRange { get; }
            = new Pair<int>(1, 1);

        public static Pair<int> TwoArityRange { get; }
            = new Pair<int>(2, 2);

        public static Pair<int> ThreeArityRange { get; }
            = new Pair<int>(3, 3);

        public static Pair<int> ZeroToMaxArityRange { get; }
            = new Pair<int>(0, int.MaxValue - 1);

        public static Pair<int> OneToMaxArityRange { get; }
            = new Pair<int>(1, int.MaxValue - 1);

        public static Pair<int> TwoToMaxArityRange { get; }
            = new Pair<int>(2, int.MaxValue - 1);

        public static Pair<int> ThreeToMaxArityRange { get; }
            = new Pair<int>(3, int.MaxValue - 1);


        public static CGpFloat64ConstantFunction Zero { get; }
            = new CGpFloat64ConstantFunction(0);
        
        public static CGpFloat64ConstantFunction One { get; }
            = new CGpFloat64ConstantFunction(1);
        
        public static CGpFloat64ConstantFunction Two { get; }
            = new CGpFloat64ConstantFunction(2);
        
        public static CGpFloat64ConstantFunction Pi { get; }
            = new CGpFloat64ConstantFunction(Math.PI);
        
        public static CGpFloat64ConstantFunction E { get; }
            = new CGpFloat64ConstantFunction(Math.E);

        public static CGpFloat64NegativeFunction Negative
            => CGpFloat64NegativeFunction.Instance;

        public static CGpFloat64InverseFunction Inverse
            => CGpFloat64InverseFunction.Instance;
        
        public static CGpFloat64PlusFunction Plus 
            => CGpFloat64PlusFunction.Instance;
        
        public static CGpFloat64TimesFunction Times 
            => CGpFloat64TimesFunction.Instance;

        public static CGpFloat64SubtractFunction Subtract 
            => CGpFloat64SubtractFunction.Instance;
        
        public static CGpFloat64DivideFunction Divide 
            => CGpFloat64DivideFunction.Instance;
        
        public static CGpFloat64UnaryFunction Abs { get; }
            = new CGpFloat64UnaryFunction("abs", Math.Abs);

        public static CGpFloat64UnaryFunction Sqrt { get; }
            = new CGpFloat64UnaryFunction(
                "sqrt", 
                x => Math.Sqrt(Math.Abs(x))
            );
        
        public static CGpFloat64UnaryFunction Cbrt { get; }
            = new CGpFloat64UnaryFunction("Cbrt", Math.Cbrt);

        public static CGpFloat64UnaryFunction Square { get; }
            = new CGpFloat64UnaryFunction("square", x => x * x);
        
        public static CGpFloat64UnaryFunction Cube { get; }
            = new CGpFloat64UnaryFunction(
                "cube", 
                x => x * x * x
            );
        
        public static CGpFloat64BinaryFunction Power { get; }
            = new CGpFloat64BinaryFunction("power", Math.Pow);

        public static CGpFloat64UnaryFunction Exp { get; }
            = new CGpFloat64UnaryFunction("exp", Math.Exp);
        
        public static CGpFloat64UnaryFunction Ln { get; }
            = new CGpFloat64UnaryFunction("ln", Math.Log);
        
        public static CGpFloat64UnaryFunction Log10 { get; }
            = new CGpFloat64UnaryFunction("log10", Math.Log10);

        public static CGpFloat64UnaryFunction Cos { get; }
            = new CGpFloat64UnaryFunction("cos", Math.Cos);

        public static CGpFloat64UnaryFunction Sin { get; }
            = new CGpFloat64UnaryFunction("sin", Math.Sin);
        
        public static CGpFloat64UnaryFunction Tan { get; }
            = new CGpFloat64UnaryFunction("tan", Math.Tan);
        
        public static CGpFloat64UnaryFunction ArcTan { get; }
            = new CGpFloat64UnaryFunction("arcTan", Float64Utils.ArcTan);

        public static CGpFloat64BinaryFunction ArcTan2 { get; }
            = new CGpFloat64BinaryFunction("arcTan2", Float64Utils.ArcTan2);

        public static CGpFloat64UnaryFunction Cosh { get; }
            = new CGpFloat64UnaryFunction("cosh", Math.Cosh);

        public static CGpFloat64UnaryFunction Sinh { get; }
            = new CGpFloat64UnaryFunction("sinh", Math.Sinh);
        
        public static CGpFloat64UnaryFunction Tanh { get; }
            = new CGpFloat64UnaryFunction("tanh", Math.Tanh);

        public static CGpFloat64UnaryFunction Step { get; }
            = new CGpFloat64UnaryFunction(
                "step", 
                x => x < 0 ? -1d : 1d
            );
        
        public static CGpFloat64UnaryFunction SoftSign { get; }
            = new CGpFloat64UnaryFunction(
                "gaussian", 
                x => x / (1 + Math.Abs(x))
            );

        public static CGpFloat64UnaryFunction Sigmoid { get; }
            = new CGpFloat64UnaryFunction(
                "sigmoid", 
                x => 1 / (1 + Math.Exp(-x))
            );
        
        public static CGpFloat64UnaryFunction Gaussian { get; }
            = new CGpFloat64UnaryFunction(
                "gaussian", 
                x => Math.Exp(-0.5 * x * x)
            );
        
        public static CGpFloat64UnaryFunction Not { get; }
            = new CGpFloat64UnaryFunction(
                "not", 
                x => x > 0 ? -1 : 1
            );


        public static CGpFloat64ConstantFunction Constant(double value)
        {
            return new CGpFloat64ConstantFunction(value);
        }
        
        public static CGpFloat64IdentityFunction Identity 
            => CGpFloat64IdentityFunction.Instance;
        
        public static CGpFloat64NaryFunction And 
            => new CGpFloat64NaryFunction(
                "and",
                xList =>
                    xList.Any(x => x <= 0) ? -1 : 1
            );
        
        public static CGpFloat64NaryFunction Or
            => new CGpFloat64NaryFunction(
                "or",
                xList =>
                    xList.Any(x => x > 0) ? 1 : -1
            );

        public static CGpFloat64NaryFunction XOr 
            => new CGpFloat64NaryFunction(
                "or",
                xList =>
                    xList.Count(x => x > 0).IsEven() ? -1 : 1
            );

        public static CGpFloat64NaryFunction NAnd
            => new CGpFloat64NaryFunction(
                "and",
                xList =>
                    xList.Any(x => x <= 0) ? 1 : -1
            );

        public static CGpFloat64NaryFunction NOr
            => new CGpFloat64NaryFunction(
                "or",
                xList =>
                    xList.Any(x => x > 0) ? -1 : 1
            );
        
        public static CGpFloat64NaryFunction XNor
            => new CGpFloat64NaryFunction(
                "or",
                xList =>
                    xList.Count(x => x > 0).IsEven() ? 1 : -1
            );


        public abstract string Name { get; }

        public abstract Pair<int> ArityRange { get; }


        public abstract double GetOutput(IReadOnlyList<double> inputs, IReadOnlyList<double> weights);

        public override string ToString()
        {
            return Name;
        }
    }
}
