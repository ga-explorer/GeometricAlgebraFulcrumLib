using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using DataStructuresLib.Basic;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicShapes.Triangles;
using NumericalGeometryLib.BasicShapes.Triangles.Immutable;
using GraphicsComposerLib.Geometry.Primitives;
using GraphicsComposerLib.Geometry.Primitives.Triangles;
using GraphicsComposerLib.Geometry.Primitives.Vertices;

namespace GraphicsComposerLib.Geometry.LatticeShapes.Surfaces
{
    public sealed class GrLatticeSurfaceTriangleGeometry3D :
        IGraphicsTriangleGeometry3D
    {
        public GrLatticeSurfaceList3D LatticeSurfaceList { get; }

        public GraphicsPrimitiveType3D PrimitiveType 
            => GraphicsPrimitiveType3D.Triangles;

        public int VertexCount 
            => LatticeSurfaceList.Count;

        public IEnumerable<int> GeometryIndices 
            => LatticeSurfaceList.TriangleVertexIndices.SelectMany(t => t.GetItems());

        public IEnumerable<IGraphicsVertex3D> GeometryVertices 
            => LatticeSurfaceList.VertexList;

        public IEnumerable<ITuple3D> GeometryPoints 
            => LatticeSurfaceList.VertexList;

        public int Count 
            => LatticeSurfaceList.TriangleVerticesList.Count;

        public ITriangle3D this[int index]
        {
            get
            {
                var (v1, v2, v3) = 
                    LatticeSurfaceList.TriangleVerticesList[index];

                return Triangle3D.Create(v1.Point, v2.Point, v3.Point);
            }
        }

        public IEnumerable<Triplet<ITuple3D>> TriangleVertexPoints
            => LatticeSurfaceList.TriangleVerticesList.Select(t => 
                new Triplet<ITuple3D>(t.Item1, t.Item2, t.Item3)
            );

        public IEnumerable<Triplet<int>> TriangleVertexIndices 
            => LatticeSurfaceList.TriangleVertexIndices;

        public IEnumerable<ITuple3D> VertexNormals
            => LatticeSurfaceList.VertexList.Select(v => v.Normal);
        
        public IEnumerable<ITuple2D> VertexTextureUVs 
            => LatticeSurfaceList.VertexList.Select(v => v.ParameterValue.ToTuple2D());
        
        public IEnumerable<Color> VertexColors 
            => LatticeSurfaceList.VertexList.Select(v => v.Color);

        public bool VertexNormalsEnabled { get; set; }
        
        public bool VertexTextureUVsEnabled { get; set; }
        
        public bool VertexColorsEnabled { get; set; }

        public GrVertexNormalComputationMethod NormalComputationMethod 
            => LatticeSurfaceList.NormalComputationMethod;


        internal GrLatticeSurfaceTriangleGeometry3D([NotNull] GrLatticeSurfaceList3D parentSet)
        {
            if (!parentSet.IsReady)
                throw new InvalidOperationException();

            LatticeSurfaceList = parentSet;
        }


        public ITuple3D GetGeometryPoint(int index)
        {
            return LatticeSurfaceList[index];
        }

        public GrNormal3D GetVertexNormal(int index)
        {
            return LatticeSurfaceList[index].Normal;
        }

        public void ComputeVertexNormals(bool inverseNormals)
        {
        }
        
        public IEnumerator<ITriangle3D> GetEnumerator()
        {
            return LatticeSurfaceList.TriangleVerticesList.Select(t => 
                Triangle3D.Create(t.Item1, t.Item2, t.Item3)
            ).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}