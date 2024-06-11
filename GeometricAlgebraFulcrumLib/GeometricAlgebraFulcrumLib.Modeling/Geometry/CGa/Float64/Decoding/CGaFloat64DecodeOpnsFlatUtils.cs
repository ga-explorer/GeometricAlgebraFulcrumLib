using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Decoding;

public static class CGaFloat64DecodeOpnsFlatUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DecodeOpnsLine2D(this CGaFloat64Blade opnsFlat)
    {
        Debug.Assert(opnsFlat.GeometricSpace.Is4D);

        return opnsFlat.DecodeOpnsHyperPlane(
            opnsFlat.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DecodeOpnsLine2D(this CGaFloat64Blade opnsFlat, LinFloat64Vector2D egaProbePoint)
    {
        return opnsFlat.DecodeOpnsHyperPlane(
            opnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DecodeOpnsPlane3D(this CGaFloat64Blade opnsFlat)
    {
        Debug.Assert(opnsFlat.GeometricSpace.Is5D);

        return opnsFlat.DecodeOpnsHyperPlane(
            opnsFlat.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DecodeOpnsPlane3D(this CGaFloat64Blade opnsFlat, LinFloat64Vector3D egaProbePoint)
    {
        return opnsFlat.DecodeOpnsHyperPlane(
            opnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DecodeOpnsHyperPlane(this CGaFloat64Blade opnsFlat)
    {
        return opnsFlat.DecodeOpnsHyperPlane(
            opnsFlat.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DecodeOpnsHyperPlane(this CGaFloat64Blade opnsFlat, CGaFloat64Blade egaProbePoint)
    {
        return opnsFlat.CGaDual().DecodeIpnsHyperPlane(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DecodeOpnsFlat(this CGaFloat64Blade opnsFlat, LinFloat64Vector2D egaProbePoint)
    {
        return opnsFlat.DecodeOpnsFlat(
            opnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DecodeOpnsFlat(this CGaFloat64Blade opnsFlat, LinFloat64Vector3D egaProbePoint)
    {
        return opnsFlat.DecodeOpnsFlat(
            opnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DecodeOpnsFlat(this CGaFloat64Blade opnsFlat, LinFloat64Vector egaProbePoint)
    {
        return opnsFlat.DecodeOpnsFlat(
            opnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DecodeOpnsFlat(this CGaFloat64Blade opnsFlat)
    {
        return opnsFlat.DecodeOpnsFlat(
            opnsFlat.GeometricSpace.ZeroVectorBlade
        );
    }

    public static CGaFloat64Flat DecodeOpnsFlat(this CGaFloat64Blade opnsFlat, CGaFloat64Blade egaProbePoint)
    {
        var cgaGeometricSpace = opnsFlat.GeometricSpace;

        var ipnsProbePoint =
            egaProbePoint.VGaVectorToIpnsPoint();

        var position =
            ipnsProbePoint
                .Lcp(opnsFlat)
                .Gp(opnsFlat.Inverse())
                .GetVectorPart((int i) => i >= 2)
                .ToConformalBlade(opnsFlat.GeometricSpace);
        //.DecodeIpnsHyperSphereVGaCenter(cgaGeometricSpace);

        var directionOpEi =
            cgaGeometricSpace.Ei.Lcp(opnsFlat.Negative());

        var weight =
            ipnsProbePoint
                .Lcp(directionOpEi)
                .SpSquared()
                .SqrtOfAbs();

        var flat = new CGaFloat64Flat(
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
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ConformalSpace)
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
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeOpnsHyperPlaneVGaDirection(this CGaFloat64Blade opnsFlat)
    {
        return opnsFlat.CGaDual().DecodeIpnsHyperPlaneVGaDirection();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeOpnsFlatVGaDirection(this CGaFloat64Blade opnsFlat)
    {
        return opnsFlat.GeometricSpace.Ei
            .Lcp(opnsFlat)
            .RemoveEi()
            .Negative()
            .DivideByNorm();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeOpnsHyperPlaneVGaNormalDirection(this CGaFloat64Blade opnsFlat)
    {
        return opnsFlat.CGaDual().DecodeIpnsHyperPlaneVGaNormalDirection();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeOpnsFlatVGaNormalDirection(this CGaFloat64Blade opnsFlat)
    {
        return opnsFlat.DecodeOpnsFlatVGaDirection().VGaNormal();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeOpnsLineVGaPosition2D(this CGaFloat64Blade opnsFlat, LinFloat64Vector2D egaProbePoint)
    {
        return opnsFlat.DecodeOpnsHyperPlaneVGaPosition(
            opnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeOpnsPlaneVGaPosition3D(this CGaFloat64Blade opnsFlat, LinFloat64Vector3D egaProbePoint)
    {
        return opnsFlat.DecodeOpnsHyperPlaneVGaPosition(
            opnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeOpnsHyperPlaneVGaPosition(this CGaFloat64Blade opnsFlat, LinFloat64Vector egaProbePoint)
    {
        return opnsFlat.DecodeOpnsHyperPlaneVGaPosition(
            opnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeOpnsHyperPlaneVGaPosition(this CGaFloat64Blade opnsFlat)
    {
        return opnsFlat.DecodeOpnsHyperPlaneVGaPosition(
            opnsFlat.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeOpnsHyperPlaneVGaPosition(this CGaFloat64Blade opnsFlat, CGaFloat64Blade egaProbePoint)
    {
        return opnsFlat.CGaDual().DecodeIpnsHyperPlaneVGaPosition();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeOpnsFlatVGaPosition(this CGaFloat64Blade opnsFlat)
    {
        return opnsFlat.DecodeOpnsFlatVGaPosition(
            opnsFlat.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D DecodeOpnsFlatVGaPosition2D(this CGaFloat64Blade opnsFlat)
    {
        return opnsFlat.DecodeOpnsFlatVGaPosition().DecodeVGaVector2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D DecodeOpnsFlatVGaPosition2D(this CGaFloat64Blade opnsFlat, LinFloat64Vector2D egaProbePoint)
    {
        return opnsFlat.DecodeOpnsFlatVGaPosition(
            opnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        ).DecodeVGaVector2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D DecodeOpnsFlatVGaPosition3D(this CGaFloat64Blade opnsFlat)
    {
        return opnsFlat.DecodeOpnsFlatVGaPosition().DecodeVGaVector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D DecodeOpnsFlatVGaPosition3D(this CGaFloat64Blade opnsFlat, LinFloat64Vector3D egaProbePoint)
    {
        return opnsFlat.DecodeOpnsFlatVGaPosition(
            opnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        ).DecodeVGaVector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeOpnsFlatVGaPosition(this CGaFloat64Blade opnsFlat, LinFloat64Vector2D egaProbePoint)
    {
        return opnsFlat.DecodeOpnsFlatVGaPosition(
            opnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeOpnsFlatVGaPosition(this CGaFloat64Blade opnsFlat, LinFloat64Vector3D egaProbePoint)
    {
        return opnsFlat.DecodeOpnsFlatVGaPosition(
            opnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D DecodeOpnsFlatVGaPosition(this CGaFloat64Blade opnsFlat, LinFloat64Vector egaProbePoint)
    {
        return opnsFlat.DecodeOpnsFlatVGaPosition(
            opnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        ).DecodeVGaVector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeOpnsFlatVGaPosition(this CGaFloat64Blade opnsFlat, CGaFloat64Blade egaProbePointBlade)
    {
        return egaProbePointBlade
            .VGaVectorToIpnsPoint()
            .Lcp(opnsFlat)
            .Gp(opnsFlat.Inverse())
            .GetVectorPart((int i) => i >= 2)
            .ToConformalBlade(opnsFlat.GeometricSpace);
        //.DecodeIpnsHyperSphereVGaCenter(egaProbePointBlade.ConformalSpace);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsHyperPlaneWeight(this CGaFloat64Blade opnsFlat)
    {
        return opnsFlat.CGaDual().DecodeIpnsHyperPlaneWeight();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsFlatWeight(this CGaFloat64Blade opnsFlat)
    {
        return opnsFlat.DecodeOpnsFlatWeight(
            opnsFlat.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsFlatWeight2D(this CGaFloat64Blade opnsFlat, LinFloat64Vector2D egaProbePoint)
    {
        return opnsFlat.DecodeOpnsFlatWeight(
            opnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsFlatWeight3D(this CGaFloat64Blade opnsFlat, LinFloat64Vector3D egaProbePoint)
    {
        return opnsFlat.DecodeOpnsFlatWeight(
            opnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsFlatWeight(this CGaFloat64Blade opnsFlat, LinFloat64Vector egaProbePoint)
    {
        return opnsFlat.DecodeOpnsFlatWeight(
            opnsFlat.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsFlatWeight(this CGaFloat64Blade opnsFlat, CGaFloat64Blade egaProbePoint)
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