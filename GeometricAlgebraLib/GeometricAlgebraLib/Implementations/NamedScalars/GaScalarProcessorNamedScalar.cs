using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraLib.Processors.Scalars;
using GeometricAlgebraLib.SymbolicExpressions;
using GeometricAlgebraLib.SymbolicExpressions.Context;

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
            => NamedScalarsCollection.GetOrDefineConstant(SymbolicScalarProcessor.ZeroScalar);

        public IGaNamedScalar<TScalar> OneScalar 
            => NamedScalarsCollection.GetOrDefineConstant(SymbolicScalarProcessor.OneScalar);
        
        public IGaNamedScalar<TScalar> MinusOneScalar 
            => NamedScalarsCollection.GetOrDefineConstant(SymbolicScalarProcessor.MinusOneScalar);
        
        public IGaNamedScalar<TScalar> PiScalar 
            => NamedScalarsCollection.GetOrDefineConstant(SymbolicScalarProcessor.PiScalar);


        public GaScalarProcessorNamedScalar([NotNull] GaNamedScalarsCollection<TScalar> baseCollection)
        {
            NamedScalarsCollection = baseCollection;
        }


        public IGaNamedScalar<TScalar> Add(IGaNamedScalar<TScalar> scalar1, IGaNamedScalar<TScalar> scalar2)
        {
            return NamedScalarsCollection.GetOrDefineVariable(
                SymbolicScalarProcessor.Add,
                scalar1,
                scalar2
            );
        }

        public IGaNamedScalar<TScalar> Add(params IGaNamedScalar<TScalar>[] scalarsList)
        {
            return NamedScalarsCollection.GetOrDefineVariable(
                SymbolicScalarProcessor.Add,
                scalarsList
            );
        }

        public IGaNamedScalar<TScalar> Add(IEnumerable<IGaNamedScalar<TScalar>> scalarsList)
        {
            return NamedScalarsCollection.GetOrDefineVariable(
                SymbolicScalarProcessor.Add,
                scalarsList
            );
        }

        public IGaNamedScalar<TScalar> Subtract(IGaNamedScalar<TScalar> scalar1, IGaNamedScalar<TScalar> scalar2)
        {
            return NamedScalarsCollection.GetOrDefineVariable(
                SymbolicScalarProcessor.Subtract,
                scalar1,
                scalar2
            );
        }

        public IGaNamedScalar<TScalar> Times(IGaNamedScalar<TScalar> scalar1, IGaNamedScalar<TScalar> scalar2)
        {
            return NamedScalarsCollection.GetOrDefineVariable(
                SymbolicScalarProcessor.Times,
                scalar1,
                scalar2
            );
        }

        public IGaNamedScalar<TScalar> Times(params IGaNamedScalar<TScalar>[] scalarsList)
        {
            return NamedScalarsCollection.GetOrDefineVariable(
                SymbolicScalarProcessor.Times,
                scalarsList
            );
        }

        public IGaNamedScalar<TScalar> Times(IEnumerable<IGaNamedScalar<TScalar>> scalarsList)
        {
            return NamedScalarsCollection.GetOrDefineVariable(
                SymbolicScalarProcessor.Times,
                scalarsList
            );
        }

        public IGaNamedScalar<TScalar> NegativeTimes(IGaNamedScalar<TScalar> scalar1, IGaNamedScalar<TScalar> scalar2)
        {
            return NamedScalarsCollection.GetOrDefineVariable(
                SymbolicScalarProcessor.NegativeTimes,
                scalar1,
                scalar2
            );
        }

        public IGaNamedScalar<TScalar> NegativeTimes(params IGaNamedScalar<TScalar>[] scalarsList)
        {
            return NamedScalarsCollection.GetOrDefineVariable(
                SymbolicScalarProcessor.NegativeTimes,
                scalarsList
            );
        }

        public IGaNamedScalar<TScalar> NegativeTimes(IEnumerable<IGaNamedScalar<TScalar>> scalarsList)
        {
            return NamedScalarsCollection.GetOrDefineVariable(
                SymbolicScalarProcessor.NegativeTimes,
                scalarsList
            );
        }

        public IGaNamedScalar<TScalar> Divide(IGaNamedScalar<TScalar> scalar1, IGaNamedScalar<TScalar> scalar2)
        {
            return NamedScalarsCollection.GetOrDefineVariable(
                SymbolicScalarProcessor.Divide,
                scalar1,
                scalar2
            );
        }

        public IGaNamedScalar<TScalar> NegativeDivide(IGaNamedScalar<TScalar> scalar1, IGaNamedScalar<TScalar> scalar2)
        {
            return NamedScalarsCollection.GetOrDefineVariable(
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
            return NamedScalarsCollection.GetOrDefineVariable(
                SymbolicScalarProcessor.Negative,
                scalar
            );
        }

        public IGaNamedScalar<TScalar> Inverse(IGaNamedScalar<TScalar> scalar)
        {
            return NamedScalarsCollection.GetOrDefineVariable(
                SymbolicScalarProcessor.Inverse,
                scalar
            );
        }

        public IGaNamedScalar<TScalar> Abs(IGaNamedScalar<TScalar> scalar)
        {
            return NamedScalarsCollection.GetOrDefineVariable(
                SymbolicScalarProcessor.Abs,
                scalar
            );
        }

        public IGaNamedScalar<TScalar> Sqrt(IGaNamedScalar<TScalar> scalar)
        {
            return NamedScalarsCollection.GetOrDefineVariable(
                SymbolicScalarProcessor.Sqrt,
                scalar
            );
        }

        public IGaNamedScalar<TScalar> SqrtOfAbs(IGaNamedScalar<TScalar> scalar)
        {
            return NamedScalarsCollection.GetOrDefineVariable(
                SymbolicScalarProcessor.SqrtOfAbs,
                scalar
            );
        }

        public IGaNamedScalar<TScalar> Exp(IGaNamedScalar<TScalar> scalar)
        {
            return NamedScalarsCollection.GetOrDefineVariable(
                SymbolicScalarProcessor.Exp,
                scalar
            );
        }

        public IGaNamedScalar<TScalar> Log(IGaNamedScalar<TScalar> scalar)
        {
            return NamedScalarsCollection.GetOrDefineVariable(
                SymbolicScalarProcessor.Log,
                scalar
            );
        }

        public IGaNamedScalar<TScalar> Log2(IGaNamedScalar<TScalar> scalar)
        {
            return NamedScalarsCollection.GetOrDefineVariable(
                SymbolicScalarProcessor.Log2,
                scalar
            );
        }

        public IGaNamedScalar<TScalar> Log10(IGaNamedScalar<TScalar> scalar)
        {
            return NamedScalarsCollection.GetOrDefineVariable(
                SymbolicScalarProcessor.Log10,
                scalar
            );
        }

        public IGaNamedScalar<TScalar> Log(IGaNamedScalar<TScalar> scalar, IGaNamedScalar<TScalar> baseScalar)
        {
            return NamedScalarsCollection.GetOrDefineVariable(
                SymbolicScalarProcessor.Log,
                scalar,
                baseScalar
            );
        }

        public IGaNamedScalar<TScalar> Cos(IGaNamedScalar<TScalar> scalar)
        {
            return NamedScalarsCollection.GetOrDefineVariable(
                SymbolicScalarProcessor.Cos,
                scalar
            );
        }

        public IGaNamedScalar<TScalar> Sin(IGaNamedScalar<TScalar> scalar)
        {
            return NamedScalarsCollection.GetOrDefineVariable(
                SymbolicScalarProcessor.Sin,
                scalar
            );
        }

        public IGaNamedScalar<TScalar> Tan(IGaNamedScalar<TScalar> scalar)
        {
            return NamedScalarsCollection.GetOrDefineVariable(
                SymbolicScalarProcessor.Tan,
                scalar
            );
        }

        public IGaNamedScalar<TScalar> ArcCos(IGaNamedScalar<TScalar> scalar)
        {
            return NamedScalarsCollection.GetOrDefineVariable(
                SymbolicScalarProcessor.ArcCos,
                scalar
            );
        }

        public IGaNamedScalar<TScalar> ArcSin(IGaNamedScalar<TScalar> scalar)
        {
            return NamedScalarsCollection.GetOrDefineVariable(
                SymbolicScalarProcessor.ArcSin,
                scalar
            );
        }

        public IGaNamedScalar<TScalar> ArcTan(IGaNamedScalar<TScalar> scalar)
        {
            return NamedScalarsCollection.GetOrDefineVariable(
                SymbolicScalarProcessor.ArcTan,
                scalar
            );
        }

        public IGaNamedScalar<TScalar> ArcTan2(IGaNamedScalar<TScalar> scalarX, IGaNamedScalar<TScalar> scalarY)
        {
            return NamedScalarsCollection.GetOrDefineVariable(
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
            return NamedScalarsCollection.GetOrDefineConstant(
                SymbolicScalarProcessor.IntegerToScalar(value)
            );
        }

        public IGaNamedScalar<TScalar> Float64ToScalar(double value)
        {
            return NamedScalarsCollection.GetOrDefineConstant(
                SymbolicScalarProcessor.Float64ToScalar(value)
            );
        }

        public IGaNamedScalar<TScalar> GetRandomScalar(Random randomGenerator, double minValue, double maxValue)
        {
            return NamedScalarsCollection.GetOrDefineConstant(
                SymbolicScalarProcessor.GetRandomScalar(randomGenerator, minValue, maxValue)
            );
        }

        public string ToText(IGaNamedScalar<TScalar> scalar)
        {
            return SymbolicScalarProcessor.ToText(scalar.RhsScalarValue);
        }

        public IGaNamedScalar<TScalar> Simplify(IGaNamedScalar<TScalar> scalar)
        {
            throw new NotImplementedException();
        }

        public IGaNamedScalar<TScalar> GetSymbol(string symbolNameText)
        {
            //return NamedScalarsCollection.GetNamedScalarByName(symbolNameText);
            throw new NotImplementedException();
        }

        public IGaNamedScalar<TScalar> SymbolicExpressionToScalar(ISymbolicExpression expression)
        {
            //return NamedScalarsCollection.GetNamedScalarByValueText(expression.ToString());
            throw new NotImplementedException();
        }

        public ISymbolicExpression ScalarToSymbolicExpression(SymbolicContext context, IGaNamedScalar<TScalar> scalar)
        {
            //return SymbolicScalarProcessor.ToSteExpression(scalar.LhsScalarValue);
            throw new NotImplementedException();
        }
    }
}