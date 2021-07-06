using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context.Evaluation
{
    internal sealed class SymbolicContextEvaluationData : 
        IReadOnlyDictionary<string, double>
    {
        private readonly Dictionary<string, double> _variablesValues 
            = new Dictionary<string, double>();


        public string EvaluationTitle { get; private set; }

        public SymbolicContext CodeBlock { get; }

        public int Count 
            => _variablesValues.Count;


        public double this[string varName]
        {
            get =>
                _variablesValues.TryGetValue(varName, out var value) 
                    ? value 
                    : 0d;
            set
            {
                if (_variablesValues.ContainsKey(varName))
                    _variablesValues[varName] = value;
                else
                    _variablesValues.Add(varName, value);
            }
        }

        public IEnumerable<string> Keys 
            => _variablesValues.Keys;

        public IEnumerable<double> Values 
            => _variablesValues.Values;

        public Dictionary<string, double> OutputVariablesValues
        {
            get
            {
                return
                    CodeBlock
                        .OutputVariables
                        .Select(item => item.InternalName)
                        .ToDictionary(outputVarName => outputVarName, outputVarName => this[outputVarName]);
            }
        }


        public SymbolicContextEvaluationData(SymbolicContext codeBlock, string evalTitle)
        {
            EvaluationTitle = evalTitle;
            CodeBlock = codeBlock;
        }


        public bool ContainsKey(string varName)
        {
            return _variablesValues.ContainsKey(varName);
        }

        public bool TryGetValue(string varName, out double value)
        {
            return _variablesValues.TryGetValue(varName, out value);
        }

        public IEnumerator<KeyValuePair<string, double>> GetEnumerator()
        {
            return _variablesValues.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
