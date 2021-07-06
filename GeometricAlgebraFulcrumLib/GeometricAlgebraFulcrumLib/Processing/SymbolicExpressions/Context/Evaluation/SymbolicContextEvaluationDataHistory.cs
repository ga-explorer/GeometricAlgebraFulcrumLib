using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context.Optimizer;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Variables;

namespace GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context.Evaluation
{
    internal sealed class SymbolicContextEvaluationDataHistory
    {
        private readonly System.Random _randomSource 
            = new System.Random(DateTime.Now.Millisecond);

        private readonly Dictionary<string, double> _inputVarsValues;

        private readonly List<SymbolicContextEvaluationData> _evaluations 
            = new List<SymbolicContextEvaluationData>();


        public SymbolicContext Context { get; }


        internal SymbolicContextEvaluationDataHistory([NotNull] SymbolicContext context, double minValue, double maxValue)
        {
            Context = context;

            _inputVarsValues = 
                Context
                .ParameterVariables
                .ToDictionary(
                    item => item.InternalName,
                    _ => minValue + (maxValue - minValue) * _randomSource.NextDouble()
                );
        }

        internal SymbolicContextEvaluationDataHistory(SymbolicContext context, Dictionary<string, ISymbolicVariableParameter> inputsWithTestValues)
        {
            Context = context;

            _inputVarsValues = new Dictionary<string, double>();

            foreach (var pair in inputsWithTestValues)
            {
                if (!Context.TryGetParameterVariable(pair.Value.InternalName, out var inputParamVar))
                    continue;

                _inputVarsValues.Add(inputParamVar.InternalName, pair.Value.RhsExpressionValue);
            }
        }


        public SymbolicContextEvaluationData AddEvaluation(string evalTitle)
        {
            var evaluationData = new SymbolicContextEvaluationData(Context, evalTitle);

            _evaluations.Add(evaluationData);

            foreach (var pair in _inputVarsValues)
                evaluationData[pair.Key] = pair.Value;

            ScOptEvaluateCodeBlock.Process(Context, evaluationData);

            return evaluationData;
        }

        public override string ToString()
        {
            var s = new StringBuilder(1024);

            var maxLength = _evaluations.Max(item => item.EvaluationTitle.Length);

            foreach (var outputVar in Context.OutputVariables)
            {
                s.Append(outputVar.InternalName).AppendLine(":");

                foreach (var evalData in _evaluations)
                    s.Append(evalData.EvaluationTitle.PadLeft(maxLength))
                        .Append(": ")
                        .Append(evalData[outputVar.InternalName])
                        .AppendLine();

                s.AppendLine();
            }

            return s.ToString();
        }
    }
}
