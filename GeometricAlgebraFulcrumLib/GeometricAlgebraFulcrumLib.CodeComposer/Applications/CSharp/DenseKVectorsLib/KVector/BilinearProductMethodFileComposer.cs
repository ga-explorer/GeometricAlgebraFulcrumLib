using System;
using GeometricAlgebraFulcrumLib.Algebra.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;
using GeometricAlgebraFulcrumLib.Storage;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib.KVector
{
    internal sealed class BilinearProductMethodFileComposer : 
        GaLibrarySymbolicContextFileComposerBase
    {
        private IGaKVectorStorage<ISymbolicExpressionAtomic> _inputKVector1;
        private IGaKVectorStorage<ISymbolicExpressionAtomic> _inputKVector2;
        private IGaKVectorStorage<ISymbolicExpressionAtomic> _outputKVector;
        private readonly GaClcOperationSpecs _operationSpecs;
        private readonly int _inputGrade1;
        private readonly int _inputGrade2;
        private readonly int _outputGrade;


        internal BilinearProductMethodFileComposer(GaLibraryComposer libGen, GaClcOperationSpecs opSpecs, int inGrade1, int inGrade2)
            : base(libGen)
        {
            _operationSpecs = opSpecs;
            
            _inputGrade1 = inGrade1;
            _inputGrade2 = inGrade2;

            _outputGrade = opSpecs.GetKVectorsBilinearProductGrade(
                VSpaceDimension,
                inGrade1, 
                inGrade2
            );
        }
        
        internal BilinearProductMethodFileComposer(GaLibraryComposer libGen, GaClcOperationSpecs opSpecs, int inGrade1, int inGrade2, int outGrade)
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
                GaClcOperationKind.BinaryOuterProduct =>
                    _inputKVector1.Op(_inputKVector2).GetKVectorPart(_outputGrade),

                GaClcOperationKind.BinaryGeometricProduct =>
                    _operationSpecs.IsEuclidean 
                        ? _inputKVector1.EGp(_inputKVector2).GetKVectorPart(_outputGrade)
                        : _inputKVector1.Gp(_inputKVector2, MultivectorProcessor).GetKVectorPart(_outputGrade),

                GaClcOperationKind.BinaryGeometricProductDual =>
                    _operationSpecs.IsEuclidean 
                        ? _inputKVector1.EGp(_inputKVector2).EDual(VSpaceDimension).GetKVectorPart(_outputGrade)
                        : _inputKVector1.Gp(_inputKVector2, MultivectorProcessor).Dual(MultivectorProcessor).GetKVectorPart(_outputGrade),

                GaClcOperationKind.BinaryLeftContractionProduct =>
                    _operationSpecs.IsEuclidean 
                        ? _inputKVector1.ELcp(_inputKVector2).GetKVectorPart(_outputGrade)
                        : MultivectorProcessor.Lcp(_inputKVector1, _inputKVector2).GetKVectorPart(_outputGrade),

                GaClcOperationKind.BinaryRightContractionProduct =>
                    _operationSpecs.IsEuclidean 
                        ? _inputKVector1.ERcp(_inputKVector2).GetKVectorPart(_outputGrade)
                        : MultivectorProcessor.Rcp(_inputKVector1, _inputKVector2).GetKVectorPart(_outputGrade),

                GaClcOperationKind.BinaryFatDotProduct =>
                    _operationSpecs.IsEuclidean 
                        ? _inputKVector1.EFdp(_inputKVector2).GetKVectorPart(_outputGrade)
                        : MultivectorProcessor.Fdp(_inputKVector1, _inputKVector2).GetKVectorPart(_outputGrade),

                GaClcOperationKind.BinaryHestenesInnerProduct =>
                    _operationSpecs.IsEuclidean 
                        ? _inputKVector1.EHip(_inputKVector2).GetKVectorPart(_outputGrade)
                        : MultivectorProcessor.Hip(_inputKVector1, _inputKVector2).GetKVectorPart(_outputGrade),

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
                MultivectorProcessor.BasisSet.KvSpaceDimension(_outputGrade);

            var methodName = _operationSpecs.GetName(
                _inputGrade1, _inputGrade2, _outputGrade
            );

            TextComposer.AppendAtNewLine(
                Templates["bilinearproduct"],
                "name", methodName,
                "num", kvSpaceDimension,
                "double", GaClcLanguage.ScalarTypeName,
                "computations", computationsText
            );

            GenerateBladeFileFinishCode();

            FileComposer.FinalizeText();
        }
    }
}
