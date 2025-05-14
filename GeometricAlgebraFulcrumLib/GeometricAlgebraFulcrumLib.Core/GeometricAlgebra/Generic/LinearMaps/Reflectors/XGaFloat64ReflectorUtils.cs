using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.LinearMaps.Reflectors;

public static class XGaReflectorUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> ReflectOn<T>(this XGaScalar<T> mv, XGaKVector<T> subspace)
    {
        Debug.Assert(subspace.IsNearBlade());

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> ReflectOn<T>(this XGaVector<T> mv, XGaKVector<T> subspace)
    {
        Debug.Assert(subspace.IsNearBlade());

        return subspace
            .Gp(mv)
            .Gp(subspace.Inverse())
            .GetVectorPart();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> ReflectOn<T>(this XGaBivector<T> mv, XGaKVector<T> subspace)
    {
        Debug.Assert(subspace.IsNearBlade());

        return subspace
            .Gp(mv)
            .Gp(subspace.Inverse())
            .GetBivectorPart();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> ReflectOn<T>(this XGaHigherKVector<T> mv, XGaKVector<T> subspace)
    {
        Debug.Assert(subspace.IsNearBlade());

        return subspace
            .Gp(mv)
            .Gp(subspace.Inverse())
            .GetHigherKVectorPart(mv.Grade);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> ReflectOn<T>(this XGaKVector<T> mv, XGaKVector<T> subspace)
    {
        Debug.Assert(subspace.IsNearBlade());

        return subspace
            .Gp(mv)
            .Gp(subspace.Inverse())
            .GetKVectorPart(mv.Grade);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> ReflectOn<T>(this XGaMultivector<T> mv, XGaKVector<T> subspace)
    {
        Debug.Assert(subspace.IsNearBlade());

        return subspace
            .Gp(mv)
            .Gp(subspace.Inverse());
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> ReflectDirectOnDirect<T>(this XGaScalar<T> mv, XGaKVector<T> subspace)
    {
        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> ReflectDirectOnDirect<T>(this XGaVector<T> mv, XGaKVector<T> subspace)
    {
        var mv1 = mv.ReflectOn(subspace);

        return subspace.IsEven() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> ReflectDirectOnDirect<T>(this XGaBivector<T> mv, XGaKVector<T> subspace)
    {
        return mv.ReflectOn(subspace);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> ReflectDirectOnDirect<T>(this XGaHigherKVector<T> mv, XGaKVector<T> subspace)
    {
        var mv1 = mv.ReflectOn(subspace);

        var n = mv.Grade * (subspace.Grade + 1);

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> ReflectDirectOnDirect<T>(this XGaKVector<T> mv, XGaKVector<T> subspace)
    {
        var mv1 = mv.ReflectOn(subspace);

        var n = mv.Grade * (subspace.Grade + 1);

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> ReflectDirectOnDirect<T>(this XGaMultivector<T> mv, XGaKVector<T> subspace)
    {
        return mv
            .GetKVectorParts()
            .Select(kv => kv.ReflectDirectOnDirect(subspace))
            .Aggregate(
                (XGaMultivector<T>) mv.Processor.ScalarZero,
                (a, b) => a.Add(b)
            );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> ReflectDirectOnDual<T>(this XGaScalar<T> mv, XGaKVector<T> subspace)
    {
        return mv;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> ReflectDirectOnDual<T>(this XGaVector<T> mv, XGaKVector<T> subspace)
    {
        var mv1 = mv.ReflectOn(subspace);
        
        return subspace.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> ReflectDirectOnDual<T>(this XGaBivector<T> mv, XGaKVector<T> subspace)
    {
        return mv.ReflectOn(subspace);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> ReflectDirectOnDual<T>(this XGaHigherKVector<T> mv, XGaKVector<T> subspace)
    {
        var mv1 = mv.ReflectOn(subspace);

        var n = mv.Grade * subspace.Grade;

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> ReflectDirectOnDual<T>(this XGaKVector<T> mv, XGaKVector<T> subspace)
    {
        var mv1 = mv.ReflectOn(subspace);

        var n = mv.Grade * subspace.Grade;

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> ReflectDirectOnDual<T>(this XGaMultivector<T> mv, XGaKVector<T> subspace)
    {
        return mv
            .GetKVectorParts()
            .Select(kv => kv.ReflectDirectOnDual(subspace))
            .Aggregate(
                (XGaMultivector<T>) mv.Processor.ScalarZero,
                (a, b) => a.Add(b)
            );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> ReflectDualOnDirect<T>(this XGaScalar<T> mv, XGaKVector<T> subspace, int vSpaceDimensions)
    {
        var n = subspace.Grade + vSpaceDimensions;

        return n.IsOdd() ? -mv : mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> ReflectDualOnDirect<T>(this XGaVector<T> mv, XGaKVector<T> subspace, int vSpaceDimensions)
    {
        var mv1 = mv.ReflectOn(subspace);

        var n = vSpaceDimensions - 1;

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> ReflectDualOnDirect<T>(this XGaBivector<T> mv, XGaKVector<T> subspace, int vSpaceDimensions)
    {
        var mv1 = mv.ReflectOn(subspace);

        var n = (mv.Grade + 1) * (subspace.Grade + 1) + vSpaceDimensions - 1;

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> ReflectDualOnDirect<T>(this XGaHigherKVector<T> mv, XGaKVector<T> subspace, int vSpaceDimensions)
    {
        var mv1 = mv.ReflectOn(subspace);

        var n = (mv.Grade + 1) * (subspace.Grade + 1) + vSpaceDimensions - 1;

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> ReflectDualOnDirect<T>(this XGaKVector<T> mv, XGaKVector<T> subspace, int vSpaceDimensions)
    {
        var mv1 = mv.ReflectOn(subspace);

        var n = (mv.Grade + 1) * (subspace.Grade + 1) + vSpaceDimensions - 1;

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> ReflectDualOnDirect<T>(this XGaMultivector<T> mv, XGaKVector<T> subspace, int vSpaceDimensions)
    {
        return mv
            .GetKVectorParts()
            .Select(kv => kv.ReflectDualOnDirect(subspace, vSpaceDimensions))
            .Aggregate(
                (XGaMultivector<T>) mv.Processor.ScalarZero,
                (a, b) => a.Add(b)
            );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> ReflectDualOnDual<T>(this XGaScalar<T> mv, XGaKVector<T> subspace)
    {
        return subspace.IsOdd() ? -mv : mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> ReflectDualOnDual<T>(this XGaVector<T> mv, XGaKVector<T> subspace)
    {
        return mv.ReflectOn(subspace);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> ReflectDualOnDual<T>(this XGaBivector<T> mv, XGaKVector<T> subspace)
    {
        var mv1 = mv.ReflectOn(subspace);
        
        return subspace.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> ReflectDualOnDual<T>(this XGaHigherKVector<T> mv, XGaKVector<T> subspace)
    {
        var mv1 = mv.ReflectOn(subspace);

        var n = (mv.Grade + 1) * subspace.Grade;

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> ReflectDualOnDual<T>(this XGaKVector<T> mv, XGaKVector<T> subspace)
    {
        var mv1 = mv.ReflectOn(subspace);

        var n = (mv.Grade + 1) * subspace.Grade;

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> ReflectDualOnDual<T>(this XGaMultivector<T> mv, XGaKVector<T> subspace)
    {
        return mv
            .GetKVectorParts()
            .Select(kv => kv.ReflectDualOnDual(subspace))
            .Aggregate(
                (XGaMultivector<T>) mv.Processor.ScalarZero,
                (a, b) => a.Add(b)
            );
    }


}