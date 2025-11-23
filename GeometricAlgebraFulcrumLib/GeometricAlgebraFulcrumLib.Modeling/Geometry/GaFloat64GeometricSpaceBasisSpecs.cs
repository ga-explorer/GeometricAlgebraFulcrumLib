using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical;
using GeometricAlgebraFulcrumLib.Utilities.Structures;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Markdown;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Markdown.Tables;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry;

public sealed class GaFloat64GeometricSpaceBasisSpecs
{
    private static IEnumerable<string> GetCGaLaTeXVectorSubscripts(int vSpaceDimensions, bool orthogonalBasis = false)
    {
        if (orthogonalBasis)
        {
            yield return "-";
            yield return "+";

            for (var i = 0; i < vSpaceDimensions - 2; i++)
                yield return (i + 1).ToString();
        }
        else
        {
            yield return "o";

            for (var i = 0; i < vSpaceDimensions - 2; i++)
                yield return (i + 1).ToString();

            yield return @"\infty";
        }
    }

    private static IEnumerable<string> GetCGaTextVectorSubscripts(int vSpaceDimensions, bool orthogonalBasis = false)
    {
        if (orthogonalBasis)
        {
            yield return "-";
            yield return "+";

            for (var i = 0; i < vSpaceDimensions - 2; i++)
                yield return (i + 1).ToString();
        }
        else
        {
            yield return "o";

            for (var i = 0; i < vSpaceDimensions - 2; i++)
                yield return (i + 1).ToString();

            yield return @"∞";
        }
    }

    private static XGaFloat64LinearMapOutermorphism GetCGaBasisMap(int vSpaceDimensions)
    {
        // If linearly independent basis F = <f1, f2, f3> is related to
        // orthonormal basis E = <e1, e2, e3> via matrix M (F = M E), then
        // the scalars of a multivector expressed using E (Ae) are related
        // to the scalars of the same multivectors expressed using basis F
        // (Af) using the inverse transpose of M: Af = inv(transpose(M)) Ae
        // Thus if M is an orthogonal matrix (or as a special case, a permutation)
        // they are related using M itself: Af = M Ae.

        var processor = XGaFloat64ConformalProcessor.Instance;

        var vectorMapArray = new double[vSpaceDimensions, vSpaceDimensions];

        for (var i = 0; i < vSpaceDimensions - 2; i++)
            vectorMapArray[i + 1, i + 2] = 1d;

        vectorMapArray[0, 0] = 1d;
        vectorMapArray[0, 1] = 1d;

        vectorMapArray[vSpaceDimensions - 1, 0] = 0.5d;
        vectorMapArray[vSpaceDimensions - 1, 1] = -0.5d;

        return vectorMapArray.ToMathNetDenseMatrix().ColumnsToOutermorphism(processor);
    }

    private static XGaFloat64LinearMapOutermorphism GetCGaBasisMapInverse(int vSpaceDimensions)
    {
        // If linearly independent basis F = <f1, f2, f3> is related to
        // orthonormal basis E = <e1, e2, e3> via matrix M (F = M E), then
        // the scalars of a multivector expressed using E (Ae) are related
        // to the scalars of the same multivectors expressed using basis F
        // (Af) using the inverse transpose of M: Af = inv(transpose(M)) Ae
        // Thus if M is an orthogonal matrix (or as a special case, a permutation)
        // they are related using M itself: Af = M Ae.

        var processor = XGaFloat64ConformalProcessor.Instance;

        var vectorMapArray = new double[vSpaceDimensions, vSpaceDimensions];

        for (var i = 0; i < vSpaceDimensions - 2; i++)
            vectorMapArray[i + 2, i + 1] = 1d;

        vectorMapArray[0, 0] = 0.5d;
        vectorMapArray[1, 0] = 0.5d;

        vectorMapArray[0, vSpaceDimensions - 1] = 1d;
        vectorMapArray[1, vSpaceDimensions - 1] = -1d;

        return vectorMapArray.ToMathNetDenseMatrix().ColumnsToOutermorphism(processor);
    }


    private static readonly Dictionary<int, GaFloat64GeometricSpaceBasisSpecs> VGaCache =
        new Dictionary<int, GaFloat64GeometricSpaceBasisSpecs>();
    
    private static readonly Dictionary<int, GaFloat64GeometricSpaceBasisSpecs> CGaCache =
        new Dictionary<int, GaFloat64GeometricSpaceBasisSpecs>();
    
    private static readonly Dictionary<int, GaFloat64GeometricSpaceBasisSpecs> CGaOrthoCache =
        new Dictionary<int, GaFloat64GeometricSpaceBasisSpecs>();

