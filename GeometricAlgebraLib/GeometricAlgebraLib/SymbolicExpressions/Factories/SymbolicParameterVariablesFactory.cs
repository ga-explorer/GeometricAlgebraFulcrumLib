using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraLib.Storage;
using GeometricAlgebraLib.SymbolicExpressions.Context;
using GeometricAlgebraLib.SymbolicExpressions.Variables;

namespace GeometricAlgebraLib.SymbolicExpressions.Factories
{
    public sealed class SymbolicParameterVariablesFactory :
        SymbolicAtomicExpressionsFactoryBase
    {
        internal SymbolicParameterVariablesFactory(SymbolicContext context) 
            : base(context)
        {
        }

        
        public IEnumerable<ISymbolicVariableParameter> GetOrDefine(IEnumerable<string> internalNamesList)
        {
            return internalNamesList.Select(Context.GetOrDefineParameterVariable);
        }

        public ISymbolicVariableParameter GetOrDefine(string internalName)
        {
            return Context.GetOrDefineParameterVariable(internalName);
        }

        public IEnumerable<ISymbolicVariableParameter> GetOrDefine(int startIndex, int count, Func<int, string> namingFunction)
        {
            for (var i = 0; i < count; i++)
            {
                var internalName = 
                    namingFunction(startIndex + i);

                yield return Context.GetOrDefineParameterVariable(internalName);
            }
        }

        public GaScalarTermStorage<ISymbolicExpressionAtomic> CreateScalarTerm(string scalarName)
        {
            var namedScalar = 
                Context.GetOrDefineParameterVariable(scalarName);

            return GaScalarTermStorage<ISymbolicExpressionAtomic>.Create(
                Context,
                namedScalar
            );
        }

        public GaVectorStorage<ISymbolicExpressionAtomic> CreateVector(params string[] scalarNames)
        {
            return GaVectorStorage<ISymbolicExpressionAtomic>.Create(
                Context,
                scalarNames
                    .Select(Context.GetOrDefineParameterVariable)
                    .Cast<ISymbolicExpressionAtomic>()
                    .ToArray()
            );
        }

        public GaVectorTermStorage<ISymbolicExpressionAtomic> CreateVectorTerm(ulong index, string scalarName)
        {
            var namedScalar = 
                Context.GetOrDefineParameterVariable(scalarName);

            return GaVectorTermStorage<ISymbolicExpressionAtomic>.Create(
                Context,
                index,
                namedScalar
            );
        }

        public GaMultivectorTermsStorage<ISymbolicExpressionAtomic> CreateDenseMultivector(int vSpaceDimensions, Func<int, string> namingFunction)
        {
            var parametersList =
                Enumerable
                    .Range(0, 1 << vSpaceDimensions)
                    .Select(id => 
                        new KeyValuePair<ulong, ISymbolicExpressionAtomic>(
                            (ulong) id, 
                            Context.GetOrDefineParameterVariable(namingFunction(id))
                        )
                    )
                    .ToDictionary(
                        pair => pair.Key,
                        pair => pair.Value
                    );

            return GaMultivectorTermsStorage<ISymbolicExpressionAtomic>.Create(
                Context,
                parametersList
            );
        }

        //TODO: Add more functions for constructing multivectors
    }
}