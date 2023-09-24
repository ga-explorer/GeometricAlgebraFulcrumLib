using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors.Composers;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.LinearMaps.Reflectors;

public static class XGaFloat64ReflectorUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar ReflectOn(this XGaFloat64Scalar mv, XGaFloat64KVector subspace)
    {
        Debug.Assert(subspace.IsNearBlade());

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector ReflectOn(this XGaFloat64Vector mv, XGaFloat64KVector subspace)
    {
        Debug.Assert(subspace.IsNearBlade());

        return subspace
            .Gp(mv)
            .Gp(subspace.Inverse())
            .GetVectorPart();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector ReflectOn(this XGaFloat64Bivector mv, XGaFloat64KVector subspace)
    {
        Debug.Assert(subspace.IsNearBlade());

        return subspace
            .Gp(mv)
            .Gp(subspace.Inverse())
            .GetBivectorPart();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64HigherKVector ReflectOn(this XGaFloat64HigherKVector mv, XGaFloat64KVector subspace)
    {
        Debug.Assert(subspace.IsNearBlade());

        return subspace
            .Gp(mv)
            .Gp(subspace.Inverse())
            .GetHigherKVectorPart(mv.Grade);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector ReflectOn(this XGaFloat64KVector mv, XGaFloat64KVector subspace)
    {
        Debug.Assert(subspace.IsNearBlade());

        return subspace
            .Gp(mv)
            .Gp(subspace.Inverse())
            .GetKVectorPart(mv.Grade);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector ReflectOn(this XGaFloat64Multivector mv, XGaFloat64KVector subspace)
    {
        Debug.Assert(subspace.IsNearBlade());

        return subspace
            .Gp(mv)
            .Gp(subspace.Inverse());
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar ReflectDirectOnDirect(this XGaFloat64Scalar mv, XGaFloat64KVector subspace)
    {
        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector ReflectDirectOnDirect(this XGaFloat64Vector mv, XGaFloat64KVector subspace)
    {
        var mv1 = mv.ReflectOn(subspace);

        return subspace.IsEven() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector ReflectDirectOnDirect(this XGaFloat64Bivector mv, XGaFloat64KVector subspace)
    {
        return mv.ReflectOn(subspace);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64HigherKVector ReflectDirectOnDirect(this XGaFloat64HigherKVector mv, XGaFloat64KVector subspace)
    {
        var mv1 = mv.ReflectOn(subspace);

        var n = mv.Grade * (subspace.Grade + 1);

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector ReflectDirectOnDirect(this XGaFloat64KVector mv, XGaFloat64KVector subspace)
    {
        var mv1 = mv.ReflectOn(subspace);

        var n = mv.Grade * (subspace.Grade + 1);

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector ReflectDirectOnDirect(this XGaFloat64Multivector mv, XGaFloat64KVector subspace)
    {
        return mv
            .GetKVectorParts()
            .Select(kv => kv.ReflectDirectOnDirect(subspace))
            .Aggregate(
                (XGaFloat64Multivector) mv.Processor.CreateZeroScalar(),
                (a, b) => a.Add(b)
            );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar ReflectDirectOnDual(this XGaFloat64Scalar mv, XGaFloat64KVector subspace)
    {
        return mv;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector ReflectDirectOnDual(this XGaFloat64Vector mv, XGaFloat64KVector subspace)
    {
        var mv1 = mv.ReflectOn(subspace);
        
        return subspace.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector ReflectDirectOnDual(this XGaFloat64Bivector mv, XGaFloat64KVector subspace)
    {
        return mv.ReflectOn(subspace);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64HigherKVector ReflectDirectOnDual(this XGaFloat64HigherKVector mv, XGaFloat64KVector subspace)
    {
        var mv1 = mv.ReflectOn(subspace);

        var n = mv.Grade * subspace.Grade;

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector ReflectDirectOnDual(this XGaFloat64KVector mv, XGaFloat64KVector subspace)
    {
        var mv1 = mv.ReflectOn(subspace);

        var n = mv.Grade * subspace.Grade;

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector ReflectDirectOnDual(this XGaFloat64Multivector mv, XGaFloat64KVector subspace)
    {
        return mv
            .GetKVectorParts()
            .Select(kv => kv.ReflectDirectOnDual(subspace))
            .Aggregate(
                (XGaFloat64Multivector) mv.Processor.CreateZeroScalar(),
                (a, b) => a.Add(b)
            );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar ReflectDualOnDirect(this XGaFloat64Scalar mv, XGaFloat64KVector subspace, int vSpaceDimensions)
    {
        var n = subspace.Grade + vSpaceDimensions;

        return n.IsOdd() ? -mv : mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector ReflectDualOnDirect(this XGaFloat64Vector mv, XGaFloat64KVector subspace, int vSpaceDimensions)
    {
        var mv1 = mv.ReflectOn(subspace);

        var n = vSpaceDimensions - 1;

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector ReflectDualOnDirect(this XGaFloat64Bivector mv, XGaFloat64KVector subspace, int vSpaceDimensions)
    {
        var mv1 = mv.ReflectOn(subspace);

        var n = (mv.Grade + 1) * (subspace.Grade + 1) + vSpaceDimensions - 1;

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64HigherKVector ReflectDualOnDirect(this XGaFloat64HigherKVector mv, XGaFloat64KVector subspace, int vSpaceDimensions)
    {
        var mv1 = mv.ReflectOn(subspace);

        var n = (mv.Grade + 1) * (subspace.Grade + 1) + vSpaceDimensions - 1;

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector ReflectDualOnDirect(this XGaFloat64KVector mv, XGaFloat64KVector subspace, int vSpaceDimensions)
    {
        var mv1 = mv.ReflectOn(subspace);

        var n = (mv.Grade + 1) * (subspace.Grade + 1) + vSpaceDimensions - 1;

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector ReflectDualOnDirect(this XGaFloat64Multivector mv, XGaFloat64KVector subspace, int vSpaceDimensions)
    {
        return mv
            .GetKVectorParts()
            .Select(kv => kv.ReflectDualOnDirect(subspace, vSpaceDimensions))
            .Aggregate(
                (XGaFloat64Multivector) mv.Processor.CreateZeroScalar(),
                (a, b) => a.Add(b)
            );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar ReflectDualOnDual(this XGaFloat64Scalar mv, XGaFloat64KVector subspace)
    {
        return subspace.IsOdd() ? -mv : mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector ReflectDualOnDual(this XGaFloat64Vector mv, XGaFloat64KVector subspace)
    {
        return mv.ReflectOn(subspace);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector ReflectDualOnDual(this XGaFloat64Bivector mv, XGaFloat64KVector subspace)
    {
        var mv1 = mv.ReflectOn(subspace);
        
        return subspace.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64HigherKVector ReflectDualOnDual(this XGaFloat64HigherKVector mv, XGaFloat64KVector subspace)
    {
        var mv1 = mv.ReflectOn(subspace);

        var n = (mv.Grade + 1) * subspace.Grade;

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector ReflectDualOnDual(this XGaFloat64KVector mv, XGaFloat64KVector subspace)
    {
        var mv1 = mv.ReflectOn(subspace);

        var n = (mv.Grade + 1) * subspace.Grade;

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector ReflectDualOnDual(this XGaFloat64Multivector mv, XGaFloat64KVector subspace)
    {
        return mv
            .GetKVectorParts()
            .Select(kv => kv.ReflectDualOnDual(subspace))
            .Aggregate(
                (XGaFloat64Multivector) mv.Processor.CreateZeroScalar(),
                (a, b) => a.Add(b)
            );
    }


}