using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives.Vertices;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Structures.Vertices;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Surfaces;

public sealed record GrParametricSurfaceLocalFrame3D :
    IGraphicsVertex3D
{

    public int VSpaceDimensions
        => 3;

    public double Item1
        => Point.X;

    public double Item2
        => Point.Y;

    public double Item3
        => Point.Z;

    public double X
        => Point.X;

    public double Y
        => Point.Y;

    public double Z
        => Point.Z;

    public int Index { get; internal set; } = -1;

    public LinFloat64Vector3D Point { get; }

    public Color Color { get; set; }

    public LinFloat64Vector2D ParameterValue { get; }

    public Float64Scalar ParameterValueU 
        => ParameterValue.Item1;

    public Float64Scalar ParameterValueV 
        => ParameterValue.Item2;

    public LinFloat64Normal3D Normal { get; }

    public bool HasParameterValue
        => true;

    public bool HasNormal
        => true;

    public bool HasColor
        => true;

    public GraphicsVertexDataKind3D DataKind
        => GraphicsVertexDataKind3D.NormalTextureColorData;
    
    public double this[int index] 
        => index switch
        {
            0 => X,
            1 => Y,
            2 => Z,
            _ => Float64Scalar.Zero
        };


    internal GrParametricSurfaceLocalFrame3D(double parameterValue1, double parameterValue2, ILinFloat64Vector3D point, ILinFloat64Vector3D normal)
    {
        ParameterValue = LinFloat64Vector2D.Create(parameterValue1, parameterValue2);
        Point = point.ToLinVector3D();
        Normal = new LinFloat64Normal3D(normal);

        Debug.Assert(IsValid());
    }

    internal GrParametricSurfaceLocalFrame3D(IGraphicsParametricSurface3D surface, double parameterValue1, double parameterValue2)
    {
        ParameterValue = LinFloat64Vector2D.Create(parameterValue1, parameterValue2);
        Point = surface.GetPoint(parameterValue1, parameterValue2);
        Normal = new LinFloat64Normal3D(
            surface.GetNormal(parameterValue1, parameterValue2).ToUnitLinVector3D()
        );

        Debug.Assert(IsValid());
    }


    public bool IsValid()
    {
        return ParameterValue.Item1.IsValid() &&
               ParameterValue.Item2.IsValid() &&
               Point.IsValid() &&
               Normal.IsValid();
    }

}