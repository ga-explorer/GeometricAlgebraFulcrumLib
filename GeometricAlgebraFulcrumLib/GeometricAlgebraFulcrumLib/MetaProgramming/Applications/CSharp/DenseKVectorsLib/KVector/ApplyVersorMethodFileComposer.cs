using System;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Geometry.Subspaces;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Languages;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.DenseKVectorsLib.KVector
{
    internal sealed class ApplyVersorMethodFileComposer : 
        GaFuLLibraryMetaContextFileComposerBase
    {
        private ISubspace<IMetaExpressionAtomic> _subspace;
        private GaKVector<IMetaExpressionAtomic> _inputKVector;
        private GaKVector<IMetaExpressionAtomic> _outputKVector;
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


        protected override void DefineContextParameters(MetaContext context)
        {
            var subspaceKVector = context.ParameterVariablesFactory.CreateDenseKVector(
                VSpaceDimension,
                _inputGrade1,
                index => $"versorScalar{index}"
            );

            _subspace = _operationSpecs.IsEuclidean
                ? GeometricProcessor.CreateSubspace(subspaceKVector.KVectorStorage)
                : EuclideanProcessor.CreateSubspace(subspaceKVector.KVectorStorage);

            _inputKVector = context.ParameterVariablesFactory.CreateDenseKVector(
                VSpaceDimension,
                _inputGrade2,
                index => $"kVectorScalar{index}"
            );
        }

        protected override void DefineContextComputations(MetaContext context)
        {
            var inputSubspace = 
                GeometricProcessor.CreateSubspace(_inputKVector.KVectorStorage);

            _outputKVector = _operationSpecs.OperationKind switch
            {
                GaFuLLanguageOperationKind.BinaryProject => 
                    _subspace.Project(inputSubspace).GetBlade(),//.GetKVectorPart(_inputGrade2),

                //GaFuLLanguageOperationKind.BinaryRotate => 
                //    _subspace.Rotate(inputSubspace).Blade,//.GetKVectorPart(_inputGrade2),

                GaFuLLanguageOperationKind.BinaryReflect => 
                    _subspace.Reflect(inputSubspace).GetBlade(),//.GetKVectorPart(_inputGrade2),

                GaFuLLanguageOperationKind.BinaryComplement => 
                    _subspace.Complement(inputSubspace).GetBlade(),//.GetKVectorPart(_inputGrade2),

                _ => throw new InvalidOperationException()
            };

            _outputKVector.SetIsOutput(true);
        }

        protected override void DefineContextExternalNames(MetaContext context)
        {
            context.SetExternalNamesByTermIndex(
                _subspace.GetBlade().KVectorStorage,
                index => $"scalars1[{index}]"
            );

            context.SetExternalNamesByTermIndex(
                _inputKVector.KVectorStorage,
                index => $"scalars2[{index}]"
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
