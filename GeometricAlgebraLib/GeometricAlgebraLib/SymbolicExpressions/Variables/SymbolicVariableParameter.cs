using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraLib.SymbolicExpressions.Context;
using GeometricAlgebraLib.SymbolicExpressions.HeadSpecs;

namespace GeometricAlgebraLib.SymbolicExpressions.Variables
{
    public sealed class SymbolicVariableParameter :
        SymbolicVariableBase, ISymbolicVariableParameter
    {
        public static SymbolicVariableParameter Create(SymbolicHeadSpecsVariable headSpecs)
        {
            return new SymbolicVariableParameter(headSpecs);
        }

        public static SymbolicVariableParameter Create(SymbolicContext context, string variableName)
        {
            return new SymbolicVariableParameter(
                SymbolicHeadSpecsVariable.Create(context, variableName)
            );
        }


        public override ISymbolicExpression RhsExpression 
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

        public override IEnumerable<ISymbolicVariableParameter> VariableParameterExpressions
        {
            get { yield return this; }
        }

        public override IEnumerable<ISymbolicVariableComputed> VariableComputedExpressions
            => Enumerable.Empty<ISymbolicVariableComputed>();



        private SymbolicVariableParameter(SymbolicHeadSpecsVariable headSpecs)
            : base(headSpecs)
        {
        }


        public override ISymbolicExpression GetExpressionCopy()
        {
            return new SymbolicVariableParameter(VariableHeadSpecs);
        }

        public override bool HasDependingVariables 
            => DependingVariablesCache.Count > 0;

        public override ISymbolicExpression GetScalarValue(bool useRhsScalarValue)
        {
            return this;
        }

        public override void ClearDependencyData()
        {
            DependingVariablesCache.Clear();
        }
    }
}