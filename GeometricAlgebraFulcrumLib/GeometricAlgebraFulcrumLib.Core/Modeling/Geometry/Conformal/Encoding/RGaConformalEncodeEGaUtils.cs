using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Encoding;

public static class RGaConformalEncodeEGaUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaFloat64Vector EncodeEGaVectorAsVector(this RGaConformalSpace conformalSpace, double x, double y)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.ConformalProcessor.Vector(0, 0, x, y);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaFloat64Vector EncodeEGaVectorAsVector(this RGaConformalSpace conformalSpace, ILinFloat64Vector2D mv)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.ConformalProcessor.Vector(0, 0, mv.X, mv.Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaFloat64Vector EncodeEGaVectorAsVector(this RGaConformalSpace conformalSpace, double x, double y, double z)
    {
        Debug.Assert(conformalSpace.Is5D);

        return conformalSpace.ConformalProcessor.Vector(0, 0, x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaFloat64Vector EncodeEGaVectorAsVector(this RGaConformalSpace conformalSpace, ILinFloat64Vector3D mv)
    {
        Debug.Assert(conformalSpace.Is5D);

        return conformalSpace.ConformalProcessor.Vector(0, 0, mv.X, mv.Y, mv.Z);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaFloat64Vector EncodeEGaVectorAsVector(this RGaConformalSpace conformalSpace, LinFloat64Vector mv)
    {
        var composer = conformalSpace.ConformalProcessor.CreateComposer();

        foreach (var (index, scalar) in mv.IndexScalarPairs)
            composer.SetVectorTerm(index + 2, scalar);

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaFloat64Vector EncodeEGaVectorAsVector(this RGaConformalSpace conformalSpace, RGaFloat64Vector mv)
    {
        Debug.Assert(mv.Processor.IsEuclidean);

        var composer = conformalSpace.ConformalProcessor.CreateComposer();

        foreach (var (index, scalar) in mv.IndexScalarPairs)
            composer.SetVectorTerm(index + 2, scalar);

        return composer.GetVector();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaFloat64Bivector EncodeEGaBivectorAsBivector(this RGaConformalSpace conformalSpace, double xy)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.ConformalProcessor
            .CreateComposer()
            .SetBivectorTerm(2, 3, xy)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaFloat64Bivector EncodeEGaBivectorAsBivector(this RGaConformalSpace conformalSpace, LinFloat64Bivector2D bivector)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.ConformalProcessor
            .CreateComposer()
            .SetBivectorTerm(2, 3, bivector.Xy)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaFloat64Bivector EncodeEGaBivectorAsBivector(this RGaConformalSpace conformalSpace, double xy, double xz, double yz)
    {
        Debug.Assert(conformalSpace.Is5D);

        return conformalSpace.ConformalProcessor
            .CreateComposer()
            .SetBivectorTerm(2, 3, xy)
            .SetBivectorTerm(2, 4, xz)
            .SetBivectorTerm(3, 4, yz)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaFloat64Bivector EncodeEGaBivectorAsBivector(this RGaConformalSpace conformalSpace, LinFloat64Bivector3D bivector)
    {
        Debug.Assert(conformalSpace.Is5D);

        return conformalSpace.ConformalProcessor
            .CreateComposer()
            .SetBivectorTerm(2, 3, bivector.Xy)
            .SetBivectorTerm(2, 4, bivector.Xz)
            .SetBivectorTerm(3, 4, bivector.Yz)
            .GetBivector();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaFloat64Bivector EncodeEGaBivectorAsBivector(this RGaConformalSpace conformalSpace, RGaFloat64Bivector mv)
    {
        Debug.Assert(mv.Processor.IsEuclidean);

        var composer = conformalSpace.ConformalProcessor.CreateComposer();

        foreach (var (id, scalar) in mv.IdScalarPairs)
            composer.SetTerm(id << 2, scalar);

        return composer.GetBivector();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaFloat64KVector EncodeEGaTrivectorAsKVector(this RGaConformalSpace conformalSpace, double xyzScalar)
    {
        Debug.Assert(conformalSpace.Is5D);

        return xyzScalar * conformalSpace.Ie.InternalKVector;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaFloat64KVector EncodeEGaTrivectorAsKVector(this RGaConformalSpace conformalSpace, LinFloat64Trivector3D trivector)
    {
        Debug.Assert(conformalSpace.Is5D);

        return trivector.Scalar123 * conformalSpace.Ie.InternalKVector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaFloat64KVector EncodeEGaKVectorAsKVector(this RGaConformalSpace conformalSpace, RGaFloat64KVector mv)
    {
        Debug.Assert(mv.Processor.IsEuclidean);

        var composer = conformalSpace.ConformalProcessor.CreateComposer();

        foreach (var (id, scalar) in mv.IdScalarPairs)
            composer.SetTerm(id << 2, scalar);

        return composer.GetKVector(mv.Grade);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeEGaVectorBlade(this ILinFloat64Vector2D egaKVector, RGaConformalSpace conformalSpace)
    {
        return conformalSpace.EncodeEGaVector(egaKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeEGaVectorBlade(this ILinFloat64Vector3D egaKVector, RGaConformalSpace conformalSpace)
    {
        return conformalSpace.EncodeEGaVector(egaKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeEGaVectorBlade(this LinFloat64Vector egaKVector, RGaConformalSpace conformalSpace)
    {
        return conformalSpace.EncodeEGaVector(egaKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeEGaVectorBlade(this RGaFloat64Vector egaKVector, RGaConformalSpace conformalSpace)
    {
        return conformalSpace.EncodeEGaVector(egaKVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeEGaBivectorBlade(this LinFloat64Bivector2D egaKVector, RGaConformalSpace conformalSpace)
    {
        return conformalSpace.EncodeEGaBivector(egaKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeEGaBivectorBlade(this LinFloat64Bivector3D egaKVector, RGaConformalSpace conformalSpace)
    {
        return conformalSpace.EncodeEGaBivector(egaKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeEGaBivectorBlade(this RGaFloat64Bivector egaKVector, RGaConformalSpace conformalSpace)
    {
        return conformalSpace.EncodeEGaBivector(egaKVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeEGaTrivectorBlade(this LinFloat64Trivector3D egaKVector, RGaConformalSpace conformalSpace)
    {
        return conformalSpace.EncodeEGaTrivector(egaKVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeEGaBlade(this RGaFloat64KVector egaKVector, RGaConformalSpace conformalSpace)
    {
        return conformalSpace.EncodeEGaBlade(egaKVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeEGaVector(this RGaConformalSpace conformalSpace, double x, double y)
    {
        return new RGaConformalBlade(
            conformalSpace, 
            conformalSpace.EncodeEGaVectorAsVector(x, y)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeEGaVector(this RGaConformalSpace conformalSpace, ILinFloat64Vector2D mv)
    {
        return new RGaConformalBlade(
            conformalSpace, 
            conformalSpace.EncodeEGaVectorAsVector(mv)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeEGaVector(this RGaConformalSpace conformalSpace, double x, double y, double z)
    {
        return new RGaConformalBlade(
            conformalSpace, 
            conformalSpace.EncodeEGaVectorAsVector(x, y, z)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeEGaVector(this RGaConformalSpace conformalSpace, ILinFloat64Vector3D mv)
    {
        return new RGaConformalBlade(
            conformalSpace, 
            conformalSpace.EncodeEGaVectorAsVector(mv)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeEGaVector(this RGaConformalSpace conformalSpace, LinFloat64Vector mv)
    {
        return new RGaConformalBlade(
            conformalSpace, 
            conformalSpace.EncodeEGaVectorAsVector(mv)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeEGaVector(this RGaConformalSpace conformalSpace, RGaFloat64Vector mv)
    {
        return new RGaConformalBlade(
            conformalSpace, 
            conformalSpace.EncodeEGaVectorAsVector(mv)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeEGaBivector(this RGaConformalSpace conformalSpace, double xy)
    {
        return new RGaConformalBlade(
            conformalSpace, 
            conformalSpace.EncodeEGaBivectorAsBivector(xy)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeEGaBivector(this RGaConformalSpace conformalSpace, LinFloat64Bivector2D bivector)
    {
        return new RGaConformalBlade(
            conformalSpace, 
            conformalSpace.EncodeEGaBivectorAsBivector(bivector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeEGaBivector(this RGaConformalSpace conformalSpace, double xy, double xz, double yz)
    {
        return new RGaConformalBlade(
            conformalSpace, 
            conformalSpace.EncodeEGaBivectorAsBivector(xy, xz, yz)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeEGaBivector(this RGaConformalSpace conformalSpace, LinFloat64Bivector3D bivector)
    {
        return new RGaConformalBlade(
            conformalSpace, 
            conformalSpace.EncodeEGaBivectorAsBivector(bivector)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeEGaBivector(this RGaConformalSpace conformalSpace, RGaFloat64Bivector mv)
    {
        return new RGaConformalBlade(
            conformalSpace, 
            conformalSpace.EncodeEGaBivectorAsBivector(mv)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeEGaTrivector(this RGaConformalSpace conformalSpace, double xyzScalar)
    {
        return new RGaConformalBlade(
            conformalSpace, 
            conformalSpace.EncodeEGaTrivectorAsKVector(xyzScalar)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeEGaTrivector(this RGaConformalSpace conformalSpace, LinFloat64Trivector3D trivector)
    {
        return new RGaConformalBlade(
            conformalSpace, 
            conformalSpace.EncodeEGaTrivectorAsKVector(trivector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeEGaBlade(this RGaConformalSpace conformalSpace, RGaFloat64KVector mv)
    {
        return new RGaConformalBlade(
            conformalSpace, 
            conformalSpace.EncodeEGaKVectorAsKVector(mv)
        );
    }
}