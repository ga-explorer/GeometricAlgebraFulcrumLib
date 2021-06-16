using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraLib.Processors.Scalars;

namespace GeometricAlgebraLib.Implementations.NamedScalars
{
    public sealed class GaScalarProcessorNamedScalar<TScalar>
        : IGaScalarProcessorNamedScalar<TScalar>
    {
        public GaNamedScalarsCollection<TScalar> NamedScalarsCollection { get; }

        public IGaSymbolicScalarProcessor<TScalar> SymbolicScalarProcessor 
            => NamedScalarsCollection.SymbolicScalarProcessor;

        public int RoundingPlaces { get; set; }
            = 13;

        public double ZeroEpsilon 
            => Math.Pow(10, -RoundingPlaces);

        public IGaNamedScalar<TScalar> ZeroScalar
            => NamedScalarsCollection.GetConstantNamedScalar(SymbolicScalarProcessor.ZeroScalar);

        public IGaNamedScalar<TScalar> OneScalar 
            => NamedScalarsCollection.GetConstantNamedScalar(SymbolicScalarProcessor.OneScalar);
        
        public IGaNamedScalar<TScalar> MinusOneScalar 
            => NamedScalarsCollection.GetConstantNamedScalar(SymbolicScalarProcessor.MinusOneScalar);
        
        public IGaNamedScalar<TScalar> PiScalar 
            => NamedScalarsCollection.GetConstantNamedScalar(SymbolicScalarProcessor.PiScalar);


        public GaScalarProcessorNamedScalar([NotNull] GaNamedScalarsCollection<TScalar> baseCollection)
        {
            NamedScalarsCollection = baseCollection;
        }


        public IGaNamedScalar<TScalar> Add(IGaNamedScalar<TScalar> scalar1, IGaNamedScalar<TScalar> scalar2)
        {
            return NamedScalarsCollection.GetVariableNamedScalar(
                SymbolicScalarProcessor.Add,
                scalar1,
                scalar2
            );
        }

        public IGaNamedScalar<TScalar> Add(params IGaNamedScalar<TScalar>[] scalarsList)
        {
            return NamedScalarsCollection.GetVariableNamedScalar(
                SymbolicScalarProcessor.Add,
                scalarsList
            );
        }

        public IGaNamedScalar<TScalar> Add(IEnumerable<IGaNamedScalar<TScalar>> scalarsList)
        {
            return NamedScalarsCollection.GetVariableNamedScalar(
                SymbolicScalarProcessor.Add,
                scalarsList
            );
        }

        public IGaNamedScalar<TScalar> Subtract(IGaNamedScalar<TScalar> scalar1, IGaNamedScalar<TScalar> scalar2)
        {
            return NamedScalarsCollection.GetVariableNamedScalar(
                SymbolicScalarProcessor.Subtract,
                scalar1,
                scalar2
            );
        }

        public IGaNamedScalar<TScalar> Times(IGaNamedScalar<TScalar> scalar1, IGaNamedScalar<TScalar> scalar2)
        {
            return NamedScalarsCollection.GetVariableNamedScalar(
                SymbolicScalarProcessor.Times,
                scalar1,
                scalar2
            );
        }

        public IGaNamedScalar<TScalar> Times(params IGaNamedScalar<TScalar>[] scalarsList)
        {
            return NamedScalarsCollection.GetVariableNamedScalar(
                SymbolicScalarProcessor.Times,
                scalarsList
            );
        }

        public IGaNamedScalar<TScalar> Times(IEnumerable<IGaNamedScalar<TScalar>> scalarsList)
        {
            return NamedScalarsCollection.GetVariableNamedScalar(
                SymbolicScalarProcessor.Times,
                scalarsList
            );
        }

        public IGaNamedScalar<TScalar> NegativeTimes(IGaNamedScalar<TScalar> scalar1, IGaNamedScalar<TScalar> scalar2)
        {
            return NamedScalarsCollection.GetVariableNamedScalar(
                SymbolicScalarProcessor.NegativeTimes,
                scalar1,
                scalar2
            );
        }

        public IGaNamedScalar<TScalar> NegativeTimes(params IGaNamedScalar<TScalar>[] scalarsList)
        {
            return NamedScalarsCollection.GetVariableNamedScalar(
                SymbolicScalarProcessor.NegativeTimes,
                scalarsList
            );
        }

        public IGaNamedScalar<TScalar> NegativeTimes(IEnumerable<IGaNamedScalar<TScalar>> scalarsList)
        {
            return NamedScalarsCollection.GetVariableNamedScalar(
                SymbolicScalarProcessor.NegativeTimes,
                scalarsList
            );
        }

        public IGaNamedScalar<TScalar> Divide(IGaNamedScalar<TScalar> scalar1, IGaNamedScalar<TScalar> scalar2)
        {
            return NamedScalarsCollection.GetVariableNamedScalar(
                SymbolicScalarProcessor.Divide,
                scalar1,
                scalar2
            );
        }

        public IGaNamedScalar<TScalar> NegativeDivide(IGaNamedScalar<TScalar> scalar1, IGaNamedScalar<TScalar> scalar2)
        {
            return NamedScalarsCollection.GetVariableNamedScalar(
                SymbolicScalarProcessor.NegativeDivide,
                scalar1,
                scalar2
            );
        }

        public IGaNamedScalar<TScalar> Positive(IGaNamedScalar<TScalar> scalar)
        {
            return scalar;
        }

        public IGaNamedScalar<TScalar> Negative(IGaNamedScalar<TScalar> scalar)
        {
            return NamedScalarsCollection.GetVariableNamedScalar(
                SymbolicScalarProcessor.Negative,
                scalar
            );
        }

        public IGaNamedScalar<TScalar> Inverse(IGaNamedScalar<TScalar> scalar)
        {
            return NamedScalarsCollection.GetVariableNamedScalar(
                SymbolicScalarProcessor.Inverse,
                scalar
            );
        }

        public IGaNamedScalar<TScalar> Abs(IGaNamedScalar<TScalar> scalar)
        {
            return NamedScalarsCollection.GetVariableNamedScalar(
                SymbolicScalarProcessor.Abs,
                scalar
            );
        }

        public IGaNamedScalar<TScalar> Sqrt(IGaNamedScalar<TScalar> scalar)
        {
            return NamedScalarsCollection.GetVariableNamedScalar(
                SymbolicScalarProcessor.Sqrt,
                scalar
            );
        }

        public IGaNamedScalar<TScalar> SqrtOfAbs(IGaNamedScalar<TScalar> scalar)
        {
            return NamedScalarsCollection.GetVariableNamedScalar(
                SymbolicScalarProcessor.SqrtOfAbs,
                scalar
            );
        }

        public IGaNamedScalar<TScalar> Exp(IGaNamedScalar<TScalar> scalar)
        {
            return NamedScalarsCollection.GetVariableNamedScalar(
                SymbolicScalarProcessor.Exp,
                scalar
            );
        }

        public IGaNamedScalar<TScalar> Log(IGaNamedScalar<TScalar> scalar)
        {
            return NamedScalarsCollection.GetVariableNamedScalar(
                SymbolicScalarProcessor.Log,
                scalar
            );
        }

        public IGaNamedScalar<TScalar> Log2(IGaNamedScalar<TScalar> scalar)
        {
            return NamedScalarsCollection.GetVariableNamedScalar(
                SymbolicScalarProcessor.Log2,
                scalar
            );
        }

        public IGaNamedScalar<TScalar> Log10(IGaNamedScalar<TScalar> scalar)
        {
            return NamedScalarsCollection.GetVariableNamedScalar(
                SymbolicScalarProcessor.Log10,
                scalar
            );
        }

        public IGaNamedScalar<TScalar> Log(IGaNamedScalar<TScalar> scalar, IGaNamedScalar<TScalar> baseScalar)
        {
            return NamedScalarsCollection.GetVariableNamedScalar(
                SymbolicScalarProcessor.Log,
                scalar,
                baseScalar
            );
        }

        public IGaNamedScalar<TScalar> Cos(IGaNamedScalar<TScalar> scalar)
        {
            return NamedScalarsCollection.GetVariableNamedScalar(
                SymbolicScalarProcessor.Cos,
                scalar
            );
        }

        public IGaNamedScalar<TScalar> Sin(IGaNamedScalar<TScalar> scalar)
        {
            return NamedScalarsCollection.GetVariableNamedScalar(
                SymbolicScalarProcessor.Sin,
                scalar
            );
        }

        public IGaNamedScalar<TScalar> Tan(IGaNamedScalar<TScalar> scalar)
        {
            return NamedScalarsCollection.GetVariableNamedScalar(
                SymbolicScalarProcessor.Tan,
                scalar
            );
        }

        public IGaNamedScalar<TScalar> ArcCos(IGaNamedScalar<TScalar> scalar)
        {
            return NamedScalarsCollection.GetVariableNamedScalar(
                SymbolicScalarProcessor.ArcCos,
                scalar
            );
        }

        public IGaNamedScalar<TScalar> ArcSin(IGaNamedScalar<TScalar> scalar)
        {
            return NamedScalarsCollection.GetVariableNamedScalar(
                SymbolicScalarProcessor.ArcSin,
                scalar
            );
        }

        public IGaNamedScalar<TScalar> ArcTan(IGaNamedScalar<TScalar> scalar)
        {
            return NamedScalarsCollection.GetVariableNamedScalar(
                SymbolicScalarProcessor.ArcTan,
                scalar
            );
        }

        public IGaNamedScalar<TScalar> ArcTan2(IGaNamedScalar<TScalar> scalarX, IGaNamedScalar<TScalar> scalarY)
        {
            return NamedScalarsCollection.GetVariableNamedScalar(
                SymbolicScalarProcessor.ArcTan2,
                scalarX,
                scalarY
            );
        }

        public bool IsValid(IGaNamedScalar<TScalar> scalar)
        {
            return SymbolicScalarProcessor.IsValid(scalar.RhsScalarValue);
        }

        public bool IsZero(IGaNamedScalar<TScalar> scalar)
        {
            return SymbolicScalarProcessor.IsZero(scalar.RhsScalarValue);
        }

        public bool IsZero(IGaNamedScalar<TScalar> scalar, bool nearZeroFlag)
        {
            return SymbolicScalarProcessor.IsZero(scalar.RhsScalarValue, nearZeroFlag);
        }

        public bool IsNearZero(IGaNamedScalar<TScalar> scalar)
        {
            return SymbolicScalarProcessor.IsNearZero(scalar.RhsScalarValue);
        }

        public IGaNamedScalar<TScalar> TextToScalar(string text)
        {
            return NamedScalarsCollection.GetNamedScalarByValueText(text);
        }

        public IGaNamedScalar<TScalar> IntegerToScalar(int value)
        {
            return NamedScalarsCollection.GetConstantNamedScalar(
                SymbolicScalarProcessor.IntegerToScalar(value)
            );
        }

        public IGaNamedScalar<TScalar> Float64ToScalar(double value)
        {
            return NamedScalarsCollection.GetConstantNamedScalar(
                SymbolicScalarProcessor.Float64ToScalar(value)
            );
        }

        public IGaNamedScalar<TScalar> GetRandomScalar(Random randomGenerator, double minValue, double maxValue)
        {
            return NamedScalarsCollection.GetConstantNamedScalar(
                SymbolicScalarProcessor.GetRandomScalar(randomGenerator, minValue, maxValue)
            );
        }

        public string ToText(IGaNamedScalar<TScalar> scalar)
        {
            return SymbolicScalarProcessor.ToText(scalar.RhsScalarValue);
        }

        public IGaNamedScalar<TScalar> GetSymbol(string symbolNameText)
        {
            return NamedScalarsCollection.GetNamedScalarByName(symbolNameText);
        }
    }
}