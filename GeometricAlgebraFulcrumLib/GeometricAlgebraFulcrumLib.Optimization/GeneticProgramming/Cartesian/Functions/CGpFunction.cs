using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Normalized;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian.Functions
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
        
        public static CGpFloat64MeanFunction Mean 
            => CGpFloat64MeanFunction.Instance;

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


        /// <summary>
        /// All functions defined here have input\output range in [-1, 1]
        /// </summary>
        public static class UnitRange
        {
            public static CGpFloat64UnaryFunction UNegative
                => new CGpFloat64UnaryFunction(
                    "uNegative",
                    UMath.Negative
                );

            public static CGpFloat64UnaryFunction UAbs
                => new CGpFloat64UnaryFunction(
                    "uAbs",
                    UMath.Abs
                );

            public static CGpFloat64UnaryFunction UReciprocal
                => new CGpFloat64UnaryFunction(
                    "uReciprocal",
                    UMath.Reciprocal
                );

            public static CGpFloat64UnaryFunction USquare
                => new CGpFloat64UnaryFunction(
                    "uSquare",
                    UMath.Square
                );
            
            public static CGpFloat64UnaryFunction UCube
                => new CGpFloat64UnaryFunction(
                    "uCube",
                    UMath.Cube
                );
            
            public static CGpFloat64UnaryFunction USqrt
                => new CGpFloat64UnaryFunction(
                    "uSqrt",
                    UMath.Sqrt
                );
            
            public static CGpFloat64UnaryFunction UCbrt
                => new CGpFloat64UnaryFunction(
                    "uCbrt",
                    UMath.Cbrt
                );

            public static CGpFloat64UnaryFunction UCos
                => new CGpFloat64UnaryFunction(
                    "uCos",
                    UMath.Cos
                );
            
            public static CGpFloat64UnaryFunction USin
                => new CGpFloat64UnaryFunction(
                    "uSin",
                    UMath.Sin
                );
            
            public static CGpFloat64UnaryFunction UTan
                => new CGpFloat64UnaryFunction(
                    "uTan",
                    UMath.Tan
                );
            
            public static CGpFloat64UnaryFunction UExp
                => new CGpFloat64UnaryFunction(
                    "uExp",
                    UMath.Exp
                );
            
            public static CGpFloat64BinaryFunction UMean
                => new CGpFloat64BinaryFunction(
                    "uMean",
                    UMath.Mean
                );
            
            public static CGpFloat64BinaryFunction UTimes
                => new CGpFloat64BinaryFunction(
                    "uTimes",
                    UMath.Times
                );

            public static CGpFloat64UnaryFunction ChebyshevType1(int n)
            {
                if (n < 1)
                    throw new ArgumentOutOfRangeException(nameof(n));

                return new CGpFloat64UnaryFunction(
                    $"T{n}",
                    x => Math.Cos(n * Math.Acos(x))
                );
            }

            /// <summary>
            /// Chebyshev polynomial of the first kind of degree 1
            /// </summary>
            public static CGpFloat64UnaryFunction T1
                => new CGpFloat64UnaryFunction(
                    "T1",
                    x => x
                );

            /// <summary>
            /// Chebyshev polynomial of the first kind of degree 2
            /// </summary>
            public static CGpFloat64UnaryFunction T2
                => new CGpFloat64UnaryFunction(
                    "T2",
                    x => 2 * x * x - 1
                );
            
            /// <summary>
            /// Chebyshev polynomial of the first kind of degree 3
            /// </summary>
            public static CGpFloat64UnaryFunction T3
                => new CGpFloat64UnaryFunction(
                    "T3",
                    x => 4 * x * x * x - 3 * x
                );
            
            /// <summary>
            /// Chebyshev polynomial of the first kind of degree 4
            /// </summary>
            public static CGpFloat64UnaryFunction T4
                => new CGpFloat64UnaryFunction(
                    "T4",
                    x => 8 * x.Power(4) - 8 * x * x + 1
                );
            
            /// <summary>
            /// Chebyshev polynomial of the first kind of degree 5
            /// </summary>
            public static CGpFloat64UnaryFunction T5
                => new CGpFloat64UnaryFunction(
                    "T5",
                    x => 16 * x.Power(5) - 20 * x * x * x + 5 * x
                );
            
        }


        public abstract string Name { get; }

        public abstract Pair<int> ArityRange { get; }


        public abstract double GetOutput(IReadOnlyList<double> inputs, IReadOnlyList<double> weights);

        public override string ToString()
        {
            return Name;
        }
    }
}
