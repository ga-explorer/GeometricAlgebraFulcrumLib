using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Numbers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors.Composers;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions;

public sealed class MetaExpressionNumberFactory :
    MetaExpressionAtomicFactoryBase
{
    public MetaExpressionNumberFactory(MetaContext context)
        : base(context)
    {
    }


    public IMetaExpressionNumber GetOrDefine(double number)
    {
        return Context.GetOrDefineLiteralNumber((long)number);
    }

    public IEnumerable<IMetaExpressionNumber> GetOrDefineNumbers(IEnumerable<double> numbersList, int roundDigits = 0)
    {
        return numbersList.Select(s => Context.GetOrDefineLiteralNumber(s, roundDigits));
    }

    public IEnumerable<IMetaExpressionNumber> GetOrDefineNumbers(IEnumerable<int> numbersList)
    {
        return numbersList.Select(i => Context.GetOrDefineLiteralNumber(i));
    }

    public IEnumerable<IMetaExpressionNumber> GetOrDefineSymbolicNumbers(IEnumerable<Tuple<string, double>> numberTextList)
    {
        return numberTextList.Select(
            tuple => Context.GetOrDefineSymbolicNumber(tuple.Item1, tuple.Item2)
        );
    }


    public XGaVector<IMetaExpressionAtomic> CreateBasisVector(int index)
    {
        return Context.XGaProcessor.VectorTerm(
            index,
            Context.GetOrDefineLiteralNumber(1)
        );
    }

    public XGaKVector<IMetaExpressionAtomic> CreateBasisBlade(ulong id)
    {
        return Context.XGaProcessor.KVectorTerm(
            id.ToUInt64IndexSet(),
            Context.GetOrDefineLiteralNumber(1)
        );
    }

    //public KVectorStorage<IMetaExpressionAtomic> CreateBasisBlade(uint grade, ulong index)
    //{
    //    return Context.CreateKVectorStorageTerm(grade,
    //        index,
    //        Context.GetOrDefineLiteralNumber(1)
    //    );
    //}

}