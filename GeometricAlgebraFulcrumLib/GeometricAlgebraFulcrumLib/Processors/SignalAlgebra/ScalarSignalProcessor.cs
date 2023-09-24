using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using DataStructuresLib.Collections;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Processors.SignalAlgebra
{
    public sealed class ScalarSignalProcessor<T> :
        IScalarProcessor<IReadOnlyList<T>>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static ScalarSignalProcessor<T> Create(IScalarProcessor<T> scalarProcessor, int signalSamplesCount)
        {
            return new ScalarSignalProcessor<T>(scalarProcessor, signalSamplesCount);
        }


        public int SignalSamplesCount { get; }

        public IScalarProcessor<T> ScalarProcessor { get; }

        public bool IsNumeric 
            => ScalarProcessor.IsNumeric;

        public bool IsSymbolic 
            => ScalarProcessor.IsSymbolic;

        public IReadOnlyList<T> ScalarZero { get; }
        
        public IReadOnlyList<T> ScalarOne { get; }
        
        public IReadOnlyList<T> ScalarMinusOne { get; }
        
        public IReadOnlyList<T> ScalarTwo { get; }
        
        public IReadOnlyList<T> ScalarMinusTwo { get; }
        
        public IReadOnlyList<T> ScalarTen { get; }
        
        public IReadOnlyList<T> ScalarMinusTen { get; }
        
        public IReadOnlyList<T> ScalarPi { get; }

        public IReadOnlyList<T> ScalarTwoPi { get; }

        public IReadOnlyList<T> ScalarPiOver2 { get; }
        
        public IReadOnlyList<T> ScalarE { get; }

        public IReadOnlyList<T> ScalarDegreeToRadian { get; }
        
        public IReadOnlyList<T> ScalarRadianToDegree { get; }


        public ScalarSignalProcessor(IScalarProcessor<T> scalarProcessor, int signalSamplesCount)
        {
            if (signalSamplesCount < 1)
                throw new ArgumentOutOfRangeException(nameof(signalSamplesCount));

            SignalSamplesCount = signalSamplesCount;
            ScalarProcessor = scalarProcessor;

            ScalarZero = new RepeatedItemReadOnlyList<T>(ScalarProcessor.ScalarZero, SignalSamplesCount);
            ScalarOne = new RepeatedItemReadOnlyList<T>(ScalarProcessor.ScalarOne, SignalSamplesCount);
            ScalarMinusOne = new RepeatedItemReadOnlyList<T>(ScalarProcessor.ScalarMinusOne, SignalSamplesCount);
            ScalarTwo = new RepeatedItemReadOnlyList<T>(ScalarProcessor.ScalarTwo, SignalSamplesCount);
            ScalarMinusTwo = new RepeatedItemReadOnlyList<T>(ScalarProcessor.ScalarMinusTwo, SignalSamplesCount);
            ScalarTen = new RepeatedItemReadOnlyList<T>(ScalarProcessor.ScalarTen, SignalSamplesCount);
            ScalarMinusTen = new RepeatedItemReadOnlyList<T>(ScalarProcessor.ScalarMinusTen, SignalSamplesCount);
            ScalarPi = new RepeatedItemReadOnlyList<T>(ScalarProcessor.ScalarPi, SignalSamplesCount);
            ScalarTwoPi = new RepeatedItemReadOnlyList<T>(ScalarProcessor.ScalarTwoPi, SignalSamplesCount);
            ScalarPiOver2 = new RepeatedItemReadOnlyList<T>(ScalarProcessor.ScalarPiOver2, SignalSamplesCount);
            ScalarE = new RepeatedItemReadOnlyList<T>(ScalarProcessor.ScalarE, SignalSamplesCount);
            ScalarDegreeToRadian = new RepeatedItemReadOnlyList<T>(ScalarProcessor.ScalarDegreeToRadian, SignalSamplesCount);
            ScalarRadianToDegree = new RepeatedItemReadOnlyList<T>(ScalarProcessor.ScalarRadianToDegree, SignalSamplesCount);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<T> GetScalarFromNumber(int value)
        {
            return new RepeatedItemReadOnlyList<T>(
                ScalarProcessor.GetScalarFromNumber(value), 
                SignalSamplesCount
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<T> GetScalarFromNumber(uint value)
        {
            return new RepeatedItemReadOnlyList<T>(
                ScalarProcessor.GetScalarFromNumber(value), 
                SignalSamplesCount
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<T> GetScalarFromNumber(long value)
        {
            return new RepeatedItemReadOnlyList<T>(
                ScalarProcessor.GetScalarFromNumber(value), 
                SignalSamplesCount
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<T> GetScalarFromNumber(ulong value)
        {
            return new RepeatedItemReadOnlyList<T>(
                ScalarProcessor.GetScalarFromNumber(value), 
                SignalSamplesCount
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<T> GetScalarFromNumber(float value)
        {
            return new RepeatedItemReadOnlyList<T>(
                ScalarProcessor.GetScalarFromNumber(value), 
                SignalSamplesCount
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<T> GetScalarFromNumber(double value)
        {
            return new RepeatedItemReadOnlyList<T>(
                ScalarProcessor.GetScalarFromNumber(value), 
                SignalSamplesCount
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<T> GetScalarFromRational(long numerator, long denominator)
        {
            return new RepeatedItemReadOnlyList<T>(
                ScalarProcessor.GetScalarFromRational(numerator, denominator), 
                SignalSamplesCount
            );
        }

        public IReadOnlyList<T> GetScalarFromRandom(Random randomGenerator, double minValue, double maxValue)
        {
            var scalarArray = new T[SignalSamplesCount];

            for (var i = 0; i < SignalSamplesCount; i++)
            {
                scalarArray[i] = ScalarProcessor.GetScalarFromRandom(
                    randomGenerator, 
                    minValue, 
                    maxValue
                );
            }

            return scalarArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<T> GetScalarFromText(string text)
        {
            return new RepeatedItemReadOnlyList<T>(
                ScalarProcessor.GetScalarFromText(text), 
                SignalSamplesCount
            );
        }
        
        private IReadOnlyList<T> UnaryOperation(IReadOnlyList<T> scalar1, Func<T, T> scalarMapping)
        {
            var count1 = scalar1.Count;

            var scalarArray = new T[SignalSamplesCount];

            for (var i = 0; i < SignalSamplesCount; i++)
            {
                var s1 = i < count1 ? scalar1[i] : ScalarProcessor.ScalarZero;

                scalarArray[i] = scalarMapping(s1);
            }

            return scalarArray;
        }

        private IReadOnlyList<T> BinaryOperation(IReadOnlyList<T> scalar1, IReadOnlyList<T> scalar2, Func<T, T, T> scalarMapping)
        {
            var count1 = scalar1.Count;
            var count2 = scalar2.Count;

            var scalarArray = new T[SignalSamplesCount];

            for (var i = 0; i < SignalSamplesCount; i++)
            {
                var s1 = i < count1 ? scalar1[i] : ScalarProcessor.ScalarZero;
                var s2 = i < count2 ? scalar2[i] : ScalarProcessor.ScalarZero;

                scalarArray[i] = scalarMapping(s1, s2);
            }

            return scalarArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<T> Add(IReadOnlyList<T> scalar1, IReadOnlyList<T> scalar2)
        {
            return BinaryOperation(scalar1, scalar2, ScalarProcessor.Add);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<T> Subtract(IReadOnlyList<T> scalar1, IReadOnlyList<T> scalar2)
        {
            return BinaryOperation(scalar1, scalar2, ScalarProcessor.Subtract);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<T> Times(IReadOnlyList<T> scalar1, IReadOnlyList<T> scalar2)
        {
            return BinaryOperation(scalar1, scalar2, ScalarProcessor.Times);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<T> Times(IntegerSign sign, IReadOnlyList<T> scalar)
        {
            if (sign.IsZero) return ScalarZero;

            return sign.IsPositive
                ? scalar 
                : Negative(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<T> NegativeTimes(IReadOnlyList<T> scalar1, IReadOnlyList<T> scalar2)
        {
            return BinaryOperation(scalar1, scalar2, ScalarProcessor.NegativeTimes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<T> Divide(IReadOnlyList<T> scalar1, IReadOnlyList<T> scalar2)
        {
            return BinaryOperation(scalar1, scalar2, ScalarProcessor.Divide);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<T> NegativeDivide(IReadOnlyList<T> scalar1, IReadOnlyList<T> scalar2)
        {
            return BinaryOperation(scalar1, scalar2, ScalarProcessor.NegativeDivide);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<T> Positive(IReadOnlyList<T> scalar)
        {
            return UnaryOperation(scalar, ScalarProcessor.Positive);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<T> Negative(IReadOnlyList<T> scalar)
        {
            return UnaryOperation(scalar, ScalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<T> Inverse(IReadOnlyList<T> scalar)
        {
            return UnaryOperation(scalar, ScalarProcessor.Inverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<T> Sign(IReadOnlyList<T> scalar)
        {
            return UnaryOperation(scalar, ScalarProcessor.Sign);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<T> UnitStep(IReadOnlyList<T> scalar)
        {
            return UnaryOperation(scalar, ScalarProcessor.UnitStep);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<T> Abs(IReadOnlyList<T> scalar)
        {
            return UnaryOperation(scalar, ScalarProcessor.Abs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<T> Power(IReadOnlyList<T> baseScalar, IReadOnlyList<T> scalar)
        {
            return BinaryOperation(baseScalar, scalar, ScalarProcessor.Power);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<T> Sqrt(IReadOnlyList<T> scalar)
        {
            return UnaryOperation(scalar, ScalarProcessor.Sqrt);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<T> SqrtOfAbs(IReadOnlyList<T> scalar)
        {
            return UnaryOperation(scalar, ScalarProcessor.SqrtOfAbs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<T> Exp(IReadOnlyList<T> scalar)
        {
            return UnaryOperation(scalar, ScalarProcessor.Exp);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<T> LogE(IReadOnlyList<T> scalar)
        {
            return UnaryOperation(scalar, ScalarProcessor.LogE);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<T> Log2(IReadOnlyList<T> scalar)
        {
            return UnaryOperation(scalar, ScalarProcessor.Log2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<T> Log10(IReadOnlyList<T> scalar)
        {
            return UnaryOperation(scalar, ScalarProcessor.Log10);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<T> Log(IReadOnlyList<T> baseScalar, IReadOnlyList<T> scalar)
        {
            return BinaryOperation(baseScalar, scalar, ScalarProcessor.Log);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<T> Cos(IReadOnlyList<T> scalar)
        {
            return UnaryOperation(scalar, ScalarProcessor.Cos);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<T> Sin(IReadOnlyList<T> scalar)
        {
            return UnaryOperation(scalar, ScalarProcessor.Sin);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<T> Tan(IReadOnlyList<T> scalar)
        {
            return UnaryOperation(scalar, ScalarProcessor.Tan);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<T> ArcCos(IReadOnlyList<T> scalar)
        {
            return UnaryOperation(scalar, ScalarProcessor.ArcCos);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<T> ArcSin(IReadOnlyList<T> scalar)
        {
            return UnaryOperation(scalar, ScalarProcessor.ArcSin);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<T> ArcTan(IReadOnlyList<T> scalar)
        {
            return UnaryOperation(scalar, ScalarProcessor.ArcTan);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<T> ArcTan2(IReadOnlyList<T> scalarX, IReadOnlyList<T> scalarY)
        {
            return BinaryOperation(scalarX, scalarY, ScalarProcessor.ArcTan2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<T> Cosh(IReadOnlyList<T> scalar)
        {
            return UnaryOperation(scalar, ScalarProcessor.Cosh);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<T> Sinh(IReadOnlyList<T> scalar)
        {
            return UnaryOperation(scalar, ScalarProcessor.Sinh);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<T> Tanh(IReadOnlyList<T> scalar)
        {
            return UnaryOperation(scalar, ScalarProcessor.Tanh);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<T> Sinc(IReadOnlyList<T> scalar)
        {
            return UnaryOperation(scalar, ScalarProcessor.Sinc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid(IReadOnlyList<T> scalar)
        {
            return scalar.All(ScalarProcessor.IsValid);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsFiniteNumber(IReadOnlyList<T> scalar)
        {
            return scalar.All(ScalarProcessor.IsFiniteNumber);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero(IReadOnlyList<T> scalar)
        {
            return scalar.All(ScalarProcessor.IsZero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero(IReadOnlyList<T> scalar, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? IsNearZero(scalar) 
                : IsZero(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearZero(IReadOnlyList<T> scalar)
        {
            return scalar.All(ScalarProcessor.IsNearZero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotZero(IReadOnlyList<T> scalar)
        {
            return scalar.All(ScalarProcessor.IsNotZero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotZero(IReadOnlyList<T> scalar, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? IsNotNearZero(scalar) 
                : IsNotZero(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearZero(IReadOnlyList<T> scalar)
        {
            return scalar.All(ScalarProcessor.IsNotNearZero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsPositive(IReadOnlyList<T> scalar)
        {
            return scalar.All(ScalarProcessor.IsPositive);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNegative(IReadOnlyList<T> scalar)
        {
            return scalar.All(ScalarProcessor.IsNegative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotPositive(IReadOnlyList<T> scalar)
        {
            return scalar.All(ScalarProcessor.IsNotPositive);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNegative(IReadOnlyList<T> scalar)
        {
            return scalar.All(ScalarProcessor.IsNotNegative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearPositive(IReadOnlyList<T> scalar)
        {
            return scalar.All(ScalarProcessor.IsNotNearPositive);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearNegative(IReadOnlyList<T> scalar)
        {
            return scalar.All(ScalarProcessor.IsNotNearNegative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToText(IReadOnlyList<T> scalar)
        {
            throw new NotImplementedException();
        }
    }
}