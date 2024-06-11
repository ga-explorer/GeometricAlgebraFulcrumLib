using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space2D;

public sealed class ComposedMap2D :
    IAffineMap2D
{
    private readonly List<IAffineMap2D> _affineMapsList
        = new List<IAffineMap2D>();


    public ComposedMap2D()
    {
        _affineMapsList.Add(new IdentityMap2D());
    }

    public ComposedMap2D ResetToIdentity()
    {
        _affineMapsList.Clear();
        _affineMapsList.Add(new IdentityMap2D());

        return this;
    }

    public ComposedMap2D ResetToTranslation(double directionX, double directionY)
    {
        _affineMapsList.Clear();
        _affineMapsList.Add(new TranslationMap2D(directionX, directionY));

        return this;
    }

    //TODO: Complete the remaining maps

    public ComposedMap2D AppendMap(IAffineMap2D affineMap)
    {
        if (_affineMapsList.Count == 1 && _affineMapsList[0] is IdentityMap2D)
            _affineMapsList.Clear();

        _affineMapsList.Add(affineMap);

        return this;
    }

    public ComposedMap2D AppendMaps(IEnumerable<IAffineMap2D> affineMapsList)
    {
        if (_affineMapsList.Count == 1 && _affineMapsList[0] is IdentityMap2D)
            _affineMapsList.Clear();

        _affineMapsList.AddRange(affineMapsList);

        return this;
    }

    public ComposedMap2D PrependMap(IAffineMap2D affineMap)
    {
        if (_affineMapsList.Count == 1 && _affineMapsList[0] is IdentityMap2D)
            _affineMapsList.Clear();

        _affineMapsList.Insert(0, affineMap);

        return this;
    }

    public ComposedMap2D PrependMaps(IEnumerable<IAffineMap2D> affineMapsList)
    {
        if (_affineMapsList.Count == 1 && _affineMapsList[0] is IdentityMap2D)
            _affineMapsList.Clear();

        _affineMapsList.InsertRange(0, affineMapsList);

        return this;
    }

    public SquareMatrix3 GetSquareMatrix3()
    {
        //Construct matrix columns
        var c1 = MapVector(LinFloat64Vector2D.Create((Float64Scalar)1, 0));
        var c2 = MapVector(LinFloat64Vector2D.Create((Float64Scalar)0, 1));
        var c3 = MapPoint(LinFloat64Vector2D.Create((Float64Scalar)0, 0));

        return new SquareMatrix3()
        {
            Scalar00 = c1.X,
            Scalar01 = c2.X,
            Scalar02 = c3.X,
            Scalar10 = c1.Y,
            Scalar11 = c2.Y,
            Scalar12 = c3.Y,
            Scalar22 = 1.0
        };
    }

    public double[,] GetArray2D()
    {
        throw new NotImplementedException();
    }

    public bool SwapsHandedness { get; }

    public LinFloat64Vector2D MapPoint(ILinFloat64Vector2D point)
    {
        return _affineMapsList.Aggregate(
            point.ToLinVector2D(),
            (current, linearMap) => linearMap.MapPoint(current)
        );
    }

    public LinFloat64Vector2D MapVector(ILinFloat64Vector2D vector)
    {
        return _affineMapsList.Aggregate(
            vector.ToLinVector2D(),
            (current, linearMap) => linearMap.MapVector(current)
        );
    }

    public LinFloat64Vector2D MapNormal(ILinFloat64Vector2D normal)
    {
        return _affineMapsList.Aggregate(
            normal.ToLinVector2D(),
            (current, linearMap) => linearMap.MapNormal(current)
        );
    }

    public IAffineMap2D GetInverseAffineMap()
    {
        var invMap = new ComposedMap2D();

        invMap.AppendMaps(
            _affineMapsList
                .Select(m => m.GetInverseAffineMap())
                .Reverse()
        );

        return invMap;
    }

    public bool IsValid()
    {
        throw new NotImplementedException();
    }
}