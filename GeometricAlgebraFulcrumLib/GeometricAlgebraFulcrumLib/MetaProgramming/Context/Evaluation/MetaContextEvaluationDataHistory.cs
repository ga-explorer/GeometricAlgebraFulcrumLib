using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Optimizer;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Variables;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Evaluation
{
    internal sealed class MetaContextEvaluationDataHistory
    {
        private readonly Random _randomSource 
            = new Random(DateTime.Now.Millisecond);

        private readonly Dictionary<string, double> _inputVarsValues;

        private readonly List<MetaContextEvaluationData> _evaluations 
            = new List<MetaContextEvaluationData>();


        public MetaContext Context { get; }


        internal MetaContextEvaluationDataHistory([NotNull] MetaContext context, double minValue, double maxValue)
        {
            Context = context;

            _inputVarsValues = 
                Context.GetParameterVariables()
                .ToDictionary(
                    item => item.InternalName,
                    _ => minValue + (maxValue - minValue) * _randomSource.NextDouble()
                );
        }

        internal MetaContextEvaluationDataHistory(MetaContext context, Dictionary<string, IMetaExpressionVariableParameter> inputsWithTestValues)
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


        public MetaContextEvaluationData AddEvaluation(string evalTitle)
        {
            var evaluationData = new MetaContextEvaluationData(Context, evalTitle);

            _evaluations.Add(evaluationData);

            foreach (var pair in _inputVarsValues)
                evaluationData[pair.Key] = pair.Value;

            McOptEvaluateCodeBlock.Process(Context, evaluationData);

            return evaluationData;
        }

        public override string ToString()
        {
            var s = new StringBuilder(1024);

            var maxLength = _evaluations.Max(item => item.EvaluationTitle.Length);

            foreach (var outputVar in Context.GetOutputVariables())
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
