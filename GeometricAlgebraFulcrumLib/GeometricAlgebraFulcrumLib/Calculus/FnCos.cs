using System;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.FunctionAlgebra;

namespace GeometricAlgebraFulcrumLib.Calculus
{
    public sealed class FnCos<T> :
        IScalarFunction<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static FnCos<T> Create(IScalarFunctionProcessor<T> functionProcessor, T magnitude, T frequency)
        {
            return new FnCos<T>(
                functionProcessor,
                magnitude,
                frequency,
                functionProcessor.ScalarProcessor.ScalarZero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static FnCos<T> Create(IScalarFunctionProcessor<T> functionProcessor, T magnitude, T frequency, T phase)
        {
            return new FnCos<T>(
                functionProcessor,
                magnitude,
                frequency,
                phase
            );
        }

    
        public IScalarProcessor<T> ScalarProcessor 
            => FunctionProcessor.ScalarProcessor;

        public IScalarFunctionProcessor<T> FunctionProcessor { get; }

        public Scalar<T> Magnitude { get; }

        public Scalar<T> Frequency { get; }

        public Scalar<T> Phase { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private FnCos(IScalarFunctionProcessor<T> functionProcessor, T magnitude, T frequency, T phase)
        {
            var scalarProcessor = functionProcessor.ScalarProcessor;

            FunctionProcessor = functionProcessor;
            Magnitude = magnitude.CreateScalar(scalarProcessor);
            Frequency = frequency.CreateScalar(scalarProcessor);
            Phase = phase.CreateScalar(scalarProcessor);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue(T t)
        {
            return Magnitude * (Frequency * t + Phase).Cos();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetDerivativeValue(T t)
        {
            return -Magnitude * Frequency * (Frequency * t + Phase).Sin();
        }

        public T GetDerivativeValue(T t, int degree)
        {
            if (degree < 0)
                throw new ArgumentOutOfRangeException(nameof(degree));

            var magnitude = degree switch
            {
                0 => Magnitude,
                1 => Magnitude * Frequency,
                2 => Magnitude * Frequency.Square(),
                3 => Magnitude * Frequency.Cube(),
                _ => Magnitude * Frequency.Power(degree)
            };

            degree %= 4;

            if (degree is 1 or 2)
                magnitude = -magnitude;

            return degree.IsEven()
                ? magnitude * (Frequency * t + Phase).Cos()
                : magnitude * (Frequency * t + Phase).Sin();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IScalarFunction<T> GetDerivative()
        {
            return FnSin<T>.Create(
                FunctionProcessor,
                -Magnitude * Frequency,
                Frequency,
                Phase
            );
        }

        public IScalarFunction<T> GetDerivative(int degree)
        {
            if (degree < 0)
                throw new ArgumentOutOfRangeException(nameof(degree));

            var magnitude = degree switch
            {
                0 => Magnitude,
                1 => Magnitude * Frequency,
                2 => Magnitude * Frequency.Square(),
                3 => Magnitude * Frequency.Cube(),
                _ => Magnitude * Frequency.Power(degree)
            };

            degree %= 4;

            if (degree is 1 or 2)
                magnitude = -magnitude;

            return degree.IsEven()
                ? Create(FunctionProcessor, magnitude, Frequency, Phase)
                : FnSin<T>.Create(FunctionProcessor, magnitude, Frequency, Phase);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarFunction<T> ToScalarFunction()
        {
            return ScalarFunction<T>.Create(
                FunctionProcessor,
                t => Magnitude * (Frequency * t + Phase).Cos()
            );
        }
    }
}