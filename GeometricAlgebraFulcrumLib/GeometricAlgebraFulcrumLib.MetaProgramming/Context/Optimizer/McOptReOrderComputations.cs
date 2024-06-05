using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Variables;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Optimizer;

internal sealed class McOptReOrderComputations : 
    MetaContextProcessorBase
{
    internal static void Process(MetaContext context)
    {
        var processor = new McOptReOrderComputations(context);

        processor.BeginProcessing();
    }


    public bool FixOutputComputationsOrder 
        => Context.ContextOptions.FixOutputComputationsOrder;

    private readonly Dictionary<string, IMetaExpressionVariableComputed> _computedVariablesDictionary = 
        new Dictionary<string, IMetaExpressionVariableComputed>();


    private McOptReOrderComputations(MetaContext context)
        : base(context)
    {
    }


    private void AddOutputVariable(IMetaExpressionVariableComputed outputVar)
    {
        if (outputVar.RhsVariables.Any(v => v.IsIntermediateVariable))
        {
            var rhsTemVarsList =
                outputVar
                    .GetUsedIntermediateVariables()
                    .Where(
                        rhsTempVar =>
                            _computedVariablesDictionary.ContainsKey(rhsTempVar.InternalName) == false
                    );

            foreach (var rhsTempVar in rhsTemVarsList)
                _computedVariablesDictionary.Add(rhsTempVar.InternalName, rhsTempVar);
        }

        _computedVariablesDictionary.Add(outputVar.InternalName, outputVar);
    }

    protected override void BeginProcessing()
    {
        var outputVariablesList = 
            FixOutputComputationsOrder
                ? Context.GetOutputVariables()
                    .OrderBy(item => item.AtomicExpressionId)
                : Context.GetOutputVariables()
                    .OrderBy(item => item.MaxComputationLevel)
                    .ThenBy(item => item.AtomicExpressionId);

        foreach (var outputVariable in outputVariablesList)
            AddOutputVariable(outputVariable);

        Context.ResetComputedVariables(
            _computedVariablesDictionary.Values
        );

        //Update ComputationOrder property for each computed variable in the block
        var i = 0;
        foreach (var computedVariable in Context.GetComputedVariables())
            computedVariable.SetComputationOrder(i++);
    }
}