using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space3D.Objects;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Variables;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Expressions
{
    public sealed class MetaExpressionParameterVariableFactory :
        MetaExpressionAtomicFactoryBase
    {
        internal MetaExpressionParameterVariableFactory(MetaContext context)
            : base(context)
        {
        }


        public IEnumerable<IMetaExpressionVariableParameter> GetOrDefine(IEnumerable<string> internalNamesList)
        {
            return internalNamesList.Select(Context.GetOrDefineParameterVariable);
        }

        public IMetaExpressionVariableParameter GetOrDefine(string internalName)
        {
            return Context.GetOrDefineParameterVariable(internalName);
        }

        public IEnumerable<IMetaExpressionVariableParameter> GetOrDefine(int startIndex, int count, Func<int, string> namingFunction)
        {
            for (var i = 0; i < count; i++)
            {
                var internalName =
                    namingFunction(startIndex + i);

                yield return Context.GetOrDefineParameterVariable(internalName);
            }
        }

        public GaKVector<IMetaExpressionAtomic> CreateScalarTerm(string scalarName)
        {
            var namedScalar =
                Context.GetOrDefineParameterVariable(scalarName);

            return Context
                .CreateKVectorStorageTerm(0, 0, namedScalar)
                .CreateKVector(Context.GeometricProcessor);
        }

        public IMetaExpressionAtomic[,] CreateDenseArray(int rowsCount, int colsCount, Func<int, int, string> namingFunction)
        {
            var array = new IMetaExpressionAtomic[rowsCount, colsCount];

            for (var j = 0; j < colsCount; j++)
                for (var i = 0; i < rowsCount; i++)
                    array[i, j] = Context.GetOrDefineParameterVariable(namingFunction(i, j));

            return array;
        }

        public GaVector<IMetaExpressionAtomic> CreateVector(params string[] scalarNames)
        {
            return Context.CreateVectorStorage(scalarNames
                    .Select(Context.GetOrDefineParameterVariable)
                    .Cast<IMetaExpressionAtomic>()
                    .ToArray()
            ).CreateVector(Context.GeometricProcessor);
        }

        public GaVector<IMetaExpressionAtomic> CreateVectorTerm(ulong index, string scalarName)
        {
            var namedScalar =
                Context.GetOrDefineParameterVariable(scalarName);

            return Context
                .CreateVectorStorageTerm(index, namedScalar)
                .CreateVector(Context.GeometricProcessor);
        }

        public GaVector<IMetaExpressionAtomic> CreateDenseVector(uint vSpaceDimension, Func<ulong, string> namingFunction)
        {
            var parametersList =
                vSpaceDimension
                    .GetRange()
                    .Select(index =>
                        new KeyValuePair<ulong, IMetaExpressionAtomic>(
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

            return Context
                .CreateVectorStorage(parametersList)
                .CreateVector(Context.GeometricProcessor);
        }

        public GaKVector<IMetaExpressionAtomic> CreateDenseKVector(uint vSpaceDimensions, uint grade, Func<ulong, string> namingFunction)
        {
            Debug.Assert(grade <= vSpaceDimensions);

            var kvSpaceDimension =
                vSpaceDimensions.KVectorSpaceDimension(grade);

            var parametersList =
                Enumerable
                    .Range(0, (int)kvSpaceDimension)
                    .Select(index =>
                        new KeyValuePair<ulong, IMetaExpressionAtomic>(
                            (ulong)index,
                            Context.GetOrDefineParameterVariable(
                                namingFunction((ulong)index)
                            )
                        )
                    )
                    .ToDictionary(
                        pair => pair.Key,
                        pair => pair.Value
                    );

            return Context
                .CreateKVectorStorage(grade, parametersList)
                .CreateKVector(Context.GeometricProcessor);
        }

        public GaMultivector<IMetaExpressionAtomic> CreateDenseMultivector(uint vSpaceDimensions, Func<ulong, string> namingFunction)
        {
            var parametersList =
                (1UL << (int)vSpaceDimensions)
                    .GetRange()
                    .Select(id =>
                        new KeyValuePair<ulong, IMetaExpressionAtomic>(
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

            return MultivectorStorage<IMetaExpressionAtomic>
                .Create(parametersList)
                .CreateMultivector(Context.GeometricProcessor);
        }

        //TODO: Add more functions for constructing multivectors and other GA-FuL objects

        public E3DVector<IMetaExpressionAtomic> CreateE3DVector(string scalarXName, string scalarYName, string scalarZName, bool assumeUnitVector = false)
        {
            var x = Context.GetOrDefineParameterVariable(scalarXName);
            var y = Context.GetOrDefineParameterVariable(scalarYName);
            var z = Context.GetOrDefineParameterVariable(scalarZName);

            return new E3DVector<IMetaExpressionAtomic>(Context, x, y, z, assumeUnitVector);
        }

        public E3DVector<IMetaExpressionAtomic> CreateE3DVector(Func<int, string> namingFunction, bool assumeUnitVector = false)
        {
            var x = Context.GetOrDefineParameterVariable(namingFunction(0));
            var y = Context.GetOrDefineParameterVariable(namingFunction(1));
            var z = Context.GetOrDefineParameterVariable(namingFunction(2));

            return new E3DVector<IMetaExpressionAtomic>(Context, x, y, z, assumeUnitVector);
        }

        public E3DPoint<IMetaExpressionAtomic> CreateE3DPoint(string scalarXName, string scalarYName, string scalarZName)
        {
            var x = Context.GetOrDefineParameterVariable(scalarXName);
            var y = Context.GetOrDefineParameterVariable(scalarYName);
            var z = Context.GetOrDefineParameterVariable(scalarZName);

            return new E3DPoint<IMetaExpressionAtomic>(Context, x, y, z);
        }

        public E3DPoint<IMetaExpressionAtomic> CreateE3DPoint(Func<int, string> namingFunction)
        {
            var x = Context.GetOrDefineParameterVariable(namingFunction(0));
            var y = Context.GetOrDefineParameterVariable(namingFunction(1));
            var z = Context.GetOrDefineParameterVariable(namingFunction(2));

            return new E3DPoint<IMetaExpressionAtomic>(Context, x, y, z);
        }

        public E3DLineSegment<IMetaExpressionAtomic> CreateE3DLineSegment(Func<int, int, string> namingFunction)
        {
            var point1 = CreateE3DPoint(
                axisIndex => namingFunction(0, axisIndex)
            );

            var point2 = CreateE3DPoint(
                axisIndex => namingFunction(1, axisIndex)
            );

            return new E3DLineSegment<IMetaExpressionAtomic>(point1, point2);
        }

        public E3DPlaneSegment<IMetaExpressionAtomic> CreateE3DPlaneSegment(Func<int, int, string> namingFunction)
        {
            var point1 = CreateE3DPoint(
                axisIndex => namingFunction(0, axisIndex)
            );

            var point2 = CreateE3DPoint(
                axisIndex => namingFunction(1, axisIndex)
            );

            var point3 = CreateE3DPoint(
                axisIndex => namingFunction(2, axisIndex)
            );

            return new E3DPlaneSegment<IMetaExpressionAtomic>(point1, point2, point3);
        }
    }
}