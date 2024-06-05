using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Projective.Blades;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Projective.Encoding;

public static class XGaProjectiveEncodeEGaUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaVector<T> EncodeEGaVectorAsVector<T>(this XGaProjectiveSpace<T> projectiveSpace, Scalar<T> x, Scalar<T> y)
    {
        Debug.Assert(projectiveSpace.Is3D);

        var zero = projectiveSpace.ScalarProcessor.Zero;

        return projectiveSpace.ProjectiveProcessor.Vector(zero, x, y);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaVector<T> EncodeEGaVectorAsVector<T>(this XGaProjectiveSpace<T> projectiveSpace, ILinVector2D<T> mv)
    {
        Debug.Assert(projectiveSpace.Is3D);

        var zero = projectiveSpace.ScalarProcessor.Zero;

        return projectiveSpace.ProjectiveProcessor.Vector(zero, mv.X, mv.Y);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaVector<T> EncodeEGaVectorAsVector<T>(this XGaProjectiveSpace<T> projectiveSpace, T x, T y)
    {
        Debug.Assert(projectiveSpace.Is4D);

        var zero = projectiveSpace.ScalarProcessor.ZeroValue;

        return projectiveSpace.ProjectiveProcessor.Vector(zero, x, y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaVector<T> EncodeEGaVectorAsVector<T>(this XGaProjectiveSpace<T> projectiveSpace, T x, T y, T z)
    {
        Debug.Assert(projectiveSpace.Is4D);

        var zero = projectiveSpace.ScalarProcessor.ZeroValue;

        return projectiveSpace.ProjectiveProcessor.Vector(zero, x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaVector<T> EncodeEGaVectorAsVector<T>(this XGaProjectiveSpace<T> projectiveSpace, Scalar<T> x, Scalar<T> y, Scalar<T> z)
    {
        Debug.Assert(projectiveSpace.Is4D);

        var zero = projectiveSpace.ScalarProcessor.Zero;

        return projectiveSpace.ProjectiveProcessor.Vector(zero, x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaVector<T> EncodeEGaVectorAsVector<T>(this XGaProjectiveSpace<T> projectiveSpace, ILinVector3D<T> mv)
    {
        Debug.Assert(projectiveSpace.Is4D);

        var zero = projectiveSpace.ScalarProcessor.Zero;

        return projectiveSpace.ProjectiveProcessor.Vector(zero, mv.X, mv.Y, mv.Z);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaVector<T> EncodeEGaVectorAsVector<T>(this XGaProjectiveSpace<T> projectiveSpace, LinVector<T> mv)
    {
        var composer = projectiveSpace.ProjectiveProcessor.CreateComposer();

        foreach (var (index, scalar) in mv.IndexScalarPairs)
            composer.SetVectorTerm(index + 1, scalar);

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaVector<T> EncodeEGaVectorAsVector<T>(this XGaProjectiveSpace<T> projectiveSpace, XGaVector<T> mv)
    {
        Debug.Assert(mv.Processor.IsEuclidean);

        var composer = projectiveSpace.ProjectiveProcessor.CreateComposer();

        foreach (var (index, scalar) in mv.IndexScalarPairs)
            composer.SetVectorTerm(index + 1, scalar);

        return composer.GetVector();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaBivector<T> EncodeEGaBivectorAsBivector<T>(this XGaProjectiveSpace<T> projectiveSpace, int xy)
    {
        Debug.Assert(projectiveSpace.Is3D);

        return projectiveSpace.ProjectiveProcessor
            .CreateComposer()
            .SetBivectorTerm(1, 2, xy)
            .GetBivector();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaBivector<T> EncodeEGaBivectorAsBivector<T>(this XGaProjectiveSpace<T> projectiveSpace, float xy)
    {
        Debug.Assert(projectiveSpace.Is3D);

        return projectiveSpace.ProjectiveProcessor
            .CreateComposer()
            .SetBivectorTerm(1, 2, xy)
            .GetBivector();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaBivector<T> EncodeEGaBivectorAsBivector<T>(this XGaProjectiveSpace<T> projectiveSpace, double xy)
    {
        Debug.Assert(projectiveSpace.Is3D);

        return projectiveSpace.ProjectiveProcessor
            .CreateComposer()
            .SetBivectorTerm(1, 2, xy)
            .GetBivector();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaBivector<T> EncodeEGaBivectorAsBivector<T>(this XGaProjectiveSpace<T> projectiveSpace, T xy)
    {
        Debug.Assert(projectiveSpace.Is3D);

        return projectiveSpace.ProjectiveProcessor
            .CreateComposer()
            .SetBivectorTerm(1, 2, xy)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaBivector<T> EncodeEGaBivectorAsBivector<T>(this XGaProjectiveSpace<T> projectiveSpace, Scalar<T> xy)
    {
        Debug.Assert(projectiveSpace.Is3D);

        return projectiveSpace.ProjectiveProcessor
            .CreateComposer()
            .SetBivectorTerm(1, 2, xy)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaBivector<T> EncodeEGaBivectorAsBivector<T>(this XGaProjectiveSpace<T> projectiveSpace, LinBivector2D<T> bivector)
    {
        Debug.Assert(projectiveSpace.Is3D);

        return projectiveSpace.ProjectiveProcessor
            .CreateComposer()
            .SetBivectorTerm(1, 2, bivector.Xy)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaBivector<T> EncodeEGaBivectorAsBivector<T>(this XGaProjectiveSpace<T> projectiveSpace, Scalar<T> xy, Scalar<T> xz, Scalar<T> yz)
    {
        Debug.Assert(projectiveSpace.Is4D);

        return projectiveSpace.ProjectiveProcessor
            .CreateComposer()
            .SetBivectorTerm(1, 2, xy)
            .SetBivectorTerm(1, 3, xz)
            .SetBivectorTerm(2, 3, yz)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaBivector<T> EncodeEGaBivectorAsBivector<T>(this XGaProjectiveSpace<T> projectiveSpace, LinBivector3D<T> bivector)
    {
        Debug.Assert(projectiveSpace.Is4D);

        return projectiveSpace.ProjectiveProcessor
            .CreateComposer()
            .SetBivectorTerm(1, 2, bivector.Xy)
            .SetBivectorTerm(1, 3, bivector.Xz)
            .SetBivectorTerm(2, 3, bivector.Yz)
            .GetBivector();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaBivector<T> EncodeEGaBivectorAsBivector<T>(this XGaProjectiveSpace<T> projectiveSpace, XGaBivector<T> mv)
    {
        Debug.Assert(mv.Processor.IsEuclidean);

        var composer = projectiveSpace.ProjectiveProcessor.CreateComposer();

        foreach (var (id, scalar) in mv.IdScalarPairs)
            composer.SetTerm(id.ShiftIndices(1), scalar);

        return composer.GetBivector();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaKVector<T> EncodeEGaTrivectorAsKVector<T>(this XGaProjectiveSpace<T> projectiveSpace, int xyzScalar)
    {
        Debug.Assert(projectiveSpace.Is4D);

        return xyzScalar * projectiveSpace.Ie.InternalKVector;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaKVector<T> EncodeEGaTrivectorAsKVector<T>(this XGaProjectiveSpace<T> projectiveSpace, float xyzScalar)
    {
        Debug.Assert(projectiveSpace.Is4D);

        return xyzScalar * projectiveSpace.Ie.InternalKVector;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaKVector<T> EncodeEGaTrivectorAsKVector<T>(this XGaProjectiveSpace<T> projectiveSpace, double xyzScalar)
    {
        Debug.Assert(projectiveSpace.Is4D);

        return xyzScalar * projectiveSpace.Ie.InternalKVector;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaKVector<T> EncodeEGaTrivectorAsKVector<T>(this XGaProjectiveSpace<T> projectiveSpace, T xyzScalar)
    {
        Debug.Assert(projectiveSpace.Is4D);
        Debug.Assert(xyzScalar is not null);

        return xyzScalar * projectiveSpace.Ie.InternalKVector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaKVector<T> EncodeEGaTrivectorAsKVector<T>(this XGaProjectiveSpace<T> projectiveSpace, Scalar<T> xyzScalar)
    {
        Debug.Assert(projectiveSpace.Is4D);

        return xyzScalar * projectiveSpace.Ie.InternalKVector;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaKVector<T> EncodeEGaTrivectorAsKVector<T>(this XGaProjectiveSpace<T> projectiveSpace, LinTrivector3D<T> trivector)
    {
        Debug.Assert(projectiveSpace.Is4D);

        return trivector.Scalar123 * projectiveSpace.Ie.InternalKVector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaKVector<T> EncodeEGaKVectorAsKVector<T>(this XGaProjectiveSpace<T> projectiveSpace, XGaKVector<T> mv)
    {
        Debug.Assert(mv.Processor.IsEuclidean);

        var composer = projectiveSpace.ProjectiveProcessor.CreateComposer();

        foreach (var (id, scalar) in mv.IdScalarPairs)
            composer.SetTerm(id.ShiftIndices(1), scalar);

        return composer.GetKVector(mv.Grade);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> EncodeEGaVectorBlade<T>(this ILinVector2D<T> egaKVector, XGaProjectiveSpace<T> projectiveSpace)
    {
        return projectiveSpace.EncodeEGaVector(egaKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> EncodeEGaVectorBlade<T>(this ILinVector3D<T> egaKVector, XGaProjectiveSpace<T> projectiveSpace)
    {
        return projectiveSpace.EncodeEGaVector(egaKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> EncodeEGaVectorBlade<T>(this LinVector<T> egaKVector, XGaProjectiveSpace<T> projectiveSpace)
    {
        return projectiveSpace.EncodeEGaVector(egaKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> EncodeEGaVectorBlade<T>(this XGaVector<T> egaKVector, XGaProjectiveSpace<T> projectiveSpace)
    {
        return projectiveSpace.EncodeEGaVector(egaKVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> EncodeEGaBivectorBlade<T>(this LinBivector2D<T> egaKVector, XGaProjectiveSpace<T> projectiveSpace)
    {
        return projectiveSpace.EncodeEGaBivector(egaKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> EncodeEGaBivectorBlade<T>(this LinBivector3D<T> egaKVector, XGaProjectiveSpace<T> projectiveSpace)
    {
        return projectiveSpace.EncodeEGaBivector(egaKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> EncodeEGaBivectorBlade<T>(this XGaBivector<T> egaKVector, XGaProjectiveSpace<T> projectiveSpace)
    {
        return projectiveSpace.EncodeEGaBivector(egaKVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> EncodeEGaTrivectorBlade<T>(this LinTrivector3D<T> egaKVector, XGaProjectiveSpace<T> projectiveSpace)
    {
        return projectiveSpace.EncodeEGaTrivector(egaKVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> EncodeEGaBlade<T>(this XGaKVector<T> egaKVector, XGaProjectiveSpace<T> projectiveSpace)
    {
        return projectiveSpace.EncodeEGaBlade(egaKVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> EncodeEGaVector<T>(this XGaProjectiveSpace<T> projectiveSpace, Scalar<T> x, Scalar<T> y)
    {
        return new XGaProjectiveBlade<T>(
            projectiveSpace, 
            projectiveSpace.EncodeEGaVectorAsVector(x, y)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> EncodeEGaVector<T>(this XGaProjectiveSpace<T> projectiveSpace, ILinVector2D<T> mv)
    {
        return new XGaProjectiveBlade<T>(
            projectiveSpace, 
            projectiveSpace.EncodeEGaVectorAsVector(mv)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> EncodeEGaVector<T>(this XGaProjectiveSpace<T> projectiveSpace, Scalar<T> x, Scalar<T> y, Scalar<T> z)
    {
        return new XGaProjectiveBlade<T>(
            projectiveSpace, 
            projectiveSpace.EncodeEGaVectorAsVector(x, y, z)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> EncodeEGaVector<T>(this XGaProjectiveSpace<T> projectiveSpace, ILinVector3D<T> mv)
    {
        return new XGaProjectiveBlade<T>(
            projectiveSpace, 
            projectiveSpace.EncodeEGaVectorAsVector(mv)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> EncodeEGaVector<T>(this XGaProjectiveSpace<T> projectiveSpace, LinVector<T> mv)
    {
        return new XGaProjectiveBlade<T>(
            projectiveSpace, 
            projectiveSpace.EncodeEGaVectorAsVector(mv)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> EncodeEGaVector<T>(this XGaProjectiveSpace<T> projectiveSpace, XGaVector<T> mv)
    {
        return new XGaProjectiveBlade<T>(
            projectiveSpace, 
            projectiveSpace.EncodeEGaVectorAsVector(mv)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> EncodeEGaBivector<T>(this XGaProjectiveSpace<T> projectiveSpace, int xy)
    {
        return new XGaProjectiveBlade<T>(
            projectiveSpace, 
            projectiveSpace.EncodeEGaBivectorAsBivector(xy)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> EncodeEGaBivector<T>(this XGaProjectiveSpace<T> projectiveSpace, Scalar<T> xy)
    {
        return new XGaProjectiveBlade<T>(
            projectiveSpace, 
            projectiveSpace.EncodeEGaBivectorAsBivector(xy)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> EncodeEGaBivector<T>(this XGaProjectiveSpace<T> projectiveSpace, LinBivector2D<T> bivector)
    {
        return new XGaProjectiveBlade<T>(
            projectiveSpace, 
            projectiveSpace.EncodeEGaBivectorAsBivector(bivector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> EncodeEGaBivector<T>(this XGaProjectiveSpace<T> projectiveSpace, Scalar<T> xy, Scalar<T> xz, Scalar<T> yz)
    {
        return new XGaProjectiveBlade<T>(
            projectiveSpace, 
            projectiveSpace.EncodeEGaBivectorAsBivector(xy, xz, yz)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> EncodeEGaBivector<T>(this XGaProjectiveSpace<T> projectiveSpace, LinBivector3D<T> bivector)
    {
        return new XGaProjectiveBlade<T>(
            projectiveSpace, 
            projectiveSpace.EncodeEGaBivectorAsBivector(bivector)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> EncodeEGaBivector<T>(this XGaProjectiveSpace<T> projectiveSpace, XGaBivector<T> mv)
    {
        return new XGaProjectiveBlade<T>(
            projectiveSpace, 
            projectiveSpace.EncodeEGaBivectorAsBivector(mv)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> EncodeEGaTrivector<T>(this XGaProjectiveSpace<T> projectiveSpace, int xyzScalar)
    {
        return new XGaProjectiveBlade<T>(
            projectiveSpace, 
            projectiveSpace.EncodeEGaTrivectorAsKVector(xyzScalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> EncodeEGaTrivector<T>(this XGaProjectiveSpace<T> projectiveSpace, Scalar<T> xyzScalar)
    {
        return new XGaProjectiveBlade<T>(
            projectiveSpace, 
            projectiveSpace.EncodeEGaTrivectorAsKVector(xyzScalar)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> EncodeEGaTrivector<T>(this XGaProjectiveSpace<T> projectiveSpace, LinTrivector3D<T> trivector)
    {
        return new XGaProjectiveBlade<T>(
            projectiveSpace, 
            projectiveSpace.EncodeEGaTrivectorAsKVector(trivector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> EncodeEGaBlade<T>(this XGaProjectiveSpace<T> projectiveSpace, XGaKVector<T> mv)
    {
        return new XGaProjectiveBlade<T>(
            projectiveSpace, 
            projectiveSpace.EncodeEGaKVectorAsKVector(mv)
        );
    }
}