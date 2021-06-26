using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.Extensions;
using GeometricAlgebraLib.Processors.Scalars;
using TextComposerLib.Text;

namespace GeometricAlgebraLib.Implementations.NamedScalars
{
    public class GaNamedScalarsCollection<TScalar>
    {
        private int _tempNamesIndex = 0;

        private int _namedScalarId = -1;

        private int _computationOrder = -1;

        private readonly Dictionary<string, IGaNamedScalar<TScalar>> _rhsScalarValueTextDictionary
            = new Dictionary<string, IGaNamedScalar<TScalar>>();

        private readonly Dictionary<string, GaNamedScalarConstant<TScalar>> _constantsDictionary
            = new Dictionary<string, GaNamedScalarConstant<TScalar>>();

        private readonly Dictionary<string, GaNamedScalarParameter<TScalar>> _parametersDictionary
            = new Dictionary<string, GaNamedScalarParameter<TScalar>>();

        private readonly Dictionary<string, GaNamedScalarVariable<TScalar>> _variablesDictionary
            = new Dictionary<string, GaNamedScalarVariable<TScalar>>();


        public IGaScalarProcessorNamedScalar<TScalar> NamedScalarProcessor { get; protected set; }

        public IGaSymbolicScalarProcessor<TScalar> SymbolicScalarProcessor { get; }

        public GaNamedScalarConstantsFactory<TScalar> ConstantsFactory { get; }

        public GaNamedScalarParametersFactory<TScalar> ParametersFactory { get; }

        public GaNamedScalarVariablesFactory<TScalar> VariablesFactory { get; }

        public string DefaultSymbolName { get; }

        public bool MergeScalars { get; set; }

        public IEnumerable<GaNamedScalarConstant<TScalar>> Constants
            => _constantsDictionary
                .Values
                .OrderBy(scalar => scalar.ScalarId);

        public IEnumerable<GaNamedScalarParameter<TScalar>> Parameters
            => _parametersDictionary
                .Values
                .OrderBy(scalar => scalar.ScalarId);

        public IEnumerable<IGaInputNamedScalar<TScalar>> Inputs
            => _constantsDictionary
                .Values
                .Cast<IGaInputNamedScalar<TScalar>>()
                .Concat(_parametersDictionary.Values)
                .OrderBy(scalar => scalar.ScalarId);

        public IEnumerable<GaNamedScalarVariable<TScalar>> Variables
            => _variablesDictionary.Values.OrderBy(scalar => scalar.ScalarId);

        public IEnumerable<GaNamedScalarVariable<TScalar>> IntermediateVariables
            => _variablesDictionary
                .Values
                .Where(s => !s.IsOutput)
                .OrderBy(scalar => scalar.ScalarId);

        public IEnumerable<GaNamedScalarVariable<TScalar>> OutputVariables
            => _variablesDictionary
                .Values
                .Where(s => s.IsOutput)
                .OrderBy(scalar => scalar.ScalarId);

        public IEnumerable<IGaNamedScalar<TScalar>> NamedScalars
            => _constantsDictionary
                .Values
                .Cast<IGaNamedScalar<TScalar>>()
                .Concat(_parametersDictionary.Values)
                .Concat(_variablesDictionary.Values)
                .OrderBy(scalar => scalar.ScalarId);


        public GaNamedScalarsCollection([NotNull] IGaSymbolicScalarProcessor<TScalar> symbolicScalarProcessor,
            [NotNull] string defaultSymbolName)
        {
            NamedScalarProcessor = new GaScalarProcessorNamedScalar<TScalar>(this);
            SymbolicScalarProcessor = symbolicScalarProcessor;
            DefaultSymbolName = defaultSymbolName;

            ConstantsFactory = new GaNamedScalarConstantsFactory<TScalar>(this);
            ParametersFactory = new GaNamedScalarParametersFactory<TScalar>(this);
            VariablesFactory = new GaNamedScalarVariablesFactory<TScalar>(this);
        }


        private string GetNewSymbolName()
        {
            _tempNamesIndex++;

            return $"{DefaultSymbolName}{_tempNamesIndex}";
        }

        internal int GetNextNamedScalarId()
        {
            _namedScalarId++;

            return _namedScalarId;
        }

