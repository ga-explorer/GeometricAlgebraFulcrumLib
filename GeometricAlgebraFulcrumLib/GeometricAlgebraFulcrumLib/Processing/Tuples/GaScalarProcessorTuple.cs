using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Processing.Scalars;

namespace GeometricAlgebraFulcrumLib.Processing.Tuples
{
    public sealed class GaScalarProcessorTuple<T>
        : IGaScalarProcessor<IGaTuple<T>>
    {
        private readonly IGaTuple<T> _zeroScalar;
        private readonly IGaTuple<T> _oneScalar;
        private readonly IGaTuple<T> _minusOneScalar;
        private readonly IGaTuple<T> _piScalar;
        public IGaScalarProcessor<T> ItemsScalarsDomain { get; }

        public bool IsNumeric => false;

        public bool IsSymbolic => false;

        public IGaTuple<T> GetZeroScalar()
        {
            return _zeroScalar;
        }

        public IGaTuple<T> GetOneScalar()
        {
            return _oneScalar;
        }

        public IGaTuple<T> GetMinusOneScalar()
        {
            return _minusOneScalar;
        }

        public IGaTuple<T> GetPiScalar()
        {
            return _piScalar;
        }

        public IGaTuple<T>[] GetZeroScalarArray1D(int count)
        {
            throw new System.NotImplementedException();
        }

        public IGaTuple<T>[,] GetZeroScalarArray2D(int count)
        {
            throw new System.NotImplementedException();
        }

        public IGaTuple<T>[,] GetZeroScalarArray2D(int count1, int count2)
        {
            throw new System.NotImplementedException();
        }


        public GaScalarProcessorTuple([NotNull] IGaScalarProcessor<T> itemsScalarsDomain)
        {
            ItemsScalarsDomain = itemsScalarsDomain;

            _zeroScalar = GaConstantTuple<T>.Create(itemsScalarsDomain, itemsScalarsDomain.GetZeroScalar());
            _oneScalar = GaConstantTuple<T>.Create(itemsScalarsDomain, itemsScalarsDomain.GetOneScalar());
            _minusOneScalar = GaConstantTuple<T>.Create(itemsScalarsDomain, itemsScalarsDomain.GetMinusOneScalar());
            _piScalar = GaConstantTuple<T>.Create(itemsScalarsDomain, itemsScalarsDomain.GetPiScalar());
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

        public IGaTuple<T> Cosh(IGaTuple<T> scalar)
        {
            return scalar.MapScalars(ItemsScalarsDomain.Cosh);
        }

        public IGaTuple<T> Sinh(IGaTuple<T> scalar)
        {
            return scalar.MapScalars(ItemsScalarsDomain.Sinh);
        }

        public IGaTuple<T> Tanh(IGaTuple<T> scalar)
        {
            return scalar.MapScalars(ItemsScalarsDomain.Tanh);
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