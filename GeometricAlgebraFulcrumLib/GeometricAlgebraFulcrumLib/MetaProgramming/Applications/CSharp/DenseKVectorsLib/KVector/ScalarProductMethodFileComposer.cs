using System;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Variables;
using GeometricAlgebraFulcrumLib.MetaProgramming.Languages;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.DenseKVectorsLib.KVector
{
    internal sealed class ScalarProductMethodFileComposer : 
        GaFuLLibraryMetaContextFileComposerBase
    {
        private readonly GaFuLLanguageOperationSpecs _operationSpecs;
        private readonly int _inputGrade;
        private readonly int _outputGrade = 0;
        private XGaKVector<IMetaExpressionAtomic> _inputKVector1;
        private XGaKVector<IMetaExpressionAtomic> _inputKVector2;
        private MetaExpressionVariableComputed _outputScalar;


        internal ScalarProductMethodFileComposer(GaFuLLibraryComposer libGen, GaFuLLanguageOperationSpecs opSpecs, int inGrade)
            : base(libGen)
        {
            _operationSpecs = opSpecs;
            _inputGrade = inGrade;
        }


        protected override void DefineContextParameters(MetaContext context)
        {
            _inputKVector1 = context.ParameterVariablesFactory.CreateDenseKVector(
                VSpaceDimensions,
                _inputGrade,
                index => $"kVector1Scalar{index}"
            );

            _inputKVector2 = context.ParameterVariablesFactory.CreateDenseKVector(
                VSpaceDimensions,
                _inputGrade,
                index => $"kVector2Scalar{index}"
            );
        }

        protected override void DefineContextComputations(MetaContext context)
        {
            var outputScalar = _operationSpecs.OperationKind switch
            {
                GaFuLLanguageOperationKind.BinaryScalarProduct =>
                    _operationSpecs.IsEuclidean
                        ? _inputKVector1.ESp(_inputKVector2)
                        : _inputKVector1.Sp(_inputKVector2),

                _ => throw new InvalidOperationException()
            };

            _outputScalar = (MetaExpressionVariableComputed) outputScalar.ScalarValue();

            _outputScalar.IsOutputVariable = true;
        }

        protected override void DefineContextExternalNames(MetaContext context)
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

            var kvSpaceDimensions = 
                VSpaceDimensions.KVectorSpaceDimension(_outputGrade);

            var methodName =
                _operationSpecs.GetName(_inputGrade, _inputGrade);

            TextComposer.AppendAtNewLine(
                Templates["bilinearproduct"],
                "name", methodName,
                "num", kvSpaceDimensions,
                "double", GeoLanguage.ScalarTypeName,
                "computations", computationsText
            );

            GenerateBladeFileFinishCode();

            FileComposer.FinalizeText();
        }
    }
}