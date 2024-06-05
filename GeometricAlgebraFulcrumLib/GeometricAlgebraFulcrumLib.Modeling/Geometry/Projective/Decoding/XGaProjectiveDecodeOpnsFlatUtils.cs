using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Projective.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Projective.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Projective.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Projective.Decoding;

public static class XGaProjectiveDecodeElementUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DecodeLine2D<T>(this XGaProjectiveBlade<T> pgaElement)
    {
        Debug.Assert(pgaElement.ProjectiveSpace.Is3D);

        return pgaElement.DecodeHyperPlane(
            pgaElement.ProjectiveSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DecodeLine2D<T>(this XGaProjectiveBlade<T> pgaElement, LinVector2D<T> egaProbePoint)
    {
        return pgaElement.DecodeHyperPlane(
            pgaElement.ProjectiveSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DecodePlane3D<T>(this XGaProjectiveBlade<T> pgaElement)
    {
        Debug.Assert(pgaElement.ProjectiveSpace.Is4D);

        return pgaElement.DecodeHyperPlane(
            pgaElement.ProjectiveSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DecodePlane3D<T>(this XGaProjectiveBlade<T> pgaElement, LinVector3D<T> egaProbePoint)
    {
        return pgaElement.DecodeHyperPlane(
            pgaElement.ProjectiveSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DecodeHyperPlane<T>(this XGaProjectiveBlade<T> pgaElement)
    {
        return pgaElement.DecodeHyperPlane(
            pgaElement.ProjectiveSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DecodeHyperPlane<T>(this XGaProjectiveBlade<T> pgaElement, XGaProjectiveBlade<T> egaProbePoint)
    {
        throw new NotImplementedException();
        //return pgaElement.CGaDual().DecodeIpnsHyperPlane(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DecodeElement<T>(this XGaProjectiveBlade<T> pgaElement, LinVector2D<T> egaProbePoint)
    {
        return pgaElement.DecodeElement(
            pgaElement.ProjectiveSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DecodeElement<T>(this XGaProjectiveBlade<T> pgaElement, LinVector3D<T> egaProbePoint)
    {
        return pgaElement.DecodeElement(
            pgaElement.ProjectiveSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DecodeElement<T>(this XGaProjectiveBlade<T> pgaElement, LinVector<T> egaProbePoint)
    {
        return pgaElement.DecodeElement(
            pgaElement.ProjectiveSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DecodeElement<T>(this XGaProjectiveBlade<T> pgaElement)
    {
        return pgaElement.DecodeElement(
            pgaElement.ProjectiveSpace.ZeroVectorBlade
        );
    }

    public static XGaProjectiveElement<T> DecodeElement<T>(this XGaProjectiveBlade<T> pgaElement, XGaProjectiveBlade<T> egaProbePoint)
    {
        throw new NotImplementedException();
        //var projectiveSpace = pgaElement.ProjectiveSpace;

        //var ipnsProbePoint =
        //    egaProbePoint.EGaVectorToPGaPoint();

        //var position =
        //    ipnsProbePoint
        //        .Lcp(pgaElement)
        //        .Gp(pgaElement.Inverse())
        //        .GetVectorPart()
        //        .GetVectorPart(i => i >= 2)
        //        .ToProjectiveBlade(pgaElement.ProjectiveSpace);
        //        //.DecodeIpnsHyperSphereEGaCenter(projectiveSpace);

        //var directionOpEi =
        //    projectiveSpace.Ei.Lcp(pgaElement.Negative());

        //var weight =
        //    ipnsProbePoint
        //        .Lcp(directionOpEi)
        //        .SpSquared()
        //        .SqrtOfAbs();

        //var flat = new XGaProjectiveElement<T>(
        //    projectiveSpace,
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
    //            egaProbePoint.GetPoint(t).EncodeEGaVectorBlade(blade.ProjectiveSpace)
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
    //            egaProbePoint.GetPoint(t).EncodeEGaVectorBlade(blade.ProjectiveSpace)
    //        )
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> DecodeHyperPlaneEGaDirection<T>(this XGaProjectiveBlade<T> pgaElement)
    {
        throw new NotImplementedException();
        //return pgaElement.CGaDual().DecodeIpnsHyperPlaneEGaDirection();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> DecodeElementEGaDirection<T>(this XGaProjectiveBlade<T> pgaElement)
    {
        throw new NotImplementedException();

        //return pgaElement.ProjectiveSpace.Ei
        //    .Lcp(pgaElement)
        //    .RemoveEi()
        //    .Negative()
        //    .DivideByNorm();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> DecodeHyperPlaneEGaNormalDirection<T>(this XGaProjectiveBlade<T> pgaElement)
    {
        throw new NotImplementedException();
        //return pgaElement.CGaDual().DecodeIpnsHyperPlaneEGaNormalDirection();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> DecodeElementEGaNormalDirection<T>(this XGaProjectiveBlade<T> pgaElement)
    {
        return pgaElement.DecodeElementEGaDirection().EGaNormal();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> DecodeLineEGaPosition2D<T>(this XGaProjectiveBlade<T> pgaElement, LinVector2D<T> egaProbePoint)
    {
        return pgaElement.DecodeHyperPlaneEGaPosition(
            pgaElement.ProjectiveSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> DecodePlaneEGaPosition3D<T>(this XGaProjectiveBlade<T> pgaElement, LinVector3D<T> egaProbePoint)
    {
        return pgaElement.DecodeHyperPlaneEGaPosition(
            pgaElement.ProjectiveSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> DecodeHyperPlaneEGaPosition<T>(this XGaProjectiveBlade<T> pgaElement, LinVector<T> egaProbePoint)
    {
        return pgaElement.DecodeHyperPlaneEGaPosition(
            pgaElement.ProjectiveSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> DecodeHyperPlaneEGaPosition<T>(this XGaProjectiveBlade<T> pgaElement)
    {
        return pgaElement.DecodeHyperPlaneEGaPosition(
            pgaElement.ProjectiveSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> DecodeHyperPlaneEGaPosition<T>(this XGaProjectiveBlade<T> pgaElement, XGaProjectiveBlade<T> egaProbePoint)
    {
        throw new NotImplementedException();
        //return pgaElement.CGaDual().DecodeIpnsHyperPlaneEGaPosition();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> DecodeElementEGaPosition<T>(this XGaProjectiveBlade<T> pgaElement)
    {
        return pgaElement.DecodeElementEGaPosition(
            pgaElement.ProjectiveSpace.ZeroVectorBlade
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> DecodeElementEGaPosition2D<T>(this XGaProjectiveBlade<T> pgaElement)
    {
        return pgaElement.DecodeElementEGaPosition().DecodeEGaVector2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> DecodeElementEGaPosition2D<T>(this XGaProjectiveBlade<T> pgaElement, LinVector2D<T> egaProbePoint)
    {
        return pgaElement.DecodeElementEGaPosition(
            pgaElement.ProjectiveSpace.EncodeEGaVector(egaProbePoint)
        ).DecodeEGaVector2D();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> DecodeElementEGaPosition3D<T>(this XGaProjectiveBlade<T> pgaElement)
    {
        return pgaElement.DecodeElementEGaPosition().DecodeEGaVector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> DecodeElementEGaPosition3D<T>(this XGaProjectiveBlade<T> pgaElement, LinVector3D<T> egaProbePoint)
    {
        return pgaElement.DecodeElementEGaPosition(
            pgaElement.ProjectiveSpace.EncodeEGaVector(egaProbePoint)
        ).DecodeEGaVector3D();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> DecodeElementEGaPosition<T>(this XGaProjectiveBlade<T> pgaElement, LinVector2D<T> egaProbePoint)
    {
        return pgaElement.DecodeElementEGaPosition(
            pgaElement.ProjectiveSpace.EncodeEGaVector(egaProbePoint)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> DecodeElementEGaPosition<T>(this XGaProjectiveBlade<T> pgaElement, LinVector3D<T> egaProbePoint)
    {
        return pgaElement.DecodeElementEGaPosition(
            pgaElement.ProjectiveSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> DecodeElementEGaPosition<T>(this XGaProjectiveBlade<T> pgaElement, LinVector<T> egaProbePoint)
    {
        return pgaElement.DecodeElementEGaPosition(
            pgaElement.ProjectiveSpace.EncodeEGaVector(egaProbePoint)
        ).DecodeEGaVector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> DecodeElementEGaPosition<T>(this XGaProjectiveBlade<T> pgaElement, XGaProjectiveBlade<T> egaProbePointBlade)
    {
        throw new NotImplementedException();

        //return egaProbePointBlade
        //    .EGaVectorToIpnsPoint()
        //    .Lcp(pgaElement)
        //    .Gp(pgaElement.Inverse())
        //    .GetVectorPart()
        //    .GetVectorPart(i => i >= 2)
        //    .ToProjectiveBlade(pgaElement.ProjectiveSpace);
        //    //.DecodeIpnsHyperSphereEGaCenter(egaProbePointBlade.ProjectiveSpace);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeHyperPlaneWeight<T>(this XGaProjectiveBlade<T> pgaElement)
    {
        throw new NotImplementedException();

        //return pgaElement.CGaDual().DecodeIpnsHyperPlaneWeight();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeElementWeight<T>(this XGaProjectiveBlade<T> pgaElement)
    {
        return pgaElement.DecodeElementWeight(
            pgaElement.ProjectiveSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeElementWeight2D<T>(this XGaProjectiveBlade<T> pgaElement, LinVector2D<T> egaProbePoint)
    {
        return pgaElement.DecodeElementWeight(
            pgaElement.ProjectiveSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeElementWeight3D<T>(this XGaProjectiveBlade<T> pgaElement, LinVector3D<T> egaProbePoint)
    {
        return pgaElement.DecodeElementWeight(
            pgaElement.ProjectiveSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeElementWeight<T>(this XGaProjectiveBlade<T> pgaElement, LinVector<T> egaProbePoint)
    {
        return pgaElement.DecodeElementWeight(
            pgaElement.ProjectiveSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeElementWeight<T>(this XGaProjectiveBlade<T> pgaElement, XGaProjectiveBlade<T> egaProbePoint)
    {
        throw new NotImplementedException();

        //var ipnsProbePoint =
        //    egaProbePoint.EGaVectorToIpnsPoint();

        //var directionOpEi =
        //    pgaElement.ProjectiveSpace.Ei.Lcp(pgaElement.Negative());

        //return ipnsProbePoint
        //    .Lcp(directionOpEi)
        //    .SpSquared()
        //    .SqrtOfAbs();
    }


}