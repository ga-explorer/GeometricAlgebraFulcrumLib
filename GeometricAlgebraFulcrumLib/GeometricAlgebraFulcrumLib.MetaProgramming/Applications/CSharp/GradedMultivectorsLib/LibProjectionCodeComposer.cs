using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;
using GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.GradedMultivectorsLib.Combinations;
using GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.GradedMultivectorsLib.Types;
using GeometricAlgebraFulcrumLib.Utilities.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Files;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;
using GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.GradedMultivectorsLib.Storage;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.GradedMultivectorsLib;

public class LibProjectionCodeComposer :
    LibSubCodeComposer
{
    public static LibProjectionCodeComposer Create(
        LibCodeComposerSpecs specs,
        TextFilesComposer codeFilesComposer
    )
    {
        return new LibProjectionCodeComposer(specs, codeFilesComposer);
    }


    private LibProjectionCodeComposer(LibCodeComposerSpecs specs, TextFilesComposer codeFilesComposer)
        : base(specs, codeFilesComposer)
    {
    }


    private string GetTrilinearOperationCode_KvKv_Kv(string funcName, LibTrilinearCombination termTable)
    {
        var in1ClassName = termTable.Input1Type.ClassName;
        var in23ClassName = termTable.Input2Type.ClassName;
        var outClassName = termTable.OutputType.ClassName;

        var scalarCode =
            termTable
                .GetIdTermListPairs()
                .Select(p =>
                    {
                        var lhsIdCode = GradedMultivectorLibUtils.GetBasisBladeCode(
                            p.Key,
                            idCode => $"Scalar{idCode}"
                        );

                        var rhsCode = p.Value.GetRhsCode(
                            idCode => $"mv1.Scalar{idCode}",
                            idCode => $"mv2.Scalar{idCode}",
                            idCode => $"mv2.Scalar{idCode}"
                        );

                        return $"{lhsIdCode} = ({rhsCode}) * mv2NormSquaredInv";
                    }
                ).Concatenate("," + Environment.NewLine);

        return new LinearTextComposer()
            .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
            .AppendLine($"public static {outClassName} {funcName}(this {in1ClassName} mv1, {in23ClassName} mv2)")
            .AppendLine("{")
            .IncreaseIndentation()
            .AppendLine($"if (mv1.IsZero() || mv2.IsZero()) return {outClassName}.Zero;")
            .AppendLine()
            .AppendLine("var mv2NormSquaredInv = 1d / mv2.NormSquared();")
            .AppendLine()
            .AppendLine($"return new {outClassName}")
            .AppendLine("{")
            .IncreaseIndentation()
            .AppendLine(scalarCode)
            .DecreaseIndentation()
            .AppendLine("};")
            .DecreaseIndentation()
            .AppendLine("}")
            .AppendLine()
            .ToString();
    }

    private string GetTrilinearOperationCode_MvKv_Kv(string funcName, LibTrilinearCombination termTable)
    {
        var in1ClassName = termTable.Input1Type.ClassName;
        var in23ClassName = termTable.Input2Type.ClassName;
        var outClassName = termTable.OutputType.ClassName;

        var in1GradeList = termTable.GetInput1BasisBladeGrades();
        var in23Grade = termTable.GetInput2BasisBladeGrades()[0];
        var outGrade = termTable.GetOutputBasisBladeGrades()[0];

        var outKvSpaceDimensions = (int)VSpaceDimensions.ComputeBinomialCoefficient(outGrade);

        var tempStorage = LibTempStorage.CreateAutomatic(
            "tempScalar",
            outKvSpaceDimensions
        );

        var codeComposer = new LinearTextComposer();

        codeComposer
            .AppendLine($"public static {outClassName} {funcName}(this {in1ClassName} mv1, {in23ClassName} mv2)")
            .AppendLine("{")
            .IncreaseIndentation()
            .AppendLine($"if (mv1.IsZero() || mv2.IsZero()) return {outClassName}.Zero;")
            .AppendLine()
            .AppendLine("var mv2NormSquaredInv = 1d / mv2.NormSquared();")
            .AppendLine()
            .AppendLine(tempStorage.GetDeclareCode())
            .AppendLine();

        foreach (var in1Grade in in1GradeList)
        {
            var termList =
                termTable.GetIdTermListPairs(term =>
                    term.Input1BasisBladeGrade == in1Grade &&
                    term.Input2BasisBladeGrade == in23Grade &&
                    term.Input3BasisBladeGrade == in23Grade &&
                    term.OutputBasisBladeGrade == outGrade
                ).ToImmutableArray();

            if (termList.Length == 0)
                continue;

            var productCode =
                termList.Select(term =>
                    {
                        var lhsCode = tempStorage[
                            (int)((ulong)term.Key).BasisBladeIdToIndex()
                        ];

                        var rhsCode = term.Value.GetRhsCode(
                            idCode => $"mv1.KVector{in1Grade}.Scalar{idCode}",
                            idCode => $"mv2.Scalar{idCode}",
                            idCode => $"mv2.Scalar{idCode}"
                        );

                        return $"{lhsCode} += ({rhsCode}) * mv2NormSquaredInv;";
                    }
                ).Concatenate(Environment.NewLine);

            codeComposer
                .AppendLine($"if (!mv1.KVector{in1Grade}.IsZero())")
                .AppendLine("{")
                .IncreaseIndentation()
                .AppendLine(productCode)
                .DecreaseIndentation()
                .AppendLine("}")
                .AppendLine();
        }

        var scalarCode = outKvSpaceDimensions.GetRange(
            index =>
            {
                var rhsCode =
                    tempStorage[index];

                var idCode =
                    GradedMultivectorLibUtils.GetBasisBladeCode(outGrade, index);

                return $"Scalar{idCode} = {rhsCode}";
            }
        ).Concatenate("," + Environment.NewLine);

        codeComposer
            .AppendLine($"return new {outClassName}")
            .AppendLine("{")
            .IncreaseIndentation()
            .AppendLine(scalarCode)
            .DecreaseIndentation()
            .AppendLine("};")
            .DecreaseIndentation()
            .AppendLine("}")
            .AppendLine();

        return codeComposer.ToString();
    }

    private string GetTrilinearOperationCode_KvKv_Mv(string funcName, LibTrilinearCombination termTable)
    {
        var in1ClassName = termTable.Input1Type.ClassName;
        var in23ClassName = termTable.Input2Type.ClassName;
        var outClassName = termTable.OutputType.ClassName;

        var in1Grade = termTable.GetInput1BasisBladeGrades()[0];
        var in23Grade = termTable.GetInput2BasisBladeGrades()[0];

        var tempStorage = LibTempStorage.CreateArray(
            "tempScalar",
            GaSpaceDimensions
        );

        var codeComposer = new LinearTextComposer();

        codeComposer
            .AppendLine($"public static {outClassName} {funcName}(this {in1ClassName} mv1, {in23ClassName} mv2)")
            .AppendLine("{")
            .IncreaseIndentation()
            .AppendLine($"if (mv1.IsZero() || mv2.IsZero()) return {outClassName}.Zero;")
            .AppendLine()
            .AppendLine("var mv2NormSquaredInv = 1d / mv2.NormSquared();")
            .AppendLine()
            .AppendLine(tempStorage.GetDeclareCode())
            .AppendLine();

        var termList =
            termTable.GetIdTermListPairs(term =>
                term.Input1BasisBladeGrade == in1Grade &&
                term.Input2BasisBladeGrade == in23Grade &&
                term.Input3BasisBladeGrade == in23Grade
            ).ToImmutableArray();

        if (termList.Length == 0)
            throw new InvalidOperationException();

        var productCode =
            termList.Select(term =>
                {
                    var lhsCode =
                        tempStorage[term.Key];

                    var rhsCode = term.Value.GetRhsCode(
                        idCode => $"mv1.Scalar{idCode}",
                        idCode => $"mv2.Scalar{idCode}",
                        idCode => $"mv2.Scalar{idCode}"
                    );

                    return $"{lhsCode} += ({rhsCode}) * mv2NormSquaredInv;";
                }
            ).Concatenate(Environment.NewLine);

        codeComposer
            .AppendLine("if (!mv2.IsZero())")
            .AppendLine("{")
            .IncreaseIndentation()
            .AppendLine(productCode)
            .DecreaseIndentation()
            .AppendLine("}")
            .AppendLine();

        codeComposer
            .AppendLine($"return {outClassName}.Create({tempStorage.ArrayName});")
            .DecreaseIndentation()
            .AppendLine("}")
            .AppendLine();

        return codeComposer.ToString();
    }

    private string GetTrilinearOperationCode_MvKv_Mv(string funcName, LibTrilinearCombination termTable)
    {
        var in1ClassName = termTable.Input1Type.ClassName;
        var in23ClassName = termTable.Input2Type.ClassName;
        var outClassName = termTable.OutputType.ClassName;

        var in1GradeList = termTable.GetInput1BasisBladeGrades();
        var in23Grade = termTable.GetInput2BasisBladeGrades()[0];

        var tempStorage = LibTempStorage.CreateArray(
            "tempScalar",
            GaSpaceDimensions
        );

        var codeComposer = new LinearTextComposer();

        codeComposer
            .AppendLine($"public static {outClassName} {funcName}(this {in1ClassName} mv1, {in23ClassName} mv2)")
            .AppendLine("{")
            .IncreaseIndentation()
            .AppendLine($"if (mv1.IsZero() || mv2.IsZero()) return {outClassName}.Zero;")
            .AppendLine()
            .AppendLine("var mv2NormSquaredInv = 1d / mv2.NormSquared();")
            .AppendLine()
            .AppendLine(tempStorage.GetDeclareCode())
            .AppendLine();

        foreach (var in1Grade in in1GradeList)
        {
            var termList =
                termTable.GetIdTermListPairs(term =>
                    term.Input1BasisBladeGrade == in1Grade &&
                    term.Input2BasisBladeGrade == in23Grade &&
                    term.Input3BasisBladeGrade == in23Grade
                ).ToImmutableArray();

            if (termList.Length == 0)
                continue;

            var productCode =
                termList.Select(term =>
                    {
                        var lhsCode =
                            tempStorage[term.Key];

                        var rhsCode = term.Value.GetRhsCode(
                            idCode => $"mv1.KVector{in1Grade}.Scalar{idCode}",
                            idCode => $"mv2.Scalar{idCode}",
                            idCode => $"mv2.Scalar{idCode}"
                        );

                        return $"{lhsCode} += ({rhsCode}) * mv2NormSquaredInv;";
                    }
                ).Concatenate(Environment.NewLine);

            codeComposer
                .AppendLine($"if (!mv1.KVector{in1Grade}.IsZero())")
                .AppendLine("{")
                .IncreaseIndentation()
                .AppendLine(productCode)
                .DecreaseIndentation()
                .AppendLine("}")
                .AppendLine();
        }

        codeComposer
            .AppendLine($"return {outClassName}.Create({tempStorage.ArrayName});")
            .DecreaseIndentation()
            .AppendLine("}")
            .AppendLine();

        return codeComposer.ToString();
    }

    private string GetTrilinearOperationCode(string funcName, LibTrilinearCombination termTable)
    {
        if (termTable.IsEmpty)
        {
            return new LinearTextComposer()
                .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
                .AppendLine($"public static {termTable.OutputType.ClassName} {funcName}(this {termTable.Input1Type.ClassName} mv1, {termTable.Input2Type.ClassName} mv2)")
                .AppendLine("{")
                .IncreaseIndentation()
                .AppendLine($"return {termTable.OutputType.ClassName}.Zero;")
                .DecreaseIndentation()
                .AppendLine("}")
                .AppendLine()
                .ToString();
        }

        if (termTable.OutputType.IsKVector)
        {
            if (termTable.Input1Type.IsKVector)
                return termTable.Input2Type.IsKVector
                    ? GetTrilinearOperationCode_KvKv_Kv(funcName, termTable)
                    : throw new InvalidOperationException(); //GetTrilinearOperationCode_KvMv_Kv(funcName, termTable);

            return termTable.Input2Type.IsKVector
                ? GetTrilinearOperationCode_MvKv_Kv(funcName, termTable)
                : throw new InvalidOperationException(); //GetTrilinearOperationCode_MvMv_Kv(funcName, termTable);
        }

        if (termTable.Input1Type.IsKVector)
            return termTable.Input2Type.IsKVector
                ? GetTrilinearOperationCode_KvKv_Mv(funcName, termTable)
                : throw new InvalidOperationException(); //GetTrilinearOperationCode_KvMv_Mv(funcName, termTable);

        return termTable.Input2Type.IsKVector
            ? GetTrilinearOperationCode_MvKv_Mv(funcName, termTable)
            : throw new InvalidOperationException(); //GetTrilinearOperationCode_MvMv_Mv(funcName, termTable);
    }

    private string GetProjectionCode()
    {
        var codeComposer = new LinearTextComposer();

        foreach (var inType1 in Types)
        {
            var inType23List =
                inType1 is LibTypeKVector kVectorType
                    ? KVectorTypes.Where(
                        t =>
                            t.Grade > 0 &&
                            t.Grade >= kVectorType.Grade &&
                            t.Grade < VSpaceDimensions
                    )
                    : KVectorTypes.Where(
                        t =>
                            t.Grade > 0 &&
                            t.Grade < VSpaceDimensions
                    );

            foreach (var inType23 in inType23List)
            {
                var termTable =
                    LibTrilinearCombination.CreateFromEqualSecondThirdInputs(
                        inType1,
                        inType23,
                        (b1, b2, b3) => b1.Lcp(b2).Lcp(b3.Reverse())
                    ).SetOutputType(inType1);

                codeComposer.AppendLineAtNewLine(
                    GetTrilinearOperationCode(
                        "ProjectOn",
                        termTable
                    )
                );
            }
        }

        return codeComposer.ToString();
    }


    public override TextFilesComposer GenerateCode()
    {
        CodeFilesComposer.InitializeFile(GaSpaceName + "Projection.cs");

        var codeComposer = CodeFilesComposer.ActiveFileTextComposer;

        codeComposer
            .AppendLine("using System;")
            .AppendLine("using System.Runtime.CompilerServices;")
            .AppendLine()
            .AppendLine($"namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.{GaSpaceName};")
            .AppendLine()
            .AppendLine($"public static class {GaSpaceName}Projection")
            .AppendLine("{")
            .IncreaseIndentation()
            .AppendLine(GetProjectionCode())
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