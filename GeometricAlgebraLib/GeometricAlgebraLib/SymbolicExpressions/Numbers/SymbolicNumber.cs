using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CodeComposerLib.SyntaxTree.Expressions;
using GeometricAlgebraLib.SymbolicExpressions.Context;
using GeometricAlgebraLib.SymbolicExpressions.HeadSpecs;
using GeometricAlgebraLib.SymbolicExpressions.Variables;

namespace GeometricAlgebraLib.SymbolicExpressions.Numbers
{
    public sealed class SymbolicNumber :
        ISymbolicNumber
    {
        public static string GetRationalNumberText(int numerator, int denominator)
        {
            return $"Rational[{numerator}, {denominator}]";
        }

        public static SymbolicNumber Create(ISymbolicHeadSpecsNumber headSpecs)
        {
            return new SymbolicNumber(headSpecs);
        }

        public static SymbolicNumber CreateZero(SymbolicContext context)
        {
            return new SymbolicNumber(
                SymbolicHeadSpecsNumberFloat64.Create(context, 0)
            );
        }

        public static SymbolicNumber CreateOne(SymbolicContext context)
        {
            return new SymbolicNumber(
                SymbolicHeadSpecsNumberFloat64.Create(context, 1)
            );
        }

        public static SymbolicNumber CreateMinusOne(SymbolicContext context)
        {
            return new SymbolicNumber(
                SymbolicHeadSpecsNumberFloat64.Create(context, -1)
            );
        }

        public static SymbolicNumber Create(SymbolicContext context, double number)
        {
            return new SymbolicNumber(
                SymbolicHeadSpecsNumberFloat64.Create(context, number)
            );
        }

        public static SymbolicNumber Create(SymbolicContext context, float number)
        {
            return new SymbolicNumber(
                SymbolicHeadSpecsNumberFloat64.Create(context, number)
            );
        }

        public static SymbolicNumber Create(SymbolicContext context, int number)
        {
            return new SymbolicNumber(
                SymbolicHeadSpecsNumberInt32.Create(context, number)
            );
        }

        public static SymbolicNumber Create(SymbolicContext context, long number)
        {
            return new SymbolicNumber(
                SymbolicHeadSpecsNumberFloat64.Create(context, number)
            );
        }

        public static SymbolicNumber CreateRational(SymbolicContext context, int numerator, int denominator)
        {
            return new SymbolicNumber(
                SymbolicHeadSpecsNumberRational.Create(context, numerator, denominator)
            );
        }

        public static SymbolicNumber CreateSymbolic(SymbolicContext context, string numberText, double numberValue)
        {
            return new SymbolicNumber(
                SymbolicHeadSpecsNumberSymbolic.Create(context, numberText, numberValue)
            );
        }


        public HashSet<ISymbolicVariableComputed> DependingVariablesCache { get; }
            = new HashSet<ISymbolicVariableComputed>();

        public SymbolicContext Context 
            => NumberHeadSpecs.Context;

        public ISymbolicHeadSpecs HeadSpecs 
            => NumberHeadSpecs;

        public ISymbolicHeadSpecsAtomic AtomicHeadSpecs 
            => NumberHeadSpecs;

        public ISymbolicHeadSpecsNumber NumberHeadSpecs { get; }

        public string NumberText 
            => NumberHeadSpecs.NumberText;

        public bool IsZero 
            => NumberHeadSpecs.NumberText == "0";

        public bool IsNearZero
        {
            get
            {
                if (NumberHeadSpecs.IsSymbolicNumber)
                    return false;

                if (NumberHeadSpecs.NumberText == "0")
                    return true;

                if (!double.TryParse(NumberHeadSpecs.NumberText, out var number))
                    return false;

                return Context.IsNearZero(number);
            }
        }

        public string HeadText 
            => NumberHeadSpecs.NumberText;

        public bool IsNumberSymbolOrVariable 
            => NumberHeadSpecs.IsSymbolicNumber;

        public bool IsNumber 
            => true;

        public bool IsLiteralNumber 
            => NumberHeadSpecs.IsLiteralNumber;

        public bool IsSymbolicNumber 
            => NumberHeadSpecs.IsSymbolicNumber;

        public bool IsVariable 
            => false;

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

        public int AtomicExpressionId { get; }
        
        public string InternalName 
            => NumberHeadSpecs.NumberText;
        
        public string ExternalName { get; set; }

        public ISymbolicExpression RhsExpression 
            => this;
        
        public string RhsExpressionText 
            => NumberHeadSpecs.NumberText;

        public double RhsExpressionValue 
            => NumberHeadSpecs.NumberValue;
        
        public string RhsExpressionValueText 
            => NumberHeadSpecs.NumberText;

        public bool IsParameterVariable 
            => false;

        public bool IsComputedVariableOrComposite 
            => false;

        public bool IsIntermediateVariable 
            => false;
        
        public bool IsOutputVariable 
            => false;

        public bool IsNumberOrParameter 
            => true;
        
        public bool IsComputedVariable 
            => false;
        
        public bool HasDependingVariables 
            => DependingVariablesCache.Count > 0;

        public IEnumerable<ISymbolicVariableComputed> DependingVariables 
            => DependingVariablesCache;
        
        public ISymbolicVariableComputed LastDependingVariable 
            => DependingVariablesCache
                .OrderByDescending(v => v.ComputationOrder)
                .FirstOrDefault();
        
        public int LastDependingVariableComputationOrder
            => LastDependingVariable?.ComputationOrder ?? -1;
        
        public int MaxComputationLevel
            => 0;
        
        public void AddDependingVariable([NotNull] ISymbolicVariableComputed computedVar)
        {
            DependingVariablesCache.Add(computedVar);
        }

        public void ClearDependencyData()
        {
            DependingVariablesCache.Clear();
        }

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
        {
            get { yield return this; }
        }

        public IEnumerable<ISymbolicNumber> NumberSubExpressions
            => Enumerable.Empty<ISymbolicNumber>();

        public IEnumerable<ISymbolicVariable> VariableExpressions
            => Enumerable.Empty<ISymbolicVariable>();

        public IEnumerable<ISymbolicVariable> VariableSubExpressions
            => Enumerable.Empty<ISymbolicVariable>();

        public IEnumerable<ISymbolicVariableParameter> VariableParameterExpressions
            => Enumerable.Empty<ISymbolicVariableParameter>();

        public IEnumerable<ISymbolicVariableParameter> VariableParameterSubExpressions
            => Enumerable.Empty<ISymbolicVariableParameter>();

        public IEnumerable<ISymbolicVariableComputed> VariableComputedExpressions
            => Enumerable.Empty<ISymbolicVariableComputed>();

        public IEnumerable<ISymbolicVariableComputed> VariableComputedSubExpressions
            => Enumerable.Empty<ISymbolicVariableComputed>();


        private SymbolicNumber([NotNull] ISymbolicHeadSpecsNumber headSpecs)
        {
            AtomicExpressionId = headSpecs.Context.GetNextAtomicExpressionId();

            NumberHeadSpecs = headSpecs;
        }


        public ISymbolicExpression Simplify()
        {
            return this;
        }

        public string GetTextDescription()
        {
            var isUsedText = HasDependingVariables
                ? "    Used"
                : "Not Used";

            return NumberHeadSpecs.IsLiteralNumber
                ? $"{isUsedText} Literal Number  : '{NumberHeadSpecs.NumberText}'"
                : $"{isUsedText} Symbolic Number : '{NumberHeadSpecs.NumberText}'";
        }

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

        public ISymbolicExpression GetExpressionCopy()
        {
            return new SymbolicNumber(NumberHeadSpecs);
        }

        public SteExpression ToSimpleTextExpression()
        {
            return NumberHeadSpecs.IsLiteralNumber
                ? SteExpression.CreateLiteralNumber(NumberText)
                : SteExpression.CreateSymbolicNumber(NumberText);
        }


        public ISymbolicExpression GetScalarValue(bool useRhsScalarValue)
        {
            return this;
        }

        public override string ToString()
        {
            return NumberHeadSpecs.NumberText;
        }
    }
}