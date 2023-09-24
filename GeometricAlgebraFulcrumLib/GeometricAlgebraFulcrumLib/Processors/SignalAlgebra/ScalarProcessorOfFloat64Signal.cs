using System;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Lite.SignalAlgebra;
using TextComposerLib.Text;

namespace GeometricAlgebraFulcrumLib.Processors.SignalAlgebra
{
    public sealed class ScalarProcessorOfFloat64Signal :
        INumericScalarProcessor<Float64Signal>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static ScalarProcessorOfFloat64Signal Create(double samplingRate, int sampleCount)
        {
            return new ScalarProcessorOfFloat64Signal(samplingRate, sampleCount);
        }


        public int SampleCount { get; }

        public double SamplingRate { get; }

        public IScalarProcessor<double> ScalarProcessor 
            => ScalarProcessorOfFloat64.DefaultProcessor;

        public bool IsNumeric 
            => ScalarProcessor.IsNumeric;

        public bool IsSymbolic 
            => ScalarProcessor.IsSymbolic;

        public Float64Signal ScalarZero { get; }
        
        public Float64Signal ScalarOne { get; }
        
        public Float64Signal ScalarMinusOne { get; }
        
        public Float64Signal ScalarTwo { get; }
        
        public Float64Signal ScalarMinusTwo { get; }
        
        public Float64Signal ScalarTen { get; }
        
        public Float64Signal ScalarMinusTen { get; }
        
        public Float64Signal ScalarPi { get; }

        public Float64Signal ScalarTwoPi { get; }

        public Float64Signal ScalarPiOver2 { get; }
        
        public Float64Signal ScalarE { get; }

        public Float64Signal ScalarDegreeToRadian { get; }

        public Float64Signal ScalarRadianToDegree { get; }


        public ScalarProcessorOfFloat64Signal(double samplingRate, int signalSamplesCount)
        {
            if (signalSamplesCount < 1)
                throw new ArgumentOutOfRangeException(nameof(signalSamplesCount));

            if (samplingRate <= 0)
                throw new ArgumentOutOfRangeException(nameof(samplingRate));

            SamplingRate = samplingRate;
            SampleCount = signalSamplesCount;

            ScalarZero = GetReadOnlyScalarFromNumber(ScalarProcessor.ScalarZero);
            ScalarOne = GetReadOnlyScalarFromNumber(ScalarProcessor.ScalarOne);
            ScalarMinusOne = GetReadOnlyScalarFromNumber(ScalarProcessor.ScalarMinusOne);
            ScalarTwo = GetReadOnlyScalarFromNumber(ScalarProcessor.ScalarTwo);
            ScalarMinusTwo = GetReadOnlyScalarFromNumber(ScalarProcessor.ScalarMinusTwo);
            ScalarTen = GetReadOnlyScalarFromNumber(ScalarProcessor.ScalarTen);
            ScalarMinusTen = GetReadOnlyScalarFromNumber(ScalarProcessor.ScalarMinusTen);
            ScalarPi = GetReadOnlyScalarFromNumber(ScalarProcessor.ScalarPi);
            ScalarTwoPi = GetReadOnlyScalarFromNumber(ScalarProcessor.ScalarTwoPi);
            ScalarPiOver2 = GetReadOnlyScalarFromNumber(ScalarProcessor.ScalarPiOver2);
            ScalarE = GetReadOnlyScalarFromNumber(ScalarProcessor.ScalarE);
            ScalarDegreeToRadian = GetReadOnlyScalarFromNumber(ScalarProcessor.ScalarDegreeToRadian);
            ScalarRadianToDegree = GetReadOnlyScalarFromNumber(ScalarProcessor.ScalarRadianToDegree);
        }


