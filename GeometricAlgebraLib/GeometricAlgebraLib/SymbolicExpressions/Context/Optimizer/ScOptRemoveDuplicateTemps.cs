using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraLib.SymbolicExpressions.Variables;

namespace GeometricAlgebraLib.SymbolicExpressions.Context.Optimizer
{
    internal sealed class ScOptRemoveDuplicateTemps : 
        SymbolicContextProcessorBase
    {
        internal static void Process(SymbolicContext context)
        {
            var processor = new ScOptRemoveDuplicateTemps(context);

            processor.BeginProcessing();
        }


        private readonly Dictionary<string, List<ISymbolicVariableComputed>> _rhsExprUseDictionary = 
            new Dictionary<string, List<ISymbolicVariableComputed>>();

        
        private ScOptRemoveDuplicateTemps(SymbolicContext context)
            : base(context)
        {
        }


        private void AddRhsExprUse(ISymbolicVariableComputed tempVar)
        {
            var rhsExprText = tempVar.RhsExpressionText;

            if (_rhsExprUseDictionary.TryGetValue(rhsExprText, out var rhsExprUseList) == false)
            {
                rhsExprUseList = new List<ISymbolicVariableComputed>();

                _rhsExprUseDictionary.Add(rhsExprText, rhsExprUseList);
            }

            rhsExprUseList.Add(tempVar);
        }

        protected override void BeginProcessing()
        {
            //Fill dictionary of RHS expressions uses
            foreach (var tempVar in Context.IntermediateVariables)
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
                ScOptDependencyUpdate.Process(Context);
        }
    }
}
