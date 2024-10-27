using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Versors;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Encoding;

public sealed class CGaFloat64Encoder :
    CGaFloat64EncoderBase
{
    public CGaFloat64VGaEncoder VGa { get; }

    public CGaFloat64HGaEncoder HGa { get; }

    public CGaFloat64PGaEncoder PGa { get; }

    public CGaFloat64OpnsDirectionEncoder OpnsDirection { get; }

    public CGaFloat64OpnsTangentEncoder OpnsTangent { get; }

    public CGaFloat64OpnsFlatEncoder OpnsFlat { get; }

    public CGaFloat64OpnsRoundEncoder OpnsRound { get; }
    
    public CGaFloat64IpnsDirectionEncoder IpnsDirection { get; }

    public CGaFloat64IpnsTangentEncoder IpnsTangent { get; }

    public CGaFloat64IpnsFlatEncoder IpnsFlat { get; }

    public CGaFloat64IpnsRoundEncoder IpnsRound { get; }


    internal CGaFloat64Encoder(CGaFloat64GeometricSpace geometricSpace)
        : base(geometricSpace)
    {
        VGa = new CGaFloat64VGaEncoder(geometricSpace);
        HGa = new CGaFloat64HGaEncoder(geometricSpace);
        PGa = new CGaFloat64PGaEncoder(geometricSpace);
        OpnsDirection = new CGaFloat64OpnsDirectionEncoder(geometricSpace);
        OpnsTangent = new CGaFloat64OpnsTangentEncoder(geometricSpace);
        OpnsFlat = new CGaFloat64OpnsFlatEncoder(geometricSpace);
        OpnsRound = new CGaFloat64OpnsRoundEncoder(geometricSpace);
        IpnsDirection = new CGaFloat64IpnsDirectionEncoder(geometricSpace);
        IpnsTangent = new CGaFloat64IpnsTangentEncoder(geometricSpace);
        IpnsFlat = new CGaFloat64IpnsFlatEncoder(geometricSpace);
        IpnsRound = new CGaFloat64IpnsRoundEncoder(geometricSpace);
    }

    
    /// <summary>
    /// Create a CGA 0-blade representing scalar
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Scalar(int s)
    {
        return new CGaFloat64Blade(
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
    public CGaFloat64Blade Scalar(double s)
    {
        return new CGaFloat64Blade(
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
    public CGaFloat64Blade Scalar(Float64Scalar s)
    {
        return new CGaFloat64Blade(
            GeometricSpace,
            ConformalProcessor.Scalar(s.ScalarValue)
        );
    }
    
    /// <summary>
    /// Create a CGA 0-blade representing scalar
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Scalar(IFloat64Scalar s)
    {
        return new CGaFloat64Blade(
            GeometricSpace,
            ConformalProcessor.Scalar(s.ScalarValue)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Blade(RGaFloat64KVector kVector)
    {
        Debug.Assert(GeometricSpace.IsValidElement(kVector));

        return new CGaFloat64Blade(GeometricSpace, kVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Point(double pointX, double pointY)
    {
        return IpnsRound.Point(pointX, pointY);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Point(IFloat64Scalar pointX, IFloat64Scalar pointY)
    {
        return IpnsRound.Point(pointX.ScalarValue, pointY.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Point(double pointX, double pointY, double pointZ)
    {
        return IpnsRound.Point(pointX, pointY, pointZ);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Point(IFloat64Scalar pointX, IFloat64Scalar pointY, IFloat64Scalar pointZ)
    {
        return IpnsRound.Point(pointX.ScalarValue, pointY.ScalarValue, pointZ.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Point(LinFloat64Vector2D egaPoint)
    {
        return IpnsRound.Point(egaPoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Point(LinFloat64Vector3D egaPoint)
    {
        return IpnsRound.Point(egaPoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Point(LinFloat64Vector egaPoint)
    {
        return IpnsRound.Point(egaPoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Point(RGaFloat64Vector egaPoint)
    {
        return IpnsRound.Point(egaPoint);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Versor Translation(double vectorX, double vectorY)
    {
        Debug.Assert(GeometricSpace.Is4D);

        var vector = LinFloat64Vector2D.Create(vectorX, vectorY);

        return Translation(
            GeometricSpace.Encode.VGa.VectorAsRGaVector(vector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Versor Translation(Float64Scalar vectorX, Float64Scalar vectorY)
    {
        Debug.Assert(GeometricSpace.Is4D);

        var vector = LinFloat64Vector2D.Create(vectorX, vectorY);

        return Translation(
            GeometricSpace.Encode.VGa.VectorAsRGaVector(vector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Versor Translation(LinFloat64Vector2D vector)
    {
        Debug.Assert(GeometricSpace.Is4D);

        return Translation(
            GeometricSpace.Encode.VGa.VectorAsRGaVector(vector)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Versor Translation(double vectorX, double vectorY, double vectorZ)
    {
        Debug.Assert(GeometricSpace.Is5D);

        var vector = LinFloat64Vector3D.Create(vectorX, vectorY, vectorZ);

        return Translation(
            GeometricSpace.Encode.VGa.VectorAsRGaVector(vector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Versor Translation(Float64Scalar vectorX, Float64Scalar vectorY, Float64Scalar vectorZ)
    {
        Debug.Assert(GeometricSpace.Is5D);

        var vector = LinFloat64Vector3D.Create(vectorX, vectorY, vectorZ);

        return Translation(
            GeometricSpace.EncodeVGa.VectorAsRGaVector(vector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Versor Translation(LinFloat64Vector3D vector)
    {
        Debug.Assert(GeometricSpace.Is5D);

        return Translation(
            GeometricSpace.Encode.VGa.VectorAsRGaVector(vector)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Versor Translation(RGaFloat64Vector egaVector)
    {
        Debug.Assert(GeometricSpace.IsValidVGaElement(egaVector));

        return (1 + GeometricSpace.EiVector.Op(egaVector).Divide(2))
            .ToConformalCGaVersor(GeometricSpace);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Versor Scaling(double scalingFactor)
    {
        var g = scalingFactor.LogE() / 2;

        return (g.Cosh() + g.Sinh() * GeometricSpace.EoiBivector).ToConformalCGaVersor(GeometricSpace);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Versor Rotation(LinFloat64Angle angle, LinFloat64Vector2D egaAxisPoint)
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
    public CGaFloat64Versor Rotation(LinFloat64Angle angle, LinFloat64Vector3D egaAxisPoint, LinFloat64Vector3D egaAxisVector)
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
    public CGaFloat64Versor Rotation(LinFloat64Angle angle, RGaFloat64Bivector bivector)
    {
        //var halfAngle = angle.HalfPolarAngle();

        var (halfAngleCos, halfAngleSin) = angle.HalfPolarAngle();

        return (halfAngleCos + halfAngleSin / bivector.Norm() * bivector).ToConformalCGaVersor(GeometricSpace);
    }
}