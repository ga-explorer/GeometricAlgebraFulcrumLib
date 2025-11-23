using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Rays;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Bounding.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.SpacePartitions;
using MIConvexHull;

// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract

namespace GeometricAlgebraFulcrumLib.Modeling.Research.UAVs;

/// <summary>
/// Main builder class
/// </summary>
public static class ObstacleAvoidingVoronoiComposer
{
    /// <summary>
    /// Vertex type for MIConvexHull
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="id"></param>
    public sealed class Vertex(double x, double y, int id = -1) : IVertex
    {
        public double[] Position { get; private set; } = [x, y];

        /// <summary>
        /// optional id to map back
        /// </summary>
        public int Id { get; } = id;
    }

    /// <summary>
    /// Triangulation cell type required by MIConvexHull
    /// </summary>
    public sealed class DelaunayCell :
        TriangulationCell<Vertex, DelaunayCell>
    {

    }


    ///// <summary>
    ///// ---------------- Pathfinding (A*) ----------------
    ///// </summary>
    //public static class AStar
    //{
    //    private class NodeRecord
    //    {
    //        public Graph.NodeData Node;
    //        public NodeRecord Parent;
    //        public double G;
    //        public double F;
    //    }

    //    /// <summary>
    //    /// ---------------- Graph structures ----------------
    //    /// </summary>
    //    public class Graph
    //    {
    //        public class NodeData
    //        {
    //            public int Id;
    //            public required LinFloat64Vector2D Position;
    //            public readonly List<(NodeData target, double weight)> Neighbors = [];
    //        }


    //        public readonly List<NodeData> Nodes = [];

    //        private readonly double _mergeTolSq;

    //        public Graph(double mergeTolerance = 1e-6)
    //        {
    //            _mergeTolSq = mergeTolerance * mergeTolerance;
    //        }

    //        // Insert or reuse a node at given position
    //        private NodeData GetOrAddNode(LinFloat64Vector2D pos)
    //        {
    //            foreach (var n in Nodes)
    //            {
    //                if ((n.Position - pos).L2Norm() <= _mergeTolSq) return n;
    //            }
    //            var node = new NodeData { Id = Nodes.Count, Position = pos };
    //            Nodes.Add(node);
    //            return node;
    //        }

    //        public void AddEdge(LinFloat64Vector2D a, LinFloat64Vector2D b)
    //        {
    //            var na = GetOrAddNode(a);
    //            var nb = GetOrAddNode(b);
    //            if (na == nb) return;

    //            var w = Math.Sqrt((na.Position - nb.Position).L2Norm());
    //            na.Neighbors.Add((nb, w));
    //            nb.Neighbors.Add((na, w));
    //        }

    //        public static Graph BuildFromEdges(IEnumerable<Float64LineSegment2D> edges)
    //        {
    //            var g = new Graph();
    //            foreach (var e in edges)
    //                g.AddEdge(e.Item1, e.Item2);
    //            return g;
    //        }
    //    }

    //    /// <summary>
    //    /// ---------------- Spatial Index (grid bucket) ----------------
    //    /// </summary>
    //    public class SpatialIndex
    //    {
    //        private readonly double _cellSize;
    //        private readonly Dictionary<(int, int), List<Graph.NodeData>> _grid = new();

    //        public SpatialIndex(IEnumerable<Graph.NodeData> nodes, double cellSize = 1.0)
    //        {
    //            _cellSize = cellSize;
    //            foreach (var n in nodes)
    //            {
    //                var key = Key(n.Position);
    //                if (!_grid.TryGetValue(key, out var list))
    //                    _grid[key] = list = [];
    //                list.Add(n);
    //            }
    //        }

    //        private (int, int) Key(LinFloat64Vector2D p)
    //        {
    //            return ((int)Math.Floor(p.X / _cellSize), (int)Math.Floor(p.Y / _cellSize));
    //        }

