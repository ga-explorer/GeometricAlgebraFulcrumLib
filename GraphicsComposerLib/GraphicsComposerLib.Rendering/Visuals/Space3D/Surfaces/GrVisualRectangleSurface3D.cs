using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Surfaces;

public sealed class GrVisualRectangleSurface3D :
    GrVisualSurface3D
{
    public double Width { get; }

    public double Height { get; }

    public Float64Tuple3D BottomLeftCorner { get; }

    public Float64Tuple3D WidthUnitDirection { get; }

    public Float64Tuple3D HeightUnitDirection { get; }

    public Float64Tuple3D WidthDirection 
        => Width * WidthUnitDirection;

    public Float64Tuple3D HeightDirection
        => Height * HeightUnitDirection;

    public Float64Tuple3D Normal
        => WidthUnitDirection.VectorUnitCross(HeightUnitDirection);


    public GrVisualRectangleSurface3D(string name, IFloat64Tuple3D bottomLeftCorner, IFloat64Tuple3D widthDirection, IFloat64Tuple3D heightDirection)
        : base(name)
    {
        Width = widthDirection.GetVectorNorm();
        Height = heightDirection.GetVectorNorm();

        BottomLeftCorner = bottomLeftCorner.ToTuple3D();
        WidthUnitDirection = widthDirection.ScaleBy(1d / Width);
        HeightUnitDirection = heightDirection.RejectOnVector(widthDirection).ToUnitVector();
    }
}