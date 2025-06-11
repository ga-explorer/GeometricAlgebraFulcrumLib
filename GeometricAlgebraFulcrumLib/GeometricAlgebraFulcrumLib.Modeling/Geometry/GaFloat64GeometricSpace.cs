using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry;

public abstract class GaFloat64GeometricSpace
{
    public GaFloat64GeometricSpaceBasisSpecs BasisSpecs { get; }

    public XGaFloat64Processor Processor
        => BasisSpecs.Processor;

    public XGaFloat64EuclideanProcessor EuclideanProcessor
        => BasisSpecs.EuclideanProcessor;

    public XGaMetric Metric
        => Processor;

    public int VSpaceDimensions
        => BasisSpecs.VSpaceDimensions;

    public ulong GaSpaceDimensions
        => 1UL << VSpaceDimensions;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected GaFloat64GeometricSpace(GaFloat64GeometricSpaceBasisSpecs basisSpecs)
    {
        BasisSpecs = basisSpecs;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValidElement(XGaFloat64Multivector mv)
    {
        return mv.Metric.HasSameSignature(Processor) &&
               mv.VSpaceDimensions <= VSpaceDimensions;
    }


    //protected string BasisBladeToLaTeX(ulong id)
    //{
    //    Debug.Assert(id > 0 && id < GaSpaceDimensions);

    //    var subscriptText = 
    //        id
    //            .PatternToPositions()
    //            .Select(i => BasisSpecs.LaTeXVectorSubscripts[i])
    //            .ConcatenateText(",");

    //    return @$"\boldsymbol{{e}}_{{{subscriptText}}}";
    //}

    //protected string ScalarToLaTeX(double scalar)
    //{
    //    Debug.Assert(scalar != 0);

    //    if (scalar == 1)
    //        return " +";

    //    if (scalar == -1)
    //        return " -";

    //    var scalarText = 
    //        Math.Abs(scalar).ToString("G8");

    //    var eIndex = 
    //        scalarText.IndexOf(
    //            'E', 
    //            StringComparison.InvariantCultureIgnoreCase
    //        );

    //    if (eIndex < 0)
    //        return scalar > 0 
    //            ? " + " + scalarText
    //            : " - " + scalarText;

    //    var scalarTextPart1 = 
    //        scalarText[..eIndex];

    //    var scalarTextPart2 = 
    //        scalarText[(eIndex + 1)..];

    //    scalarText = @$"{scalarTextPart1}\times10^{{{scalarTextPart2}}}";

    //    return scalar > 0 
    //        ? " + " + scalarText
    //        : " - " + scalarText; 
    //}

    //protected string ScalarTermToLaTeX(double scalar)
    //{
    //    Debug.Assert(scalar != 0);

    //    var scalarText = 
    //        Math.Abs(scalar).ToString("G8");

    //    var eIndex = 
    //        scalarText.IndexOf(
    //            'E', 
    //            StringComparison.InvariantCultureIgnoreCase
    //        );

    //    if (eIndex < 0)
    //        return scalar > 0 
    //            ? " + " + scalarText
    //            : " - " + scalarText;

    //    var scalarTextPart1 = 
    //        scalarText[..eIndex];

    //    var scalarTextPart2 = 
    //        scalarText[(eIndex + 1)..];

    //    scalarText = @$"{scalarTextPart1}\times10^{{{scalarTextPart2}}}";

    //    return scalar > 0 
    //        ? " + " + scalarText
    //        : " - " + scalarText;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //protected string TermToLaTeX(ulong id, double scalar)
    //{
    //    return id == 0 
    //        ? ScalarTermToLaTeX(scalar) 
    //        : $"{ScalarToLaTeX(scalar)} {BasisBladeToLaTeX(id)}";
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public string ToLaTeX(double mv)
    //{
    //    return ToLaTeX(
    //        (XGaFloat64Multivector) Processor.CreateScalar(mv)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public string ToLaTeX(XGaFloat64Scalar mv)
    //{
    //    return ToLaTeX(
    //        (XGaFloat64Multivector) mv
    //    );
    //}

    //public string ToLaTeX(XGaFloat64Multivector mv)
    //{
    //    if (mv.IsZero)
    //        return "0";

    //    var latexText =
    //        BasisMap.OmMap(mv)
    //            .IdScalarPairs
    //            .OrderBy(p => p.Key.Grade())
    //            .ThenBy(p => p.Key)
    //            .Select(p => TermToLaTeX(p.Key, p.Value))
    //            .ConcatenateText();

    //    latexText = latexText[1] == '+'
    //        ? latexText[2..] 
    //        : "-" + latexText[2..];

    //    return latexText.Trim();
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Scalar ReflectOn(XGaFloat64Scalar mv, XGaFloat64KVector subspace)
    {
        return mv.ReflectOn(subspace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector ReflectOn(XGaFloat64Vector mv, XGaFloat64KVector subspace)
    {
        return mv.ReflectOn(subspace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector ReflectOn(XGaFloat64Bivector mv, XGaFloat64KVector subspace)
    {
        return mv.ReflectOn(subspace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64HigherKVector ReflectOn(XGaFloat64HigherKVector mv, XGaFloat64KVector subspace)
    {
        return mv.ReflectOn(subspace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVector ReflectOn(XGaFloat64KVector mv, XGaFloat64KVector subspace)
    {
        return mv.ReflectOn(subspace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector ReflectOn(XGaFloat64Multivector mv, XGaFloat64KVector subspace)
    {
        return mv.ReflectOn(subspace);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Scalar ReflectOpnsOnOpns(XGaFloat64Scalar mv, XGaFloat64KVector subspace)
    {
        return mv.ReflectDirectOnDirect(subspace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector ReflectOpnsOnOpns(XGaFloat64Vector mv, XGaFloat64KVector subspace)
    {
        return mv.ReflectDirectOnDirect(subspace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector ReflectOpnsOnOpns(XGaFloat64Bivector mv, XGaFloat64KVector subspace)
    {
        return mv.ReflectDirectOnDirect(subspace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64HigherKVector ReflectOpnsOnOpns(XGaFloat64HigherKVector mv, XGaFloat64KVector subspace)
    {
        return mv.ReflectDirectOnDirect(subspace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVector ReflectOpnsOnOpns(XGaFloat64KVector mv, XGaFloat64KVector subspace)
    {
        return mv.ReflectDirectOnDirect(subspace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector ReflectOpnsOnOpns(XGaFloat64Multivector mv, XGaFloat64KVector subspace)
    {
        return mv.ReflectDirectOnDirect(subspace);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Scalar ReflectOpnsOnIpns(XGaFloat64Scalar mv, XGaFloat64KVector subspace)
    {
        return mv.ReflectDirectOnDual(subspace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector ReflectOpnsOnIpns(XGaFloat64Vector mv, XGaFloat64KVector subspace)
    {
        return mv.ReflectDirectOnDual(subspace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector ReflectOpnsOnIpns(XGaFloat64Bivector mv, XGaFloat64KVector subspace)
    {
        return mv.ReflectDirectOnDual(subspace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64HigherKVector ReflectOpnsOnIpns(XGaFloat64HigherKVector mv, XGaFloat64KVector subspace)
    {
        return mv.ReflectDirectOnDual(subspace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVector ReflectOpnsOnIpns(XGaFloat64KVector mv, XGaFloat64KVector subspace)
    {
        return mv.ReflectDirectOnDual(subspace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector ReflectOpnsOnIpns(XGaFloat64Multivector mv, XGaFloat64KVector subspace)
    {
        return mv.ReflectDirectOnDual(subspace);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Scalar ReflectIpnsOnOpns(XGaFloat64Scalar mv, XGaFloat64KVector subspace)
    {
        return mv.ReflectDualOnDirect(subspace, VSpaceDimensions);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector ReflectIpnsOnOpns(XGaFloat64Vector mv, XGaFloat64KVector subspace)
    {
        return mv.ReflectDualOnDirect(subspace, VSpaceDimensions);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector ReflectIpnsOnOpns(XGaFloat64Bivector mv, XGaFloat64KVector subspace)
    {
        return mv.ReflectDualOnDirect(subspace, VSpaceDimensions);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64HigherKVector ReflectIpnsOnOpns(XGaFloat64HigherKVector mv, XGaFloat64KVector subspace)
    {
        return mv.ReflectDualOnDirect(subspace, VSpaceDimensions);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVector ReflectIpnsOnOpns(XGaFloat64KVector mv, XGaFloat64KVector subspace)
    {
        return mv.ReflectDualOnDirect(subspace, VSpaceDimensions);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector ReflectIpnsOnOpns(XGaFloat64Multivector mv, XGaFloat64KVector subspace)
    {
        return mv.ReflectDualOnDirect(subspace, VSpaceDimensions);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Scalar ReflectIpnsOnIpns(XGaFloat64Scalar mv, XGaFloat64KVector subspace)
    {
        return mv.ReflectDualOnDual(subspace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector ReflectIpnsOnIpns(XGaFloat64Vector mv, XGaFloat64KVector subspace)
    {
        return mv.ReflectDualOnDual(subspace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector ReflectIpnsOnIpns(XGaFloat64Bivector mv, XGaFloat64KVector subspace)
    {
        return mv.ReflectDualOnDual(subspace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64HigherKVector ReflectIpnsOnIpns(XGaFloat64HigherKVector mv, XGaFloat64KVector subspace)
    {
        return mv.ReflectDualOnDual(subspace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVector ReflectIpnsOnIpns(XGaFloat64KVector mv, XGaFloat64KVector subspace)
    {
        return mv.ReflectDualOnDual(subspace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector ReflectIpnsOnIpns(XGaFloat64Multivector mv, XGaFloat64KVector subspace)
    {
        return mv.ReflectDualOnDual(subspace);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVector Intersect(XGaFloat64KVector blade1, XGaFloat64KVector blade2)
    {
        return blade1.Op(blade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVector Intersect(XGaFloat64KVector blade1, XGaFloat64KVector blade2, XGaFloat64KVector blade3)
    {
        return blade1.Op(blade2).Op(blade3);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Scalar Project(XGaFloat64Scalar blade, XGaFloat64KVector subspace)
    {
        var projectedBlade =
            blade
                .Fdp(subspace)
                .Gp(subspace)
                .GetScalarPart();

        var scalarFactor = subspace.SpSquared();

        return scalarFactor.IsNearZero()
            ? projectedBlade
            : projectedBlade.Divide(scalarFactor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector Project(XGaFloat64Vector blade, XGaFloat64KVector subspace)
    {
        var projectedBlade =
            blade
                .Fdp(subspace)
                .Gp(subspace)
                .GetVectorPart();

        var scalarFactor = subspace.SpSquared();

        return scalarFactor.IsNearZero()
            ? projectedBlade
            : projectedBlade / scalarFactor;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector Project(XGaFloat64Bivector blade, XGaFloat64KVector subspace)
    {
        var projectedBlade =
            blade
                .Fdp(subspace)
                .Gp(subspace)
                .GetBivectorPart();

        var scalarFactor = subspace.SpSquared();

        return scalarFactor.IsNearZero()
            ? projectedBlade
            : projectedBlade / scalarFactor;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64HigherKVector Project(XGaFloat64HigherKVector blade, XGaFloat64KVector subspace)
    {
        var projectedBlade =
            blade
                .Fdp(subspace)
                .Gp(subspace)
                .GetHigherKVectorPart(blade.Grade);

        var scalarFactor = subspace.SpSquared();

        return scalarFactor.IsNearZero()
            ? projectedBlade
            : projectedBlade / scalarFactor;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVector Project(XGaFloat64KVector blade, XGaFloat64KVector subspace)
    {
        var projectedBlade =
            blade
                .Fdp(subspace)
                .Gp(subspace)
                .GetKVectorPart(blade.Grade);

        var scalarFactor = subspace.SpSquared();

        return scalarFactor.IsNearZero()
            ? projectedBlade
            : projectedBlade / scalarFactor;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector Project(XGaFloat64Multivector blade, XGaFloat64KVector subspace)
    {
        var projectedBlade =
            blade
                .Fdp(subspace)
                .Gp(subspace);

        var scalarFactor = subspace.SpSquared();

        return scalarFactor.IsNearZero()
            ? projectedBlade
            : projectedBlade / scalarFactor;
    }

}