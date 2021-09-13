using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.Variables;
using GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Context;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public sealed class SymbolicParameterVariableFactory :
        SymbolicAtomicExpressionFactoryBase
    {
        internal SymbolicParameterVariableFactory(SymbolicContext context) 
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

        public KVectorStorage<ISymbolicExpressionAtomic> CreateScalarTerm(string scalarName)
        {
            var namedScalar = 
                Context.GetOrDefineParameterVariable(scalarName);

            return Context.CreateKVectorTermStorage(0,0,namedScalar);
        }

        public ISymbolicExpressionAtomic[,] CreateDenseArray(int rowsCount, int colsCount, Func<int, int, string> namingFunction)
        {
            var array = new ISymbolicExpressionAtomic[rowsCount, colsCount];

            for (var j = 0; j < colsCount; j++)
            for (var i = 0; i < rowsCount; i++)
                array[i, j] = Context.GetOrDefineParameterVariable(namingFunction(i, j));

            return array;
        }

        public VectorStorage<ISymbolicExpressionAtomic> CreateVector(params string[] scalarNames)
        {
            return Context.CreateVectorStorage(scalarNames
                    .Select(Context.GetOrDefineParameterVariable)
                    .Cast<ISymbolicExpressionAtomic>()
                    .ToArray()
            );
        }

        public VectorStorage<ISymbolicExpressionAtomic> CreateVectorTerm(ulong index, string scalarName)
        {
            var namedScalar = 
                Context.GetOrDefineParameterVariable(scalarName);

            return Context.CreateVectorTermStorage(index, namedScalar);
        }
        
        public VectorStorage<ISymbolicExpressionAtomic> CreateDenseVector(uint vSpaceDimension, Func<ulong, string> namingFunction)
        {
            var parametersList =
                vSpaceDimension
                    .GetRange()
                    .Select(index => 
                        new KeyValuePair<ulong, ISymbolicExpressionAtomic>(
                            index, 
                            Context.GetOrDefineParameterVariable(
                                namingFunction(index)
                            )
                        )
                    )
                    .ToDictionary(
                        pair => pair.Key,
                        pair => pair.Value
                    );

            return Context.CreateVectorStorage(parametersList);
        }

        public KVectorStorage<ISymbolicExpressionAtomic> CreateDenseKVector(uint vSpaceDimensions, uint grade, Func<ulong, string> namingFunction)
        {
            Debug.Assert(grade <= vSpaceDimensions);
            
            var kvSpaceDimension = 
                vSpaceDimensions.KVectorSpaceDimension(grade);

            var parametersList =
                Enumerable
                    .Range(0, (int) kvSpaceDimension)
                    .Select(index => 
                        new KeyValuePair<ulong, ISymbolicExpressionAtomic>(
                            (ulong) index, 
                            Context.GetOrDefineParameterVariable(
                                namingFunction((ulong) index)
                            )
                        )
                    )
                    .ToDictionary(
                        pair => pair.Key,
                        pair => pair.Value
                    );

            return Context.CreateKVectorStorage(grade, parametersList);
        }

        public MultivectorStorage<ISymbolicExpressionAtomic> CreateDenseMultivector(uint vSpaceDimensions, Func<ulong, string> namingFunction)
        {
            var parametersList =
                (1UL << (int) vSpaceDimensions)
                    .GetRange()
                    .Select(id => 
                        new KeyValuePair<ulong, ISymbolicExpressionAtomic>(
                            id, 
                            Context.GetOrDefineParameterVariable(
                                namingFunction(id)
                            )
                        )
                    )
                    .ToDictionary(
                        pair => pair.Key,
                        pair => pair.Value
                    );

            return MultivectorStorage<ISymbolicExpressionAtomic>.Create(parametersList);
        }

        //TODO: Add more functions for constructing multivectors
    }
}