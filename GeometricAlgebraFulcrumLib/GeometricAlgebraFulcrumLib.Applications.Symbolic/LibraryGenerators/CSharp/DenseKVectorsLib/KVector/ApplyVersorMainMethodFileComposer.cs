using GeometricAlgebraFulcrumLib.MetaProgramming.Utilities.Code;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Structured;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.CSharp.DenseKVectorsLib.KVector;

internal sealed class ApplyVersorMainMethodFileComposer : 
    GaFuLLibraryFileComposerBase 
{
    internal GaFuLLanguageOperationSpecs OperationSpecs { get; }


    internal ApplyVersorMainMethodFileComposer(GaFuLLibraryComposer libGen, GaFuLLanguageOperationSpecs opSpecs)
        : base(libGen)
    {
        OperationSpecs = opSpecs;
    }


    public override void Generate()
    {
        GenerateKVectorFileStartCode();

        var t2 = Templates["applyversor_main_case"];

        var casesText = new ListTextComposer(Environment.NewLine);

        foreach (var inGrade1 in Grades)
        {
            foreach (var inGrade2 in Grades)
            {
                var outGrade = inGrade2;

                var id = inGrade1 + inGrade2 * GradesCount;

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