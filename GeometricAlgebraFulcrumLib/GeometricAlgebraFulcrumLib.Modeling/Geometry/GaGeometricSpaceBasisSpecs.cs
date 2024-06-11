using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.LinearMaps;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry;

public sealed class GaGeometricSpaceBasisSpecs<T>
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

    private static XGaLinearMapOutermorphism<T> GetCGaBasisMap(IScalarProcessor<T> scalarProcessor, int vSpaceDimensions)
    {
        // If linearly independent basis F = <f1, f2, f3> is related to
        // orthonormal basis E = <e1, e2, e3> via matrix M (F = M E), then
        // the scalars of a multivector expressed using E (Ae) are related
        // to the scalars of the same multivectors expressed using basis F
        // (Af) using the inverse transpose of M: Af = inv(transpose(M)) Ae
        // Thus if M is an orthogonal matrix (or as a special case, a permutation)
        // they are related using M itself: Af = M Ae.

        var processor = XGaConformalProcessor<T>.CreateConformal(scalarProcessor);

        var vectorMapArray = new T[vSpaceDimensions, vSpaceDimensions];

        for (var i = 0; i < vSpaceDimensions - 2; i++)
            vectorMapArray[i + 1, i + 2] = scalarProcessor.OneValue;

        vectorMapArray[0, 0] = scalarProcessor.OneValue;
        vectorMapArray[0, 1] = scalarProcessor.OneValue;

        vectorMapArray[vSpaceDimensions - 1, 0] = scalarProcessor.Divide(scalarProcessor.OneValue, scalarProcessor.TwoValue).ScalarValue;
        vectorMapArray[vSpaceDimensions - 1, 1] = scalarProcessor.Divide(scalarProcessor.MinusOneValue, scalarProcessor.TwoValue).ScalarValue;

        return vectorMapArray
            .ColumnsToLinVectors(scalarProcessor)
            .ToLinUnilinearMap(scalarProcessor)
            .ToOutermorphism(processor);
    }

    private static XGaLinearMapOutermorphism<T> GetCGaBasisMapInverse(IScalarProcessor<T> scalarProcessor, int vSpaceDimensions)
    {
        // If linearly independent basis F = <f1, f2, f3> is related to
        // orthonormal basis E = <e1, e2, e3> via matrix M (F = M E), then
        // the scalars of a multivector expressed using E (Ae) are related
        // to the scalars of the same multivectors expressed using basis F
        // (Af) using the inverse transpose of M: Af = inv(transpose(M)) Ae
        // Thus if M is an orthogonal matrix (or as a special case, a permutation)
        // they are related using M itself: Af = M Ae.

        var processor = XGaConformalProcessor<T>.CreateConformal(scalarProcessor);

        var vectorMapArray = new T[vSpaceDimensions, vSpaceDimensions];

        for (var i = 0; i < vSpaceDimensions - 2; i++)
            vectorMapArray[i + 2, i + 1] = scalarProcessor.OneValue;

        vectorMapArray[0, 0] = scalarProcessor.Divide(scalarProcessor.OneValue, scalarProcessor.TwoValue).ScalarValue;
        vectorMapArray[1, 0] = scalarProcessor.Divide(scalarProcessor.OneValue, scalarProcessor.TwoValue).ScalarValue;

        vectorMapArray[0, vSpaceDimensions - 1] = scalarProcessor.OneValue;
        vectorMapArray[1, vSpaceDimensions - 1] = scalarProcessor.MinusOneValue;

        return vectorMapArray
            .ColumnsToLinVectors(scalarProcessor)
            .ToLinUnilinearMap(scalarProcessor)
            .ToOutermorphism(processor);
    }
    
    
    public static GaGeometricSpaceBasisSpecs<T> CreateVGa(IScalarProcessor<T> scalarProcessor, int vSpaceDimensions)
    {
        if (vSpaceDimensions < 2)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        var laTeXVectorSubscripts = 
            vSpaceDimensions
                .GetRange(i => (i + 1).ToString())
                .ToImmutableArray();

        return new GaGeometricSpaceBasisSpecs<T>(
            XGaProcessor<T>.CreateEuclidean(scalarProcessor), 
            laTeXVectorSubscripts
        );
    }
    
    public static GaGeometricSpaceBasisSpecs<T> CreatePGa(IScalarProcessor<T> scalarProcessor, int vSpaceDimensions)
    {
        if (vSpaceDimensions < 3)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));
        
        var laTeXVectorSubscripts = 
            vSpaceDimensions
                .GetRange(i => i == 0 ? "o" : i.ToString())
                .ToImmutableArray();

        return new GaGeometricSpaceBasisSpecs<T>(
            XGaConformalProcessor<T>.CreateProjective(scalarProcessor), 
            laTeXVectorSubscripts
        );
    }

    public static GaGeometricSpaceBasisSpecs<T> CreateCGa(IScalarProcessor<T> scalarProcessor, int vSpaceDimensions)
    {
        if (vSpaceDimensions < 4)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        var laTeXVectorSubscripts = 
            GetCGaVectorSubscripts(vSpaceDimensions).ToImmutableArray();

        var basisMap = GetCGaBasisMap(scalarProcessor, vSpaceDimensions);
        var basisMapInverse = GetCGaBasisMapInverse(scalarProcessor, vSpaceDimensions);

        return new GaGeometricSpaceBasisSpecs<T>(
            XGaConformalProcessor<T>.CreateConformal(scalarProcessor), 
            laTeXVectorSubscripts, 
            basisMap, 
            basisMapInverse
        );
    }
    
    public static GaGeometricSpaceBasisSpecs<T> CreateCGaOrthogonal(IScalarProcessor<T> scalarProcessor, int vSpaceDimensions)
    {
        if (vSpaceDimensions < 4)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        var laTeXVectorSubscripts = 
            GetCGaVectorSubscripts(
                vSpaceDimensions, 
                true
            ).ToImmutableArray();

        return new GaGeometricSpaceBasisSpecs<T>(
            XGaConformalProcessor<T>.CreateConformal(scalarProcessor), 
            laTeXVectorSubscripts
        );
    }


    public XGaProcessor<T> Processor { get; }

    public XGaEuclideanProcessor<T> EuclideanProcessor { get; }

    public IReadOnlyList<string> LaTeXVectorSubscripts { get; }
    
    public IXGaOutermorphism<T> BasisMap { get; }

    public IXGaOutermorphism<T> BasisMapInverse { get; }

    public IScalarProcessor<T> ScalarProcessor 
        => Processor.ScalarProcessor;

    public int VSpaceDimensions 
        => LaTeXVectorSubscripts.Count;
    
    public ulong GaSpaceDimensions 
        => 1ul << VSpaceDimensions;


    private GaGeometricSpaceBasisSpecs(XGaProcessor<T> processor, IReadOnlyList<string> laTeXVectorSubscripts)
    {
        Processor = processor;
        EuclideanProcessor = processor as XGaEuclideanProcessor<T> ?? processor.ScalarProcessor.CreateEuclideanXGaProcessor();
        LaTeXVectorSubscripts = laTeXVectorSubscripts;
        BasisMap = new XGaIdentityOutermorphism<T>(processor);
        BasisMapInverse = BasisMap;
    }

    private GaGeometricSpaceBasisSpecs(XGaProcessor<T> processor, IReadOnlyList<string> laTeXVectorSubscripts, IXGaOutermorphism<T> basisMap, IXGaOutermorphism<T> basisMapInverse)
    {
        Processor = processor;
        EuclideanProcessor = processor as XGaEuclideanProcessor<T> ?? processor.ScalarProcessor.CreateEuclideanXGaProcessor();
        LaTeXVectorSubscripts = laTeXVectorSubscripts;
        BasisMap = basisMap;
        BasisMapInverse = basisMapInverse;
    }


    private string ScalarToLaTeX(T scalar)
    {
        Debug.Assert(!ScalarProcessor.IsZero(scalar));

        if (ScalarProcessor.IsOne(scalar))
            return " +";

        if (ScalarProcessor.IsMinusOne(scalar))
            return " -";

        return $" + '{scalar}'";
    }

    private string ScalarTermToLaTeX(T scalar)
    {
        Debug.Assert(!ScalarProcessor.IsZero(scalar));

        if (ScalarProcessor.IsOne(scalar))
            return " + 1";

        if (ScalarProcessor.IsMinusOne(scalar))
            return " - 1";
        
        return $" + '{scalar}'";
    }

    private string BasisBladeToLaTeX(IIndexSet id)
    {
        Debug.Assert(id.Max() < VSpaceDimensions);

        var subscriptText = 
            id
                .Select(i => LaTeXVectorSubscripts[i])
                .ConcatenateText(",");

        return @$"\boldsymbol{{e}}_{{{subscriptText}}}";
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private string TermToLaTeX(IIndexSet id, T scalar)
    {
        return id.IsEmptySet
            ? ScalarTermToLaTeX(scalar) 
            : $"{ScalarToLaTeX(scalar)} {BasisBladeToLaTeX(id)}";
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToLaTeX(T mv)
    {
        return ToLaTeX(
            (XGaMultivector<T>) Processor.Scalar(mv)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToLaTeX(Scalar<T> mv)
    {
        return ToLaTeX(mv.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToLaTeX(IScalar<T> mv)
    {
        return ToLaTeX(mv.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToLaTeX(XGaScalar<T> mv)
    {
        return ToLaTeX(
            (XGaMultivector<T>) mv
        );
    }

    public string ToLaTeX(XGaMultivector<T> mv)
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