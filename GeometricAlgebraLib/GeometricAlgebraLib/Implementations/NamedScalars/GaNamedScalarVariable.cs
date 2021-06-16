using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using GeometricAlgebraLib.Processors.Scalars;
using TextComposerLib.Text;

namespace GeometricAlgebraLib.Implementations.NamedScalars
{
    public sealed class GaNamedScalarVariable<TScalar> :
        IGaNamedScalar<TScalar>
    {
        private readonly IReadOnlyList<IGaNamedScalar<TScalar>> _dependsOnScalars;


        public GaNamedScalarsCollection<TScalar> NamedScalarsCollection { get; }

        public IGaScalarProcessorNamedScalar<TScalar> NamedScalarProcessor
            => NamedScalarsCollection.NamedScalarProcessor;

        public IGaSymbolicScalarProcessor<TScalar> SymbolicScalarProcessor 
            => NamedScalarsCollection.SymbolicScalarProcessor;

        public int ScalarId { get; }

        public string ScalarName { get; }
        
        private string _finalScalarName = string.Empty;
        public string FinalScalarName
        {
            get => string.IsNullOrEmpty(_finalScalarName) ? ScalarName : _finalScalarName;
            set => _finalScalarName = value ?? string.Empty;
        }

        public TScalar LhsScalarValue { get; }

        public TScalar RhsScalarValue { get; }

        public string RhsScalarValueText { get; }

        public bool IsConstant 
            => false;

        public bool IsParameter 
            => false;

        public bool IsInput 
            => false;

        public bool IsIntermediate
        {
            get => !IsOutput;
            set => IsOutput = !value;
        }

        public bool IsOutput { get; set; }

        public bool IsVariable 
            => true;

        public bool IsUsedForOutputVariables { get; set; }

        public IEnumerable<IGaNamedScalar<TScalar>> DependsOnScalars 
            => _dependsOnScalars;


        internal GaNamedScalarVariable([NotNull] GaNamedScalarsCollection<TScalar> baseCollection, int scalarId, [NotNull] string scalarName, [NotNull] TScalar scalar, [NotNull] IEnumerable<IGaNamedScalar<TScalar>> dependsOnScalars)
        {
            NamedScalarsCollection = baseCollection;
            RhsScalarValue = scalar;
            RhsScalarValueText = SymbolicScalarProcessor.ToText(scalar);
            LhsScalarValue = SymbolicScalarProcessor.GetSymbol(scalarName);
            ScalarId = scalarId;
            ScalarName = scalarName;
            _dependsOnScalars = dependsOnScalars.Distinct().ToArray();
        }
        

        public override string ToString()
        {
            var isUsedText = IsUsedForOutputVariables 
                ? "    Used" 
                : "Not Used";

            var dependsOnScalarsText =
                _dependsOnScalars
                    .Select(scalar => scalar.ScalarName)
                    .Concatenate(", ");

            return new StringBuilder()
                .Append(isUsedText)
                .Append(IsOutput ? " Output       " : " Intermediate ")
                .Append(ScalarName)
                .Append($"<{FinalScalarName}>(")
                .Append(dependsOnScalarsText)
                .Append(") := ")
                .Append(SymbolicScalarProcessor.ToText(RhsScalarValue))
                .ToString();
        }
    }
}