using DataStructuresLib.Basic;
using NumericalGeometryLib.BasicMath.Constants;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Surfaces;

public sealed class GrVisualCircleSurface3D :
    GrVisualSurface3D
{
    public ITuple3D Center { get; set; } = Tuple3D.Zero;

    public ITuple3D Normal { get; set; } = Tuple3D.E2;

    public double Radius { get; set; } = 1d;

    public bool DrawEdge { get; set; } = false;


    public GrVisualCircleSurface3D(string name) 
        : base(name)
    {
    }

    
    public Triplet<Tuple3D> GetEdgePointsTriplet()
    {
        var quaternion = Axis3D.PositiveZ.CreateAxisToVectorRotationQuaternion(
            Normal.ToUnitVector()
        );

        const double angle = 2d * Math.PI / 3d;

        var a = Radius * Math.Cos(angle);
        var b = Radius * Math.Sin(angle);

        var point1 = Center + quaternion.QuaternionRotate(Radius, 0, 0);
        var point2 = Center + quaternion.QuaternionRotate(a, b, 0);
        var point3 = Center + quaternion.QuaternionRotate(a, -b, 0);

        return new Triplet<Tuple3D>(point1, point2, point3);
    }
}