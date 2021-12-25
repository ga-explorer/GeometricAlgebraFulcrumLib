using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NumericalGeometryLib.BasicMath.Tuples;
using GraphicsComposerLib.Geometry.Primitives.Vertices;

namespace GraphicsComposerLib.Geometry.Primitives.Points
{
    /// <summary>
    /// This class represents a points geometry where the list of points are completely
    /// used in rendering
    /// </summary>
    public sealed class GrPointSoupGeometry3D
        : IGraphicsPointGeometry3D
    {
        public static GrPointSoupGeometry3D Create(IReadOnlyList<ITuple3D> pointsList)
        {
            return new GrPointSoupGeometry3D(pointsList);
        }

        public static GrPointSoupGeometry3D Create(params ITuple3D[] pointsList)
        {
            return new GrPointSoupGeometry3D(pointsList);
        }

        public static GrPointSoupGeometry3D Create(IEnumerable<ITuple3D> pointsList)
        {
            return new GrPointSoupGeometry3D(pointsList.ToArray());
        }


        public IEnumerable<IGraphicsVertex3D> GeometryVertices
            => _vertexPoints.Select((p, i) => new GrVertex3D(i, p));

        private readonly IReadOnlyList<ITuple3D> _vertexPoints;
        public IEnumerable<ITuple3D> GeometryPoints 
            => _vertexPoints;

        public GraphicsPrimitiveType3D PrimitiveType
            => GraphicsPrimitiveType3D.Points;

        public int VertexCount 
            => _vertexPoints.Count;

        public int Count 
            => _vertexPoints.Count;

        public ITuple3D this[int index] 
            => _vertexPoints[index];

        public IEnumerable<int> GeometryIndices
            => Enumerable.Range(0, _vertexPoints.Count).ToArray();


        private GrPointSoupGeometry3D(IReadOnlyList<ITuple3D> pointsList)
        {
            _vertexPoints = pointsList;
        }

        
        public ITuple3D GetGeometryPoint(int index)
        {
            return _vertexPoints[index];
        }

        public IEnumerator<ITuple3D> GetEnumerator()
        {
            return _vertexPoints.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}