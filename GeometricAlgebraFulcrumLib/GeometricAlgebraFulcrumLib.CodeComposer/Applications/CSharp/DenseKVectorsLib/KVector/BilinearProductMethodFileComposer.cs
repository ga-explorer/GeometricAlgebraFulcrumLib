using System;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra;
using GeometricAlgebraFulcrumLib.CodeComposer.Languages;
using GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Context;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib.KVector
{
    internal sealed class BilinearProductMethodFileComposer : 
        GaFuLLibrarySymbolicContextFileComposerBase
    {
        private KVector<ISymbolicExpressionAtomic> _inputKVector1;
        private KVector<ISymbolicExpressionAtomic> _inputKVector2;
        private KVector<ISymbolicExpressionAtomic> _outputKVector;
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
                    _inputKVector1.Op(_inputKVector2).AsMultivector(),

                GaFuLLanguageOperationKind.BinaryGeometricProduct =>
                    _operationSpecs.IsEuclidean 
                        ? _inputKVector1.EGp(_inputKVector2)
                        : _inputKVector1.Gp(_inputKVector2),

                GaFuLLanguageOperationKind.BinaryGeometricProductDual =>
                    _operationSpecs.IsEuclidean 
                        ? _inputKVector1.EGp(_inputKVector2).EDual()
                        : _inputKVector1.Gp(_inputKVector2).Dual(),

                GaFuLLanguageOperationKind.BinaryLeftContractionProduct =>
                    _operationSpecs.IsEuclidean 
                        ? _inputKVector1.ELcp(_inputKVector2).AsMultivector()
                        : _inputKVector1.Lcp(_inputKVector2).AsMultivector(),

                GaFuLLanguageOperationKind.BinaryRightContractionProduct =>
                    _operationSpecs.IsEuclidean 
                        ? _inputKVector1.ERcp(_inputKVector2).AsMultivector()
                        : _inputKVector1.Rcp(_inputKVector2).AsMultivector(),

                GaFuLLanguageOperationKind.BinaryFatDotProduct =>
                    _operationSpecs.IsEuclidean 
                        ? _inputKVector1.EFdp(_inputKVector2).AsMultivector()
                        : _inputKVector1.Fdp(_inputKVector2).AsMultivector(),

                GaFuLLanguageOperationKind.BinaryHestenesInnerProduct =>
                    _operationSpecs.IsEuclidean 
                        ? _inputKVector1.EHip(_inputKVector2).AsMultivector()
                        : _inputKVector1.Hip(_inputKVector2).AsMultivector(),

                _ => throw new InvalidOperationException()
            };

            _outputKVector = mv.GetKVectorPart(_outputGrade);

            _outputKVector.SetIsOutput(true);
        }

        protected override void DefineContextExternalNames(SymbolicContext context)
        {
            context.SetExternalNamesByTermIndex(
                _inputKVector1.KVectorStorage,
                index => $"mv1[{index}]"
            );

            context.SetExternalNamesByTermIndex(
                _inputKVector2.KVectorStorage,
                index => $"mv2[{index}]"
            );

            context.SetExternalNamesByTermIndex(
                _outputKVector.KVectorStorage,
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
