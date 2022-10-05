using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib;
using GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space3D.Objects;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Evaluators;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Composite;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Numbers;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Variables;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
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

        public static void SetIsOutput(this IMetaExpressionAtomic scalar, bool isOutput)
        {
            if (scalar is MetaExpressionVariableComputed computedVariable)
                computedVariable.IsOutputVariable = isOutput;
        }

        public static void SetIsOutput(this IEnumerable<IMetaExpressionAtomic> scalarsList, bool isOutput)
        {
            foreach (var scalar in scalarsList)
                if (scalar is MetaExpressionVariableComputed computedVariable)
                    computedVariable.IsOutputVariable = isOutput;
        }
        
        public static void SetIsOutput(this IMultivectorStorage<IMetaExpressionAtomic> multivector, bool isOutput)
        {
            var namedScalarsList = 
                multivector
                    .GetScalars()
                    .Where(s => s.IsComputedVariable)
                    .Select(s => (MetaExpressionVariableComputed) s);

            foreach (var namedScalar in namedScalarsList)
                namedScalar.IsOutputVariable = isOutput;
        }

        public static void SetIsOutput(this IMultivectorStorageContainer<IMetaExpressionAtomic> multivector, bool isOutput)
        {
            var namedScalarsList = 
                multivector
                    .GetMultivectorStorage()
                    .GetScalars()
                    .Where(s => s.IsComputedVariable)
                    .Select(s => (MetaExpressionVariableComputed) s);

            foreach (var namedScalar in namedScalarsList)
                namedScalar.IsOutputVariable = isOutput;
        }

        public static void SetIsOutput(this IEnumerable<IMultivectorStorage<IMetaExpressionAtomic>> multivectorsList, bool isOutput)
        {
            foreach (var mv in multivectorsList)
                mv.SetIsOutput(isOutput);
        }
        
        public static void SetIsOutput(this IEnumerable<IMultivectorStorageContainer<IMetaExpressionAtomic>> multivectorsList, bool isOutput)
        {
            foreach (var mv in multivectorsList)
                mv.SetIsOutput(isOutput);
        }


        public static void SetExternalNamesByTermId(this IMultivectorStorage<IMetaExpressionAtomic> multivector, Func<ulong, string> namingFunc)
        {
            var idScalarPairs = 
                multivector
                    .GetIdScalarRecords()
                    .Where(s => !s.Scalar.IsNumber);

            foreach (var (id, scalar) in idScalarPairs)
                scalar.ExternalName = namingFunc(id);
        }
        
        public static void SetExternalNamesByTermId(this IMultivectorStorageContainer<IMetaExpressionAtomic> multivector, Func<ulong, string> namingFunc)
        {
            var idScalarPairs = 
                multivector
                    .GetMultivectorStorage()
                    .GetIdScalarRecords()
                    .Where(s => !s.Scalar.IsNumber);

            foreach (var (id, scalar) in idScalarPairs)
                scalar.ExternalName = namingFunc(id);
        }

        public static void SetExternalNamesByTermGradeIndex(this IMultivectorStorage<IMetaExpressionAtomic> multivector, Func<uint, ulong, string> namingFunc)
        {
            var indexScalarTuples = 
                multivector
                    .GetGradeIndexScalarRecords()
                    .Where(s => !s.Scalar.IsNumber);

            foreach (var (grade, index, scalar) in indexScalarTuples) 
                scalar.ExternalName = namingFunc(grade, index);
        }
        
        public static void SetExternalNamesByTermIndex(this KVectorStorage<IMetaExpressionAtomic> kVector, Func<ulong, string> namingFunc)
        {
            var indexScalarPairs = 
                kVector.GetLinVectorIndexScalarStorage()
                    .GetIndexScalarRecords()
                    .Where(s => !s.Scalar.IsNumber);

            foreach (var (index, scalar) in indexScalarPairs)
                scalar.ExternalName = namingFunc(index);
        }
        
        public static void SetExternalNamesByTermIndex(this IKVectorStorageContainer<IMetaExpressionAtomic> kVector, Func<ulong, string> namingFunc)
        {
            var indexScalarPairs = 
                kVector.GetKVectorStorage().GetLinVectorIndexScalarStorage()
                    .GetIndexScalarRecords()
                    .Where(s => !s.Scalar.IsNumber);

            foreach (var (index, scalar) in indexScalarPairs)
                scalar.ExternalName = namingFunc(index);
        }

        public static void SetExternalNamesByOrder(this IEnumerable<IMetaExpressionAtomic> namedScalars, Func<int, string> namingFunc)
        {
            var namedScalarsList = 
                namedScalars.Where(scalar => !scalar.IsNumber);

            var index = 0;
            foreach (var namedScalar in namedScalarsList)
            {
                namedScalar.ExternalName = namingFunc(index);

                index++;
            }
        }

        
        public static void SetExternalNamesByTermId(this MetaContext context, IMultivectorStorage<IMetaExpressionAtomic> multivector, Func<ulong, string> namingFunc)
        {
            var idScalarPairs = 
                multivector
                    .GetIdScalarRecords()
                    .Where(s => !s.Scalar.IsNumber);

            foreach (var (id, scalar) in idScalarPairs)
                scalar.ExternalName = namingFunc(id);
        }
        
        public static void SetExternalNamesByTermId(this MetaContext context, IMultivectorStorageContainer<IMetaExpressionAtomic> multivector, Func<ulong, string> namingFunc)
        {
            var idScalarPairs = 
                multivector
                    .GetIdScalarRecords()
                    .Where(s => !s.Scalar.IsNumber);

            foreach (var (id, scalar) in idScalarPairs)
                scalar.ExternalName = namingFunc(id);
        }

        public static void SetExternalNamesByTermGradeIndex(this MetaContext context, IMultivectorStorage<IMetaExpressionAtomic> multivector, Func<uint, ulong, string> namingFunc)
        {
            var indexScalarTuples = 
                multivector
                    .GetGradeIndexScalarRecords()
                    .Where(s => !s.Scalar.IsNumber);

            foreach (var (grade, index, scalar) in indexScalarTuples) 
                scalar.ExternalName = namingFunc(grade, index);
        }

        public static void SetExternalNamesByTermIndex(this MetaContext context, KVectorStorage<IMetaExpressionAtomic> kVector, Func<ulong, string> namingFunc)
        {
            var indexScalarPairs =
                kVector.GetLinVectorIndexScalarStorage()
                    .GetIndexScalarRecords()
                    .Where(s => !s.Scalar.IsNumber);

            foreach (var (index, scalar) in indexScalarPairs)
                scalar.ExternalName = namingFunc(index);
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

        public static void SetExternalNamesByOrder(this MetaContext context, IEnumerable<IMetaExpressionAtomic> namedScalars, Func<int, string> namingFunc)
        {
            var namedScalarsList = 
                namedScalars.Where(scalar => !scalar.IsNumber);

            var index = 0;
            foreach (var namedScalar in namedScalarsList)
            {
                namedScalar.ExternalName = namingFunc(index);

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
                    scalar.ExternalName = namingFunc(i, j);
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
                    scalar.ExternalName = namingFunc(i, j);
            }
        }

        public static void SetIntermediateExternalNamesByOrder(this MetaContext context, Func<int, string> namingFunc)
        {
            var index = 0;
            foreach (var variable in context.GetIntermediateVariables())
                variable.ExternalName = namingFunc(index++);
        }

        public static void SetIntermediateExternalNamesByNameIndex(this MetaContext context, Func<int, string> namingFunc)
        {
            foreach (var variable in context.GetIntermediateVariables())
                variable.ExternalName = namingFunc(variable.NameIndex);
        }
        
        public static void SetIntermediateExternalNamesByNameIndex(this MetaContext context, int varCountLimit, Func<int, string> namingFunc1, Func<int, string> namingFunc2)
        {
            context.SetIntermediateExternalNamesByNameIndex(
                context.GetTargetTempVarsCount() <= varCountLimit
                ? namingFunc1
                : namingFunc2
            );
        }

        
        public static void SetExternalNames(this E3DVector<IMetaExpressionAtomic> vector, Func<int, string> namingFunc)
        {
            vector.X.ExternalName = namingFunc(0);
            vector.Y.ExternalName = namingFunc(1);
            vector.Z.ExternalName = namingFunc(2);
        }

        public static void SetExternalNames(this E3DPoint<IMetaExpressionAtomic> point, Func<int, string> namingFunc)
        {
            point.X.ExternalName = namingFunc(0);
            point.Y.ExternalName = namingFunc(1);
            point.Z.ExternalName = namingFunc(2);
        }
        
        public static void SetExternalNames(this E3DLineSegment<IMetaExpressionAtomic> lineSegment, Func<int, int, string> namingFunc)
        {
            lineSegment.Point1.X.ExternalName = namingFunc(0, 0);
            lineSegment.Point1.Y.ExternalName = namingFunc(0, 1);
            lineSegment.Point1.Z.ExternalName = namingFunc(0, 2);

            lineSegment.Point2.X.ExternalName = namingFunc(1, 0);
            lineSegment.Point2.Y.ExternalName = namingFunc(1, 1);
            lineSegment.Point2.Z.ExternalName = namingFunc(1, 2);
        }
        
        public static void SetExternalNames(this E3DPlaneSegment<IMetaExpressionAtomic> planeSegment, Func<int, int, string> namingFunc)
        {
            planeSegment.Point1.X.ExternalName = namingFunc(0, 0);
            planeSegment.Point1.Y.ExternalName = namingFunc(0, 1);
            planeSegment.Point1.Z.ExternalName = namingFunc(0, 2);

            planeSegment.Point2.X.ExternalName = namingFunc(1, 0);
            planeSegment.Point2.Y.ExternalName = namingFunc(1, 1);
            planeSegment.Point2.Z.ExternalName = namingFunc(1, 2);

            planeSegment.Point3.X.ExternalName = namingFunc(2, 0);
            planeSegment.Point3.Y.ExternalName = namingFunc(2, 1);
            planeSegment.Point3.Z.ExternalName = namingFunc(2, 2);
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

        public static Tuple<bool, IReadOnlyCollection<IMetaExpression>> ReplaceAllExpressionByVariable(this IReadOnlyCollection<IMetaExpression> expressionsList, IMetaExpression oldExpr, string newVarName)
        {
            return ReplaceAllExpressionByExpression(
                expressionsList,
                oldExpr,
                oldExpr.Context.GetVariable(newVarName)
            );
        }
    }
}
