using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using AngouriMath;
using CodeComposerLib.SyntaxTree.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.HeadSpecs;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Numbers;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Variables
{
    public abstract class MetaExpressionVariableBase :
        IMetaExpressionVariable
    {
        protected HashSet<IMetaExpressionVariableComputed> DependingVariablesCache { get; }
            = new HashSet<IMetaExpressionVariableComputed>();


        public MetaContext Context 
            => VariableHeadSpecs.Context;

        public IMetaExpressionHeadSpecs HeadSpecs 
            => VariableHeadSpecs;

        public IMetaExpressionHeadSpecsAtomic AtomicHeadSpecs 
            => VariableHeadSpecs;
        
        public MetaExpressionHeadSpecsVariable VariableHeadSpecs { get; }

        public IEnumerable<IMetaExpressionVariableComputed> DependingVariables 
            => DependingVariablesCache;

        public abstract bool HasDependingVariables { get; }

        public IMetaExpressionVariableComputed LastDependingVariable 
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

        public IEnumerable<IMetaExpression> Expressions
        {
            get { yield return this; }
        }
        
        public IEnumerable<IMetaExpression> SubExpressions 
            => Enumerable.Empty<IMetaExpression>();
        
        public IEnumerable<IMetaExpressionAtomic> AtomicExpressions
        {
            get { yield return this; }
        }
        
        public IEnumerable<IMetaExpressionAtomic> AtomicSubExpressions 
            => Enumerable.Empty<IMetaExpressionAtomic>();
        
        public IEnumerable<IMetaExpressionComposite> CompositeExpressions
            => Enumerable.Empty<IMetaExpressionComposite>();
        
        public IEnumerable<IMetaExpressionComposite> CompositeSubExpressions
            => Enumerable.Empty<IMetaExpressionComposite>();
        
        public IEnumerable<IMetaExpressionNumber> NumberExpressions
            => Enumerable.Empty<IMetaExpressionNumber>();
        
        public IEnumerable<IMetaExpressionNumber> NumberSubExpressions
            => Enumerable.Empty<IMetaExpressionNumber>();
        
        public IEnumerable<IMetaExpressionVariable> VariableExpressions
        {
            get { yield return this; }
        }
        
        public IEnumerable<IMetaExpressionVariable> VariableSubExpressions
            => Enumerable.Empty<IMetaExpressionVariable>();
        
        public abstract IEnumerable<IMetaExpressionVariableParameter> VariableParameterExpressions { get; }
        
        public IEnumerable<IMetaExpressionVariableParameter> VariableParameterSubExpressions
            => Enumerable.Empty<IMetaExpressionVariableParameter>();
        
        public abstract IEnumerable<IMetaExpressionVariableComputed> VariableComputedExpressions { get; }
        
        public IEnumerable<IMetaExpressionVariableComputed> VariableComputedSubExpressions
            => Enumerable.Empty<IMetaExpressionVariableComputed>();

        public int AtomicExpressionId { get; }

        public string InternalName 
            => VariableHeadSpecs.VariableName;
        

        private string _externalName = string.Empty;
        public string ExternalName
        {
            get => string.IsNullOrEmpty(_externalName) ? InternalName : _externalName;
            set => _externalName = value ?? string.Empty;
        }
        
        public abstract IMetaExpression RhsExpression { get; }
        
        public abstract string RhsExpressionText { get; }

        public double RhsExpressionValue { get; private set; }
        
        public abstract bool IsParameterVariable { get; }

        public abstract bool IsComputedVariableOrComposite { get; }

        public abstract bool IsIntermediateVariable { get; set; }
        
        public abstract bool IsOutputVariable { get; set; }
        
        public abstract bool IsNumberOrParameter { get; }
        
        public abstract bool IsComputedVariable { get; }

        public abstract int MaxComputationLevel { get; }


        protected MetaExpressionVariableBase([NotNull] MetaExpressionHeadSpecsVariable headSpecs)
        {
            AtomicExpressionId = headSpecs.Context.GetNextAtomicExpressionId();
            VariableHeadSpecs = headSpecs;
            RhsExpressionValue = 0d;
        }


        public IMetaExpression Simplify()
        {
            return this;
        }

        public abstract string GetTextDescription();

        public Tuple<bool, IMetaExpression> ReplaceAllExpressionByExpression(IMetaExpression oldExpr, IMetaExpression newExpr)
        {
            return MetaExpressionUtils.Equals(this, oldExpr) 
                ? new Tuple<bool, IMetaExpression>(true, newExpr) 
                : new Tuple<bool, IMetaExpression>(false, this);
        }

        public Tuple<bool, IMetaExpression> ReplaceAllExpressionByVariable(IMetaExpression oldExpr, string variableName)
        {
            var newExpr = Context.GetVariable(variableName);

            return MetaExpressionUtils.Equals(this, oldExpr) 
                ? new Tuple<bool, IMetaExpression>(true, newExpr) 
                : new Tuple<bool, IMetaExpression>(false, this);
        }

        public Tuple<bool, IMetaExpression> ReplaceAllVariableByVariable(string oldVariableName, string newVariableName)
        {
            var oldExpr = Context.GetVariable(oldVariableName);
            var newExpr = Context.GetVariable(newVariableName);

            return MetaExpressionUtils.Equals(this, oldExpr) 
                ? new Tuple<bool, IMetaExpression>(true, newExpr) 
                : new Tuple<bool, IMetaExpression>(false, this);
        }

        public abstract IMetaExpression GetExpressionCopy();

        public Entity ToAngouriMathEntity()
        {
            return MathS.Var(InternalName);
        }

        public SteExpression ToSimpleTextExpression()
        {
            return SteExpression.CreateVariable(InternalName);
        }

        public abstract IMetaExpression GetScalarValue(bool useRhsScalarValue);


        public void SetRhsExpressionValue([NotNull] double number)
        {
            RhsExpressionValue = number;
        }

        public void AddDependingVariable([NotNull] IMetaExpressionVariableComputed computedVar)
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