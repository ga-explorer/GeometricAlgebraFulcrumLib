using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.LinearMaps.Reflectors;

public static class RGaFloat64ReflectorUtils
{
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Scalar ReflectOn(this RGaFloat64Scalar mv, RGaFloat64KVector subspace)
    {
        Debug.Assert(subspace.IsNearBlade());

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector ReflectOn(this RGaFloat64Vector mv, RGaFloat64KVector subspace)
    {
        Debug.Assert(subspace.IsNearBlade());

        return subspace
            .Gp(mv)
            .Gp(subspace.Inverse())
            .GetVectorPart();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector ReflectOn(this RGaFloat64Bivector mv, RGaFloat64KVector subspace)
    {
        Debug.Assert(subspace.IsNearBlade());

        return subspace
            .Gp(mv)
            .Gp(subspace.Inverse())
            .GetBivectorPart();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64HigherKVector ReflectOn(this RGaFloat64HigherKVector mv, RGaFloat64KVector subspace)
    {
        Debug.Assert(subspace.IsNearBlade());

        return subspace
            .Gp(mv)
            .Gp(subspace.Inverse())
            .GetHigherKVectorPart(mv.Grade);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector ReflectOn(this RGaFloat64KVector mv, RGaFloat64KVector subspace)
    {
        Debug.Assert(subspace.IsNearBlade());

        return subspace
            .Gp(mv)
            .Gp(subspace.Inverse())
            .GetKVectorPart(mv.Grade);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector ReflectOn(this RGaFloat64Multivector mv, RGaFloat64KVector subspace)
    {
        Debug.Assert(subspace.IsNearBlade());

        return subspace
            .Gp(mv)
            .Gp(subspace.Inverse());
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Scalar ReflectDirectOnDirect(this RGaFloat64Scalar mv, RGaFloat64KVector subspace)
    {
        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector ReflectDirectOnDirect(this RGaFloat64Vector mv, RGaFloat64KVector subspace)
    {
        var mv1 = mv.ReflectOn(subspace);

        return subspace.IsEven() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector ReflectDirectOnDirect(this RGaFloat64Bivector mv, RGaFloat64KVector subspace)
    {
        return mv.ReflectOn(subspace);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64HigherKVector ReflectDirectOnDirect(this RGaFloat64HigherKVector mv, RGaFloat64KVector subspace)
    {
        var mv1 = mv.ReflectOn(subspace);

        var n = mv.Grade * (subspace.Grade + 1);

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector ReflectDirectOnDirect(this RGaFloat64KVector mv, RGaFloat64KVector subspace)
    {
        var mv1 = mv.ReflectOn(subspace);

        var n = mv.Grade * (subspace.Grade + 1);

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector ReflectDirectOnDirect(this RGaFloat64Multivector mv, RGaFloat64KVector subspace)
    {
        return mv
            .GetKVectorParts()
            .Select(kv => kv.ReflectDirectOnDirect(subspace))
            .Aggregate(
                (RGaFloat64Multivector) mv.Processor.CreateZeroScalar(),
                (a, b) => a.Add(b)
            );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Scalar ReflectDirectOnDual(this RGaFloat64Scalar mv, RGaFloat64KVector subspace)
    {
        return mv;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector ReflectDirectOnDual(this RGaFloat64Vector mv, RGaFloat64KVector subspace)
    {
        var mv1 = mv.ReflectOn(subspace);
        
        return subspace.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector ReflectDirectOnDual(this RGaFloat64Bivector mv, RGaFloat64KVector subspace)
    {
        return mv.ReflectOn(subspace);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64HigherKVector ReflectDirectOnDual(this RGaFloat64HigherKVector mv, RGaFloat64KVector subspace)
    {
        var mv1 = mv.ReflectOn(subspace);

        var n = mv.Grade * subspace.Grade;

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector ReflectDirectOnDual(this RGaFloat64KVector mv, RGaFloat64KVector subspace)
    {
        var mv1 = mv.ReflectOn(subspace);

        var n = mv.Grade * subspace.Grade;

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector ReflectDirectOnDual(this RGaFloat64Multivector mv, RGaFloat64KVector subspace)
    {
        return mv
            .GetKVectorParts()
            .Select(kv => kv.ReflectDirectOnDual(subspace))
            .Aggregate(
                (RGaFloat64Multivector) mv.Processor.CreateZeroScalar(),
                (a, b) => a.Add(b)
            );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Scalar ReflectDualOnDirect(this RGaFloat64Scalar mv, RGaFloat64KVector subspace, int vSpaceDimensions)
    {
        var n = subspace.Grade + vSpaceDimensions;

        return n.IsOdd() ? -mv : mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector ReflectDualOnDirect(this RGaFloat64Vector mv, RGaFloat64KVector subspace, int vSpaceDimensions)
    {
        var mv1 = mv.ReflectOn(subspace);

        var n = vSpaceDimensions - 1;

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector ReflectDualOnDirect(this RGaFloat64Bivector mv, RGaFloat64KVector subspace, int vSpaceDimensions)
    {
        var mv1 = mv.ReflectOn(subspace);

        var n = (mv.Grade + 1) * (subspace.Grade + 1) + vSpaceDimensions - 1;

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64HigherKVector ReflectDualOnDirect(this RGaFloat64HigherKVector mv, RGaFloat64KVector subspace, int vSpaceDimensions)
    {
        var mv1 = mv.ReflectOn(subspace);

        var n = (mv.Grade + 1) * (subspace.Grade + 1) + vSpaceDimensions - 1;

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector ReflectDualOnDirect(this RGaFloat64KVector mv, RGaFloat64KVector subspace, int vSpaceDimensions)
    {
        var mv1 = mv.ReflectOn(subspace);

        var n = (mv.Grade + 1) * (subspace.Grade + 1) + vSpaceDimensions - 1;

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector ReflectDualOnDirect(this RGaFloat64Multivector mv, RGaFloat64KVector subspace, int vSpaceDimensions)
    {
        return mv
            .GetKVectorParts()
            .Select(kv => kv.ReflectDualOnDirect(subspace, vSpaceDimensions))
            .Aggregate(
                (RGaFloat64Multivector) mv.Processor.CreateZeroScalar(),
                (a, b) => a.Add(b)
            );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Scalar ReflectDualOnDual(this RGaFloat64Scalar mv, RGaFloat64KVector subspace)
    {
        return subspace.IsOdd() ? -mv : mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector ReflectDualOnDual(this RGaFloat64Vector mv, RGaFloat64KVector subspace)
    {
        return mv.ReflectOn(subspace);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector ReflectDualOnDual(this RGaFloat64Bivector mv, RGaFloat64KVector subspace)
    {
        var mv1 = mv.ReflectOn(subspace);
        
        return subspace.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64HigherKVector ReflectDualOnDual(this RGaFloat64HigherKVector mv, RGaFloat64KVector subspace)
    {
        var mv1 = mv.ReflectOn(subspace);

        var n = (mv.Grade + 1) * subspace.Grade;

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector ReflectDualOnDual(this RGaFloat64KVector mv, RGaFloat64KVector subspace)
    {
        var mv1 = mv.ReflectOn(subspace);

        var n = (mv.Grade + 1) * subspace.Grade;

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector ReflectDualOnDual(this RGaFloat64Multivector mv, RGaFloat64KVector subspace)
    {
        return mv
            .GetKVectorParts()
            .Select(kv => kv.ReflectDualOnDual(subspace))
            .Aggregate(
                (RGaFloat64Multivector) mv.Processor.CreateZeroScalar(),
                (a, b) => a.Add(b)
            );
    }


}