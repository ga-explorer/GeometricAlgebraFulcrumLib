using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Processing.Scalars;

namespace GeometricAlgebraFulcrumLib.Processing.Implementations.NamedScalars
{
    public sealed class GaNamedScalarConstant<TScalar> :
        IGaInputNamedScalar<TScalar>
    {
        public GaNamedScalarsCollection<TScalar> NamedScalarsCollection { get; }

        public IGaScalarProcessorNamedScalar<TScalar> NamedScalarProcessor
            => NamedScalarsCollection.NamedScalarProcessor;
        
        public IGaSymbolicScalarProcessor<TScalar> SymbolicScalarProcessor 
            => NamedScalarsCollection.SymbolicScalarProcessor;

        public int ScalarId { get; }

        public string ScalarName { get; }

        public string ExternalName
        {
            get => ScalarName; 
            set {}
        }

        public TScalar LhsScalarValue 
            => RhsScalarValue;

        public TScalar RhsScalarValue { get; }

        public string RhsScalarValueText 
            => ScalarName;

        public TScalar FinalRhsScalarValue 
            => RhsScalarValue;

        public string FinalRhsScalarValueText 
            => ScalarName;

        public bool IsConstant 
            => true;

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


        internal GaNamedScalarConstant([NotNull] GaNamedScalarsCollection<TScalar> baseCollection, [NotNull] TScalar rhsScalarValue) 
        {
            NamedScalarsCollection = baseCollection;
            ScalarId = baseCollection.GetNextNamedScalarId();
            RhsScalarValue = rhsScalarValue;
            ScalarName = SymbolicScalarProcessor.ToText(RhsScalarValue);
        }


        public TScalar GetScalarValue(bool useRhsScalarValue)
        {
            return RhsScalarValue;
        }

        public override string ToString()
        {
            var isUsedText = IsUsedForOutputVariables 
                ? "    Used" 
                : "Not Used";

            return $"{isUsedText} Constant     : {ScalarName}";
        }
    }
}