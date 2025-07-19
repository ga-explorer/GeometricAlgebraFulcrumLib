using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.HeadSpecs;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Numbers;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Variables;

public sealed class MetaExpressionVariableComputed :
    MetaExpressionVariableBase,
    IMetaExpressionVariableComputed
{
    public static MetaExpressionVariableComputed Create(MetaContext context, MetaExpressionHeadSpecsVariable headSpecs, IMetaExpression rhsExpression)
    {
        return new MetaExpressionVariableComputed(
            context,
            headSpecs,
            rhsExpression
        );
    }

    public static MetaExpressionVariableComputed Create(MetaContext context, string variableName, IMetaExpression rhsExpression)
    {
        return new MetaExpressionVariableComputed(
            context,
            MetaExpressionHeadSpecsVariable.Create(variableName),
            rhsExpression
        );
    }

    public static MetaExpressionVariableComputed CreateFactoredSubExpression(MetaContext context, string variableName, IMetaExpression rhsExpression, bool isUsedOnce)
    {
        return new MetaExpressionVariableComputed(
            context,
            MetaExpressionHeadSpecsVariable.Create(variableName),
            rhsExpression
        )
        {
            IsFactoredSubExpression = true,
            SubExpressionUseCount = isUsedOnce ? 1 : 0
        };
    }


    private IMetaExpression _rhsExpression;
    public override IMetaExpression RhsExpression
        => _rhsExpression;

    public override string RhsExpressionText
        => Context.MetaExpressionProcessor.ToText(RhsExpression);

    public override bool IsParameterVariable
        => false;

    public override bool IsComputedVariableOrComposite
        => true;

    public override bool IsIntermediateVariable
        => !IsOutputVariable;

    private bool _mergeEnabled = true;
    public override bool MergeEnabled 
        => _mergeEnabled;

    private bool _isOutputVariable;
    public override bool IsOutputVariable
        => _isOutputVariable;

    public override bool IsNumberOrParameter
        => false;

    public override bool IsComputedVariable
        => true;

    private int _maxComputationLevel;
    public override int MaxComputationLevel
        => _maxComputationLevel;

    public IEnumerable<IMetaExpressionAtomic> RhsAtomicExpressions
        => RhsExpression
            .AtomicExpressions
            .Distinct();

    public IEnumerable<IMetaExpressionAtomic> RhsNumbersAndParameters
        => RhsExpression
            .AtomicExpressions
            .Where(expr => expr.IsNumberOrParameter)
            .Distinct();

    public IEnumerable<IMetaExpressionNumber> RhsNumbers
        => RhsExpression
            .NumberExpressions
            .Distinct();

    public IEnumerable<IMetaExpressionVariable> RhsVariables
        => RhsExpression
            .VariableExpressions
            .Distinct();

    public HashSet<IMetaExpressionAtomic> RhsAtomicsCache { get; }
        = new HashSet<IMetaExpressionAtomic>();

    public IEnumerable<IMetaExpressionVariableParameter> RhsParameterVariables
        => RhsExpression
            .VariableParameterExpressions
            .Distinct();

    public IEnumerable<IMetaExpressionVariableComputed> RhsComputedVariables
        => RhsExpression
            .VariableComputedExpressions
            .Distinct();

    public IEnumerable<IMetaExpressionVariableComputed> RhsIntermediateVariables
        => RhsExpression
            .VariableComputedExpressions
            .Where(v => v.IsIntermediateVariable)
            .Distinct();

    public IEnumerable<IMetaExpressionVariableComputed> RhsOutputVariables
        => RhsExpression
            .VariableComputedExpressions
            .Where(v => v.IsOutputVariable)
            .Distinct();

    public int ComputationOrder { get; private set; }

    public override IEnumerable<IMetaExpressionVariableParameter> VariableParameterExpressions
        => Enumerable.Empty<IMetaExpressionVariableParameter>();

    public override IEnumerable<IMetaExpressionVariableComputed> VariableComputedExpressions
    {
        get { yield return this; }
    }

    public override bool HasDependingVariables
        => DependingVariablesCache.Count > 0;

    public override bool IsOutputOrHasDependingVariables
        => IsOutputVariable || DependingVariablesCache.Count > 0;

    private string _outputExternalName = string.Empty;
    public string OutputExternalName
        => string.IsNullOrEmpty(_outputExternalName)
            ? string.Empty
            : _outputExternalName;

    public bool IsReused { get; private set; }

    public int NameIndex { get; private set; } = -1;

    public bool IsFactoredSubExpression { get; private set; }

    public bool IsAffineCombination
        => Context.SymbolicEvaluator.IsAffineCombination(RhsExpression);

    public int SubExpressionUseCount { get; set; }


    private MetaExpressionVariableComputed(MetaContext context, MetaExpressionHeadSpecsVariable headSpecs, IMetaExpression rhsExpression)
        : base(context, headSpecs)
    {
        ComputationOrder = AtomicExpressionId;

        SetRhsExpression(rhsExpression);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IMetaExpression CopyToContext(MetaContext context)
    {
        return context.ImportCopy(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IMetaExpression GetScalarValue(bool useRhsScalarValue)
    {
        SimplifyRhsExpression();

        //if (RhsExpression.IsNumberOrParameter)
        //    return RhsExpression;
        
        if (RhsVariables.Count() <= 1)
            return RhsExpression;

        if (!useRhsScalarValue) 
            return this;

        if (MergeEnabled)
            return RhsExpression;

        //if (RhsVariables.Count() <= 2)
        //    return RhsExpression;
        
        //if (MergeEnabled && RhsExpression.ComputationsCount <= 4)
        //    return RhsExpression;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetReuseInfo(bool isReused, int nameIndex)
    {
        IsReused = isReused;
        NameIndex = nameIndex;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetComputationOrder(int computationOrder)
    {
        ComputationOrder = computationOrder;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int UpdateMaxComputationLevel()
    {
        _maxComputationLevel =
            RhsAtomicsCache.Count == 0
                ? 0
                : RhsAtomicsCache.Max(item =>
                    item.MaxComputationLevel
                ) + 1;

        return _maxComputationLevel;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void ClearDependencyData()
    {
        DependingVariablesCache.Clear();

        RhsAtomicsCache.Clear();
    }

    /// <summary>
    /// Replace a given variable in the RHS expression of this computed variable by another 
    /// variable
    /// </summary>
    /// <param name="oldRhsExpression"></param>
    /// <param name="newVariableName"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ReplaceRhsExpression(IMetaExpression oldRhsExpression, string newVariableName)
    {
        var (isReplaced, newRhsExpression) =
            RhsExpression.ReplaceAllExpressionByVariable(oldRhsExpression, newVariableName, Context);

        if (isReplaced)
            SetRhsExpression(newRhsExpression);

        return isReplaced;
    }

    /// <summary>
    /// Replace a given variable in the RHS expression of this computed variable by another 
    /// variable
    /// </summary>
    /// <param name="oldVariableName"></param>
    /// <param name="newVariableName"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ReplaceRhsVariable(string oldVariableName, string newVariableName)
    {
        var (isReplaced, newRhsExpression) =
            RhsExpression.ReplaceAllVariableByVariable(oldVariableName, newVariableName, Context);

        if (isReplaced)
            SetRhsExpression(newRhsExpression);

        return isReplaced;
    }

    /// <summary>
    /// Replace a given variable in the RHS expression of this computed variable by another 
    /// variable
    /// </summary>
    /// <param name="oldVariableName"></param>
    /// <param name="newExpr"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ReplaceRhsVariable(string oldVariableName, IMetaExpression newExpr)
    {
        var oldExpr = Context.GetVariable(oldVariableName);

        var (isReplaced, newRhsExpression) =
            RhsExpression.ReplaceAllExpressionByExpression(oldExpr, newExpr);

        if (isReplaced)
            SetRhsExpression(newRhsExpression);

        return isReplaced;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void EnhanceRhsExpression()
    {
        return;

        // TODO: This sometimes gives wrong expressions, needs reviewing
        SetRhsExpression(
            Context.SymbolicEvaluator.Enhance(RhsExpression)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SimplifyRhsExpression()
    {
        SetRhsExpression(
            Context.SymbolicEvaluator.Simplify(RhsExpression)
        );
    }

    /// <summary>
    /// Set the RHS expression to the given expression
    /// </summary>
    /// <param name="newRhsExpression"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetRhsExpression(IMetaExpression newRhsExpression)
    {
        _rhsExpression = newRhsExpression;

        //Debug.Assert(Context.IsValidExpression(RhsExpression));
    }

    public void SetStateFrom(IMetaExpressionVariableComputed variable)
    {
        //SetRhsExpression(
        //    variable.RhsExpression.CopyToContext(Context)
        //);

        _mergeEnabled = variable.MergeEnabled;
        _isOutputVariable = variable.IsOutputVariable;
        _externalName = variable.ExternalName;
        _outputExternalName = variable.OutputExternalName;
        _maxComputationLevel = variable.MaxComputationLevel;
        RhsExpressionValue = variable.RhsExpressionValue;
        ComputationOrder = variable.ComputationOrder;
        IsReused = variable.IsReused;
        NameIndex = variable.NameIndex;
        IsFactoredSubExpression = variable.IsFactoredSubExpression;
        SubExpressionUseCount = variable.SubExpressionUseCount;
    }

    /// <summary>
    /// Get an ordered list of all required input variables and intermediate variables
    /// necessary for this computed variable's RHS computation in whole the code block
    /// </summary>
    /// <returns></returns>
    public IEnumerable<IMetaExpressionVariable> GetUsedVariables()
    {
        //The final list containing the ordered temp variables
        var finalVariablesDictionary = new Dictionary<string, IMetaExpressionVariable>();

        var queue = new GeometricAlgebraFulcrumLib.Utilities.Structures.Collections.Queues.PriorityQueue<int, IMetaExpressionVariableComputed>();

        //Add the intermediate variables in a priority queue according to
        //their computation order in the code block
        foreach (var rhsVariable in RhsVariables)
        {
            if (rhsVariable is IMetaExpressionVariableComputed { IsIntermediateVariable: true } rhsTempVariable)
            {
                queue.Enqueue(
                    -rhsTempVariable.ComputationOrder,
                    rhsTempVariable
                );

                continue;
            }

            finalVariablesDictionary.Add(rhsVariable.InternalName, rhsVariable);
        }

        while (queue.Count > 0)
        {
            //Get the next temp variable from the queue
            var tempVar = queue.Dequeue().Value;

            //If the temp variable already exists in the final list do nothing
            if (finalVariablesDictionary.ContainsKey(tempVar.InternalName))
                continue;

            //Add tempVar to the final list 
            finalVariablesDictionary.Add(tempVar.InternalName, tempVar);

            //Create a list of variables in the RHS of tempVar's expression but not yet
            //present in the final list
            var rhsVariablesList =
                tempVar
                    .RhsVariables
                    .Where(item =>
                        !finalVariablesDictionary.ContainsKey(item.InternalName)
                    );

            //Add the temp variables in the list to the priority queue and the inputs variables to
            //the final result
            foreach (var rhsVar in rhsVariablesList)
            {
                if (rhsVar is IMetaExpressionVariableComputed { IsIntermediateVariable: true } rhsTempVar)
                {
                    queue.Enqueue(
                        -rhsTempVar.ComputationOrder,
                        rhsTempVar
                    );

                    continue;
                }

                finalVariablesDictionary.Add(rhsVar.InternalName, rhsVar);
            }
        }

        //Return the final list after reversing so that the ordering of computations is correct
        return finalVariablesDictionary.Values.Reverse();
    }

    /// <summary>
    /// Get an ordered list of all required parameter variables necessary for this
    /// computed variable's RHS computation in whole the code block
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<IMetaExpressionVariableParameter> GetUsedParameterVariables()
    {
        return GetUsedVariables()
            .Select(item => item as IMetaExpressionVariableParameter)
            .Where(item => item is null == false);
    }

    /// <summary>
    /// Get an ordered list of all required intermediate variables necessary for this
    /// computed variable's RHS computation in whole the code block
    /// </summary>
    public IEnumerable<IMetaExpressionVariableComputed> GetUsedIntermediateVariables()
    {
        //The final list containing the ordered temp variables
        var finalVariablesDictionary = new Dictionary<string, IMetaExpressionVariableComputed>();

        var queue = new GeometricAlgebraFulcrumLib.Utilities.Structures.Collections.Queues.PriorityQueue<int, IMetaExpressionVariableComputed>();

        //Add the temp variables in a priority queue according to their computation order in the
        //code block
        foreach (var rhsTempVar in RhsIntermediateVariables)
            queue.Enqueue(-rhsTempVar.ComputationOrder, rhsTempVar);

        while (queue.Count > 0)
        {
            //Get the next temp variable from the queue
            var tempVar = queue.Dequeue().Value;

            //If the temp variable already exists in the final list do nothing
            if (finalVariablesDictionary.ContainsKey(tempVar.InternalName))
                continue;

            //Add tempVar to the final list 
            finalVariablesDictionary.Add(tempVar.InternalName, tempVar);

            //Create a list of temp variables in the RHS of tempVar's expression not yet
            //present in the final list
            var rhsVariablesList =
                tempVar
                    .RhsIntermediateVariables
                    .Where(item =>
                        !finalVariablesDictionary.ContainsKey(item.InternalName)
                    );

            //Add the list to the priority queue
            foreach (var rhsTempVar in rhsVariablesList)
                queue.Enqueue(
                    -rhsTempVar.ComputationOrder,
                    rhsTempVar
                );
        }

        //Return the final list after reversing so that the ordering of computations is correct
        return finalVariablesDictionary.Values.Reverse();
    }


    public override string GetTextDescription()
    {
        var isUsedText = IsOutputOrHasDependingVariables
            ? "    Used"
            : "Not Used";

        var isOutputText = IsOutputVariable
            ? "Output         "
            : "Intermediate   ";

        return $"{isUsedText} {isOutputText} '{ExternalName}': {VariableHeadSpecs.VariableName} = {RhsExpressionText}";
    }

    public override bool UpdateExternalName(string externalName)
    {
        //Debug.Assert(
        //    externalName != "temp-1"
        //    //string.IsNullOrEmpty(_externalName)
        //);

        var externalNameOld = _externalName;

        if (string.IsNullOrEmpty(externalName))
            _externalName = string.Empty;

        else if (string.IsNullOrEmpty(_externalName))
            _externalName = externalName;

        return _externalName != externalNameOld;
    }
    
    public void DisableMerge()
    {
        if (!_mergeEnabled) return;

        _mergeEnabled = false;

        SimplifyRhsExpression();
        //Console.WriteLine($"[{RhsExpression.ComputationsCount:####}]: {InternalName} = {RhsExpressionText}");
        //Console.WriteLine();
    }

    public bool SetAsOutput(string externalName)
    {
        //Debug.Assert(
        //    //ExternalName != "tmpVar46"
        //    string.IsNullOrEmpty(_externalName)
        //);

        _isOutputVariable = true;

        var externalNameOld = _outputExternalName;

        if (string.IsNullOrEmpty(externalName))
            _outputExternalName = string.Empty;

        else if (string.IsNullOrEmpty(_outputExternalName))
            _outputExternalName = externalName;

        return _outputExternalName != externalNameOld;
    }

}