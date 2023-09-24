using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Primitives.Triangles;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.LatticeShapes.Surfaces
{
    /// <summary>
    /// This class describes the shape of a 3D surface using a list of lattice
    /// surfaces <see cref="GrLatticeSurface3D"/> and <see cref="GrLatticeSurfaceLocalFrame3D"/>
    /// </summary>
    public sealed class GrLatticeSurfaceList3D :
        IReadOnlyList<GrLatticeSurfaceLocalFrame3D>
    {
        private readonly List<GrLatticeSurface3D> _surfaceList
            = new List<GrLatticeSurface3D>();


        public int Count 
            => IsReady
                ? _surfaceList.Select(b => b.Count).Sum()
                : VertexList.Count;
        
        public bool VertexNormalsEnabled { get; set; }

        public bool VertexTextureUVsEnabled { get; set; }

        public bool VertexColorsEnabled { get; set; }

        public IReadOnlyList<GrLatticeSurfaceLocalFrame3D> VertexList { get; private set; } 
            = Array.Empty<GrLatticeSurfaceLocalFrame3D>();
        
        public IEnumerable<GrLatticeSurfaceLocalFrame3D> Vertices 
            => IsReady
                ? VertexList
                : _surfaceList.SelectMany(b => b.Vertices);
        
        public IEnumerable<IFloat64Vector3D> VertexPoints 
            => Vertices.Select(v => v.Point);

        public IEnumerable<IFloat64Vector3D> VertexNormals 
            => Vertices.Select(v => v.Normal);

        public IEnumerable<Pair<double>> VertexTextureUVs 
            => Vertices.Select(v => v.ParameterValue);

        public IEnumerable<Color> VertexColors 
            => Vertices.Select(v => v.Color);

        public IReadOnlyList<Triplet<GrLatticeSurfaceLocalFrame3D>> TriangleVerticesList { get; private set; } 
            = Array.Empty<Triplet<GrLatticeSurfaceLocalFrame3D>>();

        public IEnumerable<Triplet<IFloat64Vector3D>> TriangleVertexPoints
            => TriangleVerticesList.Select(t => 
                new Triplet<IFloat64Vector3D>(
                    t.Item1.Point,
                    t.Item2.Point,
                    t.Item3.Point
                )
            );

        public IEnumerable<Triplet<int>> TriangleVertexIndices 
            => TriangleVerticesList.Select(t => 
                new Triplet<int>(
                    t.Item1.Index,
                    t.Item2.Index,
                    t.Item3.Index
                )
            );

        public bool IsReady 
            => VertexList.Count > 0;

        public GrLatticeSurfaceLocalFrame3D this[int index] 
            => VertexList[index];

        public int SurfaceCount 
            => _surfaceList.Count;

        public IEnumerable<GrLatticeSurface3D> Surfaces 
            => _surfaceList;

        public GrVertexNormalComputationMethod NormalComputationMethod { get; set; }
            = GrVertexNormalComputationMethod.WeightedNormals;

        public bool ReverseNormals { get; set; } = false;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrLatticeSurfaceList3D Clear()
        {
            foreach (var batch in _surfaceList)
                batch.Clear();

            _surfaceList.Clear();
            VertexList = Array.Empty<GrLatticeSurfaceLocalFrame3D>();
            TriangleVerticesList = Array.Empty<Triplet<GrLatticeSurfaceLocalFrame3D>>();

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrLatticeSurface3D GetSurface(int batchIndex)
        {
            return _surfaceList[batchIndex];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrLatticeSurface3D AddSurface(int countU, int countV, bool closeU, bool closeV)
        {
            var batch = new GrLatticeSurface3D(
                this, 
                _surfaceList.Count,
                countU, 
                countV, 
                closeU, 
                closeV
            );

            _surfaceList.Add(batch);

            return batch;
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrLatticeSurfaceList3D MapPoints(Func<Float64Vector3D, Float64Vector3D> mappingFunc)
        {
            foreach (var batch in _surfaceList)
                batch.MapPoints(mappingFunc);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrLatticeSurfaceList3D TranslatePointsBy(double dx, double dy, double dz)
        {
            return MapPoints(p => 
                p.TranslateBy(dx, dy, dz)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrLatticeSurfaceList3D ScalePointsBy(double s)
        {
            return MapPoints(p => 
                p.ScaleBy(s)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrLatticeSurfaceList3D ScalePointsBy(double sx, double sy, double sz)
        {
            return MapPoints(p => 
                p.ScaleBy(sx, sy, sz)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrLatticeSurfaceList3D XRotatePointsBy(double angle)
        {
            return MapPoints(p => 
                p.XRotateBy(angle)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrLatticeSurfaceList3D YRotatePointsBy(double angle)
        {
            return MapPoints(p => 
                p.YRotateBy(angle)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrLatticeSurfaceList3D ZRotatePointsBy(double angle)
        {
            return MapPoints(p => 
                p.ZRotateBy(angle)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrLatticeSurfaceList3D XRotatePointsByDegrees(double angle)
        {
            return MapPoints(p => 
                p.XRotateByDegrees(angle)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrLatticeSurfaceList3D YRotatePointsByDegrees(double angle)
        {
            return MapPoints(p => 
                p.YRotateByDegrees(angle)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrLatticeSurfaceList3D ZRotatePointsByDegrees(double angle)
        {
            return MapPoints(p => 
                p.ZRotateByDegrees(angle)
            );
        }


        private void ComputeAverageNormals()
        {
            foreach (var (vertex1, vertex2, vertex3) in TriangleVerticesList)
            {
                //Find triangle normal, not unit but full normal vector
                var normal = vertex1.ParentSurface.ReverseNormals
                    ? Float64Vector3DAffineUtils.GetTriangleNormal(vertex3, vertex2, vertex1)
                    : Float64Vector3DAffineUtils.GetTriangleNormal(vertex1, vertex2, vertex3);

                //For debugging only
                Debug.Assert(normal.IsValid());

                vertex1.Normal.Update(normal);
                vertex2.Normal.Update(normal);
                vertex3.Normal.Update(normal);
            }
        }

        private void ComputeAverageUnitNormals()
        {
            foreach (var (vertex1, vertex2, vertex3) in TriangleVerticesList)
            {
                //Find triangle unit normal
                var normal = vertex1.ParentSurface.ReverseNormals
                    ? Float64Vector3DAffineUtils.GetTriangleUnitNormal(vertex3, vertex2, vertex1)
                    : Float64Vector3DAffineUtils.GetTriangleUnitNormal(vertex1, vertex2, vertex3);
                
                //For debugging only
                Debug.Assert(normal.IsValid());

                vertex1.Normal.Update(normal);
                vertex2.Normal.Update(normal);
                vertex3.Normal.Update(normal);
            }
        }

        private void ComputeWeightedNormals()
        {
            foreach (var (vertex1, vertex2, vertex3) in TriangleVerticesList)
            {
                //Find inner angles of triangle
                var angle1 = vertex1.GetPointsAngle(vertex3, vertex2);
                var angle2 = vertex2.GetPointsAngle(vertex1, vertex3);
                var angle3 = vertex3.GetPointsAngle(vertex2, vertex1);

                //Find triangle normal, not unit but full normal vector
                var normal = vertex1.ParentSurface.ReverseNormals
                    ? Float64Vector3DAffineUtils.GetTriangleNormal(vertex3, vertex2, vertex1)
                    : Float64Vector3DAffineUtils.GetTriangleNormal(vertex1, vertex2, vertex3);

                // For debugging only
                Debug.Assert(
                    !double.IsNaN(angle1) &&
                    !double.IsNaN(angle2) &&
                    !double.IsNaN(angle3) &&
                    normal.IsValid()
                );

                // Update normals of triangle vertices.
                // See here for more information:
                // http://www.bytehazard.com/articles/vertnorm.html
                // normal.MakeUnitVector();
                vertex1.Normal.Update(angle1 * normal);
                vertex2.Normal.Update(angle2 * normal);
                vertex3.Normal.Update(angle3 * normal);
            }
        }

        public GrLatticeSurfaceList3D FinalizeSet()
        {
            if (IsReady) 
                return this;

            var vertexList = new List<GrLatticeSurfaceLocalFrame3D>();

            var setIndexOffset = 0;
            foreach (var batch in _surfaceList)
            {
                batch.FinalizeSurface(setIndexOffset);

                vertexList.AddRange(batch.VertexList);

                setIndexOffset += batch.Count;
            }

            VertexList = vertexList;

            TriangleVerticesList = _surfaceList
                .SelectMany(b => b.GetVertexTriangles())
                .ToArray();

            // Compute vertex normals
            foreach (var vertex in VertexList)
                vertex.Normal.Reset();

            switch (NormalComputationMethod)
            {
                case GrVertexNormalComputationMethod.AverageNormals:
                    ComputeAverageNormals();
                    break;

                case GrVertexNormalComputationMethod.AverageUnitNormals:
                    ComputeAverageUnitNormals();
                    break;

                case GrVertexNormalComputationMethod.WeightedNormals:
                    ComputeWeightedNormals();
                    break;
            }

            foreach (var vertex in VertexList)
                vertex.Normal.MakeUnit();

            return this;
        }

        public GrLatticeSurfaceTriangleGeometry3D GetTriangleGeometry()
        {
            return new GrLatticeSurfaceTriangleGeometry3D(this);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<GrLatticeSurfaceLocalFrame3D> GetEnumerator()
        {
            return IsReady
                ? VertexList.GetEnumerator()
                : _surfaceList.SelectMany(b => b.Vertices).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            var composer = new LinearTextComposer();

            foreach (var batch in Surfaces)
            {
                composer
                    .AppendLine($"Surface <{batch.CurveIndex,3}>:")
                    .IncreaseIndentation();

                foreach (var vertex in batch.VertexList)
                {
                    var c = vertex.Color.ToPixel<Rgb24>();

                    composer
                        .AppendAtNewLine($"Vertex <{vertex.Index,4}>:")
                        .Append($" Point({vertex.X:F5}, {vertex.Y:F5}, {vertex.Z:F5})")
                        .Append($" Normal({vertex.Normal.X:F5}, {vertex.Normal.Y:F5}, {vertex.Normal.Z:F5})")
                        .Append($" TextureUV({vertex.ParameterValue.Item1:F5}, {vertex.ParameterValue.Item2:F5})")
                        .Append($" Color({c.R}, {c.G}, {c.B})");
                }

                composer.AppendLineAtNewLine();

                composer
                    .DecreaseIndentation()
                    .AppendAtNewLine();
            }

            composer
                .AppendLineAtNewLine($"Triangles:")
                .IncreaseIndentation();

            foreach (var (i1, i2, i3) in TriangleVertexIndices)
            {
                composer.AppendAtNewLine($"<{i1.ToString().PadLeft(4)}, {i2.ToString().PadLeft(4)}, {i3.ToString().PadLeft(4)}>");
            }
            
            composer
                .DecreaseIndentation()
                .AppendAtNewLine();

            return composer.ToString();
        }
    }
}