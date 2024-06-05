using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.LinearMaps.Reflectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry;

public abstract class XGaGeometrySpace<T>
{
    public XGaGeometrySpaceBasisSpecs<T> BasisSpecs { get; }

    public IScalarProcessor<T> ScalarProcessor 
        => BasisSpecs.ScalarProcessor;

    public XGaProcessor<T> Processor 
        => BasisSpecs.Processor;
    
    public XGaEuclideanProcessor<T> EuclideanProcessor 
        => BasisSpecs.EuclideanProcessor;

    public XGaMetric Metric 
        => Processor;

    public int VSpaceDimensions 
        => BasisSpecs.VSpaceDimensions;
    
    public ulong GaSpaceDimensions 
        => 1UL << VSpaceDimensions;

    public Scalar<T> ScalarZero { get; }

    public Scalar<T> ScalarOne { get; }

    public Scalar<T> ScalarMinusOne { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected XGaGeometrySpace(XGaGeometrySpaceBasisSpecs<T> basisSpecs)
    {
        BasisSpecs = basisSpecs;

        ScalarZero = basisSpecs.ScalarProcessor.Zero;
        ScalarOne = basisSpecs.ScalarProcessor.One;
        ScalarMinusOne = basisSpecs.ScalarProcessor.MinusOne;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValidElement(XGaMultivector<T> mv)
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
    //        (XGaMultivector<T>) Processor.CreateScalar(mv)
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public string ToLaTeX(XGaScalar<T> mv)
    //{
    //    return ToLaTeX(
    //        (XGaMultivector<T>) mv
    //    );
    //}

    //public string ToLaTeX(XGaMultivector<T> mv)
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
    public XGaScalar<T> ReflectOn(XGaScalar<T> mv, XGaKVector<T> subspace)
    {
        return mv.ReflectOn(subspace);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> ReflectOn(XGaVector<T> mv, XGaKVector<T> subspace)
    {
        return mv.ReflectOn(subspace);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> ReflectOn(XGaBivector<T> mv, XGaKVector<T> subspace)
    {
        return mv.ReflectOn(subspace);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaHigherKVector<T> ReflectOn(XGaHigherKVector<T> mv, XGaKVector<T> subspace)
    {
        return mv.ReflectOn(subspace);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> ReflectOn(XGaKVector<T> mv, XGaKVector<T> subspace)
    {
        return mv.ReflectOn(subspace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> ReflectOn(XGaMultivector<T> mv, XGaKVector<T> subspace)
    {
        return mv.ReflectOn(subspace);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> ReflectOpnsOnOpns(XGaScalar<T> mv, XGaKVector<T> subspace)
    {
        return mv.ReflectDirectOnDirect(subspace);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> ReflectOpnsOnOpns(XGaVector<T> mv, XGaKVector<T> subspace)
    {
        return mv.ReflectDirectOnDirect(subspace);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> ReflectOpnsOnOpns(XGaBivector<T> mv, XGaKVector<T> subspace)
    {
        return mv.ReflectDirectOnDirect(subspace);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaHigherKVector<T> ReflectOpnsOnOpns(XGaHigherKVector<T> mv, XGaKVector<T> subspace)
    {
        return mv.ReflectDirectOnDirect(subspace);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> ReflectOpnsOnOpns(XGaKVector<T> mv, XGaKVector<T> subspace)
    {
        return mv.ReflectDirectOnDirect(subspace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> ReflectOpnsOnOpns(XGaMultivector<T> mv, XGaKVector<T> subspace)
    {
        return mv.ReflectDirectOnDirect(subspace);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> ReflectOpnsOnIpns(XGaScalar<T> mv, XGaKVector<T> subspace)
    {
        return mv.ReflectDirectOnDual(subspace);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> ReflectOpnsOnIpns(XGaVector<T> mv, XGaKVector<T> subspace)
    {
        return mv.ReflectDirectOnDual(subspace);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> ReflectOpnsOnIpns(XGaBivector<T> mv, XGaKVector<T> subspace)
    {
        return mv.ReflectDirectOnDual(subspace);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaHigherKVector<T> ReflectOpnsOnIpns(XGaHigherKVector<T> mv, XGaKVector<T> subspace)
    {
        return mv.ReflectDirectOnDual(subspace);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> ReflectOpnsOnIpns(XGaKVector<T> mv, XGaKVector<T> subspace)
    {
        return mv.ReflectDirectOnDual(subspace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> ReflectOpnsOnIpns(XGaMultivector<T> mv, XGaKVector<T> subspace)
    {
        return mv.ReflectDirectOnDual(subspace);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> ReflectIpnsOnOpns(XGaScalar<T> mv, XGaKVector<T> subspace)
    {
        return mv.ReflectDualOnDirect(subspace, VSpaceDimensions);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> ReflectIpnsOnOpns(XGaVector<T> mv, XGaKVector<T> subspace)
    {
        return mv.ReflectDualOnDirect(subspace, VSpaceDimensions);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> ReflectIpnsOnOpns(XGaBivector<T> mv, XGaKVector<T> subspace)
    {
        return mv.ReflectDualOnDirect(subspace, VSpaceDimensions);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaHigherKVector<T> ReflectIpnsOnOpns(XGaHigherKVector<T> mv, XGaKVector<T> subspace)
    {
        return mv.ReflectDualOnDirect(subspace, VSpaceDimensions);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> ReflectIpnsOnOpns(XGaKVector<T> mv, XGaKVector<T> subspace)
    {
        return mv.ReflectDualOnDirect(subspace, VSpaceDimensions);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> ReflectIpnsOnOpns(XGaMultivector<T> mv, XGaKVector<T> subspace)
    {
        return mv.ReflectDualOnDirect(subspace, VSpaceDimensions);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> ReflectIpnsOnIpns(XGaScalar<T> mv, XGaKVector<T> subspace)
    {
        return mv.ReflectDualOnDual(subspace);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> ReflectIpnsOnIpns(XGaVector<T> mv, XGaKVector<T> subspace)
    {
        return mv.ReflectDualOnDual(subspace);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> ReflectIpnsOnIpns(XGaBivector<T> mv, XGaKVector<T> subspace)
    {
        return mv.ReflectDualOnDual(subspace);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaHigherKVector<T> ReflectIpnsOnIpns(XGaHigherKVector<T> mv, XGaKVector<T> subspace)
    {
        return mv.ReflectDualOnDual(subspace);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> ReflectIpnsOnIpns(XGaKVector<T> mv, XGaKVector<T> subspace)
    {
        return mv.ReflectDualOnDual(subspace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> ReflectIpnsOnIpns(XGaMultivector<T> mv, XGaKVector<T> subspace)
    {
        return mv.ReflectDualOnDual(subspace);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> Intersect(XGaKVector<T> blade1, XGaKVector<T> blade2)
    {
        return blade1.Op(blade2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> Intersect(XGaKVector<T> blade1, XGaKVector<T> blade2, XGaKVector<T> blade3)
    {
        return blade1.Op(blade2).Op(blade3);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> Project(XGaScalar<T> blade, XGaKVector<T> subspace)
    {
        var projectedBlade = 
            blade
                .Fdp(subspace)
                .Gp(subspace)
                .GetScalarPart();

        var scalarFactor = subspace.SpSquared();

        return scalarFactor.IsNearZero() 
            ? projectedBlade 
            : projectedBlade / scalarFactor;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> Project(XGaVector<T> blade, XGaKVector<T> subspace)
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
    public XGaBivector<T> Project(XGaBivector<T> blade, XGaKVector<T> subspace)
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
    public XGaHigherKVector<T> Project(XGaHigherKVector<T> blade, XGaKVector<T> subspace)
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
    public XGaKVector<T> Project(XGaKVector<T> blade, XGaKVector<T> subspace)
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
    public XGaMultivector<T> Project(XGaMultivector<T> blade, XGaKVector<T> subspace)
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