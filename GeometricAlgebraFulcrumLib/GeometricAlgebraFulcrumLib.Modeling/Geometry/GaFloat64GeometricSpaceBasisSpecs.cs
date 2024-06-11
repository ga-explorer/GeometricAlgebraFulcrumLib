using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Utilities.Structures;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry;

public sealed class GaFloat64GeometricSpaceBasisSpecs
{
    private static IEnumerable<string> GetCGaVectorSubscripts(int vSpaceDimensions, bool orthogonalBasis = false)
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

    private static RGaFloat64LinearMapOutermorphism GetCGaBasisMap(int vSpaceDimensions)
    {
        // If linearly independent basis F = <f1, f2, f3> is related to
        // orthonormal basis E = <e1, e2, e3> via matrix M (F = M E), then
        // the scalars of a multivector expressed using E (Ae) are related
        // to the scalars of the same multivectors expressed using basis F
        // (Af) using the inverse transpose of M: Af = inv(transpose(M)) Ae
        // Thus if M is an orthogonal matrix (or as a special case, a permutation)
        // they are related using M itself: Af = M Ae.

        var processor = RGaFloat64ConformalProcessor.Instance;

        var vectorMapArray = new double[vSpaceDimensions, vSpaceDimensions];

        for (var i = 0; i < vSpaceDimensions - 2; i++)
            vectorMapArray[i + 1, i + 2] = 1d;

        vectorMapArray[0, 0] = 1d;
        vectorMapArray[0, 1] = 1d;

        vectorMapArray[vSpaceDimensions - 1, 0] = 0.5d;
        vectorMapArray[vSpaceDimensions - 1, 1] = -0.5d;

        return vectorMapArray
            .ColumnsToLinVectors()
            .ToLinUnilinearMap()
            .ToOutermorphism(processor);
    }

    private static RGaFloat64LinearMapOutermorphism GetCGaBasisMapInverse(int vSpaceDimensions)
    {
        // If linearly independent basis F = <f1, f2, f3> is related to
        // orthonormal basis E = <e1, e2, e3> via matrix M (F = M E), then
        // the scalars of a multivector expressed using E (Ae) are related
        // to the scalars of the same multivectors expressed using basis F
        // (Af) using the inverse transpose of M: Af = inv(transpose(M)) Ae
        // Thus if M is an orthogonal matrix (or as a special case, a permutation)
        // they are related using M itself: Af = M Ae.

        var processor = RGaFloat64ConformalProcessor.Instance;

        var vectorMapArray = new double[vSpaceDimensions, vSpaceDimensions];

        for (var i = 0; i < vSpaceDimensions - 2; i++)
            vectorMapArray[i + 2, i + 1] = 1d;

        vectorMapArray[0, 0] = 0.5d;
        vectorMapArray[1, 0] = 0.5d;

        vectorMapArray[0, vSpaceDimensions - 1] = 1d;
        vectorMapArray[1, vSpaceDimensions - 1] = -1d;

        return vectorMapArray
            .ColumnsToLinVectors()
            .ToLinUnilinearMap()
            .ToOutermorphism(processor);
    }


    public static GaFloat64GeometricSpaceBasisSpecs CreateEGa(int vSpaceDimensions)
    {
        if (vSpaceDimensions < 2)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        var laTeXVectorSubscripts =
            vSpaceDimensions
                .GetRange(i => (i + 1).ToString())
                .ToImmutableArray();

        return new GaFloat64GeometricSpaceBasisSpecs(
            RGaFloat64EuclideanProcessor.Instance,
            laTeXVectorSubscripts
        );
    }

    public static GaFloat64GeometricSpaceBasisSpecs CreateCGa(int vSpaceDimensions)
    {
        if (vSpaceDimensions < 4)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        var laTeXVectorSubscripts =
            GetCGaVectorSubscripts(vSpaceDimensions).ToImmutableArray();

        var basisMap = GetCGaBasisMap(vSpaceDimensions);
        var basisMapInverse = GetCGaBasisMapInverse(vSpaceDimensions);

        return new GaFloat64GeometricSpaceBasisSpecs(
            RGaFloat64ConformalProcessor.Instance,
            laTeXVectorSubscripts,
            basisMap,
            basisMapInverse
        );
    }

    public static GaFloat64GeometricSpaceBasisSpecs CreateCGaOrthogonal(int vSpaceDimensions)
    {
        if (vSpaceDimensions < 4)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        var laTeXVectorSubscripts =
            GetCGaVectorSubscripts(
                vSpaceDimensions,
                true
            ).ToImmutableArray();

        return new GaFloat64GeometricSpaceBasisSpecs(
            RGaFloat64ConformalProcessor.Instance,
            laTeXVectorSubscripts
        );
    }


    public RGaFloat64Processor Processor { get; }

    public RGaFloat64EuclideanProcessor EuclideanProcessor
        => RGaFloat64EuclideanProcessor.Instance;

    public IReadOnlyList<string> LaTeXVectorSubscripts { get; }

    public IRGaFloat64Outermorphism BasisMap { get; }

    public IRGaFloat64Outermorphism BasisMapInverse { get; }

    public int VSpaceDimensions
        => LaTeXVectorSubscripts.Count;

    public ulong GaSpaceDimensions
        => 1ul << VSpaceDimensions;


    private GaFloat64GeometricSpaceBasisSpecs(RGaFloat64Processor processor, IReadOnlyList<string> laTeXVectorSubscripts)
    {
        Processor = processor;
        LaTeXVectorSubscripts = laTeXVectorSubscripts;
        BasisMap = new RGaFloat64IdentityOutermorphism(processor);
        BasisMapInverse = BasisMap;
    }

    private GaFloat64GeometricSpaceBasisSpecs(RGaFloat64Processor processor, IReadOnlyList<string> laTeXVectorSubscripts, IRGaFloat64Outermorphism basisMap, IRGaFloat64Outermorphism basisMapInverse)
    {
        Processor = processor;
        LaTeXVectorSubscripts = laTeXVectorSubscripts;
        BasisMap = basisMap;
        BasisMapInverse = basisMapInverse;
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

    private string BasisBladeToLaTeX(ulong id)
    {
        Debug.Assert(id > 0 && id < GaSpaceDimensions);

        var subscriptText =
            id
                .PatternToPositions()
                .Select(i => LaTeXVectorSubscripts[i])
                .ConcatenateText(",");

        return @$"\boldsymbol{{e}}_{{{subscriptText}}}";
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private string TermToLaTeX(ulong id, double scalar)
    {
        return id == 0
            ? ScalarTermToLaTeX(scalar)
            : $"{ScalarToLaTeX(scalar)} {BasisBladeToLaTeX(id)}";
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToLaTeX(double mv)
    {
        return ToLaTeX(
            (RGaFloat64Multivector)Processor.Scalar(mv)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToLaTeX(RGaFloat64Scalar mv)
    {
        return ToLaTeX(
            (RGaFloat64Multivector)mv
        );
    }

    public string ToLaTeX(RGaFloat64Multivector mv)
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
}