using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicShapes.Lines;
using GeometricAlgebraFulcrumLib.MathBase.BasicShapes.Lines.Immutable;
using GraphicsComposerLib.Geometry.Primitives.Vertices;

namespace GraphicsComposerLib.Geometry.Primitives.Lines
{
    public sealed class GrLineStripGeometry3D
        : IGraphicsLineGeometry3D
    {
        public static GrLineStripGeometry3D Create(IReadOnlyList<IFloat64Tuple3D> pointsList)
        {
            return new GrLineStripGeometry3D(pointsList);
        }

        public static GrLineStripGeometry3D Create(params IFloat64Tuple3D[] pointsList)
        {
            return new GrLineStripGeometry3D(pointsList);
        }

        public static GrLineStripGeometry3D Create(IEnumerable<IFloat64Tuple3D> pointsList)
        {
            return new GrLineStripGeometry3D(pointsList.ToArray());
        }


        public GraphicsPrimitiveType3D PrimitiveType
            => GraphicsPrimitiveType3D.LineStrip;

        public int VertexCount 
            => _vertexPoints.Count;

        public IEnumerable<IGraphicsVertex3D> GeometryVertices
            => GeometryPoints.Select((p, i) => new GrVertex3D(i, p));

        private readonly IReadOnlyList<IFloat64Tuple3D> _vertexPoints;
        public IEnumerable<IFloat64Tuple3D> GeometryPoints 
            => _vertexPoints;

        public IEnumerable<int> GeometryIndices
            => Enumerable.Range(0, _vertexPoints.Count).ToArray();

        public int Count
            => _vertexPoints.Count - 1;

        public ILineSegment3D this[int index] 
            => LineSegment3D.Create(
                _vertexPoints[index],
                _vertexPoints[index + 1]
            );

        public IEnumerable<Pair<IFloat64Tuple3D>> LineVertexPoints
        {
            get
            {
                for (var i = 0; i < _vertexPoints.Count - 1; i++)
                    yield return new Pair<IFloat64Tuple3D>(
                        _vertexPoints[i],
                        _vertexPoints[i + 1]
                    );
            }
        }

        public IEnumerable<Pair<int>> LineVertexIndices
        {
            get
            {
                for (var i = 0; i < _vertexPoints.Count - 1; i++)
                    yield return new Pair<int>(i, i + 1);
            }
        }


        private GrLineStripGeometry3D(IReadOnlyList<IFloat64Tuple3D> pointsList)
        {
            if (pointsList.Count < 2)
                throw new InvalidOperationException();

            _vertexPoints = pointsList;
        }
        
        
        public IFloat64Tuple3D GetGeometryPoint(int index)
        {
            return _vertexPoints[index];
        }

        public IEnumerator<ILineSegment3D> GetEnumerator()
        {
            for (var i = 0; i < _vertexPoints.Count - 1; i++)
                yield return LineSegment3D.Create(
                    _vertexPoints[i], 
                    _vertexPoints[i + 1]
                );
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}