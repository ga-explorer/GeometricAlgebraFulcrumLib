using System;
using GeometricAlgebraFulcrumLib.CodeComposer.Languages;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib.KVector
{
    internal sealed class BilinearProductMethodFileComposer : 
        GaFuLLibrarySymbolicContextFileComposerBase
    {
        private IGaKVectorStorage<ISymbolicExpressionAtomic> _inputKVector1;
        private IGaKVectorStorage<ISymbolicExpressionAtomic> _inputKVector2;
        private IGaKVectorStorage<ISymbolicExpressionAtomic> _outputKVector;
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
                    Processor.Op(_inputKVector1, _inputKVector2),

                GaFuLLanguageOperationKind.BinaryGeometricProduct =>
                    _operationSpecs.IsEuclidean 
                        ? Processor.EGp(_inputKVector1, _inputKVector2)
                        : Processor.Gp(_inputKVector1, _inputKVector2),

                GaFuLLanguageOperationKind.BinaryGeometricProductDual =>
                    _operationSpecs.IsEuclidean 
                        ? Processor.EDual(Processor.EGp(_inputKVector1, _inputKVector2), VSpaceDimension)
                        : Processor.Gp(_inputKVector1, _inputKVector2).Dual(Processor),

                GaFuLLanguageOperationKind.BinaryLeftContractionProduct =>
                    _operationSpecs.IsEuclidean 
                        ? Processor.ELcp(_inputKVector1, _inputKVector2)
                        : Processor.Lcp(_inputKVector1, _inputKVector2),

                GaFuLLanguageOperationKind.BinaryRightContractionProduct =>
                    _operationSpecs.IsEuclidean 
                        ? Processor.ERcp(_inputKVector1, _inputKVector2)
                        : Processor.Rcp(_inputKVector1, _inputKVector2),

                GaFuLLanguageOperationKind.BinaryFatDotProduct =>
                    _operationSpecs.IsEuclidean 
                        ? Processor.EFdp(_inputKVector1, _inputKVector2)
                        : Processor.Fdp(_inputKVector1, _inputKVector2),

                GaFuLLanguageOperationKind.BinaryHestenesInnerProduct =>
                    _operationSpecs.IsEuclidean 
                        ? Processor.EHip(_inputKVector1, _inputKVector2)
                        : Processor.Hip(_inputKVector1, _inputKVector2),

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
