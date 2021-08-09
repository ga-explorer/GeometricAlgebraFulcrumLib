using System;
using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.CodeComposer.Languages;
using GeometricAlgebraFulcrumLib.Geometry.Subspaces;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;
using GeometricAlgebraFulcrumLib.Storage;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib.KVector
{
    internal sealed class ApplyVersorMethodFileComposer : 
        GaLibrarySymbolicContextFileComposerBase
    {
        private IGaSubspace<ISymbolicExpressionAtomic> _subspace;
        private IGaStorageKVector<ISymbolicExpressionAtomic> _inputKVector;
        private IGaStorageKVector<ISymbolicExpressionAtomic> _outputKVector;
        private readonly GaLanguageOperationSpecs _operationSpecs;
        private readonly uint _inputGrade1;
        private readonly uint _inputGrade2;


        internal ApplyVersorMethodFileComposer(GaLibraryComposer libGen, GaLanguageOperationSpecs opSpecs, uint inGrade1, uint inGrade2)
            : base(libGen)
        {
            _operationSpecs = opSpecs;
            _inputGrade1 = inGrade1;
            _inputGrade2 = inGrade2;
        }


        protected override void DefineContextParameters(SymbolicContext context)
        {
            var subspaceKVector = context.ParameterVariablesFactory.CreateDenseKVector(
                VSpaceDimension,
                _inputGrade1,
                index => $"versorScalar{index}"
            );

            _subspace = _operationSpecs.IsEuclidean
                ? Processor.CreateSubspace(subspaceKVector)
                : EuclideanProcessor.CreateSubspace(subspaceKVector);

            _inputKVector = context.ParameterVariablesFactory.CreateDenseKVector(
                VSpaceDimension,
                _inputGrade2,
                index => $"kVectorScalar{index}"
            );
        }

        protected override void DefineContextComputations(SymbolicContext context)
        {
            _outputKVector = _operationSpecs.OperationKind switch
            {
                GaLanguageOperationKind.BinaryProject => 
                    _subspace.Project(_inputKVector).GetKVectorPart(_inputGrade2),

                GaLanguageOperationKind.BinaryRotate => 
                    _subspace.Rotate(_inputKVector).GetKVectorPart(_inputGrade2),

                GaLanguageOperationKind.BinaryReflect => 
                    _subspace.Reflect(_inputKVector).GetKVectorPart(_inputGrade2),

                GaLanguageOperationKind.BinaryComplement => 
                    _subspace.Complement(_inputKVector).GetKVectorPart(_inputGrade2),

                _ => throw new InvalidOperationException()
            };

            _outputKVector.SetIsOutput(true);
        }

        protected override void DefineContextExternalNames(SymbolicContext context)
        {
            context.SetExternalNamesByTermIndex(
                _subspace.Blade,
                index => $"scalars1[{index}]"
            );

            context.SetExternalNamesByTermIndex(
                _inputKVector,
                index => $"scalars2[{index}]"
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
                this.KvSpaceDimension(_inputGrade2);

            var methodName =
                _operationSpecs.GetName(_inputGrade1, _inputGrade2, _inputGrade2);
            
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
