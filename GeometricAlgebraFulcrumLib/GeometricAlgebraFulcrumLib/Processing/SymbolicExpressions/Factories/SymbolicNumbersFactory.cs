using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Numbers;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Factories
{
    public sealed class SymbolicNumbersFactory :
        SymbolicAtomicExpressionsFactoryBase
    {
        public SymbolicNumbersFactory(SymbolicContext context) 
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

        
        public GasVectorTerm<ISymbolicExpressionAtomic> CreateBasisVector(ulong index)
        {
            return Context.CreateVector(index,
                Context.GetOrDefineLiteralNumber(1)
            );
        }
        
        public IGasKVectorTerm<ISymbolicExpressionAtomic> CreateBasisBlade(ulong id)
        {
            return Context.CreateKVector(id,
                Context.GetOrDefineLiteralNumber(1)
            );
        }
        
        public IGasKVectorTerm<ISymbolicExpressionAtomic> CreateBasisBlade(uint grade, ulong index)
        {
            return Context.CreateKVector(grade,
                index,
                Context.GetOrDefineLiteralNumber(1)
            );
        }

    }
}