using System;
using System.Collections.Generic;
using System.Linq;
using AngouriMath;
using CodeComposerLib.SyntaxTree.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.HeadSpecs;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Variables;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Numbers
{
    public sealed class MetaExpressionNumber :
        IMetaExpressionNumber
    {
        public static string GetRationalNumberText(long numerator, long denominator)
        {
            return $"Rational[{numerator}, {denominator}]";
        }

        public static MetaExpressionNumber Create(IMetaExpressionHeadSpecsNumber headSpecs)
        {
            return new MetaExpressionNumber(headSpecs);
        }

        public static MetaExpressionNumber CreateZero(MetaContext context)
        {
            return new MetaExpressionNumber(
                MetaExpressionHeadSpecsNumberFloat64.Create(context, 0)
            );
        }

        public static MetaExpressionNumber CreateOne(MetaContext context)
        {
            return new MetaExpressionNumber(
                MetaExpressionHeadSpecsNumberFloat64.Create(context, 1)
            );
        }

        public static MetaExpressionNumber CreateMinusOne(MetaContext context)
        {
            return new MetaExpressionNumber(
                MetaExpressionHeadSpecsNumberFloat64.Create(context, -1)
            );
        }

        public static MetaExpressionNumber Create(MetaContext context, double number)
        {
            return new MetaExpressionNumber(
                MetaExpressionHeadSpecsNumberFloat64.Create(context, number)
            );
        }

        public static MetaExpressionNumber Create(MetaContext context, float number)
        {
            return new MetaExpressionNumber(
                MetaExpressionHeadSpecsNumberFloat64.Create(context, number)
            );
        }

        public static MetaExpressionNumber Create(MetaContext context, int number)
        {
            return new MetaExpressionNumber(
                MetaExpressionHeadSpecsNumberInt32.Create(context, number)
            );
        }

        public static MetaExpressionNumber Create(MetaContext context, uint number)
        {
            return new MetaExpressionNumber(
                MetaExpressionHeadSpecsNumberUInt32.Create(context, number)
            );
        }

        public static MetaExpressionNumber Create(MetaContext context, long number)
        {
            return new MetaExpressionNumber(
                MetaExpressionHeadSpecsNumberInt64.Create(context, number)
            );
        }

        public static MetaExpressionNumber Create(MetaContext context, ulong number)
        {
            return new MetaExpressionNumber(
                MetaExpressionHeadSpecsNumberUInt64.Create(context, number)
            );
        }

        public static MetaExpressionNumber CreateRational(MetaContext context, long numerator, long denominator)
        {
            return new MetaExpressionNumber(
                MetaExpressionHeadSpecsNumberRational.Create(context, numerator, denominator)
            );
        }

        public static MetaExpressionNumber CreateSymbolic(MetaContext context, string numberText, double numberValue)
        {
            //MetaExpressionHeadSpecsNumberRational.Create(context, numerator, denominator);

            return new MetaExpressionNumber(
                MetaExpressionHeadSpecsNumberSymbolic.Create(context, numberText, numberValue)
            );
        }


        public HashSet<IMetaExpressionVariableComputed> DependingVariablesCache { get; }
            = new HashSet<IMetaExpressionVariableComputed>();

        public MetaContext Context 
            => NumberHeadSpecs.Context;

        public IMetaExpressionHeadSpecs HeadSpecs 
            => NumberHeadSpecs;

        public IMetaExpressionHeadSpecsAtomic AtomicHeadSpecs 
            => NumberHeadSpecs;

        public IMetaExpressionHeadSpecsNumber NumberHeadSpecs { get; }

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
        
        public bool IsFiniteNumber
        {
            get
            {
                if (NumberHeadSpecs.IsSymbolicNumber)
                    return false;

                if (!double.TryParse(NumberHeadSpecs.NumberText, out var number))
                    return false;

                return double.IsFinite(number);
            }
        }

        public bool IsPositive
        {
            get
            {
                if (NumberHeadSpecs.IsSymbolicNumber)
                    return false;

                if (!double.TryParse(NumberHeadSpecs.NumberText, out var number))
                    return false;

                return Context.IsPositive(number);
            }
        }

        public bool IsNegative
        {
            get
            {
                if (NumberHeadSpecs.IsSymbolicNumber)
                    return false;

                if (!double.TryParse(NumberHeadSpecs.NumberText, out var number))
                    return false;

                return Context.IsNegative(number);
            }
        }

        public bool IsNotNearPositive
        {
            get
            {
                if (NumberHeadSpecs.IsSymbolicNumber)
                    return false;

                if (!double.TryParse(NumberHeadSpecs.NumberText, out var number))
                    return false;

                return Context.IsNotNearPositive(number);
            }
        }

        public bool IsNotNearNegative
        {
            get
            {
                if (NumberHeadSpecs.IsSymbolicNumber)
                    return false;

                if (!double.TryParse(NumberHeadSpecs.NumberText, out var number))
                    return false;

                return Context.IsNotNearNegative(number);
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

        public IMetaExpression RhsExpression 
            => this;
        
        public string RhsExpressionText 
            => NumberHeadSpecs.NumberText;

        public double RhsExpressionValue 
            => NumberHeadSpecs.NumberFloat64Value;
        
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

        public IEnumerable<IMetaExpressionVariableComputed> DependingVariables 
            => DependingVariablesCache;
        
        public IMetaExpressionVariableComputed LastDependingVariable 
            => DependingVariablesCache
                .OrderByDescending(v => v.ComputationOrder)
                .FirstOrDefault();
        
        public int LastDependingVariableComputationOrder
            => LastDependingVariable?.ComputationOrder ?? -1;
        
        public int MaxComputationLevel
            => 0;
        
        public void AddDependingVariable(IMetaExpressionVariableComputed computedVar)
        {
            DependingVariablesCache.Add(computedVar);
        }

        public void ClearDependencyData()
        {
            DependingVariablesCache.Clear();
        }

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
        {
            get { yield return this; }
        }

        public IEnumerable<IMetaExpressionNumber> NumberSubExpressions
            => Enumerable.Empty<IMetaExpressionNumber>();

        public IEnumerable<IMetaExpressionVariable> VariableExpressions
            => Enumerable.Empty<IMetaExpressionVariable>();

        public IEnumerable<IMetaExpressionVariable> VariableSubExpressions
            => Enumerable.Empty<IMetaExpressionVariable>();

        public IEnumerable<IMetaExpressionVariableParameter> VariableParameterExpressions
            => Enumerable.Empty<IMetaExpressionVariableParameter>();

        public IEnumerable<IMetaExpressionVariableParameter> VariableParameterSubExpressions
            => Enumerable.Empty<IMetaExpressionVariableParameter>();

        public IEnumerable<IMetaExpressionVariableComputed> VariableComputedExpressions
            => Enumerable.Empty<IMetaExpressionVariableComputed>();

        public IEnumerable<IMetaExpressionVariableComputed> VariableComputedSubExpressions
            => Enumerable.Empty<IMetaExpressionVariableComputed>();


        private MetaExpressionNumber(IMetaExpressionHeadSpecsNumber headSpecs)
        {
            AtomicExpressionId = headSpecs.Context.GetNextAtomicExpressionId();

            NumberHeadSpecs = headSpecs;
        }


        public IMetaExpression Simplify()
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

        public IMetaExpression GetExpressionCopy()
        {
            return new MetaExpressionNumber(NumberHeadSpecs);
        }

        public Entity ToAngouriMathEntity()
        {
            return NumberHeadSpecs switch
            {
                MetaExpressionHeadSpecsNumberFloat64 n => 
                    MathS.Numbers.Create(n.NumberFloat64Value),

                MetaExpressionHeadSpecsNumberInt32 n => 
                    MathS.Numbers.Create(n.NumberInt32Value),

                MetaExpressionHeadSpecsNumberRational n => 
                    MathS.Numbers.CreateRational(n.Numerator, n.Denominator),
                    
                MetaExpressionHeadSpecsNumberSymbolic n => 
                    n.NumberText switch
                    {
                        MetaExpressionNumberNames.Pi => MathS.pi,
                        MetaExpressionNumberNames.E => MathS.e,
                        _ => throw new InvalidOperationException()
                    },

                _ => throw new InvalidOperationException()
            };
        }

        public SteExpression ToSimpleTextExpression()
        {
            return NumberHeadSpecs.IsLiteralNumber
                ? SteExpression.CreateLiteralNumber(NumberText)
                : SteExpression.CreateSymbolicNumber(NumberText);
        }


        public IMetaExpression GetScalarValue(bool useRhsScalarValue)
        {
            return this;
        }

        public override string ToString()
        {
            return NumberHeadSpecs.NumberText;
        }
    }
}