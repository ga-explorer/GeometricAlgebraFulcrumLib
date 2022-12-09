using System;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.SignalAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using TextComposerLib.Text;

namespace GeometricAlgebraFulcrumLib.Processors.SignalAlgebra
{
    public sealed class ScalarSignalFloat64Processor :
        IScalarAlgebraNumericProcessor<ScalarSignalFloat64>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static ScalarSignalFloat64Processor Create(double samplingRate, int sampleCount)
        {
            return new ScalarSignalFloat64Processor(samplingRate, sampleCount);
        }


        public int SampleCount { get; }

        public double SamplingRate { get; }

        public IScalarAlgebraProcessor<double> ScalarProcessor 
            => ScalarAlgebraFloat64Processor.DefaultProcessor;

        public bool IsNumeric 
            => ScalarProcessor.IsNumeric;

        public bool IsSymbolic 
            => ScalarProcessor.IsSymbolic;

        public ScalarSignalFloat64 ScalarZero { get; }
        
        public ScalarSignalFloat64 ScalarOne { get; }
        
        public ScalarSignalFloat64 ScalarMinusOne { get; }
        
        public ScalarSignalFloat64 ScalarTwo { get; }
        
        public ScalarSignalFloat64 ScalarMinusTwo { get; }
        
        public ScalarSignalFloat64 ScalarTen { get; }
        
        public ScalarSignalFloat64 ScalarMinusTen { get; }
        
        public ScalarSignalFloat64 ScalarPi { get; }

        public ScalarSignalFloat64 ScalarTwoPi { get; }

        public ScalarSignalFloat64 ScalarPiOver2 { get; }
        
        public ScalarSignalFloat64 ScalarE { get; }

        public ScalarSignalFloat64 ScalarDegreeToRadian { get; }

        public ScalarSignalFloat64 ScalarRadianToDegree { get; }


        public ScalarSignalFloat64Processor(double samplingRate, int signalSamplesCount)
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


