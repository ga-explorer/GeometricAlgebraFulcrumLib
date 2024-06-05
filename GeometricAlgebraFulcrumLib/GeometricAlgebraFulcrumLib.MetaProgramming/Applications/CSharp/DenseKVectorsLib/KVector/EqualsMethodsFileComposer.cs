using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Structured;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.DenseKVectorsLib.KVector;

internal sealed class EqualsMethodsFileComposer : 
    GaFuLLibraryFileComposerBase
{
    internal EqualsMethodsFileComposer(GaFuLLibraryComposer libGen)
        : base(libGen)
    {
    }


    private void GenerateEqualsFunction(ulong kvSpaceDim)
    {
        var caseTemplate = Templates["equals_case"];

        var casesText = new ListTextComposer(Environment.NewLine);

        for (var i = 0UL; i < kvSpaceDim; i++)
            casesText.Add(caseTemplate, "num", i);

        TextComposer.AppendAtNewLine(
            Templates["equals"],
            "num", kvSpaceDim,
            "double", GeoLanguage.ScalarTypeName,
            "cases", casesText
        );
    }

    private void GenerateMainEqualsFunction()
    {
        var caseTemplate = Templates["main_equals_case"];

        var casesText = new ListTextComposer(Environment.NewLine);

        foreach (var grade in Grades)
            casesText.Add(caseTemplate,
                "grade", grade,
                "num", VSpaceDimensions.KVectorSpaceDimension(grade)
            );

        TextComposer.AppendAtNewLine(
            Templates["main_equals"],
            "signature", CurrentNamespace,
            "cases", casesText
        );
    }

    public override void Generate()
    {
        GenerateKVectorFileStartCode();

        var kvSpaceDimList =
            VSpaceDimensions
                .GetRange()
                .Select(grade => VSpaceDimensions.KVectorSpaceDimension(grade))
                .Distinct();

        foreach (var kvSpaceDim in kvSpaceDimList)
            GenerateEqualsFunction(kvSpaceDim);

        GenerateMainEqualsFunction();

        GenerateKVectorFileFinishCode();

        FileComposer.FinalizeText();
    }
}