using System.Linq;

namespace GeometricAlgebraLib.Processing.SymbolicExpressions.Context.Optimizer
{
    internal sealed class ScOptDependencyUpdate : 
        SymbolicContextProcessorBase
    {
        internal static void Process(SymbolicContext context)
        {
            var processor = new ScOptDependencyUpdate(context);

            processor.BeginProcessing();
        }


        private ScOptDependencyUpdate(SymbolicContext context)
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
            foreach (var computedVar in Context.ComputedVariables)
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
            foreach (var computedVariable in Context.ComputedVariables)
                computedVariable.SetComputationOrder(i++);
        }
    }
}
