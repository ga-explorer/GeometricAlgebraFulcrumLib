using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space3D.Objects;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Variables;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;

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

    public XGaKVector<IMetaExpressionAtomic> CreateScalarTerm(string scalarName)
    {
        var namedScalar =
            Context.GetOrDefineParameterVariable(scalarName);

        return Context
            .XGaProcessor
            .CreateScalar(namedScalar);
    }

    public IMetaExpressionAtomic[,] CreateDenseArray(int rowsCount, int colsCount, Func<int, int, string> namingFunction)
    {
        var array = new IMetaExpressionAtomic[rowsCount, colsCount];

        for (var j = 0; j < colsCount; j++)
        for (var i = 0; i < rowsCount; i++)
            array[i, j] = Context.GetOrDefineParameterVariable(namingFunction(i, j));

        return array;
    }

    //public XGaVector<IMetaExpressionAtomic> CreateVector(string scalar1Name, string scalar2Name, string scalar3Name)
    //{

    //}

    public IMetaExpressionAtomic CreateScalarValue(string scalarName)
    {
        return Context.GetOrDefineParameterVariable(scalarName);
    }
    
    public XGaScalar<IMetaExpressionAtomic> CreateScalar(string scalarName)
    {
        var scalar = 
            Context.GetOrDefineParameterVariable(scalarName);

        return Context.XGaProcessor.CreateScalar(scalar);
    }

    public XGaVector<IMetaExpressionAtomic> CreateVector(params string[] scalarNames)
    {
        return Context
            .XGaProcessor
            .CreateVector(
                scalarNames
                    .Select(Context.GetOrDefineParameterVariable)
                    .Cast<IMetaExpressionAtomic>()
                    .ToArray()
            );
    }

    public XGaVector<IMetaExpressionAtomic> CreateVectorTerm(int index, string scalarName)
    {
        var namedScalar =
            Context.GetOrDefineParameterVariable(scalarName);

        return Context
            .XGaProcessor
            .CreateTermVector(index, namedScalar);
    }

    public XGaVector<IMetaExpressionAtomic> CreateDenseVector(int vSpaceDimensions, Func<int, string> namingFunction)
    {
        var parametersList =
            vSpaceDimensions
                .GetRange()
                .Select(index =>
                    new KeyValuePair<IIndexSet, IMetaExpressionAtomic>(
                        index.IndexToIndexSet(),
                        Context.GetOrDefineParameterVariable(
                            namingFunction(index)
                        )
                    )
                );

        return Context
            .XGaProcessor
            .CreateComposer()
            .SetTerms(parametersList)
            .GetVector();
    }

    public XGaKVector<IMetaExpressionAtomic> CreateDenseKVector(int vSpaceDimensions, int grade, Func<ulong, string> namingFunction)
    {
        Debug.Assert(grade <= vSpaceDimensions);

        var kvSpaceDimensions =
            vSpaceDimensions.KVectorSpaceDimension(grade);

        var parametersList =
            Enumerable
                .Range(0, (int)kvSpaceDimensions)
                .Select(index =>
                    new KeyValuePair<IIndexSet, IMetaExpressionAtomic>(
                        BasisBladeUtils.BasisBladeGradeIndexToId(grade, (ulong)index).BitPatternToUInt64IndexSet(),
                        Context.GetOrDefineParameterVariable(
                            namingFunction((ulong)index)
                        )
                    )
                );

        return Context
            .XGaProcessor
            .CreateComposer()
            .SetTerms(parametersList)
            .GetKVector(grade);
    }

    public XGaMultivector<IMetaExpressionAtomic> CreateDenseMultivector(int vSpaceDimensions, Func<ulong, string> namingFunction)
    {
        var parametersList =
            (1UL << vSpaceDimensions)
            .GetRange()
            .Select(id =>
                new KeyValuePair<IIndexSet, IMetaExpressionAtomic>(
                    id.BitPatternToUInt64IndexSet(),
                    Context.GetOrDefineParameterVariable(
                        namingFunction(id)
                    )
                )
            );

        return Context
            .XGaProcessor
            .CreateComposer()
            .AddTerms(parametersList)
            .GetMultivector();
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