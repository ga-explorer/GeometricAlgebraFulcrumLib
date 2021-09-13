using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.Composite;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.Numbers;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.Variables;
using GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Context;
using GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Evaluators;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class SymbolicExpressionsUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ISymbolicExpression Convert<T>(this ISymbolicExpressionEvaluator<T> evaluator, T expr) where T : class
        {
            return expr.AcceptVisitor(evaluator.IntoSymbolicExpressionConverter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Convert<T>(this ISymbolicExpressionEvaluator<T> evaluator, ISymbolicExpression expr) where T : class
        {
            return expr.AcceptVisitor(evaluator.FromSymbolicExpressionConverter);
        }


        public static bool IsNullOrInvalid(this SymbolicContext context)
        {
            return 
                ReferenceEquals(context, null) || 
                !context.IsValid();
        }

        public static void SetIsOutput(this ISymbolicExpressionAtomic scalar, bool isOutput)
        {
            if (scalar is SymbolicVariableComputed computedVariable)
                computedVariable.IsOutputVariable = isOutput;
        }

        public static void SetIsOutput(this IEnumerable<ISymbolicExpressionAtomic> scalarsList, bool isOutput)
        {
            foreach (var scalar in scalarsList)
                if (scalar is SymbolicVariableComputed computedVariable)
                    computedVariable.IsOutputVariable = isOutput;
        }

        public static void SetIsOutput(this IMultivectorStorage<ISymbolicExpressionAtomic> multivector, bool isOutput)
        {
            var namedScalarsList = 
                multivector
                    .GetScalars()
                    .Where(s => s.IsComputedVariable)
                    .Select(s => (SymbolicVariableComputed) s);

            foreach (var namedScalar in namedScalarsList)
                namedScalar.IsOutputVariable = isOutput;
        }

        public static void SetIsOutput(this IEnumerable<IMultivectorStorage<ISymbolicExpressionAtomic>> multivectorsList, bool isOutput)
        {
            foreach (var mv in multivectorsList)
                mv.SetIsOutput(isOutput);
        }


        public static void SetExternalNamesByTermId(this IMultivectorStorage<ISymbolicExpressionAtomic> multivector, Func<ulong, string> namingFunc)
        {
            var idScalarPairs = 
                multivector
                    .GetIdScalarRecords()
                    .Where(s => !s.Scalar.IsNumber);

            foreach (var (id, scalar) in idScalarPairs)
                scalar.ExternalName = namingFunc(id);
        }

        public static void SetExternalNamesByTermGradeIndex(this IMultivectorStorage<ISymbolicExpressionAtomic> multivector, Func<uint, ulong, string> namingFunc)
        {
            var indexScalarTuples = 
                multivector
                    .GetGradeIndexScalarRecords()
                    .Where(s => !s.Scalar.IsNumber);

            foreach (var (grade, index, scalar) in indexScalarTuples) 
                scalar.ExternalName = namingFunc(grade, index);
        }
        
        public static void SetExternalNamesByTermIndex(this KVectorStorage<ISymbolicExpressionAtomic> kVector, Func<ulong, string> namingFunc)
        {
            var indexScalarPairs = 
                kVector.GetLinVectorIndexScalarStorage()
                    .GetIndexScalarRecords()
                    .Where(s => !s.Scalar.IsNumber);

            foreach (var (index, scalar) in indexScalarPairs)
                scalar.ExternalName = namingFunc(index);
        }
        
        public static void SetExternalNamesByOrder(this IEnumerable<ISymbolicExpressionAtomic> namedScalars, Func<int, string> namingFunc)
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

        
        public static void SetExternalNamesByTermId(this SymbolicContext context, IMultivectorStorage<ISymbolicExpressionAtomic> multivector, Func<ulong, string> namingFunc)
        {
            var idScalarPairs = 
                multivector
                    .GetIdScalarRecords()
                    .Where(s => !s.Scalar.IsNumber);

            foreach (var (id, scalar) in idScalarPairs)
                scalar.ExternalName = namingFunc(id);
        }

        public static void SetExternalNamesByTermGradeIndex(this SymbolicContext context, IMultivectorStorage<ISymbolicExpressionAtomic> multivector, Func<uint, ulong, string> namingFunc)
        {
            var indexScalarTuples = 
                multivector
                    .GetGradeIndexScalarRecords()
                    .Where(s => !s.Scalar.IsNumber);

            foreach (var (grade, index, scalar) in indexScalarTuples) 
                scalar.ExternalName = namingFunc(grade, index);
        }
        
        public static void SetExternalNamesByTermIndex(this SymbolicContext context, KVectorStorage<ISymbolicExpressionAtomic> kVector, Func<ulong, string> namingFunc)
        {
            var indexScalarPairs = 
                kVector.GetLinVectorIndexScalarStorage()
                    .GetIndexScalarRecords()
                    .Where(s => !s.Scalar.IsNumber);

            foreach (var (index, scalar) in indexScalarPairs)
                scalar.ExternalName = namingFunc(index);
        }
        
        public static void SetExternalNamesByOrder(this SymbolicContext context, IEnumerable<ISymbolicExpressionAtomic> namedScalars, Func<int, string> namingFunc)
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

        public static void SetExternalNamesByRowColIndex(this ISymbolicExpressionAtomic[,] array, Func<int, int, string> namingFunc)
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

        public static void SetExternalNamesByRowColIndex(this SymbolicContext context, ISymbolicExpressionAtomic[,] array, Func<int, int, string> namingFunc)
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

        public static void SetIntermediateExternalNamesByOrder(this SymbolicContext context, Func<int, string> namingFunc)
        {
            var index = 0;
            foreach (var variable in context.GetIntermediateVariables())
                variable.ExternalName = namingFunc(index++);
        }

        public static void SetIntermediateExternalNamesByNameIndex(this SymbolicContext context, Func<int, string> namingFunc)
        {
            foreach (var variable in context.GetIntermediateVariables())
                variable.ExternalName = namingFunc(variable.NameIndex);
        }
        
        public static void SetIntermediateExternalNamesByNameIndex(this SymbolicContext context, int varCountLimit, Func<int, string> namingFunc1, Func<int, string> namingFunc2)
        {
            context.SetIntermediateExternalNamesByNameIndex(
                context.GetTargetTempVarsCount() <= varCountLimit
                ? namingFunc1
                : namingFunc2
            );
        }

        
        public static IEnumerable<SymbolicExpressionCompositeBase> CreateCopy(this IEnumerable<SymbolicExpressionCompositeBase> exprList)
        {
            return exprList?.Select(
                a => a?.CreateCopy()
            );
        }

        public static bool Equals(this ISymbolicExpression expr1, ISymbolicExpression expr2)
        {
            if (ReferenceEquals(expr1, null) || ReferenceEquals(expr2, null))
                return false;

            if (ReferenceEquals(expr1, expr2))
                return true;

            if (expr1.GetType() != expr2.GetType())
                return false;

            if (expr1.HeadText != expr2.HeadText)
                return false;

            if (expr1 is ISymbolicVariableComputed computedVariableExpr1 && expr2 is ISymbolicVariableComputed computedVariableExpr2)
            {
                return Equals(
                    computedVariableExpr1.RhsExpression,
                    computedVariableExpr2.RhsExpression
                );
            }

            if (expr1 is ISymbolicExpressionComposite compositeExpr1 && expr2 is ISymbolicExpressionComposite compositeExpr2)
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

        public static bool Equals(this ISymbolicExpressionAtomic expr1, ISymbolicExpressionAtomic expr2)
        {
            if (ReferenceEquals(expr1, null) || ReferenceEquals(expr2, null))
                return false;

            if (ReferenceEquals(expr1, expr2))
                return true;

            if (expr1.GetType() != expr2.GetType())
                return false;

            if (expr1.HeadText != expr2.HeadText)
                return false;

            if (expr1 is ISymbolicVariableComputed computedVariableExpr1 && expr2 is ISymbolicVariableComputed computedVariableExpr2)
            {
                return Equals(
                    computedVariableExpr1.RhsExpression,
                    computedVariableExpr2.RhsExpression
                );
            }

            return true;
        }
        
        public static bool Equals(this ISymbolicExpressionComposite compositeExpr1, ISymbolicExpressionComposite compositeExpr2)
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

        public static bool Equals(this ISymbolicNumber expr1, ISymbolicNumber expr2)
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


        public static Tuple<bool, IReadOnlyCollection<ISymbolicExpression>> ReplaceAllExpressionByExpression(this IReadOnlyCollection<ISymbolicExpression> expressionsList, ISymbolicExpression oldExpr, ISymbolicExpression newExpr)
        {
            var replacedFlag = false;

            var newExpressionsArray = 
                new ISymbolicExpression[expressionsList.Count];

            var i = 0;
            foreach (var expr1 in expressionsList)
            {
                var (flag, expr2) = 
                    expr1.ReplaceAllExpressionByExpression(oldExpr, newExpr);

                replacedFlag |= flag;
                newExpressionsArray[i] = expr2;
                i++;
            }

            return new Tuple<bool, IReadOnlyCollection<ISymbolicExpression>>(
                replacedFlag,
                newExpressionsArray
            );
        }

        public static Tuple<bool, IReadOnlyCollection<ISymbolicExpression>> ReplaceAllExpressionByVariable(this IReadOnlyCollection<ISymbolicExpression> expressionsList, ISymbolicExpression oldExpr, string newVarName)
        {
            return ReplaceAllExpressionByExpression(
                expressionsList,
                oldExpr,
                oldExpr.Context.GetVariable(newVarName)
            );
        }
    }
}
