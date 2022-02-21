using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.Numbers;
using GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Context;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public sealed class SymbolicNumberFactory :
        SymbolicAtomicExpressionFactoryBase
    {
        public SymbolicNumberFactory(SymbolicContext context) 
            : base(context)
        {
        }


        public ISymbolicNumber GetOrDefine(double number)
        {
            return Context.GetOrDefineLiteralNumber((long) number);
        }

        public IEnumerable<ISymbolicNumber> GetOrDefineNumbers(IEnumerable<double> numbersList)
        {
            return numbersList.Select(Context.GetOrDefineLiteralNumber);
        }

        public IEnumerable<ISymbolicNumber> GetOrDefineNumbers(IEnumerable<int> numbersList)
        {
            return numbersList.Select(Context.GetOrDefineLiteralNumber);
        }

        public IEnumerable<ISymbolicNumber> GetOrDefineSymbolicNumbers(IEnumerable<Tuple<string, double>> numberTextList)
        {
            return numberTextList.Select(
                tuple => Context.GetOrDefineSymbolicNumber(tuple.Item1, tuple.Item2)
            );
        }

        
        public VectorStorage<ISymbolicExpressionAtomic> CreateBasisVector(ulong index)
        {
            return Context.CreateVectorStorageTerm(index,
                Context.GetOrDefineLiteralNumber(1)
            );
        }
        
        public KVectorStorage<ISymbolicExpressionAtomic> CreateBasisBlade(ulong id)
        {
            return Context.CreateKVectorStorageTerm(id,
                Context.GetOrDefineLiteralNumber(1)
            );
        }
        
        public KVectorStorage<ISymbolicExpressionAtomic> CreateBasisBlade(uint grade, ulong index)
        {
            return Context.CreateKVectorStorageTerm(grade,
                index,
                Context.GetOrDefineLiteralNumber(1)
            );
        }

    }
}