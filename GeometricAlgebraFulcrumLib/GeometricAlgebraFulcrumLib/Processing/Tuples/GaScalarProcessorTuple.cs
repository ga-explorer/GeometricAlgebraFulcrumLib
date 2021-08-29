using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Processing.Scalars;

namespace GeometricAlgebraFulcrumLib.Processing.Tuples
{
    public sealed class GaScalarProcessorTuple<T>
        : IScalarProcessor<IGaTuple<T>>
    {
        public IScalarProcessor<T> ItemScalarProcessor { get; }

        public bool IsNumeric => false;

        public bool IsSymbolic => false;

        public IGaTuple<T> ScalarZero { get; }

        public IGaTuple<T> ScalarOne { get; }

        public IGaTuple<T> ScalarMinusOne { get; }

        public IGaTuple<T> ScalarTwo { get; }

        public IGaTuple<T> ScalarMinusTwo { get; }

        public IGaTuple<T> ScalarTen { get; }

        public IGaTuple<T> ScalarMinusTen { get; }

        public IGaTuple<T> ScalarPi { get; }

        public IGaTuple<T> ScalarE { get; }


        public GaScalarProcessorTuple([NotNull] IScalarProcessor<T> itemScalarProcessor)
        {
            ItemScalarProcessor = itemScalarProcessor;

            ScalarZero = GaConstantTuple<T>.Create(itemScalarProcessor, itemScalarProcessor.ScalarZero);
            ScalarOne = GaConstantTuple<T>.Create(itemScalarProcessor, itemScalarProcessor.ScalarOne);
            ScalarMinusOne = GaConstantTuple<T>.Create(itemScalarProcessor, itemScalarProcessor.ScalarMinusOne);
            ScalarTwo = GaConstantTuple<T>.Create(itemScalarProcessor, itemScalarProcessor.ScalarTwo);
            ScalarMinusTwo = GaConstantTuple<T>.Create(itemScalarProcessor, itemScalarProcessor.ScalarMinusTwo);
            ScalarTen = GaConstantTuple<T>.Create(itemScalarProcessor, itemScalarProcessor.ScalarTen);
            ScalarMinusTen = GaConstantTuple<T>.Create(itemScalarProcessor, itemScalarProcessor.ScalarMinusTen);
            ScalarPi = GaConstantTuple<T>.Create(itemScalarProcessor, itemScalarProcessor.ScalarPi);
            ScalarE = GaConstantTuple<T>.Create(itemScalarProcessor, itemScalarProcessor.ScalarE);
        }


        public IGaTuple<T> Add(IGaTuple<T> scalar1, IGaTuple<T> scalar2)
        {
            return scalar1.Add(scalar2);
        }

        public IGaTuple<T> Subtract(IGaTuple<T> scalar1, IGaTuple<T> scalar2)
        {
            return scalar1.Subtract(scalar2);
        }

        public IGaTuple<T> Times(IGaTuple<T> scalar1, IGaTuple<T> scalar2)
        {
            return scalar1.Times(scalar2);
        }

        public IGaTuple<T> NegativeTimes(IGaTuple<T> t1, IGaTuple<T> t2)
        {
            return t1.Times(t2).Negative();
        }

        public IGaTuple<T> Divide(IGaTuple<T> scalar1, IGaTuple<T> scalar2)
        {
            return scalar1.Divide(scalar2);
        }

        public IGaTuple<T> NegativeDivide(IGaTuple<T> scalar1, IGaTuple<T> scalar2)
        {
            return scalar1.Divide(scalar2).Negative();
        }

        public IGaTuple<T> Positive(IGaTuple<T> scalar)
        {
            return scalar;
        }

        public IGaTuple<T> Negative(IGaTuple<T> scalar)
        {
            return scalar.Negative();
        }

        public IGaTuple<T> Inverse(IGaTuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.Inverse);
        }

