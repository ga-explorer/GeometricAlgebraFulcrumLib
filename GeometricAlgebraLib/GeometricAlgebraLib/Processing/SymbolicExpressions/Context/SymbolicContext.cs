using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using GeometricAlgebraLib.Processing.Scalars;
using GeometricAlgebraLib.Processing.SymbolicExpressions.Context.Optimizer;
using GeometricAlgebraLib.Processing.SymbolicExpressions.Factories;
using GeometricAlgebraLib.Processing.SymbolicExpressions.HeadSpecs;
using GeometricAlgebraLib.Processing.SymbolicExpressions.Numbers;
using GeometricAlgebraLib.Processing.SymbolicExpressions.Variables;
using TextComposerLib.Text;

namespace GeometricAlgebraLib.Processing.SymbolicExpressions.Context
{
    public sealed class SymbolicContext :
        IGaScalarProcessor<ISymbolicExpressionAtomic>
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


        public SymbolicFunctionHeadSpecsFactory FunctionHeadSpecsFactory { get; }


        public ISymbolicExpressionAtomic ZeroScalar { get; }

        public ISymbolicExpressionAtomic OneScalar { get; }
        
        public ISymbolicExpressionAtomic MinusOneScalar { get; }
        
        public ISymbolicExpressionAtomic PiScalar { get; }

        public IGaScalarProcessor<ISymbolicExpression> SymbolicExpressionProcessor { get; }

        public ISymbolicExpressionSimplifier ExpressionSimplifier { get; set; }

        public SymbolicContextOptions ContextOptions { get; }
            = new SymbolicContextOptions();

        public SymbolicNumbersFactory NumbersFactory { get; }

        public SymbolicParameterVariablesFactory ParameterVariablesFactory { get; }

        public SymbolicComputedVariablesFactory ComputedVariablesFactory { get; }

        public string DefaultSymbolName { get; set; } 
            = "tmpVar";

        public bool MergeExpressions { get; set; }

        public IEnumerable<ISymbolicNumber> Numbers
            => _numbersDictionary
                .Values
                .OrderBy(v => v.AtomicExpressionId);

        public IEnumerable<ISymbolicVariable> Variables
            => ParameterVariables
                .Cast<ISymbolicVariable>()
                .Concat(ComputedVariables);

        public IEnumerable<ISymbolicVariableParameter> ParameterVariables
            => _parametersVariablesDictionary
                .Values
                .OrderBy(v => v.AtomicExpressionId);

        public IEnumerable<ISymbolicExpressionAtomicIndependent> IndependentAtomics
            => Numbers
                .Cast<ISymbolicExpressionAtomicIndependent>()
                .Concat(ParameterVariables);

        public IEnumerable<ISymbolicVariableComputed> ComputedVariables
            => _computedVariablesDictionary
                .Values
                .OrderBy(v => v.ComputationOrder);

        public IEnumerable<ISymbolicVariableComputed> IntermediateVariables
            => ComputedVariables
                .Where(s => s.IsIntermediateVariable);

        public IEnumerable<ISymbolicVariableComputed> OutputVariables
            => ComputedVariables
                .Where(s => s.IsOutputVariable);

        public IEnumerable<ISymbolicExpressionAtomic> Atomics
            => Numbers
                .Cast<ISymbolicExpressionAtomic>()
                .Concat(ParameterVariables)
                .Concat(ComputedVariables);
        
        /// <summary>
        /// The maximum number of temporary target variables required for the
        /// computations in this block
        /// </summary>
        public int TargetTempVarsCount 
            => IntermediateVariables.Any()
                ? IntermediateVariables.Max(item => item.NameIndex) + 1
                : 0;


        public SymbolicContext()
        {
            NumbersFactory = new SymbolicNumbersFactory(this);
            ParameterVariablesFactory = new SymbolicParameterVariablesFactory(this);
            ComputedVariablesFactory = new SymbolicComputedVariablesFactory(this);

            ZeroScalar = GetOrDefineLiteralNumber(0);
            OneScalar = GetOrDefineLiteralNumber(1);
            MinusOneScalar = GetOrDefineLiteralNumber(-1);
            PiScalar = GetOrDefineSymbolicNumber("Pi", Math.PI); //TODO: This should be more dynamic

            //These must be initialized after all other members
            SymbolicExpressionProcessor = new GaScalarProcessorSymbolicExpression(this);
            FunctionHeadSpecsFactory = new SymbolicFunctionHeadSpecsFactory(this);
        }

