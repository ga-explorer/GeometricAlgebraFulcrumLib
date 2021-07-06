using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraLib.Processing.SymbolicExpressions.Context;
using GeometricAlgebraLib.Processing.SymbolicExpressions.Numbers;
using GeometricAlgebraLib.Storage;

namespace GeometricAlgebraLib.Processing.SymbolicExpressions.Factories
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

        
        public GaVectorTermStorage<ISymbolicExpressionAtomic> CreateBasisVector(ulong index)
        {
            return GaVectorTermStorage<ISymbolicExpressionAtomic>.Create(
                Context,
                index,
                Context.GetOrDefineLiteralNumber(1)
            );
        }
        
        public GaKVectorTermStorage<ISymbolicExpressionAtomic> CreateBasisBlade(ulong id)
        {
            return GaKVectorTermStorage<ISymbolicExpressionAtomic>.Create(
                Context,
                id,
                Context.GetOrDefineLiteralNumber(1)
            );
        }
        
        public GaKVectorTermStorage<ISymbolicExpressionAtomic> CreateBasisBlade(int grade, ulong index)
        {
            return GaKVectorTermStorage<ISymbolicExpressionAtomic>.Create(
                Context,
                grade,
                index,
                Context.GetOrDefineLiteralNumber(1)
            );
        }

    }
}