using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Random;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Polytopes.Space2D.Float64;

/// <summary>
/// A composer to construct a 2D maze of concentric regular polygons
/// </summary>
public class PolygonMazeComposer
{
    public static Float64LineSegment2DBvh Generate(double baseOuterRadius, int sideCount, int levelCount, int maxDoorsPerLevel)
    {
        var composer = new PolygonMazeComposer(
            baseOuterRadius, 
            sideCount, 
            levelCount, 
            maxDoorsPerLevel
        );

        return composer.GenerateMaze();
    }

    public static Float64LineSegment2DBvh Generate(double baseOuterRadius, int sideCount, int levelCount, int maxDoorsPerLevel, Random randomGenerator)
    {
        var composer = new PolygonMazeComposer(
            baseOuterRadius, 
            sideCount, 
            levelCount, 
            maxDoorsPerLevel
        );

        return composer.GenerateMaze(randomGenerator);
    }


    /// <summary>
    /// Number od polygon sides
    /// </summary>
    public int SideCount { get; }

    /// <summary>
    /// Number of maze levels (= number of polygons)
    /// </summary>
    public int LevelCount { get; }

    /// <summary>
    /// Max number of openings per level
    /// </summary>
    public int MaxDoorsPerLevel { get; }

    public string MazeName 
        => $"PolygonMaze_{SideCount}x{LevelCount}";

    /// <summary>
    /// The geometric information of the maze polygons
    /// </summary>
    public IReadOnlyList<Float64RegularPolygon2D> LevelPolygons { get; }


    public PolygonMazeComposer(double baseOuterRadius, int sideCount, int levelCount, int maxDoorsPerLevel)
    {
        if (sideCount < 3)
            throw new ArgumentOutOfRangeException(nameof(sideCount));

        if (levelCount < 3)
            throw new ArgumentOutOfRangeException(nameof(levelCount));

        SideCount = sideCount;
        LevelCount = levelCount;
        MaxDoorsPerLevel = maxDoorsPerLevel;

        LevelPolygons = 
            levelCount.GetRange(lvlIdx =>
                {
                    var p = 
                        Float64RegularPolygon2D.Convex(sideCount);

                    p.Transform = Float64SimilarityAffineMap2D.Create(
                        (lvlIdx + 1) * baseOuterRadius, 
                        p.CentralAngle.HalfPolarAngle()
                    );

                    return p;
                }
            ).ToArray();
    }


