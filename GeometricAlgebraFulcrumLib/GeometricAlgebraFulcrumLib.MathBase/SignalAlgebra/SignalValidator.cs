using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.Signals;

namespace GeometricAlgebraFulcrumLib.MathBase.SignalAlgebra
{
    public class SignalValidator
    {
        public double ZeroEpsilon { get; set; } = 1e-7;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ValidateEqualZero(Scalar<ScalarSignalFloat64> scalarSignal1)
        {
            return ValidateEqualZero(scalarSignal1.ScalarValue);
        }

        public bool ValidateEqualZero(ScalarSignalFloat64 scalarSignal1)
        {
            if (scalarSignal1.IsNearZero(ZeroEpsilon))
                return true;

            var scalarSignal1Rms =
                scalarSignal1.Select(s => s.Square()).Average().Sqrt();

            if (scalarSignal1Rms.IsNearZero(ZeroEpsilon))
                return true;

            Console.WriteLine($"RMS value: {scalarSignal1Rms:G}");
            Console.WriteLine();

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ValidateEqual(Scalar<ScalarSignalFloat64> scalarSignal1, Scalar<ScalarSignalFloat64> scalarSignal2)
        {
            return ValidateEqual(
                scalarSignal1.ScalarValue,
                scalarSignal2.ScalarValue
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ValidateEqual(Scalar<ScalarSignalFloat64> scalarSignal1, ScalarSignalFloat64 scalarSignal2)
        {
            return ValidateEqual(
                scalarSignal1.ScalarValue,
                scalarSignal2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ValidateEqual(ScalarSignalFloat64 scalarSignal1, Scalar<ScalarSignalFloat64> scalarSignal2)
        {
            return ValidateEqual(
                scalarSignal1,
                scalarSignal2.ScalarValue
            );
        }

        public bool ValidateEqual(ScalarSignalFloat64 scalarSignal1, ScalarSignalFloat64 scalarSignal2)
        {
            var errorSignal =
                scalarSignal1 - scalarSignal2;

            if (errorSignal.IsNearZero(ZeroEpsilon))
                return true;

            var snr = 
                scalarSignal1.PeakSignalToNoiseRatioDb(scalarSignal2).NaNToZero();

            if (snr > 50)
                return true;

            var scalarSignal1Rms = scalarSignal1.Select(s => s.Square()).Average().Sqrt();
            var errorSignalRms = errorSignal.Select(s => s.Square()).Average().Sqrt();

            var errorSignalRmsRatio = (errorSignalRms / scalarSignal1Rms).NaNToZero();

            Console.WriteLine($"SNR: {snr:G}, RMS error ratio: {errorSignalRmsRatio:G}");
            Console.WriteLine();

            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ValidateEqual(RGaVector<ScalarSignalFloat64> scalarSignal1, RGaVector<ScalarSignalFloat64> scalarSignal2)
        {
            return ValidateZeroNorm(
                scalarSignal1 - scalarSignal2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ValidateEqual(XGaVector<ScalarSignalFloat64> scalarSignal1, XGaVector<ScalarSignalFloat64> scalarSignal2)
        {
            return ValidateZeroNorm(
                scalarSignal1 - scalarSignal2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ValidateEqual(XGaBivector<ScalarSignalFloat64> scalarSignal1, XGaBivector<ScalarSignalFloat64> scalarSignal2)
        {
            return ValidateZeroNormSquared(
                scalarSignal1 - scalarSignal2
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ValidateZeroNorm(RGaVector<ScalarSignalFloat64> scalarSignal1)
        {
            return ValidateEqualZero(
                scalarSignal1.Norm().ScalarValue
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ValidateZeroNorm(XGaVector<ScalarSignalFloat64> scalarSignal1)
        {
            return ValidateEqualZero(
                scalarSignal1.Norm().ScalarValue
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ValidateZeroNormSquared(XGaVector<ScalarSignalFloat64> scalarSignal1)
        {
            return ValidateEqualZero(
                scalarSignal1.NormSquared().ScalarValue
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ValidateZeroNormSquared(XGaBivector<ScalarSignalFloat64> scalarSignal1)
        {
            return ValidateEqualZero(
                scalarSignal1.NormSquared().ScalarValue
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ValidateUnitNormSquared(RGaVector<ScalarSignalFloat64> scalarSignal1)
        {
            return ValidateEqual(
                scalarSignal1.NormSquared().ScalarValue,
                scalarSignal1.ScalarProcessor.GetScalarFromNumber(1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ValidateUnitNormSquared(XGaVector<ScalarSignalFloat64> scalarSignal1)
        {
            return ValidateEqual(
                scalarSignal1.NormSquared().ScalarValue,
                scalarSignal1.ScalarProcessor.GetScalarFromNumber(1)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ValidateOrthogonal(RGaVector<ScalarSignalFloat64> vectorSignal1, RGaVector<ScalarSignalFloat64> vectorSignal2)
        {
            return ValidateEqualZero(vectorSignal1.Sp(vectorSignal2).ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ValidateOrthogonal(XGaVector<ScalarSignalFloat64> vectorSignal1,
            XGaVector<ScalarSignalFloat64> vectorSignal2)
        {
            return ValidateEqualZero(vectorSignal1.Sp(vectorSignal2).ScalarValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ValidateOrthogonal(IReadOnlyList<RGaVector<ScalarSignalFloat64>> vectorSignalList)
        {
            var validatedFlag = true;
            for (var i = 0; i < vectorSignalList.Count; i++)
            {
                var vectorSignal1 = vectorSignalList[i];

                for (var j = 0; j < vectorSignalList.Count; j++)
                {
                    if (i == j) continue;

                    var vectorSignal2 = vectorSignalList[j];

                    validatedFlag &= ValidateEqualZero(vectorSignal1.Sp(vectorSignal2).ScalarValue);
                }
            }

            return validatedFlag;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ValidateOrthogonal(IReadOnlyList<XGaVector<ScalarSignalFloat64>> vectorSignalList)
        {
            var validatedFlag = true;
            for (var i = 0; i < vectorSignalList.Count; i++)
            {
                var vectorSignal1 = vectorSignalList[i];

                for (var j = 0; j < vectorSignalList.Count; j++)
                {
                    if (i == j) continue;

                    var vectorSignal2 = vectorSignalList[j];

                    validatedFlag &= ValidateEqualZero(vectorSignal1.Sp(vectorSignal2).ScalarValue);
                }
            }

            return validatedFlag;
        }

    }
}
