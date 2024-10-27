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

public static class PGaDecodeDirectionUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeWeight<T>(this PGaBlade<T> pgaDirection)
    {
        Debug.Assert(
            pgaDirection.IsIdealFlat()
        );

        return pgaDirection.PGaNorm();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> DecodeDirectionVGaDirection<T>(this PGaBlade<T> pgaDirection)
    {
        //return pgaDirection.RemoveEi().DivideByNorm();
        throw new NotImplementedException();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DecodeDirection<T>(this PGaBlade<T> pgaDirection)
    {
        return pgaDirection.DecodeDirection(
            pgaDirection.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DecodeDirection<T>(this PGaBlade<T> pgaDirection, LinVector2D<T> egaProbePoint)
    {
        return pgaDirection.DecodeDirection(
            pgaDirection.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DecodeDirection<T>(this PGaBlade<T> pgaDirection, LinVector3D<T> egaProbePoint)
    {
        return pgaDirection.DecodeDirection(
            pgaDirection.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DecodeDirection<T>(this PGaBlade<T> pgaDirection, LinVector<T> egaProbePoint)
    {
        return pgaDirection.DecodeDirection(
            pgaDirection.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DecodeDirection<T>(this PGaBlade<T> pgaDirection, PGaBlade<T> egaProbePoint)
    {
        throw new NotImplementedException();

        //Debug.Assert(
        //    pgaDirection.IsIdealFlat() &&
        //    egaProbePoint.IsVGaVector()
        //);

        //var directionOpEi =
        //    pgaDirection;

        //var weight = pgaDirection.PGaNorm();

        //return new PGaElement<T>(
        //    pgaDirection.ProjectiveSpace,
        //    weight,
        //    directionOpEi.RemoveEi()
        //);
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaProjectiveParametricElement<T> DecodeOpnsDirection<T>(this XGaProjectiveParametricBlade2D blade)
    //{
    //    if (!blade.Specs.IsOpnsDirection)
    //        throw new InvalidOperationException();

    //    return XGaProjectiveParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeOpnsDirection.Element()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaProjectiveParametricElement<T> DecodeOpnsDirection<T>(this XGaProjectiveParametricBlade3D blade)
    //{
    //    if (!blade.Specs.IsOpnsDirection)
    //        throw new InvalidOperationException();

    //    return XGaProjectiveParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeOpnsDirection.Element()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaProjectiveParametricElement<T> DecodeOpnsDirection<T>(this XGaProjectiveParametricBlade2D blade, IParametricCurve2D egaProbePoint)
    //{
    //    if (!blade.Specs.IsOpnsDirection)
    //        throw new InvalidOperationException();

    //    return XGaProjectiveParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeOpnsDirection.Element(
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ProjectiveSpace)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaProjectiveParametricElement<T> DecodeOpnsDirection<T>(this XGaProjectiveParametricBlade3D blade, IParametricCurve3D egaProbePoint)
    //{
    //    if (!blade.Specs.IsOpnsDirection)
    //        throw new InvalidOperationException();

    //    return XGaProjectiveParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeOpnsDirection.Element(
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ProjectiveSpace)
    //        )
    //    );
    //}

}