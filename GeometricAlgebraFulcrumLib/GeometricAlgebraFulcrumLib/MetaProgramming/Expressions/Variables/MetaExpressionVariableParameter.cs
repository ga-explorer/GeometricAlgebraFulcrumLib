using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.HeadSpecs;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Variables
{
    public sealed class MetaExpressionVariableParameter :
        MetaExpressionVariableBase, 
        IMetaExpressionVariableParameter
    {
        public static MetaExpressionVariableParameter Create(MetaExpressionHeadSpecsVariable headSpecs)
        {
            return new MetaExpressionVariableParameter(headSpecs);
        }

        public static MetaExpressionVariableParameter Create(MetaContext context, string variableName)
        {
            return new MetaExpressionVariableParameter(
                MetaExpressionHeadSpecsVariable.Create(context, variableName)
            );
        }


        public override IMetaExpression RhsExpression 
            => this;

        public override string RhsExpressionText
            => VariableHeadSpecs.VariableName;

        public override bool IsParameterVariable 
            => true;

        public override bool IsComputedVariableOrComposite 
            => false;

        public override bool IsIntermediateVariable
        {
            get => false;
            set => throw new InvalidOperationException();
        }

        public override bool IsOutputVariable
        {
            get => false;
            set => throw new InvalidOperationException();
        }

        public override bool IsNumberOrParameter 
            => true;

        public override bool IsComputedVariable 
            => false;

        public override int MaxComputationLevel
            => 0;

        public override string GetTextDescription()
        {
            var isUsedText = HasDependingVariables
                ? "    Used"
                : "Not Used";

            return $"{isUsedText} Parameter       '{ExternalName}': {VariableHeadSpecs.VariableName}";
        }

        public override IEnumerable<IMetaExpressionVariableParameter> VariableParameterExpressions
        {
            get { yield return this; }
        }

        public override IEnumerable<IMetaExpressionVariableComputed> VariableComputedExpressions
            => Enumerable.Empty<IMetaExpressionVariableComputed>();



        private MetaExpressionVariableParameter(MetaExpressionHeadSpecsVariable headSpecs)
            : base(headSpecs)
        {
        }


        public override IMetaExpression GetExpressionCopy()
        {
            return new MetaExpressionVariableParameter(VariableHeadSpecs);
        }

        public override bool HasDependingVariables 
            => DependingVariablesCache.Count > 0;

        public override IMetaExpression GetScalarValue(bool useRhsScalarValue)
        {
            return this;
        }

        public override void ClearDependencyData()
        {
            DependingVariablesCache.Clear();
        }
    }
}