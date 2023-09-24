using System.Diagnostics;
using DataStructuresLib.Collections.PeriodicLists2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Composers
{
    public class GridMeshComposer
    {
        public int VerticesCount
            => PositionsGrid.Count;

        public int Count1 
            => PositionsGrid.Count1;

        public int Count2
            => PositionsGrid.Count2;

        public IEnumerable<IFloat64Vector3D> VertexPositions
            => PositionsGrid;

        public IEnumerable<Normal3D> VertexNormals
            => NormalsGrid;

        public IEnumerable<IFloat64Vector2D> VertexTextureUVs
            => TextureUVsGrid;

        public IPeriodicReadOnlyList2D<IFloat64Vector3D> PositionsGrid { get; }

        public IPeriodicReadOnlyList2D<Normal3D> NormalsGrid { get; }

        public IPeriodicReadOnlyList2D<IFloat64Vector2D> TextureUVsGrid { get; }

        public IPeriodicReadOnlyList2D<IFloat64Vector3D> ColorsGrid { get; }


        public GridMeshComposer(IPeriodicReadOnlyList2D<IFloat64Vector3D> positionsGrid, IPeriodicReadOnlyList2D<IFloat64Vector3D> colorsGrid)
        {
            Debug.Assert(
                positionsGrid.Count1 == colorsGrid.Count1 &&
                positionsGrid.Count2 == colorsGrid.Count2 &&
                positionsGrid.Count1 > 1 &&
                positionsGrid.Count2 > 1
            );

            PositionsGrid = positionsGrid;
            
            ColorsGrid = colorsGrid;

            NormalsGrid = new ProListConstantValues2D<Normal3D>(
                positionsGrid.Count1,
                positionsGrid.Count2,
                new Normal3D(1, 0, 0)
            );

            var maxU = (double) (positionsGrid.Count1 - 1);
            var maxV = (double) (positionsGrid.Count2 - 1);
            TextureUVsGrid = new ProListComputedValues2D<IFloat64Vector2D>(
                positionsGrid.Count1,
                positionsGrid.Count2,
                (rowIndex, colIndex) => Float64Vector2D.Create((Float64Scalar)(rowIndex / maxU),
                    (Float64Scalar)(colIndex / maxV))
            );
        }



    }
}
