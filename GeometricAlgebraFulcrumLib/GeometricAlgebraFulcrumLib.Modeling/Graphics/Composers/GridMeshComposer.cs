using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Collections.PeriodicLists2D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Composers;

public class GridMeshComposer
{
    public int VerticesCount
        => PositionsGrid.Count;

    public int Count1 
        => PositionsGrid.Count1;

    public int Count2
        => PositionsGrid.Count2;

    public IEnumerable<ILinFloat64Vector3D> VertexPositions
        => PositionsGrid;

    public IEnumerable<LinFloat64Normal3D> VertexNormals
        => NormalsGrid;

    public IEnumerable<ILinFloat64Vector2D> VertexTextureUVs
        => TextureUVsGrid;

    public IPeriodicReadOnlyList2D<ILinFloat64Vector3D> PositionsGrid { get; }

    public IPeriodicReadOnlyList2D<LinFloat64Normal3D> NormalsGrid { get; }

    public IPeriodicReadOnlyList2D<ILinFloat64Vector2D> TextureUVsGrid { get; }

    public IPeriodicReadOnlyList2D<ILinFloat64Vector3D> ColorsGrid { get; }


    public GridMeshComposer(IPeriodicReadOnlyList2D<ILinFloat64Vector3D> positionsGrid, IPeriodicReadOnlyList2D<ILinFloat64Vector3D> colorsGrid)
    {
        Debug.Assert(
            positionsGrid.Count1 == colorsGrid.Count1 &&
            positionsGrid.Count2 == colorsGrid.Count2 &&
            positionsGrid.Count1 > 1 &&
            positionsGrid.Count2 > 1
        );

        PositionsGrid = positionsGrid;
            
        ColorsGrid = colorsGrid;

        NormalsGrid = new ProListConstantValues2D<LinFloat64Normal3D>(
            positionsGrid.Count1,
            positionsGrid.Count2,
            new LinFloat64Normal3D(1, 0, 0)
        );

        var maxU = (double) (positionsGrid.Count1 - 1);
        var maxV = (double) (positionsGrid.Count2 - 1);
        TextureUVsGrid = new ProListComputedValues2D<ILinFloat64Vector2D>(
            positionsGrid.Count1,
            positionsGrid.Count2,
            (rowIndex, colIndex) => LinFloat64Vector2D.Create((Float64Scalar)(rowIndex / maxU),
                (Float64Scalar)(colIndex / maxV))
        );
    }



}