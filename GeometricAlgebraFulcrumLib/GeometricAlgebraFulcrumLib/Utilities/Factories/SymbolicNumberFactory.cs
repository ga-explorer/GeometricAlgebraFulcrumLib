using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Numbers;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

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

        
        public GaVectorStorage<ISymbolicExpressionAtomic> CreateBasisVector(ulong index)
        {
            return Context.CreateGaVectorStorage(index,
                Context.GetOrDefineLiteralNumber(1)
            );
        }
        
        public IGaKVectorStorage<ISymbolicExpressionAtomic> CreateBasisBlade(ulong id)
        {
            return Context.CreateKVectorStorage(id,
                Context.GetOrDefineLiteralNumber(1)
            );
        }
        
        public IGaKVectorStorage<ISymbolicExpressionAtomic> CreateBasisBlade(uint grade, ulong index)
        {
            return Context.CreateKVectorStorage(grade,
                index,
                Context.GetOrDefineLiteralNumber(1)
            );
        }

    }
}