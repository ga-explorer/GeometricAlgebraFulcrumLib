using System.Linq;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Optimizer;

internal sealed class McOptDependencyUpdate : 
    MetaContextProcessorBase
{
    internal static void Process(MetaContext context)
    {
        var processor = new McOptDependencyUpdate(context);

        processor.BeginProcessing();
    }


    private McOptDependencyUpdate(MetaContext context)
        : base(context)
    {
            
    }

        
    protected override void BeginProcessing()
    {
        //Simplify RHS expressions of all computed variables
        Context.SimplifyRhsExpressions();

        //Clear dependency data of all atomics (numbers or variables) in the context
        Context.ClearDependencyData();

        //Fill dependency lists of all variables
        foreach (var computedVar in Context.GetComputedVariables())
        {
            //Find the numbers and variables used in the RHS expression of this computed variable
            var rhsAtomicsList = 
                computedVar.RhsAtomicExpressions.ToArray();

            //Iterate over all RHS numbers and variables used in this computed variable
            foreach (var rhsAtomic in rhsAtomicsList)
            {
                //Add the RHS number or variable to the cache of RHS atomics of this computed variable
                computedVar.RhsAtomicsCache.Add(rhsAtomic);

                //Add this computed variables to the list of computed variables that depend on the RHS atomic
                rhsAtomic.AddDependingVariable(computedVar);
            }

            //Update the maximum computation level for this computed variable.
            computedVar.UpdateMaxComputationLevel();
        }

        //Remove all atomics not used in computing the outputs of this context
        Context.RemoveNotUsedAtomics();

        //Update ComputationOrder property for each computed variable in the block
        var i = 0;
        foreach (var computedVariable in Context.GetComputedVariables())
            computedVariable.SetComputationOrder(i++);
    }
}