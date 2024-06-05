using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Files;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Parametric;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.GradedMultivectorsLib;

public class LibMultivectorCodeComposer :
    LibSubCodeComposer
{
    public static LibMultivectorCodeComposer Create(
        LibCodeComposerSpecs specs,
        TextFilesComposer codeFilesComposer
    )
    {
        return new LibMultivectorCodeComposer(specs, codeFilesComposer);
    }


    private LibMultivectorCodeComposer(LibCodeComposerSpecs specs, TextFilesComposer codeFilesComposer)
        : base(specs, codeFilesComposer)
    {
    }


    public string GetMultivectorStaticPropertyCode()
    {
        var codeComposer = new LinearTextComposer();

        var mvClassName = MultivectorType.ClassName;

        codeComposer
            .AppendLine($"public static {mvClassName} Zero {{ get; }} = {mvClassName}.Create(new double[{GaSpaceDimensions}]);")
            .AppendLine();

        for (var grade = 0; grade <= VSpaceDimensions; grade++)
        {
            var kvClassName = KVectorTypes[grade].ClassName;

            foreach (var basisBlade in KVectorTypes[grade].GetBasisBlades())
            {
                var memberCode = GradedMultivectorLibUtils.GetBasisBladeCode(
                    (int)basisBlade.Id,
                    idText => $"public static {mvClassName} E{idText} => Create({kvClassName}.E{idText});"
                );

                codeComposer
                    .AppendLine(memberCode)
                    .AppendLine();
            }
        }

        return codeComposer.ToString();
    }

    public string GetMultivectorStaticFactoryCode()
    {
        var mvClassName = MultivectorType.ClassName;

        var codeComposer = new LinearTextComposer();

        var codeComposer1 = new ParametricTextComposer(
            "#",
            "#",
            @"
[MethodImpl(MethodImplOptions.AggressiveInlining)]
public static #mv-class-name# Create(#kv-class-name# kVector)
{
    return new #mv-class-name#(
        #kv-arg-code#
    );
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public static #mv-class-name# Create(#kv-class-name# kVector, double scalar)
{
    return new #mv-class-name#(
        #kv-scalar-arg-code#
    );
}
".Trim()
        );

        for (var grade = 0; grade <= VSpaceDimensions; grade++)
        {
            var kvClassName = KVectorTypes[grade].ClassName;

            var g1 = grade;

            var kvArgCode = Specs.GradeCount.GetRange(
                g =>
                    g == g1 ? "kVector" : $"{KVectorTypes[g].ClassName}.Zero"
            ).Concatenate("," + Environment.NewLine);
            
            var kvScalarArgCode = Specs.GradeCount.GetRange(
                g =>
                {
                    if (g1 == 0)
                    {
                        return g == 0
                            ? $"new {ScalarType.ClassName} {{ Scalar = kVector.Scalar + scalar }}"
                            : $"{KVectorTypes[g].ClassName}.Zero";
                    }
                    else
                    {
                        if (g == 0)
                            return $"new {ScalarType.ClassName} {{ Scalar = scalar }}";

                        return g == g1 
                            ? "kVector" 
                            : $"{KVectorTypes[g].ClassName}.Zero";
                    }
                    
                }
            ).Concatenate("," + Environment.NewLine);

            codeComposer.AppendLine(
                codeComposer1.GenerateText(
                    new Dictionary<string, string>()
                    {
                        { "mv-class-name", mvClassName },
                        { "kv-class-name", kvClassName },
                        { "kv-arg-code", kvArgCode },
                        { "kv-scalar-arg-code", kvScalarArgCode }
                    }
                )
            ).AppendLine();
        }

        codeComposer1 = new ParametricTextComposer(
            "#",
            "#",
            @"
[MethodImpl(MethodImplOptions.AggressiveInlining)]
public static #mv-class-name# Create(#mv-class-name# mv, double scalar)
{
    return #mv-class-name#.Create(
        #kv-arg-code#
    );
}
".Trim()
        );

        var kvArgCode2 = 
            Specs
                .GradeCount
                .GetRange(
                    grade => grade == 0
                        ? $"new {ScalarType.ClassName} {{ Scalar = mv.KVector0.Scalar + scalar }}"
                        : $"mv.KVector{grade}"
                ).Concatenate("," + Environment.NewLine);

        codeComposer.AppendLine(
            codeComposer1.GenerateText(
                new Dictionary<string, string>()
                {
                    { "mv-class-name", mvClassName },
                    { "kv-arg-code", kvArgCode2 }
                }
            )
        ).AppendLine();

        codeComposer1 = new ParametricTextComposer(
            "#",
            "#",
            @"
[MethodImpl(MethodImplOptions.AggressiveInlining)]
public static #mv-class-name# Create(#arg-code#)
{
    return new #mv-class-name#(
        #kv-arg-code#
    );
}

