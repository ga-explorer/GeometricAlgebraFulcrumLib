using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Versors;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Encoding;

public sealed class CGaEncoder<T> :
    CGaEncoderBase<T>
{
    public CGaVGaEncoder<T> VGa { get; }

    public CGaHGaEncoder<T> HGa { get; }

    public CGaPGaEncoder<T> PGa { get; }

    public CGaOpnsDirectionEncoder<T> OpnsDirection { get; }

    public CGaOpnsTangentEncoder<T> OpnsTangent { get; }

    public CGaOpnsFlatEncoder<T> OpnsFlat { get; }

    public CGaOpnsRoundEncoder<T> OpnsRound { get; }
    
    public CGaIpnsDirectionEncoder<T> IpnsDirection { get; }

    public CGaIpnsTangentEncoder<T> IpnsTangent { get; }

    public CGaIpnsFlatEncoder<T> IpnsFlat { get; }

    public CGaIpnsRoundEncoder<T> IpnsRound { get; }


    internal CGaEncoder(CGaGeometricSpace<T> geometricSpace)
        : base(geometricSpace)
    {
        VGa = new CGaVGaEncoder<T>(geometricSpace);
        HGa = new CGaHGaEncoder<T>(geometricSpace);
        PGa = new CGaPGaEncoder<T>(geometricSpace);
        OpnsDirection = new CGaOpnsDirectionEncoder<T>(geometricSpace);
        OpnsTangent = new CGaOpnsTangentEncoder<T>(geometricSpace);
        OpnsFlat = new CGaOpnsFlatEncoder<T>(geometricSpace);
        OpnsRound = new CGaOpnsRoundEncoder<T>(geometricSpace);
        IpnsDirection = new CGaIpnsDirectionEncoder<T>(geometricSpace);
        IpnsTangent = new CGaIpnsTangentEncoder<T>(geometricSpace);
        IpnsFlat = new CGaIpnsFlatEncoder<T>(geometricSpace);
        IpnsRound = new CGaIpnsRoundEncoder<T>(geometricSpace);
    }

    
    /// <summary>
    /// Create a CGA 0-blade representing scalar
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Scalar(int s)
    {
        return new CGaBlade<T>(
            GeometricSpace,
            ConformalProcessor.Scalar(s)
        );
    }
    
    /// <summary>
    /// Create a CGA 0-blade representing scalar
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Scalar(double s)
    {
        return new CGaBlade<T>(
            GeometricSpace,
            ConformalProcessor.Scalar(s)
        );
    }

    /// <summary>
    /// Create a CGA 0-blade representing scalar
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Scalar(Scalar<T> s)
    {
        return new CGaBlade<T>(
            GeometricSpace,
            ConformalProcessor.Scalar(s)
        );
    }
    
    /// <summary>
    /// Create a CGA 0-blade representing scalar
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Scalar(IScalar<T> s)
    {
        return new CGaBlade<T>(
            GeometricSpace,
            ConformalProcessor.Scalar(s)
        );
    }

    /// <summary>
    /// Create a CGA 0-blade representing scalar
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Scalar(T s)
    {
        return new CGaBlade<T>(
            GeometricSpace,
            ConformalProcessor.Scalar(s)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Blade(XGaKVector<T> kVector)
    {
        Debug.Assert(GeometricSpace.IsValidElement(kVector));

        return new CGaBlade<T>(GeometricSpace, kVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(T pointX, T pointY)
    {
        return IpnsRound.Point(pointX, pointY);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(double pointX, double pointY)
    {
        return IpnsRound.Point(pointX, pointY);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(string pointX, string pointY)
    {
        return IpnsRound.Point(pointX, pointY);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(IScalar<T> pointX, IScalar<T> pointY)
    {
        return IpnsRound.Point(pointX, pointY);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(T pointX, T pointY, T pointZ)
    {
        return IpnsRound.Point(pointX, pointY, pointZ);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(double pointX, double pointY, double pointZ)
    {
        return IpnsRound.Point(pointX, pointY, pointZ);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(string pointX, string pointY, string pointZ)
    {
        return IpnsRound.Point(pointX, pointY, pointZ);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(IScalar<T> pointX, IScalar<T> pointY, IScalar<T> pointZ)
    {
        return IpnsRound.Point(pointX, pointY, pointZ);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(LinVector2D<T> egaPoint)
    {
        return IpnsRound.Point(egaPoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(LinVector3D<T> egaPoint)
    {
        return IpnsRound.Point(egaPoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(LinVector<T> egaPoint)
    {
        return IpnsRound.Point(egaPoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(XGaVector<T> egaPoint)
    {
        return IpnsRound.Point(egaPoint);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaVersor<T> Translation(Scalar<T> vectorX, Scalar<T> vectorY)
    {
        Debug.Assert(GeometricSpace.Is4D);

        var vector = LinVector2D<T>.Create(vectorX, vectorY);

        return Translation(
            GeometricSpace.EncodeVGa.VectorAsXGaVector(vector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaVersor<T> Translation(LinVector2D<T> vector)
    {
        Debug.Assert(GeometricSpace.Is4D);

        return Translation(
            GeometricSpace.EncodeVGa.VectorAsXGaVector(vector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaVersor<T> Translation(Scalar<T> vectorX, Scalar<T> vectorY, Scalar<T> vectorZ)
    {
        Debug.Assert(GeometricSpace.Is5D);

        var vector = LinVector3D<T>.Create(vectorX, vectorY, vectorZ);

        return Translation(
            GeometricSpace.EncodeVGa.VectorAsXGaVector(vector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaVersor<T> Translation(LinVector3D<T> vector)
    {
        Debug.Assert(GeometricSpace.Is5D);

        return Translation(
            GeometricSpace.EncodeVGa.VectorAsXGaVector(vector)
        //vector.ToXGaVector(GeometricSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaVersor<T> Translation(XGaVector<T> egaVector)
    {
        Debug.Assert(GeometricSpace.IsValidVGaElement(egaVector));

        return (1 + GeometricSpace.EiVector.Op(egaVector).Divide(2))
            .ToConformalCGaVersor(GeometricSpace);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaVersor<T> Scaling(T scalingFactor)
    {
        var scalarProcessor = GeometricSpace.ScalarProcessor;
        var g = scalarProcessor.ScalarFromValue(scalingFactor).LogE().Divide(scalarProcessor.TwoValue);

        return (g.Cosh() + g.Sinh() * GeometricSpace.EoiBivector).ToConformalCGaVersor(GeometricSpace);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaVersor<T> Rotation(LinAngle<T> angle, LinVector2D<T> egaAxisPoint)
    {
        Debug.Assert(GeometricSpace.Is4D);

        var bivector =
            GeometricSpace.EncodeIpnsFlat.Point(
                egaAxisPoint
            ).InternalBivector;
        //GeometricSpace.EncodeOpnsFlatPoint(
        //    egaAxisPoint
        //).InternalBivector;

        return Rotation(angle, bivector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaVersor<T> Rotation(LinAngle<T> angle, LinVector3D<T> egaAxisPoint, LinVector3D<T> egaAxisVector)
    {
        Debug.Assert(GeometricSpace.Is5D);

        var bivector =
            GeometricSpace.EncodeIpnsFlat.Line(
                egaAxisPoint,
                egaAxisVector
            ).InternalBivector;

        return Rotation(angle, bivector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaVersor<T> Rotation(LinAngle<T> angle, XGaBivector<T> bivector)
    {
        //var halfAngle = angle.HalfPolarAngle();

        var (halfAngleCos, halfAngleSin) = angle.HalfPolarAngle();

        return (halfAngleCos + halfAngleSin / bivector.Norm() * bivector).ToConformalCGaVersor(GeometricSpace);
    }

}