    //        public Graph.NodeData Nearest(LinFloat64Vector2D query)
    //        {
    //            var qKey = Key(query);
    //            var bestD2 = double.PositiveInfinity;
    //            Graph.NodeData best = null!;

    //            // Search neighbor cells too
    //            for (var dx = -1; dx <= 1; dx++)
    //            for (var dy = -1; dy <= 1; dy++)
    //            {
    //                var k = (qKey.Item1 + dx, qKey.Item2 + dy);

    //                if (!_grid.TryGetValue(k, out var list)) continue;

    //                foreach (var n in list)
    //                {
    //                    var d2 = (n.Position - query).L2Norm();

    //                    if (d2 >= bestD2) continue;

    //                    bestD2 = d2;
    //                    best = n;
    //                }
    //            }

    //            return best!;
    //        }
    //    }


    //    public static List<LinFloat64Vector2D> FindPath(UndirectedAdjacencyGraph<LinFloat64Vector2D> graph, SpatialIndex index, LinFloat64Vector2D startPos, LinFloat64Vector2D goalPos)
    //    {
    //        var startNode = new Graph.NodeData { Id = -1, Position = startPos };
    //        var goalNode = new Graph.NodeData { Id = -2, Position = goalPos };

    //        // Connect start and goal to nearest roadmap nodes
    //        var nStart = index.Nearest(startPos);
    //        var nGoal = index.Nearest(goalPos);

    //        if (nStart == null || nGoal == null) 
    //            return null!;

    //        Connect(startNode, nStart);
    //        Connect(goalNode, nGoal);

    //        // A*
    //        var open = new PriorityQueue<NodeRecord, double>();
    //        var records = new Dictionary<Graph.NodeData, NodeRecord>();

    //        var startRec = new NodeRecord { Node = startNode, G = 0, F = Heuristic(startPos, goalPos) };
    //        open.Enqueue(startRec, startRec.F);
    //        records[startNode] = startRec;

    //        while (open.Count > 0)
    //        {
    //            var current = open.Dequeue();

    //            if (current.Node == goalNode)
    //                return Reconstruct(current);

    //            foreach (var (nbr, w) in current.Node.Neighbors)
    //            {
    //                var gNew = current.G + w;

    //                if (records.TryGetValue(nbr, out var nbrRec) && gNew >= nbrRec.G) 
    //                    continue;

    //                nbrRec = new NodeRecord
    //                {
    //                    Node = nbr,
    //                    Parent = current,
    //                    G = gNew,
    //                    F = gNew + Heuristic(nbr.Position, goalPos)
    //                };

    //                records[nbr] = nbrRec;
    //                open.Enqueue(nbrRec, nbrRec.F);
    //            }
    //        }

    //        return null!; // no path
    //    }



    //    public static List<LinFloat64Vector2D> FindPath(Graph graph, SpatialIndex index, LinFloat64Vector2D startPos, LinFloat64Vector2D goalPos)
    //    {
    //        var startNode = new Graph.NodeData { Id = -1, Position = startPos };
    //        var goalNode = new Graph.NodeData { Id = -2, Position = goalPos };

    //        // Connect start and goal to nearest roadmap nodes
    //        var nStart = index.Nearest(startPos);
    //        var nGoal = index.Nearest(goalPos);

    //        if (nStart == null || nGoal == null) 
    //            return null!;

    //        Connect(startNode, nStart);
    //        Connect(goalNode, nGoal);

    //        // A*
    //        var open = new PriorityQueue<NodeRecord, double>();
    //        var records = new Dictionary<Graph.NodeData, NodeRecord>();

    //        var startRec = new NodeRecord { Node = startNode, G = 0, F = Heuristic(startPos, goalPos) };
    //        open.Enqueue(startRec, startRec.F);
    //        records[startNode] = startRec;

    //        while (open.Count > 0)
    //        {
    //            var current = open.Dequeue();

    //            if (current.Node == goalNode)
    //                return Reconstruct(current);

