using GeometricAlgebraFulcrumLib.MetaProgramming.Utilities.Code;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Structured;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.DenseKVectorsLib.KVector;

internal sealed class GpMethodsFileComposer : 
    GaFuLLibraryFileComposerBase 
{
    internal GaFuLLanguageOperationSpecs OperationSpecs { get; }


    internal GpMethodsFileComposer(GaFuLLibraryComposer libGen, GaFuLLanguageOperationSpecs opSpecs)
        : base(libGen)
    {
        OperationSpecs = opSpecs;
    }


    internal void GenerateMainMethod()
    {
        var caseTemplate = Templates["gp_main_case"];

        var casesText = new ListTextComposer(Environment.NewLine);

        foreach (var inGrade1 in Grades)
        foreach (var inGrade2 in Grades)
        {
            var id = inGrade1 + inGrade2 * GradesCount;

            var name = OperationSpecs.GetName(
                inGrade1, inGrade2
            );

            casesText.Add(
                caseTemplate,
                "name", name,
                "id", id,
                "g1", inGrade1,
                "g2", inGrade2,
                "signature", CurrentNamespace
            );
        }

        TextComposer.AppendAtNewLine(
            Templates["gp_main"],
            "name", OperationSpecs,
            "signature", CurrentNamespace,
            "cases", casesText
        );
    }

    internal void GenerateIntermediateMethod(string gpCaseText, string name)
    {
        TextComposer.AppendAtNewLine(
            Templates["gp"],
            "signature", CurrentNamespace,
            "name", name,
            "double", GeoLanguage.ScalarTypeName,
            "gp_case", gpCaseText
        );
    }


    public override void Generate()
    {
        throw new NotImplementedException();
    }
}