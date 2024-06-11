using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic.Elements;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic.Decoding;

public static class PGaDecodePGaElementUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> DecodePGaPoint2D<T>(this PGaBlade<T> pgaPoint)
    {
        Debug.Assert(
            pgaPoint.Geometry.Is3D &&
            pgaPoint.IsPGaPoint()
        );

        var hgaPoint =
            pgaPoint.PGaDual().PGaNormalize();

        return LinVector2D<T>.Create(
            hgaPoint[1],
            hgaPoint[2]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> DecodePGaIdealPoint2D<T>(this PGaBlade<T> pgaPoint)
    {
        Debug.Assert(
            pgaPoint.Geometry.Is3D &&
            pgaPoint.IsPGaIdealPoint()
        );

        var hgaPoint =
            pgaPoint.PGaDual().PGaNormalize();

        return LinVector2D<T>.Create(
            hgaPoint[1],
            hgaPoint[2]
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> DecodePGaPoint3D<T>(this PGaBlade<T> pgaPoint)
    {
        Debug.Assert(
            pgaPoint.Geometry.Is4D &&
            pgaPoint.IsPGaPoint()
        );

        var hgaPoint =
            pgaPoint.PGaDual().PGaNormalize();

        return LinVector3D<T>.Create(
            hgaPoint[1],
            hgaPoint[2],
            hgaPoint[3]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> DecodePGaIdealPoint3D<T>(this PGaBlade<T> pgaPoint)
    {
        Debug.Assert(
            pgaPoint.Geometry.Is4D &&
            pgaPoint.IsPGaIdealPoint()
        );

        var hgaPoint =
            pgaPoint.PGaDual().PGaNormalize();

        return LinVector3D<T>.Create(
            hgaPoint[1],
            hgaPoint[2],
            hgaPoint[3]
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DecodePGaElement<T>(this PGaBlade<T> pgaElement)
    {
        return pgaElement.DecodePGaElement(
            pgaElement.Geometry.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DecodePGaElement<T>(this PGaBlade<T> pgaElement, PGaBlade<T> egaProbePoint)
    {
        throw new NotImplementedException();
        //return pgaElement.PGaToIpns().DecodeIpnsElement(egaProbePoint);
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaProjectiveParametricElement<T> DecodePGaElement<T>(this XGaProjectiveParametricBlade2D blade)
    //{
    //    if (!blade.Specs.IsPGaElement)
    //        throw new InvalidOperationException();

    //    return XGaProjectiveParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodePGaElement()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaProjectiveParametricElement<T> DecodePGaElement<T>(this XGaProjectiveParametricBlade3D blade)
    //{
    //    if (!blade.Specs.IsPGaElement)
    //        throw new InvalidOperationException();

    //    return XGaProjectiveParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodePGaElement()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaProjectiveParametricElement<T> DecodePGaElement<T>(this XGaProjectiveParametricBlade2D blade, IParametricCurve3D egaProbePoint)
    //{
    //    if (!blade.Specs.IsPGaElement)
    //        throw new InvalidOperationException();

    //    return XGaProjectiveParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodePGaElement(
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ProjectiveSpace)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaProjectiveParametricElement<T> DecodePGaElement<T>(this XGaProjectiveParametricBlade3D blade, IParametricCurve3D egaProbePoint)
    //{
    //    if (!blade.Specs.IsPGaElement)
    //        throw new InvalidOperationException();

    //    return XGaProjectiveParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodePGaElement(
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ProjectiveSpace)
    //        )
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodePGaElementWeight<T>(this PGaBlade<T> pgaElement)
    {
        return pgaElement.PGaNorm();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> DecodePGaElementVGaDirection<T>(this PGaBlade<T> pgaElement)
    {
        return pgaElement
            .DecodePGaElementVGaNormalDirection()
            .Lcp(pgaElement.Geometry.Ie);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> DecodePGaElementVGaNormalDirection<T>(this PGaBlade<T> pgaElement)
    {
        throw new NotImplementedException();

        //if (pgaElement.IsIdealFlat())
        //{

        //}

        //return pgaElement.PGaToIpns().DecodeIpnsElementVGaNormalDirection();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> DecodePGaElementVGaPosition<T>(this PGaBlade<T> pgaElement)
    {
        throw new NotImplementedException();

        //return pgaElement.PGaToIpns().DecodeIpnsElementVGaPosition();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> DecodePGaElementVGaPosition<T>(this PGaBlade<T> pgaElement, LinVector2D<T> egaProbePoint)
    {
        throw new NotImplementedException();

        //return pgaElement.PGaToIpns().DecodeIpnsElementVGaPosition(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> DecodePGaElementVGaPosition<T>(this PGaBlade<T> pgaElement, LinVector3D<T> egaProbePoint)
    {
        throw new NotImplementedException();

        //return pgaElement.PGaToIpns().DecodeIpnsElementVGaPosition(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> DecodePGaElementVGaPosition<T>(this PGaBlade<T> pgaElement, LinVector<T> egaProbePoint)
    {
        throw new NotImplementedException();

        //return pgaElement.PGaToIpns().DecodeIpnsElementVGaPosition(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> DecodePGaElementVGaPosition<T>(this PGaBlade<T> pgaElement, XGaVector<T> egaProbePoint)
    {
        throw new NotImplementedException();

        //return pgaElement.PGaToIpns().DecodeIpnsElementVGaPosition(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> DecodePGaElementVGaPosition<T>(this PGaBlade<T> pgaElement, PGaBlade<T> egaProbePoint)
    {
        throw new NotImplementedException();

        //return pgaElement.PGaToIpns().DecodeIpnsElementVGaPosition(egaProbePoint);
    }

}