using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Decoding;

public sealed class CGaFloat64OpnsTangentBladeDecoder :
    CGaFloat64BladeDecoderBase
{
    internal CGaFloat64OpnsTangentBladeDecoder(CGaFloat64Blade blade) 
        : base(blade)
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade VGaPosition()
    {
        var cgaGeometricSpace = Blade.GeometricSpace;

        return Blade
            .Gp(cgaGeometricSpace.Ei.Lcp(Blade).Inverse().Negative())
            .DecodeIpnsHyperSphereVGaCenter(cgaGeometricSpace);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade VGaDirection()
    {
        var cgaGeometricSpace = Blade.GeometricSpace;

        return cgaGeometricSpace.Ei
            .Lcp(Blade)
            .Op(cgaGeometricSpace.Ei)
            .Negative()
            .GetVGaPart(true)
            .DivideByNorm();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Weight()
    {
        return Weight(
            Blade.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Weight2D(LinFloat64Vector2D egaProbePoint)
    {
        return Weight(
            Blade.GeometricSpace.Encode.VGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Weight3D(LinFloat64Vector3D egaProbePoint)
    {
        return Weight(
            Blade.GeometricSpace.Encode.VGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Weight3(LinFloat64Vector egaProbePoint)
    {
        return Weight(
            Blade.GeometricSpace.Encode.VGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Weight(CGaFloat64Blade egaProbePoint)
    {
        var cgaGeometricSpace = Blade.GeometricSpace;

        var directionOpEi =
            cgaGeometricSpace.Ei
                .Lcp(Blade)
                .Op(cgaGeometricSpace.Ei)
                .Negative();

        return egaProbePoint
            .VGaVectorToIpnsPoint()
            .Lcp(directionOpEi)
            .SpSquared()
            .SqrtOfAbs();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent Element()
    {
        return Element(
            Blade.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent Element(LinFloat64Vector2D egaProbePoint)
    {
        return Element(
            Blade.GeometricSpace.Encode.VGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent Element(LinFloat64Vector3D egaProbePoint)
    {
        return Element(
            Blade.GeometricSpace.Encode.VGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Tangent Element(LinFloat64Vector egaProbePoint)
    {
        return Element(
            Blade.GeometricSpace.Encode.VGa.Vector(egaProbePoint)
        );
    }

    public CGaFloat64Tangent Element(CGaFloat64Blade egaProbePoint)
    {
        var cgaGeometricSpace = Blade.GeometricSpace;

        var position =
            Blade
                .Gp(cgaGeometricSpace.Ei.Lcp(Blade).Inverse().Negative())
                .DecodeIpnsHyperSphereVGaCenter(cgaGeometricSpace);

        var directionOpEi =
            cgaGeometricSpace.Ei
                .Lcp(Blade)
                .Op(cgaGeometricSpace.Ei)
                .Negative();

        var weight =
            egaProbePoint
                .VGaVectorToIpnsPoint()
                .Lcp(directionOpEi)
                .SpSquared()
                .SqrtOfAbs();

        return new CGaFloat64Tangent(
            cgaGeometricSpace,
            weight,
            position,
            directionOpEi.GetVGaPart(true)
        );
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaConformalParametricElement Element(this XGaConformalParametricBlade2D blade)
    //{
    //    if (!blade.Specs.IsOpnsTangent)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).Element()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaConformalParametricElement Element(this XGaConformalParametricBlade3D blade)
    //{
    //    if (!blade.Specs.IsOpnsTangent)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).Element()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaConformalParametricElement Element(this XGaConformalParametricBlade2D blade, IParametricCurve2D egaProbePoint)
    //{
    //    if (!blade.Specs.IsOpnsTangent)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).Element(
    //            egaProbePoint.GetPoint(t).Encode.VGa.VectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaConformalParametricElement Element(this XGaConformalParametricBlade3D blade, IParametricCurve3D egaProbePoint)
    //{
    //    if (!blade.Specs.IsOpnsTangent)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).Element(
    //            egaProbePoint.GetPoint(t).Encode.VGa.VectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}

}