        internal int GetNextComputationOrder()
        {
            _computationOrder++;

            return _computationOrder;
        }

        public bool IsValid()
        {
            return true;
        }

        public bool TryGetNamedScalarByName(string scalarName, out IGaNamedScalar<TScalar> namedScalar)
        {
            if (_constantsDictionary.TryGetValue(scalarName, out var constantNamedScalar))
            {
                namedScalar = constantNamedScalar;
                return true;
            }

            if (_parametersDictionary.TryGetValue(scalarName, out var parameterNamedScalar))
            {
                namedScalar = parameterNamedScalar;
                return true;
            }

            if (_variablesDictionary.TryGetValue(scalarName, out var computedNamedScalar))
            {
                namedScalar = computedNamedScalar;
                return true;
            }

            namedScalar = null;
            return false;
        }

        public bool TryGetNamedScalarByValueText(string rhsScalarValueText, out IGaNamedScalar<TScalar> namedScalar)
        {
            return _rhsScalarValueTextDictionary.TryGetValue(rhsScalarValueText, out namedScalar);
        }

        public bool TryGetParameterByName(string scalarName, out GaNamedScalarParameter<TScalar> namedScalar)
        {
            return _parametersDictionary.TryGetValue(scalarName, out namedScalar);
        }

        public IGaNamedScalar<TScalar> GetNamedScalarByName(string scalarName)
        {
            if (_constantsDictionary.TryGetValue(scalarName, out var constantNamedScalar))
                return constantNamedScalar;

            if (_parametersDictionary.TryGetValue(scalarName, out var parameterNamedScalar))
                return parameterNamedScalar;

            if (_variablesDictionary.TryGetValue(scalarName, out var computedNamedScalar))
                return computedNamedScalar;

            throw new KeyNotFoundException(scalarName);
        }

        public IGaNamedScalar<TScalar> GetNamedScalarByValueText(string rhsScalarValueText)
        {
            if (_rhsScalarValueTextDictionary.TryGetValue(rhsScalarValueText, out var namedScalar))
                return namedScalar;

            throw new KeyNotFoundException(rhsScalarValueText);
        }

        public GaNamedScalarParameter<TScalar> GetParameterByName(string scalarName)
        {
            if (_parametersDictionary.TryGetValue(scalarName, out var namedScalar))
                return namedScalar;

            namedScalar = new GaNamedScalarParameter<TScalar>(
                this, 
                scalarName
            );

            _parametersDictionary.Add(
                scalarName,
                namedScalar
            );

            _rhsScalarValueTextDictionary.Add(
                scalarName,
                namedScalar
            );

            return namedScalar;
        }

        public GaNamedScalarConstant<TScalar> GetOrDefineConstant(TScalar scalar)
        {
            var scalarName = SymbolicScalarProcessor.ToText(scalar);

            if (_constantsDictionary.TryGetValue(scalarName, out var namedScalar))
                return namedScalar;

            namedScalar = new GaNamedScalarConstant<TScalar>(
                this, 
                scalar
            );

            _constantsDictionary.Add(
                scalarName, 
                namedScalar
            );

            _rhsScalarValueTextDictionary.Add(
                scalarName,
                namedScalar
            );

            return namedScalar;
        }

        public GaNamedScalarVariable<TScalar> GetOrDefineVariable(Func<TScalar, TScalar> computingFunc, IGaNamedScalar<TScalar> dependsOnNamedScalar)
        {
            var rhsScalarValue = computingFunc(
                dependsOnNamedScalar.GetScalarValue(MergeScalars)
            );

            return GetOrDefineVariable(
                rhsScalarValue,
                dependsOnNamedScalar
            );
        }

        public GaNamedScalarVariable<TScalar> GetOrDefineVariable(Func<TScalar, TScalar, TScalar> computingFunc, IGaNamedScalar<TScalar> dependsOnNamedScalar1, IGaNamedScalar<TScalar> dependsOnNamedScalar2)
        {
            var rhsScalarValue = computingFunc(
                dependsOnNamedScalar1.GetScalarValue(MergeScalars),
                dependsOnNamedScalar2.GetScalarValue(MergeScalars)
            );

            return GetOrDefineVariable(
                rhsScalarValue,
                dependsOnNamedScalar1,
                dependsOnNamedScalar2
            );
        }

