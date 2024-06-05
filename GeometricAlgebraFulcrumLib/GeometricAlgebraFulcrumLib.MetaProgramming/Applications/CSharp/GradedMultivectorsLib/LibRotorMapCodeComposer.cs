using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.GradedMultivectorsLib.Combinations;
using GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.GradedMultivectorsLib.Storage;
using GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.GradedMultivectorsLib.Types;
using GeometricAlgebraFulcrumLib.Utilities.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Files;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.GradedMultivectorsLib;

public class LibRotorMapCodeComposer :
    LibSubCodeComposer
{
    public static LibRotorMapCodeComposer Create(LibCodeComposerSpecs specs, TextFilesComposer codeFilesComposer)
    {
        return new LibRotorMapCodeComposer(specs, codeFilesComposer);
    }
    
    
    private static IRGaSignedBasisBlade Rotate(RGaBasisBlade b1, RGaBasisBlade b2, RGaBasisBlade b3)
    {
        var outputGrade = b2.Grade;
        var metric = b2.Metric;

        if (b1.Grade.IsOdd() || b3.Grade.IsOdd())
            return metric.CreateZeroBasisBlade(outputGrade);

        var b4 = 
            b1.Gp(b2).Gp(b3.Reverse());

        if (b4.Grade != outputGrade)
            return metric.CreateZeroBasisBlade(outputGrade);
        
        return b4;
    }


    private LibRotorMapCodeComposer(LibCodeComposerSpecs specs, TextFilesComposer codeFilesComposer)
        : base(specs, codeFilesComposer)
    {
    }


    private string GetTrilinearOperationCode_KvKv_Kv(string funcName, LibTrilinearCombination termTable)
    {
        var in2ClassName = termTable.Input2Type.ClassName;
        var in13ClassName = termTable.Input1Type.ClassName;
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

                        var rhsCode = p.Value.GetRhsCode(idCode => $"mv2.Scalar{idCode}",
                            idCode => $"mv1.Scalar{idCode}",
                            idCode => $"mv2.Scalar{idCode}"
                        );

                        return $"{lhsIdCode} = {rhsCode}";
                    }
                ).Concatenate("," + Environment.NewLine);

        return new LinearTextComposer()
            .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
            .AppendLine($"public static {outClassName} {funcName}(this {in2ClassName} mv1, {in13ClassName} mv2)")
            .AppendLine("{")
            .IncreaseIndentation()
            .AppendLine($"if (mv1.IsZero() || mv2.IsZero()) return {outClassName}.Zero;")
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
    
    private string GetTrilinearOperationCode_KvMv_Kv(string funcName, LibTrilinearCombination termTable)
    {
        var in2ClassName = termTable.Input2Type.ClassName;
        var in13ClassName = termTable.Input1Type.ClassName;
        var outClassName = termTable.OutputType.ClassName;

        var in2Grade = termTable.GetInput2BasisBladeGrades()[0];
        var in13GradeList = termTable.GetInput13BasisBladeGrades();
        var outGrade = termTable.GetOutputBasisBladeGrades()[0];

        var outKvSpaceDimensions = (int)VSpaceDimensions.ComputeBinomialCoefficient(outGrade);

        var codeComposer = new LinearTextComposer();

        var tempStorage = LibTempStorage.CreateAutomatic(
            "tempScalar",
            outKvSpaceDimensions
        );

        codeComposer
            .AppendLine($"public static {outClassName} {funcName}(this {in2ClassName} mv1, {in13ClassName} mv2)")
            .AppendLine("{")
            .IncreaseIndentation()
            .AppendLine($"if (mv1.IsZero() || mv2.IsZero()) return {outClassName}.Zero;")
            .AppendLine()
            .AppendLine(tempStorage.GetDeclareCode())
            .AppendLine();

        foreach (var in1Grade in in13GradeList)
        foreach (var in3Grade in in13GradeList)
        {
            var termList =
                termTable.GetIdTermListPairs(term =>
                    term.Input1BasisBladeGrade == in1Grade &&
                    term.Input2BasisBladeGrade == in2Grade &&
                    term.Input3BasisBladeGrade == in3Grade &&
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
                            idCode => $"mv2.KVector{in1Grade}.Scalar{idCode}",
                            idCode => $"mv1.Scalar{idCode}",
                            idCode => $"mv2.KVector{in3Grade}.Scalar{idCode}"
                        );

                        return $"{lhsCode} += {rhsCode};";
                    }
                ).Concatenate(Environment.NewLine);

            var zeroCheckCode = in1Grade == in3Grade
                ? $"if (!mv2.KVector{in1Grade}.IsZero())"
                : $"if (!mv2.KVector{in1Grade}.IsZero() && !mv2.KVector{in3Grade}.IsZero())";

            codeComposer
                .AppendLine(zeroCheckCode)
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

    private string GetTrilinearOperationCode_MvKv_Kv(string funcName, LibTrilinearCombination termTable)
    {
        var in2ClassName = termTable.Input2Type.ClassName;
        var in13ClassName = termTable.Input1Type.ClassName;
        var outClassName = termTable.OutputType.ClassName;

        var in2GradeList = termTable.GetInput2BasisBladeGrades();
        var in13Grade = termTable.GetInput13BasisBladeGrades()[0];
        var outGrade = termTable.GetOutputBasisBladeGrades()[0];

        var outKvSpaceDimensions = (int)VSpaceDimensions.ComputeBinomialCoefficient(outGrade);

        var tempStorage = LibTempStorage.CreateAutomatic(
            "tempScalar",
            outKvSpaceDimensions
        );

        var codeComposer = new LinearTextComposer();

        codeComposer
            .AppendLine($"public static {outClassName} {funcName}(this {in2ClassName} mv1, {in13ClassName} mv2)")
            .AppendLine("{")
            .IncreaseIndentation()
            .AppendLine($"if (mv1.IsZero() || mv2.IsZero()) return {outClassName}.Zero;")
            .AppendLine()
            .AppendLine(tempStorage.GetDeclareCode())
            .AppendLine();

        foreach (var in2Grade in in2GradeList)
        {
            var termList =
                termTable.GetIdTermListPairs(term =>
                    term.Input1BasisBladeGrade == in13Grade &&
                    term.Input2BasisBladeGrade == in2Grade &&
                    term.Input3BasisBladeGrade == in13Grade &&
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
                            idCode => $"mv2.Scalar{idCode}",
                            idCode => $"mv1.KVector{in2Grade}.Scalar{idCode}",
                            idCode => $"mv2.Scalar{idCode}"
                        );

                        return $"{lhsCode} += {rhsCode};";
                    }
                ).Concatenate(Environment.NewLine);

            codeComposer
                .AppendLine($"if (!mv1.KVector{in2Grade}.IsZero())")
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
    
    private string GetTrilinearOperationCode_MvMv_Kv(string funcName, LibTrilinearCombination termTable)
    {
        var in2ClassName = termTable.Input2Type.ClassName;
        var in13ClassName = termTable.Input1Type.ClassName;
        var outClassName = termTable.OutputType.ClassName;

        var in2GradeList = termTable.GetInput2BasisBladeGrades();
        var in13GradeList = termTable.GetInput13BasisBladeGrades();
        var outGrade = termTable.GetOutputBasisBladeGrades()[0];

        var outKvSpaceDimensions = (int)VSpaceDimensions.ComputeBinomialCoefficient(outGrade);

        var tempStorage = LibTempStorage.CreateAutomatic(
            "tempScalar",
            outKvSpaceDimensions
        );

        var codeComposer = new LinearTextComposer();

        codeComposer
            .AppendLine($"public static {outClassName} {funcName}(this {in2ClassName} mv1, {in13ClassName} mv2)")
            .AppendLine("{")
            .IncreaseIndentation()
            .AppendLine($"if (mv1.IsZero() || mv2.IsZero()) return {outClassName}.Zero;")
            .AppendLine()
            .AppendLine(tempStorage.GetDeclareCode())
            .AppendLine();

        foreach (var in2Grade in in2GradeList)
        {
            var codeComposer2 = new LinearTextComposer();

            foreach (var in1Grade in in13GradeList)
            foreach (var in3Grade in in13GradeList)
            {
                var termList =
                    termTable.GetIdTermListPairs(term =>
                        term.Input1BasisBladeGrade == in1Grade &&
                        term.Input2BasisBladeGrade == in2Grade &&
                        term.Input3BasisBladeGrade == in3Grade &&
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
                                idCode => $"mv2.KVector{in1Grade}.Scalar{idCode}",
                                idCode => $"mv1.KVector{in2Grade}.Scalar{idCode}",
                                idCode => $"mv2.KVector{in3Grade}.Scalar{idCode}"
                            );

                            return $"{lhsCode} += {rhsCode};";
                        }
                    ).Concatenate(Environment.NewLine);

                var zeroCheckCode = in1Grade == in3Grade
                    ? $"if (!mv2.KVector{in1Grade}.IsZero())"
                    : $"if (!mv2.KVector{in1Grade}.IsZero() && !mv2.KVector{in3Grade}.IsZero())";

                codeComposer2
                    .AppendLine(zeroCheckCode)
                    .AppendLine("{")
                    .IncreaseIndentation()
                    .AppendLine(productCode)
                    .DecreaseIndentation()
                    .AppendLine("}")
                    .AppendLine();
            }

            codeComposer
                .AppendLine($"if (!mv1.KVector{in2Grade}.IsZero())")
                .AppendLine("{")
                .IncreaseIndentation()
                .AppendLine(codeComposer2.ToString())
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
        var in2ClassName = termTable.Input2Type.ClassName;
        var in13ClassName = termTable.Input1Type.ClassName;
        var outClassName = termTable.OutputType.ClassName;

        var in2Grade = termTable.GetInput2BasisBladeGrades()[0];
        var in13Grade = termTable.GetInput13BasisBladeGrades()[0];

        var tempStorage = LibTempStorage.CreateArray(
            "tempScalar",
            GaSpaceDimensions
        );

        var codeComposer = new LinearTextComposer();

        codeComposer
            .AppendLine($"public static {outClassName} {funcName}(this {in2ClassName} mv1, {in13ClassName} mv2)")
            .AppendLine("{")
            .IncreaseIndentation()
            .AppendLine($"if (mv1.IsZero() || mv2.IsZero()) return {outClassName}.Zero;")
            .AppendLine()
            .AppendLine(tempStorage.GetDeclareCode())
            .AppendLine();

        var termList =
            termTable.GetIdTermListPairs(term =>
                term.Input1BasisBladeGrade == in13Grade &&
                term.Input2BasisBladeGrade == in2Grade &&
                term.Input3BasisBladeGrade == in13Grade
            ).ToImmutableArray();

        if (termList.Length == 0)
            throw new InvalidOperationException();

        var productCode =
            termList.Select(term =>
                {
                    var lhsCode =
                        tempStorage[term.Key];

                    var rhsCode = term.Value.GetRhsCode(
                        idCode => $"mv2.Scalar{idCode}",
                        idCode => $"mv1.Scalar{idCode}",
                        idCode => $"mv2.Scalar{idCode}"
                    );

                    return $"{lhsCode} += {rhsCode};";
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
    
    private string GetTrilinearOperationCode_KvMv_Mv(string funcName, LibTrilinearCombination termTable)
    {
        var in2ClassName = termTable.Input2Type.ClassName;
        var in13ClassName = termTable.Input1Type.ClassName;
        var outClassName = termTable.OutputType.ClassName;

        var in2Grade = termTable.GetInput2BasisBladeGrades()[0];
        var in13GradeList = termTable.GetInput13BasisBladeGrades();

        var tempStorage = LibTempStorage.CreateArray(
            "tempScalar",
            GaSpaceDimensions
        );

        var codeComposer = new LinearTextComposer();

        codeComposer
            .AppendLine($"public static {outClassName} {funcName}(this {in2ClassName} mv1, {in13ClassName} mv2)")
            .AppendLine("{")
            .IncreaseIndentation()
            .AppendLine($"if (mv1.IsZero() || mv2.IsZero()) return {outClassName}.Zero;")
            .AppendLine()
            .AppendLine(tempStorage.GetDeclareCode())
            .AppendLine();

        foreach (var in1Grade in in13GradeList)
        foreach (var in3Grade in in13GradeList)
        {
            var termList =
                termTable.GetIdTermListPairs(term =>
                    term.Input1BasisBladeGrade == in1Grade &&
                    term.Input2BasisBladeGrade == in2Grade &&
                    term.Input3BasisBladeGrade == in3Grade
                ).ToImmutableArray();

            if (termList.Length == 0)
                continue;

            var productCode =
                termList.Select(term =>
                    {
                        var lhsCode =
                            tempStorage[term.Key];

                        var rhsCode = term.Value.GetRhsCode(
                            idCode => $"mv2.KVector{in1Grade}.Scalar{idCode}",
                            idCode => $"mv1.Scalar{idCode}",
                            idCode => $"mv2.KVector{in3Grade}.Scalar{idCode}"
                        );

                        return $"{lhsCode} += {rhsCode};";
                    }
                ).Concatenate(Environment.NewLine);

            var zeroCheckCode = in1Grade == in3Grade
                ? $"if (!mv2.KVector{in1Grade}.IsZero())"
                : $"if (!mv2.KVector{in1Grade}.IsZero() && !mv2.KVector{in3Grade}.IsZero())";

            codeComposer
                .AppendLine(zeroCheckCode)
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

    private string GetTrilinearOperationCode_MvKv_Mv(string funcName, LibTrilinearCombination termTable)
    {
        var in2ClassName = termTable.Input2Type.ClassName;
        var in13ClassName = termTable.Input1Type.ClassName;
        var outClassName = termTable.OutputType.ClassName;

        var in2GradeList = termTable.GetInput2BasisBladeGrades();
        var in13Grade = termTable.GetInput13BasisBladeGrades()[0];

        var tempStorage = LibTempStorage.CreateArray(
            "tempScalar",
            GaSpaceDimensions
        );

        var codeComposer = new LinearTextComposer();

        codeComposer
            .AppendLine($"public static {outClassName} {funcName}(this {in2ClassName} mv1, {in13ClassName} mv2)")
            .AppendLine("{")
            .IncreaseIndentation()
            .AppendLine($"if (mv1.IsZero() || mv2.IsZero()) return {outClassName}.Zero;")
            .AppendLine()
            .AppendLine(tempStorage.GetDeclareCode())
            .AppendLine();

        foreach (var in2Grade in in2GradeList)
        {
            var termList =
                termTable.GetIdTermListPairs(term =>
                    term.Input1BasisBladeGrade == in13Grade &&
                    term.Input2BasisBladeGrade == in2Grade &&
                    term.Input3BasisBladeGrade == in13Grade
                ).ToImmutableArray();

            if (termList.Length == 0)
                continue;

            var productCode =
                termList.Select(term =>
                    {
                        var lhsCode =
                            tempStorage[term.Key];

                        var rhsCode = term.Value.GetRhsCode(
                            idCode => $"mv2.Scalar{idCode}",
                            idCode => $"mv1.KVector{in2Grade}.Scalar{idCode}",
                            idCode => $"mv2.Scalar{idCode}"
                        );

                        return $"{lhsCode} += {rhsCode};";
                    }
                ).Concatenate(Environment.NewLine);

            codeComposer
                .AppendLine($"if (!mv1.KVector{in2Grade}.IsZero())")
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
    
    private string GetTrilinearOperationCode_MvMv_Mv(string funcName, LibTrilinearCombination termTable)
    {
        var in2ClassName = termTable.Input2Type.ClassName;
        var in13ClassName = termTable.Input1Type.ClassName;
        var outClassName = termTable.OutputType.ClassName;

        var in2GradeList = termTable.GetInput2BasisBladeGrades();
        var in13GradeList = termTable.GetInput13BasisBladeGrades();

        var tempStorage = LibTempStorage.CreateArray(
            "tempScalar",
            GaSpaceDimensions
        );

        var codeComposer = new LinearTextComposer();

        codeComposer
            .AppendLine($"public static {outClassName} {funcName}(this {in2ClassName} mv1, {in13ClassName} mv2)")
            .AppendLine("{")
            .IncreaseIndentation()
            .AppendLine($"if (mv1.IsZero() || mv2.IsZero()) return {outClassName}.Zero;")
            .AppendLine()
            .AppendLine(tempStorage.GetDeclareCode())
            .AppendLine();

        foreach (var in2Grade in in2GradeList)
        {
            var codeComposer2 = new LinearTextComposer();

            foreach (var in1Grade in in13GradeList)
            foreach (var in3Grade in in13GradeList)
            {
                var termList =
                    termTable.GetIdTermListPairs(term =>
                        term.Input1BasisBladeGrade == in1Grade &&
                        term.Input2BasisBladeGrade == in2Grade &&
                        term.Input3BasisBladeGrade == in3Grade
                    ).ToImmutableArray();

                if (termList.Length == 0)
                    continue;

                var productCode =
                    termList.Select(term =>
                        {
                            var lhsCode =
                                tempStorage[term.Key];

                            var rhsCode = term.Value.GetRhsCode(
                                idCode => $"mv2.KVector{in1Grade}.Scalar{idCode}",
                                idCode => $"mv1.KVector{in2Grade}.Scalar{idCode}",
                                idCode => $"mv2.KVector{in3Grade}.Scalar{idCode}"
                            );

                            return $"{lhsCode} += {rhsCode};";
                        }
                    ).Concatenate(Environment.NewLine);

                var zeroCheckCode = in1Grade == in3Grade
                    ? $"if (!mv2.KVector{in1Grade}.IsZero())"
                    : $"if (!mv2.KVector{in1Grade}.IsZero() && !mv2.KVector{in3Grade}.IsZero())";

                codeComposer2
                    .AppendLine(zeroCheckCode)
                    .AppendLine("{")
                    .IncreaseIndentation()
                    .AppendLine(productCode)
                    .DecreaseIndentation()
                    .AppendLine("}")
                    .AppendLine();
            }

            codeComposer
                .AppendLine($"if (!mv1.KVector{in2Grade}.IsZero())")
                .AppendLine("{")
                .IncreaseIndentation()
                .AppendLine(codeComposer2.ToString())
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
                .AppendLine($"public static {termTable.OutputType.ClassName} {funcName}(this {termTable.Input2Type.ClassName} mv1, {termTable.Input1Type.ClassName} mv2)")
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
            if (termTable.Input2Type.IsKVector)
                return termTable.Input1Type.IsKVector
                    ? GetTrilinearOperationCode_KvKv_Kv(funcName, termTable)
                    : GetTrilinearOperationCode_KvMv_Kv(funcName, termTable);

            return termTable.Input1Type.IsKVector
                ? GetTrilinearOperationCode_MvKv_Kv(funcName, termTable)
                : GetTrilinearOperationCode_MvMv_Kv(funcName, termTable);
        }

        if (termTable.Input2Type.IsKVector)
            return termTable.Input1Type.IsKVector
                ? GetTrilinearOperationCode_KvKv_Mv(funcName, termTable)
                : GetTrilinearOperationCode_KvMv_Mv(funcName, termTable);

        return termTable.Input1Type.IsKVector
            ? GetTrilinearOperationCode_MvKv_Mv(funcName, termTable)
            : GetTrilinearOperationCode_MvMv_Mv(funcName, termTable);
    }

    private string GetRotorMapCode()
    {
        var codeComposer = new LinearTextComposer();
        
        foreach (var inType2 in Types)
        {
            foreach (var inType13 in Types)
            {
                // Only even blades can be rotors
                if (inType13 is LibTypeKVector inKVectorType13 && inKVectorType13.Grade.IsOdd())
                    continue;

                var termTable =
                    LibTrilinearCombination.CreateFromEqualFirstThirdInputs(
                        inType13,
                        inType2,
                        Rotate
                    ).SetOutputType(inType2);

                codeComposer.AppendLineAtNewLine(
                    GetTrilinearOperationCode(
                        "MapUsingRotor",
                        termTable
                    )
                );
            }
        }
        
        return codeComposer.ToString();
    }


    public override TextFilesComposer GenerateCode()
    {
        CodeFilesComposer.InitializeFile(GaSpaceName + "RotorMap.cs");

        var codeComposer = CodeFilesComposer.ActiveFileTextComposer;

        codeComposer
            .AppendLine("using System;")
            .AppendLine("using System.Runtime.CompilerServices;")
            .AppendLine()
            .AppendLine($"namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.{GaSpaceName};")
            .AppendLine()
            .AppendLine($"public static class {GaSpaceName}RotorMap")
            .AppendLine("{")
            .IncreaseIndentation()
            .AppendLine(GetRotorMapCode())
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