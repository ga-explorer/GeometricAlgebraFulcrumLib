using System;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra;
using GeometricAlgebraFulcrumLib.CodeComposer.Languages;
using GeometricAlgebraFulcrumLib.Geometry.Subspaces;
using GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Context;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib.KVector
{
    internal sealed class ApplyVersorMethodFileComposer : 
        GaFuLLibrarySymbolicContextFileComposerBase
    {
        private IGeoSubspace<ISymbolicExpressionAtomic> _subspace;
        private KVectorStorage<ISymbolicExpressionAtomic> _inputKVector;
        private KVectorStorage<ISymbolicExpressionAtomic> _outputKVector;
        private readonly GaFuLLanguageOperationSpecs _operationSpecs;
        private readonly uint _inputGrade1;
        private readonly uint _inputGrade2;


        internal ApplyVersorMethodFileComposer(GaFuLLibraryComposer libGen, GaFuLLanguageOperationSpecs opSpecs, uint inGrade1, uint inGrade2)
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
                ? GeometricProcessor.CreateSubspace(subspaceKVector)
                : EuclideanProcessor.CreateSubspace(subspaceKVector);

            _inputKVector = context.ParameterVariablesFactory.CreateDenseKVector(
                VSpaceDimension,
                _inputGrade2,
                index => $"kVectorScalar{index}"
            );
        }

        protected override void DefineContextComputations(SymbolicContext context)
        {
            var inputSubspace = 
                GeometricProcessor.CreateDirectSubspace(_inputKVector);

            _outputKVector = _operationSpecs.OperationKind switch
            {
                GaFuLLanguageOperationKind.BinaryProject => 
                    _subspace.Project(inputSubspace).Blade,//.GetKVectorPart(_inputGrade2),

                //GaFuLLanguageOperationKind.BinaryRotate => 
                //    _subspace.Rotate(inputSubspace).Blade,//.GetKVectorPart(_inputGrade2),

                GaFuLLanguageOperationKind.BinaryReflect => 
                    _subspace.Reflect(inputSubspace).Blade,//.GetKVectorPart(_inputGrade2),

                GaFuLLanguageOperationKind.BinaryComplement => 
                    _subspace.Complement(inputSubspace).Blade,//.GetKVectorPart(_inputGrade2),

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
                this.KVectorSpaceDimension(_inputGrade2);

            var methodName =
                _operationSpecs.GetName(_inputGrade1, _inputGrade2, _inputGrade2);
            
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
