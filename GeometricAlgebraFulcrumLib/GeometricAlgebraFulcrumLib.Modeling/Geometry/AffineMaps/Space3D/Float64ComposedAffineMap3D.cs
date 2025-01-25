using System.Collections;
using System.Numerics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;

public class Float64ComposedAffineMap3D :
    IFloat64AffineMap3D,
    IReadOnlyList<IFloat64AffineMap3D>
{
    private readonly List<IFloat64AffineMap3D> _affineMapsList
        = new List<IFloat64AffineMap3D>();


    public int Count
        => _affineMapsList.Count;

    public IFloat64AffineMap3D this[int index]
        => _affineMapsList[index];


    public Float64ComposedAffineMap3D()
    {
        _affineMapsList.Add(Float64IdentityAffineMap3D.Instance);
    }


    public Float64ComposedAffineMap3D ResetToIdentity()
    {
        _affineMapsList.Clear();
        _affineMapsList.Add(Float64IdentityAffineMap3D.Instance);

        return this;
    }

    public Float64ComposedAffineMap3D AppendAffineMap(IFloat64AffineMap3D affineMap)
    {
        if (_affineMapsList.Count == 1 && _affineMapsList[0] is Float64IdentityAffineMap3D)
            _affineMapsList.Clear();

        _affineMapsList.Add(affineMap);

        return this;
    }

    public Float64ComposedAffineMap3D AppendAffineMaps(IEnumerable<IFloat64AffineMap3D> affineMapsList)
    {
        if (_affineMapsList.Count == 1 && _affineMapsList[0] is Float64IdentityAffineMap3D)
            _affineMapsList.Clear();

        _affineMapsList.AddRange(affineMapsList);

        return this;
    }

    public Float64ComposedAffineMap3D PrependAffineMap(IFloat64AffineMap3D affineMap)
    {
        if (_affineMapsList.Count == 1 && _affineMapsList[0] is Float64IdentityAffineMap3D)
            _affineMapsList.Clear();

        _affineMapsList.Insert(0, affineMap);

        return this;
    }

    public Float64ComposedAffineMap3D PrependAffineMaps(IEnumerable<IFloat64AffineMap3D> affineMapsList)
    {
        if (_affineMapsList.Count == 1 && _affineMapsList[0] is Float64IdentityAffineMap3D)
            _affineMapsList.Clear();

        _affineMapsList.InsertRange(0, affineMapsList);

        return this;
    }

    public bool SwapsHandedness
        => _affineMapsList.Count(m => m.SwapsHandedness).IsOdd();


    public bool IsIdentity()
    {
        throw new NotImplementedException();
    }

    public bool IsNearIdentity(double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        throw new NotImplementedException();
    }

    public SquareMatrix4 GetSquareMatrix4()
    {
        //Construct matrix columns
        var c0 = MapVector(LinFloat64Vector3D.Create(1, 0, 0));
        var c1 = MapVector(LinFloat64Vector3D.Create(0, 1, 0));
        var c2 = MapVector(LinFloat64Vector3D.Create(0, 0, 1));
        var c3 = MapPoint(LinFloat64Vector3D.Create(0, 0, 0));

        return new SquareMatrix4()
        {
            Scalar00 = c0.X,
            Scalar01 = c1.X,
            Scalar02 = c2.X,
            Scalar03 = c3.X,
            Scalar10 = c0.Y,
            Scalar11 = c1.Y,
            Scalar12 = c2.Y,
            Scalar13 = c3.Y,
            Scalar20 = c0.Z,
            Scalar21 = c1.Z,
            Scalar22 = c2.Z,
            Scalar23 = c3.Z,
            Scalar33 = 1d
        };
    }

    public Matrix4x4 GetMatrix4x4()
    {
        //Construct matrix columns
        var c0 = MapVector(LinFloat64Vector3D.E1);
        var c1 = MapVector(LinFloat64Vector3D.E2);
        var c2 = MapVector(LinFloat64Vector3D.E3);
        var c3 = MapPoint(LinFloat64Vector3D.Zero);

        return new Matrix4x4(
            (float)c0.X, (float)c1.X, (float)c2.X, (float)c3.X,
            (float)c0.Y, (float)c1.Y, (float)c2.Y, (float)c3.Y,
            (float)c0.Z, (float)c1.Z, (float)c2.Z, (float)c3.Z,
            0f, 0f, 0f, 1.0f
        );
    }

    public double[,] GetArray2D()
    {
        var c0 = MapVector(LinFloat64Vector3D.E1);
        var c1 = MapVector(LinFloat64Vector3D.E2);
        var c2 = MapVector(LinFloat64Vector3D.E3);
        var c3 = MapPoint(LinFloat64Vector3D.Zero);

        var array = new double[4, 4];

        array[0, 0] = c0.X; array[0, 1] = c1.X; array[0, 2] = c2.X; array[0, 3] = c3.X;
        array[1, 0] = c0.Y; array[1, 1] = c1.Y; array[1, 2] = c2.Y; array[1, 3] = c3.Y;
        array[2, 0] = c0.Z; array[2, 1] = c1.Z; array[2, 2] = c2.Z; array[2, 3] = c3.Z;
        array[3, 3] = 1d;

        return array;
    }

    public LinFloat64Vector3D MapPoint(ILinFloat64Vector3D point)
    {
        return _affineMapsList.Aggregate(
            point.ToLinVector3D(),
            (current, linearMap) => linearMap.MapPoint(current)
        );
    }

    public LinFloat64Vector3D MapVector(ILinFloat64Vector3D vector)
    {
        return _affineMapsList.Aggregate(
            vector.ToLinVector3D(),
            (current, linearMap) => linearMap.MapVector(current)
        );
    }

    public LinFloat64Vector3D MapNormal(ILinFloat64Vector3D normal)
    {
        return _affineMapsList.Aggregate(
            normal.ToLinVector3D(),
            (current, linearMap) => linearMap.MapNormal(current)
        );
    }

    public IFloat64AffineMap3D GetInverseAffineMap()
    {
        var invMap = new Float64ComposedAffineMap3D();

        foreach (var map in _affineMapsList)
            invMap.PrependAffineMap(map.GetInverseAffineMap());

        return invMap;
    }

    public IEnumerator<IFloat64AffineMap3D> GetEnumerator()
    {
        return _affineMapsList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public bool IsValid()
    {
        throw new NotImplementedException();
    }
}