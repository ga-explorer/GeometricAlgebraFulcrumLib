using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Encoding;

public static class XGaConformalEncodeEGaUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaVector<T> EncodeEGaVectorAsVector<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> x, Scalar<T> y)
    {
        Debug.Assert(conformalSpace.Is4D);

        var zero = conformalSpace.ScalarProcessor.Zero;

        return conformalSpace.ConformalProcessor.Vector(zero, zero, x, y);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaVector<T> EncodeEGaVectorAsVector<T>(this XGaConformalSpace<T> conformalSpace, ILinVector2D<T> mv)
    {
        Debug.Assert(conformalSpace.Is4D);

        var zero = conformalSpace.ScalarProcessor.Zero;

        return conformalSpace.ConformalProcessor.Vector(zero, zero, mv.X, mv.Y);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaVector<T> EncodeEGaVectorAsVector<T>(this XGaConformalSpace<T> conformalSpace, T x, T y)
    {
        Debug.Assert(conformalSpace.Is5D);

        var zero = conformalSpace.ScalarProcessor.ZeroValue;

        return conformalSpace.ConformalProcessor.Vector(zero, zero, x, y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaVector<T> EncodeEGaVectorAsVector<T>(this XGaConformalSpace<T> conformalSpace, T x, T y, T z)
    {
        Debug.Assert(conformalSpace.Is5D);

        var zero = conformalSpace.ScalarProcessor.ZeroValue;

        return conformalSpace.ConformalProcessor.Vector(zero, zero, x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaVector<T> EncodeEGaVectorAsVector<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> x, Scalar<T> y, Scalar<T> z)
    {
        Debug.Assert(conformalSpace.Is5D);

        var zero = conformalSpace.ScalarProcessor.Zero;

        return conformalSpace.ConformalProcessor.Vector(zero, zero, x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaVector<T> EncodeEGaVectorAsVector<T>(this XGaConformalSpace<T> conformalSpace, ILinVector3D<T> mv)
    {
        Debug.Assert(conformalSpace.Is5D);

        var zero = conformalSpace.ScalarProcessor.Zero;

        return conformalSpace.ConformalProcessor.Vector(zero, zero, mv.X, mv.Y, mv.Z);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaVector<T> EncodeEGaVectorAsVector<T>(this XGaConformalSpace<T> conformalSpace, LinVector<T> mv)
    {
        var composer = conformalSpace.ConformalProcessor.CreateComposer();

        foreach (var (index, scalar) in mv.IndexScalarPairs)
            composer.SetVectorTerm(index + 2, scalar);

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaVector<T> EncodeEGaVectorAsVector<T>(this XGaConformalSpace<T> conformalSpace, XGaVector<T> mv)
    {
        Debug.Assert(mv.Processor.IsEuclidean);

        var composer = conformalSpace.ConformalProcessor.CreateComposer();

        foreach (var (index, scalar) in mv.IndexScalarPairs)
            composer.SetVectorTerm(index + 2, scalar);

        return composer.GetVector();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaBivector<T> EncodeEGaBivectorAsBivector<T>(this XGaConformalSpace<T> conformalSpace, int xy)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.ConformalProcessor
            .CreateComposer()
            .SetBivectorTerm(2, 3, xy)
            .GetBivector();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaBivector<T> EncodeEGaBivectorAsBivector<T>(this XGaConformalSpace<T> conformalSpace, float xy)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.ConformalProcessor
            .CreateComposer()
            .SetBivectorTerm(2, 3, xy)
            .GetBivector();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaBivector<T> EncodeEGaBivectorAsBivector<T>(this XGaConformalSpace<T> conformalSpace, double xy)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.ConformalProcessor
            .CreateComposer()
            .SetBivectorTerm(2, 3, xy)
            .GetBivector();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaBivector<T> EncodeEGaBivectorAsBivector<T>(this XGaConformalSpace<T> conformalSpace, T xy)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.ConformalProcessor
            .CreateComposer()
            .SetBivectorTerm(2, 3, xy)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaBivector<T> EncodeEGaBivectorAsBivector<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> xy)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.ConformalProcessor
            .CreateComposer()
            .SetBivectorTerm(2, 3, xy)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaBivector<T> EncodeEGaBivectorAsBivector<T>(this XGaConformalSpace<T> conformalSpace, LinBivector2D<T> bivector)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.ConformalProcessor
            .CreateComposer()
            .SetBivectorTerm(2, 3, bivector.Xy)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaBivector<T> EncodeEGaBivectorAsBivector<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> xy, Scalar<T> xz, Scalar<T> yz)
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
    internal static XGaBivector<T> EncodeEGaBivectorAsBivector<T>(this XGaConformalSpace<T> conformalSpace, LinBivector3D<T> bivector)
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
    internal static XGaBivector<T> EncodeEGaBivectorAsBivector<T>(this XGaConformalSpace<T> conformalSpace, XGaBivector<T> mv)
    {
        Debug.Assert(mv.Processor.IsEuclidean);

        var composer = conformalSpace.ConformalProcessor.CreateComposer();

        foreach (var (id, scalar) in mv.IdScalarPairs)
            composer.SetTerm(id.ShiftIndices(2), scalar);

        return composer.GetBivector();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaKVector<T> EncodeEGaTrivectorAsKVector<T>(this XGaConformalSpace<T> conformalSpace, int xyzScalar)
    {
        Debug.Assert(conformalSpace.Is5D);

        return xyzScalar * conformalSpace.Ie.InternalKVector;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaKVector<T> EncodeEGaTrivectorAsKVector<T>(this XGaConformalSpace<T> conformalSpace, float xyzScalar)
    {
        Debug.Assert(conformalSpace.Is5D);

        return xyzScalar * conformalSpace.Ie.InternalKVector;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaKVector<T> EncodeEGaTrivectorAsKVector<T>(this XGaConformalSpace<T> conformalSpace, double xyzScalar)
    {
        Debug.Assert(conformalSpace.Is5D);

        return xyzScalar * conformalSpace.Ie.InternalKVector;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaKVector<T> EncodeEGaTrivectorAsKVector<T>(this XGaConformalSpace<T> conformalSpace, T xyzScalar)
    {
        Debug.Assert(conformalSpace.Is5D);
        Debug.Assert(xyzScalar is not null);

        return xyzScalar * conformalSpace.Ie.InternalKVector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaKVector<T> EncodeEGaTrivectorAsKVector<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> xyzScalar)
    {
        Debug.Assert(conformalSpace.Is5D);

        return xyzScalar * conformalSpace.Ie.InternalKVector;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaKVector<T> EncodeEGaTrivectorAsKVector<T>(this XGaConformalSpace<T> conformalSpace, LinTrivector3D<T> trivector)
    {
        Debug.Assert(conformalSpace.Is5D);

        return trivector.Scalar123 * conformalSpace.Ie.InternalKVector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaKVector<T> EncodeEGaKVectorAsKVector<T>(this XGaConformalSpace<T> conformalSpace, XGaKVector<T> mv)
    {
        Debug.Assert(mv.Processor.IsEuclidean);

        var composer = conformalSpace.ConformalProcessor.CreateComposer();

        foreach (var (id, scalar) in mv.IdScalarPairs)
            composer.SetTerm(id.ShiftIndices(2), scalar);

        return composer.GetKVector(mv.Grade);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeEGaVectorBlade<T>(this ILinVector2D<T> egaKVector, XGaConformalSpace<T> conformalSpace)
    {
        return conformalSpace.EncodeEGaVector(egaKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeEGaVectorBlade<T>(this ILinVector3D<T> egaKVector, XGaConformalSpace<T> conformalSpace)
    {
        return conformalSpace.EncodeEGaVector(egaKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeEGaVectorBlade<T>(this LinVector<T> egaKVector, XGaConformalSpace<T> conformalSpace)
    {
        return conformalSpace.EncodeEGaVector(egaKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeEGaVectorBlade<T>(this XGaVector<T> egaKVector, XGaConformalSpace<T> conformalSpace)
    {
        return conformalSpace.EncodeEGaVector(egaKVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeEGaBivectorBlade<T>(this LinBivector2D<T> egaKVector, XGaConformalSpace<T> conformalSpace)
    {
        return conformalSpace.EncodeEGaBivector(egaKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeEGaBivectorBlade<T>(this LinBivector3D<T> egaKVector, XGaConformalSpace<T> conformalSpace)
    {
        return conformalSpace.EncodeEGaBivector(egaKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeEGaBivectorBlade<T>(this XGaBivector<T> egaKVector, XGaConformalSpace<T> conformalSpace)
    {
        return conformalSpace.EncodeEGaBivector(egaKVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeEGaTrivectorBlade<T>(this LinTrivector3D<T> egaKVector, XGaConformalSpace<T> conformalSpace)
    {
        return conformalSpace.EncodeEGaTrivector(egaKVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeEGaBlade<T>(this XGaKVector<T> egaKVector, XGaConformalSpace<T> conformalSpace)
    {
        return conformalSpace.EncodeEGaBlade(egaKVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeEGaVector<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> x, Scalar<T> y)
    {
        return new XGaConformalBlade<T>(
            conformalSpace, 
            conformalSpace.EncodeEGaVectorAsVector(x, y)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeEGaVector<T>(this XGaConformalSpace<T> conformalSpace, ILinVector2D<T> mv)
    {
        return new XGaConformalBlade<T>(
            conformalSpace, 
            conformalSpace.EncodeEGaVectorAsVector(mv)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeEGaVector<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> x, Scalar<T> y, Scalar<T> z)
    {
        return new XGaConformalBlade<T>(
            conformalSpace, 
            conformalSpace.EncodeEGaVectorAsVector(x, y, z)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeEGaVector<T>(this XGaConformalSpace<T> conformalSpace, ILinVector3D<T> mv)
    {
        return new XGaConformalBlade<T>(
            conformalSpace, 
            conformalSpace.EncodeEGaVectorAsVector(mv)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeEGaVector<T>(this XGaConformalSpace<T> conformalSpace, LinVector<T> mv)
    {
        return new XGaConformalBlade<T>(
            conformalSpace, 
            conformalSpace.EncodeEGaVectorAsVector(mv)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeEGaVector<T>(this XGaConformalSpace<T> conformalSpace, XGaVector<T> mv)
    {
        return new XGaConformalBlade<T>(
            conformalSpace, 
            conformalSpace.EncodeEGaVectorAsVector(mv)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeEGaBivector<T>(this XGaConformalSpace<T> conformalSpace, int xy)
    {
        return new XGaConformalBlade<T>(
            conformalSpace, 
            conformalSpace.EncodeEGaBivectorAsBivector(xy)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeEGaBivector<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> xy)
    {
        return new XGaConformalBlade<T>(
            conformalSpace, 
            conformalSpace.EncodeEGaBivectorAsBivector(xy)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeEGaBivector<T>(this XGaConformalSpace<T> conformalSpace, LinBivector2D<T> bivector)
    {
        return new XGaConformalBlade<T>(
            conformalSpace, 
            conformalSpace.EncodeEGaBivectorAsBivector(bivector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeEGaBivector<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> xy, Scalar<T> xz, Scalar<T> yz)
    {
        return new XGaConformalBlade<T>(
            conformalSpace, 
            conformalSpace.EncodeEGaBivectorAsBivector(xy, xz, yz)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeEGaBivector<T>(this XGaConformalSpace<T> conformalSpace, LinBivector3D<T> bivector)
    {
        return new XGaConformalBlade<T>(
            conformalSpace, 
            conformalSpace.EncodeEGaBivectorAsBivector(bivector)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeEGaBivector<T>(this XGaConformalSpace<T> conformalSpace, XGaBivector<T> mv)
    {
        return new XGaConformalBlade<T>(
            conformalSpace, 
            conformalSpace.EncodeEGaBivectorAsBivector(mv)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeEGaTrivector<T>(this XGaConformalSpace<T> conformalSpace, int xyzScalar)
    {
        return new XGaConformalBlade<T>(
            conformalSpace, 
            conformalSpace.EncodeEGaTrivectorAsKVector(xyzScalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeEGaTrivector<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> xyzScalar)
    {
        return new XGaConformalBlade<T>(
            conformalSpace, 
            conformalSpace.EncodeEGaTrivectorAsKVector(xyzScalar)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeEGaTrivector<T>(this XGaConformalSpace<T> conformalSpace, LinTrivector3D<T> trivector)
    {
        return new XGaConformalBlade<T>(
            conformalSpace, 
            conformalSpace.EncodeEGaTrivectorAsKVector(trivector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeEGaBlade<T>(this XGaConformalSpace<T> conformalSpace, XGaKVector<T> mv)
    {
        return new XGaConformalBlade<T>(
            conformalSpace, 
            conformalSpace.EncodeEGaKVectorAsKVector(mv)
        );
    }
}