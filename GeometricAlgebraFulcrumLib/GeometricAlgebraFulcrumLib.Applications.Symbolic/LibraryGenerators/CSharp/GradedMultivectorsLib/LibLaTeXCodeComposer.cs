using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.CSharp.GradedMultivectorsLib.Combinations;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Files;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Parametric;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.CSharp.GradedMultivectorsLib;

public class LibLaTeXCodeComposer :
    LibSubCodeComposer
{
    public static LibLaTeXCodeComposer Create(
        LibCodeComposerSpecs specs,
        TextFilesComposer codeFilesComposer
    )
    {
        return new LibLaTeXCodeComposer(specs, codeFilesComposer);
    }


    public string BasisSymbol
        => Specs.LaTeXBasisSymbol;


    private LibLaTeXCodeComposer(LibCodeComposerSpecs specs, TextFilesComposer codeFilesComposer)
        : base(specs, codeFilesComposer)
    {
    }

    
    public IReadOnlyList<string> GetBasisVectorSubscripts()
    {
        return Specs
            .VSpaceDimensions
            .GetRange(i => (i + 1).ToString())
            .ToImmutableArray();
    }

    public IReadOnlyList<string> GetBasisVectorCGaSubscripts()
    {
        var textArray = new string[VSpaceDimensions];
        
        for (var i = 0; i < VSpaceDimensions - 2; i++)
            textArray[i] = (i + 1).ToString();

        textArray[VSpaceDimensions - 2] = "o";
        textArray[VSpaceDimensions - 1] = @"\infty";

        return textArray;
    }

    public IReadOnlyList<string> GetBasisVectorPGaSubscripts()
    {
        return Specs
            .VSpaceDimensions
            .GetRange(i => i.ToString())
            .ToImmutableArray();
    }

    private LibUnilinearCombination GetCGaBasisMap()
    {
        // If linearly independent basis F = <f1, f2, f3> is related to
        // orthonormal basis E = <e1, e2, e3> via matrix M (F = M E), then
        // the scalars of a multivector expressed using E (Ae) are related
        // to the scalars of the same multivectors expressed using basis F
        // (Af) using the inverse transpose of M: Af = inv(transpose(M)) Ae
        // Thus if M is an orthogonal matrix (or as a special case, a permutation)
        // they are related using M itself: Af = M Ae.

        var vectorMapArray = new double[VSpaceDimensions, VSpaceDimensions];

        for (var i = 0; i < VSpaceDimensions - 2; i++)
            vectorMapArray[i, i + 2] = 1d;

        vectorMapArray[VSpaceDimensions - 2, 0] = 1d;
        vectorMapArray[VSpaceDimensions - 1, 0] = 0.5d;

        vectorMapArray[VSpaceDimensions - 2, 1] = 1d;
        vectorMapArray[VSpaceDimensions - 1, 1] = -0.5d;

        return LibUnilinearCombination.CreateFromOutermorphism(
            MultivectorType,
            vectorMapArray
        );
    }

    private string GetKVectorToLaTeXBasisCode(LibUnilinearCombination cgaBasisMap, int grade)
    {
        var termTable = 
            cgaBasisMap.FilterTerms(
                KVectorTypes[grade],
                KVectorTypes[grade],
                term => term.OutputBasisBladeGrade == grade
            );

        return termTable
            .GetIdTermListPairs()
            .Select(p =>
                {
                    var index = (int)((IndexSet)p.Key).BasisBladeIdToIndex();

                    var lhsCode = $"scalarArray[{index}]";

                    var rhsCode = p.Value.GetRhsCode(
                        idCode => $"mv.Scalar{idCode}"
                    );

                    return $"{lhsCode} = {rhsCode};";
                }
            ).Concatenate(Environment.NewLine);
    }
    
    private string GetMultivectorToLaTeXBasisCode(LibUnilinearCombination cgaBasisMap)
    {
        return cgaBasisMap
            .GetIdTermListPairs()
            .Select(p =>
                {
                    var id = p.Key;

                    var lhsCode = $"scalarArray[{id}]";

                    var rhsCode = p.Value.GetRhsCode(
                        idCode => $"mv.Scalar{idCode}"
                    );

                    return $"{lhsCode} = {rhsCode};";
                }
            ).Concatenate(Environment.NewLine);
    }

    private string GetConcatenateCode()
    {
        return @"
private static string Concatenate(this IEnumerable<string> items, string itemSeparator)
{
    var s = new StringBuilder();

    var flag = false;
    foreach (var item in items)
    {
        if (flag)
            s.Append(itemSeparator);
        else
            flag = true;

        s.Append(item);
    }

    return s.ToString();
}
".Trim();
    }

    private string GetToLaTeXCode(string spaceName, IReadOnlyList<string> basisVectorSubscripts)
    {
        var codeComposer = new LinearTextComposer();
        
        var toLaTeXCodeComposer = new ParametricTextComposer(
            "#",
            "#",
            @"
public static string To#space-name#LaTeX(this #class-name# mv)
{
    if (mv.IsZero()) return ""0"";
    
    var termList = new List<string>(#size#);

    #scalar-code#

    return termList.Count == 1
        ? termList[0]
        : termList.Concatenate("" + "");
}
".Trim()
        );
        
        foreach (var inKvType in KVectorTypes)
        {
            var grade = inKvType.Grade;
            var className = inKvType.ClassName;
            var size = inKvType.KvSpaceDimensions;
            var scalarCodeList = new List<string>(size);

            for (var index = 0; index < size; index++)
            {
                var scalarName =
                    GradedMultivectorLibUtils.GetBasisBladeScalarName(grade, index);

                var subscriptCode =
                    BasisBladeUtils
                        .BasisBladeGradeIndexToBasisVectorIndices((uint)grade, (ulong)index)
                        .Select(i => basisVectorSubscripts[(int)i])
                        .Concatenate(",");

                scalarCodeList.Add(
                    $@"if (mv.{scalarName} != 0d) termList.Add(@$""({{mv.{scalarName}:G}}) \boldsymbol{{{{{BasisSymbol}}}}}_{{{{{subscriptCode}}}}}"");"
                );
            }

            var scalarCode =
                scalarCodeList.Concatenate(Environment.NewLine);

            var toLaTeXCode = toLaTeXCodeComposer.GenerateText(
                new Dictionary<string, string>()
                {
                    {"space-name", spaceName},
                    {"class-name", className},
                    {"size", size.ToString()},
                    {"scalar-code", scalarCode}
                }
            );

            codeComposer
                .AppendLineAtNewLine(toLaTeXCode)
                .AppendLine();
        }

        {
            var className = MultivectorType.ClassName;
            var size = GaSpaceDimensions;
            var scalarCodeList = new List<string>(size);

            for (var grade = 0; grade <= VSpaceDimensions; grade++)
            {
                var kvSpaceDimensions =
                    (int)VSpaceDimensions.GetBinomialCoefficient(grade);

                for (var index = 0; index < kvSpaceDimensions; index++)
                {
                    var scalarName =
                        GradedMultivectorLibUtils.GetBasisBladeScalarName(grade, index);
                    
                    var subscriptCode =
                        BasisBladeUtils
                            .BasisBladeGradeIndexToBasisVectorIndices((uint)grade, (ulong)index)
                            .Select(i => basisVectorSubscripts[(int)i])
                            .Concatenate(",");

                    scalarCodeList.Add(
                        $@"if (mv.{scalarName} != 0d) termList.Add(@$""({{mv.{scalarName}:G}}) \boldsymbol{{{{{BasisSymbol}}}}}_{{{{{subscriptCode}}}}}"");"
                    );
                }
            }

            var scalarCode =
                scalarCodeList.Concatenate(Environment.NewLine);

            var toLaTeXCode = toLaTeXCodeComposer.GenerateText(
                new Dictionary<string, string>()
                {
                    {"space-name", spaceName},
                    {"class-name", className},
                    {"size", size.ToString()},
                    {"scalar-code", scalarCode}
                }
            );

            codeComposer.AppendLineAtNewLine(toLaTeXCode);
        }

        return codeComposer.ToString();
    }

    private string GetToCGaLaTeXCode()
    {
        if (!(Specs.VSpaceDimensions >= 4 && 
              Specs.Metric.NegativeSignatureBasisCount == 1 &&
              Specs.Metric.ZeroSignatureBasisCount == 0))
            return string.Empty;

        var codeComposer = new LinearTextComposer();
        
        var toLaTeXCodeComposer = new ParametricTextComposer(
            "#",
            "#",
            @"
private static double[] ToCGaBasis(this #class-name# mv)
{
    var scalarArray = new double[#size#];

    #to-latex-basis-code#

    return scalarArray;
}

public static string ToCGaLaTeX(this #class-name# mv)
{
    if (mv.IsZero()) return ""0"";
    
    var scalarArray = mv.ToCGaBasis();
    var termList = new List<string>(#size#);

    #scalar-code#

    return termList.Count == 1
        ? termList[0]
        : termList.Concatenate("" + "");
}
".Trim()
        );

        var cgaBasisMap = GetCGaBasisMap();
        var cgaBasisSubscripts = GetBasisVectorCGaSubscripts();

        foreach (var inKvType in KVectorTypes)
        {
            var grade = inKvType.Grade;
            var className = inKvType.ClassName;
            var size = inKvType.KvSpaceDimensions;
            var scalarCodeList = new List<string>(size);

            for (var index = 0; index < size; index++)
            {
                var subscriptCode =
                    BasisBladeUtils
                        .BasisBladeGradeIndexToBasisVectorIndices((uint)grade, (ulong)index)
                        .Select(i => cgaBasisSubscripts[(int)i])
                        .Concatenate(",");

                scalarCodeList.Add(
                    $@"if (scalarArray[{index}] != 0d) termList.Add(@$""({{scalarArray[{index}]:G}}) \boldsymbol{{{{{BasisSymbol}}}}}_{{{{{subscriptCode}}}}}"");"
                );
            }

            var scalarCode =
                scalarCodeList.Concatenate(Environment.NewLine);

            var toLaTeXCode = toLaTeXCodeComposer.GenerateText(
                new Dictionary<string, string>()
                {
                    {"to-latex-basis-code", GetKVectorToLaTeXBasisCode(cgaBasisMap, grade)},
                    {"class-name", className},
                    {"size", size.ToString()},
                    {"scalar-code", scalarCode}
                }
            );

            codeComposer
                .AppendLineAtNewLine(toLaTeXCode)
                .AppendLine();
        }

        {
            var className = MultivectorType.ClassName;
            var size = GaSpaceDimensions;
            var scalarCodeList = new List<string>(size);

            for (var grade = 0; grade <= VSpaceDimensions; grade++)
            {
                var kvSpaceDimensions =
                    (int)VSpaceDimensions.GetBinomialCoefficient(grade);

                for (var index = 0; index < kvSpaceDimensions; index++)
                {
                    var id =
                        BasisBladeUtils.BasisBladeGradeIndexToId(grade, (ulong)index);

                    var subscriptCode =
                        BasisBladeUtils
                            .BasisBladeGradeIndexToBasisVectorIndices((uint)grade, (ulong)index)
                            .Select(i => cgaBasisSubscripts[(int)i])
                            .Concatenate(",");

                    scalarCodeList.Add(
                        $@"if (scalarArray[{id}] != 0d) termList.Add(@$""({{scalarArray[{id}]:G}}) \boldsymbol{{{{{BasisSymbol}}}}}_{{{{{subscriptCode}}}}}"");"
                    );
                }
            }

            var scalarCode =
                scalarCodeList.Concatenate(Environment.NewLine);

            var toLaTeXCode = toLaTeXCodeComposer.GenerateText(
                new Dictionary<string, string>()
                {
                    {"to-latex-basis-code", GetMultivectorToLaTeXBasisCode(cgaBasisMap)},
                    {"class-name", className},
                    {"size", size.ToString()},
                    {"scalar-code", scalarCode}
                }
            );

            codeComposer.AppendLineAtNewLine(toLaTeXCode);
        }

        return codeComposer.ToString();
    }


    public override TextFilesComposer GenerateCode()
    {
        CodeFilesComposer.InitializeFile(GaSpaceName + "LaTeX.cs");

        var codeComposer = CodeFilesComposer.ActiveFileTextComposer;

        codeComposer
            .AppendLine("using System.Text;")
            .AppendLine("using System.Runtime.CompilerServices;")
            .AppendLine()
            .AppendLine($"namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.{GaSpaceName};")
            .AppendLine()
            .AppendLine($"public static class {GaSpaceName}LaTeX")
            .AppendLine("{")
            .IncreaseIndentation()
            .AppendLine(GetConcatenateCode())
            .AppendLine()
            .AppendLine(GetToLaTeXCode(string.Empty, GetBasisVectorSubscripts()))
            .AppendLine()
            .AppendLine(GetToLaTeXCode("PGa", GetBasisVectorPGaSubscripts()))
            .AppendLine()
            .AppendLine(GetToCGaLaTeXCode())
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