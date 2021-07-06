using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib;
using GeometricAlgebraLib.Processing.SymbolicExpressions.Context;
using GeometricAlgebraLib.Processing.SymbolicExpressions.HeadSpecs;
using GeometricAlgebraLib.Processing.SymbolicExpressions.Numbers;

namespace GeometricAlgebraLib.Processing.SymbolicExpressions.Variables
{
    public sealed class SymbolicVariableComputed :
        SymbolicVariableBase, ISymbolicVariableComputed
    {
        public static SymbolicVariableComputed Create(SymbolicHeadSpecsVariable headSpecs, ISymbolicExpression rhsExpression)
        {
            return new SymbolicVariableComputed(
                headSpecs,
                rhsExpression
            );
        }

        public static SymbolicVariableComputed Create(SymbolicContext context, string variableName, ISymbolicExpression rhsExpression)
        {
            return new SymbolicVariableComputed(
                SymbolicHeadSpecsVariable.Create(context, variableName),
                rhsExpression
            );
        }

        public static SymbolicVariableComputed CreateFactoredSubExpression(SymbolicContext context, string variableName, ISymbolicExpression rhsExpression, bool isUsedOnce)
        {
            return new SymbolicVariableComputed(
                SymbolicHeadSpecsVariable.Create(context, variableName),
                rhsExpression
            ) 
            {
                IsFactoredSubExpression = true,
                SubExpressionUseCount = isUsedOnce ? 1 : 0
            };
        }


        private ISymbolicExpression _rhsExpression;
        public override ISymbolicExpression RhsExpression 
            => _rhsExpression;

        public override string RhsExpressionText 
            => Context.SymbolicExpressionProcessor.ToText(RhsExpression);

        public override bool IsParameterVariable 
            => false;

        public override bool IsComputedVariableOrComposite 
            => true;

        public override bool IsIntermediateVariable
        {
            get => !IsOutputVariable;
            set => IsOutputVariable = !value;
        }
        
        public override bool IsOutputVariable { get; set; }

        public override bool IsNumberOrParameter 
            => false;

        public override bool IsComputedVariable 
            => true;

        private int _maxComputationLevel;
        public override int MaxComputationLevel 
            => _maxComputationLevel;

        public override string GetTextDescription()
        {
            var isUsedText = HasDependingVariables
                ? "    Used"
                : "Not Used";

            var isOutputText = IsOutputVariable
                ? "Output         "
                : "Intermediate   ";

            return $"{isUsedText} {isOutputText} '{ExternalName}': {VariableHeadSpecs.VariableName} = {RhsExpressionText}";
        }

        public IEnumerable<ISymbolicExpressionAtomic> RhsAtomicExpressions
            => RhsExpression
                .AtomicExpressions
                .Distinct();
        
        public IEnumerable<ISymbolicExpressionAtomic> RhsNumbersAndParameters
            => RhsExpression
                .AtomicExpressions
                .Where(expr => expr.IsNumberOrParameter)
                .Distinct();
        
        public IEnumerable<ISymbolicNumber> RhsNumbers
            => RhsExpression
                .NumberExpressions
                .Distinct();

        public IEnumerable<ISymbolicVariable> RhsVariables
            => RhsExpression
                .VariableExpressions
                .Distinct();

        public HashSet<ISymbolicExpressionAtomic> RhsAtomicsCache { get; } 
            = new HashSet<ISymbolicExpressionAtomic>();

        public IEnumerable<ISymbolicVariableParameter> RhsParameterVariables
            => RhsExpression
                .VariableParameterExpressions
                .Distinct();
        
        public IEnumerable<ISymbolicVariableComputed> RhsComputedVariables
            => RhsExpression
                .VariableComputedExpressions
                .Distinct();
        
        public IEnumerable<ISymbolicVariableComputed> RhsIntermediateVariables
            => RhsExpression
                .VariableComputedExpressions
                .Where(v => v.IsIntermediateVariable)
                .Distinct();
        
        public IEnumerable<ISymbolicVariableComputed> RhsOutputVariables
            => RhsExpression
                .VariableComputedExpressions
                .Where(v => v.IsOutputVariable)
                .Distinct();

        public int ComputationOrder { get; private set; }

        public override IEnumerable<ISymbolicVariableParameter> VariableParameterExpressions
            => Enumerable.Empty<ISymbolicVariableParameter>();

        public override IEnumerable<ISymbolicVariableComputed> VariableComputedExpressions
        {
            get { yield return this; }
        }

        public override bool HasDependingVariables 
            => IsOutputVariable || DependingVariablesCache.Count > 0;

        public bool IsReused { get; private set; }

        public int NameIndex { get; private set; } = -1;

        public bool IsFactoredSubExpression { get; private init; }

        public int SubExpressionUseCount { get; set; }


        private SymbolicVariableComputed(SymbolicHeadSpecsVariable headSpecs, [NotNull] ISymbolicExpression rhsExpression)
            : base(headSpecs)
        {
            _rhsExpression = rhsExpression;
            ComputationOrder = AtomicExpressionId;
        }


        public override ISymbolicExpression GetExpressionCopy()
        {
            return new SymbolicVariableComputed(
                VariableHeadSpecs,
                RhsExpression.GetExpressionCopy()
            );
        }


        public override ISymbolicExpression GetScalarValue(bool useRhsScalarValue)
        {
            return useRhsScalarValue
                ? RhsExpression 
                : this;
        }

        public void SetReuseInfo(bool isReused, int nameIndex)
        {
            IsReused = isReused;
            NameIndex = nameIndex;
        }

        public void SetComputationOrder(int computationOrder)
        {
            ComputationOrder = computationOrder;
        }

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
        public bool ReplaceRhsExpression(ISymbolicExpression oldRhsExpression, string newVariableName)
        {
            var (isReplaced, newRhsExpression) = 
                _rhsExpression.ReplaceAllExpressionByVariable(oldRhsExpression, newVariableName);

            if (isReplaced)
                _rhsExpression = newRhsExpression;

            return isReplaced;
        }

        /// <summary>
        /// Replace a given variable in the RHS expression of this computed variable by another 
        /// variable
        /// </summary>
        /// <param name="oldVariableName"></param>
        /// <param name="newVariableName"></param>
        public bool ReplaceRhsVariable(string oldVariableName, string newVariableName)
        {
            var (isReplaced, newRhsExpression) = 
                _rhsExpression.ReplaceAllVariableByVariable(oldVariableName, newVariableName);

            if (isReplaced)
                _rhsExpression = newRhsExpression;

            return isReplaced;
        }

        public void SimplifyRhsExpression()
        {
            if (Context.ExpressionSimplifier is null)
                return;

            _rhsExpression = Context.ExpressionSimplifier.Simplify(_rhsExpression);
        }

        /// <summary>
        /// Set the RHS expression to the given expression
        /// </summary>
        /// <param name="newRhsExpression"></param>
        public void ResetRhsExpression([NotNull] ISymbolicExpression newRhsExpression)
        {
            _rhsExpression = newRhsExpression;
        }

        /// <summary>
        /// Get an ordered list of all required input variables and intermediate variables
        /// necessary for this computed variable's RHS computation in whole the code block
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ISymbolicVariable> GetUsedVariables()
        {
            //The final list containing the ordered temp variables
            var finalVariablesDictionary = new Dictionary<string, ISymbolicVariable>();

            var queue = new PriorityQueue<int, ISymbolicVariableComputed>();

            //Add the intermediate variables in a priority queue according to
            //their computation order in the code block
            foreach (var rhsVariable in RhsVariables)
            {
                if (rhsVariable is ISymbolicVariableComputed {IsIntermediateVariable: true} rhsTempVariable)
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
                    if (rhsVar is ISymbolicVariableComputed {IsIntermediateVariable: true} rhsTempVar)
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
        public IEnumerable<ISymbolicVariableParameter> GetUsedParameterVariables()
        {
            return GetUsedVariables()
                .Select(item => item as ISymbolicVariableParameter)
                .Where(item => ReferenceEquals(item, null) == false);
        }

        /// <summary>
        /// Get an ordered list of all required intermediate variables necessary for this
        /// computed variable's RHS computation in whole the code block
        /// </summary>
        public IEnumerable<ISymbolicVariableComputed> GetUsedIntermediateVariables()
        {
            //The final list containing the ordered temp variables
            var finalVariablesDictionary = new Dictionary<string, ISymbolicVariableComputed>();

            var queue = new PriorityQueue<int, ISymbolicVariableComputed>();

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

        
        

    }
}