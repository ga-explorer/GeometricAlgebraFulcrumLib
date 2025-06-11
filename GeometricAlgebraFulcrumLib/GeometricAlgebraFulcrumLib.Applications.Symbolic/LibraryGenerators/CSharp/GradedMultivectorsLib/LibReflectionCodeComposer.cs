using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.CSharp.GradedMultivectorsLib.Combinations;
using GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.CSharp.GradedMultivectorsLib.Storage;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using GeometricAlgebraFulcrumLib.Utilities.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Files;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.CSharp.GradedMultivectorsLib;

public class LibReflectionCodeComposer :
    LibSubCodeComposer
{
    public static LibReflectionCodeComposer Create(LibCodeComposerSpecs specs, TextFilesComposer codeFilesComposer)
    {
        return new LibReflectionCodeComposer(specs, codeFilesComposer);
    }


    private static IXGaSignedBasisBlade ReflectDirectOnDirect(XGaBasisBlade b1, XGaBasisBlade b2, XGaBasisBlade b3)
    {
        var b4 = b1.Gp(b2).Gp(b3.Reverse());

        if (b4.Grade != b2.Grade)
            return b4.Metric.ZeroBasisBlade(b2.Grade);

        var sign = IntegerSign.Negative.Power(
            (b1.Grade + 1) * b2.Grade
        );

        return b4.Times(sign);
    }
    
    private static IXGaSignedBasisBlade ReflectDirectOnDual(XGaBasisBlade b1, XGaBasisBlade b2, XGaBasisBlade b3)
    {
        var b4 = b1.Gp(b2).Gp(b3.Reverse());

        if (b4.Grade != b2.Grade)
            return b4.Metric.ZeroBasisBlade(b2.Grade);

        var sign = IntegerSign.Negative.Power(
            b1.Grade * b2.Grade
        );

        return b4.Times(sign);
    }

    private IXGaSignedBasisBlade ReflectDualOnDirect(XGaBasisBlade b1, XGaBasisBlade b2, XGaBasisBlade b3)
    {
        var b4 = b1.Gp(b2).Gp(b3.Reverse());

        if (b4.Grade != b2.Grade)
            return b4.Metric.ZeroBasisBlade(b2.Grade);

        var sign = IntegerSign.Negative.Power(
            (b1.Grade + 1) * (b2.Grade + 1) + (VSpaceDimensions - 1)
        );

        return b4.Times(sign);
    }

    private static IXGaSignedBasisBlade ReflectDualOnDual(XGaBasisBlade b1, XGaBasisBlade b2, XGaBasisBlade b3)
    {
        var b4 = b1.Gp(b2).Gp(b3.Reverse());

        if (b4.Grade != b2.Grade)
            return b4.Metric.ZeroBasisBlade(b2.Grade);

        var sign = IntegerSign.Negative.Power(
            b1.Grade * (b2.Grade + 1)
        );

        return b4.Times(sign);
    }


    private LibReflectionCodeComposer(LibCodeComposerSpecs specs, TextFilesComposer codeFilesComposer)
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
                    : throw new InvalidOperationException(); //GetTrilinearOperationCode_KvMv_Kv(funcName, termTable);

            return termTable.Input1Type.IsKVector
                ? GetTrilinearOperationCode_MvKv_Kv(funcName, termTable)
                : throw new InvalidOperationException(); //GetTrilinearOperationCode_MvMv_Kv(funcName, termTable);
        }

        if (termTable.Input2Type.IsKVector)
            return termTable.Input1Type.IsKVector
                ? GetTrilinearOperationCode_KvKv_Mv(funcName, termTable)
                : throw new InvalidOperationException(); //GetTrilinearOperationCode_KvMv_Mv(funcName, termTable);

        return termTable.Input1Type.IsKVector
            ? GetTrilinearOperationCode_MvKv_Mv(funcName, termTable)
            : throw new InvalidOperationException(); //GetTrilinearOperationCode_MvMv_Mv(funcName, termTable);
    }

    private string GetReflectionCode()
    {
        var codeComposer = new LinearTextComposer();

        var basisMapFuncArray = new[]
        {
            ReflectDirectOnDirect,
            ReflectDirectOnDual,
            ReflectDualOnDirect,
            ReflectDualOnDual
        };
        
        var funcNameArray = new[]
        {
            "ReflectDirectOnDirect",
            "ReflectDirectOnDual",
            "ReflectDualOnDirect",
            "ReflectDualOnDual"
        };

        for (var i = 0; i < basisMapFuncArray.Length; i++)
        {
            foreach (var inType2 in Types)
            {
                var inType13List =
                    KVectorTypes.Where(t => t.Grade > 0);

                foreach (var inType13 in inType13List)
                {
                    var termTable =
                        LibTrilinearCombination.CreateFromEqualFirstThirdInputs(
                            inType13,
                            inType2,
                            basisMapFuncArray[i]
                        ).SetOutputType(inType2);

                    codeComposer.AppendLineAtNewLine(
                        GetTrilinearOperationCode(
                            funcNameArray[i],
                            termTable
                        )
                    );
                }
            }
        }

        return codeComposer.ToString();
    }


    public override TextFilesComposer GenerateCode()
    {
        CodeFilesComposer.InitializeFile(GaSpaceName + "Reflection.cs");

        var codeComposer = CodeFilesComposer.ActiveFileTextComposer;

        codeComposer
            .AppendLine("using System;")
            .AppendLine("using System.Runtime.CompilerServices;")
            .AppendLine()
            .AppendLine($"namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.{GaSpaceName};")
            .AppendLine()
            .AppendLine($"public static class {GaSpaceName}Reflection")
            .AppendLine("{")
            .IncreaseIndentation()
            .AppendLine(GetReflectionCode())
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