        public SymbolicContext(SymbolicContextOptions options)
            : this()
        {
            ContextOptions.SetOptions(options);
        }


        public SymbolicContext([NotNull] ISymbolicExpressionSimplifier expressionReducer)
            : this()
        {
            ExpressionSimplifier = expressionReducer;
        }


        private string GetNewSymbolName()
        {
            _tempNamesIndex++;

            return $"{DefaultSymbolName}{_tempNamesIndex}";
        }

        internal int GetNextAtomicExpressionId()
        {
            _atomicId++;

            return _atomicId;
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

        public bool TryGetNumber(string numberText, out ISymbolicNumber number)
        {
            return _numbersDictionary.TryGetValue(numberText, out number);
        }

        public bool TryGetParameterVariable(string internalName, out ISymbolicVariableParameter variable)
        {
            return _parametersVariablesDictionary.TryGetValue(internalName, out variable);
        }

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


        public IEnumerable<ISymbolicExpressionAtomic> GetAtomicsByRhsExpressionText(string rhsExpressionText)
        {
            return Atomics.Where(expr => 
                expr.RhsExpressionText == rhsExpressionText
            );
        }

        public IEnumerable<ISymbolicNumber> GetNumbers(string numberText)
        {
            return _numbersDictionary.Values.Where(expr => 
                expr.NumberHeadSpecs.NumberText == numberText
            );
        }

        public IEnumerable<ISymbolicVariableComputed> GetComputedVariablesByRhsExpressionText(string rhsExpressionText)
        {
            return _computedVariablesDictionary.Values.Where(expr => 
                expr.RhsExpressionText == rhsExpressionText
            );
        }


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

        public ISymbolicNumber GetNumber(string numberText)
        {
            if (_numbersDictionary.TryGetValue(numberText, out var constantAtomic))
                return constantAtomic;
            
            throw new KeyNotFoundException(numberText);
        }

        public ISymbolicVariableParameter GetParameterVariable(string internalName)
        {
            if (_parametersVariablesDictionary.TryGetValue(internalName, out var variable))
                return variable;

            throw new KeyNotFoundException(internalName);
        }

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

        public ISymbolicNumber GetOrDefineRationalNumber(int numerator, int denominator)
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

        public ISymbolicNumber GetOrDefineNumber(ISymbolicHeadSpecsNumber headSpecs)
        {
            return headSpecs switch
            {
                SymbolicHeadSpecsNumberFloat64 numberHeadSpecsFloat64 => 
                    GetOrDefineLiteralNumber(numberHeadSpecsFloat64.NumberValue),

                SymbolicHeadSpecsNumberInt32 numberHeadSpecsInt32 => 
                    GetOrDefineLiteralNumber(numberHeadSpecsInt32.NumberValue),

                SymbolicHeadSpecsNumberRational numberHeadSpecsRational => 
                    GetOrDefineRationalNumber(numberHeadSpecsRational.Numerator, numberHeadSpecsRational.Denominator),

                SymbolicHeadSpecsNumberSymbolic numberHeadSpecsSymbolic => 
                    GetOrDefineSymbolicNumber(numberHeadSpecsSymbolic.NumberText, numberHeadSpecsSymbolic.NumberValue),

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


        public ISymbolicVariableComputed GetOrDefineComputedVariable(Func<ISymbolicExpression, ISymbolicExpression> computingFunc, ISymbolicExpressionAtomic dependsOnAtomic)
        {
            var rhsScalarValue = computingFunc(
                dependsOnAtomic.GetScalarValue(MergeExpressions)
            );

            return GetOrDefineComputedVariable(
                rhsScalarValue
            );
        }

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
                SymbolicExpressionProcessor.ToText(rhsExpression);

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

        public void RemoveNotUsedAtomics()
        {
            RemoveNotUsedNumbers();

            RemoveNotUsedParameterVariables();

            RemoveNotUsedComputedVariables();
        }


        public ISymbolicExpressionAtomic Add(ISymbolicExpressionAtomic scalar1, ISymbolicExpressionAtomic scalar2)
        {
            return GetOrDefineComputedVariable(
                SymbolicExpressionProcessor.Add,
                scalar1,
                scalar2
            );
        }

        public ISymbolicExpressionAtomic Add(params ISymbolicExpressionAtomic[] scalarsList)
        {
            return GetOrDefineComputedVariable(
                SymbolicExpressionProcessor.Add,
                scalarsList
            );
        }

        public ISymbolicExpressionAtomic Add(IEnumerable<ISymbolicExpressionAtomic> scalarsList)
        {
            return GetOrDefineComputedVariable(
                SymbolicExpressionProcessor.Add,
                scalarsList
            );
        }

        public ISymbolicExpressionAtomic Subtract(ISymbolicExpressionAtomic scalar1, ISymbolicExpressionAtomic scalar2)
        {
            return GetOrDefineComputedVariable(
                SymbolicExpressionProcessor.Subtract,
                scalar1,
                scalar2
            );
        }

        public ISymbolicExpressionAtomic Times(ISymbolicExpressionAtomic scalar1, ISymbolicExpressionAtomic scalar2)
        {
            return GetOrDefineComputedVariable(
                SymbolicExpressionProcessor.Times,
                scalar1,
                scalar2
            );
        }

        public ISymbolicExpressionAtomic Times(params ISymbolicExpressionAtomic[] scalarsList)
        {
            return GetOrDefineComputedVariable(
                SymbolicExpressionProcessor.Times,
                scalarsList
            );
        }

        public ISymbolicExpressionAtomic Times(IEnumerable<ISymbolicExpressionAtomic> scalarsList)
        {
            return GetOrDefineComputedVariable(
                SymbolicExpressionProcessor.Times,
                scalarsList
            );
        }

        public ISymbolicExpressionAtomic NegativeTimes(ISymbolicExpressionAtomic scalar1, ISymbolicExpressionAtomic scalar2)
        {
            return GetOrDefineComputedVariable(
                SymbolicExpressionProcessor.NegativeTimes,
                scalar1,
                scalar2
            );
        }

        public ISymbolicExpressionAtomic NegativeTimes(params ISymbolicExpressionAtomic[] scalarsList)
        {
            return GetOrDefineComputedVariable(
                SymbolicExpressionProcessor.NegativeTimes,
                scalarsList
            );
        }

        public ISymbolicExpressionAtomic NegativeTimes(IEnumerable<ISymbolicExpressionAtomic> scalarsList)
        {
            return GetOrDefineComputedVariable(
                SymbolicExpressionProcessor.NegativeTimes,
                scalarsList
            );
        }

        public ISymbolicExpressionAtomic Divide(ISymbolicExpressionAtomic scalar1, ISymbolicExpressionAtomic scalar2)
        {
            return GetOrDefineComputedVariable(
                SymbolicExpressionProcessor.Divide,
                scalar1,
                scalar2
            );
        }

        public ISymbolicExpressionAtomic NegativeDivide(ISymbolicExpressionAtomic scalar1, ISymbolicExpressionAtomic scalar2)
        {
            return GetOrDefineComputedVariable(
                SymbolicExpressionProcessor.NegativeDivide,
                scalar1,
                scalar2
            );
        }

        public ISymbolicExpressionAtomic Positive(ISymbolicExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                SymbolicExpressionProcessor.Positive,
                scalar
            );
        }

        public ISymbolicExpressionAtomic Negative(ISymbolicExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                SymbolicExpressionProcessor.Negative,
                scalar
            );

        }

        public ISymbolicExpressionAtomic Inverse(ISymbolicExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                SymbolicExpressionProcessor.Inverse,
                scalar
            );

        }

