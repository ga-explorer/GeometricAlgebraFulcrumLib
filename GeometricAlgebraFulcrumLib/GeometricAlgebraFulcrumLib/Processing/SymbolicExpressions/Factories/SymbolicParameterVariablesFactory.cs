using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Variables;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Factories
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

        public GaStorageScalar<ISymbolicExpressionAtomic> CreateScalarTerm(string scalarName)
        {
            var namedScalar = 
                Context.GetOrDefineParameterVariable(scalarName);

            return Context.CreateStorageScalar(namedScalar);
        }

        public ISymbolicExpressionAtomic[,] CreateDenseArray(int rowsCount, int colsCount, Func<int, int, string> namingFunction)
        {
            var array = new ISymbolicExpressionAtomic[rowsCount, colsCount];

            for (var j = 0; j < colsCount; j++)
            for (var i = 0; i < rowsCount; i++)
                array[i, j] = Context.GetOrDefineParameterVariable(namingFunction(i, j));

            return array;
        }

        public IGaStorageVector<ISymbolicExpressionAtomic> CreateVector(params string[] scalarNames)
        {
            return Context.CreateStorageVector(scalarNames
                    .Select(Context.GetOrDefineParameterVariable)
                    .Cast<ISymbolicExpressionAtomic>()
                    .ToArray()
            );
        }

        public GaStorageVector<ISymbolicExpressionAtomic> CreateVectorTerm(ulong index, string scalarName)
        {
            var namedScalar = 
                Context.GetOrDefineParameterVariable(scalarName);

            return Context.CreateStorageVector(index, namedScalar);
        }
        
        public IGaStorageVector<ISymbolicExpressionAtomic> CreateDenseVector(uint vSpaceDimension, Func<ulong, string> namingFunction)
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

            return Context.CreateStorageVector(parametersList);
        }

        public IGaStorageKVector<ISymbolicExpressionAtomic> CreateDenseKVector(uint vSpaceDimensions, uint grade, Func<ulong, string> namingFunction)
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

            return Context.CreateStorageKVector(grade, parametersList);
        }

        public IGaStorageMultivectorSparse<ISymbolicExpressionAtomic> CreateDenseMultivector(uint vSpaceDimensions, Func<ulong, string> namingFunction)
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

            return GaStorageMultivectorSparse<ISymbolicExpressionAtomic>.Create(parametersList);
        }

        //TODO: Add more functions for constructing multivectors
    }
}