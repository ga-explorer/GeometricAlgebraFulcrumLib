using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Numbers;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Factories;

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

        
        public GaStorageVector<ISymbolicExpressionAtomic> CreateBasisVector(ulong index)
        {
            return Context.CreateStorageVector(index,
                Context.GetOrDefineLiteralNumber(1)
            );
        }
        
        public IGaStorageKVector<ISymbolicExpressionAtomic> CreateBasisBlade(ulong id)
        {
            return Context.CreateStorageKVector(id,
                Context.GetOrDefineLiteralNumber(1)
            );
        }
        
        public IGaStorageKVector<ISymbolicExpressionAtomic> CreateBasisBlade(uint grade, ulong index)
        {
            return Context.CreateStorageKVector(grade,
                index,
                Context.GetOrDefineLiteralNumber(1)
            );
        }

    }
}