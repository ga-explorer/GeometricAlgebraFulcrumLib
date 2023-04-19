using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.HeadSpecs;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Numbers;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Variables
{
    public sealed class MetaExpressionVariableComputed :
        MetaExpressionVariableBase, 
        IMetaExpressionVariableComputed
    {
        public static MetaExpressionVariableComputed Create(MetaExpressionHeadSpecsVariable headSpecs, IMetaExpression rhsExpression)
        {
            return new MetaExpressionVariableComputed(
                headSpecs,
                rhsExpression
            );
        }

        public static MetaExpressionVariableComputed Create(MetaContext context, string variableName, IMetaExpression rhsExpression)
        {
            return new MetaExpressionVariableComputed(
                MetaExpressionHeadSpecsVariable.Create(context, variableName),
                rhsExpression
            );
        }

        public static MetaExpressionVariableComputed CreateFactoredSubExpression(MetaContext context, string variableName, IMetaExpression rhsExpression, bool isUsedOnce)
        {
            return new MetaExpressionVariableComputed(
                MetaExpressionHeadSpecsVariable.Create(context, variableName),
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
            => IsOutputVariable || DependingVariablesCache.Count > 0;

        public bool IsReused { get; private set; }

        public int NameIndex { get; private set; } = -1;

        public bool IsFactoredSubExpression { get; private init; }

        public int SubExpressionUseCount { get; set; }


        private MetaExpressionVariableComputed(MetaExpressionHeadSpecsVariable headSpecs, IMetaExpression rhsExpression)
            : base(headSpecs)
        {
            _rhsExpression = rhsExpression;
            ComputationOrder = AtomicExpressionId;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMetaExpression GetExpressionCopy()
        {
            return new MetaExpressionVariableComputed(
                VariableHeadSpecs,
                RhsExpression.GetExpressionCopy()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMetaExpression GetScalarValue(bool useRhsScalarValue)
        {
            return useRhsScalarValue
                ? RhsExpression 
                : this;
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ReplaceRhsVariable(string oldVariableName, string newVariableName)
        {
            var (isReplaced, newRhsExpression) = 
                _rhsExpression.ReplaceAllVariableByVariable(oldVariableName, newVariableName);

            if (isReplaced)
                _rhsExpression = newRhsExpression;

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
                _rhsExpression.ReplaceAllExpressionByExpression(oldExpr, newExpr);

            if (isReplaced)
                _rhsExpression = newRhsExpression;

            return isReplaced;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SimplifyRhsExpression()
        {
            _rhsExpression = Context.SymbolicEvaluator.Simplify(_rhsExpression);
        }

        /// <summary>
        /// Set the RHS expression to the given expression
        /// </summary>
        /// <param name="newRhsExpression"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ResetRhsExpression(IMetaExpression newRhsExpression)
        {
            _rhsExpression = newRhsExpression;
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

            var queue = new DataStructuresLib.PriorityQueue<int, IMetaExpressionVariableComputed>();

            //Add the intermediate variables in a priority queue according to
            //their computation order in the code block
            foreach (var rhsVariable in RhsVariables)
            {
                if (rhsVariable is IMetaExpressionVariableComputed {IsIntermediateVariable: true} rhsTempVariable)
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
                    if (rhsVar is IMetaExpressionVariableComputed {IsIntermediateVariable: true} rhsTempVar)
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
                .Where(item => ReferenceEquals(item, null) == false);
        }

        /// <summary>
        /// Get an ordered list of all required intermediate variables necessary for this
        /// computed variable's RHS computation in whole the code block
        /// </summary>
        public IEnumerable<IMetaExpressionVariableComputed> GetUsedIntermediateVariables()
        {
            //The final list containing the ordered temp variables
            var finalVariablesDictionary = new Dictionary<string, IMetaExpressionVariableComputed>();

            var queue = new DataStructuresLib.PriorityQueue<int, IMetaExpressionVariableComputed>();

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