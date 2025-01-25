using System.Collections;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Polytopes.Space2D.Float64;

public sealed record Float64SquareGrid2D :
    Float64Grid2D,
    IEnumerable<Float64LineSegment2D>
{
    public static Float64SquareGrid2D Create(double cellRadius, double gridWidth, double gridHeight)
    {
        return new Float64SquareGrid2D(
            LinFloat64Vector2D.Zero,
            LinFloat64Vector2D.Create(0, cellRadius),
            gridWidth,
            gridHeight
        );
    }

    public static Float64SquareGrid2D Create(LinFloat64Vector2D baseVector, double gridWidth, double gridHeight)
    {
        return new Float64SquareGrid2D(
            LinFloat64Vector2D.Zero,
            baseVector,
            gridWidth,
            gridHeight
        );
    }

    public static Float64SquareGrid2D Create(LinFloat64Vector2D origin, LinFloat64Vector2D baseVector, double gridWidth, double gridHeight)
    {
        return new Float64SquareGrid2D(
            origin, baseVector, gridWidth, gridHeight
        );
    }


    public LinFloat64Vector2D Origin { get; }

    public Pair<LinFloat64Vector2D> BaseVectors { get; }

    public Float64RegularPolygon2D SquareSpecs { get; }

    public double CellRadius
        => SquareSpecs.OuterRadius;


    private Float64SquareGrid2D(LinFloat64Vector2D origin, LinFloat64Vector2D baseVector, double gridWidth, double gridHeight)
        : base(gridWidth, gridHeight)
    {
        Debug.Assert(gridWidth.IsFinite() && gridWidth > 0);
        Debug.Assert(gridHeight.IsFinite() && gridHeight > 0);

        Origin = origin;

        BaseVectors = new Pair<LinFloat64Vector2D>(
            baseVector,
            baseVector.RotateBy(LinFloat64PolarAngle.Angle90)
        );

        SquareSpecs = Float64RegularPolygon2D.CreateFromOuterRadius(4, baseVector.Norm());

        Debug.Assert(IsValid());
    }


    public override bool IsValid()
    {
        return GridWidth.IsFinite() && 
               GridWidth > 0 && 
               GridHeight.IsFinite() && 
               GridHeight > 0;
    }

    
    public Float64SquareGrid2D GetSubGrid(int subDivisionCount, int level, LinFloat64Vector2D origin, LinFloat64Angle angle)
    {
        var scalingFactor = 
            Math.Pow(subDivisionCount, -level);

        return new Float64SquareGrid2D(
            origin,
            BaseVectors.Item1.RotateBy(angle).ScaleBy(scalingFactor),
            GridWidth,
            GridHeight
        );
    }

    public Float64SquareGrid2D ScaleBaseVectors(double scalingFactor)
    {
        return new Float64SquareGrid2D(
            Origin,
            BaseVectors.Item1.ScaleBy(scalingFactor),
            GridWidth,
            GridHeight
        );
    }

    public Float64SquareGrid2D RotateBaseVectors(LinFloat64Angle angle)
    {
        return new Float64SquareGrid2D(
            Origin,
            BaseVectors.Item1.RotateBy(angle),
            GridWidth,
            GridHeight
        );
    }

    public Float64SquareGrid2D RotateScaleBaseVectors(LinFloat64Angle angle, double scalingFactor)
    {
        return new Float64SquareGrid2D(
            Origin,
            BaseVectors.Item1.RotateBy(angle).ScaleBy(scalingFactor),
            GridWidth,
            GridHeight
        );
    }

    public Float64SquareGrid2D SetBaseVectorAngle(LinFloat64Angle angle)
    {
        return new Float64SquareGrid2D(
            Origin,
            LinFloat64Vector2D.Create(CellRadius, 0).RotateBy(angle),
            GridWidth,
            GridHeight
        );
    }

    public Float64SquareGrid2D SetBaseVectorsLength(double length)
    {
        return new Float64SquareGrid2D(
            Origin,
            BaseVectors.Item1.SetLength(length),
            GridWidth,
            GridHeight
        );
    }

    public Float64SquareGrid2D SetOrigin(LinFloat64Vector2D origin)
    {
        return new Float64SquareGrid2D(
            origin,
            BaseVectors.Item1,
            GridWidth,
            GridHeight
        );
    }


    private IEnumerable<LinFloat64Vector2D> GetPoints(LinFloat64Vector2D baseVector)
    {
        var v =
            baseVector
                .RotateBy(LinFloat64PolarAngle.Angle90)
                .SetLength(SquareSpecs.InnerRadius + SquareSpecs.OuterRadius);

        var p = Origin;
        
        yield return p;

        while (p.NormSquared() <= GridDiagonalSquared)
        {
            p += v;

            yield return p;
        }

        p = Origin;
        while (p.NormSquared() <= GridDiagonalSquared)
        {
            p -= v;

            yield return p;
        }
    }

    private IEnumerable<Float64LineSegment2D> GetLines(LinFloat64Vector2D baseVector)
    {
        var v = baseVector.SetLength(GridDiagonal);

        foreach (var p in GetPoints(baseVector))
        {
            var p1 = p - v;
            var p2 = p + v;

            var line = Float64Line2D.Create(p1, p2 - p1);

            var (flag, segment) = this.ClipLine(line);

            if (flag)
                yield return segment;
        }
    }


    public IEnumerator<Float64LineSegment2D> GetEnumerator()
    {
        return GetLines(BaseVectors.Item1)
            .Concat(GetLines(BaseVectors.Item2))
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}