    public Float64LineSegment2DBvh GenerateMaze(Random? randomGenerator = null)
    {
        randomGenerator ??= new Random();

        var mazeSegments = new List<Float64LineSegment2D>();

        var firstPolygon = LevelPolygons[0];
        var levelOffset = firstPolygon.InnerRadius;
        var doorSize = levelOffset / 1;

        // Add all sides except one for first level
        var skipSideIndex = randomGenerator.GetInt32(0, SideCount);
        for (var i = 0; i < SideCount; i++)
            if (i != skipSideIndex)
                mazeSegments.Add(
                    new Float64LineSegment2D(
                        firstPolygon.GetVertexPoint(i),
                        firstPolygon.GetVertexPoint(i + 1)
                    )
                );

        for (var lvlIdx = 1; lvlIdx < LevelCount - 1; lvlIdx++)
        {
            // The polygon of this level
            var polygon = LevelPolygons[lvlIdx];
            var maxDoorsPerSide = (int)Math.Floor(polygon.SideLength / doorSize);
                    
            var tDelta = doorSize / polygon.SideLength;
            var tMin = (1 - maxDoorsPerSide * tDelta) / 2;
            var tMax = tMin + tDelta * maxDoorsPerSide;

            var tList = 
                tMin.GetLinearRange(tMax, maxDoorsPerSide, true).ToArray();

            // List for allowed door positions for this level
            var doorPosList = new List<(int, double)>(SideCount * tList.Length);

            for (var sideIdx = 0; sideIdx < SideCount; sideIdx++)
                doorPosList.AddRange(
                    tList.Select(t => (sideIdx, t))
                );
            
            // Randomly select number of doors for this level
            var doorCount = Math.Min(doorPosList.Count - 2, MaxDoorsPerLevel);

            // Randomly select the door positions for this level
            doorPosList = 
                randomGenerator
                    .GetIndexSetOfSize(doorCount, doorPosList.Count - 1)
                    .Select(i => doorPosList[i])
                    .OrderBy(p => p.Item1)
                    .ThenBy(p => p.Item2)
                    .ToList();

            var sideIndex1 = 0;
            var t1 = 0d;
            
            foreach (var (sideIndex2, t2) in doorPosList)
            {
                if (sideIndex2 > sideIndex1)
                {
                    mazeSegments.Add(
                        new Float64LineSegment2D(
                            polygon.GetSidePoint(sideIndex1, t1),
                            polygon.GetVertexPoint(sideIndex1 + 1)
                        )
                    );

                    for (var j = sideIndex1 + 1; j < sideIndex2; j++)
                        mazeSegments.Add(
                            new Float64LineSegment2D(
                                polygon.GetVertexPoint(j),
                                polygon.GetVertexPoint(j + 1)
                            )
                        );

                    mazeSegments.Add(
                        new Float64LineSegment2D(
                            polygon.GetVertexPoint(sideIndex2),
                            polygon.GetSidePoint(sideIndex2, t2)
                        )
                    );

                    sideIndex1 = sideIndex2;
                    t1 = t2;

                    continue;
                }

                mazeSegments.Add(
                    new Float64LineSegment2D(
                        polygon.GetSidePoint(sideIndex1, t1),
                        polygon.GetSidePoint(sideIndex2, t2)
                    )
                );

                var doorP1 = polygon.GetSidePoint(sideIndex2, t2);
                var doorP2 = doorP1 + levelOffset * polygon.GetSideUnitNormal(sideIndex2);

                mazeSegments.Add(
                    new Float64LineSegment2D(doorP1, doorP2)
                );

                //doorOpened = true;
                sideIndex1 = sideIndex2;
                t1 = t2 + tDelta;
            }

            // Close the polygon level
            {
                mazeSegments.Add(
                    new Float64LineSegment2D(
                        polygon.GetSidePoint(sideIndex1, t1),
                        polygon.GetVertexPoint(sideIndex1 + 1)
                    )
                );

                for (var j = sideIndex1 + 1; j < SideCount; j++)
                    mazeSegments.Add(
                        new Float64LineSegment2D(
                            polygon.GetVertexPoint(j),
                            polygon.GetVertexPoint((j + 1).Mod(SideCount))
                        )
                    );
            }
        }
        
        // Add all sides for last level
        var lastPolygon = LevelPolygons[^1];
        for (var i = 0; i < SideCount; i++)
            mazeSegments.Add(
                new Float64LineSegment2D(
                    lastPolygon.GetVertexPoint(i),
                    lastPolygon.GetVertexPoint(i + 1)
                )
            );

        return new Float64LineSegment2DBvh(SimplifySegments(mazeSegments));

        IReadOnlyList<Float64LineSegment2D> SimplifySegments(IReadOnlyList<Float64LineSegment2D> segmentList)
        {
            var result = new List<Float64LineSegment2D>(segmentList.Count);

            var (p1, p2) = segmentList[0];

            for (var i = 1; i < segmentList.Count; i++)
            {
                var (q1, q2) = segmentList[i];

                if ((q1 - p2).IsNearZero() && (q2 - p1).IsNearParallelTo(q2 - p2))
                {
                    p2 = q2;
                    continue;
                }

                result.Add(
                    new Float64LineSegment2D(p1, p2)
                );

                p1 = q1;
                p2 = q2;
            }

            result.Add(
                new Float64LineSegment2D(p1, p2)
            );

            return result;
        }
    }


}