    //            foreach (var (nbr, w) in current.Node.Neighbors)
    //            {
    //                var gNew = current.G + w;

    //                if (records.TryGetValue(nbr, out var nbrRec) && gNew >= nbrRec.G) 
    //                    continue;

    //                nbrRec = new NodeRecord
    //                {
    //                    Node = nbr,
    //                    Parent = current,
    //                    G = gNew,
    //                    F = gNew + Heuristic(nbr.Position, goalPos)
    //                };

    //                records[nbr] = nbrRec;
    //                open.Enqueue(nbrRec, nbrRec.F);
    //            }
    //        }

    //        return null!; // no path
    //    }

    //    private static void Connect(Graph.NodeData a, Graph.NodeData b)
    //    {
    //        var w = Math.Sqrt((a.Position - b.Position).L2Norm());
    //        a.Neighbors.Add((b, w));
    //        b.Neighbors.Add((a, w));
    //    }

    //    private static List<LinFloat64Vector2D> Reconstruct(NodeRecord rec)
    //    {
    //        var path = new List<LinFloat64Vector2D>();
    //        while (rec != null)
    //        {
    //            path.Add(rec.Node.Position);
    //            rec = rec.Parent;
    //        }
    //        path.Reverse();

    //        return path;
    //    }

    //    private static double Heuristic(LinFloat64Vector2D a, LinFloat64Vector2D b) =>
    //        Math.Sqrt((a - b).L2Norm());
    //}

    //public static void Example()
    //{
    //    // Example sites
    //    var rand = new Random(12345);
    //    var sites = new List<LinFloat64Vector2D>();
    //    for (var i = 0; i < 500; i++)
    //    {
    //        sites.Add(LinFloat64Vector2D.Create(rand.NextDouble() * 100.0, rand.NextDouble() * 100.0));
    //    }

    //    // Example obstacles: random segments
    //    var obs = new List<Float64LineSegment2D>();
    //    for (var i = 0; i < 200; i++)
    //    {
    //        var a = LinFloat64Vector2D.Create(rand.NextDouble() * 100.0, rand.NextDouble() * 100.0);
    //        var b = LinFloat64Vector2D.Create(rand.NextDouble() * 100.0, rand.NextDouble() * 100.0);
    //        obs.Add(new Float64LineSegment2D(a, b));
    //    }

    //    // Bounding box
    //    var bbox = new Float64BoundingBox2D(-10.0, -10.0, 110.0, 110.0);

    //    // Build obstacle-avoiding Voronoi roadmap
    //    var freeEdges = BuildVoronoiAvoidingObstacles(sites, obs, bbox);

    //    Console.WriteLine($"Built {freeEdges.Count} free-space Voronoi edges.");
    //    // Here you could export as lines, draw, or convert to graph nodes/edges for roadmap planning
    //}


