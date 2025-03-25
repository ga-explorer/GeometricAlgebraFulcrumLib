using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;
using GeneticSharp;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Optimizer;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Optimizer.Genetic;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Evaluators;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.HeadSpecs;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Numbers;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Variables;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Optimizer.Genetic2;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Processors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Collections;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Collections.Lists;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;

// ReSharper disable CompareOfFloatsByEqualityOperator

// ReSharper disable MemberCanBePrivate.Global

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context;

[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public sealed class MetaContext :
    ILinearProcessor<IMetaExpressionAtomic>,
    IXGaProcessorContainer<IMetaExpressionAtomic>
{
    private int _tempNamesIndex;

    private int _atomicId = -1;

    private int _computationOrder = -1;

    private readonly Dictionary<string, IMetaExpressionNumber> _numbersDictionary
        = new Dictionary<string, IMetaExpressionNumber>();

    private readonly Dictionary<string, IMetaExpressionVariableParameter> _parametersVariablesDictionary
        = new Dictionary<string, IMetaExpressionVariableParameter>();

    private readonly Dictionary<string, IMetaExpressionVariableComputed> _computedVariablesDictionary
        = new Dictionary<string, IMetaExpressionVariableComputed>();
        
    
    public double ZeroEpsilon
    {
        get => MetaExpressionProcessor.ZeroEpsilon;
        set => MetaExpressionProcessor.ZeroEpsilon = value;
    }

    public bool IsNumeric 
        => false;

    public bool IsSymbolic 
        => true;

    public Scalar<IMetaExpressionAtomic> Zero { get; }
    
    public Scalar<IMetaExpressionAtomic> PositiveInfinity { get; }
    
    public Scalar<IMetaExpressionAtomic> NegativeInfinity { get; }

    public Scalar<IMetaExpressionAtomic> One { get; }
    
    public Scalar<IMetaExpressionAtomic> MinusOne { get; }
    
    public Scalar<IMetaExpressionAtomic> Two { get; }
    
    public Scalar<IMetaExpressionAtomic> MinusTwo { get; }
    
    public Scalar<IMetaExpressionAtomic> Ten { get; }
    
    public Scalar<IMetaExpressionAtomic> MinusTen { get; }
    
    public Scalar<IMetaExpressionAtomic> Pi { get; }
    
    public Scalar<IMetaExpressionAtomic> PiTimes2 { get; }
    
    public Scalar<IMetaExpressionAtomic> PiTimes4 { get; }

    public Scalar<IMetaExpressionAtomic> PiOver2 { get; }
    
    public Scalar<IMetaExpressionAtomic> E { get; }
    
    public Scalar<IMetaExpressionAtomic> DegreeToRadianFactor { get; }
    
    public Scalar<IMetaExpressionAtomic> RadianToDegreeFactor { get; }

    public IMetaExpressionAtomic ZeroValue { get; }
    
    public IMetaExpressionAtomic PositiveInfinityValue { get; }
    
    public IMetaExpressionAtomic NegativeInfinityValue { get; }

    public IMetaExpressionAtomic OneValue { get; }

    public IMetaExpressionAtomic MinusOneValue { get; }

    public IMetaExpressionAtomic TwoValue { get; }
        
    public IMetaExpressionAtomic MinusTwoValue { get; }
        
    public IMetaExpressionAtomic TenValue { get; }
        
    public IMetaExpressionAtomic MinusTenValue { get; }

    public IMetaExpressionAtomic PiValue { get; }

    public IMetaExpressionAtomic PiTimes2Value { get; }
    
    public IMetaExpressionAtomic PiTimes4Value { get; }

    public IMetaExpressionAtomic PiOver2Value { get; }

    public IMetaExpressionAtomic EValue { get; }

    public IMetaExpressionAtomic DegreeToRadianFactorValue { get; }
        
    public IMetaExpressionAtomic RadianToDegreeFactorValue { get; }

    public IScalarProcessor<IMetaExpressionAtomic> ScalarProcessor 
        => this;

    public XGaProcessor<IMetaExpressionAtomic>? XGaProcessor { get; set; }
        
    //public IGeometricAlgebraProcessor<IMetaExpressionAtomic> GeometricProcessor { get; set; }
    
    public bool ContainsGeometricProcessor 
        => XGaProcessor is not null;

    public ScalarProcessorOfMetaExpression MetaExpressionProcessor { get; }

    public AngouriMathMetaExpressionEvaluator DefaultEvaluator { get; }

    private IMetaExpressionEvaluator _symbolicEvaluator;
    public IMetaExpressionEvaluator SymbolicEvaluator
    {
        get => _symbolicEvaluator ?? DefaultEvaluator;
        set => _symbolicEvaluator = value;
    }

    public MetaContextOptions ContextOptions { get; }
        = new MetaContextOptions();

    public MetaExpressionNumberFactory NumbersFactory { get; }

    public MetaExpressionConstantFactory ConstantsFactory { get; }

    public MetaExpressionParameterVariableFactory ParameterVariablesFactory { get; }

    public MetaExpressionComputedVariableFactory ComputedVariablesFactory { get; }

    public MetaExpressionFunctionHeadSpecsFactory FunctionHeadSpecsFactory { get; }

    public string DefaultSymbolName { get; set; } 
        = "tmpVar_";

    public bool MergeExpressions { get; set; }
        = false;

    public int ComputationsCount 
        => _computedVariablesDictionary.Values.Aggregate(
            0, 
            (count, compVar) => 
                count + compVar.RhsExpression.ComputationsCount
        );

    
    public Scalar<IMetaExpressionAtomic> this[int number]
    {
        get
        {
            var scalar = 
                GetOrDefineLiteralNumber(number);

            return ScalarProcessor.ScalarFromValue(scalar);
        }
    }

    public Scalar<IMetaExpressionAtomic> this[double number]
    {
        get
        {
            var scalar = 
                GetOrDefineLiteralNumber(number);

            return ScalarProcessor.ScalarFromValue(scalar);
        }
    }

    public Scalar<IMetaExpressionAtomic> this[string scalarVarName]
    {
        get
        {
            var scalar = 
                GetOrDefineParameterVariable(scalarVarName);

            return ScalarProcessor.ScalarFromValue(scalar);
        }
    }


    //context: Hide the constructors and use static Create() methods to automatically assign 
    //context: geometric algebra processors if needed
    public MetaContext()
    {
        NumbersFactory = new MetaExpressionNumberFactory(this);
        ConstantsFactory = new MetaExpressionConstantFactory(this);
        ParameterVariablesFactory = new MetaExpressionParameterVariableFactory(this);
        ComputedVariablesFactory = new MetaExpressionComputedVariableFactory(this);

        ZeroValue = GetOrDefineLiteralNumber(0);
        OneValue = GetOrDefineLiteralNumber(1);
        MinusOneValue = GetOrDefineLiteralNumber(-1);
        TwoValue = GetOrDefineLiteralNumber(2);
        MinusTwoValue = GetOrDefineLiteralNumber(-2);
        TenValue = GetOrDefineLiteralNumber(10);
        MinusTenValue = GetOrDefineLiteralNumber(-10);
        PiValue = GetOrDefineSymbolicNumber(MetaExpressionNumberNames.Pi, Math.PI);
        PiTimes2Value = GetOrDefineLiteralNumber(Math.Tau);
        PiTimes4Value = GetOrDefineLiteralNumber(4 * Math.PI);
        PiOver2Value = GetOrDefineLiteralNumber(0.5d * Math.PI);
        EValue = GetOrDefineSymbolicNumber(MetaExpressionNumberNames.E, Math.E);
        DegreeToRadianFactorValue = GetOrDefineLiteralNumber(Math.PI / 180d);
        RadianToDegreeFactorValue = GetOrDefineLiteralNumber(180d / Math.PI);
        PositiveInfinityValue = GetOrDefineLiteralNumber(double.PositiveInfinity);
        NegativeInfinityValue = GetOrDefineLiteralNumber(double.NegativeInfinity);

        Zero = this.ScalarFromValue(ZeroValue);
        One = this.ScalarFromValue(OneValue);
        MinusOne = this.ScalarFromValue(MinusOneValue);
        Two = this.ScalarFromValue(TwoValue);
        MinusTwo = this.ScalarFromValue(MinusTwoValue);
        Ten = this.ScalarFromValue(TenValue);
        MinusTen = this.ScalarFromValue(MinusTenValue);
        Pi = this.ScalarFromValue(PiValue);
        E = this.ScalarFromValue(EValue);
        PiTimes2 = this.ScalarFromValue(PiTimes2Value);
        PiTimes4 = this.ScalarFromValue(PiTimes4Value);
        PiOver2 = this.ScalarFromValue(PiOver2Value);
        DegreeToRadianFactor = this.ScalarFromValue(DegreeToRadianFactorValue);
        RadianToDegreeFactor = this.ScalarFromValue(RadianToDegreeFactorValue);
        PositiveInfinity = this.ScalarFromValue(PositiveInfinityValue);
        NegativeInfinity = this.ScalarFromValue(NegativeInfinityValue);

        //These must be initialized after all other members
        MetaExpressionProcessor = new ScalarProcessorOfMetaExpression(this);
        FunctionHeadSpecsFactory = new MetaExpressionFunctionHeadSpecsFactory(this);
        DefaultEvaluator = this.CreateAngouriMathEvaluator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MetaContext(XGaProcessor<IMetaExpressionAtomic> geometricProcessor)
        : this()
    {
        AttachXGaProcessor(geometricProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MetaContext(MetaContextOptions options)
        : this()
    {
        ContextOptions.SetOptions(options);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MetaContext(IMetaExpressionEvaluator expressionEvaluator)
        : this()
    {
        SymbolicEvaluator = expressionEvaluator;
    }


    public bool IsValidExpression(IMetaExpression expr)
    {
        if (!ReferenceEquals(expr.Context, this)) 
            return false;

        if (expr is IMetaExpressionAtomic)
            return true;

        foreach (var rhsAtomic in expr.AtomicExpressions)
        {
            if (!ReferenceEquals(rhsAtomic.Context, this)) 
                return false;

            if (rhsAtomic is MetaExpressionNumber number)
            {
                if (!_numbersDictionary.TryGetValue(number.NumberText, out var number1)) 
                    return false;

                //if (!ReferenceEquals(number, number1)) 
                //    return false;
            }
            else if (rhsAtomic is MetaExpressionVariableParameter parameter)
            {
                if (!_parametersVariablesDictionary.TryGetValue(parameter.InternalName, out var parameter1)) 
                    return false;

                if (!ReferenceEquals(parameter, parameter1)) 
                    return false;

            }
            else if (rhsAtomic is IMetaExpressionVariableComputed variable)
            {
                if (!_computedVariablesDictionary.TryGetValue(variable.InternalName, out var variable1)) 
                    return false;

                if (!ReferenceEquals(variable, variable1)) 
                    return false;

            }
            else
            {
                throw new InvalidCastException();
            }
        }

        return true;
    }

    public static bool IsValidComputationOrder(IMetaExpressionVariableComputed computedVariable)
    {
        var rhsVars = 
            computedVariable.RhsComputedVariables.ToArray();

        var level = 0;
        foreach (var rhsVar in rhsVars)
        {
            if (level < rhsVar.MaxComputationLevel)
                level = rhsVar.MaxComputationLevel;

            if (rhsVar.ComputationOrder >= computedVariable.ComputationOrder)
                return false;
        }

        if (computedVariable.MaxComputationLevel != level + 1)
            return false;

        return true;
    }

    public bool IsValidComputationOrder()
    {
        return _computedVariablesDictionary.Values.All(IsValidComputationOrder);
    }

    public bool IsValid()
    {
        foreach (var number in _numbersDictionary.Values)
        {
            if (!ReferenceEquals(number.Context, this)) 
                return false;

        }

        foreach (var parameter in _parametersVariablesDictionary.Values)
        {
            if (!ReferenceEquals(parameter.Context, this)) 
                return false;

        }

        foreach (var computedVar in _computedVariablesDictionary.Values)
        {
            if (!ReferenceEquals(computedVar.Context, this)) 
                return false;

            //if (!ReferenceEquals(computedVar.RhsExpression.Context, this)) 
            //    return false;

            //foreach (var rhsAtomicExpr in computedVar.RhsAtomicExpressions)
            //{
            //    if (!ReferenceEquals(rhsAtomicExpr.Context, this)) 
            //        return false;

            //}

            if (!IsValidExpression(computedVar.RhsExpression)) 
                return false;

        }

        return true;
    }

    public MetaContext GetContextCopy()
    {
        var context = new MetaContext();

        if (XGaProcessor is not null)
            context.AttachXGaProcessor(XGaProcessor);

        context.ContextOptions.SetOptions(ContextOptions);
        context.SymbolicEvaluator = SymbolicEvaluator.GetEvaluatorCopy(context);
        context.MergeExpressions = MergeExpressions;
        context.DefaultSymbolName = DefaultSymbolName;
        context._tempNamesIndex = _tempNamesIndex;
        context._atomicId = _atomicId;
        context._computationOrder = _computationOrder;

        foreach (var number in _numbersDictionary.Values)
            context.ImportCopy(number);

        foreach (var parameter in _parametersVariablesDictionary.Values)
            context.ImportCopy(parameter);

        foreach (var variable in _computedVariablesDictionary.Values)
            context.ImportCopy(variable);

        //Debug.Assert(context.IsValid());
        
        McOptDependencyUpdate.Process(context);

        //Console.WriteLine("Original Context:");
        //Console.WriteLine(this.ToString());
        //Console.WriteLine();

        //Console.WriteLine("Copied Context:");
        //Console.WriteLine(context.ToString());
        //Console.WriteLine();

        return context;
    }

    public MetaContext GetContextCopyWithMerge(SortedSet<int> intermediateVarIndexList)
    {
        var newContext = GetContextCopy();

        var intermediateVariableArray = 
            newContext.GetIntermediateVariables().ToImmutableArray();

        if (intermediateVarIndexList.Count > 0 && intermediateVarIndexList.Last() >= intermediateVariableArray.Length)
            throw new IndexOutOfRangeException();

        var intermediateVariables = 
            intermediateVarIndexList.Select(i => intermediateVariableArray[i]);

        foreach (var intermediateVariable in intermediateVariables)
        {
            if (intermediateVariable.RhsExpression.ComputationsCount > 20)
                continue;

            // Initialize a list of intermediate variables with the selected one
            var intermediateVariableSet = new HashSet<IMetaExpressionVariableComputed>()
            {
                intermediateVariable
            };
        
            // Add all intermediate variables that directly depend on the selected one
            // to the list
            intermediateVariableSet.AddRange(
                intermediateVariableSet.SelectMany(
                    v => v.DirectDependingIntermediateVariables
                ).ToImmutableArray()
            );
        
            // Remove all selected intermediate variables in the list from the context
            foreach (var ivar in intermediateVariableSet)
                newContext.RemoveIntermediateVariable(ivar);

            newContext.RemoveIntermediateVariable(intermediateVariable);
        }

        return newContext;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AttachXGaProcessor(XGaProcessor<IMetaExpressionAtomic> processor)
    {
        XGaProcessor = processor;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private string GetNewSymbolName()
    {
        _tempNamesIndex++;

        return $"{DefaultSymbolName}{_tempNamesIndex}";
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal int GetNextAtomicExpressionId()
    {
        _atomicId++;

        return _atomicId;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal int GetNextComputationOrder()
    {
        _computationOrder++;

        return _computationOrder;
    }
 
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<IMetaExpressionNumber> GetNumbers()
    {
        return _numbersDictionary
            .Values
            .OrderBy(v => v.AtomicExpressionId);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<IMetaExpressionVariable> GetVariables()
    {
        return GetParameterVariables()
            .Cast<IMetaExpressionVariable>()
            .Concat(GetComputedVariables());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<IMetaExpressionVariableParameter> GetParameterVariables()
    {
        return _parametersVariablesDictionary
            .Values
            .OrderBy(v => v.AtomicExpressionId);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<IMetaExpressionAtomicIndependent> GetIndependentAtomics()
    {
        return GetNumbers()
            .Cast<IMetaExpressionAtomicIndependent>()
            .Concat(GetParameterVariables());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<IMetaExpressionVariableComputed> GetComputedVariables()
    {
        return _computedVariablesDictionary
            .Values
            .OrderBy(v => v.ComputationOrder);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<IMetaExpressionVariableComputed> GetIntermediateVariables()
    {
        return _computedVariablesDictionary
            .Values
            .Where(s => s.IsIntermediateVariable)
            .OrderBy(v => v.ComputationOrder);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public BijectiveList<string, IMetaExpressionVariableComputed> GetIntermediateVariablesList()
    {
        return _computedVariablesDictionary
            .Values
            .Where(s => s.IsIntermediateVariable)
            .OrderBy(v => v.ComputationOrder)
            .ToBijectiveList(v => v.InternalName);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<IMetaExpressionVariableComputed> GetOutputVariables()
    {
        return GetComputedVariables()
            .Where(s => s.IsOutputVariable);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<IMetaExpressionAtomic> GetAtomics()
    {
        return GetNumbers()
            .Cast<IMetaExpressionAtomic>()
            .Concat(GetParameterVariables())
            .Concat(GetComputedVariables());
    }

    /// <summary>
    /// The maximum number of temporary target variables required for the
    /// computations in this block
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetTargetTempVarsCount()
    {
        return GetIntermediateVariables().Any()
            ? GetIntermediateVariables().Max(item => item.NameIndex) + 1
            : 0;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsAtomic(string internalName)
    {
        if (_numbersDictionary.ContainsKey(internalName))
            return true;

        if (_parametersVariablesDictionary.ContainsKey(internalName))
            return true;

        if (_computedVariablesDictionary.ContainsKey(internalName))
            return true;

        return false;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsNumber(string numberText)
    {
        return _numbersDictionary.ContainsKey(numberText);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsParameterVariable(string internalName)
    {
        return _parametersVariablesDictionary.ContainsKey(internalName);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsComputedVariable(string internalName)
    {
        return _computedVariablesDictionary.ContainsKey(internalName);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsVariable(string internalName)
    {
        if (_parametersVariablesDictionary.ContainsKey(internalName))
        {
            return true;
        }

        if (_computedVariablesDictionary.ContainsKey(internalName))
        {
            return true;
        }

        return false;
    }


    public bool TryGetAtomic(string internalName, out IMetaExpressionAtomic atomic)
    {
        if (_numbersDictionary.TryGetValue(internalName, out var constantAtomic))
        {
            atomic = constantAtomic;
            return true;
        }

        if (_parametersVariablesDictionary.TryGetValue(internalName, out var parameterAtomic))
        {
            atomic = parameterAtomic;
            return true;
        }

        if (_computedVariablesDictionary.TryGetValue(internalName, out var computedAtomic))
        {
            atomic = computedAtomic;
            return true;
        }

        atomic = null;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetNumber(string numberText, out IMetaExpressionNumber number)
    {
        return _numbersDictionary.TryGetValue(numberText, out number);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetParameterVariable(string internalName, out IMetaExpressionVariableParameter variable)
    {
        return _parametersVariablesDictionary.TryGetValue(internalName, out variable);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetComputedVariable(string internalName, out IMetaExpressionVariableComputed variable)
    {
        return _computedVariablesDictionary.TryGetValue(internalName, out variable);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple<bool, IMetaExpressionVariableComputed> TryGetComputedVariable(string internalName)
    {
        var foundFlag = _computedVariablesDictionary.TryGetValue(internalName, out var computedVar);

        return new Tuple<bool, IMetaExpressionVariableComputed>(foundFlag, computedVar);
    }

    public bool TryGetVariable(string internalName, out IMetaExpressionVariable variable)
    {
        if (_parametersVariablesDictionary.TryGetValue(internalName, out var parameterVariable))
        {
            variable = parameterVariable;
            return true;
        }

        if (_computedVariablesDictionary.TryGetValue(internalName, out var computedVariable))
        {
            variable = computedVariable;
            return true;
        }

        variable = null;
        return false;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<IMetaExpressionAtomic> GetAtomicsByRhsExpressionText(string rhsExpressionText)
    {
        return GetAtomics().Where(expr => 
            expr.RhsExpressionText == rhsExpressionText
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<IMetaExpressionNumber> GetNumbers(string numberText)
    {
        return _numbersDictionary.Values.Where(expr => 
            expr.NumberHeadSpecs.NumberText == numberText
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<IMetaExpressionVariableComputed> GetComputedVariablesByRhsExpressionText(string rhsExpressionText)
    {
        return _computedVariablesDictionary.Values.Where(expr => 
            expr.RhsExpressionText == rhsExpressionText
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetVariableValue(string internalName, double number)
    {
        var variable = GetVariable(internalName);

        variable.SetRhsExpressionValue(number);
    }

    public IMetaExpressionAtomic GetAtomic(string internalName)
    {
        if (_numbersDictionary.TryGetValue(internalName, out var constantAtomic))
            return constantAtomic;

        if (_parametersVariablesDictionary.TryGetValue(internalName, out var parameterVariable))
            return parameterVariable;

        if (_computedVariablesDictionary.TryGetValue(internalName, out var computedVariable))
            return computedVariable;

        throw new KeyNotFoundException(internalName);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpressionNumber GetNumber(string numberText)
    {
        if (_numbersDictionary.TryGetValue(numberText, out var constantAtomic))
            return constantAtomic;
            
        throw new KeyNotFoundException(numberText);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpressionVariableParameter GetParameterVariable(string internalName)
    {
        if (_parametersVariablesDictionary.TryGetValue(internalName, out var variable))
            return variable;

        throw new KeyNotFoundException(internalName);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpressionVariableComputed GetComputedVariable(string internalName)
    {
        if (_computedVariablesDictionary.TryGetValue(internalName, out var variable))
            return variable;

        throw new KeyNotFoundException(internalName);
    }

    public IMetaExpressionVariable GetVariable(string internalName)
    {
        if (_parametersVariablesDictionary.TryGetValue(internalName, out var parameterVariable))
            return parameterVariable;

        if (_computedVariablesDictionary.TryGetValue(internalName, out var computedVariable))
            return computedVariable;

        throw new KeyNotFoundException(internalName);
    }
        
    public IMetaExpressionNumber GetOrDefineLiteralNumber(float numberValue)
    {
        var numberText = numberValue.ToString("G");
        if (_numbersDictionary.TryGetValue(numberText, out var atomic))
            return atomic;

        //Make sure there are no duplicates in the other two dictionaries
        if (_parametersVariablesDictionary.ContainsKey(numberText))
            throw new InvalidOperationException();

        if (_computedVariablesDictionary.ContainsKey(numberText))
            throw new InvalidOperationException();

        atomic = MetaExpressionNumber.Create(this, numberValue);

        _numbersDictionary.Add(
            numberText, 
            atomic
        );

        return atomic;
    }

    public IMetaExpressionNumber GetOrDefineLiteralNumber(double numberValue, int roundDigits = 0)
    {
        var numberText = roundDigits < 7
            ? numberValue.ToString("G")
            : Math.Round(numberValue, roundDigits).ToString("G");

        if (_numbersDictionary.TryGetValue(numberText, out var atomic))
            return atomic;

        //Make sure there are no duplicates in the other two dictionaries
        if (_parametersVariablesDictionary.ContainsKey(numberText))
            throw new InvalidOperationException();

        if (_computedVariablesDictionary.ContainsKey(numberText))
            throw new InvalidOperationException();

        atomic = MetaExpressionNumber.Create(this, numberValue);

        _numbersDictionary.Add(
            numberText, 
            atomic
        );

        return atomic;
    }

    public IMetaExpressionNumber GetOrDefineLiteralNumber(int numberValue)
    {
        var numberText = numberValue.ToString();
        if (_numbersDictionary.TryGetValue(numberText, out var atomic))
            return atomic;

        //Make sure there are no duplicates in the other two dictionaries
        if (_parametersVariablesDictionary.ContainsKey(numberText))
            throw new InvalidOperationException();

        if (_computedVariablesDictionary.ContainsKey(numberText))
            throw new InvalidOperationException();

        atomic = MetaExpressionNumber.Create(this, numberValue);

        _numbersDictionary.Add(
            numberText, 
            atomic
        );

        return atomic;
    }

    public IMetaExpressionNumber GetOrDefineLiteralNumber(uint numberValue)
    {
        var numberText = numberValue.ToString();
        if (_numbersDictionary.TryGetValue(numberText, out var atomic))
            return atomic;

        //Make sure there are no duplicates in the other two dictionaries
        if (_parametersVariablesDictionary.ContainsKey(numberText))
            throw new InvalidOperationException();

        if (_computedVariablesDictionary.ContainsKey(numberText))
            throw new InvalidOperationException();

        atomic = MetaExpressionNumber.Create(this, numberValue);

        _numbersDictionary.Add(
            numberText, 
            atomic
        );

        return atomic;
    }

    public IMetaExpressionNumber GetOrDefineLiteralNumber(long numberValue)
    {
        var numberText = numberValue.ToString();
        if (_numbersDictionary.TryGetValue(numberText, out var atomic))
            return atomic;

        //Make sure there are no duplicates in the other two dictionaries
        if (_parametersVariablesDictionary.ContainsKey(numberText))
            throw new InvalidOperationException();

        if (_computedVariablesDictionary.ContainsKey(numberText))
            throw new InvalidOperationException();

        atomic = MetaExpressionNumber.Create(this, numberValue);

        _numbersDictionary.Add(
            numberText, 
            atomic
        );

        return atomic;
    }

    public IMetaExpressionNumber GetOrDefineLiteralNumber(ulong numberValue)
    {
        var numberText = numberValue.ToString();
        if (_numbersDictionary.TryGetValue(numberText, out var atomic))
            return atomic;

        //Make sure there are no duplicates in the other two dictionaries
        if (_parametersVariablesDictionary.ContainsKey(numberText))
            throw new InvalidOperationException();

        if (_computedVariablesDictionary.ContainsKey(numberText))
            throw new InvalidOperationException();

        atomic = MetaExpressionNumber.Create(this, numberValue);

        _numbersDictionary.Add(
            numberText, 
            atomic
        );

        return atomic;
    }

    public IMetaExpressionNumber GetOrDefineRationalNumber(long numerator, long denominator)
    {
        var numberText = 
            MetaExpressionNumber.GetRationalNumberText(numerator, denominator);

        if (_numbersDictionary.TryGetValue(numberText, out var atomic))
            return atomic;

        //Make sure there are no duplicates in the other two dictionaries
        if (_parametersVariablesDictionary.ContainsKey(numberText))
            throw new InvalidOperationException();

        if (_computedVariablesDictionary.ContainsKey(numberText))
            throw new InvalidOperationException();

        atomic = MetaExpressionNumber.CreateRational(this, numerator, denominator);

        _numbersDictionary.Add(
            numberText, 
            atomic
        );

        return atomic;
    }

    public IMetaExpressionNumber GetOrDefineSymbolicNumber(string numberText, double numberValue)
    {
        if (_numbersDictionary.TryGetValue(numberText, out var atomic))
            return atomic;

        if (double.TryParse(numberText, out _))
            return GetOrDefineLiteralNumber(numberValue, ContextOptions.Float64Precision);

        //Make sure there are no duplicates in the other two dictionaries
        if (_parametersVariablesDictionary.ContainsKey(numberText))
            throw new InvalidOperationException();

        if (_computedVariablesDictionary.ContainsKey(numberText))
            throw new InvalidOperationException();

        atomic = MetaExpressionNumber.CreateSymbolic(this, numberText, numberValue);

        _numbersDictionary.Add(
            numberText, 
            atomic
        );

        return atomic;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpressionNumber GetOrDefineNumber(IMetaExpressionHeadSpecsNumber headSpecs)
    {
        return headSpecs switch
        {
            MetaExpressionHeadSpecsNumberFloat32 numberHeadSpecsFloat32 => 
                GetOrDefineLiteralNumber(numberHeadSpecsFloat32.NumberFloat32Value),

            MetaExpressionHeadSpecsNumberFloat64 numberHeadSpecsFloat64 => 
                GetOrDefineLiteralNumber(numberHeadSpecsFloat64.NumberFloat64Value),

            MetaExpressionHeadSpecsNumberInt32 numberHeadSpecsInt32 => 
                GetOrDefineLiteralNumber(numberHeadSpecsInt32.NumberInt32Value),

            MetaExpressionHeadSpecsNumberUInt32 numberHeadSpecsUInt32 => 
                GetOrDefineLiteralNumber(numberHeadSpecsUInt32.NumberUInt32Value),

            MetaExpressionHeadSpecsNumberInt64 numberHeadSpecsInt64 => 
                GetOrDefineLiteralNumber(numberHeadSpecsInt64.NumberInt64Value),

            MetaExpressionHeadSpecsNumberUInt64 numberHeadSpecsUInt64 => 
                GetOrDefineLiteralNumber(numberHeadSpecsUInt64.NumberUInt64Value),

            MetaExpressionHeadSpecsNumberRational numberHeadSpecsRational => 
                GetOrDefineRationalNumber(numberHeadSpecsRational.Numerator, numberHeadSpecsRational.Denominator),

            MetaExpressionHeadSpecsNumberSymbolic numberHeadSpecsSymbolic => 
                GetOrDefineSymbolicNumber(numberHeadSpecsSymbolic.NumberText, numberHeadSpecsSymbolic.NumberFloat64Value),

            _ => throw new InvalidOperationException()
        };
    }
    

    public IMetaExpressionVariableParameter GetOrDefineParameterVariable(string parameterName)
    {
        if (_parametersVariablesDictionary.TryGetValue(parameterName, out var atomic))
            return atomic;

        //Make sure there are no duplicates in the other two dictionaries
        if (_numbersDictionary.ContainsKey(parameterName))
            throw new InvalidOperationException();

        if (_computedVariablesDictionary.ContainsKey(parameterName))
            throw new InvalidOperationException();

        atomic = MetaExpressionVariableParameter.Create(
            this, 
            parameterName
        );

        _parametersVariablesDictionary.Add(
            parameterName,
            atomic
        );

        //_rhsExpressionsTextDictionary.Add(
        //    parameterName,
        //    atomic
        //);

        return atomic;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpressionVariableComputed GetOrDefineComputedVariable(Func<IMetaExpression, IMetaExpression> computingFunc, IMetaExpressionAtomic dependsOnAtomic)
    {
        var rhsScalarValue = computingFunc(
            dependsOnAtomic.GetScalarValue(MergeExpressions)
        );

        return GetOrDefineComputedVariable(
            rhsScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpressionVariableComputed GetOrDefineComputedVariable(Func<IMetaExpression, IMetaExpression, IMetaExpression> computingFunc, IMetaExpressionAtomic dependsOnAtomic1, IMetaExpressionAtomic dependsOnAtomic2)
    {
        var rhsScalarValue = computingFunc(
            dependsOnAtomic1.GetScalarValue(MergeExpressions),
            dependsOnAtomic2.GetScalarValue(MergeExpressions)
        );

        return GetOrDefineComputedVariable(
            rhsScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpressionVariableComputed GetOrDefineComputedVariable(Func<IMetaExpression, IMetaExpression, IMetaExpression, IMetaExpression> computingFunc, IMetaExpressionAtomic dependsOnAtomic1, IMetaExpressionAtomic dependsOnAtomic2, IMetaExpressionAtomic dependsOnAtomic3)
    {
        var rhsScalarValue = computingFunc(
            dependsOnAtomic1.GetScalarValue(MergeExpressions),
            dependsOnAtomic2.GetScalarValue(MergeExpressions),
            dependsOnAtomic3.GetScalarValue(MergeExpressions)
        );

        return GetOrDefineComputedVariable(
            rhsScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpressionVariableComputed GetOrDefineComputedVariable(Func<IEnumerable<IMetaExpression>, IMetaExpression> computingFunc, IEnumerable<IMetaExpressionAtomic> dependsOnAtomics)
    {
        var atomicsArray = 
            dependsOnAtomics.ToArray();

        var rhsScalarValue = computingFunc(
            atomicsArray.Select(atomic => 
                atomic.GetScalarValue(MergeExpressions)
            )
        );

        return GetOrDefineComputedVariable(
            rhsScalarValue
        );
    }

    public IMetaExpressionVariableComputed GetOrDefineComputedVariable(IMetaExpression rhsExpression)
    {
        var rhsExpressionText = 
            MetaExpressionProcessor.ToText(rhsExpression);

        var computedVariable = 
            _computedVariablesDictionary
                .Values
                .FirstOrDefault(v => 
                    v.RhsExpressionText == rhsExpressionText
                );

        if (computedVariable is not null)
            return computedVariable;

        var internalName = GetNewSymbolName();

        //Make sure there are no duplicate names in the other two dictionaries
        if (_numbersDictionary.ContainsKey(internalName))
            throw new InvalidOperationException();

        if (_parametersVariablesDictionary.ContainsKey(internalName))
            throw new InvalidOperationException();

        computedVariable = MetaExpressionVariableComputed.Create(
            this,
            internalName,
            rhsExpression
        );

        _computedVariablesDictionary.Add(
            internalName, 
            computedVariable
        );

        return computedVariable;
    }
    
    public IMetaExpressionVariableComputed DefineSubExpressionVariable(IMetaExpression rhsExpression, bool isUsedOnce)
    {
        var internalName = GetNewSymbolName();

        //Make sure there are no duplicate names in the other two dictionaries
        if (_numbersDictionary.ContainsKey(internalName))
            throw new InvalidOperationException();

        if (_parametersVariablesDictionary.ContainsKey(internalName))
            throw new InvalidOperationException();

        var computedVariable = 
            MetaExpressionVariableComputed.CreateFactoredSubExpression(
                this,
                internalName,
                rhsExpression,
                isUsedOnce
            );

        _computedVariablesDictionary.Add(
            internalName, 
            computedVariable
        );

        return computedVariable;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpressionNumber ImportCopy(IMetaExpressionNumber number)
    {
        var newNumber = GetOrDefineNumber(number.NumberHeadSpecs);

        newNumber.SetStateFrom(number);

        return newNumber;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpressionVariableParameter ImportCopy(IMetaExpressionVariableParameter parameter)
    {
        var internalName = parameter.InternalName;

        if (_parametersVariablesDictionary.TryGetValue(internalName, out var newParameter))
            return newParameter;
        
        //Make sure there are no duplicates in the other two dictionaries
        if (_numbersDictionary.ContainsKey(internalName))
            throw new InvalidOperationException();

        if (_computedVariablesDictionary.ContainsKey(internalName))
            throw new InvalidOperationException();

        newParameter = MetaExpressionVariableParameter.Create(
            this, 
            internalName
        );

        _parametersVariablesDictionary.Add(
            internalName,
            newParameter
        );

        //_rhsExpressionsTextDictionary.Add(
        //    internalName,
        //    newParameter
        //);

        newParameter.SetStateFrom(parameter);

        return newParameter;
    }

    public IMetaExpressionVariableComputed ImportCopy(IMetaExpressionVariableComputed computedVariable)
    {
        var internalName = computedVariable.InternalName;
        
        if (_computedVariablesDictionary.TryGetValue(internalName, out var newComputedVariable))
        {
            Debug.Assert(
                newComputedVariable.RhsExpressionText == MetaExpressionProcessor.ToText(computedVariable.RhsExpression)
            );
            
            return newComputedVariable;
        }

        //Make sure there are no duplicate names in the other two dictionaries
        if (_numbersDictionary.ContainsKey(internalName))
            throw new InvalidOperationException();

        if (_parametersVariablesDictionary.ContainsKey(internalName))
            throw new InvalidOperationException();

        var newRhsExpression = computedVariable.RhsExpression.CopyToContext(this);

        newComputedVariable = MetaExpressionVariableComputed.Create(
            this,
            internalName,
            newRhsExpression
        );
        
        _computedVariablesDictionary.Add(
            internalName, 
            newComputedVariable
        );

        newComputedVariable.SetStateFrom(computedVariable);

        return newComputedVariable;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void RemoveNotUsedNumbers()
    {
        var numbersList = 
            _numbersDictionary
                .Values
                .Where(n => !n.HasDependingVariables)
                .Select(n => n.NumberText)
                .ToArray();

        foreach (var key in numbersList)
            _numbersDictionary.Remove(key);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void RemoveNotUsedParameterVariables()
    {
        var parameterVariableNamesList =
            _parametersVariablesDictionary
                .Values
                .Where(v => !v.HasDependingVariables)
                .Select(v => v.InternalName)
                .ToArray();

        foreach (var key in parameterVariableNamesList)
            _parametersVariablesDictionary.Remove(key);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void RemoveNotUsedComputedVariables()
    {
        var computedVariableNamesList =
            _computedVariablesDictionary
                .Values
                .Where(v => !v.IsOutputOrHasDependingVariables)
                .Select(v => v.InternalName)
                .ToArray();

        foreach (var key in computedVariableNamesList)
            _computedVariablesDictionary.Remove(key);
    }

    public void RemoveNotUsedComputedVariables(IReadOnlyList<string> internalNamesList)
    {
        foreach (var internalName in internalNamesList)
        {
            if (!_computedVariablesDictionary.TryGetValue(internalName, out var computedVariable))
                continue;

            if (computedVariable.IsOutputOrHasDependingVariables)
                continue;

            _computedVariablesDictionary.Remove(internalName);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void RemoveNotUsedAtomics()
    {
        RemoveNotUsedNumbers();

        RemoveNotUsedParameterVariables();

        RemoveNotUsedComputedVariables();
    }

    public void RemoveIntermediateVariable(IMetaExpressionVariableComputed intermediateVariable)
    {
        if (!intermediateVariable.IsIntermediateVariable)
            throw new InvalidOperationException();

        if (!ContainsComputedVariable(intermediateVariable.InternalName))
            return;

        //Console.WriteLine($"Removing {intermediateVariable.InternalName}");

        foreach (var depVar in intermediateVariable.DirectDependingVariables)
        {
            depVar.ReplaceRhsVariable(intermediateVariable.InternalName, intermediateVariable.RhsExpression);
            depVar.SimplifyRhsExpression();
        }

        var computedVariablesList = new List<IMetaExpressionVariableComputed>();

        foreach (var computedVariable in GetComputedVariables())
        {
            if (computedVariable.InternalName == intermediateVariable.InternalName)
                continue;

            computedVariablesList.Add(computedVariable);

            if (computedVariable.ComputationOrder > intermediateVariable.ComputationOrder)
                computedVariable.SetComputationOrder(computedVariable.ComputationOrder - 1);
        }

        ResetComputedVariables(computedVariablesList);

        McOptDependencyUpdate.Process(this);
    }
    
    public void RemoveIntermediateVariables(SortedSet<int> intermediateVariableIndexSet)
    {
        var intermediateVarList = 
            GetIntermediateVariables().ToImmutableArray();

        foreach (var index in intermediateVariableIndexSet)
        {
            var intermediateVariable = intermediateVarList[index];

            foreach (var depVar in intermediateVariable.DirectDependingVariables)
            {
                depVar.ReplaceRhsVariable(intermediateVariable.InternalName, intermediateVariable.RhsExpression);
                depVar.SimplifyRhsExpression();
            }
        }

        var intermediateVariableNameSet =
            intermediateVariableIndexSet.SelectToImmutableSortedSet(
                i => intermediateVarList[i].InternalName
            );

        var computedVariablesList = 
            new List<IMetaExpressionVariableComputed>(
                _computedVariablesDictionary.Count
            );

        var computationOrder = 0;
        foreach (var computedVariable in GetComputedVariables())
        {
            var computedVariableName = computedVariable.InternalName;

            if (intermediateVariableNameSet.Any(varName => varName == computedVariableName))
                continue;

            computedVariablesList.Add(computedVariable);

            computedVariable.SetComputationOrder(computationOrder++);
        }

        ResetComputedVariables(computedVariablesList);

        McOptDependencyUpdate.Process(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void UpdateDependencyData(bool removeNotUsedAtomics)
    {
        McOptDependencyUpdate.Process(this, removeNotUsedAtomics);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpressionAtomic> Add(IMetaExpressionAtomic scalar1, IMetaExpressionAtomic scalar2)
    {
        if (ContextOptions.PropagateConstants)
        {
            if (scalar1 is MetaExpressionNumber s1 && scalar2 is MetaExpressionNumber s2)
            {
                var number = 
                    s1.NumberHeadSpecs.NumberFloat64Value + 
                    s2.NumberHeadSpecs.NumberFloat64Value;
                
                return GetOrDefineLiteralNumber(
                    number, 
                    ContextOptions.Float64Precision
                ).ScalarFromValue(ScalarProcessor);
            }
        }

        return GetOrDefineComputedVariable(
            (a, b) => MetaExpressionProcessor.Add(a, b).ScalarValue,
            scalar1,
            scalar2
        ).ScalarFromValue(ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpressionAtomic> Subtract(IMetaExpressionAtomic scalar1, IMetaExpressionAtomic scalar2)
    {
        if (ContextOptions.PropagateConstants)
        {
            if (scalar1 is MetaExpressionNumber s1 && scalar2 is MetaExpressionNumber s2)
            {
                var number = 
                    s1.NumberHeadSpecs.NumberFloat64Value - 
                    s2.NumberHeadSpecs.NumberFloat64Value;
                
                return GetOrDefineLiteralNumber(
                    number, 
                    ContextOptions.Float64Precision
                ).ScalarFromValue(ScalarProcessor);
            }
        }

        return GetOrDefineComputedVariable(
            (a, b) => MetaExpressionProcessor.Subtract(a, b).ScalarValue,
            scalar1,
            scalar2
        ).ScalarFromValue(ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpressionAtomic> Times(IMetaExpressionAtomic scalar1, IMetaExpressionAtomic scalar2)
    {
        if (ContextOptions.PropagateConstants)
        {
            if (scalar1 is MetaExpressionNumber s1 && scalar2 is MetaExpressionNumber s2)
            {
                var number = 
                    s1.NumberHeadSpecs.NumberFloat64Value * 
                    s2.NumberHeadSpecs.NumberFloat64Value;
                
                return GetOrDefineLiteralNumber(
                    number, 
                    ContextOptions.Float64Precision
                ).ScalarFromValue(ScalarProcessor);
            }
        }

        return GetOrDefineComputedVariable(
            (a, b) => MetaExpressionProcessor.Times(a, b).ScalarValue,
            scalar1,
            scalar2
        ).ScalarFromValue(ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpressionAtomic> Divide(IMetaExpressionAtomic scalar1, IMetaExpressionAtomic scalar2)
    {
        if (ContextOptions.PropagateConstants)
        {
            if (scalar1 is MetaExpressionNumber s1 && scalar2 is MetaExpressionNumber s2)
            {
                var number = 
                    s1.NumberHeadSpecs.NumberFloat64Value / 
                    s2.NumberHeadSpecs.NumberFloat64Value;
                
                return GetOrDefineLiteralNumber(
                    number, 
                    ContextOptions.Float64Precision
                ).ScalarFromValue(ScalarProcessor);
            }
        }

        return GetOrDefineComputedVariable(
            (a, b) => MetaExpressionProcessor.Divide(a, b).ScalarValue,
            scalar1,
            scalar2
        ).ScalarFromValue(ScalarProcessor);
    }

    public Scalar<IMetaExpressionAtomic> VectorToRadians(IMetaExpressionAtomic scalarX, IMetaExpressionAtomic scalarY)
    {
        if (ContextOptions.PropagateConstants)
        {
            if (scalarX is MetaExpressionNumber s1 && scalarY is MetaExpressionNumber s2)
            {
                var number = 
                    s1.NumberHeadSpecs.NumberFloat64Value.ArcTan2(
                        s2.NumberHeadSpecs.NumberFloat64Value
                    );
                
                if (number < 0) number += Math.Tau;

                return GetOrDefineLiteralNumber(
                    number, 
                    ContextOptions.Float64Precision
                ).ScalarFromValue(ScalarProcessor);
            }
        }

        return GetOrDefineComputedVariable(
            (a, b) => MetaExpressionProcessor.VectorToRadians(a, b).ScalarValue,
            scalarX,
            scalarY
        ).ScalarFromValue(ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpressionAtomic> Positive(IMetaExpressionAtomic scalar)
    {
        if (ContextOptions.PropagateConstants)
        {
            if (scalar is MetaExpressionNumber s1)
            {
                var number = 
                    s1.NumberHeadSpecs.NumberFloat64Value;
                
                return GetOrDefineLiteralNumber(
                    number, 
                    ContextOptions.Float64Precision
                ).ScalarFromValue(ScalarProcessor);
            }
        }

        return GetOrDefineComputedVariable(
            scalar1 => MetaExpressionProcessor.Positive(scalar1).ScalarValue,
            scalar
        ).ScalarFromValue(ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpressionAtomic> Negative(IMetaExpressionAtomic scalar)
    {
        if (ContextOptions.PropagateConstants)
        {
            if (scalar is MetaExpressionNumber s1)
            {
                var number = 
                    -s1.NumberHeadSpecs.NumberFloat64Value;
                
                return GetOrDefineLiteralNumber(
                    number, 
                    ContextOptions.Float64Precision
                ).ScalarFromValue(ScalarProcessor);
            }
        }

        return GetOrDefineComputedVariable(
            scalar1 => MetaExpressionProcessor.Negative(scalar1).ScalarValue,
            scalar
        ).ScalarFromValue(ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpressionAtomic> Inverse(IMetaExpressionAtomic scalar)
    {
        if (ContextOptions.PropagateConstants)
        {
            if (scalar is MetaExpressionNumber s1)
            {
                var number = 
                    1d / s1.NumberHeadSpecs.NumberFloat64Value;
                
                return GetOrDefineLiteralNumber(
                    number, 
                    ContextOptions.Float64Precision
                ).ScalarFromValue(ScalarProcessor);
            }
        }

        return GetOrDefineComputedVariable(
            scalar1 => MetaExpressionProcessor.Inverse(scalar1).ScalarValue,
            scalar
        ).ScalarFromValue(ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpressionAtomic> Sign(IMetaExpressionAtomic scalar)
    {
        if (ContextOptions.PropagateConstants)
        {
            if (scalar is MetaExpressionNumber s1)
            {
                var number = 
                    Math.Sign(s1.NumberHeadSpecs.NumberFloat64Value);
                
                return GetOrDefineLiteralNumber(number).ScalarFromValue(ScalarProcessor);
            }
        }

        return GetOrDefineComputedVariable(
            scalar1 => MetaExpressionProcessor.Sign(scalar1).ScalarValue,
            scalar
        ).ScalarFromValue(ScalarProcessor);
    }

    public Scalar<IMetaExpressionAtomic> UnitStep(IMetaExpressionAtomic scalar)
    {
        if (ContextOptions.PropagateConstants)
        {
            if (scalar is MetaExpressionNumber s1)
            {
                var number = 
                    s1.NumberHeadSpecs.NumberFloat64Value >= 0 ? 1 : 0;
                
                return GetOrDefineLiteralNumber(number).ScalarFromValue(ScalarProcessor);
            }
        }

        return GetOrDefineComputedVariable(
            scalar1 => MetaExpressionProcessor.UnitStep(scalar1).ScalarValue,
            scalar
        ).ScalarFromValue(ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpressionAtomic> Abs(IMetaExpressionAtomic scalar)
    {
        if (ContextOptions.PropagateConstants)
        {
            if (scalar is MetaExpressionNumber s1)
            {
                var number =
                    Math.Abs(s1.NumberHeadSpecs.NumberFloat64Value);
                
                return GetOrDefineLiteralNumber(
                    number, 
                    ContextOptions.Float64Precision
                ).ScalarFromValue(ScalarProcessor);
            }
        }

        return GetOrDefineComputedVariable(
            scalar1 => MetaExpressionProcessor.Abs(scalar1).ScalarValue,
            scalar
        ).ScalarFromValue(ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpressionAtomic> Sqrt(IMetaExpressionAtomic scalar)
    {
        if (ContextOptions.PropagateConstants)
        {
            if (scalar is MetaExpressionNumber s1)
            {
                var number = 
                    Math.Sqrt(s1.NumberHeadSpecs.NumberFloat64Value);
                
                return GetOrDefineLiteralNumber(
                    number, 
                    ContextOptions.Float64Precision
                ).ScalarFromValue(ScalarProcessor);
            }
        }

        return GetOrDefineComputedVariable(
            scalar1 => MetaExpressionProcessor.Sqrt(scalar1).ScalarValue,
            scalar
        ).ScalarFromValue(ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpressionAtomic> SqrtOfAbs(IMetaExpressionAtomic scalar)
    {
        if (ContextOptions.PropagateConstants)
        {
            if (scalar is MetaExpressionNumber s1)
            {
                var number = 
                    Math.Sqrt(Math.Abs(s1.NumberHeadSpecs.NumberFloat64Value));
                
                return GetOrDefineLiteralNumber(
                    number, 
                    ContextOptions.Float64Precision
                ).ScalarFromValue(ScalarProcessor);
            }
        }

        return GetOrDefineComputedVariable(
            scalar1 => MetaExpressionProcessor.SqrtOfAbs(scalar1).ScalarValue,
            scalar
        ).ScalarFromValue(ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpressionAtomic> Exp(IMetaExpressionAtomic scalar)
    {
        if (ContextOptions.PropagateConstants)
        {
            if (scalar is MetaExpressionNumber s1)
            {
                var number = 
                    Math.Exp(s1.NumberHeadSpecs.NumberFloat64Value);
                
                return GetOrDefineLiteralNumber(
                    number, 
                    ContextOptions.Float64Precision
                ).ScalarFromValue(ScalarProcessor);
            }
        }

        return GetOrDefineComputedVariable(
            scalar1 => MetaExpressionProcessor.Exp(scalar1).ScalarValue,
            scalar
        ).ScalarFromValue(ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpressionAtomic> LogE(IMetaExpressionAtomic scalar)
    {
        if (ContextOptions.PropagateConstants)
        {
            if (scalar is MetaExpressionNumber s1)
            {
                var number = 
                    s1.NumberHeadSpecs.NumberFloat64Value.LogE();
                
                return GetOrDefineLiteralNumber(
                    number, 
                    ContextOptions.Float64Precision
                ).ScalarFromValue(ScalarProcessor);
            }
        }

        return GetOrDefineComputedVariable(
            scalar1 => MetaExpressionProcessor.LogE(scalar1).ScalarValue,
            scalar
        ).ScalarFromValue(ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpressionAtomic> Log2(IMetaExpressionAtomic scalar)
    {
        if (ContextOptions.PropagateConstants)
        {
            if (scalar is MetaExpressionNumber s1)
            {
                var number = 
                    s1.NumberHeadSpecs.NumberFloat64Value.Log2();
                
                return GetOrDefineLiteralNumber(
                    number, 
                    ContextOptions.Float64Precision
                ).ScalarFromValue(ScalarProcessor);
            }
        }

        return GetOrDefineComputedVariable(
            scalar1 => MetaExpressionProcessor.Log2(scalar1).ScalarValue,
            scalar
        ).ScalarFromValue(ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpressionAtomic> Log10(IMetaExpressionAtomic scalar)
    {
        if (ContextOptions.PropagateConstants)
        {
            if (scalar is MetaExpressionNumber s1)
            {
                var number = 
                    s1.NumberHeadSpecs.NumberFloat64Value.Log10();
                
                return GetOrDefineLiteralNumber(
                    number, 
                    ContextOptions.Float64Precision
                ).ScalarFromValue(ScalarProcessor);
            }
        }

        return GetOrDefineComputedVariable(
            scalar1 => MetaExpressionProcessor.Log10(scalar1).ScalarValue,
            scalar
        ).ScalarFromValue(ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpressionAtomic> Power(IMetaExpressionAtomic baseScalar, IMetaExpressionAtomic scalar)
    {
        if (ContextOptions.PropagateConstants)
        {
            if (baseScalar is MetaExpressionNumber s1 && scalar is MetaExpressionNumber s2)
            {
                var number = 
                    s1.NumberHeadSpecs.NumberFloat64Value.Power(
                        s2.NumberHeadSpecs.NumberFloat64Value
                    );
                
                return GetOrDefineLiteralNumber(
                    number, 
                    ContextOptions.Float64Precision
                ).ScalarFromValue(ScalarProcessor);
            }
        }

        return GetOrDefineComputedVariable(
            (baseScalar1, scalar1) => MetaExpressionProcessor.Power(baseScalar1, scalar1).ScalarValue,
            baseScalar,
            scalar
        ).ScalarFromValue(ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpressionAtomic> Log(IMetaExpressionAtomic baseScalar, IMetaExpressionAtomic scalar)
    {
        if (ContextOptions.PropagateConstants)
        {
            if (baseScalar is MetaExpressionNumber s1 && scalar is MetaExpressionNumber s2)
            {
                var number = 
                    Math.Log(
                        s2.NumberHeadSpecs.NumberFloat64Value, 
                        s1.NumberHeadSpecs.NumberFloat64Value
                    );
                
                return GetOrDefineLiteralNumber(
                    number, 
                    ContextOptions.Float64Precision
                ).ScalarFromValue(ScalarProcessor);
            }
        }

        return GetOrDefineComputedVariable(
            (baseScalar1, scalar1) => MetaExpressionProcessor.Log(baseScalar1, scalar1).ScalarValue,
            baseScalar,
            scalar
        ).ScalarFromValue(ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpressionAtomic> Cos(IMetaExpressionAtomic scalar)
    {
        if (ContextOptions.PropagateConstants)
        {
            if (scalar is MetaExpressionNumber s1)
            {
                var number = 
                    s1.NumberHeadSpecs.NumberFloat64Value.Cos();
                
                return GetOrDefineLiteralNumber(
                    number, 
                    ContextOptions.Float64Precision
                ).ScalarFromValue(ScalarProcessor);
            }
        }

        return GetOrDefineComputedVariable(
            scalar1 => MetaExpressionProcessor.Cos(scalar1).ScalarValue,
            scalar
        ).ScalarFromValue(ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpressionAtomic> Sin(IMetaExpressionAtomic scalar)
    {
        if (ContextOptions.PropagateConstants)
        {
            if (scalar is MetaExpressionNumber s1)
            {
                var number = 
                    s1.NumberHeadSpecs.NumberFloat64Value.Sin();
                
                return GetOrDefineLiteralNumber(
                    number, 
                    ContextOptions.Float64Precision
                ).ScalarFromValue(ScalarProcessor);
            }
        }

        return GetOrDefineComputedVariable(
            scalar1 => MetaExpressionProcessor.Sin(scalar1).ScalarValue,
            scalar
        ).ScalarFromValue(ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpressionAtomic> Tan(IMetaExpressionAtomic scalar)
    {
        if (ContextOptions.PropagateConstants)
        {
            if (scalar is MetaExpressionNumber s1)
            {
                var number = 
                    s1.NumberHeadSpecs.NumberFloat64Value.Tan();
                
                return GetOrDefineLiteralNumber(
                    number, 
                    ContextOptions.Float64Precision
                ).ScalarFromValue(ScalarProcessor);
            }
        }

        return GetOrDefineComputedVariable(
            scalar1 => MetaExpressionProcessor.Tan(scalar1).ScalarValue,
            scalar
        ).ScalarFromValue(ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpressionAtomic> Cosh(IMetaExpressionAtomic scalar)
    {
        if (ContextOptions.PropagateConstants)
        {
            if (scalar is MetaExpressionNumber s1)
            {
                var number = 
                    s1.NumberHeadSpecs.NumberFloat64Value.Cosh();
                
                return GetOrDefineLiteralNumber(
                    number, 
                    ContextOptions.Float64Precision
                ).ScalarFromValue(ScalarProcessor);
            }
        }

        return GetOrDefineComputedVariable(
            scalar1 => MetaExpressionProcessor.Cosh(scalar1).ScalarValue,
            scalar
        ).ScalarFromValue(ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpressionAtomic> Sinh(IMetaExpressionAtomic scalar)
    {
        if (ContextOptions.PropagateConstants)
        {
            if (scalar is MetaExpressionNumber s1)
            {
                var number = 
                    s1.NumberHeadSpecs.NumberFloat64Value.Sinh();
                
                return GetOrDefineLiteralNumber(
                    number, 
                    ContextOptions.Float64Precision
                ).ScalarFromValue(ScalarProcessor);
            }
        }

        return GetOrDefineComputedVariable(
            scalar1 => MetaExpressionProcessor.Sinh(scalar1).ScalarValue,
            scalar
        ).ScalarFromValue(ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpressionAtomic> Tanh(IMetaExpressionAtomic scalar)
    {
        if (ContextOptions.PropagateConstants)
        {
            if (scalar is MetaExpressionNumber s1)
            {
                var number = 
                    s1.NumberHeadSpecs.NumberFloat64Value.Tanh();
                
                return GetOrDefineLiteralNumber(
                    number, 
                    ContextOptions.Float64Precision
                ).ScalarFromValue(ScalarProcessor);
            }
        }

        return GetOrDefineComputedVariable(
            scalar1 => MetaExpressionProcessor.Tanh(scalar1).ScalarValue,
            scalar
        ).ScalarFromValue(ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid(IMetaExpressionAtomic scalar)
    {
        return scalar is not null;

        //return MetaExpressionProcessor.IsValid(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double ToFloat64(IMetaExpressionAtomic scalar)
    {
        return MetaExpressionProcessor.ToFloat64(scalar);
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsFiniteNumber(IMetaExpressionAtomic scalar)
    //{
    //    return MetaExpressionProcessor.IsFiniteNumber(scalar);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsZero(IMetaExpressionAtomic scalar)
    //{
    //    return MetaExpressionProcessor.IsZero(scalar);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsZero(IMetaExpressionAtomic scalar, bool nearZeroFlag)
    //{
    //    return MetaExpressionProcessor.IsZero(scalar, nearZeroFlag);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNearZero(IMetaExpressionAtomic scalar)
    //{
    //    return MetaExpressionProcessor.IsNearZero(scalar);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotZero(IMetaExpressionAtomic scalar)
    //{
    //    return MetaExpressionProcessor.IsNotZero(scalar);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotZero(IMetaExpressionAtomic scalar, bool nearZeroFlag)
    //{
    //    return MetaExpressionProcessor.IsNotZero(scalar, nearZeroFlag);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNearZero(IMetaExpressionAtomic scalar)
    //{
    //    return MetaExpressionProcessor.IsNotNearZero(scalar);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsPositive(IMetaExpressionAtomic scalar)
    //{
    //    return MetaExpressionProcessor.IsPositive(scalar);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNegative(IMetaExpressionAtomic scalar)
    //{
    //    return MetaExpressionProcessor.IsNegative(scalar);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotPositive(IMetaExpressionAtomic scalar)
    //{
    //    return MetaExpressionProcessor.IsNotPositive(scalar);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNegative(IMetaExpressionAtomic scalar)
    //{
    //    return MetaExpressionProcessor.IsNotNegative(scalar);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNearPositive(IMetaExpressionAtomic scalar)
    //{
    //    return MetaExpressionProcessor.IsNotNearPositive(scalar);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNearNegative(IMetaExpressionAtomic scalar)
    //{
    //    return MetaExpressionProcessor.IsNotNearNegative(scalar);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNearZero(double scalar)
    //{
    //    return scalar > -ZeroEpsilon && scalar < ZeroEpsilon;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsPositive(double scalar)
    //{
    //    return scalar > 0;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNegative(double scalar)
    //{
    //    return scalar < 0;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNearPositive(double scalar)
    //{
    //    return scalar < -ZeroEpsilon;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNearNegative(double scalar)
    //{
    //    return scalar > ZeroEpsilon;
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpressionAtomic> ScalarFromText(string text)
    {
        return MetaExpressionProcessor.ScalarFromText(text).MapScalar(
            s => (IMetaExpressionAtomic) s, ScalarProcessor
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpressionAtomic> ScalarFromNumber(int value)
    {
        return GetOrDefineLiteralNumber(value).ScalarFromValue(ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpressionAtomic> ScalarFromNumber(uint value)
    {
        return GetOrDefineLiteralNumber(value).ScalarFromValue(ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpressionAtomic> ScalarFromNumber(long value)
    {
        return GetOrDefineLiteralNumber(value).ScalarFromValue(ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpressionAtomic> ScalarFromNumber(ulong value)
    {
        return GetOrDefineLiteralNumber(value).ScalarFromValue(ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpressionAtomic> ScalarFromNumber(float value)
    {
        return GetOrDefineLiteralNumber(value).ScalarFromValue(ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpressionAtomic> ScalarFromNumber(double value)
    {
        return GetOrDefineLiteralNumber(value).ScalarFromValue(ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpressionAtomic> ScalarFromRational(long numerator, long denominator)
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpressionAtomic> ScalarFromRandom(Random randomGenerator, double minValue, double maxValue)
    {
        var value = 
            minValue + (maxValue - minValue) * randomGenerator.NextDouble();

        return GetOrDefineLiteralNumber(value).ScalarFromValue(ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToText(IMetaExpressionAtomic scalar)
    {
        return scalar?.ToString() ?? "<null>";
    }


    public void ClearDependencyData()
    {
        foreach (var number in _numbersDictionary.Values)
            number.ClearDependencyData();

        foreach (var parameterVariable in _parametersVariablesDictionary.Values)
            parameterVariable.ClearDependencyData();

        foreach (var computedVariable in _computedVariablesDictionary.Values)
            computedVariable.ClearDependencyData();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ResetComputedVariables(IEnumerable<IMetaExpressionVariableComputed> computedVariablesList)
    {
        _computedVariablesDictionary.Clear();

        foreach (var computedVariable in computedVariablesList)
        {
            _computedVariablesDictionary.Add(
                computedVariable.InternalName,
                computedVariable
            );
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SimplifyRhsExpressions()
    {
        //var expressionEvaluator = SymbolicEvaluator;

        foreach (var computedVariable in GetComputedVariables())
        {
            var replaceDictionary = new Dictionary<string, IMetaExpression>();

            // Replace RHS computed variables with their assigned RHS expressions for some
            // cases like constants
            foreach (var rhsVariable in computedVariable.RhsComputedVariables)
            {
                var rhsVariableExpression = rhsVariable.RhsExpression;

                if (rhsVariableExpression is IMetaExpressionNumber)
                    replaceDictionary.Add(rhsVariable.InternalName, rhsVariableExpression);
            }

            foreach (var (variableName, newExpression) in replaceDictionary)
                computedVariable.ReplaceRhsVariable(variableName, newExpression);

            // Apply symbolic simplification to the RHS expression
            computedVariable.SimplifyRhsExpression();

            //computedVariable.ResetRhsExpression(
            //    expressionEvaluator.Simplify(computedVariable.RhsExpression)
            //);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void EnhanceRhsExpressions()
    {
        foreach (var ctxVar in GetComputedVariables())
            ctxVar.EnhanceRhsExpression();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MetaContext OptimizeContext()
    {
        SimplifyRhsExpressions();
            
        var inputsWithTestValues =
            GetParameterVariables()
                .ToDictionary(
                    binding => binding.InternalName,
                    binding => binding
                );

        //Optimize low-level macro computations
        MetaContextOptimizer.Process(this, inputsWithTestValues);

        return this;
    }

    /// <summary>
    /// Use an evolutionary algorithm to optimize the code in this context 
    /// </summary>
    /// <param name="optParameters"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MetaContext OptimizeContext(McGOptParameters optParameters)
    {
        OptimizeContext();

        return MetaContextGeneticOptimizer.Process(optParameters, this);
    }

    public SortedSet<int> OptimizeContextLoop(int costThreshold)
    {
        var indexSet = new SortedSet<int>();

        var computationsCount = ComputationsCount;
        Console.WriteLine($"Original Cost = {computationsCount}");
        Console.WriteLine();

        //var minCost = computationsCount;
        //var minCostContext = this;

        var intermediateVars = 
            GetIntermediateVariablesList();

        var n = intermediateVars.Count;
        for (var i = 0; i < n; i++)
        {
            //var intermediateVar = intermediateVars[i];
            var newContext = GetContextCopy();

            var intermediateVarIndexSet = 
                intermediateVars.GetIntermediateDependencyIndexSet(i, 2);

            
            newContext.RemoveIntermediateVariables(intermediateVarIndexSet);

            newContext.OptimizeContext();

            var newComputationsCount = newContext.ComputationsCount;

            if (computationsCount - newComputationsCount >= costThreshold)
            {
                indexSet.Add(i);

                Console.WriteLine($"   Removing Intermediate Variable {i,3} of {n,3}: Cost = {newComputationsCount, 3}, Fitness = {computationsCount - newComputationsCount, 3}");
            }

            //if (newComputationsCount < minCost)
            //{
            //    minCost = newComputationsCount;
            //    minCostContext = newContext;

                
            //}
        }

        return indexSet;
    }

    /// <summary>
    /// Try new Genetic Algorithm optimization method
    /// </summary>
    /// <returns></returns>
    public MetaContext OptimizeContextGenetic()
    {
        Console.WriteLine($"Original Cost = {ComputationsCount}");
        Console.WriteLine();

        var fitness = new McOptGaFitness(this);

        var chromosome = new McOptGaChromosome(fitness.IntermediateVariableCount);

        var population = new Population(4, 8, chromosome);

        var selection = new TournamentSelection();
        var crossover = new OnePointCrossover();
        var mutation = new FlipBitMutation();
        var termination = new FitnessStagnationTermination(100);

        var ga = new GeneticAlgorithm(
            population,
            fitness,
            selection,
            crossover,
            mutation)
        {
            Termination = termination, 
            //MutationProbability = 0.5f
        };

        //Console.WriteLine("Generation: (x1, y1), (x2, y2) = distance");

        var latestFitness = 0d;

        ga.GenerationRan += (sender, e) =>
        {
            var bestChromosome = (McOptGaChromosome)ga.BestChromosome;
                
            Debug.Assert(bestChromosome.Fitness != null, "bestChromosome.Fitness != null");
                
            var bestFitness = bestChromosome.Fitness.Value;
                
            if (bestFitness == latestFitness) return;

            latestFitness = bestFitness;
            var bestContext = fitness.GetMetaContext(bestChromosome);

            Console.WriteLine(
                "Generation {0,4}: Cost = {1}, Fitness = {2}",
                ga.GenerationsNumber,
                bestContext.ComputationsCount,
                bestFitness
            );
        };

        ga.Start();

        return fitness.GetMetaContext((McOptGaChromosome)ga.BestChromosome);
    }

        
    /// <summary>
    /// The statistics related to the variables and computations in this block
    /// </summary>
    public Dictionary<string, string> GetStatistics()
    {
        var stats = new Dictionary<string, string>();

        var inputVarsTotalCount = GetParameterVariables().Count();
        var inputVarsUsedCount = GetParameterVariables().Count(inputVar => inputVar.HasDependingVariables);
        var inputVarsUnUsedCount = inputVarsTotalCount - inputVarsUsedCount;

        stats.Add("Used Input Variables: ", inputVarsUsedCount.ToString());
        stats.Add("Unused Input Variables: ", inputVarsUnUsedCount.ToString());
        stats.Add("Total Input Variables: ", inputVarsTotalCount.ToString());

        var tempVarsCount = GetIntermediateVariables().Count();
        var tempVarsSubExprCount = GetIntermediateVariables().Count(item => item.IsFactoredSubExpression);
        var tempVarsNonSubExprCount = tempVarsCount - tempVarsSubExprCount;

        stats.Add("Common Subexpressions Temp Variables: ", tempVarsSubExprCount.ToString());
        stats.Add("Generated Temp Variables: ", tempVarsNonSubExprCount.ToString());
        stats.Add("Total Temp Variables: ", tempVarsCount.ToString());

        stats.Add("Total Output Variables: ", GetOutputVariables().Count().ToString());

        stats.Add("Total Computed Variables: ", GetComputedVariables().Count().ToString());

        stats.Add("Target Temp Variables: ", GetTargetTempVarsCount().ToString());

        var computationsCountTotal =
            GetComputedVariables()
                .Select(computedVar => computedVar.RhsExpression.ComputationsCount)
                .Sum();

        var computationsCountAverage =
            computationsCountTotal / (double) GetComputedVariables().Count();

        stats.Add("Avg. Computations Count: ", computationsCountAverage.ToString("0.000"));
        stats.Add("Total Computations Count: ", computationsCountTotal.ToString());

        var memReadsCountTotal =
            GetComputedVariables()
                .Select(computedVar => computedVar.RhsVariables.Count())
                .Sum();

        var memReadsCountAverage =
            memReadsCountTotal / (double) GetComputedVariables().Count();

        stats.Add("Avg. Memory Reads: ", memReadsCountAverage.ToString("0.000"));
        stats.Add("Total Memory Reads: ", memReadsCountTotal.ToString());

        return stats;
    }

    /// <summary>
    /// The statistics related to the variables and computations in this block as a single string
    /// </summary>
    /// <returns></returns>
    public string GetStatisticsReport()
    {
        var s = new StringBuilder();

        var inputVarsTotalCount = GetParameterVariables().Count();
        var inputVarsUsedCount = GetParameterVariables().Count(inputVar => inputVar.HasDependingVariables);
        var inputVarsUnUsedCount = inputVarsTotalCount - inputVarsUsedCount;

        s.Append("Input Variables: ")
            .Append(inputVarsUsedCount)
            .Append(" used, ")
            .Append(inputVarsUnUsedCount)
            .Append(" not used, ")
            .Append(inputVarsTotalCount)
            .AppendLine(" total.")
            .AppendLine();

        var tempVarsCount = GetIntermediateVariables().Count();
        var tempVarsSubExprCount = GetIntermediateVariables().Count(item => item.IsFactoredSubExpression);
        var tempVarsNonSubExprCount = tempVarsCount - tempVarsSubExprCount;

        s.Append("Temp Variables: ")
            .Append(tempVarsSubExprCount)
            .Append(" sub-expressions, ")
            .Append(tempVarsNonSubExprCount)
            .Append(" generated temps, ")
            .Append(tempVarsCount)
            .AppendLine(" total.")
            .AppendLine();

        if (GetTargetTempVarsCount() > 0)
            s.Append("Target Temp Variables: ")
                .Append(GetTargetTempVarsCount())
                .AppendLine(" total.")
                .AppendLine();

        var outputVarsCount = GetOutputVariables().Count();

        s.Append("Output Variables: ")
            .Append(outputVarsCount)
            .AppendLine(" total.")
            .AppendLine();

        var computationsCountTotal =
            GetComputedVariables()
                .Select(computedVar => computedVar.RhsExpression.ComputationsCount)
                .Sum();
            
        var computationsCountAverage = 
            computationsCountTotal / (double) GetComputedVariables().Count();

        s.Append("Computations: ")
            .Append(computationsCountAverage)
            .Append(" average, ")
            .Append(computationsCountTotal)
            .AppendLine(" total.")
            .AppendLine();

        var memReadsCountTotal =
            GetComputedVariables()
                .Select(computedVar => computedVar.RhsVariables.Count())
                .Sum();

        var memReadsCountAverage =
            memReadsCountTotal / (double) GetComputedVariables().Count();

        s.Append("Memory Reads: ")
            .Append(memReadsCountAverage)
            .Append(" average, ")
            .Append(memReadsCountTotal)
            .AppendLine(" total.")
            .AppendLine();

        s.Append("Memory Writes: ")
            .Append(GetComputedVariables().Count())
            .AppendLine(" total.")
            .AppendLine();

        return s.ToString();
    }

    public override string ToString()
    {
        var composer = new StringBuilder();

        composer.AppendLine(
            GetAtomics()
                .Select(scalar => scalar.GetTextDescription())
                .Concatenate(Environment.NewLine)
        );

        return composer.ToString();
    }
}