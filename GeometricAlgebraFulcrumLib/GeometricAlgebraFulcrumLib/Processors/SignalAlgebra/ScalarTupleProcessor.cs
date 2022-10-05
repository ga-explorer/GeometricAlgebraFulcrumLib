using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Processors.SignalAlgebra
{
    public sealed class ScalarTupleProcessor<T> : 
        IScalarAlgebraProcessor<ILinVectorStorage<T>>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static ScalarTupleProcessor<T> Create(IScalarAlgebraProcessor<T> scalarProcessor, int tupleSize)
        {
            return new ScalarTupleProcessor<T>(scalarProcessor, tupleSize);
        }


        public IScalarAlgebraProcessor<T> ScalarProcessor { get; }

        public int TupleSize { get; }

        public bool IsNumeric 
            => ScalarProcessor.IsNumeric;

        public bool IsSymbolic 
            => ScalarProcessor.IsSymbolic;

        public ILinVectorStorage<T> ScalarZero { get; }

        public ILinVectorStorage<T> ScalarOne { get; }

        public ILinVectorStorage<T> ScalarMinusOne { get; }

        public ILinVectorStorage<T> ScalarTwo { get; }

        public ILinVectorStorage<T> ScalarMinusTwo { get; }

        public ILinVectorStorage<T> ScalarTen { get; }

        public ILinVectorStorage<T> ScalarMinusTen { get; }

        public ILinVectorStorage<T> ScalarPi { get; }

        public ILinVectorStorage<T> ScalarTwoPi { get; }

        public ILinVectorStorage<T> ScalarPiOver2 { get; }

        public ILinVectorStorage<T> ScalarE { get; }

        public ILinVectorStorage<T> ScalarDegreeToRadian { get; }
        
        public ILinVectorStorage<T> ScalarRadianToDegree { get; }


        private ScalarTupleProcessor([NotNull] IScalarAlgebraProcessor<T> scalarProcessor, int tupleSize)
        {
            if (tupleSize < 1)
                throw new ArgumentOutOfRangeException(nameof(tupleSize));

            ScalarProcessor = scalarProcessor;
            TupleSize = tupleSize;

            ScalarZero = scalarProcessor.ScalarZero.CreateLinVectorRepeatedScalarStorage(tupleSize);
            ScalarOne = scalarProcessor.ScalarOne.CreateLinVectorRepeatedScalarStorage(tupleSize);
            ScalarMinusOne = scalarProcessor.ScalarMinusOne.CreateLinVectorRepeatedScalarStorage(tupleSize);
            ScalarTwo = scalarProcessor.ScalarTwo.CreateLinVectorRepeatedScalarStorage(tupleSize);
            ScalarMinusTwo = scalarProcessor.ScalarMinusTwo.CreateLinVectorRepeatedScalarStorage(tupleSize);
            ScalarTen = scalarProcessor.ScalarTen.CreateLinVectorRepeatedScalarStorage(tupleSize);
            ScalarMinusTen = scalarProcessor.ScalarMinusTen.CreateLinVectorRepeatedScalarStorage(tupleSize);
            ScalarPi = scalarProcessor.ScalarPi.CreateLinVectorRepeatedScalarStorage(tupleSize);
            ScalarTwoPi = scalarProcessor.ScalarTwoPi.CreateLinVectorRepeatedScalarStorage(tupleSize);
            ScalarPiOver2 = scalarProcessor.ScalarPiOver2.CreateLinVectorRepeatedScalarStorage(tupleSize);
            ScalarE = scalarProcessor.ScalarE.CreateLinVectorRepeatedScalarStorage(tupleSize);
            ScalarDegreeToRadian = scalarProcessor.ScalarDegreeToRadian.CreateLinVectorRepeatedScalarStorage(tupleSize);
            ScalarRadianToDegree = scalarProcessor.ScalarRadianToDegree.CreateLinVectorRepeatedScalarStorage(tupleSize);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> Add(ILinVectorStorage<T> scalar1, ILinVectorStorage<T> scalar2)
        {
            return ScalarProcessor.Add(scalar1, scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> Subtract(ILinVectorStorage<T> scalar1, ILinVectorStorage<T> scalar2)
        {
            return ScalarProcessor.Subtract(scalar1, scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> Times(ILinVectorStorage<T> scalar1, ILinVectorStorage<T> scalar2)
        {
            return ScalarProcessor.Times(scalar1, scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> NegativeTimes(ILinVectorStorage<T> scalar1, ILinVectorStorage<T> scalar2)
        {
            return Negative(Times(scalar1, scalar2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> Divide(ILinVectorStorage<T> scalar1, ILinVectorStorage<T> scalar2)
        {
            return ScalarProcessor.Divide(scalar1, scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> NegativeDivide(ILinVectorStorage<T> scalar1, ILinVectorStorage<T> scalar2)
        {
            return Negative(Divide(scalar1, scalar2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> Positive(ILinVectorStorage<T> scalar)
        {
            return scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> Negative(ILinVectorStorage<T> scalar)
        {
            return ScalarProcessor.Negative(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> Inverse(ILinVectorStorage<T> scalar)
        {
            return scalar.MapScalars(ScalarProcessor.Inverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> Sign(ILinVectorStorage<T> scalar)
        {
            return scalar.MapScalars(ScalarProcessor.Sign);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> UnitStep(ILinVectorStorage<T> scalar)
        {
            return scalar.MapScalars(ScalarProcessor.UnitStep);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> Abs(ILinVectorStorage<T> scalar)
        {
            return scalar.MapScalars(ScalarProcessor.Abs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> Sqrt(ILinVectorStorage<T> scalar)
        {
            return scalar.MapScalars(ScalarProcessor.Sqrt);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> SqrtOfAbs(ILinVectorStorage<T> scalar)
        {
            return scalar.MapScalars(ScalarProcessor.SqrtOfAbs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> Exp(ILinVectorStorage<T> scalar)
        {
            return scalar.MapScalars(ScalarProcessor.Exp);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> LogE(ILinVectorStorage<T> scalar)
        {
            return scalar.MapScalars(ScalarProcessor.LogE);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> Log2(ILinVectorStorage<T> scalar)
        {
            return scalar.MapScalars(ScalarProcessor.Log2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> Log10(ILinVectorStorage<T> scalar)
        {
            return scalar.MapScalars(ScalarProcessor.Log10);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> Power(ILinVectorStorage<T> baseScalar, ILinVectorStorage<T> scalar)
        {
            return ScalarProcessor
                .MapScalarsIndicesUnion(
                    baseScalar, 
                    scalar, 
                    ScalarProcessor.Power
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> Log(ILinVectorStorage<T> baseScalar, ILinVectorStorage<T> scalar)
        {
            return ScalarProcessor
                .MapScalarsIndicesUnion(
                    baseScalar, 
                    scalar, 
                    ScalarProcessor.Log
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> Cos(ILinVectorStorage<T> scalar)
        {
            return scalar.MapScalars(ScalarProcessor.Cos);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> Sin(ILinVectorStorage<T> scalar)
        {
            return scalar.MapScalars(ScalarProcessor.Sin);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> Tan(ILinVectorStorage<T> scalar)
        {
            return scalar.MapScalars(ScalarProcessor.Tan);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> ArcCos(ILinVectorStorage<T> scalar)
        {
            return scalar.MapScalars(ScalarProcessor.ArcCos);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> ArcSin(ILinVectorStorage<T> scalar)
        {
            return scalar.MapScalars(ScalarProcessor.ArcSin);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> ArcTan(ILinVectorStorage<T> scalar)
        {
            return scalar.MapScalars(ScalarProcessor.ArcTan);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> ArcTan2(ILinVectorStorage<T> scalarX, ILinVectorStorage<T> scalarY)
        {
            return ScalarProcessor
                .MapScalarsIndicesUnion(
                    scalarX, 
                    scalarY, 
                    ScalarProcessor.ArcTan2
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> Cosh(ILinVectorStorage<T> scalar)
        {
            return scalar.MapScalars(ScalarProcessor.Cosh);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> Sinh(ILinVectorStorage<T> scalar)
        {
            return scalar.MapScalars(ScalarProcessor.Sinh);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> Tanh(ILinVectorStorage<T> scalar)
        {
            return scalar.MapScalars(ScalarProcessor.Tanh);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> Sinc(ILinVectorStorage<T> scalar)
        {
            return IsZero(scalar) 
                ? ScalarOne 
                : Divide(Sin(scalar), scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid(ILinVectorStorage<T> scalar)
        {
            return scalar.GetScalars().All(s => ScalarProcessor.IsValid(s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsFiniteNumber(ILinVectorStorage<T> scalar)
        {
            return scalar.GetScalars().All(s => ScalarProcessor.IsFiniteNumber(s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero(ILinVectorStorage<T> scalar)
        {
            return scalar.GetScalars().All(s => ScalarProcessor.IsZero(s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero(ILinVectorStorage<T> scalar, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? IsNearZero(scalar)
                : IsZero(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearZero(ILinVectorStorage<T> scalar)
        {
            return scalar.GetScalars().All(s => ScalarProcessor.IsNearZero(s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotZero(ILinVectorStorage<T> scalar)
        {
            return scalar.GetScalars().Any(s => !ScalarProcessor.IsZero(s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotZero(ILinVectorStorage<T> scalar, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? !IsNearZero(scalar)
                : !IsZero(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearZero(ILinVectorStorage<T> scalar)
        {
            return scalar.GetScalars().Any(s => !ScalarProcessor.IsNearZero(s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsPositive(ILinVectorStorage<T> scalar)
        {
            return scalar.GetScalars().Any(s => ScalarProcessor.IsPositive(s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNegative(ILinVectorStorage<T> scalar)
        {
            return scalar.GetScalars().Any(s => ScalarProcessor.IsNegative(s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotPositive(ILinVectorStorage<T> scalar)
        {
            return scalar.GetScalars().Any(s => ScalarProcessor.IsNotPositive(s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNegative(ILinVectorStorage<T> scalar)
        {
            return scalar.GetScalars().Any(s => ScalarProcessor.IsNotNegative(s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearPositive(ILinVectorStorage<T> scalar)
        {
            return scalar.GetScalars().Any(s => ScalarProcessor.IsNotNearPositive(s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearNegative(ILinVectorStorage<T> scalar)
        {
            return scalar.GetScalars().Any(s => ScalarProcessor.IsNotNearNegative(s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetScalarFromText(string text)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetScalarFromNumber(int value)
        {
            return ScalarProcessor
                .GetScalarFromNumber(value)
                .CreateLinVectorRepeatedScalarStorage(TupleSize);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetScalarFromNumber(uint value)
        {
            return ScalarProcessor
                .GetScalarFromNumber(value)
                .CreateLinVectorRepeatedScalarStorage(TupleSize);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetScalarFromNumber(long value)
        {
            return ScalarProcessor
                .GetScalarFromNumber(value)
                .CreateLinVectorRepeatedScalarStorage(TupleSize);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetScalarFromNumber(ulong value)
        {
            return ScalarProcessor
                .GetScalarFromNumber(value)
                .CreateLinVectorRepeatedScalarStorage(TupleSize);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetScalarFromNumber(float value)
        {
            return ScalarProcessor
                .GetScalarFromNumber(value)
                .CreateLinVectorRepeatedScalarStorage(TupleSize);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetScalarFromNumber(double value)
        {
            return ScalarProcessor
                .GetScalarFromNumber(value)
                .CreateLinVectorRepeatedScalarStorage(TupleSize);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetScalarFromRational(long numerator, long denominator)
        {
            return ScalarProcessor
                .GetScalarFromRational(numerator, denominator)
                .CreateLinVectorRepeatedScalarStorage(TupleSize);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetScalarFromRandom(Random randomGenerator, double minValue, double maxValue)
        {
            return ScalarProcessor
                .GetScalarFromRandom(randomGenerator, minValue, maxValue)
                .CreateLinVectorRepeatedScalarStorage(TupleSize);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToText(ILinVectorStorage<T> scalar)
        {
            return scalar.ToString();
        }
    }
}