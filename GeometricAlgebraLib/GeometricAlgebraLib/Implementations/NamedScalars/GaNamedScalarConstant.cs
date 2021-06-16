using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GeometricAlgebraLib.Processors.Scalars;

namespace GeometricAlgebraLib.Implementations.NamedScalars
{
    public sealed class GaNamedScalarConstant<TScalar> :
        IGaNamedScalar<TScalar>
    {
        public GaNamedScalarsCollection<TScalar> NamedScalarsCollection { get; }

        public IGaScalarProcessorNamedScalar<TScalar> NamedScalarProcessor
            => NamedScalarsCollection.NamedScalarProcessor;
        
        public IGaSymbolicScalarProcessor<TScalar> SymbolicScalarProcessor 
            => NamedScalarsCollection.SymbolicScalarProcessor;

        public int ScalarId { get; }

        public string ScalarName { get; }

        public string FinalScalarName
        {
            get => ScalarName; 
            set {}
        }

        public TScalar LhsScalarValue 
            => RhsScalarValue;

        public TScalar RhsScalarValue { get; }

        public string RhsScalarValueText 
            => ScalarName;

        public bool IsConstant 
            => true;

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


        internal GaNamedScalarConstant([NotNull] GaNamedScalarsCollection<TScalar> baseCollection, int scalarId, [NotNull] TScalar expression) 
        {
            NamedScalarsCollection = baseCollection;
            ScalarId = scalarId;
            RhsScalarValue = expression;
            ScalarName = SymbolicScalarProcessor.ToText(RhsScalarValue);
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