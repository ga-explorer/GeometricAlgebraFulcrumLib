using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.PolynomialAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.PolynomialAlgebra.BSplines
{
    public class BSplineBasisSet<T> :
        IPolynomialBasisSet<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BSplineBasisSet<T> Create(BSplineKnotVector<T> knotVector, int degree)
        {
            return new BSplineBasisSet<T>(knotVector, degree);
        }


        public IScalarAlgebraProcessor<T> ScalarProcessor 
            => KnotVector.ScalarProcessor;

        public int Degree { get; }

        public BSplineKnotVector<T> KnotVector { get; }

        public int ControlPointsCount 
            => KnotVector.Size - Degree - 1;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal BSplineBasisSet([NotNull] BSplineKnotVector<T> knotVector, int degree)
        {
            if (degree < 1 || knotVector.Size - degree - 1 < 4)
                throw new ArgumentOutOfRangeException(nameof(degree));

            Degree = degree;
            KnotVector = knotVector;
        }


        private T GetValue(int degree, int index, T parameterValue)
        {
            if (degree == 0)
                return KnotVector.BoxCar(index, parameterValue);

            var ti = KnotVector.GetKnotValue(index);
            var ti1 = KnotVector.GetKnotValue(index + 1);

            var tin = KnotVector.GetKnotValue(index + degree);
            var tin1 = KnotVector.GetKnotValue(index + degree + 1);

            var vi = GetValue(degree - 1, index, parameterValue);
            var vi1 = GetValue(degree - 1, index + 1, parameterValue);

            var a1 = ScalarProcessor.Times(vi, ScalarProcessor.Subtract(parameterValue, ti));
            var a2 = ScalarProcessor.Subtract(tin, ti);

            var b1 = ScalarProcessor.Times(vi1, ScalarProcessor.Subtract(tin1, parameterValue));
            var b2 = ScalarProcessor.Subtract(tin1, ti1);

            var a = ScalarProcessor.IsZero(a1) || ScalarProcessor.IsZero(a2)
                ? ScalarProcessor.ScalarZero
                : ScalarProcessor.Divide(a1, a2);
            
            var b = ScalarProcessor.IsZero(b1) || ScalarProcessor.IsZero(b2)
                ? ScalarProcessor.ScalarZero
                : ScalarProcessor.Divide(b1, b2);

            return ScalarProcessor.Add(a, b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> GetValue(int index, T parameterValue)
        {
            return GetValue(Degree, index, parameterValue).CreateScalar(ScalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> GetValue(int index, T parameterValue, T termScalar)
        {
            return termScalar * GetValue(index, parameterValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> GetValue(T parameterValue, params T[] termScalarsList)
        {
            return ScalarProcessor.Add(
                termScalarsList.Select(
                    (scalar, index) => GetValue(index, parameterValue, scalar).ScalarValue
                )
            ).CreateScalar(ScalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<T> GetValues(T parameterValue)
        {
            return Enumerable
                .Range(0, Degree + 1)
                .Select(index => GetValue(index, parameterValue).ScalarValue)
                .ToArray();
        }
    }
}