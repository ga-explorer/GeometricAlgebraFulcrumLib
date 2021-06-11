using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.Collections.PeriodicLists2D;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;
using EuclideanGeometryLib.GraphicsGeometry.Vertices;

namespace EuclideanGeometryLib.GraphicsGeometry.Composers
{
    public class GraphicsGridMeshComposer3D
    {
        public int VerticesCount
            => PositionsGrid.Count;

        public int Count1 
            => PositionsGrid.Count1;

        public int Count2
            => PositionsGrid.Count2;

        public IEnumerable<ITuple3D> VertexPositions
            => PositionsGrid;

        public IEnumerable<IGraphicsNormal3D> VertexNormals
            => NormalsGrid;

        public IEnumerable<ITuple2D> VertexTextureUVs
            => TextureUVsGrid;

        public IPeriodicReadOnlyList2D<ITuple3D> PositionsGrid { get; }

        public IPeriodicReadOnlyList2D<IGraphicsNormal3D> NormalsGrid { get; }

        public IPeriodicReadOnlyList2D<ITuple2D> TextureUVsGrid { get; }

        public IPeriodicReadOnlyList2D<ITuple3D> ColorsGrid { get; }


        public GraphicsGridMeshComposer3D([NotNull] IPeriodicReadOnlyList2D<ITuple3D> positionsGrid, [NotNull] IPeriodicReadOnlyList2D<ITuple3D> colorsGrid)
        {
            Debug.Assert(
                positionsGrid.Count1 == colorsGrid.Count1 &&
                positionsGrid.Count2 == colorsGrid.Count2 &&
                positionsGrid.Count1 > 1 &&
                positionsGrid.Count2 > 1
            );

            PositionsGrid = positionsGrid;
            
            ColorsGrid = colorsGrid;

            NormalsGrid = new ProListConstantValues2D<IGraphicsNormal3D>(
                positionsGrid.Count1,
                positionsGrid.Count2,
                new GraphicsNormal3D(1, 0, 0)
            );

            var maxU = (double) (positionsGrid.Count1 - 1);
            var maxV = (double) (positionsGrid.Count2 - 1);
            TextureUVsGrid = new ProListComputedValues2D<ITuple2D>(
                positionsGrid.Count1,
                positionsGrid.Count2,
                (rowIndex, colIndex) => new Tuple2D(
                    rowIndex / maxU,
                    colIndex / maxV
                )
            );
        }



    }
}
