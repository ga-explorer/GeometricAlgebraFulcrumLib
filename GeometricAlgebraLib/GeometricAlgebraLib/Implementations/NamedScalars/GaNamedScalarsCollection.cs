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

        private readonly Dictionary<string, IGaNamedScalar<TScalar>> _rhsScalarValueTextDictionary
            = new Dictionary<string, IGaNamedScalar<TScalar>>();

        private readonly Dictionary<string, GaNamedScalarConstant<TScalar>> _constantNamedScalarsDictionary
            = new Dictionary<string, GaNamedScalarConstant<TScalar>>();

        private readonly Dictionary<string, GaNamedScalarParameter<TScalar>> _parameterNamedScalarsDictionary
            = new Dictionary<string, GaNamedScalarParameter<TScalar>>();

        private readonly Dictionary<string, GaNamedScalarVariable<TScalar>> _variableNamedScalarsDictionary
            = new Dictionary<string, GaNamedScalarVariable<TScalar>>();


        public IGaScalarProcessorNamedScalar<TScalar> NamedScalarProcessor { get; protected set; }

        public IGaSymbolicScalarProcessor<TScalar> SymbolicScalarProcessor { get; }

        public GaNamedScalarConstantsFactory<TScalar> ConstantsFactory { get; }

        public GaNamedScalarParametersFactory<TScalar> ParametersFactory { get; }

        public GaNamedScalarVariablesFactory<TScalar> VariablesFactory { get; }

        public string DefaultSymbolName { get; }

        public IEnumerable<GaNamedScalarConstant<TScalar>> ConstantNamedScalars
            => _constantNamedScalarsDictionary
                .Values
                .OrderBy(scalar => scalar.ScalarId);

        public IEnumerable<GaNamedScalarParameter<TScalar>> ParameterNamedScalars
            => _parameterNamedScalarsDictionary
                .Values
                .OrderBy(scalar => scalar.ScalarId);

        public IEnumerable<GaNamedScalarVariable<TScalar>> VariableNamedScalars
            => _variableNamedScalarsDictionary.Values.OrderBy(scalar => scalar.ScalarId);

        public IEnumerable<GaNamedScalarVariable<TScalar>> IntermediateVariableNamedScalars
            => _variableNamedScalarsDictionary
                .Values
                .Where(s => !s.IsOutput)
                .OrderBy(scalar => scalar.ScalarId);

        public IEnumerable<GaNamedScalarVariable<TScalar>> OutputVariableNamedScalars
            => _variableNamedScalarsDictionary
                .Values
                .Where(s => s.IsOutput)
                .OrderBy(scalar => scalar.ScalarId);

        public IEnumerable<IGaNamedScalar<TScalar>> NamedScalars
            => _constantNamedScalarsDictionary
                .Values
                .Cast<IGaNamedScalar<TScalar>>()
                .Concat(_parameterNamedScalarsDictionary.Values)
                .Concat(_variableNamedScalarsDictionary.Values)
                .OrderBy(scalar => scalar.ScalarId);


        public GaNamedScalarsCollection([NotNull] IGaSymbolicScalarProcessor<TScalar> symbolicScalarProcessor, [NotNull] string defaultSymbolName)
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

        private int GetNamedScalarId()
        {
            _namedScalarId++;

            return _namedScalarId;
        }

        public IGaNamedScalar<TScalar> GetNamedScalarByName(string scalarName)
        {
            if (_constantNamedScalarsDictionary.TryGetValue(scalarName, out var constantNamedScalar))
                return constantNamedScalar;

            if (_parameterNamedScalarsDictionary.TryGetValue(scalarName, out var parameterNamedScalar))
                return parameterNamedScalar;

            if (_variableNamedScalarsDictionary.TryGetValue(scalarName, out var computedNamedScalar))
                return computedNamedScalar;

            throw new KeyNotFoundException(scalarName);
        }

        public IGaNamedScalar<TScalar> GetNamedScalarByValueText(string rhsScalarValueText)
        {
            if (_rhsScalarValueTextDictionary.TryGetValue(rhsScalarValueText, out var namedScalar))
                return namedScalar;

            throw new KeyNotFoundException(rhsScalarValueText);
        }

        public GaNamedScalarParameter<TScalar> GetParameterNamedScalar(string scalarName)
        {
            if (_parameterNamedScalarsDictionary.TryGetValue(scalarName, out var namedScalar))
                return namedScalar;

            namedScalar = new GaNamedScalarParameter<TScalar>(
                this, 
                GetNamedScalarId(),
                scalarName
            );

            _parameterNamedScalarsDictionary.Add(
                scalarName,
                namedScalar
            );

            _rhsScalarValueTextDictionary.Add(
                scalarName,
                namedScalar
            );

            return namedScalar;
        }

        public GaNamedScalarConstant<TScalar> GetConstantNamedScalar(TScalar scalar)
        {
            var scalarName = SymbolicScalarProcessor.ToText(scalar);

            if (_constantNamedScalarsDictionary.TryGetValue(scalarName, out var namedScalar))
                return namedScalar;

            namedScalar = new GaNamedScalarConstant<TScalar>(
                this, 
                GetNamedScalarId(),
                scalar
            );

            _constantNamedScalarsDictionary.Add(
                scalarName, 
                namedScalar
            );

            _rhsScalarValueTextDictionary.Add(
                scalarName,
                namedScalar
            );

            return namedScalar;
        }

        public GaNamedScalarVariable<TScalar> GetVariableNamedScalar(Func<TScalar, TScalar> computingFunc, IGaNamedScalar<TScalar> namedScalar)
        {
            var scalar = computingFunc(
                namedScalar.LhsScalarValue
            );

            return GetVariableNamedScalar(
                scalar,
                namedScalar
            );
        }

        public GaNamedScalarVariable<TScalar> GetVariableNamedScalar(Func<TScalar, TScalar, TScalar> computingFunc, IGaNamedScalar<TScalar> namedScalar1, IGaNamedScalar<TScalar> namedScalar2)
        {
            var scalar = computingFunc(
                namedScalar1.LhsScalarValue,
                namedScalar2.LhsScalarValue
            );

            return GetVariableNamedScalar(
                scalar,
                namedScalar1,
                namedScalar2
            );
        }

        public GaNamedScalarVariable<TScalar> GetVariableNamedScalar(Func<TScalar, TScalar, TScalar, TScalar> computingFunc, IGaNamedScalar<TScalar> namedScalar1, IGaNamedScalar<TScalar> namedScalar2, IGaNamedScalar<TScalar> namedScalar3)
        {
            var scalar = computingFunc(
                namedScalar1.LhsScalarValue,
                namedScalar2.LhsScalarValue,
                namedScalar3.LhsScalarValue
            );

            return GetVariableNamedScalar(
                scalar,
                namedScalar1,
                namedScalar2,
                namedScalar3
            );
        }

        public GaNamedScalarVariable<TScalar> GetVariableNamedScalar(Func<IEnumerable<TScalar>, TScalar> computingFunc, IEnumerable<IGaNamedScalar<TScalar>> namedScalarsList)
        {
            var namedScalarsArray = 
                namedScalarsList.ToArray();

            var scalar = computingFunc(
                namedScalarsArray.Select(namedScalar => namedScalar.LhsScalarValue)
            );

            return GetVariableNamedScalar(
                scalar,
                namedScalarsArray
            );
        }

        public GaNamedScalarVariable<TScalar> GetVariableNamedScalar(TScalar scalar, IGaNamedScalar<TScalar> dependsOnScalar)
        {
            var scalarText = SymbolicScalarProcessor.ToText(scalar);

            if (_rhsScalarValueTextDictionary.TryGetValue(scalarText, out var namedScalar) && namedScalar is GaNamedScalarVariable<TScalar> variableNamedScalar)
                return variableNamedScalar;

            var scalarName = GetNewSymbolName();

            variableNamedScalar = new GaNamedScalarVariable<TScalar>(
                this,
                GetNamedScalarId(),
                scalarName,
                scalar,
                new []{dependsOnScalar}
            );

            _variableNamedScalarsDictionary.Add(
                scalarName, 
                variableNamedScalar
            );

            _rhsScalarValueTextDictionary.Add(
                scalarText,
                variableNamedScalar
            );

            return variableNamedScalar;
        }

        public GaNamedScalarVariable<TScalar> GetVariableNamedScalar(TScalar scalar, IGaNamedScalar<TScalar> dependsOnScalar1, IGaNamedScalar<TScalar> dependsOnScalar2)
        {
            var scalarText = SymbolicScalarProcessor.ToText(scalar);

            if (_rhsScalarValueTextDictionary.TryGetValue(scalarText, out var namedScalar) && namedScalar is GaNamedScalarVariable<TScalar> variableNamedScalar)
                return variableNamedScalar;

            var scalarName = GetNewSymbolName();

            variableNamedScalar = new GaNamedScalarVariable<TScalar>(
                this,
                GetNamedScalarId(),
                scalarName,
                scalar,
                new []{dependsOnScalar1, dependsOnScalar2}
            );

            _variableNamedScalarsDictionary.Add(
                scalarName, 
                variableNamedScalar
            );

            _rhsScalarValueTextDictionary.Add(
                scalarText,
                variableNamedScalar
            );

            return variableNamedScalar;
        }

        public GaNamedScalarVariable<TScalar> GetVariableNamedScalar(TScalar scalar, IGaNamedScalar<TScalar> dependsOnScalar1, IGaNamedScalar<TScalar> dependsOnScalar2, IGaNamedScalar<TScalar> dependsOnScalar3)
        {
            var scalarText = SymbolicScalarProcessor.ToText(scalar);

            if (_rhsScalarValueTextDictionary.TryGetValue(scalarText, out var namedScalar) && namedScalar is GaNamedScalarVariable<TScalar> variableNamedScalar)
                return variableNamedScalar;

            var scalarName = GetNewSymbolName();

            variableNamedScalar = new GaNamedScalarVariable<TScalar>(
                this,
                GetNamedScalarId(),
                scalarName,
                scalar,
                new []{dependsOnScalar1, dependsOnScalar2, dependsOnScalar3}
            );

            _variableNamedScalarsDictionary.Add(
                scalarName, 
                variableNamedScalar
            );

            _rhsScalarValueTextDictionary.Add(
                scalarText,
                variableNamedScalar
            );

            return variableNamedScalar;
        }

        public GaNamedScalarVariable<TScalar> GetVariableNamedScalar(TScalar scalar, params IGaNamedScalar<TScalar>[] dependsOnScalars)
        {
            var scalarText = SymbolicScalarProcessor.ToText(scalar);

            if (_rhsScalarValueTextDictionary.TryGetValue(scalarText, out var namedScalar) && namedScalar is GaNamedScalarVariable<TScalar> variableNamedScalar)
                return variableNamedScalar;

            var scalarName = GetNewSymbolName();

            variableNamedScalar = new GaNamedScalarVariable<TScalar>(
                this,
                GetNamedScalarId(),
                scalarName,
                scalar,
                dependsOnScalars
            );

            _variableNamedScalarsDictionary.Add(
                scalarName, 
                variableNamedScalar
            );

            _rhsScalarValueTextDictionary.Add(
                scalarText,
                variableNamedScalar
            );

            return variableNamedScalar;
        }

        public GaNamedScalarVariable<TScalar> GetVariableNamedScalar(TScalar scalar, IEnumerable<IGaNamedScalar<TScalar>> dependsOnScalars)
        {
            var scalarText = SymbolicScalarProcessor.ToText(scalar);

            if (_rhsScalarValueTextDictionary.TryGetValue(scalarText, out var namedScalar) && namedScalar is GaNamedScalarVariable<TScalar> variableNamedScalar)
                return variableNamedScalar;

            var scalarName = GetNewSymbolName();

            variableNamedScalar = new GaNamedScalarVariable<TScalar>(
                this,
                GetNamedScalarId(),
                scalarName,
                scalar,
                dependsOnScalars
            );

            _variableNamedScalarsDictionary.Add(
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

                foreach (var rhsNamedScalar in namedScalar.DependsOnScalars)
                    rhsNamedScalar.IsUsedForOutputVariables = true;
            }

            _constantNamedScalarsDictionary.Remove(
                _constantNamedScalarsDictionary
                    .Values
                    .Where(scalar => !scalar.IsUsedForOutputVariables)
                    .Select(scalar => scalar.ScalarName)
            );

            _parameterNamedScalarsDictionary.Remove(
                _parameterNamedScalarsDictionary
                    .Values
                    .Where(scalar => !scalar.IsUsedForOutputVariables)
                    .Select(scalar => scalar.ScalarName)
            );

            _variableNamedScalarsDictionary.Remove(
                _variableNamedScalarsDictionary
                    .Values
                    .Where(scalar => scalar.IsIntermediate && !scalar.IsUsedForOutputVariables)
                    .Select(scalar => scalar.ScalarName)
            );

            _variableNamedScalarsDictionary.Remove(
                _variableNamedScalarsDictionary
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