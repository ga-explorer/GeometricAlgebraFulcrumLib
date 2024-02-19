using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space3D.Objects;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space3D.Maps;

public sealed class E3DMapTranslate<T> :
    E3DMap<T>
{
    public override IScalarProcessor<T> ScalarProcessor 
        => Direction.ScalarProcessor;

    public E3DVector<T> Direction { get; }



    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal E3DMapTranslate(E3DVector<T> vector)
    {
        Direction = vector;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override E3DVector<T> Map(E3DVector<T> vector)
    {
        return vector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override E3DPoint<T> Map(E3DPoint<T> point)
    {
        return point + Direction;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override E3DMap<T> GetInverse()
    {
        return new E3DMapTranslate<T>(-Direction);
    }
}