    public static GaFloat64GeometricSpaceBasisSpecs CreateVGa(int vSpaceDimensions)
    {
        if (vSpaceDimensions < 2)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        if (VGaCache.TryGetValue(vSpaceDimensions, out var specs))
            return specs;

        var vectorSubscripts =
            vSpaceDimensions
                .GetRange(i => (i + 1).ToString())
                .ToImmutableArray();
        
        specs = new GaFloat64GeometricSpaceBasisSpecs(
            XGaFloat64EuclideanProcessor.Instance,
            vectorSubscripts,
            vectorSubscripts
        );

        VGaCache.Add(vSpaceDimensions, specs);

        return specs;
    }

    public static GaFloat64GeometricSpaceBasisSpecs CreateCGa(int vSpaceDimensions)
    {
        if (vSpaceDimensions < 4)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));
        
        if (CGaCache.TryGetValue(vSpaceDimensions, out var specs))
            return specs;

        var laTeXVectorSubscripts =
            GetCGaLaTeXVectorSubscripts(vSpaceDimensions).ToImmutableArray();
        
        var textVectorSubscripts =
            GetCGaTextVectorSubscripts(vSpaceDimensions).ToImmutableArray();

        var basisMap = GetCGaBasisMap(vSpaceDimensions);
        var basisMapInverse = GetCGaBasisMapInverse(vSpaceDimensions);

        specs = new GaFloat64GeometricSpaceBasisSpecs(
            XGaFloat64ConformalProcessor.Instance,
            laTeXVectorSubscripts,
            textVectorSubscripts,
            basisMap,
            basisMapInverse
        );
        
        CGaCache.Add(vSpaceDimensions, specs);

        return specs;
    }

    public static GaFloat64GeometricSpaceBasisSpecs CreateCGaOrthogonal(int vSpaceDimensions)
    {
        if (vSpaceDimensions < 4)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));
        
        if (CGaOrthoCache.TryGetValue(vSpaceDimensions, out var specs))
            return specs;

        var laTeXVectorSubscripts =
            GetCGaLaTeXVectorSubscripts(
                vSpaceDimensions,
                true
            ).ToImmutableArray();
        
        var textVectorSubscripts =
            GetCGaTextVectorSubscripts(
                vSpaceDimensions,
                true
            ).ToImmutableArray();

        specs = new GaFloat64GeometricSpaceBasisSpecs(
            XGaFloat64ConformalProcessor.Instance,
            laTeXVectorSubscripts,
            textVectorSubscripts
        );
        
        CGaOrthoCache.Add(vSpaceDimensions, specs);

        return specs;
    }


    public XGaFloat64Processor Processor { get; }

    public XGaFloat64EuclideanProcessor EuclideanProcessor
        => XGaFloat64EuclideanProcessor.Instance;

    public IReadOnlyList<string> LaTeXVectorSubscripts { get; }

    public IReadOnlyList<string> TextVectorSubscripts { get; }

    public IXGaFloat64Outermorphism BasisMap { get; }

    public IXGaFloat64Outermorphism BasisMapInverse { get; }

    public int VSpaceDimensions
        => LaTeXVectorSubscripts.Count;

    public ulong GaSpaceDimensions
        => 1ul << VSpaceDimensions;


    private GaFloat64GeometricSpaceBasisSpecs(XGaFloat64Processor processor, IReadOnlyList<string> laTeXVectorSubscripts, IReadOnlyList<string> textVectorSubscripts)
    {
        Processor = processor;
        LaTeXVectorSubscripts = laTeXVectorSubscripts;
        TextVectorSubscripts = textVectorSubscripts;
        BasisMap = new XGaFloat64IdentityOutermorphism(processor);
        BasisMapInverse = BasisMap;
    }

    private GaFloat64GeometricSpaceBasisSpecs(XGaFloat64Processor processor, IReadOnlyList<string> laTeXVectorSubscripts, IReadOnlyList<string> textVectorSubscripts, IXGaFloat64Outermorphism basisMap, IXGaFloat64Outermorphism basisMapInverse)
    {
        Processor = processor;
        LaTeXVectorSubscripts = laTeXVectorSubscripts;
        TextVectorSubscripts = textVectorSubscripts;
        BasisMap = basisMap;
        BasisMapInverse = basisMapInverse;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<XGaFloat64KVector> GetBasisBlades(bool orderByGrade = true)
    {
        if (orderByGrade)
            return GaSpaceDimensions
                .GetRange()
                .OrderBy(id => id.Grade())
                .ThenBy(id => id)
                .Select(id => BasisMapInverse.OmMapBasisBlade((IndexSet)id));

        return GaSpaceDimensions
            .GetRange()
            .Select(id => BasisMapInverse.OmMapBasisBlade((IndexSet)id));
    }


    private static string ScalarToLaTeX(double scalar)
    {
        Debug.Assert(scalar != 0);

        if (scalar == 1)
            return " +";

        if (scalar == -1)
            return " -";

        var scalarText =
            Math.Abs(scalar).ToString("G8");

        var eIndex =
            scalarText.IndexOf(
                'E',
                StringComparison.InvariantCultureIgnoreCase
            );

        if (eIndex < 0)
            return scalar > 0
                ? " + " + scalarText
                : " - " + scalarText;

        var scalarTextPart1 =
            scalarText[..eIndex];

        var scalarTextPart2 =
            scalarText[(eIndex + 1)..];

        scalarText = @$"{scalarTextPart1}\times10^{{{scalarTextPart2}}}";

        return scalar > 0
            ? " + " + scalarText
            : " - " + scalarText;
    }
    
    private static string ScalarTermToLaTeX(double scalar)
    {
        Debug.Assert(scalar != 0);

        var scalarText =
            Math.Abs(scalar).ToString("G8");

        var eIndex =
            scalarText.IndexOf(
                'E',
                StringComparison.InvariantCultureIgnoreCase
            );

        if (eIndex < 0)
            return scalar > 0
                ? " + " + scalarText
                : " - " + scalarText;

        var scalarTextPart1 =
            scalarText[..eIndex];

        var scalarTextPart2 =
            scalarText[(eIndex + 1)..];

        scalarText = @$"{scalarTextPart1}\times10^{{{scalarTextPart2}}}";

        return scalar > 0
            ? " + " + scalarText
            : " - " + scalarText;
    }
    
    private string BasisBladeToLaTeX(IndexSet id)
    {
        Debug.Assert(!id.IsEmptySet && id.ToUInt64() < GaSpaceDimensions);

        var subscriptText =
            id
                .Select(i => LaTeXVectorSubscripts[i])
                .ConcatenateText(",");

        return @$"\boldsymbol{{e}}_{{{subscriptText}}}";
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private string TermToLaTeX(IndexSet id, double scalar)
    {
        return id.IsEmptySet
            ? ScalarTermToLaTeX(scalar)
            : $"{ScalarToLaTeX(scalar)} {BasisBladeToLaTeX(id)}";
    }
    
    
    private static string ScalarToText(double scalar)
    {
        Debug.Assert(scalar != 0);

        if (scalar == 1)
            return " +";

        if (scalar == -1)
            return " -";

        var scalarText =
            Math.Abs(scalar).ToString("G8");

        var eIndex =
            scalarText.IndexOf(
                'E',
                StringComparison.InvariantCultureIgnoreCase
            );

        if (eIndex < 0)
            return scalar > 0
                ? " + " + scalarText
                : " - " + scalarText;

        var scalarTextPart1 =
            scalarText[..eIndex];

        var scalarTextPart2 =
            scalarText[(eIndex + 1)..];

        scalarText = $"{scalarTextPart1} * 10^{scalarTextPart2}";

        return scalar > 0
            ? " + " + scalarText
            : " - " + scalarText;
    }

    private static string ScalarTermToText(double scalar)
    {
        Debug.Assert(scalar != 0);

        var scalarText =
            Math.Abs(scalar).ToString("G8");

        var eIndex =
            scalarText.IndexOf(
                'E',
                StringComparison.InvariantCultureIgnoreCase
            );

        if (eIndex < 0)
            return scalar > 0
                ? " + " + scalarText + "<>"
                : " - " + scalarText + "<>";

        var scalarTextPart1 =
            scalarText[..eIndex];

        var scalarTextPart2 =
            scalarText[(eIndex + 1)..];

        scalarText = $"{scalarTextPart1} * 10^{scalarTextPart2}";

        return scalar > 0
            ? " + " + scalarText + "<>"
            : " - " + scalarText + "<>";
    }

    private string BasisBladeToText(IndexSet id)
    {
        Debug.Assert(!id.IsEmptySet && id.ToUInt64() < GaSpaceDimensions);

        var subscriptText =
            id
                .Select(i => TextVectorSubscripts[i])
                .ConcatenateText(", ");

        return $"<{subscriptText}>";
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private string TermToText(IndexSet id, double scalar)
    {
        return id.IsEmptySet
            ? ScalarTermToText(scalar)
            : $"{ScalarToText(scalar)} {BasisBladeToText(id)}";
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToLaTeX(double mv)
    {
        return ToLaTeX(
            (XGaFloat64Multivector)Processor.Scalar(mv)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToLaTeX(XGaFloat64Scalar mv)
    {
        return ToLaTeX(
            (XGaFloat64Multivector)mv
        );
    }

    public string ToLaTeX(XGaFloat64Multivector mv)
    {
        if (mv.IsZero)
            return "0";

        var latexText =
            BasisMap.OmMap(mv)
                .IdScalarPairs
                .OrderBy(p => p.Key.Grade())
                .ThenBy(p => p.Key)
                .Select(p => TermToLaTeX(p.Key, p.Value))
                .ConcatenateText();

        latexText = latexText[1] == '+'
            ? latexText[2..]
            : "-" + latexText[2..];

        return latexText.Trim();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToText(double mv)
    {
        return ToText(
            (XGaFloat64Multivector)Processor.Scalar(mv)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToText(XGaFloat64Scalar mv)
    {
        return ToText(
            (XGaFloat64Multivector)mv
        );
    }

    public string ToText(XGaFloat64Multivector mv)
    {
        if (mv.IsZero)
            return "0";

        var latexText =
            BasisMap.OmMap(mv)
                .IdScalarPairs
                .OrderBy(p => p.Key.Grade())
                .ThenBy(p => p.Key)
                .Select(p => TermToText(p.Key, p.Value))
                .ConcatenateText();

        latexText = latexText[1] == '+'
            ? latexText[2..]
            : "-" + latexText[2..];

        return latexText.Trim();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple<IReadOnlyList<XGaFloat64KVector>, XGaFloat64Multivector[,]> GetBasisMapTable(Func<XGaFloat64Multivector, XGaFloat64Multivector, XGaFloat64Multivector> basisMap)
    {
        var basisBladeList = 
            GetBasisBlades().ToImmutableArray();

        var mapArray = basisBladeList.GetMapTable(
            basisBladeList,
            basisMap
        );

        return new Tuple<IReadOnlyList<XGaFloat64KVector>, XGaFloat64Multivector[,]>(
            basisBladeList,
            mapArray
        );
    }

    public MarkdownTable GetBasisMapMarkdownTable(Func<XGaFloat64Multivector, XGaFloat64Multivector, XGaFloat64Multivector> basisMap)
    {
        var (basisBladeList, tableArray) = 
            GetBasisMapTable(basisMap);

        var n = basisBladeList.Count;

        var composer = new MarkdownTable();
        var columns = new MarkdownTableColumn[n + 1];

        columns[0] = composer.AddColumn(
            "basisBlade", 
            MarkdownTableColumnAlignment.Right, 
            string.Empty
        );
        
        for (var j = 0; j < n; j++)
        {
            columns[j + 1] = composer.AddColumn(
                $"basisBlade{j}", 
                MarkdownTableColumnAlignment.Center, 
                $"${ToLaTeX(basisBladeList[j])}$"
            );
        }

        columns[0].AddRange(
            basisBladeList.Select(
                b => $"${ToLaTeX(b)}$"
            )
        );

        for (var j = 0; j < n; j++)
        {
            var column = columns[j + 1];

            for (var i = 0; i < n; i++)
            {
                var itemText = ToLaTeX(tableArray[i, j]);

                column.Add(
                    itemText == "0" 
                        ? string.Empty 
                        : $"${itemText}$"
                );
            }
        }

        return composer;
    }

    public MarkdownTable GetBasisInfoMarkdownTable()
    {
        var indexList = 
            (1 << VSpaceDimensions)
                .GetRange()
                .ToImmutableArray();

        var basisBladeIdList =
            GaSpaceDimensions
                .GetRange()
                .OrderBy(id => id.Grade())
                .ThenBy(id => id)
                .SelectToImmutableArray(i => (IndexSet)i);

        var basisBladeList = 
            basisBladeIdList
                .Select(id => BasisMapInverse.OmMapBasisBlade(id))
                .ToImmutableArray();

        var n = basisBladeList.Length;

        var columnHeaders = new string[]
        {
            @"Basis $\boldsymbol{E}_{i}$",
            @"Grade $k$",
            @"Index $j$",
            @"$i=\Phi\left(k,j\right)$",
            @"$\mathrm{sgn}\left(\boldsymbol{E}_{i}^{\sim}\right)$"
        };

        var mappingFunctions = new Func<int, string>[]
        {
            i => $@"${ToLaTeX(basisBladeList[i])}$",
            i => basisBladeIdList[i].Grade().ToString(),
            i => basisBladeIdList[i].BasisBladeIdToIndex().ToString(),
            i => $@"${basisBladeIdList[i].ToUInt64().PatternToString(VSpaceDimensions)}_{{2}}={basisBladeIdList[i]}$",
            i => basisBladeIdList[i].ReverseIsPositiveOfBasisBladeId() ? "+" : "-"
        };

        var mdTable = indexList.MapToMarkdownTable(
            columnHeaders,
            mappingFunctions
        );

        mdTable[0].ColumnAlignment = MarkdownTableColumnAlignment.Left;

        return mdTable;
    }
}