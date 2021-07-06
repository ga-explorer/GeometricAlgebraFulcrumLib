using System;
using GeometricAlgebraFulcrumLib.Algebra.Signatures;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;
using GeometricAlgebraFulcrumLib.Storage;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Structured;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib.KVector
{
    internal sealed class UnaryGpMethodsFileComposer 
        : GaLibrarySymbolicContextFileComposerBase
    {
        private int _inGrade;
        private int _outGrade;
        private IGaKVectorStorage<ISymbolicExpressionAtomic> _inputKVector;
        private IGaKVectorStorage<ISymbolicExpressionAtomic> _outputKVector;

        internal GaClcOperationSpecs OperationSpecs { get; }


        internal UnaryGpMethodsFileComposer(GaLibraryComposer libGen, GaClcOperationSpecs opSpecs)
            : base(libGen)
        {
            OperationSpecs = opSpecs;
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
            _outputKVector = OperationSpecs.OperationKind switch
            {
                GaClcOperationKind.UnaryGeometricProductSquared
                    => OperationSpecs.IsEuclidean
                        ? _inputKVector.EGp(_inputKVector).GetKVectorPart(_outGrade)
                        : MultivectorProcessor.Gp(_inputKVector, _inputKVector).GetKVectorPart(_outGrade),

                GaClcOperationKind.UnaryGeometricProductReverse
                    => OperationSpecs.IsEuclidean
                        ? _inputKVector.EGp(_inputKVector.GetReverse()).GetKVectorPart(_outGrade)
                        : MultivectorProcessor.Gp(_inputKVector, _inputKVector.GetReverse()).GetKVectorPart(_outGrade),

                _ => throw new InvalidOperationException()
            };

            _outputKVector.SetIsOutput(true);
        }

        protected override void DefineContextExternalNames(SymbolicContext context)
        {
            context.SetExternalNamesByTermIndex(
                _inputKVector,
                index => $"scalars[{index}]"
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
        

        private void GenerateMethod(string funcName, int inputGrade, int outputGrade)
        {
            _inGrade = inputGrade;
            _outGrade = outputGrade;

            var computationsText = GenerateCode();

            var kvSpaceDim = 
                MultivectorProcessor.BasisSet.KvSpaceDimension(_outGrade);

            TextComposer.AppendAtNewLine(
                Templates["self_bilinearproduct"],
                "name", funcName,
                "num", kvSpaceDim,
                "double", GaClcLanguage.ScalarTypeName,
                "computations", computationsText
            );
        }

        private void GenerateMethods(int inputGrade)
        {
            var gpCaseText = new ListTextComposer("," + Environment.NewLine);

            var gradesList = 
                MultivectorProcessor.BasisSet.GradesOfEGp(inputGrade, inputGrade);

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
                    "frame", CurrentNamespace,
                    "grade", outputGrade,
                    "name", funcName
                );
            }

            TextComposer.AppendAtNewLine(
                Templates["selfgp"],
                "frame", CurrentNamespace,
                "name", OperationSpecs.GetName(inputGrade, inputGrade),
                "double", GaClcLanguage.ScalarTypeName,
                "selfgp_case", gpCaseText
            );
        }

        private void GenerateMainMethod()
        {
            var casesTemplate = Templates["selfgp_main_case"];

            var casesText = new ListTextComposer(Environment.NewLine);

            foreach (var grade in MultivectorProcessor.BasisSet.Grades)
            {
                casesText.Add(
                    casesTemplate,
                    "name", OperationSpecs.GetName(grade, grade),
                    "grade", grade,
                    "frame", CurrentNamespace
                );
            }

            TextComposer.AppendAtNewLine(
                Templates["selfgp_main"],
                "name", OperationSpecs,
                "frame", CurrentNamespace,
                "cases", casesText
            );
        }

        public override void Generate()
        {
            GenerateBladeFileStartCode();

            foreach (var grade in MultivectorProcessor.BasisSet.Grades)
                GenerateMethods(grade);

            GenerateMainMethod();

            GenerateBladeFileFinishCode();

            FileComposer.FinalizeText();
        }

    }
}
