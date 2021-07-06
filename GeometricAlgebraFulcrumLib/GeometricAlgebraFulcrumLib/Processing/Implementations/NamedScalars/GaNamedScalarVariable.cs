using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using TextComposerLib.Text;

namespace GeometricAlgebraFulcrumLib.Processing.Implementations.NamedScalars
{
    public sealed class GaNamedScalarVariable<TScalar> :
        IGaNamedScalar<TScalar>
    {
        private readonly List<IGaNamedScalar<TScalar>> _rhsNamedScalars;


        public GaNamedScalarsCollection<TScalar> NamedScalarsCollection { get; }

        public IGaScalarProcessorNamedScalar<TScalar> NamedScalarProcessor
            => NamedScalarsCollection.NamedScalarProcessor;

        public IGaSymbolicScalarProcessor<TScalar> SymbolicScalarProcessor 
            => NamedScalarsCollection.SymbolicScalarProcessor;

        public int ScalarId { get; }

        public string ScalarName { get; }
        
        private string _externalName = string.Empty;
        public string ExternalName
        {
            get => string.IsNullOrEmpty(_externalName) ? ScalarName : _externalName;
            set => _externalName = value ?? string.Empty;
        }

        public TScalar LhsScalarValue { get; }

        public TScalar RhsScalarValue { get; }

        public string RhsScalarValueText { get; }

        public TScalar FinalRhsScalarValue { get; set; }

        public string FinalRhsScalarValueText 
            => SymbolicScalarProcessor.ToText(FinalRhsScalarValue);

        public int ComputationOrder { get; set; }

        public bool IsConstant 
            => false;

        public bool IsParameter 
            => false;

        public bool IsIndependent 
            => false;

        public bool IsIntermediate
        {
            get => !IsOutput;
            set => IsOutput = !value;
        }

        public bool IsOutput { get; set; }

        public bool IsDependent 
            => true;

        public bool IsUsedForOutputVariables { get; set; }

        public IEnumerable<IGaNamedScalar<TScalar>> RhsNamedScalars 
            => _rhsNamedScalars;

        public IEnumerable<IGaNamedScalar<TScalar>> RhsInputs 
            => _rhsNamedScalars.Where(scalar => scalar.IsIndependent);

        public IEnumerable<GaNamedScalarConstant<TScalar>> RhsConstants 
            => _rhsNamedScalars
                .Select(scalar => scalar as GaNamedScalarConstant<TScalar>)
                .Where(scalar => scalar is not null);

        public IEnumerable<GaNamedScalarParameter<TScalar>> RhsParameters 
            => _rhsNamedScalars
                .Select(scalar => scalar as GaNamedScalarParameter<TScalar>)
                .Where(scalar => scalar is not null);

        public IEnumerable<GaNamedScalarVariable<TScalar>> RhsVariables 
            => _rhsNamedScalars
                .Select(scalar => scalar as GaNamedScalarVariable<TScalar>)
                .Where(scalar => scalar is not null);

        public IEnumerable<GaNamedScalarVariable<TScalar>> RhsIntermediateVariables 
            => _rhsNamedScalars
                .Select(scalar => scalar as GaNamedScalarVariable<TScalar>)
                .Where(scalar => scalar is not null && scalar.IsIntermediate);

        public IEnumerable<GaNamedScalarVariable<TScalar>> RhsOutputVariables 
            => _rhsNamedScalars
                .Select(scalar => scalar as GaNamedScalarVariable<TScalar>)
                .Where(scalar => scalar is not null && scalar.IsOutput);


        internal GaNamedScalarVariable([NotNull] GaNamedScalarsCollection<TScalar> baseCollection, [NotNull] string scalarName, [NotNull] TScalar rhsScalarValue, [NotNull] IEnumerable<IGaNamedScalar<TScalar>> rhsNamedScalars)
        {
            NamedScalarsCollection = baseCollection;
            RhsScalarValue = rhsScalarValue;
            RhsScalarValueText = SymbolicScalarProcessor.ToText(rhsScalarValue);
            FinalRhsScalarValue = SymbolicScalarProcessor.ZeroScalar;
            LhsScalarValue = SymbolicScalarProcessor.GetSymbol(scalarName);
            ScalarId = baseCollection.GetNextNamedScalarId();
            ScalarName = scalarName;
            ComputationOrder = baseCollection.GetNextComputationOrder();
            _rhsNamedScalars = rhsNamedScalars.Distinct().ToList();
        }
        

        public TScalar GetScalarValue(bool useRhsScalarValue)
        {
            return useRhsScalarValue ? RhsScalarValue : LhsScalarValue;
        }

        public override string ToString()
        {
            var isUsedText = IsUsedForOutputVariables 
                ? "    Used" 
                : "Not Used";

            var dependsOnScalarsText =
                _rhsNamedScalars
                    .Select(scalar => scalar.ScalarName)
                    .Concatenate(", ");

            return new StringBuilder()
                .Append(isUsedText)
                .Append(IsOutput ? " Output       " : " Intermediate ")
                .Append($"\"{ExternalName}\": ")
                .Append(ScalarName)
                .Append('(')
                .Append(dependsOnScalarsText)
                .Append(") = ")
                .Append(SymbolicScalarProcessor.ToText(RhsScalarValue))
                .ToString();
        }
    }
}