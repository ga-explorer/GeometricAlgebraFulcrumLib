using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MetaProgramming.Utilities.Code;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Structured;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.CSharp.DenseKVectorsLib.KVector;

internal sealed class DpMethodsFileComposer : 
    GaFuLLibraryFileComposerBase 
{
    internal GaFuLLanguageOperationSpecs OperationSpecs { get; }


    internal DpMethodsFileComposer(GaFuLLibraryComposer libGen, GaFuLLanguageOperationSpecs opSpecs)
        : base(libGen)
    {
        OperationSpecs = opSpecs;
    }


    private void GenerateMethods(int inGrade1, int inGrade2)
    {
        var gpCaseText = new ListTextComposer(Environment.NewLine);
        var gradesList = 
            VSpaceDimensions
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
                "num", VSpaceDimensions.KVectorSpaceDimension(outGrade),
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

        foreach (var inGrade1 in Grades)
        {
            foreach (var inGrade2 in Grades)
            {
                var id = inGrade1 + inGrade2 * GradesCount;

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

        foreach (var grade1 in Grades)
        foreach (var grade2 in Grades)
            GenerateMethods(grade1, grade2);

        GenerateMainMethod();

        GenerateKVectorFileFinishCode();

        FileComposer.FinalizeText();
    }
}