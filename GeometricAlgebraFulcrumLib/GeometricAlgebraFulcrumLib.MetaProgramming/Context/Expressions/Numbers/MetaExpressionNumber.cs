using System.Diagnostics;
using AngouriMath;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Code.SyntaxTree.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Evaluators;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.HeadSpecs;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Variables;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Numbers;

public sealed class MetaExpressionNumber :
    IMetaExpressionNumber
{
    public static string GetRationalNumberText(long numerator, long denominator)
    {
        return $"Rational[{numerator}, {denominator}]";
    }

    public static MetaExpressionNumber Create(MetaContext context, IMetaExpressionHeadSpecsNumber headSpecs)
    {
        return new MetaExpressionNumber(context, headSpecs);
    }

    public static MetaExpressionNumber CreateZero(MetaContext context)
    {
        return new MetaExpressionNumber(
            context,
            MetaExpressionHeadSpecsNumberFloat64.Create(context.ScalarProcessor, 0)
        );
    }

    public static MetaExpressionNumber CreateOne(MetaContext context)
    {
        return new MetaExpressionNumber(
            context,
            MetaExpressionHeadSpecsNumberFloat64.Create(context.ScalarProcessor, 1)
        );
    }

    public static MetaExpressionNumber CreateMinusOne(MetaContext context)
    {
        return new MetaExpressionNumber(
            context,
            MetaExpressionHeadSpecsNumberFloat64.Create(context.ScalarProcessor, -1)
        );
    }

    public static MetaExpressionNumber Create(MetaContext context, double number)
    {
        return new MetaExpressionNumber(
            context,
            MetaExpressionHeadSpecsNumberFloat64.Create(context.ScalarProcessor, number)
        );
    }

    public static MetaExpressionNumber Create(MetaContext context, float number)
    {
        return new MetaExpressionNumber(
            context,
            MetaExpressionHeadSpecsNumberFloat32.Create(context.ScalarProcessor, number)
        );
    }

    public static MetaExpressionNumber Create(MetaContext context, int number)
    {
        return new MetaExpressionNumber(
            context,
            MetaExpressionHeadSpecsNumberInt32.Create(context.ScalarProcessor, number)
        );
    }

    public static MetaExpressionNumber Create(MetaContext context, uint number)
    {
        return new MetaExpressionNumber(
            context,
            MetaExpressionHeadSpecsNumberUInt32.Create(context.ScalarProcessor, number)
        );
    }

    public static MetaExpressionNumber Create(MetaContext context, long number)
    {
        return new MetaExpressionNumber(
            context,
            MetaExpressionHeadSpecsNumberInt64.Create(context.ScalarProcessor, number)
        );
    }

    public static MetaExpressionNumber Create(MetaContext context, ulong number)
    {
        return new MetaExpressionNumber(
            context,
            MetaExpressionHeadSpecsNumberUInt64.Create(context.ScalarProcessor, number)
        );
    }

    public static MetaExpressionNumber CreateRational(MetaContext context, long numerator, long denominator)
    {
        return new MetaExpressionNumber(
            context,
            MetaExpressionHeadSpecsNumberRational.Create(context.ScalarProcessor, numerator, denominator)
        );
    }

    public static MetaExpressionNumber CreateSymbolic(MetaContext context, string numberText, double numberValue)
    {
        return new MetaExpressionNumber(
            context,
            MetaExpressionHeadSpecsNumberSymbolic.Create(context.ScalarProcessor, numberText, numberValue)
        );
    }


    public HashSet<IMetaExpressionVariableComputed> DependingVariablesCache { get; }
        = new HashSet<IMetaExpressionVariableComputed>();

    public MetaContext Context { get; }

    public IMetaExpressionHeadSpecs HeadSpecs
        => NumberHeadSpecs;

    public IMetaExpressionHeadSpecsAtomic AtomicHeadSpecs
        => NumberHeadSpecs;

    public IMetaExpressionHeadSpecsNumber NumberHeadSpecs { get; }

    public IScalarProcessor<IMetaExpressionAtomic> ScalarProcessor
        => NumberHeadSpecs.ScalarProcessor;

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

            return number.IsNearZero(number);
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

            return number > 0;
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

            return number < 0;
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

            return number.ScalarFromNumber(ScalarProcessor).IsNotNearZeroOrPositive();
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

            return number.ScalarFromNumber(ScalarProcessor).IsNotNearZeroOrNegative();
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

    private string _externalName = string.Empty;
    public string ExternalName
        => string.IsNullOrEmpty(_externalName)
            ? InternalName
            : _externalName;

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

    public bool MergeEnabled 
        => false;

    public bool IsOutputOrHasDependingVariables
        => DependingVariablesCache.Count > 0;

    public IEnumerable<IMetaExpressionVariableComputed> DependingVariables
        => DependingVariablesCache;

    public IEnumerable<IMetaExpressionVariableComputed> DirectDependingVariables
        => DependingVariablesCache.Where(v =>
            v.RhsNumbers.Contains(this)
        );

    public IEnumerable<IMetaExpressionVariableComputed> DirectDependingIntermediateVariables
        => DependingVariablesCache.Where(v =>
            v.IsIntermediateVariable && v.RhsNumbers.Contains(this)
        );

    public IMetaExpressionVariableComputed LastDependingVariable
        => DependingVariablesCache.MaxBy(v => v.ComputationOrder);

    public int LastDependingVariableComputationOrder
        => LastDependingVariable?.ComputationOrder ?? -1;

    public int MaxComputationLevel
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


    private MetaExpressionNumber(MetaContext context, IMetaExpressionHeadSpecsNumber headSpecs)
    {
        Context = context;
        AtomicExpressionId = Context.GetNextAtomicExpressionId();
        NumberHeadSpecs = headSpecs;
    }


    public IMetaExpression Simplify(IMetaExpressionEvaluator symbolicEvaluator)
    {
        return this;
    }

    public void AddDependingVariable(IMetaExpressionVariableComputed computedVar)
    {
        DependingVariablesCache.Add(computedVar);
    }

    public void ClearDependencyData()
    {
        DependingVariablesCache.Clear();
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

    public Tuple<bool, IMetaExpression> ReplaceAllExpressionByVariable(IMetaExpression oldExpr, string variableName, MetaContext context)
    {
        var newExpr = context.GetVariable(variableName);

        return MetaExpressionUtils.Equals(this, oldExpr)
            ? new Tuple<bool, IMetaExpression>(true, newExpr)
            : new Tuple<bool, IMetaExpression>(false, this);
    }

    public Tuple<bool, IMetaExpression> ReplaceAllVariableByVariable(string oldVariableName, string newVariableName, MetaContext context)
    {
        var oldExpr = context.GetVariable(oldVariableName);
        var newExpr = context.GetVariable(newVariableName);

        return MetaExpressionUtils.Equals(this, oldExpr)
            ? new Tuple<bool, IMetaExpression>(true, newExpr)
            : new Tuple<bool, IMetaExpression>(false, this);
    }

    public IMetaExpression CopyToContext(MetaContext context)
    {
        return context.ImportCopy(this);
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

    public void SetStateFrom(IMetaExpressionNumber number)
    {
        UpdateExternalName(number.ExternalName);
    }

    public bool UpdateExternalName(string externalName)
    {
        Debug.Assert(
            string.IsNullOrEmpty(_externalName) || _externalName == externalName
        );

        var externalNameOld = _externalName;

        if (string.IsNullOrEmpty(_externalName))
            _externalName = externalName ?? string.Empty;

        return _externalName != externalNameOld;
    }

    public override string ToString()
    {
        return NumberHeadSpecs.NumberText;
    }
}