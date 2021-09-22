using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Processors.TupleAlgebra
{
    public sealed class ScalarAlgebraTupleProcessor<T>
        : IScalarAlgebraProcessor<IGeoTuple<T>>
    {
        public IScalarAlgebraProcessor<T> ItemScalarProcessor { get; }

        public bool IsNumeric => false;

        public bool IsSymbolic => false;

        public IGeoTuple<T> ScalarZero { get; }

        public IGeoTuple<T> ScalarOne { get; }

        public IGeoTuple<T> ScalarMinusOne { get; }

        public IGeoTuple<T> ScalarTwo { get; }

        public IGeoTuple<T> ScalarMinusTwo { get; }

        public IGeoTuple<T> ScalarTen { get; }

        public IGeoTuple<T> ScalarMinusTen { get; }

        public IGeoTuple<T> ScalarPi { get; }

        public IGeoTuple<T> ScalarE { get; }


        public ScalarAlgebraTupleProcessor([NotNull] IScalarAlgebraProcessor<T> itemScalarProcessor)
        {
            ItemScalarProcessor = itemScalarProcessor;

            ScalarZero = GeoConstantTuple<T>.Create(itemScalarProcessor, itemScalarProcessor.ScalarZero);
            ScalarOne = GeoConstantTuple<T>.Create(itemScalarProcessor, itemScalarProcessor.ScalarOne);
            ScalarMinusOne = GeoConstantTuple<T>.Create(itemScalarProcessor, itemScalarProcessor.ScalarMinusOne);
            ScalarTwo = GeoConstantTuple<T>.Create(itemScalarProcessor, itemScalarProcessor.ScalarTwo);
            ScalarMinusTwo = GeoConstantTuple<T>.Create(itemScalarProcessor, itemScalarProcessor.ScalarMinusTwo);
            ScalarTen = GeoConstantTuple<T>.Create(itemScalarProcessor, itemScalarProcessor.ScalarTen);
            ScalarMinusTen = GeoConstantTuple<T>.Create(itemScalarProcessor, itemScalarProcessor.ScalarMinusTen);
            ScalarPi = GeoConstantTuple<T>.Create(itemScalarProcessor, itemScalarProcessor.ScalarPi);
            ScalarE = GeoConstantTuple<T>.Create(itemScalarProcessor, itemScalarProcessor.ScalarE);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGeoTuple<T> Add(IGeoTuple<T> scalar1, IGeoTuple<T> scalar2)
        {
            return scalar1.Add(scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGeoTuple<T> Subtract(IGeoTuple<T> scalar1, IGeoTuple<T> scalar2)
        {
            return scalar1.Subtract(scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGeoTuple<T> Times(IGeoTuple<T> scalar1, IGeoTuple<T> scalar2)
        {
            return scalar1.Times(scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGeoTuple<T> NegativeTimes(IGeoTuple<T> t1, IGeoTuple<T> t2)
        {
            return t1.Times(t2).Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGeoTuple<T> Divide(IGeoTuple<T> scalar1, IGeoTuple<T> scalar2)
        {
            return scalar1.Divide(scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGeoTuple<T> NegativeDivide(IGeoTuple<T> scalar1, IGeoTuple<T> scalar2)
        {
            return scalar1.Divide(scalar2).Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGeoTuple<T> Positive(IGeoTuple<T> scalar)
        {
            return scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGeoTuple<T> Negative(IGeoTuple<T> scalar)
        {
            return scalar.Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGeoTuple<T> Inverse(IGeoTuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.Inverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGeoTuple<T> Abs(IGeoTuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.Abs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGeoTuple<T> Sqrt(IGeoTuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.Sqrt);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGeoTuple<T> SqrtOfAbs(IGeoTuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.SqrtOfAbs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGeoTuple<T> Exp(IGeoTuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.Exp);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGeoTuple<T> LogE(IGeoTuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.LogE);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGeoTuple<T> Log2(IGeoTuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.Log2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGeoTuple<T> Log10(IGeoTuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.Log10);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGeoTuple<T> Power(IGeoTuple<T> baseScalar, IGeoTuple<T> scalar)
        {
            return scalar.MapScalars(
                baseScalar, 
                (s1, s2) => ItemScalarProcessor.Power(s1, s2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGeoTuple<T> Log(IGeoTuple<T> baseScalar, IGeoTuple<T> scalar)
        {
            return scalar.MapScalars(
                baseScalar, 
                (s1, s2) => ItemScalarProcessor.Log(s1, s2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGeoTuple<T> Cos(IGeoTuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.Cos);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGeoTuple<T> Sin(IGeoTuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.Sin);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGeoTuple<T> Tan(IGeoTuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.Tan);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGeoTuple<T> ArcCos(IGeoTuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.ArcCos);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGeoTuple<T> ArcSin(IGeoTuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.ArcSin);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGeoTuple<T> ArcTan(IGeoTuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.ArcTan);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGeoTuple<T> ArcTan2(IGeoTuple<T> scalarX, IGeoTuple<T> scalarY)
        {
            return scalarX.MapScalars(scalarY, ItemScalarProcessor.ArcTan2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGeoTuple<T> Cosh(IGeoTuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.Cosh);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGeoTuple<T> Sinh(IGeoTuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.Sinh);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGeoTuple<T> Tanh(IGeoTuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.Tanh);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid(IGeoTuple<T> scalar)
        {
            return scalar.IsValid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero(IGeoTuple<T> scalar)
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero(IGeoTuple<T> scalar, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? scalar.IsNearZero()
                : scalar.IsZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearZero(IGeoTuple<T> scalar)
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotZero(IGeoTuple<T> scalar)
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotZero(IGeoTuple<T> scalar, bool nearZeroFlag)
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearZero(IGeoTuple<T> scalar)
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsPositive(IGeoTuple<T> scalar)
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNegative(IGeoTuple<T> scalar)
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotPositive(IGeoTuple<T> scalar)
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNegative(IGeoTuple<T> scalar)
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearPositive(IGeoTuple<T> scalar)
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearNegative(IGeoTuple<T> scalar)
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGeoTuple<T> GetScalarFromText(string text)
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGeoTuple<T> GetScalarFromNumber(int value)
        {
            return GeoConstantTuple<T>.Create(
                ItemScalarProcessor, 
                ItemScalarProcessor.GetScalarFromNumber(value)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGeoTuple<T> GetScalarFromNumber(uint value)
        {
            return GeoConstantTuple<T>.Create(
                ItemScalarProcessor, 
                ItemScalarProcessor.GetScalarFromNumber(value)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGeoTuple<T> GetScalarFromNumber(long value)
        {
            return GeoConstantTuple<T>.Create(
                ItemScalarProcessor, 
                ItemScalarProcessor.GetScalarFromNumber(value)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGeoTuple<T> GetScalarFromNumber(ulong value)
        {
            return GeoConstantTuple<T>.Create(
                ItemScalarProcessor, 
                ItemScalarProcessor.GetScalarFromNumber(value)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGeoTuple<T> GetScalarFromNumber(float value)
        {
            return GeoConstantTuple<T>.Create(
                ItemScalarProcessor, 
                ItemScalarProcessor.GetScalarFromNumber(value)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGeoTuple<T> GetScalarFromNumber(double value)
        {
            return GeoConstantTuple<T>.Create(
                ItemScalarProcessor, 
                ItemScalarProcessor.GetScalarFromNumber(value)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGeoTuple<T> GetScalarFromRational(long numerator, long denominator)
        {
            return GeoConstantTuple<T>.Create(
                ItemScalarProcessor, 
                ItemScalarProcessor.GetScalarFromRational(numerator, denominator)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGeoTuple<T> GetScalarFromRandom(System.Random randomGenerator, double minValue, double maxValue)
        {
            return GeoConstantTuple<T>.Create(
                ItemScalarProcessor, 
                ItemScalarProcessor.GetScalarFromRandom(randomGenerator, minValue, maxValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToText(IGeoTuple<T> scalar)
        {
            return scalar.ToString();
        }
    }
}