using System.Diagnostics;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath.Constants;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace NumericalGeometryLib.BasicMath.Maps.Space3D;

public sealed class VectorToAxisRotation3D :
    SimpleVectorRotation3D
{
    private readonly Tuple3D _uVector;
    private readonly Tuple3D _vVector;
    //private readonly Axis3D _vAxis;
    private readonly Tuple3D _vuProjVector;
    private readonly double _vuDotPlus1Inv;


    public VectorToAxisRotation3D(ITuple3D u, Axis3D vAxis)
    {
        _uVector = u.ToUnitVector();
        _vVector = vAxis.GetVector3D();
        //_vAxis = vAxis;

        var vuDot = u.GetComponent(vAxis);

        Debug.Assert(
            !(vuDot + 1d).IsNearZero()
        );

        _vuDotPlus1Inv = 1d / (1d + vuDot);
        _vuProjVector = _vVector - vuDot * _uVector;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Tuple3D Rotate(ITuple3D x)
    {
        //var r = x.VectorDot(_vuProjVector) * _vuDotPlus1Inv;
        //var s = x.VectorDot(_uVector);

        //return x - (r + s) * _uVector - (r - s) * _vVector;

        var r = x.VectorDot(_vuProjVector) * _vuDotPlus1Inv;
        var s = x.VectorDot(_uVector);

        return x - (r + s) * _uVector - (r - s) * _vVector;
    }

    public override SimpleVectorRotation3D GetInverse()
    {
        throw new System.NotImplementedException();
    }
}