using System;
using GeometricAlgebraFulcrumLib.CodeComposer.Languages;
using GeometricAlgebraFulcrumLib.Processing.Products;
using GeometricAlgebraFulcrumLib.Processing.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Variables;
using GeometricAlgebraFulcrumLib.Storage;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Structured;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib.KVector
{
    internal sealed class SdfMethodsFileComposer 
        : GaLibrarySymbolicContextFileComposerBase
    {
        private uint _inGrade;
        private IGasKVector<ISymbolicExpressionAtomic> _inputKVector;
        private SymbolicVariableComputed _outputScalar;
        private GaLanguageOperationSpecs _operationSpecs;

        private readonly GaLanguageOperationSpecs[] _operationSpecsArray = new[]
        {
            GaLanguageOperationKind.UnaryNorm.CreateEuclideanOperationSpecs(),
            GaLanguageOperationKind.UnaryNorm.CreateMetricOperationSpecs(),

            GaLanguageOperationKind.UnaryNormSquared.CreateEuclideanOperationSpecs(),
            GaLanguageOperationKind.UnaryNormSquared.CreateMetricOperationSpecs()

        };


        internal SdfMethodsFileComposer(GaLibraryComposer libGen)
            : base(libGen)
        {
        }

        
        protected override void DefineContextParameters(SymbolicContext context)
        {
            _inputKVector = context.ParameterVariablesFactory.CreateDenseKVector(
                VSpaceDimension,
                _inGrade,
                index => $"kVectorScalar{index}"
            );
        }

        protected override void DefineContextComputations(SymbolicContext context)
        {
            var outputScalar = _operationSpecs.OperationKind switch
            {
                GaLanguageOperationKind.UnaryNorm =>
                    _operationSpecs.IsEuclidean
                        ? _inputKVector.ENorm() 
                        : _inputKVector.Norm(Processor),

                GaLanguageOperationKind.UnaryNormSquared =>
                    _operationSpecs.IsEuclidean
                        ? _inputKVector.ENormSquared() 
                        : _inputKVector.NormSquared(Processor),

                //GaClcOperationKind.UnaryMagnitude =>
                //    OperationSpecs.IsEuclidean
                //        ? _inputKVector.EMagnitude() 
                //        : _inputKVector.Magnitude(Processor),

                //GaClcOperationKind.UnaryMagnitudeSquared =>
                //    OperationSpecs.IsEuclidean
                //        ? _inputKVector.EMagnitudeSquared() 
                //        : _inputKVector.MagnitudeSquared(Processor),

                _ => throw new InvalidOperationException()
            };

            _outputScalar = (SymbolicVariableComputed) outputScalar;

            _outputScalar.IsOutputVariable = true;
        }

        protected override void DefineContextExternalNames(SymbolicContext context)
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

        private void GenerateNormFunction(GaLanguageOperationSpecs opSpecs, uint inGrade)
        {
            _inGrade = inGrade;
            _operationSpecs = opSpecs;

            var computationsCode = GenerateCode();

            TextComposer.AppendAtNewLine(
                Templates["norm"],
                "name", opSpecs,
                "grade", inGrade,
                "double", GaLanguage.ScalarTypeName,
                "computations", computationsCode
            );
        }

        private void GenerateMainNormFunction(string opSpecs)
        {
            var caseTemplate = Templates["main_norm_case"];

            var casesText = new ListTextComposer(Environment.NewLine);

            foreach (var grade in Processor.Grades)
                casesText.Add(
                    caseTemplate,
                    "name", opSpecs,
                    "grade", grade
                );

            TextComposer.AppendAtNewLine(
                Templates["main_norm"],
                "name", opSpecs,
                "double", GaLanguage.ScalarTypeName,
                "main_norm_case", casesText
            );
        }

        public override void Generate()
        {
            GenerateBladeFileStartCode();

            foreach (var opSpecs in _operationSpecsArray)
            {
                GenerateBeginRegion(opSpecs.GetName());

                foreach (var inGrade in Processor.Grades)
                    GenerateNormFunction(opSpecs, inGrade);

                GenerateMainNormFunction(opSpecs.GetName());

                GenerateEndRegion();
            }

            GenerateBladeFileFinishCode();

            FileComposer.FinalizeText();
        }
    }
}