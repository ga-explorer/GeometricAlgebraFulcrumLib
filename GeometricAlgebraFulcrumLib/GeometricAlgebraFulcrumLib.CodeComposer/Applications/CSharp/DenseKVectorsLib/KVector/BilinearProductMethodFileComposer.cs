using System;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra;
using GeometricAlgebraFulcrumLib.CodeComposer.Languages;
using GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Context;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib.KVector
{
    internal sealed class BilinearProductMethodFileComposer : 
        GaFuLLibrarySymbolicContextFileComposerBase
    {
        private KVectorStorage<ISymbolicExpressionAtomic> _inputKVector1;
        private KVectorStorage<ISymbolicExpressionAtomic> _inputKVector2;
        private KVectorStorage<ISymbolicExpressionAtomic> _outputKVector;
        private readonly GaFuLLanguageOperationSpecs _operationSpecs;
        private readonly uint _inputGrade1;
        private readonly uint _inputGrade2;
        private readonly uint _outputGrade;


        internal BilinearProductMethodFileComposer(GaFuLLibraryComposer libGen, GaFuLLanguageOperationSpecs opSpecs, uint inGrade1, uint inGrade2)
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
        
        internal BilinearProductMethodFileComposer(GaFuLLibraryComposer libGen, GaFuLLanguageOperationSpecs opSpecs, uint inGrade1, uint inGrade2, uint outGrade)
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
            var mv = _operationSpecs.OperationKind switch
            {
                GaFuLLanguageOperationKind.BinaryOuterProduct =>
                    GeometricProcessor.Op(_inputKVector1, _inputKVector2),

                GaFuLLanguageOperationKind.BinaryGeometricProduct =>
                    _operationSpecs.IsEuclidean 
                        ? GeometricProcessor.EGp(_inputKVector1, _inputKVector2)
                        : GeometricProcessor.Gp(_inputKVector1, _inputKVector2),

                GaFuLLanguageOperationKind.BinaryGeometricProductDual =>
                    _operationSpecs.IsEuclidean 
                        ? GeometricProcessor.EDual(GeometricProcessor.EGp(_inputKVector1, _inputKVector2), VSpaceDimension)
                        : GeometricProcessor.Gp(_inputKVector1, _inputKVector2).Dual(GeometricProcessor),

                GaFuLLanguageOperationKind.BinaryLeftContractionProduct =>
                    _operationSpecs.IsEuclidean 
                        ? GeometricProcessor.ELcp(_inputKVector1, _inputKVector2)
                        : GeometricProcessor.Lcp(_inputKVector1, _inputKVector2),

                GaFuLLanguageOperationKind.BinaryRightContractionProduct =>
                    _operationSpecs.IsEuclidean 
                        ? GeometricProcessor.ERcp(_inputKVector1, _inputKVector2)
                        : GeometricProcessor.Rcp(_inputKVector1, _inputKVector2),

                GaFuLLanguageOperationKind.BinaryFatDotProduct =>
                    _operationSpecs.IsEuclidean 
                        ? GeometricProcessor.EFdp(_inputKVector1, _inputKVector2)
                        : GeometricProcessor.Fdp(_inputKVector1, _inputKVector2),

                GaFuLLanguageOperationKind.BinaryHestenesInnerProduct =>
                    _operationSpecs.IsEuclidean 
                        ? GeometricProcessor.EHip(_inputKVector1, _inputKVector2)
                        : GeometricProcessor.Hip(_inputKVector1, _inputKVector2),

                _ => throw new InvalidOperationException()
            };

            _outputKVector = mv.GetKVectorPart(_outputGrade);

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
                this.KVectorSpaceDimension(_outputGrade);

            var methodName = _operationSpecs.GetName(
                _inputGrade1, _inputGrade2, _outputGrade
            );

            TextComposer.AppendAtNewLine(
                Templates["bilinearproduct"],
                "name", methodName,
                "num", kvSpaceDimension,
                "double", GeoLanguage.ScalarTypeName,
                "computations", computationsText
            );

            GenerateBladeFileFinishCode();

            FileComposer.FinalizeText();
        }
    }
}
