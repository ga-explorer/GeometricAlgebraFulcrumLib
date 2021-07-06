using System;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Structured;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib.KVector
{
    internal sealed class ApplyVersorMainMethodFileComposer : 
        GaLibraryFileComposerBase 
    {
        internal GaClcOperationSpecs OperationSpecs { get; }


        internal ApplyVersorMainMethodFileComposer(GaLibraryComposer libGen, GaClcOperationSpecs opSpecs)
            : base(libGen)
        {
            OperationSpecs = opSpecs;
        }


        public override void Generate()
        {
            GenerateKVectorFileStartCode();

            var t2 = Templates["applyversor_main_case"];

            var casesText = new ListTextComposer(Environment.NewLine);

            foreach (var inGrade1 in MultivectorProcessor.BasisSet.Grades)
            {
                foreach (var inGrade2 in MultivectorProcessor.BasisSet.Grades)
                {
                    var outGrade = inGrade2;

                    var id = inGrade1 + inGrade2 * MultivectorProcessor.BasisSet.GradesCount;

                    var name = OperationSpecs.GetName(
                        inGrade1, inGrade2, outGrade
                    );

                    casesText.Add(t2,
                        "name", name,
                        "id", id,
                        "g1", inGrade1,
                        "g2", inGrade2,
                        "grade", outGrade,
                        "frame", CurrentNamespace
                    );
                }
            }
            
            TextComposer.AppendAtNewLine(
                Templates["applyversor_main"],
                "name", OperationSpecs.GetName(),
                "frame", CurrentNamespace,
                "cases", casesText
            );

            GenerateKVectorFileFinishCode();

            FileComposer.FinalizeText();
        }
    }
}
