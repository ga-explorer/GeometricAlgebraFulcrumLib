using GeometricAlgebraFulcrumLib.MetaProgramming.Utilities.Code;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Structured;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.CSharp.DenseKVectorsLib.KVector;

internal sealed class BilinearProductMainMethodFileComposer : 
    GaFuLLibraryFileComposerBase
{
    internal GaFuLLanguageOperationSpecs OperationSpecs { get; }

    internal string ZeroCondition { get; }

    internal Func<int, int, int> GetFinalGrade { get; }

    internal Func<int, int, bool> IsLegalGrade { get; }


    internal BilinearProductMainMethodFileComposer(GaFuLLibraryComposer libGen, GaFuLLanguageOperationSpecs opSpecs, string zeroCondition, Func<int, int, int> getFinalGrade, Func<int, int, bool> isLegalGrade)
        : base(libGen)
    {
        OperationSpecs = opSpecs;

        ZeroCondition = zeroCondition;

        GetFinalGrade = getFinalGrade;

        IsLegalGrade = isLegalGrade;
    }


    public string GetCasesText()
    {
        var t2 = Templates["bilinearproduct_main_case"];

        var casesText = new ListTextComposer(Environment.NewLine);

        foreach (var grade1 in Grades)
        foreach (var grade2 in Grades)
        {
            if (IsLegalGrade(grade1, grade2) == false)
                continue;

            var grade = 
                GetFinalGrade(grade1, grade2);

            var id = 
                grade1 + grade2 * GradesCount;

            var name = OperationSpecs.GetName(
                grade1, grade2, grade
            );

            casesText.Add(t2,
                "name", name,
                "id", id,
                "g1", grade1,
                "g2", grade2,
                "grade", grade,
                "signature", CurrentNamespace
            );
        }

        return casesText.ToString();
    }

    public override void Generate()
    {
        GenerateKVectorFileStartCode();

        var casesText = GetCasesText();

        TextComposer.AppendAtNewLine(
            Templates["bilinearproduct_main"],
            "name", OperationSpecs,
            "signature", CurrentNamespace,
            "zerocond", ZeroCondition,
            "cases", casesText
        );

        GenerateKVectorFileFinishCode();

        FileComposer.FinalizeText();
    }
}