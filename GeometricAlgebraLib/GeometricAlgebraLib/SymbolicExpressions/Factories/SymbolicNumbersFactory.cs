using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraLib.SymbolicExpressions.Context;
using GeometricAlgebraLib.SymbolicExpressions.Numbers;

namespace GeometricAlgebraLib.SymbolicExpressions.Factories
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
    }
}