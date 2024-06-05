using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Geometry.Euclidean.Space3D.Objects;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Geometry.Euclidean.Space3D.Maps;

public sealed class E3DMapAffine<T> :
    E3DMap<T>
{
    public override IScalarProcessor<T> ScalarProcessor 
        => OriginImage.ScalarProcessor;

    public E3DPoint<T> OriginImage { get; }

    public E3DVector<T> XAxisImage { get; }

    public E3DVector<T> YAxisImage { get; }

    public E3DVector<T> ZAxisImage { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal E3DMapAffine(E3DPoint<T> originImage, E3DVector<T> xImage, E3DVector<T> yImage, E3DVector<T> zImage)
    {
        OriginImage = originImage;
        XAxisImage = xImage;
        YAxisImage = yImage;
        ZAxisImage = zImage;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override E3DVector<T> Map(E3DVector<T> vector)
    {
        return vector.X * XAxisImage +
               vector.Y * YAxisImage +
               vector.Z * ZAxisImage;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override E3DPoint<T> Map(E3DPoint<T> point)
    {
        return OriginImage +
               point.X * XAxisImage +
               point.Y * YAxisImage +
               point.Z * ZAxisImage;
    }

    public override E3DMap<T> GetInverse()
    {
        throw new System.NotImplementedException();
    }
}