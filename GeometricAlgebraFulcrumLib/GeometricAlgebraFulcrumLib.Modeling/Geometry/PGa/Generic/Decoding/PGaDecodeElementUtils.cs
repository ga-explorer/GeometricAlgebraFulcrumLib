using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic.Decoding;

public static class PGaDecodeElementUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DecodeLine2D<T>(this PGaBlade<T> pgaElement)
    {
        Debug.Assert(pgaElement.Geometry.Is3D);

        return pgaElement.DecodeHyperPlane(
            pgaElement.Geometry.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DecodeLine2D<T>(this PGaBlade<T> pgaElement, LinVector2D<T> egaProbePoint)
    {
        return pgaElement.DecodeHyperPlane(
            pgaElement.Geometry.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DecodePlane3D<T>(this PGaBlade<T> pgaElement)
    {
        Debug.Assert(pgaElement.Geometry.Is4D);

        return pgaElement.DecodeHyperPlane(
            pgaElement.Geometry.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DecodePlane3D<T>(this PGaBlade<T> pgaElement, LinVector3D<T> egaProbePoint)
    {
        return pgaElement.DecodeHyperPlane(
            pgaElement.Geometry.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DecodeHyperPlane<T>(this PGaBlade<T> pgaElement)
    {
        return pgaElement.DecodeHyperPlane(
            pgaElement.Geometry.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DecodeHyperPlane<T>(this PGaBlade<T> pgaElement, PGaBlade<T> egaProbePoint)
    {
        throw new NotImplementedException();
        //return pgaElement.CGaDual().DecodeIpnsHyperPlane(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DecodeElement<T>(this PGaBlade<T> pgaElement, LinVector2D<T> egaProbePoint)
    {
        return pgaElement.DecodeElement(
            pgaElement.Geometry.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DecodeElement<T>(this PGaBlade<T> pgaElement, LinVector3D<T> egaProbePoint)
    {
        return pgaElement.DecodeElement(
            pgaElement.Geometry.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DecodeElement<T>(this PGaBlade<T> pgaElement, LinVector<T> egaProbePoint)
    {
        return pgaElement.DecodeElement(
            pgaElement.Geometry.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DecodeElement<T>(this PGaBlade<T> pgaElement)
    {
        return pgaElement.DecodeElement(
            pgaElement.Geometry.ZeroVectorBlade
        );
    }

    public static PGaElement<T> DecodeElement<T>(this PGaBlade<T> pgaElement, PGaBlade<T> egaProbePoint)
    {
        throw new NotImplementedException();
        //var pgaGeometricSpace = pgaElement.ProjectiveSpace;

        //var ipnsProbePoint =
        //    egaProbePoint.VGaVectorToPGaPoint();

        //var position =
        //    ipnsProbePoint
        //        .Lcp(pgaElement)
        //        .Gp(pgaElement.Inverse())
        //        .GetVectorPart()
        //        .GetVectorPart(i => i >= 2)
        //        .ToProjectiveBlade(pgaElement.ProjectiveSpace);
        //        //.DecodeIpnsHyperSphereVGaCenter(pgaGeometricSpace);

        //var directionOpEi =
        //    pgaGeometricSpace.Ei.Lcp(pgaElement.Negative());

        //var weight =
        //    ipnsProbePoint
        //        .Lcp(directionOpEi)
        //        .SpSquared()
        //        .SqrtOfAbs();

        //var flat = new PGaElement<T>(
        //    pgaGeometricSpace,
        //    weight,
        //    position,
        //    directionOpEi.RemoveEi()
        //);

        //Debug.Assert(
        //    flat.PositionToIpnsPoint().Op(pgaElement).IsNearZero()
        //);

        //return flat;
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaProjectiveParametricElement<T> DecodeElement<T>(this XGaProjectiveParametricBlade2D blade)
    //{
    //    if (!blade.Specs.IsElement)
    //        throw new InvalidOperationException();

    //    return XGaProjectiveParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeElement()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaProjectiveParametricElement<T> DecodeElement<T>(this XGaProjectiveParametricBlade3D blade)
    //{
    //    if (!blade.Specs.IsElement)
    //        throw new InvalidOperationException();

    //    return XGaProjectiveParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeElement()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaProjectiveParametricElement<T> DecodeElement<T>(this XGaProjectiveParametricBlade2D blade, IParametricCurve2D egaProbePoint)
    //{
    //    if (!blade.Specs.IsElement)
    //        throw new InvalidOperationException();

    //    return XGaProjectiveParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeElement(
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ProjectiveSpace)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaProjectiveParametricElement<T> DecodeElement<T>(this XGaProjectiveParametricBlade3D blade, IParametricCurve3D egaProbePoint)
    //{
    //    if (!blade.Specs.IsElement)
    //        throw new InvalidOperationException();

    //    return XGaProjectiveParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeElement(
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ProjectiveSpace)
    //        )
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> DecodeHyperPlaneVGaDirection<T>(this PGaBlade<T> pgaElement)
    {
        throw new NotImplementedException();
        //return pgaElement.CGaDual().DecodeIpnsHyperPlaneVGaDirection();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> DecodeElementVGaDirection<T>(this PGaBlade<T> pgaElement)
    {
        throw new NotImplementedException();

        //return pgaElement.ProjectiveSpace.Ei
        //    .Lcp(pgaElement)
        //    .RemoveEi()
        //    .Negative()
        //    .DivideByNorm();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> DecodeHyperPlaneVGaNormalDirection<T>(this PGaBlade<T> pgaElement)
    {
        throw new NotImplementedException();
        //return pgaElement.CGaDual().DecodeIpnsHyperPlaneVGaNormalDirection();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> DecodeElementVGaNormalDirection<T>(this PGaBlade<T> pgaElement)
    {
        return pgaElement.DecodeElementVGaDirection().VGaNormal();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> DecodeLineVGaPosition2D<T>(this PGaBlade<T> pgaElement, LinVector2D<T> egaProbePoint)
    {
        return pgaElement.DecodeHyperPlaneVGaPosition(
            pgaElement.Geometry.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> DecodePlaneVGaPosition3D<T>(this PGaBlade<T> pgaElement, LinVector3D<T> egaProbePoint)
    {
        return pgaElement.DecodeHyperPlaneVGaPosition(
            pgaElement.Geometry.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> DecodeHyperPlaneVGaPosition<T>(this PGaBlade<T> pgaElement, LinVector<T> egaProbePoint)
    {
        return pgaElement.DecodeHyperPlaneVGaPosition(
            pgaElement.Geometry.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> DecodeHyperPlaneVGaPosition<T>(this PGaBlade<T> pgaElement)
    {
        return pgaElement.DecodeHyperPlaneVGaPosition(
            pgaElement.Geometry.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> DecodeHyperPlaneVGaPosition<T>(this PGaBlade<T> pgaElement, PGaBlade<T> egaProbePoint)
    {
        throw new NotImplementedException();
        //return pgaElement.CGaDual().DecodeIpnsHyperPlaneVGaPosition();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> DecodeElementVGaPosition<T>(this PGaBlade<T> pgaElement)
    {
        return pgaElement.DecodeElementVGaPosition(
            pgaElement.Geometry.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> DecodeElementVGaPosition2D<T>(this PGaBlade<T> pgaElement)
    {
        return pgaElement.DecodeElementVGaPosition().DecodeVGaVector2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> DecodeElementVGaPosition2D<T>(this PGaBlade<T> pgaElement, LinVector2D<T> egaProbePoint)
    {
        return pgaElement.DecodeElementVGaPosition(
            pgaElement.Geometry.EncodeVGaVector(egaProbePoint)
        ).DecodeVGaVector2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> DecodeElementVGaPosition3D<T>(this PGaBlade<T> pgaElement)
    {
        return pgaElement.DecodeElementVGaPosition().DecodeVGaVector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> DecodeElementVGaPosition3D<T>(this PGaBlade<T> pgaElement, LinVector3D<T> egaProbePoint)
    {
        return pgaElement.DecodeElementVGaPosition(
            pgaElement.Geometry.EncodeVGaVector(egaProbePoint)
        ).DecodeVGaVector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> DecodeElementVGaPosition<T>(this PGaBlade<T> pgaElement, LinVector2D<T> egaProbePoint)
    {
        return pgaElement.DecodeElementVGaPosition(
            pgaElement.Geometry.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> DecodeElementVGaPosition<T>(this PGaBlade<T> pgaElement, LinVector3D<T> egaProbePoint)
    {
        return pgaElement.DecodeElementVGaPosition(
            pgaElement.Geometry.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> DecodeElementVGaPosition<T>(this PGaBlade<T> pgaElement, LinVector<T> egaProbePoint)
    {
        return pgaElement.DecodeElementVGaPosition(
            pgaElement.Geometry.EncodeVGaVector(egaProbePoint)
        ).DecodeVGaVector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> DecodeElementVGaPosition<T>(this PGaBlade<T> pgaElement, PGaBlade<T> egaProbePointBlade)
    {
        throw new NotImplementedException();

        //return egaProbePointBlade
        //    .VGaVectorToIpnsPoint()
        //    .Lcp(pgaElement)
        //    .Gp(pgaElement.Inverse())
        //    .GetVectorPart()
        //    .GetVectorPart(i => i >= 2)
        //    .ToProjectiveBlade(pgaElement.ProjectiveSpace);
        //    //.DecodeIpnsHyperSphereVGaCenter(egaProbePointBlade.ProjectiveSpace);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeHyperPlaneWeight<T>(this PGaBlade<T> pgaElement)
    {
        throw new NotImplementedException();

        //return pgaElement.CGaDual().DecodeIpnsHyperPlaneWeight();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeElementWeight<T>(this PGaBlade<T> pgaElement)
    {
        return pgaElement.DecodeElementWeight(
            pgaElement.Geometry.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeElementWeight2D<T>(this PGaBlade<T> pgaElement, LinVector2D<T> egaProbePoint)
    {
        return pgaElement.DecodeElementWeight(
            pgaElement.Geometry.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeElementWeight3D<T>(this PGaBlade<T> pgaElement, LinVector3D<T> egaProbePoint)
    {
        return pgaElement.DecodeElementWeight(
            pgaElement.Geometry.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeElementWeight<T>(this PGaBlade<T> pgaElement, LinVector<T> egaProbePoint)
    {
        return pgaElement.DecodeElementWeight(
            pgaElement.Geometry.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeElementWeight<T>(this PGaBlade<T> pgaElement, PGaBlade<T> egaProbePoint)
    {
        throw new NotImplementedException();

        //var ipnsProbePoint =
        //    egaProbePoint.VGaVectorToIpnsPoint();

        //var directionOpEi =
        //    pgaElement.ProjectiveSpace.Ei.Lcp(pgaElement.Negative());

        //return ipnsProbePoint
        //    .Lcp(directionOpEi)
        //    .SpSquared()
        //    .SqrtOfAbs();
    }


}