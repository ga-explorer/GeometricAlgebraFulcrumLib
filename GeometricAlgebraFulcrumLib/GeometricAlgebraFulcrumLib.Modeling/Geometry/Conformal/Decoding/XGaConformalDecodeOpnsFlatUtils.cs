using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Decoding;

public static class XGaConformalDecodeOpnsFlatUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalFlat<T> DecodeOpnsLine2D<T>(this XGaConformalBlade<T> opnsFlat)
    {
        Debug.Assert(opnsFlat.ConformalSpace.Is4D);

        return opnsFlat.DecodeOpnsHyperPlane(
            opnsFlat.ConformalSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalFlat<T> DecodeOpnsLine2D<T>(this XGaConformalBlade<T> opnsFlat, LinVector2D<T> egaProbePoint)
    {
        return opnsFlat.DecodeOpnsHyperPlane(
            opnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalFlat<T> DecodeOpnsPlane3D<T>(this XGaConformalBlade<T> opnsFlat)
    {
        Debug.Assert(opnsFlat.ConformalSpace.Is5D);

        return opnsFlat.DecodeOpnsHyperPlane(
            opnsFlat.ConformalSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalFlat<T> DecodeOpnsPlane3D<T>(this XGaConformalBlade<T> opnsFlat, LinVector3D<T> egaProbePoint)
    {
        return opnsFlat.DecodeOpnsHyperPlane(
            opnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalFlat<T> DecodeOpnsHyperPlane<T>(this XGaConformalBlade<T> opnsFlat)
    {
        return opnsFlat.DecodeOpnsHyperPlane(
            opnsFlat.ConformalSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalFlat<T> DecodeOpnsHyperPlane<T>(this XGaConformalBlade<T> opnsFlat, XGaConformalBlade<T> egaProbePoint)
    {
        return opnsFlat.CGaDual().DecodeIpnsHyperPlane(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalFlat<T> DecodeOpnsFlat<T>(this XGaConformalBlade<T> opnsFlat, LinVector2D<T> egaProbePoint)
    {
        return opnsFlat.DecodeOpnsFlat(
            opnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalFlat<T> DecodeOpnsFlat<T>(this XGaConformalBlade<T> opnsFlat, LinVector3D<T> egaProbePoint)
    {
        return opnsFlat.DecodeOpnsFlat(
            opnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalFlat<T> DecodeOpnsFlat<T>(this XGaConformalBlade<T> opnsFlat, LinVector<T> egaProbePoint)
    {
        return opnsFlat.DecodeOpnsFlat(
            opnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalFlat<T> DecodeOpnsFlat<T>(this XGaConformalBlade<T> opnsFlat)
    {
        return opnsFlat.DecodeOpnsFlat(
            opnsFlat.ConformalSpace.ZeroVectorBlade
        );
    }

    public static XGaConformalFlat<T> DecodeOpnsFlat<T>(this XGaConformalBlade<T> opnsFlat, XGaConformalBlade<T> egaProbePoint)
    {
        var conformalSpace = opnsFlat.ConformalSpace;

        var ipnsProbePoint =
            egaProbePoint.EGaVectorToIpnsPoint();

        var position =
            ipnsProbePoint
                .Lcp(opnsFlat)
                .Gp(opnsFlat.Inverse())
                .GetVectorPart()
                .GetVectorPart(i => i >= 2)
                .ToConformalBlade(opnsFlat.ConformalSpace);
                //.DecodeIpnsHyperSphereEGaCenter(conformalSpace);

        var directionOpEi =
            conformalSpace.Ei.Lcp(opnsFlat.Negative());

        var weight =
            ipnsProbePoint
                .Lcp(directionOpEi)
                .SpSquared()
                .SqrtOfAbs();

        var flat = new XGaConformalFlat<T>(
            conformalSpace,
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
    //            egaProbePoint.GetPoint(t).EncodeEGaVectorBlade(blade.ConformalSpace)
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
    //            egaProbePoint.GetPoint(t).EncodeEGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeOpnsHyperPlaneEGaDirection<T>(this XGaConformalBlade<T> opnsFlat)
    {
        return opnsFlat.CGaDual().DecodeIpnsHyperPlaneEGaDirection();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeOpnsFlatEGaDirection<T>(this XGaConformalBlade<T> opnsFlat)
    {
        return opnsFlat.ConformalSpace.Ei
            .Lcp(opnsFlat)
            .RemoveEi()
            .Negative()
            .DivideByNorm();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeOpnsHyperPlaneEGaNormalDirection<T>(this XGaConformalBlade<T> opnsFlat)
    {
        return opnsFlat.CGaDual().DecodeIpnsHyperPlaneEGaNormalDirection();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeOpnsFlatEGaNormalDirection<T>(this XGaConformalBlade<T> opnsFlat)
    {
        return opnsFlat.DecodeOpnsFlatEGaDirection().EGaNormal();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeOpnsLineEGaPosition2D<T>(this XGaConformalBlade<T> opnsFlat, LinVector2D<T> egaProbePoint)
    {
        return opnsFlat.DecodeOpnsHyperPlaneEGaPosition(
            opnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeOpnsPlaneEGaPosition3D<T>(this XGaConformalBlade<T> opnsFlat, LinVector3D<T> egaProbePoint)
    {
        return opnsFlat.DecodeOpnsHyperPlaneEGaPosition(
            opnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeOpnsHyperPlaneEGaPosition<T>(this XGaConformalBlade<T> opnsFlat, LinVector<T> egaProbePoint)
    {
        return opnsFlat.DecodeOpnsHyperPlaneEGaPosition(
            opnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeOpnsHyperPlaneEGaPosition<T>(this XGaConformalBlade<T> opnsFlat)
    {
        return opnsFlat.DecodeOpnsHyperPlaneEGaPosition(
            opnsFlat.ConformalSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeOpnsHyperPlaneEGaPosition<T>(this XGaConformalBlade<T> opnsFlat, XGaConformalBlade<T> egaProbePoint)
    {
        return opnsFlat.CGaDual().DecodeIpnsHyperPlaneEGaPosition();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeOpnsFlatEGaPosition<T>(this XGaConformalBlade<T> opnsFlat)
    {
        return opnsFlat.DecodeOpnsFlatEGaPosition(
            opnsFlat.ConformalSpace.ZeroVectorBlade
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> DecodeOpnsFlatEGaPosition2D<T>(this XGaConformalBlade<T> opnsFlat)
    {
        return opnsFlat.DecodeOpnsFlatEGaPosition().DecodeEGaVector2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> DecodeOpnsFlatEGaPosition2D<T>(this XGaConformalBlade<T> opnsFlat, LinVector2D<T> egaProbePoint)
    {
        return opnsFlat.DecodeOpnsFlatEGaPosition(
            opnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        ).DecodeEGaVector2D();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> DecodeOpnsFlatEGaPosition3D<T>(this XGaConformalBlade<T> opnsFlat)
    {
        return opnsFlat.DecodeOpnsFlatEGaPosition().DecodeEGaVector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> DecodeOpnsFlatEGaPosition3D<T>(this XGaConformalBlade<T> opnsFlat, LinVector3D<T> egaProbePoint)
    {
        return opnsFlat.DecodeOpnsFlatEGaPosition(
            opnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        ).DecodeEGaVector3D();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeOpnsFlatEGaPosition<T>(this XGaConformalBlade<T> opnsFlat, LinVector2D<T> egaProbePoint)
    {
        return opnsFlat.DecodeOpnsFlatEGaPosition(
            opnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeOpnsFlatEGaPosition<T>(this XGaConformalBlade<T> opnsFlat, LinVector3D<T> egaProbePoint)
    {
        return opnsFlat.DecodeOpnsFlatEGaPosition(
            opnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> DecodeOpnsFlatEGaPosition<T>(this XGaConformalBlade<T> opnsFlat, LinVector<T> egaProbePoint)
    {
        return opnsFlat.DecodeOpnsFlatEGaPosition(
            opnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        ).DecodeEGaVector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeOpnsFlatEGaPosition<T>(this XGaConformalBlade<T> opnsFlat, XGaConformalBlade<T> egaProbePointBlade)
    {
        return egaProbePointBlade
            .EGaVectorToIpnsPoint()
            .Lcp(opnsFlat)
            .Gp(opnsFlat.Inverse())
            .GetVectorPart()
            .GetVectorPart(i => i >= 2)
            .ToConformalBlade(opnsFlat.ConformalSpace);
            //.DecodeIpnsHyperSphereEGaCenter(egaProbePointBlade.ConformalSpace);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeOpnsHyperPlaneWeight<T>(this XGaConformalBlade<T> opnsFlat)
    {
        return opnsFlat.CGaDual().DecodeIpnsHyperPlaneWeight();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeOpnsFlatWeight<T>(this XGaConformalBlade<T> opnsFlat)
    {
        return opnsFlat.DecodeOpnsFlatWeight(
            opnsFlat.ConformalSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeOpnsFlatWeight2D<T>(this XGaConformalBlade<T> opnsFlat, LinVector2D<T> egaProbePoint)
    {
        return opnsFlat.DecodeOpnsFlatWeight(
            opnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeOpnsFlatWeight3D<T>(this XGaConformalBlade<T> opnsFlat, LinVector3D<T> egaProbePoint)
    {
        return opnsFlat.DecodeOpnsFlatWeight(
            opnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeOpnsFlatWeight<T>(this XGaConformalBlade<T> opnsFlat, LinVector<T> egaProbePoint)
    {
        return opnsFlat.DecodeOpnsFlatWeight(
            opnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeOpnsFlatWeight<T>(this XGaConformalBlade<T> opnsFlat, XGaConformalBlade<T> egaProbePoint)
    {
        var ipnsProbePoint =
            egaProbePoint.EGaVectorToIpnsPoint();

        var directionOpEi =
            opnsFlat.ConformalSpace.Ei.Lcp(opnsFlat.Negative());

        return ipnsProbePoint
            .Lcp(directionOpEi)
            .SpSquared()
            .SqrtOfAbs();
    }


}