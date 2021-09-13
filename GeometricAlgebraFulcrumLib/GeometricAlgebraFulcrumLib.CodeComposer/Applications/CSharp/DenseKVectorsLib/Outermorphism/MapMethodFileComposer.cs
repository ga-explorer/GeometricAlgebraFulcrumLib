using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra;
using GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Context;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib.Outermorphism
{
    internal class MapMethodFileComposer : 
        GaFuLLibrarySymbolicContextFileComposerBase
    {
        private readonly uint _inputGrade;
        private ISymbolicExpressionAtomic[,] _linearMapArray;
        private KVectorStorage<ISymbolicExpressionAtomic> _inputKVector;
        private KVectorStorage<ISymbolicExpressionAtomic> _outputKVector;


        internal MapMethodFileComposer(GaFuLLibraryComposer libGen, uint inGrade)
            : base(libGen)
        {
            _inputGrade = inGrade;
        }

        
        protected override void DefineContextParameters(SymbolicContext context)
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

        protected override void DefineContextComputations(SymbolicContext context)
        {
            var outermorphism =
                GeometricProcessor.CreateLinearMapOutermorphism(_linearMapArray);

            _outputKVector = outermorphism.OmMapKVector(_inputKVector);

            _outputKVector.SetIsOutput(true);
        }

        protected override void DefineContextExternalNames(SymbolicContext context)
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
