using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Algebra.ComplexAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Composite;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Evaluators;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Numbers;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Variables;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Euclidean.Space3D.Objects;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions;

public static class MetaExpressionUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IMetaExpression Convert<T>(this IMetaExpressionEvaluator<T> evaluator, T expr) where T : class
    {
        return expr.AcceptVisitor(evaluator.IntoSymbolicExpressionConverter);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Convert<T>(this IMetaExpressionEvaluator<T> evaluator, IMetaExpression expr) where T : class
    {
        return expr.AcceptVisitor(evaluator.FromSymbolicExpressionConverter);
    }


    public static bool IsNullOrInvalid(this MetaContext context)
    {
        return
            ReferenceEquals(context, null) ||
            !context.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPlus(this IMetaExpression expression)
    {
        return expression is MetaExpressionFunction funcExpr &&
               funcExpr.FunctionHeadSpecs.FunctionName == MetaExpressionFunctionNames.Plus;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPlusContaining(this IMetaExpression expression, IMetaExpressionVariable rhsVariable)
    {
        return expression is MetaExpressionFunction funcExpr &&
               funcExpr.FunctionHeadSpecs.FunctionName == MetaExpressionFunctionNames.Plus &&
               funcExpr.Arguments
                   .Select(a => a as IMetaExpressionVariable)
                   .Any(a =>
                       a is not null && a.InternalName == rhsVariable.InternalName
                    );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsTimesOfAtomics(this IMetaExpression expression)
    {
        return expression is MetaExpressionFunction funcExpr &&
               funcExpr.FunctionHeadSpecs.FunctionName == MetaExpressionFunctionNames.Times &&
               funcExpr.Arguments.All(a => a is IMetaExpressionAtomic);
    }


    public static void SetAsOutput(this IMetaExpressionAtomic scalar, string externalName)
    {
        if (scalar is MetaExpressionVariableComputed computedVariable)
            computedVariable.SetAsOutput(externalName);

        else
        {
            var variable = (MetaExpressionVariableComputed)scalar.Context.GetOrDefineComputedVariable(scalar);

            variable.SetAsOutput(externalName);
        }
    }

    public static void SetAsOutput(this Scalar<IMetaExpressionAtomic> scalar, string externalName)
    {
        scalar.ScalarValue.SetAsOutput(externalName);
    }

    public static void SetAsOutput(this IScalar<IMetaExpressionAtomic> scalar, string externalName)
    {
        scalar.ScalarValue.SetAsOutput(externalName);
    }

    public static void SetAsOutput(this LinDirectedAngle<IMetaExpressionAtomic> angle, string externalName)
    {
        angle.RadiansValue.SetAsOutput(externalName);
    }

    public static void SetAsOutput(this LinPolarAngle<IMetaExpressionAtomic> angle, string externalNameCos, string externalNameSin)
    {
        angle.CosValue.SetAsOutput(externalNameCos);
        angle.SinValue.SetAsOutput(externalNameSin);
    }

    public static void SetIsOutput(this ComplexNumber<IMetaExpressionAtomic> number, string externalNameReal, string externalNameImaginary)
    {
        number.Real.SetAsOutput(externalNameReal);
        number.Imaginary.SetAsOutput(externalNameImaginary);
    }


    public static void SetAsOutput(this IPair<IMetaExpressionAtomic> vector, string externalNameItem1, string externalNameItem2)
    {
        vector.Item1.SetAsOutput(externalNameItem1);
        vector.Item2.SetAsOutput(externalNameItem2);
    }

    public static void SetAsOutput(this IPair<Scalar<IMetaExpressionAtomic>> vector, string externalNameItem1, string externalNameItem2)
    {
        vector.Item1.SetAsOutput(externalNameItem1);
        vector.Item2.SetAsOutput(externalNameItem2);
    }

    public static void SetAsOutput(this IPair<IMetaExpressionAtomic> vector, IPair<string> externalNames)
    {
        vector.Item1.SetAsOutput(externalNames.Item1);
        vector.Item2.SetAsOutput(externalNames.Item2);
    }

    public static void SetAsOutput(this IPair<Scalar<IMetaExpressionAtomic>> vector, IPair<string> externalNames)
    {
        vector.Item1.SetAsOutput(externalNames.Item1);
        vector.Item2.SetAsOutput(externalNames.Item2);
    }

    public static void SetAsOutput(this IPair<IMetaExpressionAtomic> vector, Func<int, string> externalNameMap)
    {
        vector.Item1.SetAsOutput(externalNameMap(0));
        vector.Item2.SetAsOutput(externalNameMap(1));
    }

    public static void SetAsOutput(this IPair<Scalar<IMetaExpressionAtomic>> vector, Func<int, string> externalNameMap)
    {
        vector.Item1.SetAsOutput(externalNameMap(0));
        vector.Item2.SetAsOutput(externalNameMap(1));
    }

    public static void SetAsOutput(this ITriplet<IMetaExpressionAtomic> vector, string externalNameItem1, string externalNameItem2, string externalNameItem3)
    {
        vector.Item1.SetAsOutput(externalNameItem1);
        vector.Item2.SetAsOutput(externalNameItem2);
        vector.Item3.SetAsOutput(externalNameItem3);
    }

    public static void SetAsOutput(this ITriplet<Scalar<IMetaExpressionAtomic>> vector, string externalNameItem1, string externalNameItem2, string externalNameItem3)
    {
        vector.Item1.SetAsOutput(externalNameItem1);
        vector.Item2.SetAsOutput(externalNameItem2);
        vector.Item3.SetAsOutput(externalNameItem3);
    }

    public static void SetAsOutput(this ITriplet<IMetaExpressionAtomic> vector, ITriplet<string> externalNames)
    {
        vector.Item1.SetAsOutput(externalNames.Item1);
        vector.Item2.SetAsOutput(externalNames.Item2);
        vector.Item3.SetAsOutput(externalNames.Item3);
    }

    public static void SetAsOutput(this ITriplet<Scalar<IMetaExpressionAtomic>> vector, ITriplet<string> externalNames)
    {
        vector.Item1.SetAsOutput(externalNames.Item1);
        vector.Item2.SetAsOutput(externalNames.Item2);
        vector.Item3.SetAsOutput(externalNames.Item3);
    }

    public static void SetAsOutput(this ITriplet<IMetaExpressionAtomic> vector, Func<int, string> externalNameMap)
    {
        vector.Item1.SetAsOutput(externalNameMap(0));
        vector.Item2.SetAsOutput(externalNameMap(1));
        vector.Item3.SetAsOutput(externalNameMap(2));
    }

    public static void SetAsOutput(this ITriplet<Scalar<IMetaExpressionAtomic>> vector, Func<int, string> externalNameMap)
    {
        vector.Item1.SetAsOutput(externalNameMap(0));
        vector.Item2.SetAsOutput(externalNameMap(1));
        vector.Item3.SetAsOutput(externalNameMap(2));
    }

    public static void SetAsOutput(this IQuad<IMetaExpressionAtomic> vector, string externalNameItem1, string externalNameItem2, string externalNameItem3, string externalNameItem4)
    {
        vector.Item1.SetAsOutput(externalNameItem1);
        vector.Item2.SetAsOutput(externalNameItem2);
        vector.Item3.SetAsOutput(externalNameItem3);
        vector.Item4.SetAsOutput(externalNameItem4);
    }

    public static void SetAsOutput(this IQuad<Scalar<IMetaExpressionAtomic>> vector, string externalNameItem1, string externalNameItem2, string externalNameItem3, string externalNameItem4)
    {
        vector.Item1.SetAsOutput(externalNameItem1);
        vector.Item2.SetAsOutput(externalNameItem2);
        vector.Item3.SetAsOutput(externalNameItem3);
        vector.Item4.SetAsOutput(externalNameItem4);
    }

    public static void SetAsOutput(this IQuad<IMetaExpressionAtomic> vector, IQuad<string> externalNames)
    {
        vector.Item1.SetAsOutput(externalNames.Item1);
        vector.Item2.SetAsOutput(externalNames.Item2);
        vector.Item3.SetAsOutput(externalNames.Item3);
        vector.Item4.SetAsOutput(externalNames.Item4);
    }

    public static void SetAsOutput(this IQuad<Scalar<IMetaExpressionAtomic>> vector, IQuad<string> externalNames)
    {
        vector.Item1.SetAsOutput(externalNames.Item1);
        vector.Item2.SetAsOutput(externalNames.Item2);
        vector.Item3.SetAsOutput(externalNames.Item3);
        vector.Item4.SetAsOutput(externalNames.Item4);
    }

    public static void SetAsOutput(this IQuad<IMetaExpressionAtomic> vector, Func<int, string> externalNameMap)
    {
        vector.Item1.SetAsOutput(externalNameMap(0));
        vector.Item2.SetAsOutput(externalNameMap(1));
        vector.Item3.SetAsOutput(externalNameMap(2));
        vector.Item4.SetAsOutput(externalNameMap(3));
    }

    public static void SetAsOutput(this IQuad<Scalar<IMetaExpressionAtomic>> vector, Func<int, string> externalNameMap)
    {
        vector.Item1.SetAsOutput(externalNameMap(0));
        vector.Item2.SetAsOutput(externalNameMap(1));
        vector.Item3.SetAsOutput(externalNameMap(2));
        vector.Item4.SetAsOutput(externalNameMap(3));
    }

    public static void SetAsOutput(this IEnumerable<IMetaExpressionAtomic> scalarsList, Func<int, string> externalNameMap)
    {
        var i = 0;
        foreach (var scalar in scalarsList)
            scalar.SetAsOutput(externalNameMap(i++));
    }

    public static void SetAsOutput(this IEnumerable<Scalar<IMetaExpressionAtomic>> scalarsList, Func<int, string> externalNameMap)
    {
        var i = 0;
        foreach (var scalar in scalarsList)
            scalar.SetAsOutput(externalNameMap(i++));
    }


    public static void SetAsOutputByTermIndex(this XGaKVector<IMetaExpressionAtomic> kVector, Func<ulong, string> namingFunc)
    {
        var idScalarPairs =
            kVector
                .IdScalarPairs
                .Where(s => !s.Value.IsNumber);

        foreach (var (id, scalar) in idScalarPairs)
        {
            var index = id.ToUInt64().BasisBladeIdToIndex();

            scalar.SetAsOutput(namingFunc(index));
        }
    }

    public static void SetAsOutputByTermId(this XGaMultivector<IMetaExpressionAtomic> multivector, Func<ulong, string> namingFunc)
    {
        var idScalarPairs =
            multivector
                .IdScalarPairs
                .Where(s => !s.Value.IsNumber);

        foreach (var (id, scalar) in idScalarPairs)
            scalar.SetAsOutput(namingFunc(id.ToUInt64()));
    }

    public static void SetAsOutputByTermGradeIndex(this XGaMultivector<IMetaExpressionAtomic> multivector, Func<int, ulong, string> namingFunc)
    {
        var indexScalarTuples =
            multivector
                .IdScalarPairs
                .Where(s => !s.Value.IsNumber);

        foreach (var (id, scalar) in indexScalarTuples)
        {
            var (grade, index) = id.ToUInt64().BasisBladeIdToGradeIndex();

            scalar.SetAsOutput(namingFunc((int)grade, index));
        }
    }

    public static void SetAsOutputByTermIndex(this CGaBlade<IMetaExpressionAtomic> kVector, Func<ulong, string> namingFunc)
    {
        kVector.InternalKVector.SetAsOutputByTermIndex(namingFunc);
    }

    //public static void SetAsOutputByTermIndex(this IKVectorStorageContainer<IMetaExpressionAtomic> kVector, Func<ulong, string> namingFunc)
    //{
    //    var indexScalarPairs =
    //        kVector.GetKVectorStorage().GetLinVectorIndexScalarStorage()
    //            .GetIndexScalarRecords()
    //            .Where(s => !s.Scalar.IsNumber);

    //    foreach (var (index, scalar) in indexScalarPairs)
    //        scalar.SetAsOutput(namingFunc(index));
    //}

    //public static void SetAsOutputByTermId(this IMultivectorStorageContainer<IMetaExpressionAtomic> multivector, Func<ulong, string> namingFunc)
    //{
    //    var idScalarPairs =
    //        multivector
    //            .GetMultivectorStorage()
    //            .GetIdScalarRecords()
    //            .Where(s => !s.Scalar.IsNumber);

    //    foreach (var (id, scalar) in idScalarPairs)
    //        scalar.SetAsOutput(namingFunc(id));
    //}

    public static void SetAsOutputByRowColIndex(this IMetaExpressionAtomic[,] array, Func<int, int, string> namingFunc)
    {
        var rowsCount = array.GetLength(0);
        var colsCount = array.GetLength(1);

        for (var j = 0; j < colsCount; j++)
            for (var i = 0; i < rowsCount; i++)
            {
                var scalar = array[i, j];

                if (!scalar.IsNumber)
                    scalar.SetAsOutput(namingFunc(i, j));
            }
    }


    public static void SetExternalName(this Scalar<IMetaExpressionAtomic> kVector, string name)
    {
        var scalar =
            kVector.ScalarValue;

        if (!scalar.IsNumber)
            scalar.UpdateExternalName(name);
    }

    public static void SetExternalName(this IScalar<IMetaExpressionAtomic> kVector, string name)
    {
        var scalar =
            kVector.ScalarValue;

        if (!scalar.IsNumber)
            scalar.UpdateExternalName(name);
    }

    public static void SetExternalName(this XGaScalar<IMetaExpressionAtomic> kVector, string name)
    {
        var scalar =
            kVector.ScalarValue;

        if (!scalar.IsNumber)
            scalar.UpdateExternalName(name);
    }

    public static void SetExternalNamesByTermIndex(this XGaKVector<IMetaExpressionAtomic> kVector, Func<ulong, string> namingFunc)
    {
        var idScalarPairs =
            kVector
                .IdScalarPairs
                .Where(s => !s.Value.IsNumber);

        foreach (var (id, scalar) in idScalarPairs)
        {
            var index = id.ToUInt64().BasisBladeIdToIndex();

            scalar.UpdateExternalName(namingFunc(index));
        }
    }

    //public static void SetExternalNamesByTermIndex(this IKVectorStorageContainer<IMetaExpressionAtomic> kVector, Func<ulong, string> namingFunc)
    //{
    //    var indexScalarPairs =
    //        kVector.GetKVectorStorage().GetLinVectorIndexScalarStorage()
    //            .GetIndexScalarRecords()
    //            .Where(s => !s.Scalar.IsNumber);

    //    foreach (var (index, scalar) in indexScalarPairs)
    //        scalar.UpdateExternalName(namingFunc(index));
    //}

    public static void SetExternalNamesByTermId(this XGaMultivector<IMetaExpressionAtomic> multivector, Func<ulong, string> namingFunc)
    {
        var idScalarPairs =
            multivector
                .IdScalarPairs
                .Where(s => !s.Value.IsNumber);

        foreach (var (id, scalar) in idScalarPairs)
            scalar.UpdateExternalName(namingFunc(id.ToUInt64()));
    }

    //public static void SetExternalNamesByTermId(this IMultivectorStorageContainer<IMetaExpressionAtomic> multivector, Func<ulong, string> namingFunc)
    //{
    //    var idScalarPairs =
    //        multivector
    //            .GetMultivectorStorage()
    //            .GetIdScalarRecords()
    //            .Where(s => !s.Scalar.IsNumber);

    //    foreach (var (id, scalar) in idScalarPairs)
    //        scalar.UpdateExternalName(namingFunc(id));
    //}

    public static void SetExternalNamesByTermGradeIndex(this XGaMultivector<IMetaExpressionAtomic> multivector, Func<int, ulong, string> namingFunc)
    {
        var indexScalarTuples =
            multivector
                .IdScalarPairs
                .Where(s => !s.Value.IsNumber);

        foreach (var (id, scalar) in indexScalarTuples)
        {
            var (grade, index) = id.ToUInt64().BasisBladeIdToGradeIndex();

            scalar.UpdateExternalName(namingFunc((int)grade, index));
        }
    }

    public static void SetExternalNames(this IEnumerable<IMetaExpressionAtomic> namedScalars, Func<int, string> namingFunc)
    {
        var namedScalarsList =
            namedScalars.Where(scalar => !scalar.IsNumber);

        var index = 0;
        foreach (var namedScalar in namedScalarsList)
        {
            namedScalar.UpdateExternalName(namingFunc(index));

            index++;
        }
    }


    public static void SetExternalNamesByTermId(this MetaContext context, XGaMultivector<IMetaExpressionAtomic> multivector, Func<ulong, string> namingFunc)
    {
        var idScalarPairs =
            multivector
                .IdScalarPairs
                .Where(s => !s.Value.IsNumber);

        foreach (var (id, scalar) in idScalarPairs)
            scalar.UpdateExternalName(namingFunc(id.ToUInt64()));
    }

    //public static void SetExternalNamesByTermId(this MetaContext context, IMultivectorStorageContainer<IMetaExpressionAtomic> multivector, Func<ulong, string> namingFunc)
    //{
    //    var idScalarPairs =
    //        multivector
    //            .GetIdScalarRecords()
    //            .Where(s => !s.Scalar.IsNumber);

    //    foreach (var (id, scalar) in idScalarPairs)
    //        scalar.UpdateExternalName(namingFunc(id));
    //}

    public static void SetExternalNamesByTermGradeIndex(this MetaContext context, XGaMultivector<IMetaExpressionAtomic> multivector, Func<uint, ulong, string> namingFunc)
    {
        var indexScalarTuples =
            multivector
                .IdScalarPairs
                .Where(s => !s.Value.IsNumber);

        foreach (var (id, scalar) in indexScalarTuples)
        {
            var (grade, index) = id.ToUInt64().BasisBladeIdToGradeIndex();

            scalar.UpdateExternalName(namingFunc(grade, index));
        }
    }

    public static void SetExternalNamesByTermIndex(this MetaContext context, XGaKVector<IMetaExpressionAtomic> kVector, Func<ulong, string> namingFunc)
    {
        var idScalarPairs =
            kVector
                .IdScalarPairs
                .Where(s => !s.Value.IsNumber);

        foreach (var (id, scalar) in idScalarPairs)
        {
            var index = id.ToUInt64().BasisVectorIndexToId();

            scalar.UpdateExternalName(namingFunc(index));
        }
    }

    //public static void SetExternalNamesByTermIndex(this MetaContext context, IKVectorStorageContainer<ISymbolicExpressionAtomic> kVector, Func<ulong, string> namingFunc)
    //{
    //    var indexScalarPairs = 
    //        kVector.GetKVectorStorage().GetLinVectorIndexScalarStorage()
    //            .GetIndexScalarRecords()
    //            .Where(s => !s.Scalar.IsNumber);

    //    foreach (var (index, scalar) in indexScalarPairs)
    //        scalar.ExternalName = namingFunc(index);
    //}

    public static void SetExternalNames(this MetaContext context, IEnumerable<IMetaExpressionAtomic> namedScalars, Func<int, string> namingFunc)
    {
        var namedScalarsList =
            namedScalars.Where(scalar => !scalar.IsNumber);

        var index = 0;
        foreach (var namedScalar in namedScalarsList)
        {
            namedScalar.UpdateExternalName(namingFunc(index));

            index++;
        }
    }

    public static void SetExternalNamesByRowColIndex(this IMetaExpressionAtomic[,] array, Func<int, int, string> namingFunc)
    {
        var rowsCount = array.GetLength(0);
        var colsCount = array.GetLength(1);

        for (var j = 0; j < colsCount; j++)
            for (var i = 0; i < rowsCount; i++)
            {
                var scalar = array[i, j];

                if (!scalar.IsNumber)
                    scalar.UpdateExternalName(namingFunc(i, j));
            }
    }

    public static void SetExternalNamesByRowColIndex(this MetaContext context, IMetaExpressionAtomic[,] array, Func<int, int, string> namingFunc)
    {
        var rowsCount = array.GetLength(0);
        var colsCount = array.GetLength(1);

        for (var j = 0; j < colsCount; j++)
            for (var i = 0; i < rowsCount; i++)
            {
                var scalar = array[i, j];

                if (!scalar.IsNumber)
                    scalar.UpdateExternalName(namingFunc(i, j));
            }
    }


    public static void SetComputedExternalNamesByOrder(this MetaContext context, Func<int, string> namingFunc)
    {
        var index = 0;
        foreach (var variable in context.GetComputedVariables())
        {
            variable.UpdateExternalName(string.Empty);
            variable.UpdateExternalName(namingFunc(index++));
        }
    }

    public static void SetComputedExternalNamesByOrder(this MetaContext context, int varCountLimit, Func<int, string> namingFunc1, Func<int, string> namingFunc2)
    {
        var computedVariables =
            context.GetComputedVariables().ToImmutableArray();

        var index = 0;
        if (computedVariables.Length <= varCountLimit)
        {
            foreach (var variable in computedVariables)
            {
                variable.UpdateExternalName(string.Empty);
                variable.UpdateExternalName(namingFunc1(index++));
            }
        }
        else
        {
            foreach (var variable in computedVariables)
            {
                variable.UpdateExternalName(string.Empty);
                variable.UpdateExternalName(namingFunc2(index++));
            }
        }
    }

    //public static void SetComputedExternalNamesByNameIndex(this MetaContext context, Func<int, string> namingFunc)
    //{
    //    foreach (var variable in context.GetComputedVariables())
    //        variable.UpdateExternalName(namingFunc(variable.NameIndex));
    //}

    //public static void SetComputedExternalNamesByNameIndex(this MetaContext context, int varCountLimit, Func<int, string> namingFunc1, Func<int, string> namingFunc2)
    //{
    //    context.SetComputedExternalNamesByOrder(
    //        context.GetTargetTempVarsCount() <= varCountLimit
    //            ? namingFunc1
    //            : namingFunc2
    //    );
    //}


    public static void SetExternalNames(this E3DVector<IMetaExpressionAtomic> vector, Func<int, string> namingFunc)
    {
        vector.X.UpdateExternalName(namingFunc(0));
        vector.Y.UpdateExternalName(namingFunc(1));
        vector.Z.UpdateExternalName(namingFunc(2));
    }

    public static void SetExternalNames(this E3DPoint<IMetaExpressionAtomic> point, Func<int, string> namingFunc)
    {
        point.X.UpdateExternalName(namingFunc(0));
        point.Y.UpdateExternalName(namingFunc(1));
        point.Z.UpdateExternalName(namingFunc(2));
    }

    public static void SetExternalNames(this E3DLineSegment<IMetaExpressionAtomic> lineSegment, Func<int, int, string> namingFunc)
    {
        lineSegment.Point1.X.UpdateExternalName(namingFunc(0, 0));
        lineSegment.Point1.Y.UpdateExternalName(namingFunc(0, 1));
        lineSegment.Point1.Z.UpdateExternalName(namingFunc(0, 2));

        lineSegment.Point2.X.UpdateExternalName(namingFunc(1, 0));
        lineSegment.Point2.Y.UpdateExternalName(namingFunc(1, 1));
        lineSegment.Point2.Z.UpdateExternalName(namingFunc(1, 2));
    }

    public static void SetExternalNames(this E3DPlaneSegment<IMetaExpressionAtomic> planeSegment, Func<int, int, string> namingFunc)
    {
        planeSegment.Point1.X.UpdateExternalName(namingFunc(0, 0));
        planeSegment.Point1.Y.UpdateExternalName(namingFunc(0, 1));
        planeSegment.Point1.Z.UpdateExternalName(namingFunc(0, 2));

        planeSegment.Point2.X.UpdateExternalName(namingFunc(1, 0));
        planeSegment.Point2.Y.UpdateExternalName(namingFunc(1, 1));
        planeSegment.Point2.Z.UpdateExternalName(namingFunc(1, 2));

        planeSegment.Point3.X.UpdateExternalName(namingFunc(2, 0));
        planeSegment.Point3.Y.UpdateExternalName(namingFunc(2, 1));
        planeSegment.Point3.Z.UpdateExternalName(namingFunc(2, 2));
    }


    public static IEnumerable<MetaExpressionCompositeBase> CreateCopy(this IEnumerable<MetaExpressionCompositeBase> exprList)
    {
        return exprList?.Select(
            a => a?.CreateCopy()
        );
    }

    public static bool Equals(this IMetaExpression expr1, IMetaExpression expr2)
    {
        if (ReferenceEquals(expr1, null) || ReferenceEquals(expr2, null))
            return false;

        if (ReferenceEquals(expr1, expr2))
            return true;

        if (expr1.GetType() != expr2.GetType())
            return false;

        if (expr1.HeadText != expr2.HeadText)
            return false;

        if (expr1 is IMetaExpressionVariableComputed computedVariableExpr1 && expr2 is IMetaExpressionVariableComputed computedVariableExpr2)
        {
            return Equals(
                computedVariableExpr1.RhsExpression,
                computedVariableExpr2.RhsExpression
            );
        }

        if (expr1 is IMetaExpressionComposite compositeExpr1 && expr2 is IMetaExpressionComposite compositeExpr2)
        {
            if (compositeExpr1.Count == 0 && compositeExpr2.Count == 0)
                return true;

            if (compositeExpr1.Count != compositeExpr2.Count)
                return false;

            var count = compositeExpr1.Count;
            for (var i = 0; i < count; i++)
                if (!Equals(compositeExpr1[i], compositeExpr2[i]))
                    return false;

            return true;
        }

        return true;
    }

    public static bool Equals(this IMetaExpressionAtomic expr1, IMetaExpressionAtomic expr2)
    {
        if (ReferenceEquals(expr1, null) || ReferenceEquals(expr2, null))
            return false;

        if (ReferenceEquals(expr1, expr2))
            return true;

        if (expr1.GetType() != expr2.GetType())
            return false;

        if (expr1.HeadText != expr2.HeadText)
            return false;

        if (expr1 is IMetaExpressionVariableComputed computedVariableExpr1 && expr2 is IMetaExpressionVariableComputed computedVariableExpr2)
        {
            return Equals(
                computedVariableExpr1.RhsExpression,
                computedVariableExpr2.RhsExpression
            );
        }

        return true;
    }

    public static bool Equals(this IMetaExpressionComposite compositeExpr1, IMetaExpressionComposite compositeExpr2)
    {
        if (ReferenceEquals(compositeExpr1, null) || ReferenceEquals(compositeExpr2, null))
            return false;

        if (ReferenceEquals(compositeExpr1, compositeExpr2))
            return true;

        if (compositeExpr1.GetType() != compositeExpr2.GetType())
            return false;

        if (compositeExpr1.HeadText != compositeExpr2.HeadText)
            return false;

        if (compositeExpr1.Count == 0 && compositeExpr2.Count == 0)
            return true;

        if (compositeExpr1.Count != compositeExpr2.Count)
            return false;

        var count = compositeExpr1.Count;
        for (var i = 0; i < count; i++)
            if (!Equals(compositeExpr1[i], compositeExpr2[i]))
                return false;

        return true;
    }

    public static bool Equals(this IMetaExpressionNumber expr1, IMetaExpressionNumber expr2)
    {
        if (ReferenceEquals(expr1, null) || ReferenceEquals(expr2, null))
            return false;

        if (ReferenceEquals(expr1, expr2))
            return true;

        if (expr1.GetType() != expr2.GetType())
            return false;

        if (expr1.NumberHeadSpecs.NumberText != expr2.NumberHeadSpecs.NumberText)
            return false;

        return true;
    }


    public static Tuple<bool, IReadOnlyCollection<IMetaExpression>> ReplaceAllExpressionByExpression(this IReadOnlyCollection<IMetaExpression> expressionsList, IMetaExpression oldExpr, IMetaExpression newExpr)
    {
        var replacedFlag = false;

        var newExpressionsArray =
            new IMetaExpression[expressionsList.Count];

        var i = 0;
        foreach (var expr1 in expressionsList)
        {
            var (flag, expr2) =
                expr1.ReplaceAllExpressionByExpression(oldExpr, newExpr);

            replacedFlag |= flag;
            newExpressionsArray[i] = expr2;
            i++;
        }

        return new Tuple<bool, IReadOnlyCollection<IMetaExpression>>(
            replacedFlag,
            newExpressionsArray
        );
    }

    public static Tuple<bool, IReadOnlyCollection<IMetaExpression>> ReplaceAllExpressionByVariable(this IReadOnlyCollection<IMetaExpression> expressionsList, IMetaExpression oldExpr, string newVarName, MetaContext context)
    {
        return expressionsList.ReplaceAllExpressionByExpression(
            oldExpr,
            context.GetVariable(newVarName)
        );
    }
}