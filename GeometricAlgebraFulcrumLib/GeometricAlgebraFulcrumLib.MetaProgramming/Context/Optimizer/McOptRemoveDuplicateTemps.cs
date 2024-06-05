using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Variables;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Optimizer;

internal sealed class McOptRemoveDuplicateTemps : 
    MetaContextProcessorBase
{
    internal static void Process(MetaContext context)
    {
        var processor = new McOptRemoveDuplicateTemps(context);

        processor.BeginProcessing();
    }


    private readonly Dictionary<string, List<IMetaExpressionVariableComputed>> _rhsExprUseDictionary = 
        new Dictionary<string, List<IMetaExpressionVariableComputed>>();

        
    private McOptRemoveDuplicateTemps(MetaContext context)
        : base(context)
    {
    }


    private void AddRhsExprUse(IMetaExpressionVariableComputed tempVar)
    {
        var rhsExprText = tempVar.RhsExpressionText;

        if (_rhsExprUseDictionary.TryGetValue(rhsExprText, out var rhsExprUseList) == false)
        {
            rhsExprUseList = new List<IMetaExpressionVariableComputed>();

            _rhsExprUseDictionary.Add(rhsExprText, rhsExprUseList);
        }

        rhsExprUseList.Add(tempVar);
    }

    protected override void BeginProcessing()
    {
        //Fill dictionary of RHS expressions uses
        foreach (var tempVar in Context.GetIntermediateVariables())
            AddRhsExprUse(tempVar);

        //Select sub-expressions with multiple uses
        var selectedLists =
            _rhsExprUseDictionary
                .Select(pair => pair.Value)
                .Where(list => list.Count > 1);

        //A list for marking duplicate temp variables to be deleted from code block
        var removedVariableNamesList = new List<string>();

        foreach (var tempVarList in selectedLists)
        {
            //The temp variable to keep
            var tempVarNew = tempVarList[0];

            //Iterate over all duplicate temp variables to be replaced and removed
            foreach (var tempVarOld in tempVarList.Skip(1))
            {
                //Mark the duplicate for removal
                removedVariableNamesList.Add(tempVarOld.InternalName);

                //Replace the duplicate temp by the temp variable to keep
                foreach (var computedVar in tempVarOld.DependingVariables)
                    computedVar.ReplaceRhsVariable(
                        tempVarOld.InternalName, 
                        tempVarNew.InternalName
                    );
            }
        }

        //Remove all duplicate temps from code block
        Context.RemoveNotUsedComputedVariables(removedVariableNamesList);

        //Update dependency information if needed
        if (removedVariableNamesList.Count > 0)
            McOptDependencyUpdate.Process(Context);
    }
}