using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Decoding;

public static class CGaDecodeOpnsFlatUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DecodeOpnsLine2D<T>(this CGaBlade<T> opnsFlat)
    {
        Debug.Assert(opnsFlat.GeometricSpace.Is4D);

        return opnsFlat.DecodeOpnsHyperPlane(
            opnsFlat.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DecodeOpnsLine2D<T>(this CGaBlade<T> opnsFlat, LinVector2D<T> egaProbePoint)
    {
        return opnsFlat.DecodeOpnsHyperPlane(
            opnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DecodeOpnsPlane3D<T>(this CGaBlade<T> opnsFlat)
    {
        Debug.Assert(opnsFlat.GeometricSpace.Is5D);

        return opnsFlat.DecodeOpnsHyperPlane(
            opnsFlat.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DecodeOpnsPlane3D<T>(this CGaBlade<T> opnsFlat, LinVector3D<T> egaProbePoint)
    {
        return opnsFlat.DecodeOpnsHyperPlane(
            opnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DecodeOpnsHyperPlane<T>(this CGaBlade<T> opnsFlat)
    {
        return opnsFlat.DecodeOpnsHyperPlane(
            opnsFlat.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DecodeOpnsHyperPlane<T>(this CGaBlade<T> opnsFlat, CGaBlade<T> egaProbePoint)
    {
        return opnsFlat.CGaDual().DecodeIpnsHyperPlane(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DecodeOpnsFlat<T>(this CGaBlade<T> opnsFlat, LinVector2D<T> egaProbePoint)
    {
        return opnsFlat.DecodeOpnsFlat(
            opnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DecodeOpnsFlat<T>(this CGaBlade<T> opnsFlat, LinVector3D<T> egaProbePoint)
    {
        return opnsFlat.DecodeOpnsFlat(
            opnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DecodeOpnsFlat<T>(this CGaBlade<T> opnsFlat, LinVector<T> egaProbePoint)
    {
        return opnsFlat.DecodeOpnsFlat(
            opnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DecodeOpnsFlat<T>(this CGaBlade<T> opnsFlat)
    {
        return opnsFlat.DecodeOpnsFlat(
            opnsFlat.GeometricSpace.ZeroVectorBlade
        );
    }

    public static CGaFlat<T> DecodeOpnsFlat<T>(this CGaBlade<T> opnsFlat, CGaBlade<T> egaProbePoint)
    {
        var cgaGeometricSpace = opnsFlat.GeometricSpace;

        var ipnsProbePoint =
            egaProbePoint.VGaVectorToIpnsPoint();

        var position =
            ipnsProbePoint
                .Lcp(opnsFlat)
                .Gp(opnsFlat.Inverse())
                .GetVectorPart()
                .GetVectorPart(i => i >= 2)
                .ToConformalBlade(opnsFlat.GeometricSpace);
        //.DecodeIpnsHyperSphereVGaCenter(cgaGeometricSpace);

        var directionOpEi =
            cgaGeometricSpace.Ei.Lcp(opnsFlat.Negative());

        var weight =
            ipnsProbePoint
                .Lcp(directionOpEi)
                .SpSquared()
                .SqrtOfAbs();

        var flat = new CGaFlat<T>(
            cgaGeometricSpace,
            weight,
            position,
            directionOpEi.RemoveEi()
        );

        Debug.Assert(
            flat.PositionToIpnsPoint().Op(opnsFlat).IsNearZero()
        );

        return flat;
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> DecodeOpnsFlat<T>(this XGaConformalParametricBlade2D blade)
    //{
    //    if (!blade.Specs.IsOpnsFlat)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeOpnsFlat()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> DecodeOpnsFlat<T>(this XGaConformalParametricBlade3D blade)
    //{
    //    if (!blade.Specs.IsOpnsFlat)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeOpnsFlat()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> DecodeOpnsFlat<T>(this XGaConformalParametricBlade2D blade, IParametricCurve2D egaProbePoint)
    //{
    //    if (!blade.Specs.IsOpnsFlat)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeOpnsFlat(
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> DecodeOpnsFlat<T>(this XGaConformalParametricBlade3D blade, IParametricCurve3D egaProbePoint)
    //{
    //    if (!blade.Specs.IsOpnsFlat)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeOpnsFlat(
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeOpnsHyperPlaneVGaDirection<T>(this CGaBlade<T> opnsFlat)
    {
        return opnsFlat.CGaDual().DecodeIpnsHyperPlaneVGaDirection();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeOpnsFlatVGaDirection<T>(this CGaBlade<T> opnsFlat)
    {
        return opnsFlat.GeometricSpace.Ei
            .Lcp(opnsFlat)
            .RemoveEi()
            .Negative()
            .DivideByNorm();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeOpnsHyperPlaneVGaNormalDirection<T>(this CGaBlade<T> opnsFlat)
    {
        return opnsFlat.CGaDual().DecodeIpnsHyperPlaneVGaNormalDirection();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeOpnsFlatVGaNormalDirection<T>(this CGaBlade<T> opnsFlat)
    {
        return opnsFlat.DecodeOpnsFlatVGaDirection().VGaNormal();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeOpnsLineVGaPosition2D<T>(this CGaBlade<T> opnsFlat, LinVector2D<T> egaProbePoint)
    {
        return opnsFlat.DecodeOpnsHyperPlaneVGaPosition(
            opnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeOpnsPlaneVGaPosition3D<T>(this CGaBlade<T> opnsFlat, LinVector3D<T> egaProbePoint)
    {
        return opnsFlat.DecodeOpnsHyperPlaneVGaPosition(
            opnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeOpnsHyperPlaneVGaPosition<T>(this CGaBlade<T> opnsFlat, LinVector<T> egaProbePoint)
    {
        return opnsFlat.DecodeOpnsHyperPlaneVGaPosition(
            opnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeOpnsHyperPlaneVGaPosition<T>(this CGaBlade<T> opnsFlat)
    {
        return opnsFlat.DecodeOpnsHyperPlaneVGaPosition(
            opnsFlat.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeOpnsHyperPlaneVGaPosition<T>(this CGaBlade<T> opnsFlat, CGaBlade<T> egaProbePoint)
    {
        return opnsFlat.CGaDual().DecodeIpnsHyperPlaneVGaPosition();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeOpnsFlatVGaPosition<T>(this CGaBlade<T> opnsFlat)
    {
        return opnsFlat.DecodeOpnsFlatVGaPosition(
            opnsFlat.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> DecodeOpnsFlatVGaPosition2D<T>(this CGaBlade<T> opnsFlat)
    {
        return opnsFlat.DecodeOpnsFlatVGaPosition().DecodeVGaVector2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> DecodeOpnsFlatVGaPosition2D<T>(this CGaBlade<T> opnsFlat, LinVector2D<T> egaProbePoint)
    {
        return opnsFlat.DecodeOpnsFlatVGaPosition(
            opnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        ).DecodeVGaVector2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> DecodeOpnsFlatVGaPosition3D<T>(this CGaBlade<T> opnsFlat)
    {
        return opnsFlat.DecodeOpnsFlatVGaPosition().DecodeVGaVector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> DecodeOpnsFlatVGaPosition3D<T>(this CGaBlade<T> opnsFlat, LinVector3D<T> egaProbePoint)
    {
        return opnsFlat.DecodeOpnsFlatVGaPosition(
            opnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        ).DecodeVGaVector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeOpnsFlatVGaPosition<T>(this CGaBlade<T> opnsFlat, LinVector2D<T> egaProbePoint)
    {
        return opnsFlat.DecodeOpnsFlatVGaPosition(
            opnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeOpnsFlatVGaPosition<T>(this CGaBlade<T> opnsFlat, LinVector3D<T> egaProbePoint)
    {
        return opnsFlat.DecodeOpnsFlatVGaPosition(
            opnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> DecodeOpnsFlatVGaPosition<T>(this CGaBlade<T> opnsFlat, LinVector<T> egaProbePoint)
    {
        return opnsFlat.DecodeOpnsFlatVGaPosition(
            opnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        ).DecodeVGaVector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeOpnsFlatVGaPosition<T>(this CGaBlade<T> opnsFlat, CGaBlade<T> egaProbePointBlade)
    {
        return egaProbePointBlade
            .VGaVectorToIpnsPoint()
            .Lcp(opnsFlat)
            .Gp(opnsFlat.Inverse())
            .GetVectorPart()
            .GetVectorPart(i => i >= 2)
            .ToConformalBlade(opnsFlat.GeometricSpace);
        //.DecodeIpnsHyperSphereVGaCenter(egaProbePointBlade.ConformalSpace);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeOpnsHyperPlaneWeight<T>(this CGaBlade<T> opnsFlat)
    {
        return opnsFlat.CGaDual().DecodeIpnsHyperPlaneWeight();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeOpnsFlatWeight<T>(this CGaBlade<T> opnsFlat)
    {
        return opnsFlat.DecodeOpnsFlatWeight(
            opnsFlat.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeOpnsFlatWeight2D<T>(this CGaBlade<T> opnsFlat, LinVector2D<T> egaProbePoint)
    {
        return opnsFlat.DecodeOpnsFlatWeight(
            opnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeOpnsFlatWeight3D<T>(this CGaBlade<T> opnsFlat, LinVector3D<T> egaProbePoint)
    {
        return opnsFlat.DecodeOpnsFlatWeight(
            opnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeOpnsFlatWeight<T>(this CGaBlade<T> opnsFlat, LinVector<T> egaProbePoint)
    {
        return opnsFlat.DecodeOpnsFlatWeight(
            opnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeOpnsFlatWeight<T>(this CGaBlade<T> opnsFlat, CGaBlade<T> egaProbePoint)
    {
        var ipnsProbePoint =
            egaProbePoint.VGaVectorToIpnsPoint();

        var directionOpEi =
            opnsFlat.GeometricSpace.Ei.Lcp(opnsFlat.Negative());

        return ipnsProbePoint
            .Lcp(directionOpEi)
            .SpSquared()
            .SqrtOfAbs();
    }


}