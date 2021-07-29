using System;
using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.CodeComposer.Languages;
using GeometricAlgebraFulcrumLib.Processing.Products;
using GeometricAlgebraFulcrumLib.Processing.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;
using GeometricAlgebraFulcrumLib.Storage;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib.KVector
{
    internal sealed class BilinearProductMethodFileComposer : 
        GaLibrarySymbolicContextFileComposerBase
    {
        private IGasKVector<ISymbolicExpressionAtomic> _inputKVector1;
        private IGasKVector<ISymbolicExpressionAtomic> _inputKVector2;
        private IGasKVector<ISymbolicExpressionAtomic> _outputKVector;
        private readonly GaLanguageOperationSpecs _operationSpecs;
        private readonly uint _inputGrade1;
        private readonly uint _inputGrade2;
        private readonly uint _outputGrade;


        internal BilinearProductMethodFileComposer(GaLibraryComposer libGen, GaLanguageOperationSpecs opSpecs, uint inGrade1, uint inGrade2)
            : base(libGen)
        {
            _operationSpecs = opSpecs;
            
            _inputGrade1 = inGrade1;
            _inputGrade2 = inGrade2;

            var (isValid, outputGrade) = opSpecs.GetKVectorsBilinearProductGrade(
                VSpaceDimension,
                inGrade1, 
                inGrade2
            );

            if (!isValid)
                throw new InvalidOperationException();

            _outputGrade = outputGrade;
        }
        
        internal BilinearProductMethodFileComposer(GaLibraryComposer libGen, GaLanguageOperationSpecs opSpecs, uint inGrade1, uint inGrade2, uint outGrade)
            : base(libGen)
        {
            _operationSpecs = opSpecs;
            
            _inputGrade1 = inGrade1;
            _inputGrade2 = inGrade2;

            _outputGrade = outGrade;
        }


        protected override void DefineContextParameters(SymbolicContext context)
        {
            _inputKVector1 = context.ParameterVariablesFactory.CreateDenseKVector(
                VSpaceDimension,
                _inputGrade1,
                index => $"mv1Scalar{index}"
            );

            _inputKVector2 = context.ParameterVariablesFactory.CreateDenseKVector(
                VSpaceDimension,
                _inputGrade2,
                index => $"mv2Scalar{index}"
            );
        }

        protected override void DefineContextComputations(SymbolicContext context)
        {
            _outputKVector = _operationSpecs.OperationKind switch
            {
                GaLanguageOperationKind.BinaryOuterProduct =>
                    _inputKVector1.Op(_inputKVector2).GetKVectorPart(_outputGrade),

                GaLanguageOperationKind.BinaryGeometricProduct =>
                    _operationSpecs.IsEuclidean 
                        ? _inputKVector1.EGp(_inputKVector2).GetKVectorPart(_outputGrade)
                        : _inputKVector1.Gp(_inputKVector2, Processor).GetKVectorPart(_outputGrade),

                GaLanguageOperationKind.BinaryGeometricProductDual =>
                    _operationSpecs.IsEuclidean 
                        ? _inputKVector1.EGp(_inputKVector2).EDual(VSpaceDimension).GetKVectorPart(_outputGrade)
                        : _inputKVector1.Gp(_inputKVector2, Processor).Dual(Processor).GetKVectorPart(_outputGrade),

                GaLanguageOperationKind.BinaryLeftContractionProduct =>
                    _operationSpecs.IsEuclidean 
                        ? _inputKVector1.ELcp(_inputKVector2).GetKVectorPart(_outputGrade)
                        : Processor.Lcp(_inputKVector1, _inputKVector2).GetKVectorPart(_outputGrade),

                GaLanguageOperationKind.BinaryRightContractionProduct =>
                    _operationSpecs.IsEuclidean 
                        ? _inputKVector1.ERcp(_inputKVector2).GetKVectorPart(_outputGrade)
                        : Processor.Rcp(_inputKVector1, _inputKVector2).GetKVectorPart(_outputGrade),

                GaLanguageOperationKind.BinaryFatDotProduct =>
                    _operationSpecs.IsEuclidean 
                        ? _inputKVector1.EFdp(_inputKVector2).GetKVectorPart(_outputGrade)
                        : Processor.Fdp(_inputKVector1, _inputKVector2).GetKVectorPart(_outputGrade),

                GaLanguageOperationKind.BinaryHestenesInnerProduct =>
                    _operationSpecs.IsEuclidean 
                        ? _inputKVector1.EHip(_inputKVector2).GetKVectorPart(_outputGrade)
                        : Processor.Hip(_inputKVector1, _inputKVector2).GetKVectorPart(_outputGrade),

                _ => throw new InvalidOperationException()
            };
            

            _outputKVector.SetIsOutput(true);
        }

        protected override void DefineContextExternalNames(SymbolicContext context)
        {
            context.SetExternalNamesByTermIndex(
                _inputKVector1,
                index => $"mv1[{index}]"
            );

            context.SetExternalNamesByTermIndex(
                _inputKVector2,
                index => $"mv2[{index}]"
            );

            context.SetExternalNamesByTermIndex(
                _outputKVector,
                index => $"c[{index}]"
            );

            context.SetIntermediateExternalNamesByNameIndex(
                DenseKVectorsLibraryComposer.MaxTargetLocalVars,
                index => $"tempVar{index:X4}",
                index => $"tempArray[{index}]"
            );
        }
        
        public override void Generate()
        {
            GenerateBladeFileStartCode();

            var computationsText = 
                GenerateCode();

            var kvSpaceDimension = 
                this.KvSpaceDimension(_outputGrade);

            var methodName = _operationSpecs.GetName(
                _inputGrade1, _inputGrade2, _outputGrade
            );

            TextComposer.AppendAtNewLine(
                Templates["bilinearproduct"],
                "name", methodName,
                "num", kvSpaceDimension,
                "double", GaLanguage.ScalarTypeName,
                "computations", computationsText
            );

            GenerateBladeFileFinishCode();

            FileComposer.FinalizeText();
        }
    }
}
