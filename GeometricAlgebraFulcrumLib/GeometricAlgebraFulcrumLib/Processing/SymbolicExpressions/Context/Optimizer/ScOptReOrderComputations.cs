using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Variables;

namespace GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context.Optimizer
{
    internal sealed class ScOptReOrderComputations : 
        SymbolicContextProcessorBase
    {
        internal static void Process(SymbolicContext context)
        {
            var processor = new ScOptReOrderComputations(context);

            processor.BeginProcessing();
        }


        public bool OrderOutputsById 
            => Context.ContextOptions.FixOutputComputationsOrder;

        private readonly Dictionary<string, ISymbolicVariableComputed> _computedVariablesDictionary = 
            new Dictionary<string, ISymbolicVariableComputed>();


        private ScOptReOrderComputations(SymbolicContext context)
            : base(context)
        {
        }


        private void AddOutputVariable(ISymbolicVariableComputed outputVar)
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
            var outputVariablesList = OrderOutputsById
                ? Context
                    .OutputVariables
                    .OrderBy(item => item.AtomicExpressionId)
                : Context
                    .OutputVariables
                    .OrderBy(item => item.MaxComputationLevel)
                    .ThenBy(item => item.AtomicExpressionId);

            foreach (var outputVariable in outputVariablesList)
                AddOutputVariable(outputVariable);

            Context.ResetComputedVariables(
                _computedVariablesDictionary.Values
            );

            //Update ComputationOrder property for each computed variable in the block
            var i = 0;
            foreach (var computedVariable in Context.ComputedVariables)
                computedVariable.SetComputationOrder(i++);
        }
    }
}