    /// <summary>
    /// Epsilon for geometric tests
    /// </summary>
    private const double GeometricEpsilon = 1e-9;
    // Public API:
    // sites: list of points (voronoi sites)
    // obstacles: BVH of obstacle line segments
    // boundingBox: bounding box to clip unbounded edges
    // returns: list of free-space segments (each segment strictly not touching obstacles)
    public static List<Float64LineSegment2D> BuildVoronoiAvoidingObstacles(
        IReadOnlyCollection<LinFloat64Vector2D> sites,
        Float64LineSegment2DBvh obstacles,
        Float64BoundingBox2D boundingBox,
        double tolerance = Float64Utils.DefaultTolerance
    )
    {
        if (sites.Count < 2) return [];

        // Convert sites to MIConvexHull vertices
        var vertices = sites.Select((s, i) => new Vertex(s.X, s.Y, i)).ToArray();

        // Delaunay triangulation
        var delaunay = DelaunayTriangulation<Vertex, DelaunayCell>.Create(vertices, 1e-8);

        // Extract triangle cells
        var cells = delaunay.Cells.ToList();
        var cellCount = cells.Count;

        var circumcenters = new LinFloat64Vector2D[cellCount];

        // Map vertices to indices
        var vertexToIndex = new Dictionary<Vertex, int>(vertices.Length);
        for (var i = 0; i < vertices.Length; i++)
            vertexToIndex[vertices[i]] = i;

        // Cell -> site indices
        var cellVertexIndices = new int[cellCount][];
        for (var ci = 0; ci < cellCount; ci++)
        {
            var cell = cells[ci];
            var v = cell.Vertices;
            if (v.Length != 3)
            {
                cellVertexIndices[ci] = null;
                circumcenters[ci] = LinFloat64Vector2D.Create(double.NaN, double.NaN);
                continue;
            }

            var ids = new int[3];
            for (var k = 0; k < 3; k++)
                ids[k] = vertexToIndex[v[k]];
            cellVertexIndices[ci] = ids;

            // Compute circumcenter of triangle
            var p0 = LinFloat64Vector2D.Create(v[0].Position[0], v[0].Position[1]);
            var p1 = LinFloat64Vector2D.Create(v[1].Position[0], v[1].Position[1]);
            var p2 = LinFloat64Vector2D.Create(v[2].Position[0], v[2].Position[1]);
            circumcenters[ci] = p0.GetCircumcenter(p1, p2);
        }

        // Build adjacency map (edge -> cell)
        var edgeMap = new Dictionary<(int a, int b), int>();
        var voronoiCandidates = new List<Float64LineSegment2D>();

        for (var ci = 0; ci < cellCount; ci++)
        {
            var ids = cellVertexIndices[ci];
            if (ids == null) continue;

            for (var e = 0; e < 3; e++)
            {
                var i0 = ids[e];
                var i1 = ids[(e + 1) % 3];
                var key = (a: Math.Min(i0, i1), b: Math.Max(i0, i1));

                if (edgeMap.TryGetValue(key, out var otherCellIndex))
                {
                    // Shared edge → Voronoi edge between circumcenters
                    var c1 = circumcenters[ci];
                    var c2 = circumcenters[otherCellIndex];
                    if (!c1.IsFinite() || !c2.IsFinite()) continue;
                    if (!PointsCoincide(c1, c2))
                        voronoiCandidates.Add(new Float64LineSegment2D(c1, c2));
                }
                else
                {
                    edgeMap[key] = ci;
                }
            }
        }

        // Build count map for boundary edges
        var edgeCount = new Dictionary<(int a, int b), int>();
        foreach (var arr in cellVertexIndices)
        {
            if (arr == null) continue;
            for (var e = 0; e < 3; e++)
            {
                var i0 = arr[e];
                var i1 = arr[(e + 1) % 3];
                var key = (a: Math.Min(i0, i1), b: Math.Max(i0, i1));
                edgeCount.TryGetValue(key, out var c);
                edgeCount[key] = c + 1;
            }
        }

        // Handle boundary edges (infinite Voronoi rays)
        var worldBox = Float64BoundingBox2D.Containing(
            boundingBox.MinX, boundingBox.MinY,
            boundingBox.MaxX, boundingBox.MaxY
        );

        foreach (var (key, ci) in edgeMap)
        {
            if (!edgeCount.TryGetValue(key, out var cnt) || cnt != 1)
                continue; // not boundary

            var ids = cellVertexIndices[ci];
            if (ids == null) continue;

            var s0 = key.a;
            var s1 = key.b;

            var p0 = LinFloat64Vector2D.Create(vertices[s0].Position[0], vertices[s0].Position[1]);
            var p1 = LinFloat64Vector2D.Create(vertices[s1].Position[0], vertices[s1].Position[1]);
            var mid = (p0 + p1) * 0.5;
            var de = p1 - p0;
            var dir = LinFloat64Vector2D.Create(-de.Y, de.X);
            var cc = circumcenters[ci];
            if (!cc.IsFinite()) continue;

            if (Math.Abs(dir.X) < 1e-12 && Math.Abs(dir.Y) < 1e-12)
                continue;

            if (worldBox.TryIntersectRay(new Float64Ray2D(cc, dir), out var tMin, out var tMax))
            {
                if (tMax < 0) continue;
                var tStart = Math.Max(0.0, tMin);
                var pA = cc;
                var pB = cc + dir * tMax;

                if (!PointsCoincide(pA, pB))
                    voronoiCandidates.Add(new Float64LineSegment2D(pA, pB));
            }
        }

        // --- Replace BIH logic with BVH proximity checks ---
        var freePieces = new List<Float64LineSegment2D>();

        foreach (var seg in voronoiCandidates)
        {
            // Quick test: if segment has no intersection with any obstacle (within tolerance)
            if (obstacles.GetShortestDistanceToSegment(seg) > tolerance)
            {
                freePieces.Add(seg);
                continue;
            }

            // Otherwise, split by intersections (optional refinement)
            // We can test subsegments by midpoint sampling
            const int steps = 10;
            var p1 = seg.Item1;
            var p2 = seg.Item2;
            var dir = p2 - p1;

            var prevT = 0.0;
            var lastFree = false;
            var prevP = p1;

            for (var i = 1; i <= steps; i++)
            {
                var t = i / (double)steps;
                var pm = p1 + dir * t;
                var free = obstacles.GetShortestDistanceToPoint(pm) > tolerance;

                if (free && !lastFree)
                    prevP = pm;
                else if (!free && lastFree)
                {
                    var segFree = new Float64LineSegment2D(prevP, pm);
                    if (segFree.GetLength() > tolerance)
                        freePieces.Add(segFree);
                }

                lastFree = free;
                prevT = t;
            }

            // If last piece still free, keep it
            if (lastFree)
            {
                var lastSeg = new Float64LineSegment2D(prevP, p2);
                if (lastSeg.GetLength() > tolerance)
                    freePieces.Add(lastSeg);
            }
        }

        return freePieces;
    }

