using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Projective.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Projective.Elements;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Projective.Decoding;

public static class XGaProjectiveDecodePGaElementUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> DecodePGaPoint2D<T>(this XGaProjectiveBlade<T> pgaPoint)
    {
        Debug.Assert(
            pgaPoint.ProjectiveSpace.Is3D &&
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
    public static LinVector2D<T> DecodePGaIdealPoint2D<T>(this XGaProjectiveBlade<T> pgaPoint)
    {
        Debug.Assert(
            pgaPoint.ProjectiveSpace.Is3D &&
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
    public static LinVector3D<T> DecodePGaPoint3D<T>(this XGaProjectiveBlade<T> pgaPoint)
    {
        Debug.Assert(
            pgaPoint.ProjectiveSpace.Is4D &&
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
    public static LinVector3D<T> DecodePGaIdealPoint3D<T>(this XGaProjectiveBlade<T> pgaPoint)
    {
        Debug.Assert(
            pgaPoint.ProjectiveSpace.Is4D &&
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
    public static XGaProjectiveElement<T> DecodePGaElement<T>(this XGaProjectiveBlade<T> pgaElement)
    {
        return pgaElement.DecodePGaElement(
            pgaElement.ProjectiveSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DecodePGaElement<T>(this XGaProjectiveBlade<T> pgaElement, XGaProjectiveBlade<T> egaProbePoint)
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
    //            egaProbePoint.GetPoint(t).EncodeEGaVectorBlade(blade.ProjectiveSpace)
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
    //            egaProbePoint.GetPoint(t).EncodeEGaVectorBlade(blade.ProjectiveSpace)
    //        )
    //    );
    //}
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodePGaElementWeight<T>(this XGaProjectiveBlade<T> pgaElement)
    {
        return pgaElement.PGaNorm();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> DecodePGaElementEGaDirection<T>(this XGaProjectiveBlade<T> pgaElement)
    {
        return pgaElement
            .DecodePGaElementEGaNormalDirection()
            .Lcp(pgaElement.ProjectiveSpace.Ie);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> DecodePGaElementEGaNormalDirection<T>(this XGaProjectiveBlade<T> pgaElement)
    {
        throw new NotImplementedException();

        //if (pgaElement.IsIdealFlat())
        //{

        //}

        //return pgaElement.PGaToIpns().DecodeIpnsElementEGaNormalDirection();
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> DecodePGaElementEGaPosition<T>(this XGaProjectiveBlade<T> pgaElement)
    {
        throw new NotImplementedException();

        //return pgaElement.PGaToIpns().DecodeIpnsElementEGaPosition();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> DecodePGaElementEGaPosition<T>(this XGaProjectiveBlade<T> pgaElement, LinVector2D<T> egaProbePoint)
    {
        throw new NotImplementedException();

        //return pgaElement.PGaToIpns().DecodeIpnsElementEGaPosition(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> DecodePGaElementEGaPosition<T>(this XGaProjectiveBlade<T> pgaElement, LinVector3D<T> egaProbePoint)
    {
        throw new NotImplementedException();

        //return pgaElement.PGaToIpns().DecodeIpnsElementEGaPosition(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> DecodePGaElementEGaPosition<T>(this XGaProjectiveBlade<T> pgaElement, LinVector<T> egaProbePoint)
    {
        throw new NotImplementedException();

        //return pgaElement.PGaToIpns().DecodeIpnsElementEGaPosition(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> DecodePGaElementEGaPosition<T>(this XGaProjectiveBlade<T> pgaElement, XGaVector<T> egaProbePoint)
    {
        throw new NotImplementedException();

        //return pgaElement.PGaToIpns().DecodeIpnsElementEGaPosition(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> DecodePGaElementEGaPosition<T>(this XGaProjectiveBlade<T> pgaElement, XGaProjectiveBlade<T> egaProbePoint)
    {
        throw new NotImplementedException();

        //return pgaElement.PGaToIpns().DecodeIpnsElementEGaPosition(egaProbePoint);
    }

}