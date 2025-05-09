using System.Collections;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Polytopes.Space2D.Float64;

public sealed record Float64TriangleGrid2D :
    Float64Grid2D,
    IEnumerable<Float64LineSegment2D>
{
    public static Float64TriangleGrid2D Create(double cellRadius, double gridWidth, double gridHeight)
    {
        return new Float64TriangleGrid2D(
            LinFloat64Vector2D.Zero,
            LinFloat64Vector2D.Create(0, cellRadius),
            gridWidth,
            gridHeight
        );
    }

    public static Float64TriangleGrid2D Create(LinFloat64Vector2D baseVector, double gridWidth, double gridHeight)
    {
        return new Float64TriangleGrid2D(
            LinFloat64Vector2D.Zero,
            baseVector,
            gridWidth,
            gridHeight
        );
    }

    public static Float64TriangleGrid2D Create(LinFloat64Vector2D origin, LinFloat64Vector2D baseVector, double gridWidth, double gridHeight)
    {
        return new Float64TriangleGrid2D(
            origin, baseVector, gridWidth, gridHeight
        );
    }


    public LinFloat64Vector2D Origin { get; }

    public Triplet<LinFloat64Vector2D> BaseVectors { get; }

    public Float64RegularPolygon2D TriangleSpecs { get; }

    public double CellRadius
        => TriangleSpecs.OuterRadius;


    private Float64TriangleGrid2D(LinFloat64Vector2D origin, LinFloat64Vector2D baseVector, double gridWidth, double gridHeight)
        : base(gridWidth, gridHeight)
    {
        Debug.Assert(gridWidth.IsFinite() && gridWidth > 0);
        Debug.Assert(gridHeight.IsFinite() && gridHeight > 0);

        Origin = origin;

        BaseVectors = new Triplet<LinFloat64Vector2D>(
            baseVector,
            baseVector.RotateBy(LinFloat64PolarAngle.Angle120),
            baseVector.RotateBy(LinFloat64PolarAngle.Angle240)
        );

        TriangleSpecs = Float64RegularPolygon2D.CreateFromOuterRadius(3, baseVector.Norm());

        Debug.Assert(IsValid());
    }


    public override bool IsValid()
    {
        return GridWidth.IsFinite() && 
               GridWidth > 0 && 
               GridHeight.IsFinite() && 
               GridHeight > 0;
    }

    
    public Float64TriangleGrid2D GetSubGrid(int subDivisionCount, int level, LinFloat64Vector2D origin, LinFloat64Angle angle)
    {
        var scalingFactor = 
            Math.Pow(subDivisionCount, -level);

        return new Float64TriangleGrid2D(
            origin,
            BaseVectors.Item1.RotateBy(angle).ScaleBy(scalingFactor),
            GridWidth,
            GridHeight
        );
    }

    public Float64TriangleGrid2D ScaleBaseVectors(double scalingFactor)
    {
        return new Float64TriangleGrid2D(
            Origin,
            BaseVectors.Item1.ScaleBy(scalingFactor),
            GridWidth,
            GridHeight
        );
    }

    public Float64TriangleGrid2D RotateBaseVectors(LinFloat64Angle angle)
    {
        return new Float64TriangleGrid2D(
            Origin,
            BaseVectors.Item1.RotateBy(angle),
            GridWidth,
            GridHeight
        );
    }

    public Float64TriangleGrid2D RotateScaleBaseVectors(LinFloat64Angle angle, double scalingFactor)
    {
        return new Float64TriangleGrid2D(
            Origin,
            BaseVectors.Item1.RotateBy(angle).ScaleBy(scalingFactor),
            GridWidth,
            GridHeight
        );
    }

    public Float64TriangleGrid2D SetBaseVectorAngle(LinFloat64Angle angle)
    {
        return new Float64TriangleGrid2D(
            Origin,
            LinFloat64Vector2D.Create(CellRadius, 0).RotateBy(angle),
            GridWidth,
            GridHeight
        );
    }

    public Float64TriangleGrid2D SetBaseVectorsLength(double length)
    {
        return new Float64TriangleGrid2D(
            Origin,
            BaseVectors.Item1.SetLength(length),
            GridWidth,
            GridHeight
        );
    }

    public Float64TriangleGrid2D SetOrigin(LinFloat64Vector2D origin)
    {
        return new Float64TriangleGrid2D(
            origin,
            BaseVectors.Item1,
            GridWidth,
            GridHeight
        );
    }


    ///// <summary>
    ///// Line intersection with Axis-Aligned-Box using Skala algorithm
    ///// https://en.wikipedia.org/wiki/Line_clipping
    ///// https://link.springer.com/article/10.1007/s00371-005-0305-3
    ///// </summary>
    //private Tuple<bool, LineSegment2D> LineGridIntersection(LinFloat64Vector2D lineOrigin, LinFloat64Vector2D lineDirection)
    //{
    //    const int n = 4;

    //    var x = new LinFloat64Vector3D[]
    //    {
    //        LinFloat64Vector3D.Create(-GridWidth / 2, -GridHeight / 2, 1),
    //        LinFloat64Vector3D.Create(GridWidth / 2, -GridHeight / 2, 1),
    //        LinFloat64Vector3D.Create(GridWidth / 2, GridHeight / 2, 1),
    //        LinFloat64Vector3D.Create(-GridWidth / 2, GridHeight / 2, 1)
    //    };

    //    var e = new LinFloat64Vector3D[]
    //    {
    //        x[1] - x[0],
    //        x[2] - x[1],
    //        x[3] - x[2],
    //        x[0] - x[3]
    //    };

    //    var tab1 = new int[] { -1, 0, 0, 1, 1, -2, 0, 2, 2, 0, -2, 1, 1, 0, 0, -1 };
    //    var tab2 = new int[] { -1, 3, 1, 3, 2, -2, 2, 3, 3, 2, -2, 2, 3, 1, 3, -1 };

    //    var xA = lineOrigin.ToXyLinVector3D() + LinFloat64Vector3D.E3;
    //    var xB = xA + lineDirection.ToXyLinVector3D();

    //    var p = xA.VectorCross(xB);

    //    var c = 0;
    //    for (var k = 0; k < n; k++)
    //        if (p.VectorESp(x[k]) >= 0) c |= 1 << k;

    //    Debug.Assert(tab1[c] >= -1 && tab2[c] >= -1);

    //    if (tab1[c] < 0 && tab2[c] < 0)
    //        return new Tuple<bool, LineSegment2D>(
    //            false,
    //            LineSegment2D.Create(0, 0, 1, 1)
    //        );

    //    var i = tab1[c];
    //    var j = tab2[c];

    //    var yA = p.VectorCross(e[i]);
    //    var yB = p.VectorCross(e[j]);

    //    return new Tuple<bool, LineSegment2D>(
    //        true,
    //        LineSegment2D.Create(
    //            yA.Item1 / yA.Item3,
    //            yA.Item2 / yA.Item3,
    //            yB.Item1 / yB.Item3,
    //            yB.Item2 / yB.Item3
    //        )
    //    );
    //}

    private IEnumerable<LinFloat64Vector2D> GetPoints(LinFloat64Vector2D baseVector)
    {
        var v =
            baseVector
                .RotateBy(LinFloat64PolarAngle.Angle90)
                .SetLength(TriangleSpecs.InnerRadius + TriangleSpecs.OuterRadius);

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
            .Concat(GetLines(BaseVectors.Item3))
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}