    /// <summary>
    /// Builds a Voronoi roadmap that avoids obstacles and maintains a minimum clearance distance.
    /// </summary>
    /// <param name="sites">Input Voronoi sites.</param>
    /// <param name="obstacles">BVH of obstacle line segments.</param>
    /// <param name="boundingBox">World bounding box for clipping unbounded edges.</param>
    /// <param name="tolerance">Numerical tolerance for intersection tests.</param>
    /// <param name="clearanceDistance">Minimum distance Voronoi edges must keep from obstacles.</param>
    /// <returns>List of free Voronoi edges (segments) that satisfy clearance constraints.</returns>
    public static List<Float64LineSegment2D> BuildVoronoiAvoidingObstacles(
        IReadOnlyCollection<LinFloat64Vector2D> sites,
        Float64LineSegment2DBvh obstacles,
        Float64BoundingBox2D boundingBox,
        double clearanceDistance,
        double tolerance = Float64Utils.DefaultTolerance
    )
    {
        if (sites.Count < 2) return [];

        // --- Build Delaunay triangulation ---
        var vertices = sites.Select((s, i) => new Vertex(s.X, s.Y, i)).ToArray();
        var delaunay = DelaunayTriangulation<Vertex, DelaunayCell>.Create(vertices, 1e-8);

        var cells = delaunay.Cells.ToList();
        var cellCount = cells.Count;
        var circumcenters = new LinFloat64Vector2D[cellCount];

        var vertexToIndex = new Dictionary<Vertex, int>(vertices.Length);
        for (var i = 0; i < vertices.Length; i++)
            vertexToIndex[vertices[i]] = i;

        var cellVertexIndices = new int[cellCount][];
        for (var ci = 0; ci < cellCount; ci++)
        {
            var cell = cells[ci];
            var v = cell.Vertices;

            if (v.Length != 3)
            {
                cellVertexIndices[ci] = null;
                circumcenters[ci] = LinFloat64Vector2D.Create(double.NaN, double.NaN);
                continue;
            }

            var ids = new int[3];
            for (var k = 0; k < 3; k++)
                ids[k] = vertexToIndex[v[k]];
            cellVertexIndices[ci] = ids;

            // Compute circumcenter
            var p0 = LinFloat64Vector2D.Create(v[0].Position[0], v[0].Position[1]);
            var p1 = LinFloat64Vector2D.Create(v[1].Position[0], v[1].Position[1]);
            var p2 = LinFloat64Vector2D.Create(v[2].Position[0], v[2].Position[1]);
            circumcenters[ci] = p0.GetCircumcenter(p1, p2);
        }

        // --- Generate Voronoi edges ---
        var edgeMap = new Dictionary<(int a, int b), int>();
        var voronoiCandidates = new List<Float64LineSegment2D>();

        for (var ci = 0; ci < cellCount; ci++)
        {
            var ids = cellVertexIndices[ci];
            if (ids == null) continue;

            for (var e = 0; e < 3; e++)
            {
                var i0 = ids[e];
                var i1 = ids[(e + 1) % 3];
                var key = (Math.Min(i0, i1), Math.Max(i0, i1));

                if (edgeMap.TryGetValue(key, out var otherCellIndex))
                {
                    var c1 = circumcenters[ci];
                    var c2 = circumcenters[otherCellIndex];
                    if (!c1.IsFinite() || !c2.IsFinite()) continue;
                    if (!PointsCoincide(c1, c2))
                        voronoiCandidates.Add(new Float64LineSegment2D(c1, c2));
                }
                else
                    edgeMap[key] = ci;
            }
        }

        // --- Add boundary rays (clipped to bounding box) ---
        var edgeCount = new Dictionary<(int a, int b), int>();
        foreach (var arr in cellVertexIndices)
        {
            if (arr == null) continue;
            for (var e = 0; e < 3; e++)
            {
                var i0 = arr[e];
                var i1 = arr[(e + 1) % 3];
                var key = (Math.Min(i0, i1), Math.Max(i0, i1));
                edgeCount.TryGetValue(key, out var c);
                edgeCount[key] = c + 1;
            }
        }

        var worldBox = Float64BoundingBox2D.Containing(
            boundingBox.MinX, boundingBox.MinY,
            boundingBox.MaxX, boundingBox.MaxY
        );

        foreach (var (key, ci) in edgeMap)
        {
            if (!edgeCount.TryGetValue(key, out var cnt) || cnt != 1)
                continue;

            var ids = cellVertexIndices[ci];
            if (ids == null) continue;

            int s0 = key.a, s1 = key.b;
            var p0 = LinFloat64Vector2D.Create(vertices[s0].Position[0], vertices[s0].Position[1]);
            var p1 = LinFloat64Vector2D.Create(vertices[s1].Position[0], vertices[s1].Position[1]);
            var de = p1 - p0;
            var dir = LinFloat64Vector2D.Create(-de.Y, de.X);
            var cc = circumcenters[ci];
            if (!cc.IsFinite()) continue;

            if (worldBox.TryIntersectRay(new Float64Ray2D(cc, dir), out var tMin, out var tMax))
            {
                if (tMax < 0) continue;
                var pA = cc;
                var pB = cc + dir * tMax;
                if (!PointsCoincide(pA, pB))
                    voronoiCandidates.Add(new Float64LineSegment2D(pA, pB));
            }
        }

        // --- Filter Voronoi edges by obstacle clearance ---
        var freePieces = new List<Float64LineSegment2D>();

        foreach (var seg in voronoiCandidates)
        {
            // Step 1: quick distance check to see if entire segment is safely away
            if (obstacles.GetShortestDistanceToSegment(seg) >= clearanceDistance + tolerance)
            {
                freePieces.Add(seg);
                continue;
            }

            // Step 2: sample along segment to find safe subsegments
            const int samples = 16; // number of sample points along each segment
            var p1 = seg.Item1;
            var p2 = seg.Item2;
            var dir = p2 - p1;

            LinFloat64Vector2D? start = null;
            for (var i = 0; i <= samples; i++)
            {
                var t = i / (double)samples;
                var pt = p1 + dir * t;

                // Check local clearance
                var d = obstacles.GetShortestDistanceToPoint(pt);
                var safe = d >= clearanceDistance + tolerance;

                if (safe && start == null)
                    start = pt;
                else if ((!safe || i == samples) && start != null)
                {
                    var end = safe ? pt : p1 + dir * ((i - 1.0) / samples);
                    if (!PointsCoincide(start.Value, end))
                        freePieces.Add(new Float64LineSegment2D(start.Value, end));
                    start = null;
                }
            }
        }

        return freePieces;
    }

