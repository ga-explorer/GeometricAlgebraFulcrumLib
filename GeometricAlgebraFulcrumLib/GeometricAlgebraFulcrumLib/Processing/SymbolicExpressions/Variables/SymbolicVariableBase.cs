using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CodeComposerLib.SyntaxTree.Expressions;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.HeadSpecs;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Numbers;

namespace GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Variables
{
    public abstract class SymbolicVariableBase :
        ISymbolicVariable
    {
        protected HashSet<ISymbolicVariableComputed> DependingVariablesCache { get; }
            = new HashSet<ISymbolicVariableComputed>();


        public SymbolicContext Context 
            => VariableHeadSpecs.Context;

        public ISymbolicHeadSpecs HeadSpecs 
            => VariableHeadSpecs;

        public ISymbolicHeadSpecsAtomic AtomicHeadSpecs 
            => VariableHeadSpecs;
        
        public SymbolicHeadSpecsVariable VariableHeadSpecs { get; }

        public IEnumerable<ISymbolicVariableComputed> DependingVariables 
            => DependingVariablesCache;

        public abstract bool HasDependingVariables { get; }

        public ISymbolicVariableComputed LastDependingVariable 
            => DependingVariablesCache
                .OrderByDescending(v => v.ComputationOrder)
                .FirstOrDefault();

        public int LastDependingVariableComputationOrder
            => LastDependingVariable?.ComputationOrder ?? -1;

        public string HeadText 
            => VariableHeadSpecs.HeadText;

        public bool IsNumberSymbolOrVariable 
            => true;

        public bool IsNumber 
            => false;
        
        public bool IsLiteralNumber 
            => false;
        
        public bool IsSymbolicNumber 
            => false;
        
        public bool IsVariable 
            => true;
        
        public bool IsFunction 
            => false;
        
        public bool IsArrayAccess 
            => false;
        
        public bool IsOperator 
            => false;
        
        public bool IsAtomic 
            => true;
        
        public bool IsComposite 
            => false;

        public int ComputationsCount 
            => 0;

        public IEnumerable<ISymbolicExpression> Expressions
        {
            get { yield return this; }
        }
        
        public IEnumerable<ISymbolicExpression> SubExpressions 
            => Enumerable.Empty<ISymbolicExpression>();
        
        public IEnumerable<ISymbolicExpressionAtomic> AtomicExpressions
        {
            get { yield return this; }
        }
        
        public IEnumerable<ISymbolicExpressionAtomic> AtomicSubExpressions 
            => Enumerable.Empty<ISymbolicExpressionAtomic>();
        
        public IEnumerable<ISymbolicExpressionComposite> CompositeExpressions
            => Enumerable.Empty<ISymbolicExpressionComposite>();
        
        public IEnumerable<ISymbolicExpressionComposite> CompositeSubExpressions
            => Enumerable.Empty<ISymbolicExpressionComposite>();
        
        public IEnumerable<ISymbolicNumber> NumberExpressions
            => Enumerable.Empty<ISymbolicNumber>();
        
        public IEnumerable<ISymbolicNumber> NumberSubExpressions
            => Enumerable.Empty<ISymbolicNumber>();
        
        public IEnumerable<ISymbolicVariable> VariableExpressions
        {
            get { yield return this; }
        }
        
        public IEnumerable<ISymbolicVariable> VariableSubExpressions
            => Enumerable.Empty<ISymbolicVariable>();
        
        public abstract IEnumerable<ISymbolicVariableParameter> VariableParameterExpressions { get; }
        
        public IEnumerable<ISymbolicVariableParameter> VariableParameterSubExpressions
            => Enumerable.Empty<ISymbolicVariableParameter>();
        
        public abstract IEnumerable<ISymbolicVariableComputed> VariableComputedExpressions { get; }
        
        public IEnumerable<ISymbolicVariableComputed> VariableComputedSubExpressions
            => Enumerable.Empty<ISymbolicVariableComputed>();

        public int AtomicExpressionId { get; }

        public string InternalName 
            => VariableHeadSpecs.VariableName;
        

        private string _externalName = string.Empty;
        public string ExternalName
        {
            get => string.IsNullOrEmpty(_externalName) ? InternalName : _externalName;
            set => _externalName = value ?? string.Empty;
        }
        
        public abstract ISymbolicExpression RhsExpression { get; }
        
        public abstract string RhsExpressionText { get; }

        public double RhsExpressionValue { get; private set; }
        
        public abstract bool IsParameterVariable { get; }

        public abstract bool IsComputedVariableOrComposite { get; }

        public abstract bool IsIntermediateVariable { get; set; }
        
        public abstract bool IsOutputVariable { get; set; }
        
        public abstract bool IsNumberOrParameter { get; }
        
        public abstract bool IsComputedVariable { get; }

        public abstract int MaxComputationLevel { get; }


        protected SymbolicVariableBase([NotNull] SymbolicHeadSpecsVariable headSpecs)
        {
            AtomicExpressionId = headSpecs.Context.GetNextAtomicExpressionId();
            VariableHeadSpecs = headSpecs;
            RhsExpressionValue = 0d;
        }


        public ISymbolicExpression Simplify()
        {
            return this;
        }

        public abstract string GetTextDescription();

        public Tuple<bool, ISymbolicExpression> ReplaceAllExpressionByExpression(ISymbolicExpression oldExpr, ISymbolicExpression newExpr)
        {
            return SymbolicExpressionsUtils.Equals(this, oldExpr) 
                ? new Tuple<bool, ISymbolicExpression>(true, newExpr) 
                : new Tuple<bool, ISymbolicExpression>(false, this);
        }

        public Tuple<bool, ISymbolicExpression> ReplaceAllExpressionByVariable(ISymbolicExpression oldExpr, string variableName)
        {
            var newExpr = Context.GetVariable(variableName);

            return SymbolicExpressionsUtils.Equals(this, oldExpr) 
                ? new Tuple<bool, ISymbolicExpression>(true, newExpr) 
                : new Tuple<bool, ISymbolicExpression>(false, this);
        }

        public Tuple<bool, ISymbolicExpression> ReplaceAllVariableByVariable(string oldVariableName, string newVariableName)
        {
            var oldExpr = Context.GetVariable(oldVariableName);
            var newExpr = Context.GetVariable(newVariableName);

            return SymbolicExpressionsUtils.Equals(this, oldExpr) 
                ? new Tuple<bool, ISymbolicExpression>(true, newExpr) 
                : new Tuple<bool, ISymbolicExpression>(false, this);
        }

        public abstract ISymbolicExpression GetExpressionCopy();

        public SteExpression ToSimpleTextExpression()
        {
            return SteExpression.CreateVariable(ExternalName);
        }

        public abstract ISymbolicExpression GetScalarValue(bool useRhsScalarValue);


        public void SetRhsExpressionValue([NotNull] double number)
        {
            RhsExpressionValue = number;
        }

        public void AddDependingVariable([NotNull] ISymbolicVariableComputed computedVar)
        {
            DependingVariablesCache.Add(computedVar);
        }

        public abstract void ClearDependencyData();

        public override string ToString()
        {
            return VariableHeadSpecs.VariableName;
        }
    }
}