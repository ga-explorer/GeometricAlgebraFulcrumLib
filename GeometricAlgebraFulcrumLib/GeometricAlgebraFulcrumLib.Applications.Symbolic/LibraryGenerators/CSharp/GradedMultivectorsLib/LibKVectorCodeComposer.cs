using GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.CSharp.GradedMultivectorsLib.Combinations;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;
using GeometricAlgebraFulcrumLib.Utilities.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Files;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Parametric;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.CSharp.GradedMultivectorsLib;

public class LibKVectorCodeComposer :
    LibSubCodeComposer
{
    public static LibKVectorCodeComposer Create(
        LibCodeComposerSpecs specs,
        TextFilesComposer codeFilesComposer,
        int grade
    )
    {
        return new LibKVectorCodeComposer(specs, codeFilesComposer)
        {
            Grade = grade
        };
    }


    public int Grade { get; init; }


    private LibKVectorCodeComposer(LibCodeComposerSpecs specs, TextFilesComposer codeFilesComposer)
        : base(specs, codeFilesComposer)
    {
    }


    private string GetStaticPropertiesCode()
    {
        var className = KVectorTypes[Grade].ClassName;

        var codeComposer = new LinearTextComposer();

        codeComposer.AppendLine(
            $"public static {className} Zero {{ get; }} = new {className}();"
        ).AppendLine();

        foreach (var basisBlade in KVectorTypes[Grade].GetBasisBlades())
        {
            var memberCode = GradedMultivectorLibUtils.GetBasisBladeCode(
                (int)basisBlade.Id,
                idText => $"public static {className} E{idText} {{ get; }} = new {className}() {{ Scalar{idText} = 1d }};"
            );

            codeComposer
                .AppendLine(memberCode)
                .AppendLine();
        }

        return codeComposer.ToString();
    }
    
    public string GetStaticFactoryCode()
    {
        var kvClassName = 
            KVectorTypes[Grade].ClassName;

        var kvSpaceDimensions =
            (int)VSpaceDimensions.GetBinomialCoefficient(Grade);

        var kvArrayCode =
            kvSpaceDimensions.GetRange(
                index =>
                {
                    var id = (int)BasisBladeUtils.BasisBladeGradeIndexToId(Grade, (ulong)index);

                    return $"Scalar{GradedMultivectorLibUtils.GetBasisBladeCode(id)} = scalarArray[{index}]";
                }
            ).Concatenate("," + Environment.NewLine);
        
        var codeComposer = new ParametricTextComposer(
            "#",
            "#",
            @"
[MethodImpl(MethodImplOptions.AggressiveInlining)]
public static #kv-class-name# Create(params double[] scalarArray)
{
    return new #kv-class-name#
    {
        #kv-array-code#
    };
}
".Trim()
        );

        return codeComposer.GenerateText(
            new Dictionary<string, string>
            {
                {"kv-class-name", kvClassName},
                {"kv-array-code", kvArrayCode}
            }
        );
    }

    private string GetStaticOperatorsCode()
    {
        var kv1ClassName = KVectorTypes[Grade].ClassName;
        var mvClassName = MultivectorType.ClassName;
        var codeComposer = new LinearTextComposer();

        var codeComposer1 = new ParametricTextComposer(
            "#",
            "#",
        @"
[MethodImpl(MethodImplOptions.AggressiveInlining)]
public static #class1-name# operator +(#class1-name# mv)
{
    return mv;
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public static #class1-name# operator -(#class1-name# mv)
{
    return mv.Negative();
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public static #class1-name# operator *(#class1-name# mv1, double mv2)
{
    return mv1.Times(mv2);
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public static #class1-name# operator *(double mv1, #class1-name# mv2)
{
    return mv2.Times(mv1);
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public static #class1-name# operator /(#class1-name# mv1, double mv2)
{
    return mv1.Times(1d / mv2);
}
".Trim()
            );

        codeComposer.AppendLine(
            codeComposer1.GenerateText(
                new Dictionary<string, string>()
                {
                    {"class1-name", kv1ClassName}
                }
            )
        );

        codeComposer1 = new ParametricTextComposer(
            "#",
            "#",
            @"
[MethodImpl(MethodImplOptions.AggressiveInlining)]
public static #out-class-name# operator +(#class1-name# mv1, #class2-name# mv2)
{
    return mv1.Add(mv2);
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public static #out-class-name# operator -(#class1-name# mv1, #class2-name# mv2)
{
    return mv1.Subtract(mv2);
}
".Trim()
        );

        for (var grade2 = 0; grade2 < VSpaceDimensions; grade2++)
        {
            var kv2ClassName = KVectorTypes[grade2].ClassName;
            var outClassName = Grade == grade2 ? kv1ClassName : mvClassName;

            codeComposer.AppendLine(
                codeComposer1.GenerateText(
                    new Dictionary<string, string>()
                    {
                        {"class1-name", kv1ClassName},
                        {"class2-name", kv2ClassName},
                        {"out-class-name", outClassName}
                    }
                )
            ).AppendLine();
        }

        codeComposer.AppendLine(
            codeComposer1.GenerateText(
                new Dictionary<string, string>()
                {
                    {"class1-name", kv1ClassName},
                    {"class2-name", mvClassName},
                    {"out-class-name", mvClassName}
                }
            )
        ).AppendLine();

        {
            codeComposer1 = new ParametricTextComposer(
                "#",
                "#",
                @"
[MethodImpl(MethodImplOptions.AggressiveInlining)]
public static #out-class-name# operator +(#class1-name# mv1, double mv2)
{
    return #add1-double-code#;
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public static #out-class-name# operator +(double mv1, #class1-name# mv2)
{
    return #add2-double-code#;
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public static #out-class-name# operator -(#class1-name# mv1, double mv2)
{
    return #subtract1-double-code#;
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public static #out-class-name# operator -(double mv1, #class1-name# mv2)
{
    return #subtract2-double-code#;
}
".Trim()
            );

            var outClassName = 
                Grade == 0 ? kv1ClassName : mvClassName;

            var add1DoubleCode = 
                Grade == 0
                    ? $"new {kv1ClassName}(mv1.Scalar + mv2)"
                    : $"{mvClassName}.Create(mv1, mv2)";

            var add2DoubleCode = 
                Grade == 0
                    ? $"new {kv1ClassName}(mv1 + mv2.Scalar)"
                    : $"{mvClassName}.Create(mv2, mv1)";

            var subtract1DoubleCode =  
                Grade == 0
                    ? $"new {kv1ClassName}(mv1.Scalar - mv2)"
                    : $"{mvClassName}.Create(mv1, -mv2)";

            var subtract2DoubleCode =  
                Grade == 0
                    ? $"new {kv1ClassName}(mv1 - mv2.Scalar)"
                    : $"{mvClassName}.Create(mv2.Negative(), mv1)";

            codeComposer.AppendLine(
                codeComposer1.GenerateText(
                    new Dictionary<string, string>()
                    {
                        { "class1-name", kv1ClassName },
                        { "out-class-name", outClassName },
                        { "add1-double-code", add1DoubleCode },
                        { "add2-double-code", add2DoubleCode },
                        { "subtract1-double-code", subtract1DoubleCode },
                        { "subtract2-double-code", subtract2DoubleCode }
                    }
                )
            ).AppendLine();
        }

        return codeComposer.ToString();
    }

    private string GetPropertiesCode()
    {
        return Specs
            .GetKVectorScalarNames(Grade)
            .Select(scalarName => $"public double {scalarName} {{ get; init; }}")
            .Concatenate(Environment.NewLine + Environment.NewLine);
    }

    private string GetConstructorCode()
    {
        var className = KVectorTypes[Grade].ClassName;

        var codeComposer = new LinearTextComposer();

        codeComposer
            .AppendLine(@$"
[MethodImpl(MethodImplOptions.AggressiveInlining)]
public {className}()
{{
}}
".Trim());

        if (Grade == 0 || Grade == VSpaceDimensions)
        {
            var scalarName = 
                GradedMultivectorLibUtils.GetBasisBladeScalarName(Grade, 0);

            codeComposer
                .AppendLine()
                .AppendLine(@$"
[MethodImpl(MethodImplOptions.AggressiveInlining)]
public {className}(double scalar)
{{
    {scalarName} = scalar;
}}
".Trim());
        }

        return codeComposer.ToString();
    }

    private string GetIsValidCode()
    {
        var scalarsCode =
            Specs
                .GetKVectorScalarNames(Grade)
                .Select(scalarName => $"!double.IsNaN({scalarName})")
                .Concatenate(" &&" + Environment.NewLine);

        var codeComposer = new ParametricTextComposer(
            "#",
            "#",
            @"
[MethodImpl(MethodImplOptions.AggressiveInlining)]
public bool IsValid()
{
    return
        #scalars_code#;
}
".Trim()
        );

        return codeComposer.GenerateText(
            new Dictionary<string, string>()
            {
                    {"scalars_code", scalarsCode}
            }
        );
    }

    private string GetIsZeroCode()
    {
        var scalarsCode =
            Specs
                .GetKVectorScalarNames(Grade)
                .Select(scalarName => $"{scalarName} == 0d")
                .Concatenate(" &&" + Environment.NewLine);

        var codeComposer = new ParametricTextComposer(
            "#",
            "#",
            @"
private bool? _isZero;
[MethodImpl(MethodImplOptions.AggressiveInlining)]
public bool IsZero()
{
    _isZero ??= 
        #scalars_code#;

    return _isZero.Value;
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public bool IsNearZero(double epsilon = 1e-12d)
{
    var norm = this.Norm();

    return norm > -epsilon && norm < epsilon;
}
".Trim()
        );

        return codeComposer.GenerateText(
            new Dictionary<string, string>()
            {
                    {"scalars_code", scalarsCode}
            }
        );
    }

    private string GetToArrayCode()
    {
        var kvSpaceDimensions =
            (int)VSpaceDimensions.GetBinomialCoefficient(Grade);

        var mvArrayCode =
            kvSpaceDimensions.GetRange(
                index =>
                {
                    var id = (int)BasisBladeUtils.BasisBladeGradeIndexToId(Grade, (ulong)index);

                    return $"scalarArray[{id}] = Scalar{GradedMultivectorLibUtils.GetBasisBladeCode(id)};";
                }
            ).Concatenate(Environment.NewLine);

        var kvArrayCode =
            kvSpaceDimensions.GetRange(
                index =>
                {
                    var id = (int)BasisBladeUtils.BasisBladeGradeIndexToId(Grade, (ulong)index);

                    return $"Scalar{GradedMultivectorLibUtils.GetBasisBladeCode(id)}";
                }
            ).Concatenate("," + Environment.NewLine);

        var codeComposer = new ParametricTextComposer(
            "#",
            "#",
            @"
[MethodImpl(MethodImplOptions.AggressiveInlining)]
public double[] GetMultivectorArray()
{
    var scalarArray = new double[#mv-array-size#];
    
    #mv-array-code#
    
    return scalarArray;
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public double[] GetKVectorArray()
{
    return new double[]
    {
        #kv-array-code#
    };
}
".Trim()
        );

        return codeComposer.GenerateText(
            new Dictionary<string, string>
            {
                    {"mv-array-size", GaSpaceDimensions.ToString()},
                    {"mv-array-code", mvArrayCode},
                    {"kv-array-code", kvArrayCode}
            }
        );
    }

    private string GetUnaryOperationsCode()
    {
        var className = KVectorTypes[Grade].ClassName;

        var timesScalarsCode =
            KVectorTypes[Grade].GetBasisBladeIDs()
                .Select(id => GradedMultivectorLibUtils.GetBasisBladeCode(
                        id,
                        idText => $"Scalar{idText} = Scalar{idText} * mv2"
                    )
                ).Concatenate("," + Environment.NewLine);

        var negativeScalarsCode =
            KVectorTypes[Grade].GetBasisBladeIDs()
                .Select(id => GradedMultivectorLibUtils.GetBasisBladeCode(
                        id,
                        idText => $"Scalar{idText} = -Scalar{idText}"
                    )
                ).Concatenate("," + Environment.NewLine);

        var reverseCode = Grade.ReverseIsNegativeOfGrade()
            ? "Negative()"
            : "this";

        var gradeInvolutionCode = Grade.GradeInvolutionIsNegativeOfGrade()
            ? "Negative()"
            : "this";

        var cliffordConjugateCode = Grade.CliffordConjugateIsNegativeOfGrade()
            ? "Negative()"
            : "this";

        var reverseSignCode = Grade.ReverseIsNegativeOfGrade()
            ? "-"
            : "";

        var codeComposer = new ParametricTextComposer(
            "#",
            "#",
            @"
[MethodImpl(MethodImplOptions.AggressiveInlining)]
public #class-name# Times(double mv2)
{
    return new #class-name#()
    {
        #times-scalars-code#
    };
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public #class-name# Divide(double mv2)
{
    return Times(1d / mv2);
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public #class-name# DivideByNorm()
{
    return Times(1d / this.Norm());
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public #class-name# DivideByNormSquared()
{
    return Times(1d / this.NormSquared());
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public #class-name# DivideBySpSquared()
{
    return Times(1d / this.SpSquared());
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public #class-name# Negative()
{
    return new #class-name#()
    {
        #negative-scalars-code#
    };
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public #class-name# Reverse()
{
    return #reverse-code#;
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public #class-name# GradeInvolution()
{
    return #grade-involution-code#;
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public #class-name# CliffordConjugate()
{
    return #clifford-conjugate-code#;
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public #class-name# Inverse()
{
    return Times(#reverse-sign#1d / this.NormSquared());
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public #class-name# InverseTimes(double mv2)
{
    return Times(#reverse-sign#mv2 / this.NormSquared());
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public #class-name# PseudoInverse()
{
    var conjugate = Conjugate();

    return conjugate.Times(
        1d / conjugate.Sp(this).Scalar
    );
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public #class-name# PseudoInverseTimes(double mv2)
{
    var conjugate = Conjugate();

    return conjugate.Times(
        mv2 / conjugate.Sp(this).Scalar
    );
}
".Trim()
        );

        return codeComposer.GenerateText(
            new Dictionary<string, string>()
            {
                    { "class-name", className },
                    { "times-scalars-code", timesScalarsCode },
                    { "negative-scalars-code", negativeScalarsCode },
                    { "reverse-code", reverseCode },
                    { "grade-involution-code", gradeInvolutionCode },
                    { "clifford-conjugate-code", cliffordConjugateCode },
                    { "reverse-sign", reverseSignCode }
            }
        );
    }

    private string GetUnaryOperationCode(int inGrade, int outGrade, LibUnilinearCombination termTable)
    {
        var outClassName = KVectorTypes[outGrade].ClassName;

        string outputCode;

        if (termTable.IsEmpty)
        {
            outputCode = $"{outClassName}.Zero";
        }
        else if (inGrade == outGrade && termTable.IsKVectorIdentity(inGrade))
        {
            outputCode = "this";
        }
        else if (inGrade == outGrade && termTable.IsKVectorNegativeIdentity(inGrade))
        {
            outputCode = "Negative()";
        }
        else if (termTable.IsOutputKVector(outGrade))
        {
            var scalarCode =
                termTable.GetIdTermListPairs().Select(p =>
                    {
                        var idText = GradedMultivectorLibUtils.GetBasisBladeCode(p.Key);
                        var rhsCode = p.Value.GetRhsCode(
                            idText1 => $"Scalar{idText1}"
                        );

                        return $"Scalar{idText} = {rhsCode}";
                    }
                ).Concatenate("," + Environment.NewLine);

            outputCode =
                new LinearTextComposer()
                    .AppendLine($"new {outClassName}()")
                    .AppendLine("{")
                    .IncreaseIndentation()
                    .AppendLine(scalarCode)
                    .DecreaseIndentation()
                    .AppendLine("}")
                    .ToString();
        }
        else
        {
            throw new InvalidOperationException();
        }

        return outputCode.Trim();
    }

    private string GetConjugateCode()
    {
        var codeComposer = new LinearTextComposer();

        var termTable = LibUnilinearCombination.Create(
            KVectorTypes[Grade],
            b1 => b1.Conjugate()
        );

        var outClassName = KVectorTypes[Grade].ClassName;
        var conjugateCode = GetUnaryOperationCode(Grade, Grade, termTable);

        var codeComposer1 = new ParametricTextComposer(
            "#",
            "#",
            @"
[MethodImpl(MethodImplOptions.AggressiveInlining)]
public #class-name# Conjugate()
{
    return #conjugate-code#;
}
".Trim()
        );

        codeComposer
            .AppendLineAtNewLine(
                codeComposer1.GenerateText(
                    new Dictionary<string, string>()
                    {
                            { "class-name", outClassName },
                            { "conjugate-code", conjugateCode }
                    }
                )
            );

        return codeComposer.ToString();
    }

    private string GetDualCode()
    {
        var codeComposer = new LinearTextComposer();

        if (!Metric.IsDegenerate)
        {
            var eiInv =
                Metric.CreateBasisPseudoScalarInverse(VSpaceDimensions);

            var termTable = LibUnilinearCombination.Create(
                KVectorTypes[Grade],
                b1 => b1.Lcp(eiInv)
            );

            var outClassName = Specs.GetOutputClassName(termTable);
            var dualCode = GetUnaryOperationCode(Grade, VSpaceDimensions - Grade, termTable);

            var codeComposer1 = new ParametricTextComposer(
                "#",
                "#",
                @"
[MethodImpl(MethodImplOptions.AggressiveInlining)]
public #class-name# Dual()
{
    return #dual-code#;
}
".Trim()
            );

            codeComposer
                .AppendLineAtNewLine(
                    codeComposer1.GenerateText(
                        new Dictionary<string, string>()
                        {
                                { "class-name", outClassName },
                                { "dual-code", dualCode }
                        }
                    )
                ).AppendLine();
        }

        {
            var ei =
                Metric.CreateBasisPseudoScalar(VSpaceDimensions);

            var termTable = LibUnilinearCombination.Create(
                KVectorTypes[Grade],
                b1 => b1.Lcp(ei)
            );

            var outClassName = Specs.GetOutputClassName(termTable);
            var unDualCode = GetUnaryOperationCode(Grade, VSpaceDimensions - Grade, termTable);

            var codeComposer1 = new ParametricTextComposer(
                "#",
                "#",
                @"
[MethodImpl(MethodImplOptions.AggressiveInlining)]
public #class-name# UnDual()
{
    return #undual-code#;
}
".Trim()
            );

            codeComposer
                .AppendLineAtNewLine(
                    codeComposer1.GenerateText(
                        new Dictionary<string, string>()
                        {
                                { "class-name", outClassName },
                                { "undual-code", unDualCode }
                        }
                    )
                ).AppendLine();
        }

        {
            var codeComposer1 = new ParametricTextComposer(
                "#",
                "#",
                @"
[MethodImpl(MethodImplOptions.AggressiveInlining)]
public #class-name# Dual(#kv2-class-name# kv2)
{
    return this.Lcp(kv2.Inverse());
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public #class-name# UnDual(#kv2-class-name# kv2)
{
    return this.Lcp(kv2);
}
".Trim()
            );

            for (var grade2 = Grade; grade2 <= VSpaceDimensions; grade2++)
            {
                var kv2ClassName = KVectorTypes[grade2].ClassName;
                var outClassName = Specs.GetLcpOutType(Grade, grade2).ClassName;

                codeComposer
                    .AppendLineAtNewLine(
                        codeComposer1.GenerateText(
                            new Dictionary<string, string>()
                            {
                                    { "class-name", outClassName },
                                    { "kv2-class-name", kv2ClassName }
                            }
                        )
                    ).AppendLine();
            }
        }

        return codeComposer.ToString();
    }

    private string GetAddSubtractCode()
    {
        var mvClassName = MultivectorType.ClassName;
        var kv1ClassName = KVectorTypes[Grade].ClassName;

        var codeComposer = new LinearTextComposer();

        for (var grade2 = 0; grade2 <= VSpaceDimensions; grade2++)
        {
            var kv2ClassName = KVectorTypes[grade2].ClassName;

            if (grade2 == Grade)
            {
                var scalarCode =
                    KVectorTypes[Grade].GetBasisBladeIDs()
                        .Select(id =>
                        {
                            var idText = GradedMultivectorLibUtils.GetBasisBladeCode(id);

                            return $"Scalar{idText} = Scalar{idText} + mv2.Scalar{idText}";
                        }).Concatenate("," + Environment.NewLine);

                codeComposer
                    .AppendLineAtNewLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
                    .AppendLine($"public {kv1ClassName} Add({kv1ClassName} mv2)")
                    .AppendLine("{")
                    .IncreaseIndentation()
                    .AppendLine("if (mv2.IsZero()) return this;")
                    .AppendLine("if (IsZero()) return mv2;")
                    .AppendLine()
                    .AppendLine($"return new {kv1ClassName}()")
                    .AppendLine("{")
                    .IncreaseIndentation()
                    .AppendLine(scalarCode)
                    .DecreaseIndentation()
                    .AppendLine("};");
            }
            else
            {
                var g2 = grade2;
                var kVectorCode =
                    Specs.GradeCount.GetRange(
                        g =>
                        {
                            if (g == Grade) return "this";
                            if (g == g2) return "mv2";
                            return KVectorTypes[g].ClassName + ".Zero";
                        }
                    ).Concatenate("," + Environment.NewLine);

                codeComposer
                    .AppendLineAtNewLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
                    .AppendLine($"public {mvClassName} Add({kv2ClassName} mv2)")
                    .AppendLine("{")
                    .IncreaseIndentation()
                    .AppendLine($"if (mv2.IsZero()) return {mvClassName}.Create(this);")
                    .AppendLine($"if (IsZero()) return {mvClassName}.Create(mv2);")
                    .AppendLine()
                    .AppendLine($"return {mvClassName}.Create(")
                    .IncreaseIndentation()
                    .AppendLine(kVectorCode)
                    .DecreaseIndentation()
                    .AppendLine(");");
            }

            codeComposer
                .DecreaseIndentation()
                .AppendLine("}")
                .AppendLine();
        }

        {
            var mvCode =
                Specs.GradeCount.GetRange(
                    g => g == Grade
                        ? $"Add(mv2.KVector{Grade})"
                        : $"mv2.KVector{g}"
                ).Concatenate("," + Environment.NewLine);

            codeComposer
                .AppendLineAtNewLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
                .AppendLine($"public {mvClassName} Add({mvClassName} mv2)")
                .AppendLine("{")
                .IncreaseIndentation()
                .AppendLine($"if (mv2.IsZero()) return {mvClassName}.Create(this);")
                .AppendLine("if (IsZero()) return mv2;")
                .AppendLine()
                .AppendLine($"return {mvClassName}.Create(")
                .IncreaseIndentation()
                .AppendLine(mvCode)
                .DecreaseIndentation()
                .AppendLine(");")
                .DecreaseIndentation()
                .AppendLine("}")
                .AppendLine();
        }

        for (var grade2 = 0; grade2 <= VSpaceDimensions; grade2++)
        {
            var kv2ClassName = KVectorTypes[grade2].ClassName;

            if (grade2 == Grade)
            {
                var scalarCode =
                    KVectorTypes[Grade].GetBasisBladeIDs()
                        .Select(id =>
                        {
                            var idText = GradedMultivectorLibUtils.GetBasisBladeCode(id);

                            return $"Scalar{idText} = Scalar{idText} - mv2.Scalar{idText}";
                        }).Concatenate("," + Environment.NewLine);

                codeComposer
                    .AppendLineAtNewLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
                    .AppendLine($"public {kv1ClassName} Subtract({kv1ClassName} mv2)")
                    .AppendLine("{")
                    .IncreaseIndentation()
                    .AppendLine("if (mv2.IsZero()) return this;")
                    .AppendLine("if (IsZero()) return mv2.Negative();")
                    .AppendLine()
                    .AppendLine($"return new {kv1ClassName}()")
                    .AppendLine("{")
                    .IncreaseIndentation()
                    .AppendLine(scalarCode)
                    .DecreaseIndentation()
                    .AppendLine("};");
            }
            else
            {
                var g2 = grade2;
                var kVectorCode =
                    Specs.GradeCount.GetRange(
                        g =>
                        {
                            if (g == Grade) return "this";
                            if (g == g2) return "mv2.Negative()";
                            return KVectorTypes[g].ClassName + ".Zero";
                        }
                    ).Concatenate("," + Environment.NewLine);

                codeComposer
                    .AppendLineAtNewLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
                    .AppendLine($"public {mvClassName} Subtract({kv2ClassName} mv2)")
                    .AppendLine("{")
                    .IncreaseIndentation()
                    .AppendLine($"if (mv2.IsZero()) return {mvClassName}.Create(this);")
                    .AppendLine($"if (IsZero()) return {mvClassName}.Create(mv2.Negative());")
                    .AppendLine()
                    .AppendLine($"return {mvClassName}.Create(")
                    .IncreaseIndentation()
                    .AppendLine(kVectorCode)
                    .DecreaseIndentation()
                    .AppendLine(");");
            }

            codeComposer
                .DecreaseIndentation()
                .AppendLine("}")
                .AppendLine();
        }

        {
            var mvCode =
                Specs.GradeCount.GetRange(
                    g => g == Grade
                        ? $"Subtract(mv2.KVector{Grade})"
                        : $"mv2.KVector{g}.Negative()"
                ).Concatenate("," + Environment.NewLine);

            codeComposer
                .AppendLineAtNewLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
                .AppendLine($"public {mvClassName} Subtract({mvClassName} mv2)")
                .AppendLine("{")
                .IncreaseIndentation()
                .AppendLine($"if (mv2.IsZero()) return {mvClassName}.Create(this);")
                .AppendLine("if (IsZero()) return mv2.Negative();")
                .AppendLine()
                .AppendLine($"return {mvClassName}.Create(")
                .IncreaseIndentation()
                .AppendLine(mvCode)
                .DecreaseIndentation()
                .AppendLine(");")
                .DecreaseIndentation()
                .AppendLine("}")
                .AppendLine();
        }

        return codeComposer.ToString();
    }


    public override TextFilesComposer GenerateCode()
    {
        var kvClassName =
            KVectorTypes[Grade].ClassName;

        CodeFilesComposer.InitializeFile(kvClassName + ".cs");

        var codeComposer = CodeFilesComposer.ActiveFileTextComposer;

        codeComposer
            .AppendLine("using System.Runtime.CompilerServices;")
            .AppendLine()
            .AppendLine($"namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.{GaSpaceName};")
            .AppendLine()
            .AppendLine($"public sealed partial class {kvClassName}")
            .AppendLine("{")
            .IncreaseIndentation()
            .AppendLine(GetStaticPropertiesCode())
            .AppendLine()
            .AppendLine(GetStaticFactoryCode())
            .AppendLine()
            .AppendLine(GetStaticOperatorsCode())
            .AppendLine()
            .AppendLine(GetPropertiesCode())
            .AppendLine()
            .AppendLine(GetConstructorCode())
            .AppendLine()
            .AppendLine(GetIsValidCode())
            .AppendLine()
            .AppendLine(GetIsZeroCode())
            .AppendLine()
            .AppendLine(GetToArrayCode())
            .AppendLine()
            .AppendLine(GetUnaryOperationsCode())
            .AppendLine()
            .AppendLine(GetConjugateCode())
            .AppendLine()
            .AppendLine(GetDualCode())
            .AppendLine()
            .AppendLine(GetAddSubtractCode())
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