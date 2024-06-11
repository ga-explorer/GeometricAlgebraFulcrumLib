using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives.Vertices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Structures.Vertices;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Surfaces;

public sealed record GrParametricSurfaceLocalFrame3D :
    IGraphicsVertex3D
{

    public int VSpaceDimensions
        => 3;

    public Float64Scalar Item1
        => Point.X;

    public Float64Scalar Item2
        => Point.Y;

    public Float64Scalar Item3
        => Point.Z;

    public Float64Scalar X
        => Point.X;

    public Float64Scalar Y
        => Point.Y;

    public Float64Scalar Z
        => Point.Z;

    public int Index { get; internal set; } = -1;

    public LinFloat64Vector3D Point { get; }

    public Color Color { get; set; }

    public Pair<Float64Scalar> ParameterValue { get; }

    public LinFloat64Normal3D Normal { get; }

    public bool HasParameterValue
        => true;

    public bool HasNormal
        => true;

    public bool HasColor
        => true;

    public GraphicsVertexDataKind3D DataKind
        => GraphicsVertexDataKind3D.NormalTextureColorData;


    internal GrParametricSurfaceLocalFrame3D(double parameterValue1, double parameterValue2, ILinFloat64Vector3D point, ILinFloat64Vector3D normal)
    {
        ParameterValue = new Pair<Float64Scalar>(parameterValue1, parameterValue2);
        Point = point.ToLinVector3D();
        Normal = new LinFloat64Normal3D(normal);

        Debug.Assert(IsValid());
    }

    internal GrParametricSurfaceLocalFrame3D(IGraphicsParametricSurface3D surface, double parameterValue1, double parameterValue2)
    {
        ParameterValue = new Pair<Float64Scalar>(parameterValue1, parameterValue2);
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