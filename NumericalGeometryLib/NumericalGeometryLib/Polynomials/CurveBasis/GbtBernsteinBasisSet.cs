using System;
using System.Runtime.CompilerServices;

namespace NumericalGeometryLib.Polynomials.CurveBasis
{
    /// <summary>
    /// Generalized Blended Trigonometric Bernstein Basis
    /// See paper "Geometric modeling and applications of generalized
    /// blended trigonometric Bézier curves with shape parameters"
    /// https://link.springer.com/article/10.1186/s13662-020-03001-4
    /// </summary>
    public sealed class GbtBernsteinBasisSet :
        GbBernsteinBasisSetBase
    {
        private double _alpha;
        public double Alpha
        {
            get => _alpha;
            set
            {
                if (value is < -1d or > 1d)
                    throw new ArgumentOutOfRangeException(nameof(value));

                _alpha = value;
            }
        }

        private double _beta;
        public double Beta
        {
            get => _beta;
            set
            {
                if (value is < -1d or > 1d)
                    throw new ArgumentOutOfRangeException(nameof(value));

                _beta = value;
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GbtBernsteinBasisSet(int degree)
            : base(degree)
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double GetValueDegree20(double parameterValue)
        {
            var sin = Math.Sin(0.5d * Math.PI * parameterValue);

            return (1d - sin) * (1d - _alpha * sin);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double GetValueDegree22(double parameterValue)
        {
            var cos = Math.Cos(0.5d * Math.PI * parameterValue);

            return (1d - cos) * (1d - _beta * cos);
        }
    }
}