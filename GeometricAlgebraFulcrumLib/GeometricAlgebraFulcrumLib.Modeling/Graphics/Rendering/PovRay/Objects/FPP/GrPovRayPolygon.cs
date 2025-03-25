using System.Collections;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Lists;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.FPP;

public class GrPovRayPolygon :
    GrPovRayObject,
    IGrPovRayFinitePatchObject,
    IReadOnlyList<GrPovRayVector2Value>
{
    public static GrPovRayPolygon CreateSquare(double length)
    {
        var x = length / 2d;
        var y = length / 2d;

        var polygon = new GrPovRayPolygon();

        return polygon.AddClosedPolygon(
            LinFloat64Vector2D.Create(x, y),
            LinFloat64Vector2D.Create(-x, y),
            LinFloat64Vector2D.Create(-x, -y),
            LinFloat64Vector2D.Create(x, -y)
        );
    }

    public static GrPovRayPolygon CreateRectangleXy(double width, double height)
    {
        var x = width / 2d;
        var y = height / 2d;

        var polygon = new GrPovRayPolygon();

        return polygon.AddClosedPolygon(
            LinFloat64Vector2D.Create(x, y),
            LinFloat64Vector2D.Create(-x, y),
            LinFloat64Vector2D.Create(-x, -y),
            LinFloat64Vector2D.Create(x, -y)
        );
    }

    public static GrPovRayPolygon CreateRectangleYz(double width, double height)
    {
        var polygon = CreateRectangleXy(width, height);

        polygon.AffineMap.Rotate(
            LinBasisVectorPair3D.PxPy, 
            LinBasisVectorPair3D.PyPz
        );

        return polygon;
    }
    
    public static GrPovRayPolygon CreateRectangleZy(double width, double height)
    {
        var polygon = CreateRectangleXy(width, height);

        polygon.AffineMap.Rotate(
            LinBasisVector3D.Px, 
            LinBasisVector3D.Pz
        );

        return polygon;
    }

    public static GrPovRayPolygon CreateRectangleZx(double width, double height)
    {
        var polygon = CreateRectangleXy(width, height);

        polygon.AffineMap.Rotate(
            LinBasisVectorPair3D.PxPy, 
            LinBasisVectorPair3D.PzPx
        );

        return polygon;
    }
    
    public static GrPovRayPolygon CreateRectangleXz(double width, double height)
    {
        var polygon = CreateRectangleXy(width, height);

        polygon.AffineMap.Rotate(
            LinBasisVector3D.Py, 
            LinBasisVector3D.Pz
        );

        return polygon;
    }

    public static GrPovRayPolygon CreateRegularPolygon(int sideCount, double radius)
    {
        var polygon = new GrPovRayPolygon();

        var vertexList = 
            0d.GetLinearRange(Math.Tau, sideCount, true)
                .Select(a => a.RadiansToPolarAngle().ToLinVector2D(radius))
                .Cast<IPair<Float64Scalar>>()
                .ToArray();

        polygon.AddClosedPolygon(vertexList);

        return polygon;
    }
    
    public static GrPovRayPolygon CreateClosedPolygon(IReadOnlyList<IPair<Float64Scalar>> pointList)
    {
        return new GrPovRayPolygon().AddClosedPolygon(pointList);
    }
    
    public static GrPovRayPolygon CreateClosedPolygon(params IPair<Float64Scalar>[] pointList)
    {
        return new GrPovRayPolygon().AddClosedPolygon(pointList);
    }

    public static GrPovRayPolygon CreatePolygon()
    {
        return new GrPovRayPolygon();
    }


    public GrPovRayVector2List Points { get; }
        = new GrPovRayVector2List();

    public int Count
        => Points.Count;

    public GrPovRayVector2Value this[int index]
    {
        get => Points[index];
        set => Points[index] = value;
    }
    

    private GrPovRayPolygon()
    {
    }

    
    public GrPovRayPolygon AddClosedPolygon(IReadOnlyList<IPair<Float64Scalar>> pointList)
    {
        foreach (var point in pointList)
            Points.Append(point);

        Points.Append(pointList.First());

        return this;
    }

    public GrPovRayPolygon AddClosedPolygon(params IPair<Float64Scalar>[] pointList)
    {
        foreach (var point in pointList)
            Points.Append(point);

        Points.Append(pointList.First());

        return this;
    }


    public GrPovRayPolygon SetProperties(GrPovRayObjectProperties properties)
    {
        Properties = new GrPovRayObjectProperties(properties);

        return this;
    }

    public override string GetPovRayCode()
    {
        var composer = new LinearTextComposer();

        composer
            .AppendLine("polygon {")
            .IncreaseIndentation()
            .AppendAtNewLine(Points.Count + ", ")
            .AppendAtNewLine(Points.GetPovRayCode())
            .AppendAtNewLine(GetModifiersCode())
            .DecreaseIndentation()
            .AppendAtNewLine("}");

        return composer.ToString();
    }

    public IEnumerator<GrPovRayVector2Value> GetEnumerator()
    {
        return Points.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}