    /// <summary>
    /// Utility: test if two points coincide within numerical tolerance.
    /// </summary>
    private static bool PointsCoincide(LinFloat64Vector2D p1, LinFloat64Vector2D p2, double eps = 1e-12)
    {
        return p1.GetDistanceSquaredToPoint(p2) < eps * eps;
    }

    // ---------- Numeric helpers ----------

    private static bool PointsCoincide(LinFloat64Vector2D a, LinFloat64Vector2D b)
        => LinFloat64Vector2DUtils.L2Norm((a - b)) < 1e-18;

    ///// <summary>
    ///// Test whether a segment intersects any obstacle segment (coarse via BIH)
    ///// </summary>
    ///// <param name="seg"></param>
    ///// <param name="bih"></param>
    ///// <param name="obstacles"></param>
    ///// <param name="tolerance"></param>
    ///// <returns></returns>
    //private static bool SegmentIntersectsAnyObstacle(Float64LineSegment2D seg, Float64LineSegmentBih2D bih, Float64LineSegment2DBvh obstacles, double tolerance = Float64Utils.DefaultTolerance)
    //{
    //    var candidates = new List<int>();
    //    bih.QueryCandidates(seg, candidates);
    //    foreach (var oi in candidates)
    //    {
    //        var (intFlag, _, _) =
    //            seg.GetSegmentIntersectionParameters(obstacles[oi], tolerance);

