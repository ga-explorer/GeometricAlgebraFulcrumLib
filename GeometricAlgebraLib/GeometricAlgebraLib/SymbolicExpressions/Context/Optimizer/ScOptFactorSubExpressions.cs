using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraLib.SymbolicExpressions.Variables;

namespace GeometricAlgebraLib.SymbolicExpressions.Context.Optimizer
{
    internal sealed class ScOptFactorSubExpressions : 
        SymbolicContextProcessorBase
    {
        internal static void Process(SymbolicContext context)
        {
            var factoredSubExpressions = new ScOptFactorSubExpressions(context);

            factoredSubExpressions.BeginProcessing();
        }


        private readonly Dictionary<string, ISymbolicVariableComputed> _subExpressionsTextDictionary = 
            new Dictionary<string, ISymbolicVariableComputed>();

        private List<ISymbolicVariableComputed> _computedVariablesList =
            new List<ISymbolicVariableComputed>();


        private ScOptFactorSubExpressions(SymbolicContext context)
            : base(context)
        {
        }


        private void AddTempVariable(string rhsExprText, ISymbolicVariableComputed varInfo)
        {
            varInfo.SetComputationOrder(_computedVariablesList.Count);

            _subExpressionsTextDictionary.Add(rhsExprText, varInfo);

            _computedVariablesList.Add(varInfo);
        }

        private void AddVariable(ISymbolicVariableComputed varInfo)
        {
            varInfo.SetComputationOrder(_computedVariablesList.Count);

            _computedVariablesList.Add(varInfo);
        }

        private void AddRhsExpression(ISymbolicVariableComputed computedVariable)
        {
            var rhsExpression = computedVariable.RhsExpression;
            var rhsExpressionText = rhsExpression.ToString();

            if (computedVariable.IsIntermediateVariable)
            {
                //If this whole expression is already assigned to a temp variable,
                //add the temp to the output.
                AddTempVariable(rhsExpressionText, computedVariable);

                return;
            }

            //If this whole expression is instead assigned to an output variable,
            //create a factored expression temp variable and add it to the output
            //with use count 1 and add the original output variable to the output
            if (!rhsExpression.IsAtomic && !_subExpressionsTextDictionary.ContainsKey(rhsExpressionText))
            {
                var subExpressionVariable = 
                    Context.DefineSubExpressionVariable(
                        rhsExpression, 
                        true
                    );

                AddTempVariable(rhsExpressionText, subExpressionVariable);
            }

            AddVariable(computedVariable);
        }

        private void AddSubExpressions(ISymbolicVariableComputed computedVar)
        {
            var subExpressionsList = 
                computedVar.RhsExpression.SubExpressions;

            foreach (var subExpression in subExpressionsList)
            {
                var subExpressionText = subExpression.ToString();

                if (_subExpressionsTextDictionary.TryGetValue(subExpressionText, out var subExpressionVariable))
                {
                    subExpressionVariable.SubExpressionUseCount++;
                }
                else
                {
                    subExpressionVariable = 
                        Context.DefineSubExpressionVariable(
                            subExpression, 
                            false
                        );

                    AddTempVariable(subExpressionText, subExpressionVariable);
                }
            }

            AddRhsExpression(computedVar);
        }

        /// <summary>
        /// True if this computed variable is relevant. A relevant computed variable is
        /// either an output variable, an intermediate non-factored sub-expression, or
        /// a factored, non-constant, and not a single variable sub-expression with more
        /// than one use in following computed variables
        /// </summary>
        /// <param name="computedVariable"></param>
        /// <returns></returns>
        private bool IsRelevantComputedVariable(ISymbolicVariableComputed computedVariable)
        {
            if (computedVariable.IsOutputVariable || !computedVariable.IsFactoredSubExpression)
                return true;

            ////This condition produces simplest rhs expressions per computation but take a lot of time
            ////during sub-expression substitution
            //if (!computedVariable.RhsExpression.IsAtomic)
            //    return true;

            //This condition produces arbitrarily complex rhs expressions per computation but take much less time
            //during sub-expression substitution
            if (computedVariable.SubExpressionUseCount > 1 && !computedVariable.RhsExpression.IsAtomic)
                return true;

            //var computationsCount = computedVariable.RhsExpr.ComputationsCount();

            //if (computationsCount >= 5 && computationsCount <= 15)
            //    return true;

            return false;
        }

        private void SelectRelevantComputedVariables()
        {
            var finalList = new List<ISymbolicVariableComputed>(_computedVariablesList.Count);

            foreach (var computedVar in _computedVariablesList.Where(IsRelevantComputedVariable))
            {
                computedVar.SetComputationOrder(finalList.Count);
                finalList.Add(computedVar);
            }

            _computedVariablesList = finalList;
        }

        private void PropagateSubExpressions()
        {
            var factoredSubExpressionsList =
                _computedVariablesList
                    .Where(computedVar => computedVar.IsIntermediateVariable && computedVar.IsFactoredSubExpression)
                    .ToArray();

            foreach (var tempVar in factoredSubExpressionsList)
                for (var j = tempVar.ComputationOrder + 1; j < _computedVariablesList.Count; j++)
                    _computedVariablesList[j].ReplaceRhsExpression(
                        tempVar.RhsExpression, 
                        tempVar.InternalName
                    );
        }

        protected override void BeginProcessing()
        {
            //Add all RHS expressions and subexpressions of all computed variables to a list
            foreach (var computedVar in Context.ComputedVariables)
                AddSubExpressions(computedVar);

            //Select which temp variables to add to the final computations list
            SelectRelevantComputedVariables();

            //Replace sub-expressions used multiple times in computations by their temp variables
            PropagateSubExpressions();

            //Update code block
            Context.ResetComputedVariables(_computedVariablesList);

            ScOptDependencyUpdate.Process(Context);
        }
    }
}
