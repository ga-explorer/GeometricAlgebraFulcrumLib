using System;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Languages;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Structured;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.DenseKVectorsLib.KVector
{
    internal sealed class UnaryGpMethodsFileComposer 
        : GaFuLLibraryMetaContextFileComposerBase
    {
        private uint _inGrade;
        private uint _outGrade;
        private GaKVector<IMetaExpressionAtomic> _inputKVector;
        private GaKVector<IMetaExpressionAtomic> _outputKVector;

        internal GaFuLLanguageOperationSpecs OperationSpecs { get; }


        internal UnaryGpMethodsFileComposer(GaFuLLibraryComposer libGen, GaFuLLanguageOperationSpecs opSpecs)
            : base(libGen)
        {
            OperationSpecs = opSpecs;
        }

        
        protected override void DefineContextParameters(MetaContext context)
        {
            _inputKVector = context.ParameterVariablesFactory.CreateDenseKVector(
                VSpaceDimension,
                _inGrade,
                index => $"kVectorScalar{index}"
            );
        }
        
        protected override void DefineContextComputations(MetaContext context)
        {
            _outputKVector = OperationSpecs.OperationKind switch
            {
                GaFuLLanguageOperationKind.UnaryGeometricProductSquared
                    => OperationSpecs.IsEuclidean
                        ? _inputKVector.EGp(_inputKVector).GetKVectorPart(_outGrade)
                        : _inputKVector.Gp(_inputKVector).GetKVectorPart(_outGrade),

                GaFuLLanguageOperationKind.UnaryGeometricProductReverse
                    => OperationSpecs.IsEuclidean
                        ? _inputKVector.EGp(_inputKVector.Reverse()).GetKVectorPart(_outGrade)
                        : _inputKVector.Gp(_inputKVector.Reverse()).GetKVectorPart(_outGrade),

                _ => throw new InvalidOperationException()
            };

            _outputKVector.SetIsOutput(true);
        }

        protected override void DefineContextExternalNames(MetaContext context)
        {
            context.SetExternalNamesByTermIndex(
                _inputKVector.KVectorStorage,
                index => $"scalars[{index}]"
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
        

        private void GenerateMethod(string funcName, uint inputGrade, uint outputGrade)
        {
            _inGrade = inputGrade;
            _outGrade = outputGrade;

            var computationsText = GenerateCode();

            var kvSpaceDim = 
                this.KVectorSpaceDimension(_outGrade);

            TextComposer.AppendAtNewLine(
                Templates["self_bilinearproduct"],
                "name", funcName,
                "num", kvSpaceDim,
                "double", GeoLanguage.ScalarTypeName,
                "computations", computationsText
            );
        }

        private void GenerateMethods(uint inputGrade)
        {
            var gpCaseText = new ListTextComposer("," + Environment.NewLine);

            var gradesList = 
                GeometricProcessor.GradesOfEGp(inputGrade, inputGrade);

            foreach (var outputGrade in gradesList)
            {
                var funcName = OperationSpecs.GetName(
                    inputGrade, inputGrade, outputGrade
                );

                GenerateMethod(
                    funcName,
                    inputGrade,
                    outputGrade
                );

                gpCaseText.Add(Templates["selfgp_case"],
                    "signature", CurrentNamespace,
                    "grade", outputGrade,
                    "name", funcName
                );
            }

            TextComposer.AppendAtNewLine(
                Templates["selfgp"],
                "signature", CurrentNamespace,
                "name", OperationSpecs.GetName(inputGrade, inputGrade),
                "double", GeoLanguage.ScalarTypeName,
                "selfgp_case", gpCaseText
            );
        }

        private void GenerateMainMethod()
        {
            var casesTemplate = Templates["selfgp_main_case"];

            var casesText = new ListTextComposer(Environment.NewLine);

            foreach (var grade in GeometricProcessor.Grades)
            {
                casesText.Add(
                    casesTemplate,
                    "name", OperationSpecs.GetName(grade, grade),
                    "grade", grade,
                    "signature", CurrentNamespace
                );
            }

            TextComposer.AppendAtNewLine(
                Templates["selfgp_main"],
                "name", OperationSpecs,
                "signature", CurrentNamespace,
                "cases", casesText
            );
        }

        public override void Generate()
        {
            GenerateBladeFileStartCode();

            foreach (var grade in GeometricProcessor.Grades)
                GenerateMethods(grade);

            GenerateMainMethod();

            GenerateBladeFileFinishCode();

            FileComposer.FinalizeText();
        }

    }
}
