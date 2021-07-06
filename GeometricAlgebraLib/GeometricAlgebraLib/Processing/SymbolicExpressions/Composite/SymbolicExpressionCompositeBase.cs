using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CodeComposerLib.SyntaxTree.Expressions;
using DataStructuresLib.Extensions;
using GeometricAlgebraLib.Processing.SymbolicExpressions.Context;
using GeometricAlgebraLib.Processing.SymbolicExpressions.HeadSpecs;
using GeometricAlgebraLib.Processing.SymbolicExpressions.Numbers;
using GeometricAlgebraLib.Processing.SymbolicExpressions.Variables;

namespace GeometricAlgebraLib.Processing.SymbolicExpressions.Composite
{
    public abstract class SymbolicExpressionCompositeBase : 
        ISymbolicExpressionComposite
    {
        public static bool operator ==(SymbolicExpressionCompositeBase symbolicExpr1, SymbolicExpressionCompositeBase symbolicExpr2)
        {
            return SymbolicExpressionsUtils.Equals(symbolicExpr1, symbolicExpr2);
        }

        public static bool operator !=(SymbolicExpressionCompositeBase symbolicExpr1, SymbolicExpressionCompositeBase symbolicExpr2)
        {
            return !SymbolicExpressionsUtils.Equals(symbolicExpr1, symbolicExpr2);
        }

        
        protected List<ISymbolicExpression> ArgumentsList { get; }

        public SymbolicContext Context 
            => CompositeHeadSpecs.Context;

        public abstract ISymbolicHeadSpecs HeadSpecs { get; }
        
        public abstract ISymbolicHeadSpecsComposite CompositeHeadSpecs { get; }

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

        public IEnumerable<ISymbolicExpression> Arguments 
            => ArgumentsList;

        public IEnumerable<ISymbolicExpressionAtomic> AtomicArguments
            => ArgumentsList
                .Select(expr => expr as ISymbolicExpressionAtomic)
                .Where(expr => expr is not null);

        public IEnumerable<ISymbolicExpressionComposite> CompositeArguments
            => ArgumentsList
                .Select(expr => expr as ISymbolicExpressionComposite)
                .Where(expr => expr is not null);

        public IEnumerable<ISymbolicNumber> NumberArguments
            => ArgumentsList
                .Select(expr => expr as ISymbolicNumber)
                .Where(expr => expr is not null);

        public IEnumerable<ISymbolicVariable> VariableArguments
            => ArgumentsList
                .Select(expr => expr as ISymbolicVariable)
                .Where(expr => expr is not null);
        
        public IEnumerable<ISymbolicVariableParameter> VariableParameterArguments
            => ArgumentsList
                .Select(expr => expr as ISymbolicVariableParameter)
                .Where(expr => expr is not null);
        
        public IEnumerable<ISymbolicVariableComputed> VariableComputedArguments
            => ArgumentsList
                .Select(expr => expr as ISymbolicVariableComputed)
                .Where(expr => expr is not null);

        public int Count 
            => ArgumentsList.Count;

        public ISymbolicExpression FirstArgument 
            => ArgumentsList[0];

        public ISymbolicExpression SecondArgument 
            => ArgumentsList[1];

        public ISymbolicExpression ThirdArgument 
            => ArgumentsList[2];

        public ISymbolicExpression LastArgument 
            => ArgumentsList[^1];

        public ISymbolicExpression this[int i]
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

                var stack = new Stack<ISymbolicExpressionComposite>(CompositeArguments);

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

        public IEnumerable<ISymbolicExpression> Expressions
        {
            get
            {
                yield return this;

                var stack = new Stack<ISymbolicExpressionComposite>();

                stack.Push(this);

                while (stack.Count > 0)
                {
                    var expr = stack.Pop();

                    foreach (var subExpr in expr.Arguments)
                    {
                        yield return subExpr;

                        if (subExpr is ISymbolicExpressionComposite compositeExpr)
                            stack.Push(compositeExpr);
                    }
                }
            }
        }

        public IEnumerable<ISymbolicExpression> SubExpressions
        {
            get
            {
                var stack = new Stack<ISymbolicExpressionComposite>();

                stack.Push(this);

                while (stack.Count > 0)
                {
                    var expr = stack.Pop();

                    foreach (var subExpr in expr.Arguments)
                    {
                        yield return subExpr;

                        if (subExpr is ISymbolicExpressionComposite compositeExpr)
                            stack.Push(compositeExpr);
                    }
                }
            }
        }

        public IEnumerable<ISymbolicExpressionAtomic> AtomicExpressions
            => SubExpressions
                .Where(subExpr => subExpr is ISymbolicExpressionAtomic)
                .Cast<ISymbolicExpressionAtomic>();

        public IEnumerable<ISymbolicExpressionAtomic> AtomicSubExpressions
            => SubExpressions
                .Where(subExpr => subExpr is ISymbolicExpressionAtomic)
                .Cast<ISymbolicExpressionAtomic>();

        public IEnumerable<ISymbolicExpressionComposite> CompositeExpressions
            => Expressions
                .Where(subExpr => subExpr is ISymbolicExpressionComposite)
                .Cast<ISymbolicExpressionComposite>();

        public IEnumerable<ISymbolicExpressionComposite> CompositeSubExpressions
            => SubExpressions
                .Where(subExpr => subExpr is ISymbolicExpressionComposite)
                .Cast<ISymbolicExpressionComposite>();
        
        public IEnumerable<ISymbolicNumber> NumberExpressions
            => SubExpressions
                .Where(subExpr => subExpr is ISymbolicNumber)
                .Cast<ISymbolicNumber>();
        
        public IEnumerable<ISymbolicNumber> NumberSubExpressions
            => SubExpressions
                .Where(subExpr => subExpr is ISymbolicNumber)
                .Cast<ISymbolicNumber>();
        
        public IEnumerable<ISymbolicVariable> VariableExpressions
            => SubExpressions
                .Where(subExpr => subExpr is ISymbolicVariable)
                .Cast<ISymbolicVariable>();

        public IEnumerable<ISymbolicVariable> VariableSubExpressions
            => SubExpressions
                .Where(subExpr => subExpr is ISymbolicVariable)
                .Cast<ISymbolicVariable>();

        public IEnumerable<ISymbolicVariableParameter> VariableParameterExpressions
            => SubExpressions
                .Where(subExpr => subExpr is ISymbolicVariableParameter)
                .Cast<ISymbolicVariableParameter>();

        public IEnumerable<ISymbolicVariableParameter> VariableParameterSubExpressions
            => SubExpressions
                .Where(subExpr => subExpr is ISymbolicVariableParameter)
                .Cast<ISymbolicVariableParameter>();

        public IEnumerable<ISymbolicVariableComputed> VariableComputedExpressions
            => SubExpressions
                .Where(subExpr => subExpr is ISymbolicVariableComputed)
                .Cast<ISymbolicVariableComputed>();

        public IEnumerable<ISymbolicVariableComputed> VariableComputedSubExpressions
            => SubExpressions
                .Where(subExpr => subExpr is ISymbolicVariableComputed)
                .Cast<ISymbolicVariableComputed>();


        protected SymbolicExpressionCompositeBase()
        {
            ArgumentsList = new List<ISymbolicExpression>();
        }

        protected SymbolicExpressionCompositeBase(int capacity)
        {
            ArgumentsList = new List<ISymbolicExpression>(capacity);
        }

        protected SymbolicExpressionCompositeBase([NotNull] IEnumerable<ISymbolicExpression> arguments)
        {
            ArgumentsList = new List<ISymbolicExpression>(arguments);
        }


        public ISymbolicExpression GetScalarValue(bool useRhsScalarValue)
        {
            return this;
        }

        /// <summary>
        /// Create a copy of this text expression tree
        /// </summary>
        /// <returns></returns>
        public abstract SymbolicExpressionCompositeBase CreateCopy();

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
        public void ResetArguments(params ISymbolicExpression[] arguments)
        {
            ArgumentsList.Clear();
            ArgumentsList.AddRange(arguments);
        }

        /// <summary>
        /// Reset the arguments of this expression without changing the head
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public void ResetArguments(IEnumerable<ISymbolicExpression> arguments)
        {
            ArgumentsList.Clear();
            ArgumentsList.AddRange(arguments);
        }
        
        /// <summary>
        /// Add the given argument to this expression
        /// </summary>
        /// <param name="argument"></param>
        /// <returns>The argument that was added to this expression</returns>
        public void AppendArgument(ISymbolicExpression argument)
        {
            ArgumentsList.Add(argument);
        }

        /// <summary>
        /// Add several arguments to this expression
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public void AppendArguments(params ISymbolicExpression[] arguments)
        {
            ArgumentsList.AddRange(arguments);
        }

        /// <summary>
        /// Add several arguments to this expression
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public void AppendArguments(IEnumerable<ISymbolicExpression> arguments)
        {
            ArgumentsList.AddRange(arguments);
        }

        /// <summary>
        /// Insert the given argument into this expression as the first argument
        /// </summary>
        /// <param name="argument">The argument that was inserted into this expression</param>
        /// <returns></returns>
        public void PrependArgument(ISymbolicExpression argument)
        {
            ArgumentsList.Insert(0, argument);
        }

        /// <summary>
        /// Insert several arguments into this expression
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public void PrependArguments(params ISymbolicExpression[] arguments)
        {
            ArgumentsList.InsertRange(0, arguments);
        }

        /// <summary>
        /// Insert several arguments into this expression
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public void PrependArguments(IEnumerable<ISymbolicExpression> arguments)
        {
            ArgumentsList.InsertRange(0, arguments);
        }

        /// <summary>
        /// Insert the given argument into this expression
        /// </summary>
        /// <param name="index"></param>
        /// <param name="argument">The argument that was inserted into this expression</param>
        /// <returns></returns>
        public void InsertArgument(int index, ISymbolicExpression argument)
        {
            ArgumentsList.Insert(index, argument);
        }

        /// <summary>
        /// Insert several arguments into this expression
        /// </summary>
        /// <param name="index"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public void InsertArguments(int index, params ISymbolicExpression[] arguments)
        {
            ArgumentsList.InsertRange(index, arguments);
        }

        /// <summary>
        /// Insert several arguments into this expression
        /// </summary>
        /// <param name="index"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public void InsertArguments(int index, IEnumerable<ISymbolicExpression> arguments)
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


        public abstract SymbolicExpressionCompositeBase GetEmptyExpressionCopy();

        public ISymbolicExpression GetExpressionCopy()
        {
            var expr = GetEmptyExpressionCopy();

            expr.AppendArguments(
                ArgumentsList.Select(argExpr => 
                    argExpr.GetExpressionCopy()
                )
            );

            return expr;
        }

        public abstract SteExpression ToSimpleTextExpression();

        public ISymbolicExpressionComposite GetExpressionCopy(IEnumerable<ISymbolicExpression> argumentsList)
        {
            var expr = GetEmptyExpressionCopy();

            expr.AppendArguments(argumentsList);

            return expr;
        }

        public ISymbolicExpression Simplify()
        {
            return Context.ExpressionSimplifier?.Simplify(this) ?? this;
        }

        public Tuple<bool, ISymbolicExpression> ReplaceAllExpressionByExpression(ISymbolicExpression oldExpr, ISymbolicExpression newExpr)
        {
            if (SymbolicExpressionsUtils.Equals(this, oldExpr))
                return new Tuple<bool, ISymbolicExpression>(true, newExpr);

            var (replacedFlag, argumentsList) =
                ArgumentsList.ReplaceAllExpressionByExpression(oldExpr, newExpr);

            if (!replacedFlag)
                return new Tuple<bool, ISymbolicExpression>(false, this);

            var compositeExpr = GetExpressionCopy(argumentsList);

            return new Tuple<bool, ISymbolicExpression>(true, compositeExpr);
        }

        public Tuple<bool, ISymbolicExpression> ReplaceAllExpressionByVariable(ISymbolicExpression oldExpr, string variableName)
        {
            var newExpr = Context.GetVariable(variableName);

            return ReplaceAllExpressionByExpression(oldExpr, newExpr);
        }

        public Tuple<bool, ISymbolicExpression> ReplaceAllVariableByVariable(string oldVariableName, string newVariableName)
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
            return SymbolicExpressionsUtils.Equals(
                this, 
                obj as ISymbolicExpressionComposite
            );
        }

        public IEnumerator<ISymbolicExpression> GetEnumerator()
        {
            return ArgumentsList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
