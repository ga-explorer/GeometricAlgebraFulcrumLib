using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GeometricAlgebraFulcrumLib.Processing.Tuples;

namespace GeometricAlgebraFulcrumLib.Processing.Scalars
{
    public sealed class GaScalarProcessorTuple<T>
        : IGaScalarProcessor<IGaTuple<T>>
    {
        public IGaScalarProcessor<T> ItemsScalarsDomain { get; }

        public IGaTuple<T> ZeroScalar { get; }

        public IGaTuple<T> OneScalar { get; }

        public IGaTuple<T> MinusOneScalar { get; }

        public IGaTuple<T> PiScalar { get; }


        public GaScalarProcessorTuple([NotNull] IGaScalarProcessor<T> itemsScalarsDomain)
        {
            ItemsScalarsDomain = itemsScalarsDomain;

            ZeroScalar = GaConstantTuple<T>.Create(itemsScalarsDomain, itemsScalarsDomain.ZeroScalar);
            OneScalar = GaConstantTuple<T>.Create(itemsScalarsDomain, itemsScalarsDomain.OneScalar);
            MinusOneScalar = GaConstantTuple<T>.Create(itemsScalarsDomain, itemsScalarsDomain.MinusOneScalar);
            PiScalar = GaConstantTuple<T>.Create(itemsScalarsDomain, itemsScalarsDomain.PiScalar);
        }


        public IGaTuple<T> Add(IGaTuple<T> scalar1, IGaTuple<T> scalar2)
        {
            return scalar1.Add(scalar2);
        }

        public IGaTuple<T> Add(params IGaTuple<T>[] scalarsList)
        {
            return scalarsList.Skip(1).Aggregate(
                scalarsList[0],
                (current, item) => current.Add(item)
            );
        }

        public IGaTuple<T> Add(IEnumerable<IGaTuple<T>> scalarsList)
        {
            return scalarsList.Skip(1).Aggregate(
                scalarsList.First(),
                (current, item) => current.Add(item)
            );
        }

        public IGaTuple<T> Subtract(IGaTuple<T> scalar1, IGaTuple<T> scalar2)
        {
            return scalar1.Subtract(scalar2);
        }

        public IGaTuple<T> Times(IGaTuple<T> scalar1, IGaTuple<T> scalar2)
        {
            return scalar1.Times(scalar2);
        }

        public IGaTuple<T> Times(params IGaTuple<T>[] scalarsList)
        {
            return scalarsList.Skip(1).Aggregate(
                scalarsList[0],
                (current, item) => current.Times(item)
            );
        }

        public IGaTuple<T> Times(IEnumerable<IGaTuple<T>> scalarsList)
        {
            return scalarsList.Skip(1).Aggregate(
                scalarsList.First(),
                (current, item) => current.Times(item)
            );
        }

        public IGaTuple<T> NegativeTimes(IGaTuple<T> t1, IGaTuple<T> t2)
        {
            return t1.Times(t2).Negative();
        }

        public IGaTuple<T> NegativeTimes(params IGaTuple<T>[] scalarsList)
        {
            return Times(scalarsList).Negative();
        }

        public IGaTuple<T> NegativeTimes(IEnumerable<IGaTuple<T>> scalarsList)
        {
            return Times(scalarsList).Negative();
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
            return scalar.MapScalars(ItemsScalarsDomain.Inverse);
        }

        public IGaTuple<T> Abs(IGaTuple<T> scalar)
        {
            return scalar.MapScalars(ItemsScalarsDomain.Abs);
        }

        public IGaTuple<T> Sqrt(IGaTuple<T> scalar)
        {
            return scalar.MapScalars(ItemsScalarsDomain.Sqrt);
        }

        public IGaTuple<T> SqrtOfAbs(IGaTuple<T> scalar)
        {
            return scalar.MapScalars(ItemsScalarsDomain.SqrtOfAbs);
        }

        public IGaTuple<T> Exp(IGaTuple<T> scalar)
        {
            return scalar.MapScalars(ItemsScalarsDomain.Exp);
        }

        public IGaTuple<T> Log(IGaTuple<T> scalar)
        {
            return scalar.MapScalars(ItemsScalarsDomain.Log);
        }

        public IGaTuple<T> Log2(IGaTuple<T> scalar)
        {
            return scalar.MapScalars(ItemsScalarsDomain.Log2);
        }

        public IGaTuple<T> Log10(IGaTuple<T> scalar)
        {
            return scalar.MapScalars(ItemsScalarsDomain.Log10);
        }

        public IGaTuple<T> Log(IGaTuple<T> scalar, IGaTuple<T> baseScalar)
        {
            return scalar.MapScalars(
                baseScalar, 
                (s1, s2) => ItemsScalarsDomain.Log(s1, s2)
            );
        }

        public IGaTuple<T> Cos(IGaTuple<T> scalar)
        {
            return scalar.MapScalars(ItemsScalarsDomain.Cos);
        }

        public IGaTuple<T> Sin(IGaTuple<T> scalar)
        {
            return scalar.MapScalars(ItemsScalarsDomain.Sin);
        }

        public IGaTuple<T> Tan(IGaTuple<T> scalar)
        {
            return scalar.MapScalars(ItemsScalarsDomain.Tan);
        }

        public IGaTuple<T> ArcCos(IGaTuple<T> scalar)
        {
            return scalar.MapScalars(ItemsScalarsDomain.ArcCos);
        }

        public IGaTuple<T> ArcSin(IGaTuple<T> scalar)
        {
            return scalar.MapScalars(ItemsScalarsDomain.ArcSin);
        }

        public IGaTuple<T> ArcTan(IGaTuple<T> scalar)
        {
            return scalar.MapScalars(ItemsScalarsDomain.ArcTan);
        }

        public IGaTuple<T> ArcTan2(IGaTuple<T> scalarX, IGaTuple<T> scalarY)
        {
            return scalarX.MapScalars(scalarY, ItemsScalarsDomain.ArcTan2);
        }

        public bool IsValid(IGaTuple<T> scalar)
        {
            throw new System.NotImplementedException();
        }

        public bool IsZero(IGaTuple<T> scalar)
        {
            return scalar.IsZero();
        }

        public bool IsZero(IGaTuple<T> scalar, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? scalar.IsNearZero()
                : scalar.IsZero();
        }

        public bool IsNearZero(IGaTuple<T> scalar)
        {
            return scalar.IsNearZero();
        }

        public IGaTuple<T> TextToScalar(string text)
        {
            throw new System.NotImplementedException();
        }

        public IGaTuple<T> IntegerToScalar(int value)
        {
            return GaConstantTuple<T>.Create(
                ItemsScalarsDomain, 
                ItemsScalarsDomain.IntegerToScalar(value)
            );
        }

        public IGaTuple<T> Float64ToScalar(double value)
        {
            return GaConstantTuple<T>.Create(
                ItemsScalarsDomain, 
                ItemsScalarsDomain.Float64ToScalar(value)
            );
        }

        public IGaTuple<T> GetRandomScalar(System.Random randomGenerator, double minValue, double maxValue)
        {
            return GaConstantTuple<T>.Create(
                ItemsScalarsDomain, 
                ItemsScalarsDomain.GetRandomScalar(randomGenerator, minValue, maxValue)
            );
        }

        public string ToText(IGaTuple<T> scalar)
        {
            return scalar.ToString();
        }
    }
}