        public GaNamedScalarVariable<TScalar> GetOrDefineVariable(Func<TScalar, TScalar, TScalar, TScalar> computingFunc, IGaNamedScalar<TScalar> dependsOnNamedScalar1, IGaNamedScalar<TScalar> dependsOnNamedScalar2, IGaNamedScalar<TScalar> dependsOnNamedScalar3)
        {
            var rhsScalarValue = computingFunc(
                dependsOnNamedScalar1.GetScalarValue(MergeScalars),
                dependsOnNamedScalar2.GetScalarValue(MergeScalars),
                dependsOnNamedScalar3.GetScalarValue(MergeScalars)
            );

            return GetOrDefineVariable(
                rhsScalarValue,
                dependsOnNamedScalar1,
                dependsOnNamedScalar2,
                dependsOnNamedScalar3
            );
        }

        public GaNamedScalarVariable<TScalar> GetOrDefineVariable(Func<IEnumerable<TScalar>, TScalar> computingFunc, IEnumerable<IGaNamedScalar<TScalar>> dependsOnNamedScalars)
        {
            var namedScalarsArray = 
                dependsOnNamedScalars.ToArray();

            var rhsScalarValue = computingFunc(
                namedScalarsArray.Select(namedScalar => namedScalar.GetScalarValue(MergeScalars))
            );

            return GetOrDefineVariable(
                rhsScalarValue,
                namedScalarsArray
            );
        }

        public GaNamedScalarVariable<TScalar> GetOrDefineVariable(TScalar scalarValue, IGaNamedScalar<TScalar> dependsOnNamedScalar)
        {
            var scalarText = SymbolicScalarProcessor.ToText(scalarValue);

            if (_rhsScalarValueTextDictionary.TryGetValue(scalarText, out var namedScalar) && namedScalar is GaNamedScalarVariable<TScalar> variableNamedScalar)
                return variableNamedScalar;

            var scalarName = GetNewSymbolName();

            variableNamedScalar = new GaNamedScalarVariable<TScalar>(
                this,
                scalarName,
                scalarValue,
                new []{dependsOnNamedScalar}
            );

            _variablesDictionary.Add(
                scalarName, 
                variableNamedScalar
            );

            _rhsScalarValueTextDictionary.Add(
                scalarText,
                variableNamedScalar
            );

            return variableNamedScalar;
        }

        public GaNamedScalarVariable<TScalar> GetOrDefineVariable(TScalar scalarValue, IGaNamedScalar<TScalar> dependsOnNamedScalar1, IGaNamedScalar<TScalar> dependsOnNamedScalar2)
        {
            var scalarText = SymbolicScalarProcessor.ToText(scalarValue);

            if (_rhsScalarValueTextDictionary.TryGetValue(scalarText, out var namedScalar) && namedScalar is GaNamedScalarVariable<TScalar> variableNamedScalar)
                return variableNamedScalar;

            var scalarName = GetNewSymbolName();

            variableNamedScalar = new GaNamedScalarVariable<TScalar>(
                this,
                scalarName,
                scalarValue,
                new []{dependsOnNamedScalar1, dependsOnNamedScalar2}
            );

            _variablesDictionary.Add(
                scalarName, 
                variableNamedScalar
            );

            _rhsScalarValueTextDictionary.Add(
                scalarText,
                variableNamedScalar
            );

            return variableNamedScalar;
        }

        public GaNamedScalarVariable<TScalar> GetOrDefineVariable(TScalar scalarValue, IGaNamedScalar<TScalar> dependsOnNamedScalar1, IGaNamedScalar<TScalar> dependsOnNamedScalar2, IGaNamedScalar<TScalar> dependsOnNamedScalar3)
        {
            var scalarText = SymbolicScalarProcessor.ToText(scalarValue);

            if (_rhsScalarValueTextDictionary.TryGetValue(scalarText, out var namedScalar) && namedScalar is GaNamedScalarVariable<TScalar> variableNamedScalar)
                return variableNamedScalar;

            var scalarName = GetNewSymbolName();

            variableNamedScalar = new GaNamedScalarVariable<TScalar>(
                this,
                scalarName,
                scalarValue,
                new []{dependsOnNamedScalar1, dependsOnNamedScalar2, dependsOnNamedScalar3}
            );

            _variablesDictionary.Add(
                scalarName, 
                variableNamedScalar
            );

            _rhsScalarValueTextDictionary.Add(
                scalarText,
                variableNamedScalar
            );

            return variableNamedScalar;
        }

