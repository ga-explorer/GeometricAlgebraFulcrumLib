using System.Data;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Lite.Maps.Space1D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.Functions.Polynomials
{
    public class DfAffinePolynomial :
        DifferentialCustomFunction,
        IAffineMap1D
    {
        public static DfAffinePolynomial Zero { get; }
            = new DfAffinePolynomial(0d, 0d);

        public static DfAffinePolynomial One { get; }
            = new DfAffinePolynomial(1d, 0d);

        public static DfAffinePolynomial MinusOne { get; }
            = new DfAffinePolynomial(-1d, 0d);

        public static DfAffinePolynomial Identity { get; }
            = new DfAffinePolynomial(0d, 1d);

        public static DfAffinePolynomial OnePlusIdentity { get; }
            = new DfAffinePolynomial(1d, 1d);

        public static DfAffinePolynomial MinusOnePlusIdentity { get; }
            = new DfAffinePolynomial(-1d, 1d);

        public static DfAffinePolynomial MinusIdentity { get; }
            = new DfAffinePolynomial(0d, -1d);

        public static DfAffinePolynomial OneMinusIdentity { get; }
            = new DfAffinePolynomial(1d, -1d);

        public static DfAffinePolynomial MinusOneMinusIdentity { get; }
            = new DfAffinePolynomial(-1d, -1d);


        public static DfAffinePolynomial Create(double scalarConstant, double scalarFactor)
        {
            if (scalarFactor.IsZero())
            {
                if (scalarConstant.IsZero()) return Zero;
                if (scalarConstant.IsOne()) return One;
                if (scalarConstant.IsMinusOne()) return MinusOne;

                return new DfAffinePolynomial(scalarConstant, scalarFactor);
            }

            if (scalarFactor.IsOne())
            {
                if (scalarConstant.IsZero()) return Identity;
                if (scalarConstant.IsOne()) return OnePlusIdentity;
                if (scalarConstant.IsMinusOne()) return MinusOnePlusIdentity;

                return new DfAffinePolynomial(scalarConstant, scalarFactor);
            }

            if (scalarFactor.IsMinusOne())
            {
                if (scalarConstant.IsZero()) return MinusIdentity;
                if (scalarConstant.IsOne()) return OneMinusIdentity;
                if (scalarConstant.IsMinusOne()) return MinusOneMinusIdentity;

                return new DfAffinePolynomial(scalarConstant, scalarFactor);
            }

            return new DfAffinePolynomial(scalarConstant, scalarFactor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfAffinePolynomial Create(double inputValue1, double inputValue2, double outputValue1, double outputValue2)
        {
            var dtInv = 1d / (inputValue2 - inputValue1);

            var scalarFactor = (outputValue2 - outputValue1) * dtInv;
            var scalarConstant = (inputValue2 * outputValue1 - inputValue1 * outputValue2) * dtInv;

            return Create(scalarConstant, scalarFactor);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfAffinePolynomial operator -(DfAffinePolynomial p1)
        {
            return new DfAffinePolynomial(-p1.ScalarConstant, -p1.ScalarFactor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfAffinePolynomial operator +(DfAffinePolynomial p1, double p2)
        {
            return new DfAffinePolynomial(
                p1.ScalarConstant + p2,
                p1.ScalarFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfAffinePolynomial operator +(double p1, DfAffinePolynomial p2)
        {
            return new DfAffinePolynomial(
                p1 + p2.ScalarConstant,
                p2.ScalarFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfAffinePolynomial operator +(DfAffinePolynomial p1, DfConstant p2)
        {
            return new DfAffinePolynomial(
                p1.ScalarConstant + p2.Value,
                p1.ScalarFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfAffinePolynomial operator +(DfConstant p1, DfAffinePolynomial p2)
        {
            return new DfAffinePolynomial(
                p1.Value + p2.ScalarConstant,
                p2.ScalarFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfAffinePolynomial operator +(DfAffinePolynomial p1, DfVar p2)
        {
            return new DfAffinePolynomial(
                p1.ScalarConstant,
                p1.ScalarFactor + 1d
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfAffinePolynomial operator +(DfVar p1, DfAffinePolynomial p2)
        {
            return new DfAffinePolynomial(
                p2.ScalarConstant,
                1d + p2.ScalarFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfAffinePolynomial operator +(DfAffinePolynomial p1, DfAffinePolynomial p2)
        {
            return new DfAffinePolynomial(
                p1.ScalarConstant + p2.ScalarConstant,
                p1.ScalarFactor + p2.ScalarFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfAffinePolynomial operator -(DfAffinePolynomial p1, double p2)
        {
            return new DfAffinePolynomial(
                p1.ScalarConstant - p2,
                p1.ScalarFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfAffinePolynomial operator -(double p1, DfAffinePolynomial p2)
        {
            return new DfAffinePolynomial(
                p1 - p2.ScalarConstant,
                -p2.ScalarFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfAffinePolynomial operator -(DfAffinePolynomial p1, DfConstant p2)
        {
            return new DfAffinePolynomial(
                p1.ScalarConstant - p2.Value,
                p1.ScalarFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfAffinePolynomial operator -(DfConstant p1, DfAffinePolynomial p2)
        {
            return new DfAffinePolynomial(
                p1.Value - p2.ScalarConstant,
                -p2.ScalarFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfAffinePolynomial operator -(DfAffinePolynomial p1, DfVar p2)
        {
            return new DfAffinePolynomial(
                p1.ScalarConstant,
                p1.ScalarFactor - 1d
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfAffinePolynomial operator -(DfVar p1, DfAffinePolynomial p2)
        {
            return new DfAffinePolynomial(
                -p2.ScalarConstant,
                1d - p2.ScalarFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfAffinePolynomial operator -(DfAffinePolynomial p1, DfAffinePolynomial p2)
        {
            return new DfAffinePolynomial(
                p1.ScalarConstant - p2.ScalarConstant,
                p1.ScalarFactor - p2.ScalarFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfAffinePolynomial operator *(DfAffinePolynomial p1, double p2)
        {
            return new DfAffinePolynomial(
                p1.ScalarConstant * p2,
                p1.ScalarFactor * p2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfAffinePolynomial operator *(double p1, DfAffinePolynomial p2)
        {
            return new DfAffinePolynomial(
                p1 * p2.ScalarConstant,
                p1 * p2.ScalarFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfAffinePolynomial operator *(DfAffinePolynomial p1, DfConstant p2)
        {
            return new DfAffinePolynomial(
                p1.ScalarConstant * p2.Value,
                p1.ScalarFactor * p2.Value
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfAffinePolynomial operator *(DfConstant p1, DfAffinePolynomial p2)
        {
            return new DfAffinePolynomial(
                p1.Value * p2.ScalarConstant,
                p1.Value * p2.ScalarFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfAffinePolynomial operator /(DfAffinePolynomial p1, double p2)
        {
            p2 = 1d / p2;

            return new DfAffinePolynomial(
                p1.ScalarConstant * p2,
                p1.ScalarFactor * p2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfAffinePolynomial operator /(DfAffinePolynomial p1, DfConstant p2)
        {
            var c2 = 1d / p2.Value;

            return new DfAffinePolynomial(
                p1.ScalarConstant * c2,
                p1.ScalarFactor * c2
            );
        }


        public double ScalarConstant { get; }

        public double ScalarFactor { get; }

        public bool IsZero
            => ScalarConstant.IsZero() &&
               ScalarFactor.IsZero();

        public bool IsOne
            => ScalarConstant.IsOne() &&
               ScalarFactor.IsZero();

        public bool IsMinusOne
            => ScalarConstant.IsMinusOne() &&
               ScalarFactor.IsZero();

        public bool IsIdentity
            => ScalarConstant.IsZero() &&
               ScalarFactor.IsOne();

        public bool IsOnePlusIdentity
            => ScalarConstant.IsOne() &&
               ScalarFactor.IsOne();

        public bool IsMinusOnePlusIdentity
            => ScalarConstant.IsMinusOne() &&
               ScalarFactor.IsOne();

        public bool IsMinusIdentity
            => ScalarConstant.IsZero() &&
               ScalarFactor.IsMinusOne();

        public bool IsOneMinusIdentity
            => ScalarConstant.IsOne() &&
               ScalarFactor.IsMinusOne();

        public bool IsMinusOneMinusIdentity
            => ScalarConstant.IsMinusOne() &&
               ScalarFactor.IsMinusOne();

        public override bool IsConstant
            => ScalarConstant.IsZero();
        
        public bool SwapsHandedness 
            => ScalarFactor < 0;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private DfAffinePolynomial(double scalarConstant, double scalarFactor)
            : base(false)
        {
            if (scalarFactor.IsNaNOrInfinite() || scalarConstant.IsNaNOrInfinite())
                throw new RowNotInTableException();

            ScalarConstant = scalarConstant;
            ScalarFactor = scalarFactor;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsSame(DfAffinePolynomial p2)
        {
            return 
                (ScalarFactor - p2.ScalarFactor).IsZero() &&
                (ScalarConstant - p2.ScalarConstant).IsZero();
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearSame(DfAffinePolynomial p2, double epsilon = 1e-12)
        {
            return 
                (ScalarFactor / p2.ScalarFactor - 1).IsNearZero(epsilon) &&
                (ScalarConstant - p2.ScalarConstant).IsNearZero(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double GetValue(double t)
        {
            return ScalarFactor * t + ScalarConstant;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Tuple<bool, DifferentialFunction> TrySimplify()
        {
            return new Tuple<bool, DifferentialFunction>(false, this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override DifferentialFunction Simplify()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override DifferentialFunction GetDerivative1()
        {
            return ScalarFactor;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DfAffinePolynomial InverseAffinePolynomial()
        {
            var scalarFactor = 1d / ScalarFactor;
            var scalarConstant = -ScalarConstant * scalarFactor;

            return new DfAffinePolynomial(
                scalarConstant,
                scalarFactor
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DfMonomialPolynomial ToMonomialPolynomial()
        {
            return DfMonomialPolynomial.Create(ScalarConstant, ScalarFactor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return ScalarConstant.IsValid() &&
                   ScalarFactor.IsValid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double MapPoint(double point)
        {
            return ScalarFactor * point + ScalarConstant;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double MapVector(double vector)
        {
            return ScalarFactor * vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SquareMatrix2 GetSquareMatrix2()
        {
            return new SquareMatrix2()
            {
                Scalar00 = ScalarFactor,
                Scalar01 = ScalarConstant,
                Scalar11 = 1d
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[,] GetArray2D()
        {
            return new double[,]
            {
                {ScalarFactor, ScalarConstant},
                {0d, 1d}
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IAffineMap1D GetInverseAffineMap()
        {
            return InverseAffinePolynomial();
        }
    }
}