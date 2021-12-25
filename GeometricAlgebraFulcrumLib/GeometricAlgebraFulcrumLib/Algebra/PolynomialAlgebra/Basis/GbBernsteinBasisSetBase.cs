using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Antlr4.Runtime.Misc;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Algebra.PolynomialAlgebra.Basis
{
    public abstract class GbBernsteinBasisSetBase<T> :
        IPolynomialBasisSet<T>
    {
        public IScalarAlgebraProcessor<T> ScalarProcessor { get; }

        public int Degree { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GbBernsteinBasisSetBase([NotNull] IScalarAlgebraProcessor<T> scalarProcessor, int degree)
        {
            if (degree is < 2 or > 64)
                throw new ArgumentOutOfRangeException(nameof(degree));

            Degree = degree;
            ScalarProcessor = scalarProcessor;
        }


        public abstract T GetValueDegree20(T parameterValue);

        public abstract T GetValueDegree22(T parameterValue);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValueDegree21(T parameterValue)
        {
            return ScalarProcessor.Subtract(
                ScalarProcessor.ScalarOne,
                ScalarProcessor.Add(
                    GetValueDegree20(parameterValue), 
                    GetValueDegree22(parameterValue)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Triplet<T> GetValueDegree2(T parameterValue)
        {
            var b02 = GetValueDegree20(parameterValue);
            var b22 = GetValueDegree22(parameterValue);

            var b12 = ScalarProcessor.Subtract(
                ScalarProcessor.ScalarOne,
                ScalarProcessor.Add(b02, b22)
            );

            return new Triplet<T>(b02, b12, b22);
        }

        
        public T GetValue(int index, T parameterValue)
        {
            if (index < 0 || index > Degree)
                return ScalarProcessor.ScalarZero;

            if (Degree == 2)
                return index switch
                {
                    0 => GetValueDegree20(parameterValue),
                    2 => GetValueDegree22(parameterValue),
                    _ => GetValueDegree21(parameterValue)
                };

            if (index == Degree)
                return ScalarProcessor.Times(
                    ScalarProcessor.Power(parameterValue, Degree - 2),
                    GetValueDegree22(parameterValue)
                );

            var oneMinusParameterValue = 
                ScalarProcessor.Subtract(ScalarProcessor.ScalarOne, parameterValue);

            if (index == 0)
                return ScalarProcessor.Times(
                    ScalarProcessor.Power(oneMinusParameterValue, Degree - 2),
                    GetValueDegree20(parameterValue)
                );

            var (b02, b12, b22) = GetValueDegree2(parameterValue);
            
            if (index == Degree - 1)
                return ScalarProcessor.Add(
                    ScalarProcessor.Times(
                        ScalarProcessor.Power(parameterValue, Degree - 2),
                        b12
                    ),
                    ScalarProcessor.Times(
                        Degree - 2,
                        ScalarProcessor.Power(parameterValue, Degree - 3),
                        oneMinusParameterValue,
                        b22
                    )
                );

            if (index == 1)
                return ScalarProcessor.Add(
                    ScalarProcessor.Times(
                        Degree - 2,
                        parameterValue,
                        ScalarProcessor.Power(oneMinusParameterValue, Degree - 3),
                        b02
                    ),
                    ScalarProcessor.Times(
                        ScalarProcessor.Power(oneMinusParameterValue, Degree - 2),
                        b12
                    )
                );

            return ScalarProcessor.Add(
                ScalarProcessor.Times(
                    ScalarProcessor.BinomialCoefficient(Degree - 2, index),
                    ScalarProcessor.Power(parameterValue, index),
                    ScalarProcessor.Power(oneMinusParameterValue, Degree - 2 - index),
                    b02
                ),
                ScalarProcessor.Times(
                    ScalarProcessor.BinomialCoefficient(Degree - 2, index - 1),
                    ScalarProcessor.Power(parameterValue, index - 1),
                    ScalarProcessor.Power(oneMinusParameterValue, Degree - 2 - (index - 1)),
                    b12
                ),
                ScalarProcessor.Times(
                    ScalarProcessor.BinomialCoefficient(Degree - 2, index - 2),
                    ScalarProcessor.Power(parameterValue, index - 2),
                    ScalarProcessor.Power(oneMinusParameterValue, Degree - 2 - (index - 2)),
                    b22
                )
            );
        }
        
        private T GetValue(int index, T parameterValue, Triplet<T> degree2Values)
        {
            if (index < 0 || index > Degree)
                return ScalarProcessor.ScalarZero;

            if (Degree == 2)
                return index switch
                {
                    0 => degree2Values.Item1,
                    2 => degree2Values.Item3,
                    _ => degree2Values.Item2
                };

            if (index == Degree)
                return ScalarProcessor.Times(
                    ScalarProcessor.Power(parameterValue, Degree - 2),
                    degree2Values.Item3
                );

            var oneMinusParameterValue = 
                ScalarProcessor.Subtract(ScalarProcessor.ScalarOne, parameterValue);

            if (index == 0)
                return ScalarProcessor.Times(
                    ScalarProcessor.Power(oneMinusParameterValue, Degree - 2),
                    degree2Values.Item1
                );

            var (b02, b12, b22) = degree2Values;
            
            if (index == Degree - 1)
                return ScalarProcessor.Add(
                    ScalarProcessor.Times(
                        ScalarProcessor.Power(parameterValue, Degree - 2),
                        b12
                    ),
                    ScalarProcessor.Times(
                        Degree - 2,
                        ScalarProcessor.Power(parameterValue, Degree - 3),
                        oneMinusParameterValue,
                        b22
                    )
                );

            if (index == 1)
                return ScalarProcessor.Add(
                    ScalarProcessor.Times(
                        Degree - 2,
                        parameterValue,
                        ScalarProcessor.Power(oneMinusParameterValue, Degree - 3),
                        b02
                    ),
                    ScalarProcessor.Times(
                        ScalarProcessor.Power(oneMinusParameterValue, Degree - 2),
                        b12
                    )
                );

            return ScalarProcessor.Add(
                ScalarProcessor.Times(
                    ScalarProcessor.BinomialCoefficient(Degree - 2, index),
                    ScalarProcessor.Power(parameterValue, index),
                    ScalarProcessor.Power(oneMinusParameterValue, Degree - 2 - index),
                    b02
                ),
                ScalarProcessor.Times(
                    ScalarProcessor.BinomialCoefficient(Degree - 2, index - 1),
                    ScalarProcessor.Power(parameterValue, index - 1),
                    ScalarProcessor.Power(oneMinusParameterValue, Degree - 2 - (index - 1)),
                    b12
                ),
                ScalarProcessor.Times(
                    ScalarProcessor.BinomialCoefficient(Degree - 2, index - 2),
                    ScalarProcessor.Power(parameterValue, index - 2),
                    ScalarProcessor.Power(oneMinusParameterValue, Degree - 2 - (index - 2)),
                    b22
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue(int index, T parameterValue, T termScalar)
        {
            return ScalarProcessor.Times(
                termScalar,
                GetValue(index, parameterValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue(T parameterValue, params T[] termScalarsList)
        {
            var degree2Values = 
                GetValueDegree2(parameterValue);

            return ScalarProcessor.Add(
                termScalarsList.Select((termScalar, index) => 
                    ScalarProcessor.Times(
                        termScalar,
                        GetValue(index, parameterValue, degree2Values)
                    )
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<T> GetValues(T parameterValue)
        {
            var degree2Values = 
                GetValueDegree2(parameterValue);

            return Enumerable.Range(0, Degree + 1).Select(
                index => GetValue(index, parameterValue, degree2Values)
            ).ToArray();
        }
    }
}