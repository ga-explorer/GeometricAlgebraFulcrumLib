using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Bounding.Float64;


namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Polytopes.Space2D.Float64;

public abstract record Float64Grid2D
{
    public Float64BoundingBox2D BoundingBox { get; }

    public double GridWidth 
        => BoundingBox.SizeX;

    public double GridHeight 
        => BoundingBox.SizeY;
    
    public double GridDiagonal 
        => BoundingBox.DiagonalLength;

    public double GridDiagonalSquared
        => BoundingBox.DiagonalLength.Square();

    public double MinX 
        => BoundingBox.MinX;

    public double MinY 
        => BoundingBox.MinY;
    
    public double MaxX 
        => BoundingBox.MaxX;

    public double MaxY 
        => BoundingBox.MaxY;


    protected Float64Grid2D(double gridWidth, double gridHeight)
    {
        BoundingBox = Float64BoundingBox2D.Around(0, 0, gridWidth, gridHeight);
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
        return Float64BoundingBox2D.Containing(
            MinX, MinY, MaxX, MaxY
        );
    }

    public Float64BoundingBoxComposer2D GetBoundingBoxComposer()
    {
        return Float64BoundingBoxComposer2D.Containing(
            MinX, 
            MinY, 
            MaxX, 
            MaxY
        );
    }

    public IFloat64BoundingSurface2D MapUsing(IFloat64AffineMap2D affineMap)
    {
        throw new NotImplementedException();
    }
}