using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using AngouriMath;
using CodeComposerLib.SyntaxTree.Expressions;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.HeadSpecs;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Numbers;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Variables;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Composite
{
    public abstract class MetaExpressionCompositeBase : 
        IMetaExpressionComposite
    {
        public static bool operator ==(MetaExpressionCompositeBase symbolicExpr1, MetaExpressionCompositeBase symbolicExpr2)
        {
            return MetaExpressionUtils.Equals(symbolicExpr1, symbolicExpr2);
        }

        public static bool operator !=(MetaExpressionCompositeBase symbolicExpr1, MetaExpressionCompositeBase symbolicExpr2)
        {
            return !MetaExpressionUtils.Equals(symbolicExpr1, symbolicExpr2);
        }

        
        protected List<IMetaExpression> ArgumentsList { get; }

        public MetaContext Context 
            => CompositeHeadSpecs.Context;

        public abstract IMetaExpressionHeadSpecs HeadSpecs { get; }
        
        public abstract IMetaExpressionHeadSpecsComposite CompositeHeadSpecs { get; }

        public abstract string HeadText { get; }

        public bool IsNumberSymbolOrVariable 
            => false;

        public bool IsNumberOrParameter 
            => false;

        public bool IsNumber 
            => false;

        public bool IsVariable 
            => false;

        public bool IsLiteralNumber 
            => false;

        public bool IsSymbolicNumber 
            => false;

        public abstract bool IsFunction { get; }

        public abstract bool IsArrayAccess { get; }

        public abstract bool IsOperator { get; }

        public bool IsAtomic 
            => false;

        public bool IsComposite 
            => true;

        public bool IsParameterVariable 
            => false;

        public bool IsComputedVariable 
            => false;

        public bool IsComputedVariableOrComposite 
            => true;

        public bool IsIntermediateVariable 
            => false;

        public bool IsOutputVariable 
            => false;

        public bool IsEmptyComposite 
            => ReferenceEquals(ArgumentsList, null) == false && 
               ArgumentsList.Count == 0;

        public bool IsUnaryComposite 
            => ReferenceEquals(ArgumentsList, null) == false && 
               ArgumentsList.Count == 1;

        public bool IsBinaryComposite 
            => ReferenceEquals(ArgumentsList, null) == false && 
               ArgumentsList.Count == 2;

        public bool IsTernaryComposite 
            => ReferenceEquals(ArgumentsList, null) == false && 
               ArgumentsList.Count == 3;

        public bool HasNoArguments
            => ArgumentsList.Count == 0;

        public bool HasArguments 
            => ArgumentsList.Count > 0;

        public IEnumerable<IMetaExpression> Arguments 
            => ArgumentsList;

        public IEnumerable<IMetaExpressionAtomic> AtomicArguments
            => ArgumentsList
                .Select(expr => expr as IMetaExpressionAtomic)
                .Where(expr => expr is not null);

        public IEnumerable<IMetaExpressionComposite> CompositeArguments
            => ArgumentsList
                .Select(expr => expr as IMetaExpressionComposite)
                .Where(expr => expr is not null);

        public IEnumerable<IMetaExpressionNumber> NumberArguments
            => ArgumentsList
                .Select(expr => expr as IMetaExpressionNumber)
                .Where(expr => expr is not null);

        public IEnumerable<IMetaExpressionVariable> VariableArguments
            => ArgumentsList
                .Select(expr => expr as IMetaExpressionVariable)
                .Where(expr => expr is not null);
        
        public IEnumerable<IMetaExpressionVariableParameter> VariableParameterArguments
            => ArgumentsList
                .Select(expr => expr as IMetaExpressionVariableParameter)
                .Where(expr => expr is not null);
        
        public IEnumerable<IMetaExpressionVariableComputed> VariableComputedArguments
            => ArgumentsList
                .Select(expr => expr as IMetaExpressionVariableComputed)
                .Where(expr => expr is not null);

        public int Count 
            => ArgumentsList.Count;

        public IMetaExpression FirstArgument 
            => ArgumentsList[0];

        public IMetaExpression SecondArgument 
            => ArgumentsList[1];

        public IMetaExpression ThirdArgument 
            => ArgumentsList[2];

        public IMetaExpression LastArgument 
            => ArgumentsList[^1];

        public IMetaExpression this[int i]
        {
            get => ArgumentsList[i];
            set => ArgumentsList[i] = value 
                                      ?? throw new ArgumentNullException(nameof(value));
        }

        public int ComputationsCount
        {
            get
            {
                if (ArgumentsList == null)
                    return 0;

                var count = ArgumentsList.Count - 1;

                var stack = new Stack<IMetaExpressionComposite>(CompositeArguments);

                while (stack.Count > 0)
                {
                    var expr = stack.Pop();
                    var argumentsCount = expr.Count;

                    if (argumentsCount == 0)
                        continue;

                    count += argumentsCount - 1;

                    stack.Push(expr.CompositeArguments);
                }

                return count;
            }
        }

        public IEnumerable<IMetaExpression> Expressions
        {
            get
            {
                yield return this;

                var stack = new Stack<IMetaExpressionComposite>();

                stack.Push(this);

                while (stack.Count > 0)
                {
                    var expr = stack.Pop();

                    foreach (var subExpr in expr.Arguments)
                    {
                        yield return subExpr;

                        if (subExpr is IMetaExpressionComposite compositeExpr)
                            stack.Push(compositeExpr);
                    }
                }
            }
        }

        public IEnumerable<IMetaExpression> SubExpressions
        {
            get
            {
                var stack = new Stack<IMetaExpressionComposite>();

                stack.Push(this);

                while (stack.Count > 0)
                {
                    var expr = stack.Pop();

                    foreach (var subExpr in expr.Arguments)
                    {
                        yield return subExpr;

                        if (subExpr is IMetaExpressionComposite compositeExpr)
                            stack.Push(compositeExpr);
                    }
                }
            }
        }

        public IEnumerable<IMetaExpressionAtomic> AtomicExpressions
            => SubExpressions
                .Where(subExpr => subExpr is IMetaExpressionAtomic)
                .Cast<IMetaExpressionAtomic>();

        public IEnumerable<IMetaExpressionAtomic> AtomicSubExpressions
            => SubExpressions
                .Where(subExpr => subExpr is IMetaExpressionAtomic)
                .Cast<IMetaExpressionAtomic>();

        public IEnumerable<IMetaExpressionComposite> CompositeExpressions
            => Expressions
                .Where(subExpr => subExpr is IMetaExpressionComposite)
                .Cast<IMetaExpressionComposite>();

        public IEnumerable<IMetaExpressionComposite> CompositeSubExpressions
            => SubExpressions
                .Where(subExpr => subExpr is IMetaExpressionComposite)
                .Cast<IMetaExpressionComposite>();
        
        public IEnumerable<IMetaExpressionNumber> NumberExpressions
            => SubExpressions
                .Where(subExpr => subExpr is IMetaExpressionNumber)
                .Cast<IMetaExpressionNumber>();
        
        public IEnumerable<IMetaExpressionNumber> NumberSubExpressions
            => SubExpressions
                .Where(subExpr => subExpr is IMetaExpressionNumber)
                .Cast<IMetaExpressionNumber>();
        
        public IEnumerable<IMetaExpressionVariable> VariableExpressions
            => SubExpressions
                .Where(subExpr => subExpr is IMetaExpressionVariable)
                .Cast<IMetaExpressionVariable>();

        public IEnumerable<IMetaExpressionVariable> VariableSubExpressions
            => SubExpressions
                .Where(subExpr => subExpr is IMetaExpressionVariable)
                .Cast<IMetaExpressionVariable>();

        public IEnumerable<IMetaExpressionVariableParameter> VariableParameterExpressions
            => SubExpressions
                .Where(subExpr => subExpr is IMetaExpressionVariableParameter)
                .Cast<IMetaExpressionVariableParameter>();

        public IEnumerable<IMetaExpressionVariableParameter> VariableParameterSubExpressions
            => SubExpressions
                .Where(subExpr => subExpr is IMetaExpressionVariableParameter)
                .Cast<IMetaExpressionVariableParameter>();

        public IEnumerable<IMetaExpressionVariableComputed> VariableComputedExpressions
            => SubExpressions
                .Where(subExpr => subExpr is IMetaExpressionVariableComputed)
                .Cast<IMetaExpressionVariableComputed>();

        public IEnumerable<IMetaExpressionVariableComputed> VariableComputedSubExpressions
            => SubExpressions
                .Where(subExpr => subExpr is IMetaExpressionVariableComputed)
                .Cast<IMetaExpressionVariableComputed>();


        protected MetaExpressionCompositeBase()
        {
            ArgumentsList = new List<IMetaExpression>();
        }

        protected MetaExpressionCompositeBase(int capacity)
        {
            ArgumentsList = new List<IMetaExpression>(capacity);
        }

        protected MetaExpressionCompositeBase([NotNull] IEnumerable<IMetaExpression> arguments)
        {
            ArgumentsList = new List<IMetaExpression>(arguments);
        }


        public IMetaExpression GetScalarValue(bool useRhsScalarValue)
        {
            return this;
        }

        /// <summary>
        /// Create a copy of this text expression tree
        /// </summary>
        /// <returns></returns>
        public abstract MetaExpressionCompositeBase CreateCopy();

        /// <summary>
        /// Clear all arguments of this expressions
        /// </summary>
        /// <returns></returns>
        public void ClearArguments()
        {
            ArgumentsList.Clear();
        }

        /// <summary>
        /// Remove an argument in this expression
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public void RemoveArgument(int index)
        {
            ArgumentsList.RemoveAt(index);
        }

        /// <summary>
        /// Reset the arguments of this expression without changing the head
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public void ResetArguments(params IMetaExpression[] arguments)
        {
            ArgumentsList.Clear();
            ArgumentsList.AddRange(arguments);
        }

        /// <summary>
        /// Reset the arguments of this expression without changing the head
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public void ResetArguments(IEnumerable<IMetaExpression> arguments)
        {
            ArgumentsList.Clear();
            ArgumentsList.AddRange(arguments);
        }
        
        /// <summary>
        /// Add the given argument to this expression
        /// </summary>
        /// <param name="argument"></param>
        /// <returns>The argument that was added to this expression</returns>
        public void AppendArgument(IMetaExpression argument)
        {
            ArgumentsList.Add(argument);
        }

        /// <summary>
        /// Add several arguments to this expression
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public void AppendArguments(params IMetaExpression[] arguments)
        {
            ArgumentsList.AddRange(arguments);
        }

        /// <summary>
        /// Add several arguments to this expression
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public void AppendArguments(IEnumerable<IMetaExpression> arguments)
        {
            ArgumentsList.AddRange(arguments);
        }

        /// <summary>
        /// Insert the given argument into this expression as the first argument
        /// </summary>
        /// <param name="argument">The argument that was inserted into this expression</param>
        /// <returns></returns>
        public void PrependArgument(IMetaExpression argument)
        {
            ArgumentsList.Insert(0, argument);
        }

        /// <summary>
        /// Insert several arguments into this expression
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public void PrependArguments(params IMetaExpression[] arguments)
        {
            ArgumentsList.InsertRange(0, arguments);
        }

        /// <summary>
        /// Insert several arguments into this expression
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public void PrependArguments(IEnumerable<IMetaExpression> arguments)
        {
            ArgumentsList.InsertRange(0, arguments);
        }

        /// <summary>
        /// Insert the given argument into this expression
        /// </summary>
        /// <param name="index"></param>
        /// <param name="argument">The argument that was inserted into this expression</param>
        /// <returns></returns>
        public void InsertArgument(int index, IMetaExpression argument)
        {
            ArgumentsList.Insert(index, argument);
        }

        /// <summary>
        /// Insert several arguments into this expression
        /// </summary>
        /// <param name="index"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public void InsertArguments(int index, params IMetaExpression[] arguments)
        {
            ArgumentsList.InsertRange(index, arguments);
        }

        /// <summary>
        /// Insert several arguments into this expression
        /// </summary>
        /// <param name="index"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void InsertArguments(int index, IEnumerable<IMetaExpression> arguments)
        {
            ArgumentsList.InsertRange(index, arguments);
        }


        ///// <summary>
        ///// Reset the head and arguments of this expression
        ///// </summary>
        ///// <param name="headSpecs"></param>
        ///// <returns></returns>
        //public void Reset(ISymbolicHeadSpecsAtomic headSpecs)
        //{
        //    HeadSpecs = headSpecs;
        //    ArgumentsList.Clear();
        //}

        ///// <summary>
        ///// Reset the head and arguments of this expression
        ///// </summary>
        ///// <param name="headSpecs"></param>
        ///// <returns></returns>
        //public SymbolicExpressionCompositeBase Reset(ISymbolicHeadSpecsComposite headSpecs)
        //{
        //    HeadSpecs = headSpecs;
        //    ArgumentsList.Clear(); 

        //    return this;
        //}

        ///// <summary>
        ///// Reset the head and arguments of this expression
        ///// </summary>
        ///// <param name="headSpecs"></param>
        ///// <param name="arguments"></param>
        ///// <returns></returns>
        //public SymbolicExpressionCompositeBase Reset(ISymbolicHeadSpecsComposite headSpecs, IEnumerable<SymbolicExpressionCompositeBase> arguments)
        //{
        //    HeadSpecs = headSpecs;
        //    ArgumentsList.Clear(); 
        //    ArgumentsList.AddRange(arguments);

        //    return this;
        //}

        ///// <summary>
        ///// Reset the head and arguments of this expression
        ///// </summary>
        ///// <param name="headSpecs"></param>
        ///// <param name="arguments"></param>
        ///// <returns></returns>
        //public SymbolicExpressionCompositeBase Reset(ISymbolicHeadSpecsComposite headSpecs, params SymbolicExpressionCompositeBase[] arguments)
        //{
        //    HeadSpecs = headSpecs;
        //    ArgumentsList.Clear(); 
        //    ArgumentsList.AddRange(arguments);

        //    return this;
        //}

        ///// <summary>
        ///// Reset the head of this expression without changing the arguments
        ///// </summary>
        ///// <param name="headSpecs"></param>
        ///// <returns></returns>
        //public SymbolicExpressionCompositeBase ResetHead(ISymbolicHeadSpecsComposite headSpecs)
        //{
        //    HeadSpecs = headSpecs;
        //    ArgumentsList.Clear(); 

        //    return this;
        //}

        ///// <summary>
        ///// Reset the head and arguments of this expression as a copy of the given expression
        ///// </summary>
        ///// <param name="expr"></param>
        ///// <returns></returns>
        //public SymbolicExpressionCompositeBase ResetAsCopy(SymbolicExpressionCompositeBase expr)
        //{
        //    HeadSpecs = expr.HeadSpecs;

        //    if (expr.IsAtomic)
        //    {
        //        ArgumentsList = null;

        //        return this;
        //    }

        //    ArgumentsList = new List<SymbolicExpressionCompositeBase>(expr.ArgumentsList.Count);

        //    for (var i = 0; i < expr.ArgumentsList.Count; i++)
        //        ArgumentsList.Add(
        //            expr.ArgumentsList[i].CreateCopy()
        //            );

        //    return this;
        //}


        public abstract MetaExpressionCompositeBase GetEmptyExpressionCopy();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMetaExpression GetExpressionCopy()
        {
            var expr = GetEmptyExpressionCopy();

            expr.AppendArguments(
                ArgumentsList.Select(argExpr => 
                    argExpr.GetExpressionCopy()
                )
            );

            return expr;
        }

        public abstract Entity ToAngouriMathEntity();

        public abstract SteExpression ToSimpleTextExpression();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMetaExpressionComposite GetExpressionCopy(IEnumerable<IMetaExpression> argumentsList)
        {
            var expr = GetEmptyExpressionCopy();

            expr.AppendArguments(argumentsList);

            return expr;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMetaExpression Simplify()
        {
            return Context.SymbolicEvaluator.Simplify(this);
        }

        public Tuple<bool, IMetaExpression> ReplaceAllExpressionByExpression(IMetaExpression oldExpr, IMetaExpression newExpr)
        {
            if (MetaExpressionUtils.Equals(this, oldExpr))
                return new Tuple<bool, IMetaExpression>(true, newExpr);

            var (replacedFlag, argumentsList) =
                ArgumentsList.ReplaceAllExpressionByExpression(oldExpr, newExpr);

            if (!replacedFlag)
                return new Tuple<bool, IMetaExpression>(false, this);

            var compositeExpr = GetExpressionCopy(argumentsList);

            return new Tuple<bool, IMetaExpression>(true, compositeExpr);
        }

        public Tuple<bool, IMetaExpression> ReplaceAllExpressionByVariable(IMetaExpression oldExpr, string variableName)
        {
            var newExpr = Context.GetVariable(variableName);

            return ReplaceAllExpressionByExpression(oldExpr, newExpr);
        }

        public Tuple<bool, IMetaExpression> ReplaceAllVariableByVariable(string oldVariableName, string newVariableName)
        {
            var oldExpr = Context.GetVariable(oldVariableName);
            var newExpr = Context.GetVariable(newVariableName);

            return ReplaceAllExpressionByExpression(oldExpr, newExpr);
        }


        public override int GetHashCode()
        {
            return HashCode.Combine(HeadText, Count);
        }

        public override bool Equals(object obj)
        {
            return MetaExpressionUtils.Equals(
                this, 
                obj as IMetaExpressionComposite
            );
        }

        public IEnumerator<IMetaExpression> GetEnumerator()
        {
            return ArgumentsList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
