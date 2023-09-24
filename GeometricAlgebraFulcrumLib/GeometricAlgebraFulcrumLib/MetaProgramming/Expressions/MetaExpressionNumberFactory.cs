using System;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Numbers;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Expressions
{
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

        public IEnumerable<IMetaExpressionNumber> GetOrDefineNumbers(IEnumerable<double> numbersList)
        {
            return numbersList.Select(Context.GetOrDefineLiteralNumber);
        }

        public IEnumerable<IMetaExpressionNumber> GetOrDefineNumbers(IEnumerable<int> numbersList)
        {
            return numbersList.Select(Context.GetOrDefineLiteralNumber);
        }

        public IEnumerable<IMetaExpressionNumber> GetOrDefineSymbolicNumbers(IEnumerable<Tuple<string, double>> numberTextList)
        {
            return numberTextList.Select(
                tuple => Context.GetOrDefineSymbolicNumber(tuple.Item1, tuple.Item2)
            );
        }


        public XGaVector<IMetaExpressionAtomic> CreateBasisVector(int index)
        {
            return Context.XGaProcessor.CreateTermVector(
                index,
                Context.GetOrDefineLiteralNumber(1)
            );
        }

        public XGaKVector<IMetaExpressionAtomic> CreateBasisBlade(ulong id)
        {
            return Context.XGaProcessor.CreateTermKVector(
                id.BitPatternToUInt64IndexSet(),
                Context.GetOrDefineLiteralNumber(1)
            );
        }

        public KVectorStorage<IMetaExpressionAtomic> CreateBasisBlade(uint grade, ulong index)
        {
            return Context.CreateKVectorStorageTerm(grade,
                index,
                Context.GetOrDefineLiteralNumber(1)
            );
        }

    }
}