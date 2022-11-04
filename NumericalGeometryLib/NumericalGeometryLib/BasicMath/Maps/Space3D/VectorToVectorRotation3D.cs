using System.Diagnostics;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace NumericalGeometryLib.BasicMath.Maps.Space3D;

public sealed class VectorToVectorRotation3D :
    SimpleVectorRotation3D
{
    private readonly Tuple3D _uVector;
    private readonly Tuple3D _vVector;
    private readonly Tuple3D _vuProjVector;
    private readonly double _vuDotPlus1Inv;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorToVectorRotation3D(ITuple3D u, ITuple3D v)
    {
        _uVector = u.ToUnitVector();
        _vVector = v.ToUnitVector();

        var vuDot = v.VectorDot(u);

        Debug.Assert(
            !(vuDot + 1d).IsNearZero()
        );

        _vuDotPlus1Inv = 1d / (1d + vuDot);
        _vuProjVector = _vVector - vuDot * _uVector;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Tuple3D Rotate(ITuple3D x)
    {
        var r = x.VectorDot(_vuProjVector) * _vuDotPlus1Inv;
        var s = x.VectorDot(_uVector);

        return x - (r + s) * _uVector - (r - s) * _vVector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override SimpleVectorRotation3D GetInverse()
    {
        return new VectorToVectorRotation3D(
            _vVector,
            _uVector
        );
    }
}