public static #mv-class-name# Create(params double[] scalarArray)
{
    #kv-from-array-code#

    return new #mv-class-name#(
        #kv-arg-code#
    );
}
".Trim()
        );

        var argCode = Specs.GradeCount.GetRange(
            grade => $"{KVectorTypes[grade].ClassName} kVector{grade}"
        ).Concatenate(", ");

        var kvFromArrayCodeComposer = new LinearTextComposer();

        for (var grade = 0; grade <= VSpaceDimensions; grade++)
        {
            var kvClassName =
                KVectorTypes[grade].ClassName;

            var rhsCode =
                KVectorTypes[grade].GetBasisBladeIDs()
                    .Select(id => $"    Scalar{GradedMultivectorLibUtils.GetBasisBladeCode(id)} = scalarArray[{id}]")
                    .Concatenate("," + Environment.NewLine);

            kvFromArrayCodeComposer
                .AppendLineAtNewLine($"var kVector{grade} = new {kvClassName}()")
                .AppendLine("{")
                .AppendLine(rhsCode)
                .AppendLine("};")
                .AppendLine();
        }

        var kvFromArrayCode =
            kvFromArrayCodeComposer.ToString();

        var kvArgCode1 = Specs.GradeCount.GetRange(
            grade => $"kVector{grade}"
        ).Concatenate("," + Environment.NewLine);

        codeComposer.AppendLine(
            codeComposer1.GenerateText(
                new Dictionary<string, string>()
                {
                    { "mv-class-name", mvClassName },
                    { "arg-code", argCode },
                    { "kv-from-array-code", kvFromArrayCode },
                    { "kv-arg-code", kvArgCode1 }
                }
            )
        ).AppendLine();

        return codeComposer.ToString();
    }

    public string GetMultivectorStaticOperatorCode()
    {
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
                    {"class1-name", mvClassName}
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

        for (var grade1 = 0; grade1 < VSpaceDimensions; grade1++)
        {
            var kv1ClassName = KVectorTypes[grade1].ClassName;

            codeComposer.AppendLine(
                codeComposer1.GenerateText(
                    new Dictionary<string, string>()
                    {
                        {"class1-name", mvClassName},
                        {"class2-name", kv1ClassName},
                        {"out-class-name", mvClassName}
                    }
                )
            );
        }

        codeComposer.AppendLine(
            codeComposer1.GenerateText(
                new Dictionary<string, string>()
                {
                    {"class1-name", mvClassName},
                    {"class2-name", mvClassName},
                    {"out-class-name", mvClassName}
                }
            )
        );

        {
            codeComposer1 = new ParametricTextComposer(
                "#",
                "#",
                @"
[MethodImpl(MethodImplOptions.AggressiveInlining)]
public static #class-name# operator +(#class-name# mv1, double mv2)
{
    return #add1-double-code#;
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public static #class-name# operator +(double mv1, #class-name# mv2)
{
    return #add2-double-code#;
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public static #class-name# operator -(#class-name# mv1, double mv2)
{
    return #subtract1-double-code#;
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public static #class-name# operator -(double mv1, #class-name# mv2)
{
    return #subtract2-double-code#;
}
".Trim()
            );

            var add1DoubleCode = $"{mvClassName}.Create(mv1, mv2)";
            var add2DoubleCode = $"{mvClassName}.Create(mv2, mv1)";
            var subtract1DoubleCode = $"{mvClassName}.Create(mv1, -mv2)";
            var subtract2DoubleCode = $"{mvClassName}.Create(mv2.Negative(), mv1)";

            codeComposer.AppendLine(
                codeComposer1.GenerateText(
                    new Dictionary<string, string>()
                    {
                        { "class-name", mvClassName },
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

    public string GetMultivectorPropertyCode()
    {
        var codeComposer = new LinearTextComposer();

        for (var grade = 0; grade <= VSpaceDimensions; grade++)
        {
            var kvClassName = KVectorTypes[grade].ClassName;

            codeComposer
                .AppendLineAtNewLine($"public {kvClassName} KVector{grade} {{ get; }}")
                .AppendLine();
        }

        for (var grade = 0; grade <= VSpaceDimensions; grade++)
        {
            //var kvClassName = KVectorTypes[grade].ClassName;

            foreach (var id in KVectorTypes[grade].GetBasisBladeIDs())
            {
                var idText = GradedMultivectorLibUtils.GetBasisBladeCode(id);

                codeComposer
                    .AppendLineAtNewLine($"public double Scalar{idText} => KVector{grade}.Scalar{idText};")
                    .AppendLine();

            }
        }

        return codeComposer.ToString();
    }

    public string GetMultivectorConstructorCode()
    {
        var codeComposer = new LinearTextComposer();

        var className = MultivectorType.ClassName;

        var argCode = Specs.GradeCount.GetRange(
            grade => $"{KVectorTypes[grade].ClassName} kVector{grade}"
        ).Concatenate(", ");

        var assignCode = Specs.GradeCount.GetRange(
            grade => $"    KVector{grade} = kVector{grade};"
        ).Concatenate(Environment.NewLine);

        codeComposer.AppendLine(@$"
[MethodImpl(MethodImplOptions.AggressiveInlining)]
private {className}({argCode})
{{
{assignCode}
}}
".Trim()
        );

        return codeComposer.ToString();
    }

    public string GetMultivectorIsValidCode()
    {
        var kVectorsCode =
            Specs
                .GradeCount
                .GetRange(grade => $"KVector{grade}.IsValid()")
                .Concatenate(" &&" + Environment.NewLine);

        var codeComposer = new ParametricTextComposer(
            "#",
            "#",
            @"
[MethodImpl(MethodImplOptions.AggressiveInlining)]
public bool IsValid()
{
    return
        #kvectors_code#;
}
".Trim()
        );

        return codeComposer.GenerateText(
            new Dictionary<string, string>()
            {
                    {"kvectors_code", kVectorsCode}
            }
        );
    }

    public string GetMultivectorIsZeroCode()
    {
        var kVectorsCode =
            Specs
                .GradeCount
                .GetRange(grade => $"KVector{grade}.IsZero()")
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
        #kvectors_code#;

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
                    {"kvectors_code", kVectorsCode}
            }
        );
    }

    public string GetMultivectorToArrayCode()
    {
        var mvArrayCode =
            GaSpaceDimensions.GetRange(
                id => $"Scalar{GradedMultivectorLibUtils.GetBasisBladeCode(id)}"
            ).Concatenate("," + Environment.NewLine);

        var kvArraysCode =
            Specs.GradeCount.GetRange(
                grade => $"KVector{grade}.GetKVectorArray()"
            ).Concatenate("," + Environment.NewLine);

        var codeComposer = new ParametricTextComposer(
            "#",
            "#",
            @"
[MethodImpl(MethodImplOptions.AggressiveInlining)]
public double[] GetMultivectorArray()
{
    return new double[]
    {
        #mv-array-code#
    };
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public double[][] GetKVectorArrays()
{
    return new double[][]
    {
        #kv-arrays-code#
    };
}
".Trim()
        );

        return codeComposer.GenerateText(
            new Dictionary<string, string>
            {
                    {"mv-array-code", mvArrayCode},
                    {"kv-arrays-code", kvArraysCode}
            }
        );
    }

    public string GetMultivectorUnaryOperationsCode()
    {
        var className = MultivectorType.ClassName;

        var timesKVectorsCode =
            Specs
                .GradeCount
                .GetRange(grade =>
                    $"KVector{grade} * mv2"
                ).Concatenate("," + Environment.NewLine);

        var negativeKVectorsCode =
            Specs
                .GradeCount
                .GetRange(grade =>
                    $"KVector{grade}.Negative()"
                ).Concatenate("," + Environment.NewLine);

        var reverseKVectorsCode =
            Specs
                .GradeCount
                .GetRange(grade =>
                    grade.ReverseIsNegativeOfGrade()
                        ? $"KVector{grade}.Negative()"
                        : $"KVector{grade}"
                ).Concatenate("," + Environment.NewLine);

        var gradeInvolutionKVectorsCode =
            Specs
                .GradeCount
                .GetRange(grade =>
                    grade.GradeInvolutionIsNegativeOfGrade()
                        ? $"KVector{grade}.Negative()"
                        : $"KVector{grade}"
                ).Concatenate("," + Environment.NewLine);

        var cliffordConjugateKVectorsCode =
            Specs
                .GradeCount
                .GetRange(grade =>
                    grade.CliffordConjugateIsNegativeOfGrade()
                        ? $"KVector{grade}.Negative()"
                        : $"KVector{grade}"
                ).Concatenate("," + Environment.NewLine);

        var conjugateKVectorsCode =
            Specs
                .GradeCount
                .GetRange(grade =>
                    $"KVector{grade}.Conjugate()"
                ).Concatenate("," + Environment.NewLine);

        var codeComposer = new ParametricTextComposer(
            "#",
            "#",
            @"
[MethodImpl(MethodImplOptions.AggressiveInlining)]
public #class-name# Times(double mv2)
{
    return new #class-name#(
        #times-kvectors-code#
    );
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
    return new #class-name#(
        #negative-kvectors-code#
    );
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public #class-name# Reverse()
{
    return new #class-name#(
        #reverse-kvectors-code#
    );
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public #class-name# GradeInvolution()
{
    return new #class-name#(
        #grade-involution-kvectors-code#
    );
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public #class-name# CliffordConjugate()
{
    return new #class-name#(
        #clifford-conjugate-kvectors-code#
    );
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public #class-name# Inverse()
{
    var mvReverse = Reverse();

    return mvReverse.Times(
        1d / mvReverse.Sp(this).Scalar
    );
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public #class-name# InverseTimes(double mv2)
{
    var mvReverse = Reverse();

    return mvReverse.Times(
        mv2 / mvReverse.Sp(this).Scalar
    );
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

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public #class-name# Conjugate()
{
    return new #class-name#(
        #conjugate-kvectors-code#
    );
}
".Trim()
        );

        return codeComposer.GenerateText(
            new Dictionary<string, string>()
            {
                    { "class-name", className },
                    { "times-kvectors-code", timesKVectorsCode },
                    { "negative-kvectors-code", negativeKVectorsCode },
                    { "reverse-kvectors-code", reverseKVectorsCode },
                    { "grade-involution-kvectors-code", gradeInvolutionKVectorsCode },
                    { "clifford-conjugate-kvectors-code", cliffordConjugateKVectorsCode },
                    { "conjugate-kvectors-code", conjugateKVectorsCode }
            }
        );
    }

    public string GetMultivectorDualCode()
    {
        var codeComposer = new LinearTextComposer();

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

            for (var grade2 = 0; grade2 <= VSpaceDimensions; grade2++)
            {
                var kv2ClassName = KVectorTypes[grade2].ClassName;
                var outClassName = Specs.GetLcpOutType(
                    MultivectorType,
                    KVectorTypes[grade2]
                ).ClassName;

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

    public string GetMultivectorAddSubtractCode()
    {
        var mvClassName = MultivectorType.ClassName;

        var codeComposer = new LinearTextComposer();

        for (var grade1 = 0; grade1 <= VSpaceDimensions; grade1++)
        {
            var kv1ClassName = KVectorTypes[grade1].ClassName;

            var g1 = grade1;
            var mvCode =
                Specs.GradeCount.GetRange(
                    g => g == g1
                        ? $"KVector{g}.Add(mv2)"
                        : $"KVector{g}"
                ).Concatenate("," + Environment.NewLine);

            codeComposer
                .AppendLineAtNewLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
                .AppendLine($"public {mvClassName} Add({kv1ClassName} mv2)")
                .AppendLine("{")
                .IncreaseIndentation()
                .AppendLine("if (mv2.IsZero()) return this;")
                .AppendLine($"if (IsZero()) return {mvClassName}.Create(mv2);")
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

        {
            var mvCode =
                Specs.GradeCount.GetRange(
                    g => $"KVector{g}.Add(mv2.KVector{g})"
                ).Concatenate("," + Environment.NewLine);

            codeComposer
                .AppendLineAtNewLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
                .AppendLine($"public {mvClassName} Add({mvClassName} mv2)")
                .AppendLine("{")
                .IncreaseIndentation()
                .AppendLine("if (mv2.IsZero()) return this;")
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

        for (var grade1 = 0; grade1 <= VSpaceDimensions; grade1++)
        {
            var kv1ClassName = KVectorTypes[grade1].ClassName;

            var g1 = grade1;
            var mvCode =
                Specs.GradeCount.GetRange(
                    g => g == g1
                        ? $"KVector{g}.Subtract(mv2)"
                        : $"KVector{g}"
                ).Concatenate("," + Environment.NewLine);

            codeComposer
                .AppendLineAtNewLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
                .AppendLine($"public {mvClassName} Subtract({kv1ClassName} mv2)")
                .AppendLine("{")
                .IncreaseIndentation()
                .AppendLine("if (mv2.IsZero()) return this;")
                .AppendLine($"if (IsZero()) return {mvClassName}.Create(mv2.Negative());")
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

        {
            var mvCode =
                Specs.GradeCount.GetRange(
                    g => $"KVector{g}.Subtract(mv2.KVector{g})"
                ).Concatenate("," + Environment.NewLine);

            codeComposer
                .AppendLineAtNewLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
                .AppendLine($"public {mvClassName} Subtract({mvClassName} mv2)")
                .AppendLine("{")
                .IncreaseIndentation()
                .AppendLine("if (mv2.IsZero()) return this;")
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
        var mvClassName =
            MultivectorType.ClassName;

        CodeFilesComposer.InitializeFile(mvClassName + ".cs");

        var codeComposer = CodeFilesComposer.ActiveFileTextComposer;

        codeComposer
            .AppendLine("using System.Runtime.CompilerServices;")
            .AppendLine()
            .AppendLine($"namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.{GaSpaceName};")
            .AppendLine()
            .AppendLine($"public sealed partial class {mvClassName}")
            .AppendLine("{")
            .IncreaseIndentation()
            .AppendLine(GetMultivectorStaticPropertyCode())
            .AppendLine()
            .AppendLine(GetMultivectorStaticFactoryCode())
            .AppendLine()
            .AppendLine(GetMultivectorStaticOperatorCode())
            .AppendLine()
            .AppendLine(GetMultivectorPropertyCode())
            .AppendLine()
            .AppendLine(GetMultivectorConstructorCode())
            .AppendLine()
            .AppendLine(GetMultivectorIsValidCode())
            .AppendLine()
            .AppendLine(GetMultivectorIsZeroCode())
            .AppendLine()
            .AppendLine(GetMultivectorToArrayCode())
            .AppendLine()
            .AppendLine(GetMultivectorUnaryOperationsCode())
            .AppendLine()
            .AppendLine(GetMultivectorDualCode())
            .AppendLine()
            .AppendLine(GetMultivectorAddSubtractCode())
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