        public Float64Signal GetReadOnlyScalarFromNumber(double value)
        {
            return Float64Signal.CreateConstant(
                SamplingRate, 
                SampleCount,
                value,
                true
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Signal GetScalarFromNumber(int value)
        {
            return Float64Signal.CreateConstant(
                SamplingRate, 
                SampleCount,
                value,
                false
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Signal GetScalarFromNumber(uint value)
        {
            return Float64Signal.CreateConstant(
                SamplingRate, 
                SampleCount,
                value,
                false
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Signal GetScalarFromNumber(long value)
        {
            return Float64Signal.CreateConstant(
                SamplingRate, 
                SampleCount,
                value,
                false
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Signal GetScalarFromNumber(ulong value)
        {
            return Float64Signal.CreateConstant(
                SamplingRate, 
                SampleCount,
                value,
                false
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Signal GetScalarFromNumber(float value)
        {
            return Float64Signal.CreateConstant(
                SamplingRate, 
                SampleCount,
                value,
                false
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Signal GetScalarFromNumber(double value)
        {
            return Float64Signal.CreateConstant(
                SamplingRate, 
                SampleCount,
                value,
                false
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Signal GetScalarFromRational(long numerator, long denominator)
        {
            return Float64Signal.CreateConstant(
                SamplingRate, 
                SampleCount,
                numerator / (double) denominator,
                false
            );
        }

        public Float64Signal GetScalarFromRandom(Random randomGenerator, double minValue, double maxValue)
        {
            var scalarArray = new double[SampleCount];

            for (var i = 0; i < SampleCount; i++)
            {
                scalarArray[i] = ScalarProcessor.GetScalarFromRandom(
                    randomGenerator, 
                    minValue, 
                    maxValue
                );
            }

            return Float64Signal.Create(SamplingRate, scalarArray, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Signal GetScalarFromText(string text)
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Signal Add(Float64Signal scalar1, Float64Signal scalar2)
        {
            return scalar1.MapSamples(scalar2, ScalarProcessor.Add);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Signal Subtract(Float64Signal scalar1, Float64Signal scalar2)
        {
            return scalar1.MapSamples(scalar2, ScalarProcessor.Subtract);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Signal Times(Float64Signal scalar1, Float64Signal scalar2)
        {
            return scalar1.MapSamples(
                scalar2, 
                (s1, s2) => ScalarProcessor.Times(s1, s2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Signal Times(IntegerSign sign, Float64Signal scalar)
        {
            if (sign.IsZero) return ScalarZero;

            return sign.IsPositive 
                ? scalar 
                : scalar.MapSamples(ScalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Signal NegativeTimes(Float64Signal scalar1, Float64Signal scalar2)
        {
            return scalar1.MapSamples(scalar2, ScalarProcessor.NegativeTimes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Signal Divide(Float64Signal scalar1, Float64Signal scalar2)
        {
            return scalar1.MapSamples(scalar2, ScalarProcessor.Divide);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Signal NegativeDivide(Float64Signal scalar1, Float64Signal scalar2)
        {
            return scalar1.MapSamples(scalar2, ScalarProcessor.NegativeDivide);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Signal Positive(Float64Signal scalar)
        {
            return scalar.MapSamples(ScalarProcessor.Positive);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Signal Negative(Float64Signal scalar)
        {
            return scalar.MapSamples(ScalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Signal Inverse(Float64Signal scalar)
        {
            return scalar.MapSamples(ScalarProcessor.Inverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Signal Sign(Float64Signal scalar)
        {
            return scalar.MapSamples(
                s => ScalarProcessor.Sign(s)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Signal UnitStep(Float64Signal scalar)
        {
            return scalar.MapSamples(ScalarProcessor.UnitStep);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Signal Abs(Float64Signal scalar)
        {
            return scalar.MapSamples(ScalarProcessor.Abs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Signal Power(Float64Signal baseScalar, Float64Signal scalar)
        {
            return baseScalar.MapSamples(scalar, ScalarProcessor.Power);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Signal Sqrt(Float64Signal scalar)
        {
            return scalar.MapSamples(ScalarProcessor.Sqrt);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Signal SqrtOfAbs(Float64Signal scalar)
        {
            return scalar.MapSamples(ScalarProcessor.SqrtOfAbs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Signal Exp(Float64Signal scalar)
        {
            return scalar.MapSamples(ScalarProcessor.Exp);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Signal LogE(Float64Signal scalar)
        {
            return scalar.MapSamples(ScalarProcessor.LogE);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Signal Log2(Float64Signal scalar)
        {
            return scalar.MapSamples(ScalarProcessor.Log2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Signal Log10(Float64Signal scalar)
        {
            return scalar.MapSamples(ScalarProcessor.Log10);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Signal Log(Float64Signal baseScalar, Float64Signal scalar)
        {
            return baseScalar.MapSamples(scalar, ScalarProcessor.Log);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Signal Cos(Float64Signal scalar)
        {
            return scalar.MapSamples(ScalarProcessor.Cos);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Signal Sin(Float64Signal scalar)
        {
            return scalar.MapSamples(ScalarProcessor.Sin);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Signal Tan(Float64Signal scalar)
        {
            return scalar.MapSamples(ScalarProcessor.Tan);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Signal ArcCos(Float64Signal scalar)
        {
            return scalar.MapSamples(ScalarProcessor.ArcCos);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Signal ArcSin(Float64Signal scalar)
        {
            return scalar.MapSamples(ScalarProcessor.ArcSin);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Signal ArcTan(Float64Signal scalar)
        {
            return scalar.MapSamples(ScalarProcessor.ArcTan);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Signal ArcTan2(Float64Signal scalarX, Float64Signal scalarY)
        {
            return scalarX.MapSamples(scalarY, ScalarProcessor.ArcTan2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Signal Cosh(Float64Signal scalar)
        {
            return scalar.MapSamples(ScalarProcessor.Cosh);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Signal Sinh(Float64Signal scalar)
        {
            return scalar.MapSamples(ScalarProcessor.Sinh);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Signal Tanh(Float64Signal scalar)
        {
            return scalar.MapSamples(ScalarProcessor.Tanh);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Signal Sinc(Float64Signal scalar)
        {
            return scalar.MapSamples(ScalarProcessor.Sinc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid(Float64Signal scalar)
        {
            return scalar.All(ScalarProcessor.IsValid);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsFiniteNumber(Float64Signal scalar)
        {
            return scalar.All(ScalarProcessor.IsFiniteNumber);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero(Float64Signal scalar)
        {
            return scalar.All(ScalarProcessor.IsZero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero(Float64Signal scalar, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? IsNearZero(scalar) 
                : IsZero(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearZero(Float64Signal scalar)
        {
            return scalar.All(ScalarProcessor.IsNearZero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotZero(Float64Signal scalar)
        {
            return scalar.All(ScalarProcessor.IsNotZero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotZero(Float64Signal scalar, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? IsNotNearZero(scalar) 
                : IsNotZero(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearZero(Float64Signal scalar)
        {
            return scalar.All(ScalarProcessor.IsNotNearZero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsPositive(Float64Signal scalar)
        {
            return scalar.All(ScalarProcessor.IsPositive);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNegative(Float64Signal scalar)
        {
            return scalar.All(ScalarProcessor.IsNegative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotPositive(Float64Signal scalar)
        {
            return scalar.All(ScalarProcessor.IsNotPositive);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNegative(Float64Signal scalar)
        {
            return scalar.All(ScalarProcessor.IsNotNegative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearPositive(Float64Signal scalar)
        {
            return scalar.All(ScalarProcessor.IsNotNearPositive);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearNegative(Float64Signal scalar)
        {
            return scalar.All(ScalarProcessor.IsNotNearNegative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToText(Float64Signal scalar)
        {
            return scalar
                .Select(s => s.ToString("G"))
                .Concatenate(", ", "{", "}");
        }
    }
}