using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using NumericalGeometryLib.BasicMath;

namespace GeometricAlgebraFulcrumLib.Algebra.SignalAlgebra
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

            var snr = scalarSignal1.SignalToNoiseRatio(errorSignal).NaNToZero();

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
        public bool ValidateEqual(GaVector<ScalarSignalFloat64> scalarSignal1, GaVector<ScalarSignalFloat64> scalarSignal2)
        {
            return ValidateZeroNorm(
                scalarSignal1 - scalarSignal2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ValidateEqual(GaBivector<ScalarSignalFloat64> scalarSignal1, GaBivector<ScalarSignalFloat64> scalarSignal2)
        {
            return ValidateZeroNormSquared(
                scalarSignal1 - scalarSignal2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ValidateZeroNorm(GaVector<ScalarSignalFloat64> scalarSignal1)
        {
            return ValidateEqualZero(
                scalarSignal1.Norm()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ValidateZeroNormSquared(GaVector<ScalarSignalFloat64> scalarSignal1)
        {
            return ValidateEqualZero(
                scalarSignal1.NormSquared()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ValidateZeroNormSquared(GaBivector<ScalarSignalFloat64> scalarSignal1)
        {
            return ValidateEqualZero(
                scalarSignal1.NormSquared()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ValidateUnitNormSquared(GaVector<ScalarSignalFloat64> scalarSignal1)
        {
            return ValidateEqual(
                scalarSignal1.NormSquared(),
                scalarSignal1.GeometricProcessor.GetScalarFromNumber(1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ValidateOrthogonal(GaVector<ScalarSignalFloat64> vectorSignal1,
            GaVector<ScalarSignalFloat64> vectorSignal2)
        {
            return ValidateEqualZero(vectorSignal1.Sp(vectorSignal2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ValidateOrthogonal(IReadOnlyList<GaVector<ScalarSignalFloat64>> vectorSignalList)
        {
            var validatedFlag = true;
            for (var i = 0; i < vectorSignalList.Count; i++)
            {
                var vectorSignal1 = vectorSignalList[i];

                for (var j = 0; j < vectorSignalList.Count; j++)
                {
                    if (i == j) continue;

                    var vectorSignal2 = vectorSignalList[j];

                    validatedFlag &= ValidateEqualZero(vectorSignal1.Sp(vectorSignal2));
                }
            }

            return validatedFlag;
        }

    }
}