        public ScalarSignalFloat64 GetReadOnlyScalarFromNumber(double value)
        {
            return ScalarSignalFloat64.CreateConstant(
                SamplingRate, 
                SampleCount,
                value,
                true
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 GetScalarFromNumber(int value)
        {
            return ScalarSignalFloat64.CreateConstant(
                SamplingRate, 
                SampleCount,
                value,
                false
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 GetScalarFromNumber(uint value)
        {
            return ScalarSignalFloat64.CreateConstant(
                SamplingRate, 
                SampleCount,
                value,
                false
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 GetScalarFromNumber(long value)
        {
            return ScalarSignalFloat64.CreateConstant(
                SamplingRate, 
                SampleCount,
                value,
                false
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 GetScalarFromNumber(ulong value)
        {
            return ScalarSignalFloat64.CreateConstant(
                SamplingRate, 
                SampleCount,
                value,
                false
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 GetScalarFromNumber(float value)
        {
            return ScalarSignalFloat64.CreateConstant(
                SamplingRate, 
                SampleCount,
                value,
                false
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 GetScalarFromNumber(double value)
        {
            return ScalarSignalFloat64.CreateConstant(
                SamplingRate, 
                SampleCount,
                value,
                false
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 GetScalarFromRational(long numerator, long denominator)
        {
            return ScalarSignalFloat64.CreateConstant(
                SamplingRate, 
                SampleCount,
                numerator / (double) denominator,
                false
            );
        }

        public ScalarSignalFloat64 GetScalarFromRandom(Random randomGenerator, double minValue, double maxValue)
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

            return ScalarSignalFloat64.Create(SamplingRate, scalarArray, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 GetScalarFromText(string text)
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Add(ScalarSignalFloat64 scalar1, ScalarSignalFloat64 scalar2)
        {
            return scalar1.MapSamples(scalar2, ScalarProcessor.Add);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Subtract(ScalarSignalFloat64 scalar1, ScalarSignalFloat64 scalar2)
        {
            return scalar1.MapSamples(scalar2, ScalarProcessor.Subtract);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Times(ScalarSignalFloat64 scalar1, ScalarSignalFloat64 scalar2)
        {
            return scalar1.MapSamples(
                scalar2, 
                (s1, s2) => ScalarProcessor.Times(s1, s2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 NegativeTimes(ScalarSignalFloat64 scalar1, ScalarSignalFloat64 scalar2)
        {
            return scalar1.MapSamples(scalar2, ScalarProcessor.NegativeTimes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Divide(ScalarSignalFloat64 scalar1, ScalarSignalFloat64 scalar2)
        {
            return scalar1.MapSamples(scalar2, ScalarProcessor.Divide);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 NegativeDivide(ScalarSignalFloat64 scalar1, ScalarSignalFloat64 scalar2)
        {
            return scalar1.MapSamples(scalar2, ScalarProcessor.NegativeDivide);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Positive(ScalarSignalFloat64 scalar)
        {
            return scalar.MapSamples(ScalarProcessor.Positive);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Negative(ScalarSignalFloat64 scalar)
        {
            return scalar.MapSamples(ScalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Inverse(ScalarSignalFloat64 scalar)
        {
            return scalar.MapSamples(ScalarProcessor.Inverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Sign(ScalarSignalFloat64 scalar)
        {
            return scalar.MapSamples(
                s => ScalarProcessor.Sign(s)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 UnitStep(ScalarSignalFloat64 scalar)
        {
            return scalar.MapSamples(ScalarProcessor.UnitStep);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Abs(ScalarSignalFloat64 scalar)
        {
            return scalar.MapSamples(ScalarProcessor.Abs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Power(ScalarSignalFloat64 baseScalar, ScalarSignalFloat64 scalar)
        {
            return baseScalar.MapSamples(scalar, ScalarProcessor.Power);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Sqrt(ScalarSignalFloat64 scalar)
        {
            return scalar.MapSamples(ScalarProcessor.Sqrt);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 SqrtOfAbs(ScalarSignalFloat64 scalar)
        {
            return scalar.MapSamples(ScalarProcessor.SqrtOfAbs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Exp(ScalarSignalFloat64 scalar)
        {
            return scalar.MapSamples(ScalarProcessor.Exp);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 LogE(ScalarSignalFloat64 scalar)
        {
            return scalar.MapSamples(ScalarProcessor.LogE);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Log2(ScalarSignalFloat64 scalar)
        {
            return scalar.MapSamples(ScalarProcessor.Log2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Log10(ScalarSignalFloat64 scalar)
        {
            return scalar.MapSamples(ScalarProcessor.Log10);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Log(ScalarSignalFloat64 baseScalar, ScalarSignalFloat64 scalar)
        {
            return baseScalar.MapSamples(scalar, ScalarProcessor.Log);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Cos(ScalarSignalFloat64 scalar)
        {
            return scalar.MapSamples(ScalarProcessor.Cos);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Sin(ScalarSignalFloat64 scalar)
        {
            return scalar.MapSamples(ScalarProcessor.Sin);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Tan(ScalarSignalFloat64 scalar)
        {
            return scalar.MapSamples(ScalarProcessor.Tan);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 ArcCos(ScalarSignalFloat64 scalar)
        {
            return scalar.MapSamples(ScalarProcessor.ArcCos);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 ArcSin(ScalarSignalFloat64 scalar)
        {
            return scalar.MapSamples(ScalarProcessor.ArcSin);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 ArcTan(ScalarSignalFloat64 scalar)
        {
            return scalar.MapSamples(ScalarProcessor.ArcTan);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 ArcTan2(ScalarSignalFloat64 scalarX, ScalarSignalFloat64 scalarY)
        {
            return scalarX.MapSamples(scalarY, ScalarProcessor.ArcTan2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Cosh(ScalarSignalFloat64 scalar)
        {
            return scalar.MapSamples(ScalarProcessor.Cosh);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Sinh(ScalarSignalFloat64 scalar)
        {
            return scalar.MapSamples(ScalarProcessor.Sinh);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Tanh(ScalarSignalFloat64 scalar)
        {
            return scalar.MapSamples(ScalarProcessor.Tanh);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarSignalFloat64 Sinc(ScalarSignalFloat64 scalar)
        {
            return scalar.MapSamples(ScalarProcessor.Sinc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid(ScalarSignalFloat64 scalar)
        {
            return scalar.All(ScalarProcessor.IsValid);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsFiniteNumber(ScalarSignalFloat64 scalar)
        {
            return scalar.All(ScalarProcessor.IsFiniteNumber);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero(ScalarSignalFloat64 scalar)
        {
            return scalar.All(ScalarProcessor.IsZero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero(ScalarSignalFloat64 scalar, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? IsNearZero(scalar) 
                : IsZero(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearZero(ScalarSignalFloat64 scalar)
        {
            return scalar.All(ScalarProcessor.IsNearZero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotZero(ScalarSignalFloat64 scalar)
        {
            return scalar.All(ScalarProcessor.IsNotZero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotZero(ScalarSignalFloat64 scalar, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? IsNotNearZero(scalar) 
                : IsNotZero(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearZero(ScalarSignalFloat64 scalar)
        {
            return scalar.All(ScalarProcessor.IsNotNearZero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsPositive(ScalarSignalFloat64 scalar)
        {
            return scalar.All(ScalarProcessor.IsPositive);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNegative(ScalarSignalFloat64 scalar)
        {
            return scalar.All(ScalarProcessor.IsNegative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotPositive(ScalarSignalFloat64 scalar)
        {
            return scalar.All(ScalarProcessor.IsNotPositive);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNegative(ScalarSignalFloat64 scalar)
        {
            return scalar.All(ScalarProcessor.IsNotNegative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearPositive(ScalarSignalFloat64 scalar)
        {
            return scalar.All(ScalarProcessor.IsNotNearPositive);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearNegative(ScalarSignalFloat64 scalar)
        {
            return scalar.All(ScalarProcessor.IsNotNearNegative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToText(ScalarSignalFloat64 scalar)
        {
            return scalar
                .Select(s => s.ToString("G"))
                .Concatenate(", ", "{", "}");
        }
    }
}