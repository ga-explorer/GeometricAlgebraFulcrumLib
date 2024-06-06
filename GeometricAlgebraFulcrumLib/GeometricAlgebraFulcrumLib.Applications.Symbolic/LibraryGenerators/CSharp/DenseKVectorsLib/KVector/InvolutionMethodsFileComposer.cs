using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MetaProgramming.Utilities.Code;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Structured;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.CSharp.DenseKVectorsLib.KVector;

internal sealed class InvolutionMethodsFileComposer : 
    GaFuLLibraryFileComposerBase
{
    internal InvolutionMethodsFileComposer(GaFuLLibraryComposer libGen)
        : base(libGen)
    {
    }


    private void GenerateNegativeFunction(ulong kvSpaceDim)
    {
        var caseTemplate = Templates["negative_case"];

        var casesText = new ListTextComposer("," + Environment.NewLine);

        for (var i = 0UL; i < kvSpaceDim; i++)
            casesText.Add(caseTemplate, "num", i);

        TextComposer.AppendAtNewLine(
            Templates["negative"],
            "num", kvSpaceDim,
            "double", GeoLanguage.ScalarTypeName,
            "cases", casesText
        );
    }

    private void GenerateMainInvolutionFunction(GaFuLLanguageOperationSpecs opSpecs, Func<int, bool> useNegative)
    {
        var caseTemplate1 = Templates["main_negative_case"];
        var caseTemplate2 = Templates["main_negative_case2"];

        var casesText = new ListTextComposer(Environment.NewLine);

        foreach (var grade in Grades)
            if (useNegative(grade))
                casesText.Add(caseTemplate1,
                    "signature", CurrentNamespace,
                    "grade", grade,
                    "num", VSpaceDimensions.KVectorSpaceDimension(grade)
                );
            else
                casesText.Add(caseTemplate2,
                    "grade", grade
                );

        TextComposer.AppendAtNewLine(
            Templates["main_involution"],
            "signature", CurrentNamespace,
            "name", opSpecs.GetName(),
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
            GenerateNegativeFunction(kvSpaceDim);

        GenerateMainInvolutionFunction(
            GaFuLLanguageOperationKind.UnaryNegative.CreateEuclideanOperationSpecs(), 
            _ => true
        );

        GenerateMainInvolutionFunction(
            GaFuLLanguageOperationKind.UnaryReverse.CreateEuclideanOperationSpecs(), 
            BasisBladeUtils.ReverseIsNegativeOfGrade
        );

        GenerateMainInvolutionFunction(
            GaFuLLanguageOperationKind.UnaryGradeInvolution.CreateEuclideanOperationSpecs(), 
            BasisBladeUtils.GradeInvolutionIsNegativeOfGrade
        );

        GenerateMainInvolutionFunction(
            GaFuLLanguageOperationKind.UnaryCliffordConjugate.CreateEuclideanOperationSpecs(), 
            BasisBladeUtils.CliffordConjugateIsNegativeOfGrade
        );

        GenerateKVectorFileFinishCode();

        FileComposer.FinalizeText();
    }
}