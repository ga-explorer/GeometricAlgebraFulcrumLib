using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Processors.TupleAlgebra
{
    public sealed class ScalarAlgebraTupleProcessor<T>
        : IScalarAlgebraProcessor<ITuple<T>>
    {
        public IScalarAlgebraProcessor<T> ItemScalarProcessor { get; }

        public bool IsNumeric => false;

        public bool IsSymbolic => false;

        public ITuple<T> ScalarZero { get; }

        public ITuple<T> ScalarOne { get; }

        public ITuple<T> ScalarMinusOne { get; }

        public ITuple<T> ScalarTwo { get; }

        public ITuple<T> ScalarMinusTwo { get; }

        public ITuple<T> ScalarTen { get; }

        public ITuple<T> ScalarMinusTen { get; }

        public ITuple<T> ScalarPi { get; }

        public ITuple<T> ScalarPiOver2 { get; }

        public ITuple<T> ScalarE { get; }


        public ScalarAlgebraTupleProcessor([NotNull] IScalarAlgebraProcessor<T> itemScalarProcessor)
        {
            ItemScalarProcessor = itemScalarProcessor;

            ScalarZero = ConstantTuple<T>.Create(itemScalarProcessor, itemScalarProcessor.ScalarZero);
            ScalarOne = ConstantTuple<T>.Create(itemScalarProcessor, itemScalarProcessor.ScalarOne);
            ScalarMinusOne = ConstantTuple<T>.Create(itemScalarProcessor, itemScalarProcessor.ScalarMinusOne);
            ScalarTwo = ConstantTuple<T>.Create(itemScalarProcessor, itemScalarProcessor.ScalarTwo);
            ScalarMinusTwo = ConstantTuple<T>.Create(itemScalarProcessor, itemScalarProcessor.ScalarMinusTwo);
            ScalarTen = ConstantTuple<T>.Create(itemScalarProcessor, itemScalarProcessor.ScalarTen);
            ScalarMinusTen = ConstantTuple<T>.Create(itemScalarProcessor, itemScalarProcessor.ScalarMinusTen);
            ScalarPi = ConstantTuple<T>.Create(itemScalarProcessor, itemScalarProcessor.ScalarPi);
            ScalarPiOver2 = ConstantTuple<T>.Create(itemScalarProcessor, itemScalarProcessor.ScalarPiOver2);
            ScalarE = ConstantTuple<T>.Create(itemScalarProcessor, itemScalarProcessor.ScalarE);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ITuple<T> Add(ITuple<T> scalar1, ITuple<T> scalar2)
        {
            return scalar1.Add(scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ITuple<T> Subtract(ITuple<T> scalar1, ITuple<T> scalar2)
        {
            return scalar1.Subtract(scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ITuple<T> Times(ITuple<T> scalar1, ITuple<T> scalar2)
        {
            return scalar1.Times(scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ITuple<T> NegativeTimes(ITuple<T> t1, ITuple<T> t2)
        {
            return t1.Times(t2).Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ITuple<T> Divide(ITuple<T> scalar1, ITuple<T> scalar2)
        {
            return scalar1.Divide(scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ITuple<T> NegativeDivide(ITuple<T> scalar1, ITuple<T> scalar2)
        {
            return scalar1.Divide(scalar2).Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ITuple<T> Positive(ITuple<T> scalar)
        {
            return scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ITuple<T> Negative(ITuple<T> scalar)
        {
            return scalar.Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ITuple<T> Inverse(ITuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.Inverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ITuple<T> Abs(ITuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.Abs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ITuple<T> Sqrt(ITuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.Sqrt);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ITuple<T> SqrtOfAbs(ITuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.SqrtOfAbs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ITuple<T> Exp(ITuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.Exp);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ITuple<T> LogE(ITuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.LogE);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ITuple<T> Log2(ITuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.Log2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ITuple<T> Log10(ITuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.Log10);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ITuple<T> Power(ITuple<T> baseScalar, ITuple<T> scalar)
        {
            return scalar.MapScalars(
                baseScalar, 
                (s1, s2) => ItemScalarProcessor.Power(s1, s2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ITuple<T> Log(ITuple<T> baseScalar, ITuple<T> scalar)
        {
            return scalar.MapScalars(
                baseScalar, 
                (s1, s2) => ItemScalarProcessor.Log(s1, s2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ITuple<T> Cos(ITuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.Cos);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ITuple<T> Sin(ITuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.Sin);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ITuple<T> Tan(ITuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.Tan);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ITuple<T> ArcCos(ITuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.ArcCos);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ITuple<T> ArcSin(ITuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.ArcSin);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ITuple<T> ArcTan(ITuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.ArcTan);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ITuple<T> ArcTan2(ITuple<T> scalarX, ITuple<T> scalarY)
        {
            return scalarX.MapScalars(scalarY, ItemScalarProcessor.ArcTan2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ITuple<T> Cosh(ITuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.Cosh);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ITuple<T> Sinh(ITuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.Sinh);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ITuple<T> Tanh(ITuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.Tanh);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid(ITuple<T> scalar)
        {
            return scalar.IsValid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero(ITuple<T> scalar)
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero(ITuple<T> scalar, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? scalar.IsNearZero()
                : scalar.IsZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearZero(ITuple<T> scalar)
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotZero(ITuple<T> scalar)
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotZero(ITuple<T> scalar, bool nearZeroFlag)
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearZero(ITuple<T> scalar)
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsPositive(ITuple<T> scalar)
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNegative(ITuple<T> scalar)
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotPositive(ITuple<T> scalar)
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNegative(ITuple<T> scalar)
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearPositive(ITuple<T> scalar)
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearNegative(ITuple<T> scalar)
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ITuple<T> GetScalarFromText(string text)
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ITuple<T> GetScalarFromNumber(int value)
        {
            return ConstantTuple<T>.Create(
                ItemScalarProcessor, 
                ItemScalarProcessor.GetScalarFromNumber(value)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ITuple<T> GetScalarFromNumber(uint value)
        {
            return ConstantTuple<T>.Create(
                ItemScalarProcessor, 
                ItemScalarProcessor.GetScalarFromNumber(value)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ITuple<T> GetScalarFromNumber(long value)
        {
            return ConstantTuple<T>.Create(
                ItemScalarProcessor, 
                ItemScalarProcessor.GetScalarFromNumber(value)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ITuple<T> GetScalarFromNumber(ulong value)
        {
            return ConstantTuple<T>.Create(
                ItemScalarProcessor, 
                ItemScalarProcessor.GetScalarFromNumber(value)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ITuple<T> GetScalarFromNumber(float value)
        {
            return ConstantTuple<T>.Create(
                ItemScalarProcessor, 
                ItemScalarProcessor.GetScalarFromNumber(value)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ITuple<T> GetScalarFromNumber(double value)
        {
            return ConstantTuple<T>.Create(
                ItemScalarProcessor, 
                ItemScalarProcessor.GetScalarFromNumber(value)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ITuple<T> GetScalarFromRational(long numerator, long denominator)
        {
            return ConstantTuple<T>.Create(
                ItemScalarProcessor, 
                ItemScalarProcessor.GetScalarFromRational(numerator, denominator)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ITuple<T> GetScalarFromRandom(System.Random randomGenerator, double minValue, double maxValue)
        {
            return ConstantTuple<T>.Create(
                ItemScalarProcessor, 
                ItemScalarProcessor.GetScalarFromRandom(randomGenerator, minValue, maxValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToText(ITuple<T> scalar)
        {
            return scalar.ToString();
        }
    }
}