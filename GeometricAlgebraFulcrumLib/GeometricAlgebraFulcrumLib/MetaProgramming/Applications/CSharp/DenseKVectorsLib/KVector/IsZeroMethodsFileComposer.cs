using System;
using System.Linq;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Structured;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.DenseKVectorsLib.KVector;

internal sealed class IsZeroMethodsFileComposer : 
    GaFuLLibraryFileComposerBase
{
    internal IsZeroMethodsFileComposer(GaFuLLibraryComposer libGen)
        : base(libGen)
    {
    }


    private void GenerateIsZeroFunction(ulong kvSpaceDim)
    {
        var t1 = Templates["iszero"];
        var t2 = Templates["iszero_case"];
        var t3 = Templates["trimscalars_case"];

        var iszeroCasesText = new ListTextComposer(" ||" + Environment.NewLine);
        var trimCoefsCasesText = new ListTextComposer("," + Environment.NewLine);

        for (var i = 0UL; i < kvSpaceDim; i++)
        {
            iszeroCasesText.Add(t2, "num", i);
            trimCoefsCasesText.Add(t3, "num", i);
        }

        TextComposer.AppendAtNewLine(
            t1,
            "num", kvSpaceDim,
            "double", GeoLanguage.ScalarTypeName,
            "iszero_case", iszeroCasesText,
            "trimscalars_case", trimCoefsCasesText
        );
    }

    private void GenerateMainIsZeroFunction()
    {
        var t1 = Templates["main_iszero"];
        var t2 = Templates["main_iszero_case"];
        var t3 = Templates["main_trimscalars_case"];

        var iszeroCasesText = new ListTextComposer(Environment.NewLine);
        var trimcoefsCasesText = new ListTextComposer(Environment.NewLine);

        foreach (var grade in Grades)
        {
            iszeroCasesText.Add(t2,
                "grade", grade,
                "num", VSpaceDimensions.KVectorSpaceDimension(grade)
            );

            trimcoefsCasesText.Add(t3,
                "signature", CurrentNamespace,
                "grade", grade,
                "num", VSpaceDimensions.KVectorSpaceDimension(grade)
            );
        }

        TextComposer.AppendAtNewLine(t1,
            "signature", CurrentNamespace,
            "main_iszero_case", iszeroCasesText,
            "main_trimscalars_case", trimcoefsCasesText
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
            GenerateIsZeroFunction(kvSpaceDim);

        GenerateMainIsZeroFunction();

        GenerateKVectorFileFinishCode();

        FileComposer.FinalizeText();
    }
}