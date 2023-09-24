using System;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Variables;
using GeometricAlgebraFulcrumLib.MetaProgramming.Languages;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Structured;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.DenseKVectorsLib.KVector
{
    /// <summary>
    /// This class gives an example of generating several related macros into a single file
    /// Before calling the protected GenerateComputationsCode() method we must call the protected
    /// SetBaseMetaContext() method to change the base macro
    /// </summary>
    internal sealed class NormMethodsFileComposer 
        : GaFuLLibraryMetaContextFileComposerBase
    {
        private int _inGrade;
        private XGaKVector<IMetaExpressionAtomic> _inputKVector;
        private MetaExpressionVariableComputed _outputScalar;
        private GaFuLLanguageOperationSpecs _operationSpecs;

        private readonly GaFuLLanguageOperationSpecs[] _operationSpecsArray 
            = {
                GaFuLLanguageOperationKind.UnaryNorm.CreateEuclideanOperationSpecs(),
                GaFuLLanguageOperationKind.UnaryNorm.CreateMetricOperationSpecs(),

                GaFuLLanguageOperationKind.UnaryNormSquared.CreateEuclideanOperationSpecs(),
                GaFuLLanguageOperationKind.UnaryNormSquared.CreateMetricOperationSpecs(),
                
                //GeoClcOperationKind.UnaryMagnitude.CreateEuclideanOperationSpecs(),
                //GeoClcOperationKind.UnaryMagnitude.CreateMetricOperationSpecs(),
                
                //GeoClcOperationKind.UnaryMagnitudeSquared.CreateEuclideanOperationSpecs(),
                //GeoClcOperationKind.UnaryMagnitudeSquared.CreateMetricOperationSpecs(),
            };


        internal NormMethodsFileComposer(GaFuLLibraryComposer libGen)
            : base(libGen)
        {
        }

        
        protected override void DefineContextParameters(MetaContext context)
        {
            _inputKVector = context.ParameterVariablesFactory.CreateDenseKVector(
                VSpaceDimensions,
                _inGrade,
                index => $"kVectorScalar{index}"
            );
        }

        protected override void DefineContextComputations(MetaContext context)
        {
            var outputScalar = _operationSpecs.OperationKind switch
            {
                GaFuLLanguageOperationKind.UnaryNorm =>
                    _operationSpecs.IsEuclidean
                        ? _inputKVector.ENorm() 
                        : _inputKVector.Norm(),

                GaFuLLanguageOperationKind.UnaryNormSquared =>
                    _operationSpecs.IsEuclidean
                        ? _inputKVector.ENormSquared() 
                        : _inputKVector.NormSquared(),

                //GeoClcOperationKind.UnaryMagnitude =>
                //    OpSpecs.IsEuclidean
                //        ? _inputKVector.EMagnitude() 
                //        : _inputKVector.Magnitude(GeometricProcessor),

                //GeoClcOperationKind.UnaryMagnitudeSquared =>
                //    OpSpecs.IsEuclidean
                //        ? _inputKVector.EMagnitudeSquared() 
                //        : _inputKVector.MagnitudeSquared(GeometricProcessor),

                _ => throw new InvalidOperationException()
            };

            _outputScalar = (MetaExpressionVariableComputed) outputScalar.ScalarValue();

            _outputScalar.IsOutputVariable = true;
        }

        protected override void DefineContextExternalNames(MetaContext context)
        {
            _inputKVector.SetExternalNamesByTermIndex(
                index => $"scalars[{index}]"
            );

            _outputScalar.ExternalName = "result";
            
            context.SetIntermediateExternalNamesByNameIndex(
                DenseKVectorsLibraryComposer.MaxTargetLocalVars,
                index => $"tempVar{index:X4}",
                index => $"tempArray[{index}]"
            );
        }

        private void GenerateNormFunction(GaFuLLanguageOperationSpecs opSpecs, int inGrade)
        {
            _inGrade = inGrade;
            _operationSpecs = opSpecs;

            var computationsCode = GenerateCode();

            TextComposer.AppendAtNewLine(
                Templates["norm"],
                "name", opSpecs,
                "grade", inGrade,
                "double", GeoLanguage.ScalarTypeName,
                "computations", computationsCode
            );
        }

        private void GenerateMainNormFunction(string opSpecs)
        {
            var caseTemplate = Templates["main_norm_case"];

            var casesText = new ListTextComposer(Environment.NewLine);

            foreach (var grade in Grades)
                casesText.Add(
                    caseTemplate,
                    "name", opSpecs,
                    "grade", grade
                );

            TextComposer.AppendAtNewLine(
                Templates["main_norm"],
                "name", opSpecs,
                "double", GeoLanguage.ScalarTypeName,
                "main_norm_case", casesText
            );
        }

        public override void Generate()
        {
            GenerateBladeFileStartCode();

            foreach (var opSpecs in _operationSpecsArray)
            {
                GenerateBeginRegion(opSpecs.GetName());

                foreach (var inGrade in Grades)
                    GenerateNormFunction(opSpecs, inGrade);

                GenerateMainNormFunction(opSpecs.GetName());

                GenerateEndRegion();
            }

            GenerateBladeFileFinishCode();

            FileComposer.FinalizeText();
        }
    }
}
