using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Surfaces;

public sealed class GrVisualRectangleSurface3D :
    GrVisualSurface3D
{
    public double Width { get; }

    public double Height { get; }

    public Tuple3D BottomLeftCorner { get; }

    public Tuple3D WidthUnitDirection { get; }

    public Tuple3D HeightUnitDirection { get; }

    public Tuple3D WidthDirection 
        => Width * WidthUnitDirection;

    public Tuple3D HeightDirection
        => Height * HeightUnitDirection;

    public Tuple3D Normal
        => WidthUnitDirection.VectorUnitCross(HeightUnitDirection);


    public GrVisualRectangleSurface3D(string name, ITuple3D bottomLeftCorner, ITuple3D widthDirection, ITuple3D heightDirection)
        : base(name)
    {
        Width = widthDirection.GetLength();
        Height = heightDirection.GetLength();

        BottomLeftCorner = bottomLeftCorner.ToTuple3D();
        WidthUnitDirection = widthDirection.ScaleBy(1d / Width);
        HeightUnitDirection = heightDirection.RejectOnVector(widthDirection).ToUnitVector();
    }
}