        public IGaTuple<T> Abs(IGaTuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.Abs);
        }

        public IGaTuple<T> Sqrt(IGaTuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.Sqrt);
        }

        public IGaTuple<T> SqrtOfAbs(IGaTuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.SqrtOfAbs);
        }

        public IGaTuple<T> Exp(IGaTuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.Exp);
        }

        public IGaTuple<T> Log(IGaTuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.Log);
        }

        public IGaTuple<T> Log2(IGaTuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.Log2);
        }

        public IGaTuple<T> Log10(IGaTuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.Log10);
        }
        
        public IGaTuple<T> Power(IGaTuple<T> baseScalar, IGaTuple<T> scalar)
        {
            return scalar.MapScalars(
                baseScalar, 
                (s1, s2) => ItemScalarProcessor.Power(s1, s2)
            );
        }

        public IGaTuple<T> Log(IGaTuple<T> baseScalar, IGaTuple<T> scalar)
        {
            return scalar.MapScalars(
                baseScalar, 
                (s1, s2) => ItemScalarProcessor.Log(s1, s2)
            );
        }

        public IGaTuple<T> Cos(IGaTuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.Cos);
        }

        public IGaTuple<T> Sin(IGaTuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.Sin);
        }

        public IGaTuple<T> Tan(IGaTuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.Tan);
        }

        public IGaTuple<T> ArcCos(IGaTuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.ArcCos);
        }

        public IGaTuple<T> ArcSin(IGaTuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.ArcSin);
        }

        public IGaTuple<T> ArcTan(IGaTuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.ArcTan);
        }

        public IGaTuple<T> ArcTan2(IGaTuple<T> scalarX, IGaTuple<T> scalarY)
        {
            return scalarX.MapScalars(scalarY, ItemScalarProcessor.ArcTan2);
        }

        public IGaTuple<T> Cosh(IGaTuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.Cosh);
        }

        public IGaTuple<T> Sinh(IGaTuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.Sinh);
        }

        public IGaTuple<T> Tanh(IGaTuple<T> scalar)
        {
            return scalar.MapScalars(ItemScalarProcessor.Tanh);
        }

        public bool IsValid(IGaTuple<T> scalar)
        {
            return scalar.IsValid();
        }

        public bool IsZero(IGaTuple<T> scalar)
        {
            throw new System.NotImplementedException();
        }

        public bool IsZero(IGaTuple<T> scalar, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? scalar.IsNearZero()
                : scalar.IsZero();
        }

        public bool IsNearZero(IGaTuple<T> scalar)
        {
            throw new System.NotImplementedException();
        }

        public bool IsNotZero(IGaTuple<T> scalar)
        {
            throw new System.NotImplementedException();
        }

        public bool IsNotZero(IGaTuple<T> scalar, bool nearZeroFlag)
        {
            throw new System.NotImplementedException();
        }

        public bool IsNotNearZero(IGaTuple<T> scalar)
        {
            throw new System.NotImplementedException();
        }

        public bool IsPositive(IGaTuple<T> scalar)
        {
            throw new System.NotImplementedException();
        }

        public bool IsNegative(IGaTuple<T> scalar)
        {
            throw new System.NotImplementedException();
        }

        public bool IsNotPositive(IGaTuple<T> scalar)
        {
            throw new System.NotImplementedException();
        }

        public bool IsNotNegative(IGaTuple<T> scalar)
        {
            throw new System.NotImplementedException();
        }

        public bool IsNotNearPositive(IGaTuple<T> scalar)
        {
            throw new System.NotImplementedException();
        }

        public bool IsNotNearNegative(IGaTuple<T> scalar)
        {
            throw new System.NotImplementedException();
        }

        public IGaTuple<T> GetScalarFromText(string text)
        {
            throw new System.NotImplementedException();
        }

        public IGaTuple<T> GetScalarFromInteger(int value)
        {
            return GaConstantTuple<T>.Create(
                ItemScalarProcessor, 
                ItemScalarProcessor.GetScalarFromInteger(value)
            );
        }

        public IGaTuple<T> GetScalarFromFloat64(double value)
        {
            return GaConstantTuple<T>.Create(
                ItemScalarProcessor, 
                ItemScalarProcessor.GetScalarFromFloat64(value)
            );
        }

        public IGaTuple<T> GetScalarFromRandom(System.Random randomGenerator, double minValue, double maxValue)
        {
            return GaConstantTuple<T>.Create(
                ItemScalarProcessor, 
                ItemScalarProcessor.GetScalarFromRandom(randomGenerator, minValue, maxValue)
            );
        }

        public string ToText(IGaTuple<T> scalar)
        {
            return scalar.ToString();
        }
    }
}