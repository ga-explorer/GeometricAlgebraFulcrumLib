using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.LinearMaps.Reflectors;

public static class RGaReflectorUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScalar<T> ReflectOn<T>(this RGaScalar<T> mv, RGaKVector<T> subspace)
    {
        Debug.Assert(subspace.IsNearBlade());

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> ReflectOn<T>(this RGaVector<T> mv, RGaKVector<T> subspace)
    {
        Debug.Assert(subspace.IsNearBlade());

        return subspace
            .Gp(mv)
            .Gp(subspace.Inverse())
            .GetVectorPart();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaBivector<T> ReflectOn<T>(this RGaBivector<T> mv, RGaKVector<T> subspace)
    {
        Debug.Assert(subspace.IsNearBlade());

        return subspace
            .Gp(mv)
            .Gp(subspace.Inverse())
            .GetBivectorPart();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaHigherKVector<T> ReflectOn<T>(this RGaHigherKVector<T> mv, RGaKVector<T> subspace)
    {
        Debug.Assert(subspace.IsNearBlade());

        return subspace
            .Gp(mv)
            .Gp(subspace.Inverse())
            .GetHigherKVectorPart(mv.Grade);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> ReflectOn<T>(this RGaKVector<T> mv, RGaKVector<T> subspace)
    {
        Debug.Assert(subspace.IsNearBlade());

        return subspace
            .Gp(mv)
            .Gp(subspace.Inverse())
            .GetKVectorPart(mv.Grade);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> ReflectOn<T>(this RGaMultivector<T> mv, RGaKVector<T> subspace)
    {
        Debug.Assert(subspace.IsNearBlade());

        return subspace
            .Gp(mv)
            .Gp(subspace.Inverse());
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScalar<T> ReflectDirectOnDirect<T>(this RGaScalar<T> mv, RGaKVector<T> subspace)
    {
        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> ReflectDirectOnDirect<T>(this RGaVector<T> mv, RGaKVector<T> subspace)
    {
        var mv1 = mv.ReflectOn(subspace);

        return subspace.IsEven() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaBivector<T> ReflectDirectOnDirect<T>(this RGaBivector<T> mv, RGaKVector<T> subspace)
    {
        return mv.ReflectOn(subspace);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaHigherKVector<T> ReflectDirectOnDirect<T>(this RGaHigherKVector<T> mv, RGaKVector<T> subspace)
    {
        var mv1 = mv.ReflectOn(subspace);

        var n = mv.Grade * (subspace.Grade + 1);

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> ReflectDirectOnDirect<T>(this RGaKVector<T> mv, RGaKVector<T> subspace)
    {
        var mv1 = mv.ReflectOn(subspace);

        var n = mv.Grade * (subspace.Grade + 1);

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> ReflectDirectOnDirect<T>(this RGaMultivector<T> mv, RGaKVector<T> subspace)
    {
        return mv
            .GetKVectorParts()
            .Select(kv => kv.ReflectDirectOnDirect(subspace))
            .Aggregate(
                (RGaMultivector<T>) mv.Processor.ScalarZero,
                (a, b) => a.Add(b)
            );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScalar<T> ReflectDirectOnDual<T>(this RGaScalar<T> mv, RGaKVector<T> subspace)
    {
        return mv;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> ReflectDirectOnDual<T>(this RGaVector<T> mv, RGaKVector<T> subspace)
    {
        var mv1 = mv.ReflectOn(subspace);
        
        return subspace.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaBivector<T> ReflectDirectOnDual<T>(this RGaBivector<T> mv, RGaKVector<T> subspace)
    {
        return mv.ReflectOn(subspace);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaHigherKVector<T> ReflectDirectOnDual<T>(this RGaHigherKVector<T> mv, RGaKVector<T> subspace)
    {
        var mv1 = mv.ReflectOn(subspace);

        var n = mv.Grade * subspace.Grade;

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> ReflectDirectOnDual<T>(this RGaKVector<T> mv, RGaKVector<T> subspace)
    {
        var mv1 = mv.ReflectOn(subspace);

        var n = mv.Grade * subspace.Grade;

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> ReflectDirectOnDual<T>(this RGaMultivector<T> mv, RGaKVector<T> subspace)
    {
        return mv
            .GetKVectorParts()
            .Select(kv => kv.ReflectDirectOnDual(subspace))
            .Aggregate(
                (RGaMultivector<T>) mv.Processor.ScalarZero,
                (a, b) => a.Add(b)
            );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScalar<T> ReflectDualOnDirect<T>(this RGaScalar<T> mv, RGaKVector<T> subspace, int vSpaceDimensions)
    {
        var n = subspace.Grade + vSpaceDimensions;

        return n.IsOdd() ? -mv : mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> ReflectDualOnDirect<T>(this RGaVector<T> mv, RGaKVector<T> subspace, int vSpaceDimensions)
    {
        var mv1 = mv.ReflectOn(subspace);

        var n = vSpaceDimensions - 1;

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaBivector<T> ReflectDualOnDirect<T>(this RGaBivector<T> mv, RGaKVector<T> subspace, int vSpaceDimensions)
    {
        var mv1 = mv.ReflectOn(subspace);

        var n = (mv.Grade + 1) * (subspace.Grade + 1) + vSpaceDimensions - 1;

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaHigherKVector<T> ReflectDualOnDirect<T>(this RGaHigherKVector<T> mv, RGaKVector<T> subspace, int vSpaceDimensions)
    {
        var mv1 = mv.ReflectOn(subspace);

        var n = (mv.Grade + 1) * (subspace.Grade + 1) + vSpaceDimensions - 1;

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> ReflectDualOnDirect<T>(this RGaKVector<T> mv, RGaKVector<T> subspace, int vSpaceDimensions)
    {
        var mv1 = mv.ReflectOn(subspace);

        var n = (mv.Grade + 1) * (subspace.Grade + 1) + vSpaceDimensions - 1;

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> ReflectDualOnDirect<T>(this RGaMultivector<T> mv, RGaKVector<T> subspace, int vSpaceDimensions)
    {
        return mv
            .GetKVectorParts()
            .Select(kv => kv.ReflectDualOnDirect(subspace, vSpaceDimensions))
            .Aggregate(
                (RGaMultivector<T>) mv.Processor.ScalarZero,
                (a, b) => a.Add(b)
            );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScalar<T> ReflectDualOnDual<T>(this RGaScalar<T> mv, RGaKVector<T> subspace)
    {
        return subspace.IsOdd() ? -mv : mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> ReflectDualOnDual<T>(this RGaVector<T> mv, RGaKVector<T> subspace)
    {
        return mv.ReflectOn(subspace);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaBivector<T> ReflectDualOnDual<T>(this RGaBivector<T> mv, RGaKVector<T> subspace)
    {
        var mv1 = mv.ReflectOn(subspace);
        
        return subspace.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaHigherKVector<T> ReflectDualOnDual<T>(this RGaHigherKVector<T> mv, RGaKVector<T> subspace)
    {
        var mv1 = mv.ReflectOn(subspace);

        var n = (mv.Grade + 1) * subspace.Grade;

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> ReflectDualOnDual<T>(this RGaKVector<T> mv, RGaKVector<T> subspace)
    {
        var mv1 = mv.ReflectOn(subspace);

        var n = (mv.Grade + 1) * subspace.Grade;

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> ReflectDualOnDual<T>(this RGaMultivector<T> mv, RGaKVector<T> subspace)
    {
        return mv
            .GetKVectorParts()
            .Select(kv => kv.ReflectDualOnDual(subspace))
            .Aggregate(
                (RGaMultivector<T>) mv.Processor.ScalarZero,
                (a, b) => a.Add(b)
            );
    }


}