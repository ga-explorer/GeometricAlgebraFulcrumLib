using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraLib.Algebra.Basis;
using GeometricAlgebraLib.Processing.SymbolicExpressions.Context;
using GeometricAlgebraLib.Processing.SymbolicExpressions.Variables;
using GeometricAlgebraLib.Storage;

namespace GeometricAlgebraLib.Processing.SymbolicExpressions.Factories
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

        public ISymbolicExpressionAtomic[,] CreateDenseArray(int rowsCount, int colsCount, Func<int, int, string> namingFunction)
        {
            var array = new ISymbolicExpressionAtomic[rowsCount, colsCount];

            for (var j = 0; j < colsCount; j++)
            for (var i = 0; i < rowsCount; i++)
                array[i, j] = Context.GetOrDefineParameterVariable(namingFunction(i, j));

            return array;
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
        
        public GaVectorStorage<ISymbolicExpressionAtomic> CreateDenseVector(int vSpaceDimension, Func<ulong, string> namingFunction)
        {
            var parametersList =
                Enumerable
                    .Range(0, vSpaceDimension)
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

            return GaVectorStorage<ISymbolicExpressionAtomic>.Create(
                Context,
                parametersList
            );
        }

        public GaKVectorStorage<ISymbolicExpressionAtomic> CreateDenseKVector(int vSpaceDimensions, int grade, Func<ulong, string> namingFunction)
        {
            Debug.Assert(grade >= 0 && grade <= vSpaceDimensions);
            
            var kvSpaceDimension = 
                GaBasisUtils.KvSpaceDimension(vSpaceDimensions, grade);

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

            return GaKVectorStorage<ISymbolicExpressionAtomic>.Create(
                Context,
                grade,
                parametersList
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
                            Context.GetOrDefineParameterVariable(
                                namingFunction(id)
                            )
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