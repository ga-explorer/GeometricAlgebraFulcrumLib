using GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.GradedMultivectorsLib.Combinations;
using GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.GradedMultivectorsLib.Types;
using System;
using System.Collections.Immutable;
using System.Linq;
using TextComposerLib;
using TextComposerLib.Files;
using TextComposerLib.Text;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Parametric;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.GradedMultivectorsLib;

public class LibNormCodeComposer :
    LibSubCodeComposer
{
    public static LibNormCodeComposer Create(
        LibCodeComposerSpecs specs,
        TextFilesComposer codeFilesComposer
    )
    {
        return new LibNormCodeComposer(specs, codeFilesComposer);
    }


    private LibNormCodeComposer(LibCodeComposerSpecs specs, TextFilesComposer codeFilesComposer)
        : base(specs, codeFilesComposer)
    {
    }


    private string GetNormSquaredCode_Kv(LibBilinearCombination termTable)
    {
        var inClassName = termTable.Input1Type.ClassName;

        var scalarCode =
            termTable
                .GetIdTermListPairs()
                .Select(p =>
                    {
                        var rhsCode = p.Value.GetRhsCode(
                            idCode => $"mv.Scalar{idCode}",
                            idCode => $"mv.Scalar{idCode}"
                        );

                        return $"return {rhsCode};";
                    }
                ).Concatenate("," + Environment.NewLine);

        return new LinearTextComposer()
            .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
            .AppendLine($"public static double NormSquared(this {inClassName} mv)")
            .AppendLine("{")
            .IncreaseIndentation()
            .AppendLine("if (mv.IsZero()) return 0d;")
            .AppendLine()
            .AppendLine(scalarCode)
            .DecreaseIndentation()
            .AppendLine("}")
            .AppendLine()
            .ToString();
    }

    private string GetNormSquaredCode_Mv(LibBilinearCombination termTable)
    {
        var inClassName = termTable.Input1Type.ClassName;

        var inGradeList = termTable.GetInputBasisBladeGrades();

        var codeComposer = new LinearTextComposer();

        codeComposer
            .AppendLine($"public static double NormSquared(this {inClassName} mv)")
            .AppendLine("{")
            .IncreaseIndentation()
            .AppendLine("if (mv.IsZero()) return 0d;")
            .AppendLine()
            .AppendLine("var normSquared = 0d;")
            .AppendLine();

        foreach (var inGrade in inGradeList)
        {
            var termList =
                termTable.GetIdTermListPairs(term =>
                    term.Input1BasisBladeGrade == inGrade &&
                    term.Input2BasisBladeGrade == inGrade
                ).ToImmutableArray();

            if (termList.Length == 0)
                continue;

            var productCode =
                termList.Select(term =>
                    {
                        var rhsCode = term.Value.GetRhsCode(
                            idCode => $"mv.KVector{inGrade}.Scalar{idCode}",
                            idCode => $"mv.KVector{inGrade}.Scalar{idCode}"
                        );

                        return $"normSquared += {rhsCode};";
                    }
                ).Concatenate(Environment.NewLine);

            codeComposer
                .AppendLine($"if (!mv.KVector{inGrade}.IsZero())")
                .AppendLine("{")
                .IncreaseIndentation()
                .AppendLine(productCode)
                .DecreaseIndentation()
                .AppendLine("}")
                .AppendLine();
        }

        codeComposer
            .AppendLine("return normSquared;")
            .DecreaseIndentation()
            .AppendLine("}")
            .AppendLine();

        return codeComposer.ToString();
    }

    private string GetNormSquaredCode(LibType inType)
    {
        var termTable = LibBilinearCombination.CreateFromEqualInputs(
            inType,
            (b1, b2) =>
                b1.Sp(b2.Reverse())
        );

        if (termTable.IsEmpty)
        {
            return new LinearTextComposer()
                .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
                .AppendLine($"public static double NormSquared(this {termTable.Input1Type.ClassName} mv)")
                .AppendLine("{")
                .IncreaseIndentation()
                .AppendLine("return 0d;")
                .DecreaseIndentation()
                .AppendLine("}")
                .AppendLine()
                .ToString();
        }

        return inType.IsKVector
            ? GetNormSquaredCode_Kv(termTable)
            : GetNormSquaredCode_Mv(termTable);
    }


    private string GetSpSquaredCode_Kv(LibBilinearCombination termTable)
    {
        var inClassName = termTable.Input1Type.ClassName;

        var scalarCode =
            termTable
                .GetIdTermListPairs()
                .Select(p =>
                    {
                        var rhsCode = p.Value.GetRhsCode(
                            idCode => $"mv.Scalar{idCode}",
                            idCode => $"mv.Scalar{idCode}"
                        );

                        return $"return {rhsCode};";
                    }
                ).Concatenate("," + Environment.NewLine);

        return new LinearTextComposer()
            .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
            .AppendLine($"public static double SpSquared(this {inClassName} mv)")
            .AppendLine("{")
            .IncreaseIndentation()
            .AppendLine("if (mv.IsZero()) return 0d;")
            .AppendLine()
            .AppendLine(scalarCode)
            .DecreaseIndentation()
            .AppendLine("}")
            .AppendLine()
            .ToString();
    }

    private string GetSpSquaredCode_Mv(LibBilinearCombination termTable)
    {
        var inClassName = termTable.Input1Type.ClassName;

        var inGradeList = termTable.GetInputBasisBladeGrades();

        var codeComposer = new LinearTextComposer();

        codeComposer
            .AppendLine($"public static double SpSquared(this {inClassName} mv)")
            .AppendLine("{")
            .IncreaseIndentation()
            .AppendLine("if (mv.IsZero()) return 0d;")
            .AppendLine()
            .AppendLine("var normSquared = 0d;")
            .AppendLine();

        foreach (var inGrade in inGradeList)
        {
            var termList =
                termTable.GetIdTermListPairs(term =>
                    term.Input1BasisBladeGrade == inGrade &&
                    term.Input2BasisBladeGrade == inGrade
                ).ToImmutableArray();

            if (termList.Length == 0)
                continue;

            var productCode =
                termList.Select(term =>
                    {
                        var rhsCode = term.Value.GetRhsCode(
                            idCode => $"mv.KVector{inGrade}.Scalar{idCode}",
                            idCode => $"mv.KVector{inGrade}.Scalar{idCode}"
                        );

                        return $"normSquared += {rhsCode};";
                    }
                ).Concatenate(Environment.NewLine);

            codeComposer
                .AppendLine($"if (!mv.KVector{inGrade}.IsZero())")
                .AppendLine("{")
                .IncreaseIndentation()
                .AppendLine(productCode)
                .DecreaseIndentation()
                .AppendLine("}")
                .AppendLine();
        }

        codeComposer
            .AppendLine("return normSquared;")
            .DecreaseIndentation()
            .AppendLine("}")
            .AppendLine();

        return codeComposer.ToString();
    }

    private string GetSpSquaredCode(LibType inType)
    {
        var termTable = LibBilinearCombination.CreateFromEqualInputs(
            inType,
            (b1, b2) =>
                b1.Sp(b2)
        );

        if (termTable.IsEmpty)
        {
            return new LinearTextComposer()
                .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
                .AppendLine($"public static double SpSquared(this {termTable.Input1Type.ClassName} mv)")
                .AppendLine("{")
                .IncreaseIndentation()
                .AppendLine("return 0d;")
                .DecreaseIndentation()
                .AppendLine("}")
                .AppendLine()
                .ToString();
        }

        return inType.IsKVector
            ? GetSpSquaredCode_Kv(termTable)
            : GetSpSquaredCode_Mv(termTable);
    }


    private string GetNormCode()
    {
        var codeComposer = new LinearTextComposer();

        var codeComposer1 = new ParametricTextComposer(
            "#",
            "#",
            @"
[MethodImpl(MethodImplOptions.AggressiveInlining)]
public static double Norm(this #class-name# mv)
{
    return Math.Sqrt(Math.Abs(mv.NormSquared()));
}
".Trim()
        );

        foreach (var inType in Types)
        {
            codeComposer.AppendLineAtNewLine(
                GetSpSquaredCode(inType)
            );
        }

        foreach (var inType in Types)
        {
            codeComposer.AppendLineAtNewLine(
                GetNormSquaredCode(inType)
            );
        }

        foreach (var inType in Types)
        {
            codeComposer.AppendLineAtNewLine(
                codeComposer1.GenerateText("class-name", inType.ClassName)
            ).AppendLine();
        }

        return codeComposer.ToString();
    }


    public override TextFilesComposer GenerateCode()
    {
        CodeFilesComposer.InitializeFile(GaSpaceName + "Norm.cs");

        var codeComposer = CodeFilesComposer.ActiveFileTextComposer;

        codeComposer
            .AppendLine("using System;")
            .AppendLine("using System.Runtime.CompilerServices;")
            .AppendLine()
            .AppendLine($"namespace GeometricAlgebraFulcrumLib.Generations.Algebra.{GaSpaceName};")
            .AppendLine()
            .AppendLine($"public static class {GaSpaceName}Norm")
            .AppendLine("{")
            .IncreaseIndentation()
            .AppendLine(GetNormCode())
            .AppendLine()
            .DecreaseIndentation()
            .AppendLineAtNewLine("}")
            .AppendLine();

        CodeFilesComposer.ActiveFileComposer.FinalizeText(
            code => code.RemoveRepeatedEmptyLines()
        );

        return CodeFilesComposer;
    }
}