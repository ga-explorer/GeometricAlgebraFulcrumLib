using System.Collections.Immutable;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Polytopes.Space2D.Float64;

public sealed record Float64PolarGrid2D :
    Float64Grid2D
{
    public static Float64PolarGrid2D Create(double cellRadius, int radialLineCount, double gridWidth, double gridHeight)
    {
        return new Float64PolarGrid2D(
            LinFloat64Vector2D.Zero,
            LinFloat64Vector2D.Create(cellRadius, 0),
            radialLineCount, 
            gridWidth, 
            gridHeight
        );
    }

    public static Float64PolarGrid2D Create(LinFloat64Vector2D baseVector, int radialLineCount, double gridWidth, double gridHeight)
    {
        return new Float64PolarGrid2D(
            LinFloat64Vector2D.Zero,
            baseVector, 
            radialLineCount, 
            gridWidth, 
            gridHeight
        );
    }

    public static Float64PolarGrid2D Create(LinFloat64Vector2D origin, LinFloat64Vector2D baseVector, int radialLineCount, double gridWidth, double gridHeight)
    {
        return new Float64PolarGrid2D(
            origin,
            baseVector, 
            radialLineCount, 
            gridWidth, 
            gridHeight
        );
    }


    public LinFloat64Vector2D Origin { get; }

    public IReadOnlyList<LinFloat64Vector2D> BaseVectors { get; }

    public int RadialLineCount 
        => BaseVectors.Count;

    public double BaseCircleRadius 
        => BaseVectors[0].Norm();


    private Float64PolarGrid2D(LinFloat64Vector2D origin, LinFloat64Vector2D baseVector, int radialLineCount, double gridWidth, double gridHeight) 
        : base(gridWidth, gridHeight)
    {
        Origin = origin;

        BaseVectors = 0d.GetLinearPeriodicRange(
            Math.Tau, 
            radialLineCount
        ).Select(
            r => baseVector.RotateBy(r.RadiansToPolarAngle())
        ).ToImmutableArray();

        Debug.Assert(IsValid());
    }


    public override bool IsValid()
    {
        return GridWidth.IsFinite() && 
               GridWidth > 0 && 
               GridHeight.IsFinite() && 
               GridHeight > 0;
    }
    
    public Float64PolarGrid2D GetSubGrid(int subDivisionCount, int level, LinFloat64Vector2D origin, LinFloat64Angle angle)
    {
        var scalingFactor = 
            Math.Pow(subDivisionCount, -level);

        var radialLineCount = 
            (int)(RadialLineCount * subDivisionCount.IntegerPower(level));

        return new Float64PolarGrid2D(
            origin,
            BaseVectors[0].RotateBy(angle).ScaleBy(scalingFactor),
            radialLineCount,
            GridWidth,
            GridHeight
        );
    }

    public IEnumerable<Float64LineSegment2D> GetRadialLineSegments()
    {
        return BaseVectors.Select(v => 
            Float64LineSegment2D.Create(
                Origin,
                Origin + v.SetLength(GridDiagonal)
            )
        );
    }
    
    public IEnumerable<double> GetCircleRadii()
    {
        for (var r = BaseCircleRadius; r <= GridDiagonal; r+= BaseCircleRadius)
            yield return r;
    }
}