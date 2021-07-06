using System;
using System.Linq;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Structured;

namespace GeometricAlgebraLib.CodeComposer.Applications.CSharp.DenseKVectorsLib.KVector
{
    internal sealed class DpMethodsFileComposer : 
        GaLibraryFileComposerBase 
    {
        internal GaClcOperationSpecs OperationSpecs { get; }


        internal DpMethodsFileComposer(GaLibraryComposer libGen, GaClcOperationSpecs opSpecs)
            : base(libGen)
        {
            OperationSpecs = opSpecs;
        }


        private void GenerateMethods(int inGrade1, int inGrade2)
        {
            var gpCaseText = new ListTextComposer(Environment.NewLine);
            var gradesList = 
                MultivectorProcessor
                    .BasisSet
                    .GradesOfEGp(inGrade1, inGrade2)
                    .OrderByDescending(grade => grade);

            foreach (var outGrade in gradesList)
            {
                var funcName = 
                    GaClcOperationKind
                        .BinaryGeometricProduct
                        .CreateEuclideanOperationSpecs()
                        .GetName(inGrade1, inGrade2, outGrade);

                gpCaseText.Add(Templates["dp_case"],
                    "name", funcName,
                    "num", MultivectorProcessor.BasisSet.KvSpaceDimension(outGrade),
                    "frame", CurrentNamespace,
                    "grade", outGrade
                );
            }

            TextComposer.AppendAtNewLine(
                Templates["dp"],
                "frame", CurrentNamespace,
                "name", OperationSpecs.GetName(inGrade1, inGrade2),
                "double", GaClcLanguage.ScalarTypeName,
                "dp_case", gpCaseText
            );
        }

        private void GenerateMainMethod()
        {
            var casesText = new ListTextComposer(Environment.NewLine);

            foreach (var inGrade1 in MultivectorProcessor.BasisSet.Grades)
            {
                foreach (var inGrade2 in MultivectorProcessor.BasisSet.Grades)
                {
                    var id = inGrade1 + inGrade2 * MultivectorProcessor.BasisSet.GradesCount;

                    casesText.Add(Templates["dp_main_case"],
                        "name", OperationSpecs.GetName(inGrade1, inGrade2),
                        "id", id,
                        "g1", inGrade1,
                        "g2", inGrade2,
                        "frame", CurrentNamespace
                    );
                }
            }

            TextComposer.AppendAtNewLine(
                Templates["dp_main"],
                "name", OperationSpecs,
                "frame", CurrentNamespace,
                "cases", casesText
            );
        }

        public override void Generate()
        {
            GenerateKVectorFileStartCode();

            foreach (var grade1 in MultivectorProcessor.BasisSet.Grades)
                foreach (var grade2 in MultivectorProcessor.BasisSet.Grades)
                    GenerateMethods(grade1, grade2);

            GenerateMainMethod();

            GenerateKVectorFileFinishCode();

            FileComposer.FinalizeText();
        }
    }
}
