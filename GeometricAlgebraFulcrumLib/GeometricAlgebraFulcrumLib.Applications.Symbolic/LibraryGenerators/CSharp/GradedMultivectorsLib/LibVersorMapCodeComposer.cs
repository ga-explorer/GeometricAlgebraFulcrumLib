﻿using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.CSharp.GradedMultivectorsLib.Combinations;
using GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.CSharp.GradedMultivectorsLib.Storage;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Files;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.CSharp.GradedMultivectorsLib;

public class LibVersorMapCodeComposer :
    LibSubCodeComposer
{
    public static LibVersorMapCodeComposer Create(LibCodeComposerSpecs specs, TextFilesComposer codeFilesComposer)
    {
        return new LibVersorMapCodeComposer(specs, codeFilesComposer);
    }
    
    
    private static IXGaSignedBasisBlade VersorMap(XGaBasisBlade b1, XGaBasisBlade b2, XGaBasisBlade b3)
    {
        var outputGrade = b2.Grade;
        var metric = b2.Metric;

        if (b1.Grade.IsOdd() != b3.Grade.IsOdd())
            return metric.ZeroBasisBlade(outputGrade);

        var b4 = 
            b1.Grade.IsOdd()
            ? b1.Gp(b2.GradeInvolution()).Gp(b3.Reverse())
            : b1.Gp(b2).Gp(b3.Reverse());

        if (b4.Grade != outputGrade)
            return metric.ZeroBasisBlade(outputGrade);
        
        return b4;
    }
    
    private static IXGaSignedBasisBlade EvenVersorMap(XGaBasisBlade b1, XGaBasisBlade b2, XGaBasisBlade b3)
    {
        var outputGrade = b2.Grade;
        var metric = b2.Metric;

        if (b1.Grade.IsOdd() || b3.Grade.IsOdd())
            return metric.ZeroBasisBlade(outputGrade);

        var b4 = 
            b1.Gp(b2).Gp(b3.Reverse());

        if (b4.Grade != outputGrade)
            return metric.ZeroBasisBlade(outputGrade);
        
        return b4;
    }
    
    private static IXGaSignedBasisBlade OddVersorMap(XGaBasisBlade b1, XGaBasisBlade b2, XGaBasisBlade b3)
    {
        var outputGrade = b2.Grade;
        var metric = b2.Metric;

        if (b1.Grade.IsEven() || b3.Grade.IsEven())
            return metric.ZeroBasisBlade(outputGrade);

        var b4 = 
            b1.Gp(b2.GradeInvolution()).Gp(b3.Reverse());

        if (b4.Grade != outputGrade)
            return metric.ZeroBasisBlade(outputGrade);
        
        return b4;
    }


    private LibVersorMapCodeComposer(LibCodeComposerSpecs specs, TextFilesComposer codeFilesComposer)
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

                        var rhsCode = p.Value.GetRhsCode(
                            idCode => $"mv2.Scalar{idCode}",
                            idCode => $"mv1.Scalar{idCode}",
                            idCode => $"mv2.Scalar{idCode}"
                        );

                        return $"{lhsIdCode} = ({rhsCode}) * mv2NormSquaredInv";
                    }
                ).Concatenate("," + Environment.NewLine);

        return new LinearTextComposer()
            .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
            .AppendLine($"public static {outClassName} {funcName}(this {in2ClassName} mv1, {in13ClassName} mv2)")
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
            .AppendLine("var mv2NormSquaredInv = 1d / mv2.NormSquared();")
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
                            (int)((IndexSet)term.Key).BasisBladeIdToIndex()
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

                return $"Scalar{idCode} = ({rhsCode}) * mv2NormSquaredInv";
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
            .AppendLine("var mv2NormSquaredInv = 1d / mv2.NormSquared();")
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
                            (int)((IndexSet)term.Key).BasisBladeIdToIndex()
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

                return $"Scalar{idCode} = ({rhsCode}) * mv2NormSquaredInv";
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
            .AppendLine("var mv2NormSquaredInv = 1d / mv2.NormSquared();")
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
                                (int)((IndexSet)term.Key).BasisBladeIdToIndex()
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

                return $"Scalar{idCode} = ({rhsCode}) * mv2NormSquaredInv";
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
            .AppendLine("var mv2NormSquaredInv = 1d / mv2.NormSquared();")
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
            .AppendLine("var mv2NormSquaredInv = 1d / mv2.NormSquared();")
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

                        return $"{lhsCode} += ({rhsCode}) * mv2NormSquaredInv;";
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
            .AppendLine("var mv2NormSquaredInv = 1d / mv2.NormSquared();")
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

                        return $"{lhsCode} += ({rhsCode}) * mv2NormSquaredInv;";
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
            .AppendLine("var mv2NormSquaredInv = 1d / mv2.NormSquared();")
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

                            return $"{lhsCode} += ({rhsCode}) * mv2NormSquaredInv;";
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

    private string GetVersorMapCode()
    {
        var codeComposer = new LinearTextComposer();
        
        foreach (var inType2 in Types)
        {
            foreach (var inType13 in KVectorTypes)
            {
                var termTable =
                    LibTrilinearCombination.CreateFromEqualFirstThirdInputs(
                        inType13,
                        inType2,
                        VersorMap
                    ).SetOutputType(inType2);

                codeComposer.AppendLineAtNewLine(
                    GetTrilinearOperationCode(
                        "MapUsingVersor",
                        termTable
                    )
                );
            }

            {
                var inType13 = MultivectorType;

                var termTable =
                    LibTrilinearCombination.CreateFromEqualFirstThirdInputs(
                        inType13,
                        inType2,
                        EvenVersorMap
                    ).SetOutputType(inType2);

                codeComposer.AppendLineAtNewLine(
                    GetTrilinearOperationCode(
                        "MapUsingEvenVersor",
                        termTable
                    )
                );
            }

            {
                var inType13 = MultivectorType;

                var termTable =
                    LibTrilinearCombination.CreateFromEqualFirstThirdInputs(
                        inType13,
                        inType2,
                        OddVersorMap
                    ).SetOutputType(inType2);

                codeComposer.AppendLineAtNewLine(
                    GetTrilinearOperationCode(
                        "MapUsingOddVersor",
                        termTable
                    )
                );
            }
        }
        
        return codeComposer.ToString();
    }


    public override TextFilesComposer GenerateCode()
    {
        CodeFilesComposer.InitializeFile(GaSpaceName + "VersorMap.cs");

        var codeComposer = CodeFilesComposer.ActiveFileTextComposer;

        codeComposer
            .AppendLine("using System;")
            .AppendLine("using System.Runtime.CompilerServices;")
            .AppendLine()
            .AppendLine($"namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.{GaSpaceName};")
            .AppendLine()
            .AppendLine($"public static class {GaSpaceName}VersorMap")
            .AppendLine("{")
            .IncreaseIndentation()
            .AppendLine(GetVersorMapCode())
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