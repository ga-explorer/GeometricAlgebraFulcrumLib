using System;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Structured;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib.KVector
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


        private void GenerateMethods(uint inGrade1, uint inGrade2)
        {
            var gpCaseText = new ListTextComposer(Environment.NewLine);
            var gradesList = 
                this
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
                    "num", this.KvSpaceDimension(outGrade),
                    "signature", CurrentNamespace,
                    "grade", outGrade
                );
            }

            TextComposer.AppendAtNewLine(
                Templates["dp"],
                "signature", CurrentNamespace,
                "name", OperationSpecs.GetName(inGrade1, inGrade2),
                "double", GaClcLanguage.ScalarTypeName,
                "dp_case", gpCaseText
            );
        }

        private void GenerateMainMethod()
        {
            var casesText = new ListTextComposer(Environment.NewLine);

            foreach (var inGrade1 in Processor.Grades)
            {
                foreach (var inGrade2 in Processor.Grades)
                {
                    var id = inGrade1 + inGrade2 * Processor.GradesCount;

                    casesText.Add(Templates["dp_main_case"],
                        "name", OperationSpecs.GetName(inGrade1, inGrade2),
                        "id", id,
                        "g1", inGrade1,
                        "g2", inGrade2,
                        "signature", CurrentNamespace
                    );
                }
            }

            TextComposer.AppendAtNewLine(
                Templates["dp_main"],
                "name", OperationSpecs,
                "signature", CurrentNamespace,
                "cases", casesText
            );
        }

        public override void Generate()
        {
            GenerateKVectorFileStartCode();

            foreach (var grade1 in Processor.Grades)
                foreach (var grade2 in Processor.Grades)
                    GenerateMethods(grade1, grade2);

            GenerateMainMethod();

            GenerateKVectorFileFinishCode();

            FileComposer.FinalizeText();
        }
    }
}