        public ISymbolicExpressionAtomic Abs(ISymbolicExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                SymbolicExpressionProcessor.Abs,
                scalar
            );

        }

        public ISymbolicExpressionAtomic Sqrt(ISymbolicExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                SymbolicExpressionProcessor.Sqrt,
                scalar
            );

        }

        public ISymbolicExpressionAtomic SqrtOfAbs(ISymbolicExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                SymbolicExpressionProcessor.SqrtOfAbs,
                scalar
            );

        }

        public ISymbolicExpressionAtomic Exp(ISymbolicExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                SymbolicExpressionProcessor.Exp,
                scalar
            );

        }

        public ISymbolicExpressionAtomic Log(ISymbolicExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                SymbolicExpressionProcessor.Log,
                scalar
            );

        }

        public ISymbolicExpressionAtomic Log2(ISymbolicExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                SymbolicExpressionProcessor.Log2,
                scalar
            );

        }

        public ISymbolicExpressionAtomic Log10(ISymbolicExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                SymbolicExpressionProcessor.Log10,
                scalar
            );

        }

        public ISymbolicExpressionAtomic Log(ISymbolicExpressionAtomic scalar, ISymbolicExpressionAtomic baseScalar)
        {
            return GetOrDefineComputedVariable(
                SymbolicExpressionProcessor.Log,
                scalar,
                baseScalar
            );
        }

        public ISymbolicExpressionAtomic Cos(ISymbolicExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                SymbolicExpressionProcessor.Cos,
                scalar
            );

        }

        public ISymbolicExpressionAtomic Sin(ISymbolicExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                SymbolicExpressionProcessor.Sin,
                scalar
            );

        }

        public ISymbolicExpressionAtomic Tan(ISymbolicExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                SymbolicExpressionProcessor.Tan,
                scalar
            );

        }

        public ISymbolicExpressionAtomic ArcCos(ISymbolicExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                SymbolicExpressionProcessor.ArcCos,
                scalar
            );

        }

        public ISymbolicExpressionAtomic ArcSin(ISymbolicExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                SymbolicExpressionProcessor.ArcSin,
                scalar
            );

        }

        public ISymbolicExpressionAtomic ArcTan(ISymbolicExpressionAtomic scalar)
        {
            return GetOrDefineComputedVariable(
                SymbolicExpressionProcessor.ArcTan,
                scalar
            );

        }

        public ISymbolicExpressionAtomic ArcTan2(ISymbolicExpressionAtomic scalarX, ISymbolicExpressionAtomic scalarY)
        {
            return GetOrDefineComputedVariable(
                SymbolicExpressionProcessor.ArcTan2,
                scalarX,
                scalarY
            );
        }

        public bool IsValid(ISymbolicExpressionAtomic scalar)
        {
            return SymbolicExpressionProcessor.IsValid(scalar);
        }

        public bool IsZero(ISymbolicExpressionAtomic scalar)
        {
            return SymbolicExpressionProcessor.IsZero(scalar);
        }

        public bool IsZero(ISymbolicExpressionAtomic scalar, bool nearZeroFlag)
        {
            return SymbolicExpressionProcessor.IsZero(scalar, nearZeroFlag);
        }

        public bool IsNearZero(ISymbolicExpressionAtomic scalar)
        {
            return SymbolicExpressionProcessor.IsNearZero(scalar);
        }

        public bool IsNearZero(double scalar)
        {
            return scalar > -ZeroEpsilon && scalar < ZeroEpsilon;
        }

        public ISymbolicExpressionAtomic TextToScalar(string text)
        {
            return (ISymbolicExpressionAtomic) SymbolicExpressionProcessor.TextToScalar(text);
        }

        public ISymbolicExpressionAtomic IntegerToScalar(int value)
        {
            return GetOrDefineLiteralNumber((double) value);
        }

        public ISymbolicExpressionAtomic Float64ToScalar(double value)
        {
            return GetOrDefineLiteralNumber((long) value);
        }

        public ISymbolicExpressionAtomic GetRandomScalar(System.Random randomGenerator, double minValue, double maxValue)
        {
            var value = 
                minValue + (maxValue - minValue) * randomGenerator.NextDouble();

            return GetOrDefineLiteralNumber((long) value);
        }

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

        public void ResetComputedVariables(IEnumerable<ISymbolicVariableComputed> computedVariablesList)
        {
            _computedVariablesDictionary.Clear();

            foreach (var computedVariable in computedVariablesList)
                _computedVariablesDictionary.Add(
                    computedVariable.InternalName, 
                    computedVariable
                );
        }

        public void SimplifyRhsExpressions()
        {
            if (ExpressionSimplifier is null) 
                return;

            foreach (var computedVariable in ComputedVariables)
                computedVariable.ResetRhsExpression(
                    ExpressionSimplifier.Simplify(computedVariable.RhsExpression)
                );
        }

        public void OptimizeContext()
        {
            SimplifyRhsExpressions();

            var inputsWithTestValues =
                ParameterVariables
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

            var inputVarsTotalCount = ParameterVariables.Count();
            var inputVarsUsedCount = ParameterVariables.Count(inputVar => inputVar.HasDependingVariables);
            var inputVarsUnUsedCount = inputVarsTotalCount - inputVarsUsedCount;

            stats.Add("Used Input Variables: ", inputVarsUsedCount.ToString());
            stats.Add("Unused Input Variables: ", inputVarsUnUsedCount.ToString());
            stats.Add("Total Input Variables: ", inputVarsTotalCount.ToString());

            var tempVarsCount = IntermediateVariables.Count();
            var tempVarsSubExprCount = IntermediateVariables.Count(item => item.IsFactoredSubExpression);
            var tempVarsNonSubExprCount = tempVarsCount - tempVarsSubExprCount;

            stats.Add("Common Subexpressions Temp Variables: ", tempVarsSubExprCount.ToString());
            stats.Add("Generated Temp Variables: ", tempVarsNonSubExprCount.ToString());
            stats.Add("Total Temp Variables: ", tempVarsCount.ToString());

            stats.Add("Total Output Variables: ", OutputVariables.Count().ToString());

            stats.Add("Total Computed Variables: ", ComputedVariables.Count().ToString());

            stats.Add("Target Temp Variables: ", TargetTempVarsCount.ToString());

            var computationsCountTotal =
                ComputedVariables
                    .Select(computedVar => computedVar.RhsExpression.ComputationsCount)
                    .Sum();

            var computationsCountAverage =
                computationsCountTotal / (double) ComputedVariables.Count();

            stats.Add("Avg. Computations Count: ", computationsCountAverage.ToString("0.000"));
            stats.Add("Total Computations Count: ", computationsCountTotal.ToString());

            var memReadsCountTotal =
                ComputedVariables
                    .Select(computedVar => computedVar.RhsVariables.Count())
                    .Sum();

            var memReadsCountAverage =
                memReadsCountTotal / (double) ComputedVariables.Count();

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

            var inputVarsTotalCount = ParameterVariables.Count();
            var inputVarsUsedCount = ParameterVariables.Count(inputVar => inputVar.HasDependingVariables);
            var inputVarsUnUsedCount = inputVarsTotalCount - inputVarsUsedCount;

            s.Append("Input Variables: ")
                .Append(inputVarsUsedCount)
                .Append(" used, ")
                .Append(inputVarsUnUsedCount)
                .Append(" not used, ")
                .Append(inputVarsTotalCount)
                .AppendLine(" total.")
                .AppendLine();

            var tempVarsCount = IntermediateVariables.Count();
            var tempVarsSubExprCount = IntermediateVariables.Count(item => item.IsFactoredSubExpression);
            var tempVarsNonSubExprCount = tempVarsCount - tempVarsSubExprCount;

            s.Append("Temp Variables: ")
                .Append(tempVarsSubExprCount)
                .Append(" sub-expressions, ")
                .Append(tempVarsNonSubExprCount)
                .Append(" generated temps, ")
                .Append(tempVarsCount)
                .AppendLine(" total.")
                .AppendLine();

            if (TargetTempVarsCount > 0)
                s.Append("Target Temp Variables: ")
                    .Append(TargetTempVarsCount)
                    .AppendLine(" total.")
                    .AppendLine();

            var outputVarsCount = OutputVariables.Count();

            s.Append("Output Variables: ")
                .Append(outputVarsCount)
                .AppendLine(" total.")
                .AppendLine();

            var computationsCountTotal =
                ComputedVariables
                    .Select(computedVar => computedVar.RhsExpression.ComputationsCount)
                    .Sum();
            
            var computationsCountAverage = 
                computationsCountTotal / (double) ComputedVariables.Count();

            s.Append("Computations: ")
                .Append(computationsCountAverage)
                .Append(" average, ")
                .Append(computationsCountTotal)
                .AppendLine(" total.")
                .AppendLine();

            var memReadsCountTotal =
                ComputedVariables
                    .Select(computedVar => computedVar.RhsVariables.Count())
                    .Sum();

            var memReadsCountAverage =
                memReadsCountTotal / (double) ComputedVariables.Count();

            s.Append("Memory Reads: ")
                .Append(memReadsCountAverage)
                .Append(" average, ")
                .Append(memReadsCountTotal)
                .AppendLine(" total.")
                .AppendLine();

            s.Append("Memory Writes: ")
                .Append(ComputedVariables.Count())
                .AppendLine(" total.")
                .AppendLine();

            return s.ToString();
        }

        public override string ToString()
        {
            var composer = new StringBuilder();

            composer.AppendLine(
                Atomics
                    .Select(scalar => scalar.GetTextDescription())
                    .Concatenate(Environment.NewLine)
            );

            return composer.ToString();
        }
    }
}
