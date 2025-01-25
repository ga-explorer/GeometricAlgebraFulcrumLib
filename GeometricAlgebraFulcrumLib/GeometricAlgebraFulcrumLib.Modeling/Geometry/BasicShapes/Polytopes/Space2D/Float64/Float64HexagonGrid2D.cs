using System.Collections;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Polytopes.Space2D.Float64;

public sealed record Float64HexagonGrid2D :
    Float64Grid2D,
    IEnumerable<LinFloat64Vector2D>
{
    public static Float64HexagonGrid2D Create(double cellRadius, double gridWidth, double gridHeight)
    {
        return new Float64HexagonGrid2D(
            LinFloat64Vector2D.Zero,
            LinFloat64Vector2D.Create(0, cellRadius),
            gridWidth,
            gridHeight
        );
    }

    public static Float64HexagonGrid2D Create(LinFloat64Vector2D baseVector, double gridWidth, double gridHeight)
    {
        return new Float64HexagonGrid2D(
            LinFloat64Vector2D.Zero,
            baseVector,
            gridWidth,
            gridHeight
        );
    }

    public static Float64HexagonGrid2D Create(LinFloat64Vector2D origin, LinFloat64Vector2D baseVector, double gridWidth, double gridHeight)
    {
        return new Float64HexagonGrid2D(
            origin, baseVector, gridWidth, gridHeight
        );
    }
    
    
    public LinFloat64Vector2D Origin { get; }

    public Triplet<LinFloat64Vector2D> BaseVectors { get; }

    public Float64RegularPolygon2D HexagonSpecs { get; }

    public double CellRadius
        => HexagonSpecs.OuterRadius;


    private Float64HexagonGrid2D(LinFloat64Vector2D origin, LinFloat64Vector2D baseVector, double gridWidth, double gridHeight)
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

        HexagonSpecs = Float64RegularPolygon2D.CreateFromOuterRadius(
            6, 
            baseVector.Norm()
        );
        
        Debug.Assert(IsValid());
    }

    
    public override bool IsValid()
    {
        return GridWidth.IsFinite() && 
               GridWidth > 0 && 
               GridHeight.IsFinite() && 
               GridHeight > 0;
    }

    public Float64HexagonGrid2D GetSubGrid(int subDivisionCount, int level, LinFloat64Vector2D origin, LinFloat64Angle angle)
    {
        var scalingFactor = 
            Math.Pow(subDivisionCount, -level);

        return new Float64HexagonGrid2D(
            origin,
            BaseVectors.Item1.RotateBy(angle).ScaleBy(scalingFactor),
            GridWidth,
            GridHeight
        );
    }

    public Float64HexagonGrid2D ScaleBaseVectors(double scalingFactor)
    {
        return new Float64HexagonGrid2D(
            Origin,
            BaseVectors.Item1.ScaleBy(scalingFactor),
            GridWidth,
            GridHeight
        );
    }

    public Float64HexagonGrid2D RotateBaseVectors(LinFloat64Angle angle)
    {
        return new Float64HexagonGrid2D(
            Origin,
            BaseVectors.Item1.RotateBy(angle),
            GridWidth,
            GridHeight
        );
    }

    public Float64HexagonGrid2D RotateScaleBaseVectors(LinFloat64Angle angle, double scalingFactor)
    {
        return new Float64HexagonGrid2D(
            Origin,
            BaseVectors.Item1.RotateBy(angle).ScaleBy(scalingFactor),
            GridWidth,
            GridHeight
        );
    }

    public Float64HexagonGrid2D SetBaseVectorAngle(LinFloat64Angle angle)
    {
        return new Float64HexagonGrid2D(
            Origin,
            LinFloat64Vector2D.Create(CellRadius, 0).RotateBy(angle),
            GridWidth,
            GridHeight
        );
    }

    public Float64HexagonGrid2D SetBaseVectorsLength(double length)
    {
        return new Float64HexagonGrid2D(
            Origin,
            BaseVectors.Item1.SetLength(length),
            GridWidth,
            GridHeight
        );
    }

    public Float64HexagonGrid2D SetOrigin(LinFloat64Vector2D origin)
    {
        return new Float64HexagonGrid2D(
            origin,
            BaseVectors.Item1,
            GridWidth,
            GridHeight
        );
    }


    private IEnumerable<LinFloat64Vector2D> GetOriginTranslations1(LinFloat64Vector2D origin)
    {
        var v =
            BaseVectors.Item1;

        var p = origin;
        while (ContainsPoint(p))
        {
            p += 3 * v;

            yield return p;
        }

        p = origin;
        while (ContainsPoint(p))
        {
            p -= 3 * v;

            yield return p;
        }
    }

    private IEnumerable<LinFloat64Vector2D> GetOriginTranslations2(LinFloat64Vector2D origin)
    {
        var v =
            BaseVectors.Item1
                .RotateBy(LinFloat64PolarAngle.Angle90)
                .SetLength(2 * HexagonSpecs.InnerRadius);

        var p = origin;
        while (ContainsPoint(p))
        {
            p += v;

            yield return p;
        }

        p = origin;
        while (ContainsPoint(p))
        {
            p -= v;

            yield return p;
        }
    }

    public IEnumerable<LinFloat64Vector2D> GetOrigins(LinFloat64Vector2D origin)
    {
        {
            var ot1 = origin;

            yield return ot1;

            var originList2 =
                GetOriginTranslations2(ot1);

            foreach (var ot2 in originList2)
                yield return ot2;
        }

        var originList1 =
            GetOriginTranslations1(origin);

        foreach (var ot1 in originList1)
        {
            yield return ot1;

            var originList2 =
                GetOriginTranslations2(ot1);

            foreach (var ot2 in originList2)
                yield return ot2;
        }
    }

    public IEnumerator<LinFloat64Vector2D> GetEnumerator()
    {
        return GetOrigins(Origin)
            .Concat(
                GetOrigins(
                    Origin + (BaseVectors.Item2 - BaseVectors.Item1)
                )
            ).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}