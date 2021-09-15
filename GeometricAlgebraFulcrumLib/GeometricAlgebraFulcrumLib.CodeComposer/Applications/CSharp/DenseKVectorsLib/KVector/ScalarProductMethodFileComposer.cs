using System;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.Variables;
using GeometricAlgebraFulcrumLib.CodeComposer.Languages;
using GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Context;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib.KVector
{
    internal sealed class ScalarProductMethodFileComposer : 
        GaFuLLibrarySymbolicContextFileComposerBase
    {
        private readonly GaFuLLanguageOperationSpecs _operationSpecs;
        private readonly uint _inputGrade;
        private readonly uint _outputGrade = 0U;
        private KVectorStorage<ISymbolicExpressionAtomic> _inputKVector1;
        private KVectorStorage<ISymbolicExpressionAtomic> _inputKVector2;
        private SymbolicVariableComputed _outputScalar;


        internal ScalarProductMethodFileComposer(GaFuLLibraryComposer libGen, GaFuLLanguageOperationSpecs opSpecs, uint inGrade)
            : base(libGen)
        {
            _operationSpecs = opSpecs;
            _inputGrade = inGrade;
        }


        protected override void DefineContextParameters(SymbolicContext context)
        {
            _inputKVector1 = context.ParameterVariablesFactory.CreateDenseKVector(
                VSpaceDimension,
                _inputGrade,
                index => $"kVector1Scalar{index}"
            );

            _inputKVector2 = context.ParameterVariablesFactory.CreateDenseKVector(
                VSpaceDimension,
                _inputGrade,
                index => $"kVector2Scalar{index}"
            );
        }

        protected override void DefineContextComputations(SymbolicContext context)
        {
            var outputScalar = _operationSpecs.OperationKind switch
            {
                GaFuLLanguageOperationKind.BinaryScalarProduct =>
                    _operationSpecs.IsEuclidean
                        ? GeometricProcessor.ESp(_inputKVector1, _inputKVector2)
                        : GeometricProcessor.Sp(_inputKVector1, _inputKVector2),

                _ => throw new InvalidOperationException()
            };

            _outputScalar = (SymbolicVariableComputed) outputScalar;

            _outputScalar.IsOutputVariable = true;
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

            _outputScalar.ExternalName = "c";
            
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

            var methodName =
                _operationSpecs.GetName(_inputGrade, _inputGrade);

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