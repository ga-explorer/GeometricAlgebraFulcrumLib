using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic.LinearMaps;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.DenseKVectorsLib.Outermorphism
{
    internal class MapMethodFileComposer : 
        GaFuLLibraryMetaContextFileComposerBase
    {
        private readonly int _inputGrade;
        private IMetaExpressionAtomic[,] _linearMapArray;
        private XGaKVector<IMetaExpressionAtomic> _inputKVector;
        private XGaKVector<IMetaExpressionAtomic> _outputKVector;


        internal MapMethodFileComposer(GaFuLLibraryComposer libGen, int inGrade)
            : base(libGen)
        {
            _inputGrade = inGrade;
        }

        
        protected override void DefineContextParameters(MetaContext context)
        {
            _linearMapArray = context.ParameterVariablesFactory.CreateDenseArray(
                VSpaceDimensions,
                VSpaceDimensions,
                (row, col) => $"omScalarR{row}C{col}"
            );

            _inputKVector = context.ParameterVariablesFactory.CreateDenseKVector(
                VSpaceDimensions,
                _inputGrade,
                index => $"kVectorScalar{index}"
            );
        }

        protected override void DefineContextComputations(MetaContext context)
        {
            var outermorphism =
                _linearMapArray
                    .ColumnsToLinVectors(GeometricProcessor.ScalarProcessor)
                    .CreateLinUnilinearMap(GeometricProcessor.ScalarProcessor)
                    .ToOutermorphism(GeometricProcessor);
                
            _outputKVector = outermorphism.OmMap(_inputKVector);

            _outputKVector.SetIsOutput(true);
        }

        protected override void DefineContextExternalNames(MetaContext context)
        {
            _inputKVector.SetExternalNamesByTermIndex(
                index => $"kVectorScalars[{index}]"
            );

            _outputKVector.SetExternalNamesByTermIndex(
                index => $"mappedKVectorScalars[{index}]"
            );

            context.SetIntermediateExternalNamesByNameIndex(
                DenseKVectorsLibraryComposer.MaxTargetLocalVars,
                index => $"tempVar{index:X4}",
                index => $"tempArray[{index}]"
            );
        }

        public override void Generate()
        {
            GenerateOutermorphismFileStartCode();

            var computationsText = GenerateCode();

            TextComposer.Append(
                Templates["om_apply"],
                "double", GeoLanguage.ScalarTypeName,
                "grade", _inputGrade,
                "num", VSpaceDimensions.KVectorSpaceDimension(_inputGrade),
                "computations", computationsText
            );

            GenerateOutermorphismFileFinishCode();

            FileComposer.FinalizeText();
        }
    }
}
