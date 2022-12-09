using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.DenseKVectorsLib.Outermorphism
{
    internal class MapMethodFileComposer : 
        GaFuLLibraryMetaContextFileComposerBase
    {
        private readonly uint _inputGrade;
        private IMetaExpressionAtomic[,] _linearMapArray;
        private GaKVector<IMetaExpressionAtomic> _inputKVector;
        private GaKVector<IMetaExpressionAtomic> _outputKVector;


        internal MapMethodFileComposer(GaFuLLibraryComposer libGen, uint inGrade)
            : base(libGen)
        {
            _inputGrade = inGrade;
        }

        
        protected override void DefineContextParameters(MetaContext context)
        {
            _linearMapArray = context.ParameterVariablesFactory.CreateDenseArray(
                (int) VSpaceDimension,
                (int) VSpaceDimension,
                (row, col) => $"omScalarR{row}C{col}"
            );

            _inputKVector = context.ParameterVariablesFactory.CreateDenseKVector(
                VSpaceDimension,
                _inputGrade,
                index => $"kVectorScalar{index}"
            );
        }

        protected override void DefineContextComputations(MetaContext context)
        {
            var outermorphism =
                GeometricProcessor.CreateLinearMapOutermorphism(_linearMapArray);

            _outputKVector = outermorphism.OmMap(_inputKVector);

            _outputKVector.SetIsOutput(true);
        }

        protected override void DefineContextExternalNames(MetaContext context)
        {
            _inputKVector.SetExternalNamesByTermIndex(
                index => $"kVectorScalars[{index}]"
            );

            _outputKVector.SetExternalNamesByTermIndex(
                index => $"mappedKVectorScalars[{index}]"
            );

            context.SetIntermediateExternalNamesByNameIndex(
                DenseKVectorsLibraryComposer.MaxTargetLocalVars,
                index => $"tempVar{index:X4}",
                index => $"tempArray[{index}]"
            );
        }

        public override void Generate()
        {
            GenerateOutermorphismFileStartCode();

            var computationsText = GenerateCode();

            TextComposer.Append(
                Templates["om_apply"],
                "double", GeoLanguage.ScalarTypeName,
                "grade", _inputGrade,
                "num", this.KVectorSpaceDimension(_inputGrade),
                "computations", computationsText
            );

            GenerateOutermorphismFileFinishCode();

            FileComposer.FinalizeText();
        }
    }
}
