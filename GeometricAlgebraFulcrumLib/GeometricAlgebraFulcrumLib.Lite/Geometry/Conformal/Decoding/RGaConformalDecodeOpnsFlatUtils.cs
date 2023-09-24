using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Elements;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Encoding;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Decoding;

public static class RGaConformalDecodeOpnsFlatUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalFlat DecodeOpnsLine2D(this RGaConformalBlade opnsFlat)
    {
        Debug.Assert(opnsFlat.ConformalSpace.Is4D);

        return opnsFlat.DecodeOpnsHyperPlane(
            opnsFlat.ConformalSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalFlat DecodeOpnsLine2D(this RGaConformalBlade opnsFlat, Float64Vector2D egaProbePoint)
    {
        return opnsFlat.DecodeOpnsHyperPlane(
            opnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalFlat DecodeOpnsPlane3D(this RGaConformalBlade opnsFlat)
    {
        Debug.Assert(opnsFlat.ConformalSpace.Is5D);

        return opnsFlat.DecodeOpnsHyperPlane(
            opnsFlat.ConformalSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalFlat DecodeOpnsPlane3D(this RGaConformalBlade opnsFlat, Float64Vector3D egaProbePoint)
    {
        return opnsFlat.DecodeOpnsHyperPlane(
            opnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalFlat DecodeOpnsHyperPlane(this RGaConformalBlade opnsFlat)
    {
        return opnsFlat.DecodeOpnsHyperPlane(
            opnsFlat.ConformalSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalFlat DecodeOpnsHyperPlane(this RGaConformalBlade opnsFlat, RGaConformalBlade egaProbePoint)
    {
        return opnsFlat.CGaDual().DecodeIpnsHyperPlane(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalFlat DecodeOpnsFlat(this RGaConformalBlade opnsFlat, Float64Vector2D egaProbePoint)
    {
        return opnsFlat.DecodeOpnsFlat(
            opnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalFlat DecodeOpnsFlat(this RGaConformalBlade opnsFlat, Float64Vector3D egaProbePoint)
    {
        return opnsFlat.DecodeOpnsFlat(
            opnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalFlat DecodeOpnsFlat(this RGaConformalBlade opnsFlat, Float64Vector egaProbePoint)
    {
        return opnsFlat.DecodeOpnsFlat(
            opnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalFlat DecodeOpnsFlat(this RGaConformalBlade opnsFlat)
    {
        return opnsFlat.DecodeOpnsFlat(
            opnsFlat.ConformalSpace.ZeroVectorBlade
        );
    }

    public static RGaConformalFlat DecodeOpnsFlat(this RGaConformalBlade opnsFlat, RGaConformalBlade egaProbePoint)
    {
        var conformalSpace = opnsFlat.ConformalSpace;

        var ipnsProbePoint =
            egaProbePoint.EGaVectorToIpnsPoint();

        var position =
            ipnsProbePoint
                .Lcp(opnsFlat)
                .Gp(opnsFlat.Inverse())
                .GetVectorPart((int i) => i >= 2)
                .ToConformalBlade(opnsFlat.ConformalSpace);
                //.DecodeIpnsHyperSphereEGaCenter(conformalSpace);

        var directionOpEi =
            conformalSpace.Ei.Lcp(opnsFlat.Negative());

        var weight =
            ipnsProbePoint
                .Lcp(directionOpEi)
                .SpSquared()
                .SqrtOfAbs();

        var flat = new RGaConformalFlat(
            conformalSpace,
            weight,
            position,
            directionOpEi.RemoveEi()
        );

        Debug.Assert(
            flat.IpnsFlatPosition.Op(opnsFlat).IsNearZero()
        );

        return flat;
    }
    
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalParametricElement DecodeOpnsFlat(this RGaConformalParametricBlade2D blade)
    //{
    //    if (!blade.Specs.IsOpnsFlat)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeOpnsFlat()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalParametricElement DecodeOpnsFlat(this RGaConformalParametricBlade3D blade)
    //{
    //    if (!blade.Specs.IsOpnsFlat)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeOpnsFlat()
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalParametricElement DecodeOpnsFlat(this RGaConformalParametricBlade2D blade, IParametricCurve2D egaProbePoint)
    //{
    //    if (!blade.Specs.IsOpnsFlat)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeOpnsFlat(
    //            egaProbePoint.GetPoint(t).EncodeEGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalParametricElement DecodeOpnsFlat(this RGaConformalParametricBlade3D blade, IParametricCurve3D egaProbePoint)
    //{
    //    if (!blade.Specs.IsOpnsFlat)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeOpnsFlat(
    //            egaProbePoint.GetPoint(t).EncodeEGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeOpnsHyperPlaneEGaDirection(this RGaConformalBlade opnsFlat)
    {
        return opnsFlat.CGaDual().DecodeIpnsHyperPlaneEGaDirection();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeOpnsFlatEGaDirection(this RGaConformalBlade opnsFlat)
    {
        return opnsFlat.ConformalSpace.Ei
            .Lcp(opnsFlat)
            .RemoveEi()
            .Negative()
            .DivideByNorm();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeOpnsHyperPlaneEGaNormalDirection(this RGaConformalBlade opnsFlat)
    {
        return opnsFlat.CGaDual().DecodeIpnsHyperPlaneEGaNormalDirection();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeOpnsFlatEGaNormalDirection(this RGaConformalBlade opnsFlat)
    {
        return opnsFlat.DecodeOpnsFlatEGaDirection().EGaNormal();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeOpnsLineEGaPosition2D(this RGaConformalBlade opnsFlat, Float64Vector2D egaProbePoint)
    {
        return opnsFlat.DecodeOpnsHyperPlaneEGaPosition(
            opnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeOpnsPlaneEGaPosition3D(this RGaConformalBlade opnsFlat, Float64Vector3D egaProbePoint)
    {
        return opnsFlat.DecodeOpnsHyperPlaneEGaPosition(
            opnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeOpnsHyperPlaneEGaPosition(this RGaConformalBlade opnsFlat, Float64Vector egaProbePoint)
    {
        return opnsFlat.DecodeOpnsHyperPlaneEGaPosition(
            opnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeOpnsHyperPlaneEGaPosition(this RGaConformalBlade opnsFlat)
    {
        return opnsFlat.DecodeOpnsHyperPlaneEGaPosition(
            opnsFlat.ConformalSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeOpnsHyperPlaneEGaPosition(this RGaConformalBlade opnsFlat, RGaConformalBlade egaProbePoint)
    {
        return opnsFlat.CGaDual().DecodeIpnsHyperPlaneEGaPosition();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeOpnsFlatEGaPosition(this RGaConformalBlade opnsFlat)
    {
        return opnsFlat.DecodeOpnsFlatEGaPosition(
            opnsFlat.ConformalSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D DecodeOpnsFlatEGaPosition2D(this RGaConformalBlade opnsFlat, Float64Vector2D egaProbePoint)
    {
        return opnsFlat.DecodeOpnsFlatEGaPosition(
            opnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        ).DecodeEGaVector2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D DecodeOpnsFlatEGaPosition3D(this RGaConformalBlade opnsFlat, Float64Vector3D egaProbePoint)
    {
        return opnsFlat.DecodeOpnsFlatEGaPosition(
            opnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        ).DecodeEGaVector3D();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeOpnsFlatEGaPosition(this RGaConformalBlade opnsFlat, Float64Vector2D egaProbePoint)
    {
        return opnsFlat.DecodeOpnsFlatEGaPosition(
            opnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeOpnsFlatEGaPosition(this RGaConformalBlade opnsFlat, Float64Vector3D egaProbePoint)
    {
        return opnsFlat.DecodeOpnsFlatEGaPosition(
            opnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D DecodeOpnsFlatEGaPosition(this RGaConformalBlade opnsFlat, Float64Vector egaProbePoint)
    {
        return opnsFlat.DecodeOpnsFlatEGaPosition(
            opnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        ).DecodeEGaVector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeOpnsFlatEGaPosition(this RGaConformalBlade opnsFlat, RGaConformalBlade egaProbePointBlade)
    {
        return egaProbePointBlade
            .EGaVectorToIpnsPoint()
            .Lcp(opnsFlat)
            .Gp(opnsFlat.Inverse())
            .GetVectorPart((int i) => i >= 2)
            .ToConformalBlade(opnsFlat.ConformalSpace);
            //.DecodeIpnsHyperSphereEGaCenter(egaProbePointBlade.ConformalSpace);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsHyperPlaneWeight(this RGaConformalBlade opnsFlat)
    {
        return opnsFlat.CGaDual().DecodeIpnsHyperPlaneWeight();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsFlatWeight(this RGaConformalBlade opnsFlat)
    {
        return opnsFlat.DecodeOpnsFlatWeight(
            opnsFlat.ConformalSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsFlatWeight2D(this RGaConformalBlade opnsFlat, Float64Vector2D egaProbePoint)
    {
        return opnsFlat.DecodeOpnsFlatWeight(
            opnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsFlatWeight3D(this RGaConformalBlade opnsFlat, Float64Vector3D egaProbePoint)
    {
        return opnsFlat.DecodeOpnsFlatWeight(
            opnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsFlatWeight(this RGaConformalBlade opnsFlat, Float64Vector egaProbePoint)
    {
        return opnsFlat.DecodeOpnsFlatWeight(
            opnsFlat.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsFlatWeight(this RGaConformalBlade opnsFlat, RGaConformalBlade egaProbePoint)
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