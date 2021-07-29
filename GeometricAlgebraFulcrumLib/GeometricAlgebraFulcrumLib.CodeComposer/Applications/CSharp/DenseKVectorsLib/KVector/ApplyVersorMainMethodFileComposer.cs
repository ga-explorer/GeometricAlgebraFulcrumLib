using System;
using GeometricAlgebraFulcrumLib.CodeComposer.Languages;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Structured;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib.KVector
{
    internal sealed class ApplyVersorMainMethodFileComposer : 
        GaLibraryFileComposerBase 
    {
        internal GaLanguageOperationSpecs OperationSpecs { get; }


        internal ApplyVersorMainMethodFileComposer(GaLibraryComposer libGen, GaLanguageOperationSpecs opSpecs)
            : base(libGen)
        {
            OperationSpecs = opSpecs;
        }


        public override void Generate()
        {
            GenerateKVectorFileStartCode();

            var t2 = Templates["applyversor_main_case"];

            var casesText = new ListTextComposer(Environment.NewLine);

            foreach (var inGrade1 in Processor.Grades)
            {
                foreach (var inGrade2 in Processor.Grades)
                {
                    var outGrade = inGrade2;

                    var id = inGrade1 + inGrade2 * Processor.GradesCount;

                    var name = OperationSpecs.GetName(
                        inGrade1, inGrade2, outGrade
                    );

                    casesText.Add(t2,
                        "name", name,
                        "id", id,
                        "g1", inGrade1,
                        "g2", inGrade2,
                        "grade", outGrade,
                        "signature", CurrentNamespace
                    );
                }
            }
            
            TextComposer.AppendAtNewLine(
                Templates["applyversor_main"],
                "name", OperationSpecs.GetName(),
                "signature", CurrentNamespace,
                "cases", casesText
            );

            GenerateKVectorFileFinishCode();

            FileComposer.FinalizeText();
        }
    }
}
