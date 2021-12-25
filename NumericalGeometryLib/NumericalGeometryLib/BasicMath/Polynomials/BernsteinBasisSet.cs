using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Combinations;

namespace NumericalGeometryLib.BasicMath.Polynomials
{
    public sealed class BernsteinBasisSet :
        IPolynomialBasisSet
    {
        public int Degree { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BernsteinBasisSet(int degree)
        {
            if (degree is < 0 or > 64)
                throw new ArgumentOutOfRangeException(nameof(degree));

            Degree = degree;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetValue(int index, double parameterValue)
        {
            if (index < 0 || index > Degree)
                return 0d;

            var parameterValueMinusOne = 1 - parameterValue;

            return Degree.GetBinomialCoefficient(index) *
                   Math.Pow(parameterValue, index) *
                   Math.Pow(parameterValueMinusOne, Degree - index);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetValue(int index, double parameterValue, double termScalar)
        {
            return termScalar * GetValue(index, parameterValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetValue(double parameterValue, params double[] termScalarsList)
        {
            return termScalarsList.Select(
                (termScalar, index) => 
                    GetValue(index, parameterValue, termScalar)
            ).Sum();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<double> GetValues(double parameterValue)
        {
            return Enumerable.Range(0, Degree + 1).Select(
                index => GetValue(index, parameterValue)
            ).ToArray();
        }
    }
}