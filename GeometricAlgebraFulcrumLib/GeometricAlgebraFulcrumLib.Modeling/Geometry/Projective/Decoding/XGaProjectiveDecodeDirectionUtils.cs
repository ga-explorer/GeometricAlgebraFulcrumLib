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

public static class XGaProjectiveDecodeDirectionUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeWeight<T>(this XGaProjectiveBlade<T> pgaDirection)
    {
        Debug.Assert(
            pgaDirection.IsIdealFlat()
        );

        return pgaDirection.PGaNorm();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> DecodeDirectionEGaDirection<T>(this XGaProjectiveBlade<T> pgaDirection)
    {
        //return pgaDirection.RemoveEi().DivideByNorm();
        throw new NotImplementedException();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DecodeDirection<T>(this XGaProjectiveBlade<T> pgaDirection)
    {
        return pgaDirection.DecodeDirection(
            pgaDirection.ProjectiveSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DecodeDirection<T>(this XGaProjectiveBlade<T> pgaDirection, LinVector2D<T> egaProbePoint)
    {
        return pgaDirection.DecodeDirection(
            pgaDirection.ProjectiveSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DecodeDirection<T>(this XGaProjectiveBlade<T> pgaDirection, LinVector3D<T> egaProbePoint)
    {
        return pgaDirection.DecodeDirection(
            pgaDirection.ProjectiveSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DecodeDirection<T>(this XGaProjectiveBlade<T> pgaDirection, LinVector<T> egaProbePoint)
    {
        return pgaDirection.DecodeDirection(
            pgaDirection.ProjectiveSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DecodeDirection<T>(this XGaProjectiveBlade<T> pgaDirection, XGaProjectiveBlade<T> egaProbePoint)
    {
        throw new NotImplementedException();

        //Debug.Assert(
        //    pgaDirection.IsIdealFlat() &&
        //    egaProbePoint.IsEGaVector()
        //);

        //var directionOpEi =
        //    pgaDirection;

        //var weight = pgaDirection.PGaNorm();

        //return new XGaProjectiveElement<T>(
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
    //        t => blade.GetBlade(t).DecodeOpnsDirection()
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
    //        t => blade.GetBlade(t).DecodeOpnsDirection()
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
    //        t => blade.GetBlade(t).DecodeOpnsDirection(
    //            egaProbePoint.GetPoint(t).EncodeEGaVectorBlade(blade.ProjectiveSpace)
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
    //        t => blade.GetBlade(t).DecodeOpnsDirection(
    //            egaProbePoint.GetPoint(t).EncodeEGaVectorBlade(blade.ProjectiveSpace)
    //        )
    //    );
    //}

}