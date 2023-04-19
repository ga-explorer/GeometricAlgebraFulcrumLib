using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;

namespace GeometricAlgebraFulcrumLib.Algebra.PolynomialAlgebra.BSplines
{
    public sealed class BSplineKnotVector<T> :
        IReadOnlyList<BSplineKnot<T>>
    {
        private readonly List<BSplineKnot<T>> _knotList
            = new List<BSplineKnot<T>>();


        public IScalarProcessor<T> ScalarProcessor { get; }

        public int Size 
            => _knotList.Count == 0 
                ? 0 : _knotList[^1].Index2 + 1;

        public int Count 
            => _knotList.Count;

        public BSplineKnot<T> this[int index] 
            => _knotList[index];

        public BSplineKnot<T> FirstKnot 
            => _knotList[0];

        public int FirstKnotMultiplicity 
            => _knotList[0].Multiplicity;
        
        public T FirstKnotValue 
            => _knotList[0].Value;

        public BSplineKnot<T> LastKnot 
            => _knotList[^1];

        public int LastKnotMultiplicity 
            => _knotList[^1].Multiplicity;
        
        public T LastKnotValue 
            => _knotList[^1].Value;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BSplineKnotVector(IScalarProcessor<T> scalarProcessor)
        {
            ScalarProcessor = scalarProcessor;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BSplineKnotVector<T> AppendKnot(T value)
        {
            _knotList.Add(
                new BSplineKnot<T>(Size, value, 1)
            );

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BSplineKnotVector<T> AppendKnot(T value, int multiplicity)
        {
            _knotList.Add(
                new BSplineKnot<T>(Size, value, multiplicity)
            );

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetKnotValue(int index)
        {
            return _knotList.First(knot => knot.ContainsIndex(index)).Value;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetKnotValueDifference(int index1, int index2)
        {
            return ScalarProcessor.Subtract(
                _knotList.First(knot => knot.ContainsIndex(index1)).Value,
                _knotList.First(knot => knot.ContainsIndex(index2)).Value
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T BoxCar(int index, T value)
        {
            if (index < 0 || index >= Size)
                throw new IndexOutOfRangeException(nameof(index));

            for (var knotIndex = 0; knotIndex < _knotList.Count; knotIndex++)
            {
                var knot = _knotList[knotIndex];
                var n = knot.Multiplicity;

                if (index >= n)
                {
                    index -= n;
                    continue;
                }

                if (index < n - 1 || knotIndex == _knotList.Count - 1)
                    return ScalarProcessor.ScalarZero;

                var value1 = knot.Value;
                var value2 = _knotList[knotIndex + 1].Value;

                return ScalarProcessor.BoxCar(value, value1, value2);
            }

            return ScalarProcessor.ScalarZero;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetKnotValues()
        {
            foreach (var knot in _knotList)
            {
                var n = knot.Multiplicity;

                while (n > 0)
                {
                    yield return knot.Value;

                    n--;
                }
            }
        }

        public BSplineKnotVector<T> ScaleMultiplicity(int scalingFactor)
        {
            if (scalingFactor < 0)
                throw new ArgumentOutOfRangeException(nameof(scalingFactor));

            var knotVector = new BSplineKnotVector<T>(ScalarProcessor);

            foreach (var knot in _knotList)
                knotVector._knotList.Add(new BSplineKnot<T>(
                    knot.Index1,
                    knot.Value,
                    knot.Multiplicity * scalingFactor
                ));

            return knotVector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BSplineBasisSet<T> CreateBSplineBasisSet(int degree)
        {
            return new BSplineBasisSet<T>(this, degree);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<BSplineKnot<T>> GetEnumerator()
        {
            return _knotList.GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
