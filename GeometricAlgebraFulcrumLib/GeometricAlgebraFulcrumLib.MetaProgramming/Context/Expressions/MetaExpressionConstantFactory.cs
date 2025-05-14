using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Numbers;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions;

public sealed class MetaExpressionConstantFactory :
    MetaExpressionAtomicFactoryBase
{
    internal MetaExpressionConstantFactory(MetaContext context)
        : base(context)
    {
    }


    public XGaVector<IMetaExpressionAtomic> CreateVectorTerm(int index)
    {
        return Context
            .XGaProcessor
            .VectorTerm(index, Context.OneValue);
    }

    public XGaVector<IMetaExpressionAtomic> CreateVectorTerm(int index, int numberValue)
    {
        var number =
            Context.GetOrDefineLiteralNumber(numberValue);

        return Context
            .XGaProcessor
            .VectorTerm(index, number);
    }

    public XGaVector<IMetaExpressionAtomic> CreateVectorTerm(int index, uint numberValue)
    {
        var number =
            Context.GetOrDefineLiteralNumber(numberValue);

        return Context
            .XGaProcessor
            .VectorTerm(index, number);
    }

    public XGaVector<IMetaExpressionAtomic> CreateVectorTerm(int index, long numberValue)
    {
        var number =
            Context.GetOrDefineLiteralNumber(numberValue);

        return Context
            .XGaProcessor
            .VectorTerm(index, number);
    }

    public XGaVector<IMetaExpressionAtomic> CreateVectorTerm(int index, ulong numberValue)
    {
        var number =
            Context.GetOrDefineLiteralNumber(numberValue);

        return Context
            .XGaProcessor
            .VectorTerm(index, number);
    }

    public XGaVector<IMetaExpressionAtomic> CreateVectorTerm(int index, float numberValue)
    {
        var number =
            Context.GetOrDefineLiteralNumber(numberValue);

        return Context
            .XGaProcessor
            .VectorTerm(index, number);
    }

    public XGaVector<IMetaExpressionAtomic> CreateVectorTerm(int index, double numberValue)
    {
        var number =
            Context.GetOrDefineLiteralNumber(numberValue);

        return Context
            .XGaProcessor
            .VectorTerm(index, number);
    }

    public XGaVector<IMetaExpressionAtomic> CreateVectorTerm(int index, long numerator, long denominator)
    {
        var number =
            Context.GetOrDefineRationalNumber(numerator, denominator);

        return Context
            .XGaProcessor
            .VectorTerm(index, number);
    }

    public XGaVector<IMetaExpressionAtomic> CreateVectorTerm(int index, IMetaExpressionNumber number)
    {
        return Context
            .XGaProcessor
            .VectorTerm(index, number);
    }
}