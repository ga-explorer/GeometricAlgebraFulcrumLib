using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Optimizer;
using GeometricAlgebraFulcrumLib.MetaProgramming.Evaluators;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.HeadSpecs;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Numbers;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Variables;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using TextComposerLib.Text;

// ReSharper disable MemberCanBePrivate.Global

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public sealed class MetaContext :
        ILinearAlgebraProcessor<IMetaExpressionAtomic>,
        IXGaProcessorContainer<IMetaExpressionAtomic>
    {
        private int _tempNamesIndex;

        private int _atomicId = -1;

        private int _computationOrder = -1;

        private readonly Dictionary<string, IMetaExpressionNumber> _numbersDictionary
            = new Dictionary<string, IMetaExpressionNumber>();

        private readonly Dictionary<string, IMetaExpressionVariableParameter> _parametersVariablesDictionary
            = new Dictionary<string, IMetaExpressionVariableParameter>();

        private readonly Dictionary<string, IMetaExpressionVariableComputed> _computedVariablesDictionary
            = new Dictionary<string, IMetaExpressionVariableComputed>();
        

        public double ZeroEpsilon { get; set; }
            = 1e-13d;

        public bool IsNumeric 
            => false;

        public bool IsSymbolic 
            => true;
        
        public IMetaExpressionAtomic ScalarZero { get; }

        public IMetaExpressionAtomic ScalarOne { get; }

        public IMetaExpressionAtomic ScalarMinusOne { get; }

        public IMetaExpressionAtomic ScalarTwo { get; }
        
        public IMetaExpressionAtomic ScalarMinusTwo { get; }
        
        public IMetaExpressionAtomic ScalarTen { get; }
        
        public IMetaExpressionAtomic ScalarMinusTen { get; }

        public IMetaExpressionAtomic ScalarPi { get; }

        public IMetaExpressionAtomic ScalarTwoPi { get; }

        public IMetaExpressionAtomic ScalarPiOver2 { get; }

        public IMetaExpressionAtomic ScalarE { get; }

        public IMetaExpressionAtomic ScalarDegreeToRadian { get; }
        
        public IMetaExpressionAtomic ScalarRadianToDegree { get; }

        public IScalarProcessor<IMetaExpressionAtomic> ScalarProcessor 
            => this;

        public XGaProcessor<IMetaExpressionAtomic> XGaProcessor { get; set; }
        
        //public IGeometricAlgebraProcessor<IMetaExpressionAtomic> GeometricProcessor { get; set; }
        
        public bool ContainsGeometricProcessor 
            => XGaProcessor is not null;

        public ScalarAlgebraMetaExpressionProcessor MetaExpressionProcessor { get; }

        public AngouriMathMetaExpressionEvaluator DefaultEvaluator { get; }

        private IMetaExpressionEvaluator _symbolicEvaluator;
        public IMetaExpressionEvaluator SymbolicEvaluator
        {
            get => _symbolicEvaluator ?? DefaultEvaluator;
            set => _symbolicEvaluator = value;
        }

        public MetaContextOptions ContextOptions { get; }
            = new MetaContextOptions();

        public MetaExpressionNumberFactory NumbersFactory { get; }

        public MetaExpressionParameterVariableFactory ParameterVariablesFactory { get; }

        public MetaExpressionComputedVariableFactory ComputedVariablesFactory { get; }

        public MetaExpressionFunctionHeadSpecsFactory FunctionHeadSpecsFactory { get; }

        public string DefaultSymbolName { get; set; } 
            = "tmpVar";

        public bool MergeExpressions { get; set; }


        //TODO: Hide the constructors and use static Create() methods to automatically assign 
        //TODO: geometric algebra processors if needed
        public MetaContext()
        {
            NumbersFactory = new MetaExpressionNumberFactory(this);
            ParameterVariablesFactory = new MetaExpressionParameterVariableFactory(this);
            ComputedVariablesFactory = new MetaExpressionComputedVariableFactory(this);

            ScalarZero = GetOrDefineLiteralNumber(0);
            ScalarOne = GetOrDefineLiteralNumber(1);
            ScalarMinusOne = GetOrDefineLiteralNumber(-1);
            ScalarTwo = GetOrDefineLiteralNumber(2);
            ScalarMinusTwo = GetOrDefineLiteralNumber(-2);
            ScalarTen = GetOrDefineLiteralNumber(10);
            ScalarMinusTen = GetOrDefineLiteralNumber(-10);
            ScalarPi = GetOrDefineSymbolicNumber(MetaExpressionNumberNames.Pi, Math.PI);
            ScalarTwoPi = GetOrDefineLiteralNumber(2 * Math.PI);
            ScalarPiOver2 = GetOrDefineLiteralNumber(0.5d * Math.PI);
            ScalarE = GetOrDefineSymbolicNumber(MetaExpressionNumberNames.E, Math.E);
            ScalarDegreeToRadian = GetOrDefineLiteralNumber(Math.PI / 180d);
            ScalarRadianToDegree = GetOrDefineLiteralNumber(180d / Math.PI);

            //These must be initialized after all other members
            MetaExpressionProcessor = new ScalarAlgebraMetaExpressionProcessor(this);
            FunctionHeadSpecsFactory = new MetaExpressionFunctionHeadSpecsFactory(this);
            DefaultEvaluator = this.CreateAngouriMathEvaluator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MetaContext(XGaProcessor<IMetaExpressionAtomic> geometricProcessor)
            : this()
        {
            AttachXGaProcessor(geometricProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MetaContext(MetaContextOptions options)
            : this()
        {
            ContextOptions.SetOptions(options);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MetaContext(IMetaExpressionEvaluator expressionEvaluator)
            : this()
        {
            SymbolicEvaluator = expressionEvaluator;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AttachXGaProcessor(XGaProcessor<IMetaExpressionAtomic> processor)
        {
            XGaProcessor = processor;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private string GetNewSymbolName()
        {
            _tempNamesIndex++;

            return $"{DefaultSymbolName}{_tempNamesIndex}";
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
        public IEnumerable<IMetaExpressionNumber> GetNumbers()
        {
            return _numbersDictionary
                .Values
                .OrderBy(v => v.AtomicExpressionId);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IMetaExpressionVariable> GetVariables()
        {
            return GetParameterVariables()
                .Cast<IMetaExpressionVariable>()
                .Concat(GetComputedVariables());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IMetaExpressionVariableParameter> GetParameterVariables()
        {
            return _parametersVariablesDictionary
                .Values
                .OrderBy(v => v.AtomicExpressionId);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IMetaExpressionAtomicIndependent> GetIndependentAtomics()
        {
            return GetNumbers()
                .Cast<IMetaExpressionAtomicIndependent>()
                .Concat(GetParameterVariables());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IMetaExpressionVariableComputed> GetComputedVariables()
        {
            return _computedVariablesDictionary
                .Values
                .OrderBy(v => v.ComputationOrder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IMetaExpressionVariableComputed> GetIntermediateVariables()
        {
            return GetComputedVariables()
                .Where(s => s.IsIntermediateVariable);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IMetaExpressionVariableComputed> GetOutputVariables()
        {
            return GetComputedVariables()
                .Where(s => s.IsOutputVariable);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IMetaExpressionAtomic> GetAtomics()
        {
            return GetNumbers()
                .Cast<IMetaExpressionAtomic>()
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


        public bool TryGetAtomic(string internalName, out IMetaExpressionAtomic atomic)
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
        public bool TryGetNumber(string numberText, out IMetaExpressionNumber number)
        {
            return _numbersDictionary.TryGetValue(numberText, out number);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetParameterVariable(string internalName, out IMetaExpressionVariableParameter variable)
        {
            return _parametersVariablesDictionary.TryGetValue(internalName, out variable);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetComputedVariable(string internalName, out IMetaExpressionVariableComputed variable)
        {
            return _computedVariablesDictionary.TryGetValue(internalName, out variable);
        }

        public bool TryGetVariable(string internalName, out IMetaExpressionVariable variable)
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
        public IEnumerable<IMetaExpressionAtomic> GetAtomicsByRhsExpressionText(string rhsExpressionText)
        {
            return GetAtomics().Where(expr => 
                expr.RhsExpressionText == rhsExpressionText
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IMetaExpressionNumber> GetNumbers(string numberText)
        {
            return _numbersDictionary.Values.Where(expr => 
                expr.NumberHeadSpecs.NumberText == numberText
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IMetaExpressionVariableComputed> GetComputedVariablesByRhsExpressionText(string rhsExpressionText)
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

        public IMetaExpressionAtomic GetAtomic(string internalName)
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
        public IMetaExpressionNumber GetNumber(string numberText)
        {
            if (_numbersDictionary.TryGetValue(numberText, out var constantAtomic))
                return constantAtomic;
            
            throw new KeyNotFoundException(numberText);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMetaExpressionVariableParameter GetParameterVariable(string internalName)
        {
            if (_parametersVariablesDictionary.TryGetValue(internalName, out var variable))
                return variable;

            throw new KeyNotFoundException(internalName);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMetaExpressionVariableComputed GetComputedVariable(string internalName)
        {
            if (_computedVariablesDictionary.TryGetValue(internalName, out var variable))
                return variable;

            throw new KeyNotFoundException(internalName);
        }

        public IMetaExpressionVariable GetVariable(string internalName)
        {
            if (_parametersVariablesDictionary.TryGetValue(internalName, out var parameterVariable))
                return parameterVariable;

            if (_computedVariablesDictionary.TryGetValue(internalName, out var computedVariable))
                return computedVariable;

            throw new KeyNotFoundException(internalName);
        }
        
        public IMetaExpressionNumber GetOrDefineLiteralNumber(float numberValue)
        {
            var numberText = numberValue.ToString("G");
            if (_numbersDictionary.TryGetValue(numberText, out var atomic))
                return atomic;

            //Make sure there are no duplicates in the other two dictionaries
            if (_parametersVariablesDictionary.ContainsKey(numberText))
                throw new InvalidOperationException();

            if (_computedVariablesDictionary.ContainsKey(numberText))
                throw new InvalidOperationException();

            atomic = MetaExpressionNumber.Create(
                this, 
                numberValue
            );

            _numbersDictionary.Add(
                numberText, 
                atomic
            );

            return atomic;
        }

        public IMetaExpressionNumber GetOrDefineLiteralNumber(double numberValue)
        {
            var numberText = numberValue.ToString("G");
            if (_numbersDictionary.TryGetValue(numberText, out var atomic))
                return atomic;

            //Make sure there are no duplicates in the other two dictionaries
            if (_parametersVariablesDictionary.ContainsKey(numberText))
                throw new InvalidOperationException();

            if (_computedVariablesDictionary.ContainsKey(numberText))
                throw new InvalidOperationException();

            atomic = MetaExpressionNumber.Create(
                this, 
                numberValue
            );

            _numbersDictionary.Add(
                numberText, 
                atomic
            );

            return atomic;
        }

        public IMetaExpressionNumber GetOrDefineLiteralNumber(int numberValue)
        {
            var numberText = numberValue.ToString();
            if (_numbersDictionary.TryGetValue(numberText, out var atomic))
                return atomic;

            //Make sure there are no duplicates in the other two dictionaries
            if (_parametersVariablesDictionary.ContainsKey(numberText))
                throw new InvalidOperationException();

            if (_computedVariablesDictionary.ContainsKey(numberText))
                throw new InvalidOperationException();

            atomic = MetaExpressionNumber.Create(
                this, 
                numberValue
            );

            _numbersDictionary.Add(
                numberText, 
                atomic
            );

            return atomic;
        }

        public IMetaExpressionNumber GetOrDefineLiteralNumber(uint numberValue)
        {
            var numberText = numberValue.ToString();
            if (_numbersDictionary.TryGetValue(numberText, out var atomic))
                return atomic;

            //Make sure there are no duplicates in the other two dictionaries
            if (_parametersVariablesDictionary.ContainsKey(numberText))
                throw new InvalidOperationException();

            if (_computedVariablesDictionary.ContainsKey(numberText))
                throw new InvalidOperationException();

            atomic = MetaExpressionNumber.Create(
                this, 
                numberValue
            );

            _numbersDictionary.Add(
                numberText, 
                atomic
            );

            return atomic;
        }

        public IMetaExpressionNumber GetOrDefineLiteralNumber(long numberValue)
        {
            var numberText = numberValue.ToString();
            if (_numbersDictionary.TryGetValue(numberText, out var atomic))
                return atomic;

            //Make sure there are no duplicates in the other two dictionaries
            if (_parametersVariablesDictionary.ContainsKey(numberText))
                throw new InvalidOperationException();

            if (_computedVariablesDictionary.ContainsKey(numberText))
                throw new InvalidOperationException();

            atomic = MetaExpressionNumber.Create(
                this, 
                numberValue
            );

            _numbersDictionary.Add(
                numberText, 
                atomic
            );

            return atomic;
        }

        public IMetaExpressionNumber GetOrDefineLiteralNumber(ulong numberValue)
        {
            var numberText = numberValue.ToString();
            if (_numbersDictionary.TryGetValue(numberText, out var atomic))
                return atomic;

            //Make sure there are no duplicates in the other two dictionaries
            if (_parametersVariablesDictionary.ContainsKey(numberText))
                throw new InvalidOperationException();

            if (_computedVariablesDictionary.ContainsKey(numberText))
                throw new InvalidOperationException();

            atomic = MetaExpressionNumber.Create(
                this, 
                numberValue
            );

            _numbersDictionary.Add(
                numberText, 
                atomic
            );

            return atomic;
        }

        public IMetaExpressionNumber GetOrDefineRationalNumber(long numerator, long denominator)
        {
            var numberText = 
                MetaExpressionNumber.GetRationalNumberText(numerator, denominator);

            if (_numbersDictionary.TryGetValue(numberText, out var atomic))
                return atomic;

            //Make sure there are no duplicates in the other two dictionaries
            if (_parametersVariablesDictionary.ContainsKey(numberText))
                throw new InvalidOperationException();

            if (_computedVariablesDictionary.ContainsKey(numberText))
                throw new InvalidOperationException();

            atomic = MetaExpressionNumber.CreateRational(
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

        public IMetaExpressionNumber GetOrDefineSymbolicNumber(string numberText, double numberValue)
        {
            if (_numbersDictionary.TryGetValue(numberText, out var atomic))
                return atomic;

            //Make sure there are no duplicates in the other two dictionaries
            if (_parametersVariablesDictionary.ContainsKey(numberText))
                throw new InvalidOperationException();

            if (_computedVariablesDictionary.ContainsKey(numberText))
                throw new InvalidOperationException();

            atomic = MetaExpressionNumber.CreateSymbolic(
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
        public IMetaExpressionNumber GetOrDefineNumber(IMetaExpressionHeadSpecsNumber headSpecs)
        {
            return headSpecs switch
            {
                MetaExpressionHeadSpecsNumberFloat32 numberHeadSpecsFloat32 => 
                    GetOrDefineLiteralNumber(numberHeadSpecsFloat32.NumberFloat32Value),

                MetaExpressionHeadSpecsNumberFloat64 numberHeadSpecsFloat64 => 
                    GetOrDefineLiteralNumber(numberHeadSpecsFloat64.NumberFloat64Value),

                MetaExpressionHeadSpecsNumberInt32 numberHeadSpecsInt32 => 
                    GetOrDefineLiteralNumber(numberHeadSpecsInt32.NumberInt32Value),

                MetaExpressionHeadSpecsNumberUInt32 numberHeadSpecsUInt32 => 
                    GetOrDefineLiteralNumber(numberHeadSpecsUInt32.NumberUInt32Value),

                MetaExpressionHeadSpecsNumberInt64 numberHeadSpecsInt64 => 
                    GetOrDefineLiteralNumber(numberHeadSpecsInt64.NumberInt64Value),

                MetaExpressionHeadSpecsNumberUInt64 numberHeadSpecsUInt64 => 
                    GetOrDefineLiteralNumber(numberHeadSpecsUInt64.NumberUInt64Value),

                MetaExpressionHeadSpecsNumberRational numberHeadSpecsRational => 
                    GetOrDefineRationalNumber(numberHeadSpecsRational.Numerator, numberHeadSpecsRational.Denominator),

                MetaExpressionHeadSpecsNumberSymbolic numberHeadSpecsSymbolic => 
                    GetOrDefineSymbolicNumber(numberHeadSpecsSymbolic.NumberText, numberHeadSpecsSymbolic.NumberFloat64Value),

                _ => throw new InvalidOperationException()
            };
        }

        public IMetaExpressionVariableParameter GetOrDefineParameterVariable(string parameterName)
        {
            if (_parametersVariablesDictionary.TryGetValue(parameterName, out var atomic))
                return atomic;

            //Make sure there are no duplicates in the other two dictionaries
            if (_numbersDictionary.ContainsKey(parameterName))
                throw new InvalidOperationException();

            if (_computedVariablesDictionary.ContainsKey(parameterName))
                throw new InvalidOperationException();

            atomic = MetaExpressionVariableParameter.Create(
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
        public IMetaExpressionVariableComputed GetOrDefineComputedVariable(Func<IMetaExpression, IMetaExpression> computingFunc, IMetaExpressionAtomic dependsOnAtomic)
        {
            var rhsScalarValue = computingFunc(
                dependsOnAtomic.GetScalarValue(MergeExpressions)
            );

            return GetOrDefineComputedVariable(
                rhsScalarValue
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMetaExpressionVariableComputed GetOrDefineComputedVariable(Func<IMetaExpression, IMetaExpression, IMetaExpression> computingFunc, IMetaExpressionAtomic dependsOnAtomic1, IMetaExpressionAtomic dependsOnAtomic2)
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
        public IMetaExpressionVariableComputed GetOrDefineComputedVariable(Func<IMetaExpression, IMetaExpression, IMetaExpression, IMetaExpression> computingFunc, IMetaExpressionAtomic dependsOnAtomic1, IMetaExpressionAtomic dependsOnAtomic2, IMetaExpressionAtomic dependsOnAtomic3)
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
        public IMetaExpressionVariableComputed GetOrDefineComputedVariable(Func<IEnumerable<IMetaExpression>, IMetaExpression> computingFunc, IEnumerable<IMetaExpressionAtomic> dependsOnAtomics)
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

        public IMetaExpressionVariableComputed GetOrDefineComputedVariable(IMetaExpression rhsExpression)
        {
            var rhsExpressionText = 
                MetaExpressionProcessor.ToText(rhsExpression);

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

            computedVariable = MetaExpressionVariableComputed.Create(
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

        public IMetaExpressionVariableComputed DefineSubExpressionVariable(IMetaExpression rhsExpression, bool isUsedOnce)
        {
            var internalName = GetNewSymbolName();

            //Make sure there are no duplicate names in the other two dictionaries
            if (_numbersDictionary.ContainsKey(internalName))
                throw new InvalidOperationException();

            if (_parametersVariablesDictionary.ContainsKey(internalName))
                throw new InvalidOperationException();

            var computedVariable = 
                MetaExpressionVariableComputed.CreateFactoredSubExpression(
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
        public IMetaExpressionAtomic Add(IMetaExpressionAtomic scalar1, IMetaExpressionAtomic scalar2)
        {
            return GetOrDefineComputedVariable(
                MetaExpressionProcessor.Add,
                scalar1,
                scalar2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMetaExpressionAtomic Subtract(IMetaExpressionAtomic scalar1, IMetaExpressionAtomic scalar2)
        {
            return GetOrDefineComputedVariable(
                MetaExpressionProcessor.Subtract,
                scalar1,
                scalar2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMetaExpressionAtomic Times(IMetaExpressionAtomic scalar1, IMetaExpressionAtomic scalar2)
        {
            return GetOrDefineComputedVariable(
                MetaExpressionProcessor.Times,
                scalar1,
                scalar2
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMetaExpressionAtomic Times(IntegerSign sign, IMetaExpressionAtomic scalar)
        {
            if (sign.IsZero) return ScalarZero;

            return sign.IsPositive
                ? scalar 
                : Negative(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMetaExpressionAtomic NegativeTimes(IMetaExpressionAtomic scalar1, IMetaExpressionAtomic scalar2)
        {
            return GetOrDefineComputedVariable(
                MetaExpressionProcessor.NegativeTimes,
                scalar1,
                scalar2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMetaExpressionAtomic Divide(IMetaExpressionAtomic scalar1, IMetaExpressionAtomic scalar2)
        {
            return GetOrDefineComputedVariable(
                MetaExpressionProcessor.Divide,
                scalar1,
                scalar2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMetaExpressionAtomic NegativeDivide(IMetaExpressionAtomic scalar1, IMetaExpressionAtomic scalar2)
        {
            return GetOrDefineComputedVariable(
                MetaExpressionProcessor.NegativeDivide,
                scalar1,
                scalar2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMetaExpressionAtomic Positive(IMetaExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                MetaExpressionProcessor.Positive,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMetaExpressionAtomic Negative(IMetaExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                MetaExpressionProcessor.Negative,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMetaExpressionAtomic Inverse(IMetaExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                MetaExpressionProcessor.Inverse,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMetaExpressionAtomic Sign(IMetaExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                MetaExpressionProcessor.Sign,
                scalar
            );
        }

        public IMetaExpressionAtomic UnitStep(IMetaExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                MetaExpressionProcessor.UnitStep,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMetaExpressionAtomic Abs(IMetaExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                MetaExpressionProcessor.Abs,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMetaExpressionAtomic Sqrt(IMetaExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                MetaExpressionProcessor.Sqrt,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMetaExpressionAtomic SqrtOfAbs(IMetaExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                MetaExpressionProcessor.SqrtOfAbs,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMetaExpressionAtomic Exp(IMetaExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                MetaExpressionProcessor.Exp,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMetaExpressionAtomic LogE(IMetaExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                MetaExpressionProcessor.LogE,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMetaExpressionAtomic Log2(IMetaExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                MetaExpressionProcessor.Log2,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMetaExpressionAtomic Log10(IMetaExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                MetaExpressionProcessor.Log10,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMetaExpressionAtomic Power(IMetaExpressionAtomic baseScalar, IMetaExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                MetaExpressionProcessor.Power,
                baseScalar,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMetaExpressionAtomic Log(IMetaExpressionAtomic baseScalar, IMetaExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                MetaExpressionProcessor.Log,
                baseScalar,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMetaExpressionAtomic Cos(IMetaExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                MetaExpressionProcessor.Cos,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMetaExpressionAtomic Sin(IMetaExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                MetaExpressionProcessor.Sin,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMetaExpressionAtomic Tan(IMetaExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                MetaExpressionProcessor.Tan,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMetaExpressionAtomic ArcCos(IMetaExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                MetaExpressionProcessor.ArcCos,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMetaExpressionAtomic ArcSin(IMetaExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                MetaExpressionProcessor.ArcSin,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMetaExpressionAtomic ArcTan(IMetaExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                MetaExpressionProcessor.ArcTan,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMetaExpressionAtomic ArcTan2(IMetaExpressionAtomic scalarX, IMetaExpressionAtomic scalarY)
        {
            return GetOrDefineComputedVariable(
                MetaExpressionProcessor.ArcTan2,
                scalarX,
                scalarY
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMetaExpressionAtomic Cosh(IMetaExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                MetaExpressionProcessor.Cosh,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMetaExpressionAtomic Sinh(IMetaExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                MetaExpressionProcessor.Sinh,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMetaExpressionAtomic Tanh(IMetaExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                MetaExpressionProcessor.Tanh,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMetaExpressionAtomic Sinc(IMetaExpressionAtomic scalar)
        {
            return IsZero(scalar) 
                ? ScalarOne 
                : Divide(Sin(scalar), scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid(IMetaExpressionAtomic scalar)
        {
            return MetaExpressionProcessor.IsValid(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsFiniteNumber(IMetaExpressionAtomic scalar)
        {
            return MetaExpressionProcessor.IsFiniteNumber(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero(IMetaExpressionAtomic scalar)
        {
            return MetaExpressionProcessor.IsZero(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero(IMetaExpressionAtomic scalar, bool nearZeroFlag)
        {
            return MetaExpressionProcessor.IsZero(scalar, nearZeroFlag);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearZero(IMetaExpressionAtomic scalar)
        {
            return MetaExpressionProcessor.IsNearZero(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotZero(IMetaExpressionAtomic scalar)
        {
            return MetaExpressionProcessor.IsNotZero(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotZero(IMetaExpressionAtomic scalar, bool nearZeroFlag)
        {
            return MetaExpressionProcessor.IsNotZero(scalar, nearZeroFlag);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearZero(IMetaExpressionAtomic scalar)
        {
            return MetaExpressionProcessor.IsNotNearZero(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsPositive(IMetaExpressionAtomic scalar)
        {
            return MetaExpressionProcessor.IsPositive(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNegative(IMetaExpressionAtomic scalar)
        {
            return MetaExpressionProcessor.IsNegative(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotPositive(IMetaExpressionAtomic scalar)
        {
            return MetaExpressionProcessor.IsNotPositive(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNegative(IMetaExpressionAtomic scalar)
        {
            return MetaExpressionProcessor.IsNotNegative(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearPositive(IMetaExpressionAtomic scalar)
        {
            return MetaExpressionProcessor.IsNotNearPositive(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearNegative(IMetaExpressionAtomic scalar)
        {
            return MetaExpressionProcessor.IsNotNearNegative(scalar);
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
        public IMetaExpressionAtomic GetScalarFromText(string text)
        {
            return (IMetaExpressionAtomic) MetaExpressionProcessor.GetScalarFromText(text);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMetaExpressionAtomic GetScalarFromNumber(int value)
        {
            return GetOrDefineLiteralNumber(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMetaExpressionAtomic GetScalarFromNumber(uint value)
        {
            return GetOrDefineLiteralNumber(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMetaExpressionAtomic GetScalarFromNumber(long value)
        {
            return GetOrDefineLiteralNumber(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMetaExpressionAtomic GetScalarFromNumber(ulong value)
        {
            return GetOrDefineLiteralNumber(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMetaExpressionAtomic GetScalarFromNumber(float value)
        {
            return GetOrDefineLiteralNumber(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMetaExpressionAtomic GetScalarFromNumber(double value)
        {
            return GetOrDefineLiteralNumber(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMetaExpressionAtomic GetScalarFromRational(long numerator, long denominator)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMetaExpressionAtomic GetScalarFromRandom(Random randomGenerator, double minValue, double maxValue)
        {
            var value = 
                minValue + (maxValue - minValue) * randomGenerator.NextDouble();

            return GetOrDefineLiteralNumber((long) value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToText(IMetaExpressionAtomic scalar)
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
        public void ResetComputedVariables(IEnumerable<IMetaExpressionVariableComputed> computedVariablesList)
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
            //var expressionEvaluator = SymbolicEvaluator;

            foreach (var computedVariable in GetComputedVariables())
            {
                var replaceDictionary = new Dictionary<string, IMetaExpression>();

                // Replace RHS computed variables with their assigned RHS expressions for some
                // cases like constants
                foreach (var rhsVariable in computedVariable.RhsComputedVariables)
                {
                    var rhsVariableExpression = rhsVariable.RhsExpression;

                    if (rhsVariableExpression is IMetaExpressionNumber)
                        replaceDictionary.Add(rhsVariable.InternalName, rhsVariableExpression);
                }

                foreach (var (variableName, newExpression) in replaceDictionary)
                    computedVariable.ReplaceRhsVariable(variableName, newExpression);

                // Apply symbolic simplification to the RHS expression
                computedVariable.SimplifyRhsExpression();

                //computedVariable.ResetRhsExpression(
                //    expressionEvaluator.Simplify(computedVariable.RhsExpression)
                //);
            }
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
            MetaContextOptimizer.Process(this, inputsWithTestValues);
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
