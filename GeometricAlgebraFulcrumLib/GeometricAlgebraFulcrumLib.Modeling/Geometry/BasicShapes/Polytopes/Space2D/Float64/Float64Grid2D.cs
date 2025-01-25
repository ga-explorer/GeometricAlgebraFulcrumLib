using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space2D.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Polytopes.Space2D.Float64;

public abstract record Float64Grid2D :
    IFloat64BoundingBox2D
{
    public double GridWidth { get; }

    public double GridHeight { get; }
    
    public double GridDiagonal
        => (GridWidth.Square() + GridHeight.Square()).Sqrt();

    public double GridDiagonalSquared
        => GridWidth.Square() + GridHeight.Square();

    public double MinX 
        => -GridWidth / 2;

    public double MinY 
        => -GridHeight / 2;

    public double MaxX 
        => GridWidth / 2;

    public double MaxY 
        => GridHeight / 2;
    
    public bool IntersectionTestsEnabled 
        => true;


    protected Float64Grid2D(double gridWidth, double gridHeight)
    {
        GridWidth = gridWidth;
        GridHeight = gridHeight;
    }
    

    public abstract bool IsValid();
    
    public bool ContainsPoint(LinFloat64Vector2D point)
    {
        return point.Item1 >= -GridWidth &&
               point.Item1 <= GridWidth &&
               point.Item2 >= -GridHeight &&
               point.Item2 <= GridHeight;
    }

    public Float64BoundingBox2D GetBoundingBox()
    {
        return Float64BoundingBox2D.Create(
            MinX, MinY, MaxX, MaxY
        );
    }

    public Float64BoundingBoxComposer2D GetBoundingBoxComposer()
    {
        return Float64BoundingBoxComposer2D.CreateFromPoints(
            MinX, 
            MinY, 
            MaxX, 
            MaxY
        );
    }

    public IFloat64BorderCurve2D MapUsing(IFloat64AffineMap2D affineMap)
    {
        throw new NotImplementedException();
    }
}