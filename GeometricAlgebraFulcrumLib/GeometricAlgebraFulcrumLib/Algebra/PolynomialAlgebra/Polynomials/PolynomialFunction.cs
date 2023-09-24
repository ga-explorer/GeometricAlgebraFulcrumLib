using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.PolynomialAlgebra.Polynomials
{
    public class PolynomialFunction<T> :
        IReadOnlyList<Scalar<T>>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PolynomialFunction<T> CreateZero(IScalarProcessor<T> scalarProcessor)
        {
            return new PolynomialFunction<T>(
                scalarProcessor, 
                new[] {scalarProcessor.ScalarZero}
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PolynomialFunction<T> Create(IScalarProcessor<T> scalarProcessor, params T[] monomialCoefficients)
        {
            return new PolynomialFunction<T>(
                scalarProcessor, 
                monomialCoefficients.Select(t => t ?? scalarProcessor.ScalarZero).ToArray()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PolynomialFunction<T> Create(IScalarProcessor<T> scalarProcessor, IEnumerable<T> monomialCoefficients)
        {
            return new PolynomialFunction<T>(
                scalarProcessor, 
                monomialCoefficients.Select(t => t ?? scalarProcessor.ScalarZero).ToArray()
            );
        }


        private readonly T[] _monomialCoefficientArray;

        public IScalarProcessor<T> ScalarProcessor { get; }

        public int Degree 
            => _monomialCoefficientArray.Length - 1;

        public int Count 
            => _monomialCoefficientArray.Length;

        public Scalar<T> this[int index] 
            => _monomialCoefficientArray[index].CreateScalar(ScalarProcessor);

        public IEnumerable<Scalar<T>> MonomialCoefficients
            => _monomialCoefficientArray.Select(c => c.CreateScalar(ScalarProcessor));


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private PolynomialFunction(IScalarProcessor<T> scalarProcessor, T[] monomialCoefficientArray)
        {
            ScalarProcessor = scalarProcessor;
            _monomialCoefficientArray = monomialCoefficientArray;
        }


        public T GetValue(T t)
        {
            var value = ScalarProcessor.ScalarZero;

            for (var i = 0; i < _monomialCoefficientArray.Length; i++)
            {
                var ci = _monomialCoefficientArray[i];
                var ti = ScalarProcessor.Power(t, i);

                value = ScalarProcessor.Add(value, ScalarProcessor.Times(ci, ti));
            }

            return value;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> GetValue(Scalar<T> t)
        {
            return GetValue(t.ScalarValue).CreateScalar(t.ScalarProcessor);
        }
        
        public T GetValueDt1(T t)
        {
            var value = ScalarProcessor.ScalarZero;

            for (var i = 1; i < _monomialCoefficientArray.Length; i++)
            {
                var ci = ScalarProcessor.Times(i, _monomialCoefficientArray[i]);
                var ti = ScalarProcessor.Power(t, i);

                value = ScalarProcessor.Add(
                    value, 
                    ScalarProcessor.Times(ci, ti)
                );
            }

            return value;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> GetValueDt1(Scalar<T> t)
        {
            return GetValueDt1(t.ScalarValue).CreateScalar(t.ScalarProcessor);
        }

        public T GetValueDt2(T t)
        {
            var value = ScalarProcessor.ScalarZero;

            for (var i = 2; i < _monomialCoefficientArray.Length; i++)
            {
                var ci = ScalarProcessor.Times((i - 1) * i, _monomialCoefficientArray[i]);
                var ti = ScalarProcessor.Power(t, i);

                value = ScalarProcessor.Add(
                    value, 
                    ScalarProcessor.Times(ci, ti)
                );
            }

            return value;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> GetValueDt2(Scalar<T> t)
        {
            return GetValueDt2(t.ScalarValue).CreateScalar(t.ScalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetValues(IEnumerable<T> tList)
        {
            return tList.Select(GetValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<Scalar<T>> GetValues(IEnumerable<Scalar<T>> tList)
        {
            return tList.Select(GetValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetValuesDt1(IEnumerable<T> tList)
        {
            return tList.Select(GetValueDt1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<Scalar<T>> GetValuesDt1(IEnumerable<Scalar<T>> tList)
        {
            return tList.Select(GetValueDt1);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetValuesDt2(IEnumerable<T> tList)
        {
            return tList.Select(GetValueDt2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<Scalar<T>> GetValuesDt2(IEnumerable<Scalar<T>> tList)
        {
            return tList.Select(GetValueDt2);
        }

        private PolynomialFunction<T> GetFirstDerivative()
        {
            var monomialCoefficientArray = new T[_monomialCoefficientArray.Length - 1];
            monomialCoefficientArray[0] = _monomialCoefficientArray[1];

            for (var i = 2; i < _monomialCoefficientArray.Length; i++)
            {
                monomialCoefficientArray[i - 1] =
                    ScalarProcessor.Times(i, _monomialCoefficientArray[i]);
            }

            return new PolynomialFunction<T>(ScalarProcessor, monomialCoefficientArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PolynomialFunction<T> GetDerivative1()
        {
            return Degree < 1 
                ? CreateZero(ScalarProcessor) 
                : GetFirstDerivative();
        }

        public PolynomialFunction<T> GetDerivative(int degree = 1)
        {
            if (degree < 0)
                throw new ArgumentOutOfRangeException(nameof(degree));

            if (degree == 0)
                return this;

            if (degree > Degree)
                return CreateZero(ScalarProcessor);

            var derivative = this;
            while (degree > 0)
            {
                derivative = derivative.GetFirstDerivative();

                degree--;
            }

            return derivative;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<Scalar<T>> GetEnumerator()
        {
            return _monomialCoefficientArray
                .Select(c => c.CreateScalar(ScalarProcessor))
                .GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