    //        if (intFlag) return true;
    //    }

    //    return false;
    //}

    /// <summary>
    /// Test whether a segment intersects any obstacle segment (coarse via BIH)
    /// </summary>
    /// <param name="seg"></param>
    /// <param name="bih"></param>
    /// <param name="obstacles"></param>
    /// <param name="tolerance"></param>
    /// <returns></returns>
    private static bool SegmentIntersectsAnyObstacle(Float64LineSegment2D seg, Float64Bih2D bih, Float64LineSegment2DBvh obstacles, double tolerance = Float64Utils.DefaultTolerance)
    {
        var candidates = new List<int>();
        bih.QueryCandidates(seg.GetBoundingBox(), candidates);
        foreach (var oi in candidates)
        {
            var (intFlag, _, _) =
                seg.GetSegmentIntersectionParameters(obstacles[oi], tolerance);

            if (intFlag) return true;
        }

        return false;
    }

    /// <summary>
    /// Test whether a point lies on any obstacle segment (within tolerance)
    /// </summary>
    /// <param name="p"></param>
    /// <param name="bih"></param>
    /// <param name="obstacles"></param>
    /// <returns></returns>
    private static bool PointOnAnyObstacle(LinFloat64Vector2D p, Float64Bih2D bih, Float64LineSegment2DBvh obstacles)
    {
        // create tiny box around point to fetch candidates
        const double eps = 1e-9;

        var tinySeg = new Float64LineSegment2D(
            LinFloat64Vector2D.Create(p.X - eps, p.Y - eps),
            LinFloat64Vector2D.Create(p.X + eps, p.Y + eps)
        );

        var candidates = new List<int>();
        bih.QueryCandidates(tinySeg.GetBoundingBox(), candidates);

        return candidates.Any(oi => obstacles[oi].SegmentContainsPoint(p));
    }

}
