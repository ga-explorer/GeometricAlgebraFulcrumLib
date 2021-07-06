using GeometricAlgebraLib.Processing.Scalars;

namespace GeometricAlgebraLib.Processing.Implementations.NamedScalars
{
    public sealed class GaNamedScalarParameter<TScalar> :
        IGaInputNamedScalar<TScalar>
    {
        public GaNamedScalarsCollection<TScalar> NamedScalarsCollection { get; }

        public IGaScalarProcessorNamedScalar<TScalar> NamedScalarProcessor
            => NamedScalarsCollection.NamedScalarProcessor;

        public IGaSymbolicScalarProcessor<TScalar> SymbolicScalarProcessor 
            => NamedScalarsCollection.SymbolicScalarProcessor;

        public int ScalarId { get; }

        public string ScalarName { get; }

        private string _finalScalarName = string.Empty;
        public string ExternalName
        {
            get => string.IsNullOrEmpty(_finalScalarName) ? ScalarName : _finalScalarName;
            set => _finalScalarName = value ?? string.Empty;
        }

        public TScalar LhsScalarValue { get; }

        public TScalar RhsScalarValue 
            => LhsScalarValue;

        public string RhsScalarValueText 
            => ScalarName;

        public TScalar FinalRhsScalarValue { get; set; }

        public string FinalRhsScalarValueText 
            => SymbolicScalarProcessor.ToText(FinalRhsScalarValue);

        public bool IsConstant 
            => false;

        public bool IsParameter 
            => true;

        public bool IsIndependent 
            => true;

        public bool IsIntermediate 
            => false;

        public bool IsOutput 
            => false;

        public bool IsDependent 
            => false;

        public bool IsUsedForOutputVariables { get; set; }


        internal GaNamedScalarParameter(GaNamedScalarsCollection<TScalar> baseCollection, string scalarName)
        {
            NamedScalarsCollection = baseCollection;
            ScalarId = baseCollection.GetNextNamedScalarId();
            ScalarName = scalarName;
            LhsScalarValue = SymbolicScalarProcessor.TextToScalar(scalarName);
            FinalRhsScalarValue = SymbolicScalarProcessor.ZeroScalar;
        }
        

        public TScalar GetScalarValue(bool useRhsScalarValue)
        {
            return LhsScalarValue;
        }

        public override string ToString()
        {
            var isUsedText = IsUsedForOutputVariables 
                ? "    Used" 
                : "Not Used";

            return $"{isUsedText} Parameter    \"{ExternalName}\": {ScalarName}";
        }
    }
}