using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;
using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.GradedMultivectorsLib.Combinations;
using GeometricAlgebraFulcrumLib.Utilities.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Files;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;
using GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.GradedMultivectorsLib.Storage;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.GradedMultivectorsLib;

public class LibProductCodeComposer :
    LibSubCodeComposer
{
    public static LibProductCodeComposer Create(
        LibCodeComposerSpecs specs,
        TextFilesComposer codeFilesComposer,
        string productClassName,
        string productFunctionName,
        Func<RGaBasisBlade, RGaBasisBlade, IRGaSignedBasisBlade> basisMapFunc
    )
    {
        return new LibProductCodeComposer(specs, codeFilesComposer)
        {
            ProductClassName = productClassName,
            ProductFunctionName = productFunctionName,
            BasisMapFunc = basisMapFunc
        };
    }


    public string ProductClassName { get; init; }

    public string ProductFunctionName { get; init; }

    public Func<RGaBasisBlade, RGaBasisBlade, IRGaSignedBasisBlade> BasisMapFunc { get; init; }


    private LibProductCodeComposer(LibCodeComposerSpecs specs, TextFilesComposer codeFilesComposer)
        : base(specs, codeFilesComposer)
    {
    }


    private string GetBilinearOperationCode_KvKv_Kv(LibBilinearCombination termTable)
    {
        var in1ClassName = termTable.Input1Type.ClassName;
        var in2ClassName = termTable.Input2Type.ClassName;
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
                            idCode => $"mv2.Scalar{idCode}"
                        );

                        return $"{lhsIdCode} = {rhsCode}";
                    }
                ).Concatenate("," + Environment.NewLine);

        return new LinearTextComposer()
            .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
            .AppendLine($"public static {outClassName} {ProductFunctionName}(this {in1ClassName} mv1, {in2ClassName} mv2)")
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

    private string GetBilinearOperationCode_KvMv_Kv(LibBilinearCombination termTable)
    {
        var in1ClassName = termTable.Input1Type.ClassName;
        var in2ClassName = termTable.Input2Type.ClassName;
        var outClassName = termTable.OutputType.ClassName;

        var in1Grade = termTable.GetInput1BasisBladeGrades()[0];
        var in2GradeList = termTable.GetInput2BasisBladeGrades();
        var outGrade = termTable.GetOutputBasisBladeGrades()[0];

        var outKvSpaceDimensions = (int)VSpaceDimensions.ComputeBinomialCoefficient(outGrade);

        var codeComposer = new LinearTextComposer();

        var tempStorage = LibTempStorage.CreateAutomatic(
            "tempScalar",
            outKvSpaceDimensions
        );

        codeComposer
            .AppendLine($"public static {outClassName} {ProductFunctionName}(this {in1ClassName} mv1, {in2ClassName} mv2)")
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
                    term.Input1BasisBladeGrade == in1Grade &&
                    term.Input2BasisBladeGrade == in2Grade &&
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
                            idCode => $"mv1.Scalar{idCode}",
                            idCode => $"mv2.KVector{in2Grade}.Scalar{idCode}"
                        );

                        return $"{lhsCode} += {rhsCode};";
                    }
                ).Concatenate(Environment.NewLine);

            codeComposer
                .AppendLine($"if (!mv2.KVector{in2Grade}.IsZero())")
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

    private string GetBilinearOperationCode_MvKv_Kv(LibBilinearCombination termTable)
    {
        var in1ClassName = termTable.Input1Type.ClassName;
        var in2ClassName = termTable.Input2Type.ClassName;
        var outClassName = termTable.OutputType.ClassName;

        var in1GradeList = termTable.GetInput1BasisBladeGrades();
        var in2Grade = termTable.GetInput2BasisBladeGrades()[0];
        var outGrade = termTable.GetOutputBasisBladeGrades()[0];

        var outKvSpaceDimensions = (int)VSpaceDimensions.ComputeBinomialCoefficient(outGrade);

        var tempStorage = LibTempStorage.CreateAutomatic(
            "tempScalar",
            outKvSpaceDimensions
        );

        var codeComposer = new LinearTextComposer();

        codeComposer
            .AppendLine($"public static {outClassName} {ProductFunctionName}(this {in1ClassName} mv1, {in2ClassName} mv2)")
            .AppendLine("{")
            .IncreaseIndentation()
            .AppendLine($"if (mv1.IsZero() || mv2.IsZero()) return {outClassName}.Zero;")
            .AppendLine()
            .AppendLine(tempStorage.GetDeclareCode())
            .AppendLine();

        foreach (var in1Grade in in1GradeList)
        {
            var termList =
                termTable.GetIdTermListPairs(term =>
                    term.Input1BasisBladeGrade == in1Grade &&
                    term.Input2BasisBladeGrade == in2Grade &&
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
                            idCode => $"mv2.Scalar{idCode}"
                        );

                        return $"{lhsCode} += {rhsCode};";
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

    private string GetBilinearOperationCode_MvMv_Kv(LibBilinearCombination termTable)
    {
        var in1ClassName = termTable.Input1Type.ClassName;
        var in2ClassName = termTable.Input2Type.ClassName;
        var outClassName = termTable.OutputType.ClassName;

        var in1GradeList = termTable.GetInput1BasisBladeGrades();
        var in2GradeList = termTable.GetInput2BasisBladeGrades();
        var outGrade = termTable.GetOutputBasisBladeGrades()[0];

        var outKvSpaceDimensions = (int)VSpaceDimensions.ComputeBinomialCoefficient(outGrade);

        var tempStorage = LibTempStorage.CreateAutomatic(
            "tempScalar",
            outKvSpaceDimensions
        );

        var codeComposer = new LinearTextComposer();

        codeComposer
            .AppendLine($"public static {outClassName} {ProductFunctionName}(this {in1ClassName} mv1, {in2ClassName} mv2)")
            .AppendLine("{")
            .IncreaseIndentation()
            .AppendLine($"if (mv1.IsZero() || mv2.IsZero()) return {outClassName}.Zero;")
            .AppendLine()
            .AppendLine(tempStorage.GetDeclareCode())
            .AppendLine();

        foreach (var in1Grade in in1GradeList)
        {
            var codeComposer2 = new LinearTextComposer();

            foreach (var in2Grade in in2GradeList)
            {
                var termList =
                    termTable.GetIdTermListPairs(term =>
                        term.Input1BasisBladeGrade == in1Grade &&
                        term.Input2BasisBladeGrade == in2Grade &&
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
                                idCode => $"mv2.KVector{in2Grade}.Scalar{idCode}"
                            );

                            return $"{lhsCode} += {rhsCode};";
                        }
                    ).Concatenate(Environment.NewLine);

                codeComposer2
                    .AppendLine($"if (!mv2.KVector{in2Grade}.IsZero())")
                    .AppendLine("{")
                    .IncreaseIndentation()
                    .AppendLine(productCode)
                    .DecreaseIndentation()
                    .AppendLine("}")
                    .AppendLine();
            }

            codeComposer
                .AppendLine($"if (!mv1.KVector{in1Grade}.IsZero())")
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

    private string GetBilinearOperationCode_KvKv_Mv(LibBilinearCombination termTable)
    {
        var in1ClassName = termTable.Input1Type.ClassName;
        var in2ClassName = termTable.Input2Type.ClassName;
        var outClassName = termTable.OutputType.ClassName;

        var in1Grade = termTable.GetInput1BasisBladeGrades()[0];
        var in2Grade = termTable.GetInput2BasisBladeGrades()[0];

        var tempStorage = LibTempStorage.CreateArray(
            "tempScalar",
            GaSpaceDimensions
        );

        var codeComposer = new LinearTextComposer();

        codeComposer
            .AppendLine($"public static {outClassName} {ProductFunctionName}(this {in1ClassName} mv1, {in2ClassName} mv2)")
            .AppendLine("{")
            .IncreaseIndentation()
            .AppendLine($"if (mv1.IsZero() || mv2.IsZero()) return {outClassName}.Zero;")
            .AppendLine()
            .AppendLine(tempStorage.GetDeclareCode())
            .AppendLine();

        var termList =
            termTable.GetIdTermListPairs(term =>
                term.Input1BasisBladeGrade == in1Grade &&
                term.Input2BasisBladeGrade == in2Grade
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

    private string GetBilinearOperationCode_KvMv_Mv(LibBilinearCombination termTable)
    {
        var in1ClassName = termTable.Input1Type.ClassName;
        var in2ClassName = termTable.Input2Type.ClassName;
        var outClassName = termTable.OutputType.ClassName;

        var in1Grade = termTable.GetInput1BasisBladeGrades()[0];
        var in2GradeList = termTable.GetInput2BasisBladeGrades();

        var tempStorage = LibTempStorage.CreateArray(
            "tempScalar",
            GaSpaceDimensions
        );

        var codeComposer = new LinearTextComposer();

        codeComposer
            .AppendLine($"public static {outClassName} {ProductFunctionName}(this {in1ClassName} mv1, {in2ClassName} mv2)")
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
                    term.Input1BasisBladeGrade == in1Grade &&
                    term.Input2BasisBladeGrade == in2Grade
                ).ToImmutableArray();

            if (termList.Length == 0)
                continue;

            var productCode =
                termList.Select(term =>
                    {
                        var lhsCode =
                            tempStorage[term.Key];

                        var rhsCode = term.Value.GetRhsCode(
                            idCode => $"mv1.Scalar{idCode}",
                            idCode => $"mv2.KVector{in2Grade}.Scalar{idCode}"
                        );

                        return $"{lhsCode} += {rhsCode};";
                    }
                ).Concatenate(Environment.NewLine);

            codeComposer
                .AppendLine($"if (!mv2.KVector{in2Grade}.IsZero())")
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

    private string GetBilinearOperationCode_MvKv_Mv(LibBilinearCombination termTable)
    {
        var in1ClassName = termTable.Input1Type.ClassName;
        var in2ClassName = termTable.Input2Type.ClassName;
        var outClassName = termTable.OutputType.ClassName;

        var in1GradeList = termTable.GetInput1BasisBladeGrades();
        var in2Grade = termTable.GetInput2BasisBladeGrades()[0];

        var tempStorage = LibTempStorage.CreateArray(
            "tempScalar",
            GaSpaceDimensions
        );

        var codeComposer = new LinearTextComposer();

        codeComposer
            .AppendLine($"public static {outClassName} {ProductFunctionName}(this {in1ClassName} mv1, {in2ClassName} mv2)")
            .AppendLine("{")
            .IncreaseIndentation()
            .AppendLine($"if (mv1.IsZero() || mv2.IsZero()) return {outClassName}.Zero;")
            .AppendLine()
            .AppendLine(tempStorage.GetDeclareCode())
            .AppendLine();

        foreach (var in1Grade in in1GradeList)
        {
            var termList =
                termTable.GetIdTermListPairs(term =>
                    term.Input1BasisBladeGrade == in1Grade &&
                    term.Input2BasisBladeGrade == in2Grade
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
                            idCode => $"mv2.Scalar{idCode}"
                        );

                        return $"{lhsCode} += {rhsCode};";
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

    private string GetBilinearOperationCode_MvMv_Mv(LibBilinearCombination termTable)
    {
        var in1ClassName = termTable.Input1Type.ClassName;
        var in2ClassName = termTable.Input2Type.ClassName;
        var outClassName = termTable.OutputType.ClassName;

        var in1GradeList = termTable.GetInput1BasisBladeGrades();
        var in2GradeList = termTable.GetInput2BasisBladeGrades();

        var tempStorage = LibTempStorage.CreateArray(
            "tempScalar",
            GaSpaceDimensions
        );

        var codeComposer = new LinearTextComposer();

        codeComposer
            .AppendLine($"public static {outClassName} {ProductFunctionName}(this {in1ClassName} mv1, {in2ClassName} mv2)")
            .AppendLine("{")
            .IncreaseIndentation()
            .AppendLine($"if (mv1.IsZero() || mv2.IsZero()) return {outClassName}.Zero;")
            .AppendLine()
            .AppendLine(tempStorage.GetDeclareCode())
            .AppendLine();

        foreach (var in1Grade in in1GradeList)
        {
            var codeComposer2 = new LinearTextComposer();

            foreach (var in2Grade in in2GradeList)
            {
                var termList =
                    termTable.GetIdTermListPairs(term =>
                        term.Input1BasisBladeGrade == in1Grade &&
                        term.Input2BasisBladeGrade == in2Grade
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
                                idCode => $"mv2.KVector{in2Grade}.Scalar{idCode}"
                            );

                            return $"{lhsCode} += {rhsCode};";
                        }
                    ).Concatenate(Environment.NewLine);

                codeComposer2
                    .AppendLine($"if (!mv2.KVector{in2Grade}.IsZero())")
                    .AppendLine("{")
                    .IncreaseIndentation()
                    .AppendLine(productCode)
                    .DecreaseIndentation()
                    .AppendLine("}")
                    .AppendLine();
            }

            codeComposer
                .AppendLine($"if (!mv1.KVector{in1Grade}.IsZero())")
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

    private string GetBilinearOperationCode(LibBilinearCombination termTable)
    {
        if (termTable.IsEmpty)
        {
            return new LinearTextComposer()
                .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
                .AppendLine($"public static {termTable.OutputType.ClassName} {ProductFunctionName}(this {termTable.Input1Type.ClassName} mv1, {termTable.Input2Type.ClassName} mv2)")
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
                    ? GetBilinearOperationCode_KvKv_Kv(termTable)
                    : GetBilinearOperationCode_KvMv_Kv(termTable);

            return termTable.Input2Type.IsKVector
                ? GetBilinearOperationCode_MvKv_Kv(termTable)
                : GetBilinearOperationCode_MvMv_Kv(termTable);
        }

        if (termTable.Input1Type.IsKVector)
            return termTable.Input2Type.IsKVector
                ? GetBilinearOperationCode_KvKv_Mv(termTable)
                : GetBilinearOperationCode_KvMv_Mv(termTable);

        return termTable.Input2Type.IsKVector
            ? GetBilinearOperationCode_MvKv_Mv(termTable)
            : GetBilinearOperationCode_MvMv_Mv(termTable);
    }

    private string GetProductCode()
    {
        var codeComposer = new LinearTextComposer();

        foreach (var inType1 in Types)
        foreach (var inType2 in Types)
        {
            var termTable = LibBilinearCombination.Create(
                inType1,
                inType2,
                BasisMapFunc
            ).SelectOutputType(Types);

            codeComposer.AppendLineAtNewLine(
                GetBilinearOperationCode(termTable)
            );
        }

        return codeComposer.ToString();
    }


    public override TextFilesComposer GenerateCode()
    {
        CodeFilesComposer.InitializeFile($"{GaSpaceName}{ProductClassName}.cs");

        var codeComposer = CodeFilesComposer.ActiveFileTextComposer;

        var productCode =
            GetProductCode();

        codeComposer
            .AppendLine("using System.Runtime.CompilerServices;")
            .AppendLine()
            .AppendLine($"namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.{GaSpaceName};")
            .AppendLine()
            .AppendLine($"public static class {GaSpaceName}{ProductClassName}")
            .AppendLine("{")
            .IncreaseIndentation()
            .AppendLine(productCode)
            .DecreaseIndentation()
            .AppendLineAtNewLine("}")
            .AppendLine();

        CodeFilesComposer.ActiveFileComposer.FinalizeText(
            code => code.RemoveRepeatedEmptyLines()
        );

        return CodeFilesComposer;
    }
}