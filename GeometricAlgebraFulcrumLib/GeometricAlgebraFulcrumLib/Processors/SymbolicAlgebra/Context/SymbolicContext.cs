using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.HeadSpecs;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.Numbers;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.Variables;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Context.Optimizer;
using GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Evaluators;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using TextComposerLib.Text;

// ReSharper disable MemberCanBePrivate.Global

namespace GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Context
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public sealed class SymbolicContext :
        ILinearAlgebraProcessor<ISymbolicExpressionAtomic>
    {
        private int _tempNamesIndex;

        private int _atomicId = -1;

        private int _computationOrder = -1;

        private readonly Dictionary<string, ISymbolicNumber> _numbersDictionary
            = new Dictionary<string, ISymbolicNumber>();

        private readonly Dictionary<string, ISymbolicVariableParameter> _parametersVariablesDictionary
            = new Dictionary<string, ISymbolicVariableParameter>();

        private readonly Dictionary<string, ISymbolicVariableComputed> _computedVariablesDictionary
            = new Dictionary<string, ISymbolicVariableComputed>();
        

        public double ZeroEpsilon { get; set; }
            = 1e-13d;

        public bool IsNumeric 
            => false;

        public bool IsSymbolic 
            => true;
        
        public ISymbolicExpressionAtomic ScalarZero { get; }

        public ISymbolicExpressionAtomic ScalarOne { get; }

        public ISymbolicExpressionAtomic ScalarMinusOne { get; }

        public ISymbolicExpressionAtomic ScalarTwo { get; }
        
        public ISymbolicExpressionAtomic ScalarMinusTwo { get; }
        
        public ISymbolicExpressionAtomic ScalarTen { get; }
        
        public ISymbolicExpressionAtomic ScalarMinusTen { get; }

        public ISymbolicExpressionAtomic ScalarPi { get; }

        public ISymbolicExpressionAtomic ScalarE { get; }
        
        public IScalarAlgebraProcessor<ISymbolicExpressionAtomic> ScalarProcessor 
            => this;

        public ScalarAlgebraSymbolicExpressionProcessor SymbolicScalarProcessor { get; }

        public AngouriMathSymbolicExpressionEvaluator DefaultEvaluator { get; }

        private ISymbolicExpressionEvaluator _symbolicEvaluator;
        public ISymbolicExpressionEvaluator SymbolicEvaluator
        {
            get => _symbolicEvaluator ?? DefaultEvaluator;
            set => _symbolicEvaluator = value;
        }

        public SymbolicContextOptions ContextOptions { get; }
            = new SymbolicContextOptions();

        public SymbolicNumberFactory NumbersFactory { get; }

        public SymbolicParameterVariableFactory ParameterVariablesFactory { get; }

        public SymbolicComputedVariableFactory ComputedVariablesFactory { get; }

        public SymbolicFunctionHeadSpecsFactory FunctionHeadSpecsFactory { get; }

        public string DefaultSymbolName { get; set; } 
            = "tmpVar";

        public bool MergeExpressions { get; set; }



        public SymbolicContext()
        {
            NumbersFactory = new SymbolicNumberFactory(this);
            ParameterVariablesFactory = new SymbolicParameterVariableFactory(this);
            ComputedVariablesFactory = new SymbolicComputedVariableFactory(this);

            ScalarZero = GetOrDefineLiteralNumber(0);
            ScalarOne = GetOrDefineLiteralNumber(1);
            ScalarMinusOne = GetOrDefineLiteralNumber(-1);
            ScalarTwo = GetOrDefineLiteralNumber(2);
            ScalarMinusTwo = GetOrDefineLiteralNumber(-2);
            ScalarTen = GetOrDefineLiteralNumber(10);
            ScalarMinusTen = GetOrDefineLiteralNumber(-10);
            ScalarPi = GetOrDefineSymbolicNumber(SymbolicNumberNames.Pi, Math.PI);
            ScalarE = GetOrDefineSymbolicNumber(SymbolicNumberNames.E, Math.E);

            //These must be initialized after all other members
            SymbolicScalarProcessor = new ScalarAlgebraSymbolicExpressionProcessor(this);
            FunctionHeadSpecsFactory = new SymbolicFunctionHeadSpecsFactory(this);
            DefaultEvaluator = this.CreateAngouriMathEvaluator();
        }

        public SymbolicContext(SymbolicContextOptions options)
            : this()
        {
            ContextOptions.SetOptions(options);
        }

        public SymbolicContext([NotNull] ISymbolicExpressionEvaluator expressionEvaluator)
            : this()
        {
            SymbolicEvaluator = expressionEvaluator;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private string GetNewSymbolName()
        {
            _tempNamesIndex++;

            return $"{DefaultSymbolName}_{_tempNamesIndex}";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal int GetNextAtomicExpressionId()
        {
            _atomicId++;

            return _atomicId;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal int GetNextComputationOrder()
        {
            _computationOrder++;

            return _computationOrder;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return true;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ISymbolicNumber> GetNumbers()
        {
            return _numbersDictionary
                .Values
                .OrderBy(v => v.AtomicExpressionId);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ISymbolicVariable> GetVariables()
        {
            return GetParameterVariables()
                .Cast<ISymbolicVariable>()
                .Concat(GetComputedVariables());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ISymbolicVariableParameter> GetParameterVariables()
        {
            return _parametersVariablesDictionary
                .Values
                .OrderBy(v => v.AtomicExpressionId);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ISymbolicExpressionAtomicIndependent> GetIndependentAtomics()
        {
            return GetNumbers()
                .Cast<ISymbolicExpressionAtomicIndependent>()
                .Concat(GetParameterVariables());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ISymbolicVariableComputed> GetComputedVariables()
        {
            return _computedVariablesDictionary
                .Values
                .OrderBy(v => v.ComputationOrder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ISymbolicVariableComputed> GetIntermediateVariables()
        {
            return GetComputedVariables()
                .Where(s => s.IsIntermediateVariable);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ISymbolicVariableComputed> GetOutputVariables()
        {
            return GetComputedVariables()
                .Where(s => s.IsOutputVariable);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ISymbolicExpressionAtomic> GetAtomics()
        {
            return GetNumbers()
                .Cast<ISymbolicExpressionAtomic>()
                .Concat(GetParameterVariables())
                .Concat(GetComputedVariables());
        }

        /// <summary>
        /// The maximum number of temporary target variables required for the
        /// computations in this block
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetTargetTempVarsCount()
        {
            return GetIntermediateVariables().Any()
                ? GetIntermediateVariables().Max(item => item.NameIndex) + 1
                : 0;
        }


        public bool TryGetAtomic(string internalName, out ISymbolicExpressionAtomic atomic)
        {
            if (_numbersDictionary.TryGetValue(internalName, out var constantAtomic))
            {
                atomic = constantAtomic;
                return true;
            }

            if (_parametersVariablesDictionary.TryGetValue(internalName, out var parameterAtomic))
            {
                atomic = parameterAtomic;
                return true;
            }

            if (_computedVariablesDictionary.TryGetValue(internalName, out var computedAtomic))
            {
                atomic = computedAtomic;
                return true;
            }

            atomic = null;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetNumber(string numberText, out ISymbolicNumber number)
        {
            return _numbersDictionary.TryGetValue(numberText, out number);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetParameterVariable(string internalName, out ISymbolicVariableParameter variable)
        {
            return _parametersVariablesDictionary.TryGetValue(internalName, out variable);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetComputedVariable(string internalName, out ISymbolicVariableComputed variable)
        {
            return _computedVariablesDictionary.TryGetValue(internalName, out variable);
        }

        public bool TryGetVariable(string internalName, out ISymbolicVariable variable)
        {
            if (_parametersVariablesDictionary.TryGetValue(internalName, out var parameterVariable))
            {
                variable = parameterVariable;
                return true;
            }

            if (_computedVariablesDictionary.TryGetValue(internalName, out var computedVariable))
            {
                variable = computedVariable;
                return true;
            }

            variable = null;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ISymbolicExpressionAtomic> GetAtomicsByRhsExpressionText(string rhsExpressionText)
        {
            return GetAtomics().Where(expr => 
                expr.RhsExpressionText == rhsExpressionText
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ISymbolicNumber> GetNumbers(string numberText)
        {
            return _numbersDictionary.Values.Where(expr => 
                expr.NumberHeadSpecs.NumberText == numberText
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ISymbolicVariableComputed> GetComputedVariablesByRhsExpressionText(string rhsExpressionText)
        {
            return _computedVariablesDictionary.Values.Where(expr => 
                expr.RhsExpressionText == rhsExpressionText
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetVariableValue(string internalName, double number)
        {
            var variable = GetVariable(internalName);

            variable.SetRhsExpressionValue(number);
        }

        public ISymbolicExpressionAtomic GetAtomic(string internalName)
        {
            if (_numbersDictionary.TryGetValue(internalName, out var constantAtomic))
                return constantAtomic;

            if (_parametersVariablesDictionary.TryGetValue(internalName, out var parameterVariable))
                return parameterVariable;

            if (_computedVariablesDictionary.TryGetValue(internalName, out var computedVariable))
                return computedVariable;

            throw new KeyNotFoundException(internalName);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicNumber GetNumber(string numberText)
        {
            if (_numbersDictionary.TryGetValue(numberText, out var constantAtomic))
                return constantAtomic;
            
            throw new KeyNotFoundException(numberText);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicVariableParameter GetParameterVariable(string internalName)
        {
            if (_parametersVariablesDictionary.TryGetValue(internalName, out var variable))
                return variable;

            throw new KeyNotFoundException(internalName);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicVariableComputed GetComputedVariable(string internalName)
        {
            if (_computedVariablesDictionary.TryGetValue(internalName, out var variable))
                return variable;

            throw new KeyNotFoundException(internalName);
        }

        public ISymbolicVariable GetVariable(string internalName)
        {
            if (_parametersVariablesDictionary.TryGetValue(internalName, out var parameterVariable))
                return parameterVariable;

            if (_computedVariablesDictionary.TryGetValue(internalName, out var computedVariable))
                return computedVariable;

            throw new KeyNotFoundException(internalName);
        }
        
        public ISymbolicNumber GetOrDefineLiteralNumber(float numberValue)
        {
            var numberText = numberValue.ToString("G");
            if (_numbersDictionary.TryGetValue(numberText, out var atomic))
                return atomic;

            //Make sure there are no duplicates in the other two dictionaries
            if (_parametersVariablesDictionary.ContainsKey(numberText))
                throw new InvalidOperationException();

            if (_computedVariablesDictionary.ContainsKey(numberText))
                throw new InvalidOperationException();

            atomic = SymbolicNumber.Create(
                this, 
                numberValue
            );

            _numbersDictionary.Add(
                numberText, 
                atomic
            );

            return atomic;
        }

        public ISymbolicNumber GetOrDefineLiteralNumber(double numberValue)
        {
            var numberText = numberValue.ToString("G");
            if (_numbersDictionary.TryGetValue(numberText, out var atomic))
                return atomic;

            //Make sure there are no duplicates in the other two dictionaries
            if (_parametersVariablesDictionary.ContainsKey(numberText))
                throw new InvalidOperationException();

            if (_computedVariablesDictionary.ContainsKey(numberText))
                throw new InvalidOperationException();

            atomic = SymbolicNumber.Create(
                this, 
                numberValue
            );

            _numbersDictionary.Add(
                numberText, 
                atomic
            );

            return atomic;
        }

        public ISymbolicNumber GetOrDefineLiteralNumber(int numberValue)
        {
            var numberText = numberValue.ToString();
            if (_numbersDictionary.TryGetValue(numberText, out var atomic))
                return atomic;

            //Make sure there are no duplicates in the other two dictionaries
            if (_parametersVariablesDictionary.ContainsKey(numberText))
                throw new InvalidOperationException();

            if (_computedVariablesDictionary.ContainsKey(numberText))
                throw new InvalidOperationException();

            atomic = SymbolicNumber.Create(
                this, 
                numberValue
            );

            _numbersDictionary.Add(
                numberText, 
                atomic
            );

            return atomic;
        }

        public ISymbolicNumber GetOrDefineLiteralNumber(uint numberValue)
        {
            var numberText = numberValue.ToString();
            if (_numbersDictionary.TryGetValue(numberText, out var atomic))
                return atomic;

            //Make sure there are no duplicates in the other two dictionaries
            if (_parametersVariablesDictionary.ContainsKey(numberText))
                throw new InvalidOperationException();

            if (_computedVariablesDictionary.ContainsKey(numberText))
                throw new InvalidOperationException();

            atomic = SymbolicNumber.Create(
                this, 
                numberValue
            );

            _numbersDictionary.Add(
                numberText, 
                atomic
            );

            return atomic;
        }

        public ISymbolicNumber GetOrDefineLiteralNumber(long numberValue)
        {
            var numberText = numberValue.ToString();
            if (_numbersDictionary.TryGetValue(numberText, out var atomic))
                return atomic;

            //Make sure there are no duplicates in the other two dictionaries
            if (_parametersVariablesDictionary.ContainsKey(numberText))
                throw new InvalidOperationException();

            if (_computedVariablesDictionary.ContainsKey(numberText))
                throw new InvalidOperationException();

            atomic = SymbolicNumber.Create(
                this, 
                numberValue
            );

            _numbersDictionary.Add(
                numberText, 
                atomic
            );

            return atomic;
        }

        public ISymbolicNumber GetOrDefineLiteralNumber(ulong numberValue)
        {
            var numberText = numberValue.ToString();
            if (_numbersDictionary.TryGetValue(numberText, out var atomic))
                return atomic;

            //Make sure there are no duplicates in the other two dictionaries
            if (_parametersVariablesDictionary.ContainsKey(numberText))
                throw new InvalidOperationException();

            if (_computedVariablesDictionary.ContainsKey(numberText))
                throw new InvalidOperationException();

            atomic = SymbolicNumber.Create(
                this, 
                numberValue
            );

            _numbersDictionary.Add(
                numberText, 
                atomic
            );

            return atomic;
        }

        public ISymbolicNumber GetOrDefineRationalNumber(long numerator, long denominator)
        {
            var numberText = 
                SymbolicNumber.GetRationalNumberText(numerator, denominator);

            if (_numbersDictionary.TryGetValue(numberText, out var atomic))
                return atomic;

            //Make sure there are no duplicates in the other two dictionaries
            if (_parametersVariablesDictionary.ContainsKey(numberText))
                throw new InvalidOperationException();

            if (_computedVariablesDictionary.ContainsKey(numberText))
                throw new InvalidOperationException();

            atomic = SymbolicNumber.CreateRational(
                this, 
                numerator, 
                denominator
            );

            _numbersDictionary.Add(
                numberText, 
                atomic
            );

            return atomic;
        }

        public ISymbolicNumber GetOrDefineSymbolicNumber(string numberText, double numberValue)
        {
            if (_numbersDictionary.TryGetValue(numberText, out var atomic))
                return atomic;

            //Make sure there are no duplicates in the other two dictionaries
            if (_parametersVariablesDictionary.ContainsKey(numberText))
                throw new InvalidOperationException();

            if (_computedVariablesDictionary.ContainsKey(numberText))
                throw new InvalidOperationException();

            atomic = SymbolicNumber.CreateSymbolic(
                this, 
                numberText,
                numberValue
            );

            _numbersDictionary.Add(
                numberText, 
                atomic
            );

            return atomic;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicNumber GetOrDefineNumber(ISymbolicHeadSpecsNumber headSpecs)
        {
            return headSpecs switch
            {
                SymbolicHeadSpecsNumberFloat32 numberHeadSpecsFloat32 => 
                    GetOrDefineLiteralNumber(numberHeadSpecsFloat32.NumberFloat32Value),

                SymbolicHeadSpecsNumberFloat64 numberHeadSpecsFloat64 => 
                    GetOrDefineLiteralNumber(numberHeadSpecsFloat64.NumberFloat64Value),

                SymbolicHeadSpecsNumberInt32 numberHeadSpecsInt32 => 
                    GetOrDefineLiteralNumber(numberHeadSpecsInt32.NumberInt32Value),

                SymbolicHeadSpecsNumberUInt32 numberHeadSpecsUInt32 => 
                    GetOrDefineLiteralNumber(numberHeadSpecsUInt32.NumberUInt32Value),

                SymbolicHeadSpecsNumberInt64 numberHeadSpecsInt64 => 
                    GetOrDefineLiteralNumber(numberHeadSpecsInt64.NumberInt64Value),

                SymbolicHeadSpecsNumberUInt64 numberHeadSpecsUInt64 => 
                    GetOrDefineLiteralNumber(numberHeadSpecsUInt64.NumberUInt64Value),

                SymbolicHeadSpecsNumberRational numberHeadSpecsRational => 
                    GetOrDefineRationalNumber(numberHeadSpecsRational.Numerator, numberHeadSpecsRational.Denominator),

                SymbolicHeadSpecsNumberSymbolic numberHeadSpecsSymbolic => 
                    GetOrDefineSymbolicNumber(numberHeadSpecsSymbolic.NumberText, numberHeadSpecsSymbolic.NumberFloat64Value),

                _ => throw new InvalidOperationException()
            };
        }

        public ISymbolicVariableParameter GetOrDefineParameterVariable(string parameterName)
        {
            if (_parametersVariablesDictionary.TryGetValue(parameterName, out var atomic))
                return atomic;

            //Make sure there are no duplicates in the other two dictionaries
            if (_numbersDictionary.ContainsKey(parameterName))
                throw new InvalidOperationException();

            if (_computedVariablesDictionary.ContainsKey(parameterName))
                throw new InvalidOperationException();

            atomic = SymbolicVariableParameter.Create(
                this, 
                parameterName
            );

            _parametersVariablesDictionary.Add(
                parameterName,
                atomic
            );

            //_rhsExpressionsTextDictionary.Add(
            //    parameterName,
            //    atomic
            //);

            return atomic;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicVariableComputed GetOrDefineComputedVariable(Func<ISymbolicExpression, ISymbolicExpression> computingFunc, ISymbolicExpressionAtomic dependsOnAtomic)
        {
            var rhsScalarValue = computingFunc(
                dependsOnAtomic.GetScalarValue(MergeExpressions)
            );

            return GetOrDefineComputedVariable(
                rhsScalarValue
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicVariableComputed GetOrDefineComputedVariable(Func<ISymbolicExpression, ISymbolicExpression, ISymbolicExpression> computingFunc, ISymbolicExpressionAtomic dependsOnAtomic1, ISymbolicExpressionAtomic dependsOnAtomic2)
        {
            var rhsScalarValue = computingFunc(
                dependsOnAtomic1.GetScalarValue(MergeExpressions),
                dependsOnAtomic2.GetScalarValue(MergeExpressions)
            );

            return GetOrDefineComputedVariable(
                rhsScalarValue
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicVariableComputed GetOrDefineComputedVariable(Func<ISymbolicExpression, ISymbolicExpression, ISymbolicExpression, ISymbolicExpression> computingFunc, ISymbolicExpressionAtomic dependsOnAtomic1, ISymbolicExpressionAtomic dependsOnAtomic2, ISymbolicExpressionAtomic dependsOnAtomic3)
        {
            var rhsScalarValue = computingFunc(
                dependsOnAtomic1.GetScalarValue(MergeExpressions),
                dependsOnAtomic2.GetScalarValue(MergeExpressions),
                dependsOnAtomic3.GetScalarValue(MergeExpressions)
            );

            return GetOrDefineComputedVariable(
                rhsScalarValue
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicVariableComputed GetOrDefineComputedVariable(Func<IEnumerable<ISymbolicExpression>, ISymbolicExpression> computingFunc, IEnumerable<ISymbolicExpressionAtomic> dependsOnAtomics)
        {
            var atomicsArray = 
                dependsOnAtomics.ToArray();

            var rhsScalarValue = computingFunc(
                atomicsArray.Select(atomic => 
                    atomic.GetScalarValue(MergeExpressions)
                )
            );

            return GetOrDefineComputedVariable(
                rhsScalarValue
            );
        }

        public ISymbolicVariableComputed GetOrDefineComputedVariable(ISymbolicExpression rhsExpression)
        {
            var rhsExpressionText = 
                SymbolicScalarProcessor.ToText(rhsExpression);

            var computedVariable = 
                _computedVariablesDictionary
                    .Values
                    .FirstOrDefault(v => 
                        v.RhsExpressionText == rhsExpressionText
                    );

            if (computedVariable is not null)
                return computedVariable;

            var internalName = GetNewSymbolName();

            //Make sure there are no duplicate names in the other two dictionaries
            if (_numbersDictionary.ContainsKey(internalName))
                throw new InvalidOperationException();

            if (_parametersVariablesDictionary.ContainsKey(internalName))
                throw new InvalidOperationException();

            computedVariable = SymbolicVariableComputed.Create(
                this,
                internalName,
                rhsExpression
            );

            _computedVariablesDictionary.Add(
                internalName, 
                computedVariable
            );

            return computedVariable;
        }

        public ISymbolicVariableComputed DefineSubExpressionVariable(ISymbolicExpression rhsExpression, bool isUsedOnce)
        {
            var internalName = GetNewSymbolName();

            //Make sure there are no duplicate names in the other two dictionaries
            if (_numbersDictionary.ContainsKey(internalName))
                throw new InvalidOperationException();

            if (_parametersVariablesDictionary.ContainsKey(internalName))
                throw new InvalidOperationException();

            var computedVariable = 
                SymbolicVariableComputed.CreateFactoredSubExpression(
                    this,
                    internalName,
                    rhsExpression,
                    isUsedOnce
                );

            _computedVariablesDictionary.Add(
                internalName, 
                computedVariable
            );

            return computedVariable;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveNotUsedNumbers()
        {
            var numbersList = 
                _numbersDictionary
                    .Values
                    .Where(n => !n.HasDependingVariables)
                    .Select(n => n.NumberText)
                    .ToArray();

            foreach (var key in numbersList)
                _numbersDictionary.Remove(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveNotUsedParameterVariables()
        {
            var parameterVariableNamesList =
                _parametersVariablesDictionary
                    .Values
                    .Where(v => !v.HasDependingVariables)
                    .Select(v => v.InternalName)
                    .ToArray();

            foreach (var key in parameterVariableNamesList)
                _parametersVariablesDictionary.Remove(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveNotUsedComputedVariables()
        {
            var computedVariableNamesList =
                _computedVariablesDictionary
                    .Values
                    .Where(v => !v.HasDependingVariables)
                    .Select(v => v.InternalName)
                    .ToArray();

            foreach (var key in computedVariableNamesList)
                _computedVariablesDictionary.Remove(key);
        }

        public void RemoveNotUsedComputedVariables(IReadOnlyList<string> internalNamesList)
        {
            foreach (var internalName in internalNamesList)
            {
                if (!_computedVariablesDictionary.TryGetValue(internalName, out var computedVariable))
                    continue;

                if (computedVariable.HasDependingVariables)
                    continue;

                _computedVariablesDictionary.Remove(internalName);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveNotUsedAtomics()
        {
            RemoveNotUsedNumbers();

            RemoveNotUsedParameterVariables();

            RemoveNotUsedComputedVariables();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpressionAtomic Add(ISymbolicExpressionAtomic scalar1, ISymbolicExpressionAtomic scalar2)
        {
            return GetOrDefineComputedVariable(
                SymbolicScalarProcessor.Add,
                scalar1,
                scalar2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpressionAtomic Subtract(ISymbolicExpressionAtomic scalar1, ISymbolicExpressionAtomic scalar2)
        {
            return GetOrDefineComputedVariable(
                SymbolicScalarProcessor.Subtract,
                scalar1,
                scalar2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpressionAtomic Times(ISymbolicExpressionAtomic scalar1, ISymbolicExpressionAtomic scalar2)
        {
            return GetOrDefineComputedVariable(
                SymbolicScalarProcessor.Times,
                scalar1,
                scalar2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpressionAtomic NegativeTimes(ISymbolicExpressionAtomic scalar1, ISymbolicExpressionAtomic scalar2)
        {
            return GetOrDefineComputedVariable(
                SymbolicScalarProcessor.NegativeTimes,
                scalar1,
                scalar2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpressionAtomic Divide(ISymbolicExpressionAtomic scalar1, ISymbolicExpressionAtomic scalar2)
        {
            return GetOrDefineComputedVariable(
                SymbolicScalarProcessor.Divide,
                scalar1,
                scalar2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpressionAtomic NegativeDivide(ISymbolicExpressionAtomic scalar1, ISymbolicExpressionAtomic scalar2)
        {
            return GetOrDefineComputedVariable(
                SymbolicScalarProcessor.NegativeDivide,
                scalar1,
                scalar2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpressionAtomic Positive(ISymbolicExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                SymbolicScalarProcessor.Positive,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpressionAtomic Negative(ISymbolicExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                SymbolicScalarProcessor.Negative,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpressionAtomic Inverse(ISymbolicExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                SymbolicScalarProcessor.Inverse,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpressionAtomic Abs(ISymbolicExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                SymbolicScalarProcessor.Abs,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpressionAtomic Sqrt(ISymbolicExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                SymbolicScalarProcessor.Sqrt,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpressionAtomic SqrtOfAbs(ISymbolicExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                SymbolicScalarProcessor.SqrtOfAbs,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpressionAtomic Exp(ISymbolicExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                SymbolicScalarProcessor.Exp,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpressionAtomic LogE(ISymbolicExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                SymbolicScalarProcessor.LogE,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpressionAtomic Log2(ISymbolicExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                SymbolicScalarProcessor.Log2,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpressionAtomic Log10(ISymbolicExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                SymbolicScalarProcessor.Log10,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpressionAtomic Power(ISymbolicExpressionAtomic baseScalar, ISymbolicExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                SymbolicScalarProcessor.Power,
                baseScalar,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpressionAtomic Log(ISymbolicExpressionAtomic baseScalar, ISymbolicExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                SymbolicScalarProcessor.Log,
                baseScalar,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpressionAtomic Cos(ISymbolicExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                SymbolicScalarProcessor.Cos,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpressionAtomic Sin(ISymbolicExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                SymbolicScalarProcessor.Sin,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpressionAtomic Tan(ISymbolicExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                SymbolicScalarProcessor.Tan,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpressionAtomic ArcCos(ISymbolicExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                SymbolicScalarProcessor.ArcCos,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpressionAtomic ArcSin(ISymbolicExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                SymbolicScalarProcessor.ArcSin,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpressionAtomic ArcTan(ISymbolicExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                SymbolicScalarProcessor.ArcTan,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpressionAtomic ArcTan2(ISymbolicExpressionAtomic scalarX, ISymbolicExpressionAtomic scalarY)
        {
            return GetOrDefineComputedVariable(
                SymbolicScalarProcessor.ArcTan2,
                scalarX,
                scalarY
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpressionAtomic Cosh(ISymbolicExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                SymbolicScalarProcessor.Cosh,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpressionAtomic Sinh(ISymbolicExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                SymbolicScalarProcessor.Sinh,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpressionAtomic Tanh(ISymbolicExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                SymbolicScalarProcessor.Tanh,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid(ISymbolicExpressionAtomic scalar)
        {
            return SymbolicScalarProcessor.IsValid(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero(ISymbolicExpressionAtomic scalar)
        {
            return SymbolicScalarProcessor.IsZero(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero(ISymbolicExpressionAtomic scalar, bool nearZeroFlag)
        {
            return SymbolicScalarProcessor.IsZero(scalar, nearZeroFlag);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearZero(ISymbolicExpressionAtomic scalar)
        {
            return SymbolicScalarProcessor.IsNearZero(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotZero(ISymbolicExpressionAtomic scalar)
        {
            return SymbolicScalarProcessor.IsNotZero(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotZero(ISymbolicExpressionAtomic scalar, bool nearZeroFlag)
        {
            return SymbolicScalarProcessor.IsNotZero(scalar, nearZeroFlag);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearZero(ISymbolicExpressionAtomic scalar)
        {
            return SymbolicScalarProcessor.IsNotNearZero(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsPositive(ISymbolicExpressionAtomic scalar)
        {
            return SymbolicScalarProcessor.IsPositive(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNegative(ISymbolicExpressionAtomic scalar)
        {
            return SymbolicScalarProcessor.IsNegative(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotPositive(ISymbolicExpressionAtomic scalar)
        {
            return SymbolicScalarProcessor.IsNotPositive(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNegative(ISymbolicExpressionAtomic scalar)
        {
            return SymbolicScalarProcessor.IsNotNegative(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearPositive(ISymbolicExpressionAtomic scalar)
        {
            return SymbolicScalarProcessor.IsNotNearPositive(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearNegative(ISymbolicExpressionAtomic scalar)
        {
            return SymbolicScalarProcessor.IsNotNearNegative(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearZero(double scalar)
        {
            return scalar > -ZeroEpsilon && scalar < ZeroEpsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsPositive(double scalar)
        {
            return scalar > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNegative(double scalar)
        {
            return scalar < 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearPositive(double scalar)
        {
            return scalar < -ZeroEpsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearNegative(double scalar)
        {
            return scalar > ZeroEpsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpressionAtomic GetScalarFromText(string text)
        {
            return (ISymbolicExpressionAtomic) SymbolicScalarProcessor.GetScalarFromText(text);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpressionAtomic GetScalarFromNumber(int value)
        {
            return GetOrDefineLiteralNumber(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpressionAtomic GetScalarFromNumber(uint value)
        {
            return GetOrDefineLiteralNumber(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpressionAtomic GetScalarFromNumber(long value)
        {
            return GetOrDefineLiteralNumber(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpressionAtomic GetScalarFromNumber(ulong value)
        {
            return GetOrDefineLiteralNumber(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpressionAtomic GetScalarFromNumber(float value)
        {
            return GetOrDefineLiteralNumber(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpressionAtomic GetScalarFromNumber(double value)
        {
            return GetOrDefineLiteralNumber(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpressionAtomic GetScalarFromRational(long numerator, long denominator)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ISymbolicExpressionAtomic GetScalarFromRandom(Random randomGenerator, double minValue, double maxValue)
        {
            var value = 
                minValue + (maxValue - minValue) * randomGenerator.NextDouble();

            return GetOrDefineLiteralNumber((long) value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToText(ISymbolicExpressionAtomic scalar)
        {
            return scalar.ToString();
        }


        public void ClearDependencyData()
        {
            foreach (var number in _numbersDictionary.Values)
                number.ClearDependencyData();

            foreach (var parameterVariable in _parametersVariablesDictionary.Values)
                parameterVariable.ClearDependencyData();

            foreach (var computedVariable in _computedVariablesDictionary.Values)
                computedVariable.ClearDependencyData();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ResetComputedVariables(IEnumerable<ISymbolicVariableComputed> computedVariablesList)
        {
            _computedVariablesDictionary.Clear();

            foreach (var computedVariable in computedVariablesList)
                _computedVariablesDictionary.Add(
                    computedVariable.InternalName, 
                    computedVariable
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SimplifyRhsExpressions()
        {
            var expressionEvaluator = SymbolicEvaluator;

            foreach (var computedVariable in GetComputedVariables())
                computedVariable.ResetRhsExpression(
                    expressionEvaluator.Simplify(computedVariable.RhsExpression)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void OptimizeContext()
        {
            SimplifyRhsExpressions();

            var inputsWithTestValues =
                GetParameterVariables()
                    .ToDictionary(
                        binding => binding.InternalName,
                        binding => binding
                    );

            //Optimize low-level macro computations
            SymbolicContextOptimizer.Process(this, inputsWithTestValues);
        }

        
        /// <summary>
        /// The statistics related to the variables and computations in this block
        /// </summary>
        public Dictionary<string, string> GetStatistics()
        {
            var stats = new Dictionary<string, string>();

            var inputVarsTotalCount = GetParameterVariables().Count();
            var inputVarsUsedCount = GetParameterVariables().Count(inputVar => inputVar.HasDependingVariables);
            var inputVarsUnUsedCount = inputVarsTotalCount - inputVarsUsedCount;

            stats.Add("Used Input Variables: ", inputVarsUsedCount.ToString());
            stats.Add("Unused Input Variables: ", inputVarsUnUsedCount.ToString());
            stats.Add("Total Input Variables: ", inputVarsTotalCount.ToString());

            var tempVarsCount = GetIntermediateVariables().Count();
            var tempVarsSubExprCount = GetIntermediateVariables().Count(item => item.IsFactoredSubExpression);
            var tempVarsNonSubExprCount = tempVarsCount - tempVarsSubExprCount;

            stats.Add("Common Subexpressions Temp Variables: ", tempVarsSubExprCount.ToString());
            stats.Add("Generated Temp Variables: ", tempVarsNonSubExprCount.ToString());
            stats.Add("Total Temp Variables: ", tempVarsCount.ToString());

            stats.Add("Total Output Variables: ", GetOutputVariables().Count().ToString());

            stats.Add("Total Computed Variables: ", GetComputedVariables().Count().ToString());

            stats.Add("Target Temp Variables: ", GetTargetTempVarsCount().ToString());

            var computationsCountTotal =
                GetComputedVariables()
                    .Select(computedVar => computedVar.RhsExpression.ComputationsCount)
                    .Sum();

            var computationsCountAverage =
                computationsCountTotal / (double) GetComputedVariables().Count();

            stats.Add("Avg. Computations Count: ", computationsCountAverage.ToString("0.000"));
            stats.Add("Total Computations Count: ", computationsCountTotal.ToString());

            var memReadsCountTotal =
                GetComputedVariables()
                    .Select(computedVar => computedVar.RhsVariables.Count())
                    .Sum();

            var memReadsCountAverage =
                memReadsCountTotal / (double) GetComputedVariables().Count();

            stats.Add("Avg. Memory Reads: ", memReadsCountAverage.ToString("0.000"));
            stats.Add("Total Memory Reads: ", memReadsCountTotal.ToString());

            return stats;
        }

        /// <summary>
        /// The statistics related to the variables and computations in this block as a single string
        /// </summary>
        /// <returns></returns>
        public string GetStatisticsReport()
        {
            var s = new StringBuilder();

            var inputVarsTotalCount = GetParameterVariables().Count();
            var inputVarsUsedCount = GetParameterVariables().Count(inputVar => inputVar.HasDependingVariables);
            var inputVarsUnUsedCount = inputVarsTotalCount - inputVarsUsedCount;

            s.Append("Input Variables: ")
                .Append(inputVarsUsedCount)
                .Append(" used, ")
                .Append(inputVarsUnUsedCount)
                .Append(" not used, ")
                .Append(inputVarsTotalCount)
                .AppendLine(" total.")
                .AppendLine();

            var tempVarsCount = GetIntermediateVariables().Count();
            var tempVarsSubExprCount = GetIntermediateVariables().Count(item => item.IsFactoredSubExpression);
            var tempVarsNonSubExprCount = tempVarsCount - tempVarsSubExprCount;

            s.Append("Temp Variables: ")
                .Append(tempVarsSubExprCount)
                .Append(" sub-expressions, ")
                .Append(tempVarsNonSubExprCount)
                .Append(" generated temps, ")
                .Append(tempVarsCount)
                .AppendLine(" total.")
                .AppendLine();

            if (GetTargetTempVarsCount() > 0)
                s.Append("Target Temp Variables: ")
                    .Append(GetTargetTempVarsCount())
                    .AppendLine(" total.")
                    .AppendLine();

            var outputVarsCount = GetOutputVariables().Count();

            s.Append("Output Variables: ")
                .Append(outputVarsCount)
                .AppendLine(" total.")
                .AppendLine();

            var computationsCountTotal =
                GetComputedVariables()
                    .Select(computedVar => computedVar.RhsExpression.ComputationsCount)
                    .Sum();
            
            var computationsCountAverage = 
                computationsCountTotal / (double) GetComputedVariables().Count();

            s.Append("Computations: ")
                .Append(computationsCountAverage)
                .Append(" average, ")
                .Append(computationsCountTotal)
                .AppendLine(" total.")
                .AppendLine();

            var memReadsCountTotal =
                GetComputedVariables()
                    .Select(computedVar => computedVar.RhsVariables.Count())
                    .Sum();

            var memReadsCountAverage =
                memReadsCountTotal / (double) GetComputedVariables().Count();

            s.Append("Memory Reads: ")
                .Append(memReadsCountAverage)
                .Append(" average, ")
                .Append(memReadsCountTotal)
                .AppendLine(" total.")
                .AppendLine();

            s.Append("Memory Writes: ")
                .Append(GetComputedVariables().Count())
                .AppendLine(" total.")
                .AppendLine();

            return s.ToString();
        }

        public override string ToString()
        {
            var composer = new StringBuilder();

            composer.AppendLine(
                GetAtomics()
                    .Select(scalar => scalar.GetTextDescription())
                    .Concatenate(Environment.NewLine)
            );

            return composer.ToString();
        }
    }
}