        public GaNamedScalarVariable<TScalar> GetOrDefineVariable(TScalar scalarValue, params IGaNamedScalar<TScalar>[] dependsOnNamedScalars)
        {
            var scalarText = SymbolicScalarProcessor.ToText(scalarValue);

            if (_rhsScalarValueTextDictionary.TryGetValue(scalarText, out var namedScalar) && namedScalar is GaNamedScalarVariable<TScalar> variableNamedScalar)
                return variableNamedScalar;

            var scalarName = GetNewSymbolName();

            variableNamedScalar = new GaNamedScalarVariable<TScalar>(
                this,
                scalarName,
                scalarValue,
                dependsOnNamedScalars
            );

            _variablesDictionary.Add(
                scalarName, 
                variableNamedScalar
            );

            _rhsScalarValueTextDictionary.Add(
                scalarText,
                variableNamedScalar
            );

            return variableNamedScalar;
        }

        public GaNamedScalarVariable<TScalar> GetOrDefineVariable(TScalar scalarValue, IEnumerable<IGaNamedScalar<TScalar>> dependsOnNamedScalars)
        {
            var scalarText = SymbolicScalarProcessor.ToText(scalarValue);

            if (_rhsScalarValueTextDictionary.TryGetValue(scalarText, out var namedScalar) && namedScalar is GaNamedScalarVariable<TScalar> variableNamedScalar)
                return variableNamedScalar;

            var scalarName = GetNewSymbolName();

            variableNamedScalar = new GaNamedScalarVariable<TScalar>(
                this,
                scalarName,
                scalarValue,
                dependsOnNamedScalars
            );

            _variablesDictionary.Add(
                scalarName, 
                variableNamedScalar
            );

            _rhsScalarValueTextDictionary.Add(
                scalarText,
                variableNamedScalar
            );

            return variableNamedScalar;
        }


        private void OptimizeCollection_RemoveNotUsedNamedScalars()
        {
            foreach (var namedScalar in NamedScalars)
                namedScalar.IsUsedForOutputVariables = namedScalar.IsOutput;

            foreach (var namedScalar in NamedScalars.OrderByDescending(scalar => scalar.ScalarId))
            {
                if (!namedScalar.IsUsedForOutputVariables) 
                    continue;

                if (namedScalar is not GaNamedScalarVariable<TScalar> variableNamedScalar) 
                    continue;

                foreach (var rhsNamedScalar in variableNamedScalar.RhsNamedScalars)
                    rhsNamedScalar.IsUsedForOutputVariables = true;
            }

            _constantsDictionary.Remove(
                _constantsDictionary
                    .Values
                    .Where(scalar => !scalar.IsUsedForOutputVariables)
                    .Select(scalar => scalar.ScalarName)
            );

            _parametersDictionary.Remove(
                _parametersDictionary
                    .Values
                    .Where(scalar => !scalar.IsUsedForOutputVariables)
                    .Select(scalar => scalar.ScalarName)
            );

            _variablesDictionary.Remove(
                _variablesDictionary
                    .Values
                    .Where(scalar => scalar.IsIntermediate && !scalar.IsUsedForOutputVariables)
                    .Select(scalar => scalar.ScalarName)
            );

            _variablesDictionary.Remove(
                _variablesDictionary
                    .Values
                    .Where(scalar => !scalar.IsUsedForOutputVariables)
                    .Select(scalar => scalar.RhsScalarValueText)
                    .ToArray()
            );
        }

        public void OptimizeCollection()
        {
            OptimizeCollection_RemoveNotUsedNamedScalars();
        }


        public override string ToString()
        {
            return NamedScalars
                .Select(scalar => scalar.ToString())
                .Concatenate(Environment.NewLine);
        }
    }
}