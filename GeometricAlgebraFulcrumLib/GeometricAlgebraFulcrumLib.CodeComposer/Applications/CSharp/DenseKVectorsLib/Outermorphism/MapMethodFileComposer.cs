using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms.Computed;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;
using GeometricAlgebraFulcrumLib.Storage;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib.Outermorphism
{
    internal class MapMethodFileComposer : 
        GaLibrarySymbolicContextFileComposerBase
    {
        private readonly int _inputGrade;
        private ISymbolicExpressionAtomic[,] _linearMapArray;
        private IGaKVectorStorage<ISymbolicExpressionAtomic> _inputKVector;
        private IGaKVectorStorage<ISymbolicExpressionAtomic> _outputKVector;


        internal MapMethodFileComposer(GaLibraryComposer libGen, int inGrade)
            : base(libGen)
        {
            _inputGrade = inGrade;
        }

        
        protected override void DefineContextParameters(SymbolicContext context)
        {
            _linearMapArray = context.ParameterVariablesFactory.CreateDenseArray(
                VSpaceDimension,
                VSpaceDimension,
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
                GaOmComputed<ISymbolicExpressionAtomic>.Create(
                    MultivectorProcessor, 
                    _linearMapArray
                );

            _outputKVector = outermorphism.MapKVector(_inputKVector);

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
                "double", GaClcLanguage.ScalarTypeName,
                "grade", _inputGrade,
                "num", MultivectorProcessor.BasisSet.KvSpaceDimension(_inputGrade),
                "computations", computationsText
            );

            GenerateOutermorphismFileFinishCode();

            FileComposer.FinalizeText();
        }
    }
}
