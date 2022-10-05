using System;
using System.Linq;
using GeometricAlgebraFulcrumLib.MetaProgramming.Languages;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Structured;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.DenseKVectorsLib.KVector
{
    internal sealed class DpMethodsFileComposer : 
        GaFuLLibraryFileComposerBase 
    {
        internal GaFuLLanguageOperationSpecs OperationSpecs { get; }


        internal DpMethodsFileComposer(GaFuLLibraryComposer libGen, GaFuLLanguageOperationSpecs opSpecs)
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
                    GaFuLLanguageOperationKind
                        .BinaryGeometricProduct
                        .CreateEuclideanOperationSpecs()
                        .GetName(inGrade1, inGrade2, outGrade);

                gpCaseText.Add(Templates["dp_case"],
                    "name", funcName,
                    "num", this.KVectorSpaceDimension(outGrade),
                    "signature", CurrentNamespace,
                    "grade", outGrade
                );
            }

            TextComposer.AppendAtNewLine(
                Templates["dp"],
                "signature", CurrentNamespace,
                "name", OperationSpecs.GetName(inGrade1, inGrade2),
                "double", GeoLanguage.ScalarTypeName,
                "dp_case", gpCaseText
            );
        }

        private void GenerateMainMethod()
        {
            var casesText = new ListTextComposer(Environment.NewLine);

            foreach (var inGrade1 in GeometricProcessor.Grades)
            {
                foreach (var inGrade2 in GeometricProcessor.Grades)
                {
                    var id = inGrade1 + inGrade2 * GeometricProcessor.GradesCount;

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

            foreach (var grade1 in GeometricProcessor.Grades)
                foreach (var grade2 in GeometricProcessor.Grades)
                    GenerateMethods(grade1, grade2);

            GenerateMainMethod();

            GenerateKVectorFileFinishCode();

            FileComposer.FinalizeText();
        }
    }
}
