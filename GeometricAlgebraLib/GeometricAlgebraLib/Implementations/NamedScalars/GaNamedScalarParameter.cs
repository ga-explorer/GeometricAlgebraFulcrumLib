using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraLib.Processors.Scalars;

namespace GeometricAlgebraLib.Implementations.NamedScalars
{
    public sealed class GaNamedScalarParameter<TScalar> :
        IGaNamedScalar<TScalar>
    {
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

        public TScalar RhsScalarValue 
            => LhsScalarValue;

        public string RhsScalarValueText 
            => ScalarName;

        public bool IsConstant 
            => false;

        public bool IsParameter 
            => true;

        public bool IsInput 
            => true;

        public bool IsIntermediate 
            => false;

        public bool IsOutput 
            => false;

        public bool IsVariable 
            => false;

        public bool IsUsedForOutputVariables { get; set; }

        public IEnumerable<IGaNamedScalar<TScalar>> DependsOnScalars 
            => Enumerable.Empty<IGaNamedScalar<TScalar>>();


        internal GaNamedScalarParameter(GaNamedScalarsCollection<TScalar> baseCollection, int scalarId, string scalarName)
        {
            NamedScalarsCollection = baseCollection;
            ScalarId = scalarId;
            ScalarName = scalarName;
            LhsScalarValue = SymbolicScalarProcessor.TextToScalar(scalarName);
        }
        

        public override string ToString()
        {
            var isUsedText = IsUsedForOutputVariables 
                ? "    Used" 
                : "Not Used";

            return $"{isUsedText} Parameter    {ScalarName}<{FinalScalarName}>";
        }
    }
}