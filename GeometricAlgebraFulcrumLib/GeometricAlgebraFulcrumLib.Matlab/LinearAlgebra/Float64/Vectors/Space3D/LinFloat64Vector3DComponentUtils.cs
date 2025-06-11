using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space3D;

public static class LinFloat64Vector3DComponentUtils
{
    /// <summary>
    /// The value of the smallest component in this vector
    /// </summary>
    
    public static double GetMinComponent(this ITriplet<double> vector)
    {
        return vector.Item1 < vector.Item2
            ? vector.Item1 < vector.Item3 ? vector.Item1 : vector.Item3
            : vector.Item2 < vector.Item3 ? vector.Item2 : vector.Item3;
    }

    /// <summary>
    /// The value of the largest component in this vector
    /// </summary>
    
    public static double GetMaxComponent(this ITriplet<double> vector)
    {
        return vector.Item1 > vector.Item2
            ? vector.Item1 > vector.Item3 ? vector.Item1 : vector.Item3
            : vector.Item2 > vector.Item3 ? vector.Item2 : vector.Item3;
    }

    /// <summary>
    /// The index of the smallest component in this vector
    /// </summary>
    
    public static int GetMinComponentIndex(this ITriplet<double> vector)
    {
        return vector.Item1 < vector.Item2 ? vector.Item1 < vector.Item3 ? 0 : 2 : vector.Item2 < vector.Item3 ? 1 : 2;
    }

    /// <summary>
    /// The index of the largest component in this vector
    /// </summary>
    
    public static int GetMaxComponentIndex(this ITriplet<double> vector)
    {
        return vector.Item1 > vector.Item2 ? vector.Item1 > vector.Item3 ? 0 : 2 : vector.Item2 > vector.Item3 ? 1 : 2;
    }

    /// <summary>
    /// The index of the largest absolute component in this vector
    /// </summary>
    
    public static int GetMaxAbsComponentIndex(this ITriplet<double> vector)
    {
        var absX = vector.Item1.Abs();
        var absY = vector.Item2.Abs();
        var absZ = vector.Item3.Abs();

        if (absX >= absY)
            return absX >= absZ ? 0 : 2;

        return absY >= absZ ? 1 : 2;
    }

    /// <summary>
    /// The index of the largest absolute component in this vector
    /// </summary>
    
    public static int GetMinAbsComponentIndex(this ITriplet<double> vector)
    {
        var absX = Math.Abs(vector.Item1);
        var absY = Math.Abs(vector.Item2);
        var absZ = Math.Abs(vector.Item3);

        if (absX < absY)
            return absX < absZ ? 0 : 2;

        return absY < absZ ? 1 : 2;
    }

    /// <summary>
    /// Returns a new vector containing component-wise minimum values of the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    
    public static LinFloat64Vector3D Min(this ITriplet<double> v1, ITriplet<double> v2)
    {
        return LinFloat64Vector3D.Create(
            v1.Item1 < v2.Item1 ? v1.Item1 : v2.Item1,
            v1.Item2 < v2.Item2 ? v1.Item2 : v2.Item2,
            v1.Item3 < v2.Item3 ? v1.Item3 : v2.Item3
        );
    }

    /// <summary>
    /// Returns a new vector containing component-wise maximum values of the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    
    public static LinFloat64Vector3D Max(this ITriplet<double> v1, ITriplet<double> v2)
    {
        return LinFloat64Vector3D.Create(
            v1.Item1 > v2.Item1 ? v1.Item1 : v2.Item1,
            v1.Item2 > v2.Item2 ? v1.Item2 : v2.Item2,
            v1.Item3 > v2.Item3 ? v1.Item3 : v2.Item3
        );
    }

    /// <summary>
    /// Returns a permuted version of the components of this vector
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="xIndex"></param>
    /// <param name="yIndex"></param>
    /// <param name="zIndex"></param>
    /// <returns></returns>
    
    public static LinFloat64Vector3D Permute(this ITriplet<double> vector, int xIndex, int yIndex, int zIndex)
    {
        return LinFloat64Vector3D.Create(
            vector.GetComponent(1 << xIndex),
            vector.GetComponent(1 << yIndex),
            vector.GetComponent(1 << zIndex)
        );
    }

    /// <summary>
    /// Returns a permuted version of the components of this vector. The given indices are always
    /// converted to a valid range using modulus operation
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="xIndex"></param>
    /// <param name="yIndex"></param>
    /// <param name="zIndex"></param>
    /// <returns></returns>
    
    public static LinFloat64Vector3D SafePermute(this ITriplet<double> vector, int xIndex, int yIndex, int zIndex)
    {
        return LinFloat64Vector3D.Create(
            vector.GetComponent(1 << xIndex.Mod(3)),
            vector.GetComponent(1 << yIndex.Mod(3)),
            vector.GetComponent(1 << zIndex.Mod(3))
        );
    }

    
    public static double GetComponent(this ITriplet<double> vector, int index)
    {
        return index switch
        {
            0 => vector.Item1,
            1 => vector.Item2,
            2 => vector.Item3,
            _ => 0d
        };
    }

    
    public static double GetComponent(this ITriplet<double> vector, LinBasisVector axis)
    {
        if (axis == LinBasisVector.Px) return vector.Item1;
        if (axis == LinBasisVector.Nx) return -vector.Item1;
        if (axis == LinBasisVector.Py) return vector.Item2;
        if (axis == LinBasisVector.Ny) return -vector.Item2;
        if (axis == LinBasisVector.Pz) return vector.Item3;
        if (axis == LinBasisVector.Nz) return -vector.Item3;

        return 0d;
    }

    
    public static IEnumerable<double> GetComponents(this ITriplet<double> vector)
    {
        yield return vector.Item1;
        yield return vector.Item2;
        yield return vector.Item3;
    }

    
    public static IEnumerable<double> GetComponents(this IEnumerable<ITriplet<double>> vectorsList)
    {
        return vectorsList.SelectMany(t => t.GetComponents());
    }


    
    public static LinFloat64Vector3D MapComponents(this ITriplet<double> vector, Func<double, double> scalarMapping)
    {
        return LinFloat64Vector3D.Create(
            scalarMapping(vector.Item1),
            scalarMapping(vector.Item2),
            scalarMapping(vector.Item3)
        );
    }

    
    public static LinFloat64Vector3D ComponentsMin(this ITriplet<double> vector, double scalar)
    {
        return LinFloat64Vector3D.Create(
            Math.Min(vector.Item1, scalar),
            Math.Min(vector.Item2, scalar),
            Math.Min(vector.Item3, scalar)
        );
    }

    
    public static LinFloat64Vector3D ComponentsMax(this ITriplet<double> vector, double scalar)
    {
        return LinFloat64Vector3D.Create(
            Math.Max(vector.Item1, scalar),
            Math.Max(vector.Item2, scalar),
            Math.Max(vector.Item3, scalar)
        );
    }

    
    public static double ComponentsMin(this ITriplet<double> vector)
    {
        return Math.Min(vector.Item1, Math.Min(vector.Item2, vector.Item3));
    }

    
    public static double ComponentsMax(this ITriplet<double> vector)
    {
        return Math.Max(vector.Item1, Math.Max(vector.Item2, vector.Item3));
    }

    
    public static LinFloat64Vector3D ComponentsAbs(this ITriplet<double> vector)
    {
        return LinFloat64Vector3D.Create(Math.Abs(vector.Item1),
            Math.Abs(vector.Item2),
            Math.Abs(vector.Item3));
    }

    
    public static LinFloat64Vector3D ComponentsProduct(this ITriplet<double> vector1, ITriplet<double> vector2)
    {
        return LinFloat64Vector3D.Create(vector1.Item1 * vector2.Item1,
            vector1.Item2 * vector2.Item2,
            vector1.Item3 * vector2.Item3);
    }

    
    public static LinFloat64Vector3D ComponentsProduct(this ITriplet<double> vector1, ITriplet<double> vector2, ITriplet<double> vector3)
    {
        return LinFloat64Vector3D.Create(vector1.Item1 * vector2.Item1 * vector3.Item1,
            vector1.Item2 * vector2.Item2 * vector3.Item2,
            vector1.Item3 * vector2.Item3 * vector3.Item3);
    }

    
    public static Pair<double> GetTupleXPair(this IReadOnlyList<ITriplet<double>> itemArray, int index)
    {
        return new Pair<double>(
            itemArray[index].Item1,
            itemArray[index + 1].Item1
        );
    }

    
    public static Triplet<double> GetTupleXTriplet(this IReadOnlyList<ITriplet<double>> itemArray, int index)
    {
        return new Triplet<double>(
            itemArray[index].Item1,
            itemArray[index + 1].Item1,
            itemArray[index + 2].Item1
        );
    }

    
    public static Quad<double> GetTupleXQuad(this IReadOnlyList<ITriplet<double>> itemArray, int index)
    {
        return new Quad<double>(
            itemArray[index].Item1,
            itemArray[index + 1].Item1,
            itemArray[index + 2].Item1,
            itemArray[index + 3].Item1
        );
    }

    
    public static Quint<double> GetTupleXQuint(this IReadOnlyList<ITriplet<double>> itemArray, int index)
    {
        return new Quint<double>(
            itemArray[index].Item1,
            itemArray[index + 1].Item1,
            itemArray[index + 2].Item1,
            itemArray[index + 3].Item1,
            itemArray[index + 4].Item1
        );
    }

    
    public static Hexad<double> GetTupleXHexad(this IReadOnlyList<ITriplet<double>> itemArray, int index)
    {
        return new Hexad<double>(
            itemArray[index].Item1,
            itemArray[index + 1].Item1,
            itemArray[index + 2].Item1,
            itemArray[index + 3].Item1,
            itemArray[index + 4].Item1,
            itemArray[index + 5].Item1
        );
    }


    
    public static Pair<double> GetTupleYPair(this IReadOnlyList<ITriplet<double>> itemArray, int index)
    {
        return new Pair<double>(
            itemArray[index].Item2,
            itemArray[index + 1].Item2
        );
    }

    
    public static Triplet<double> GetTupleYTriplet(this IReadOnlyList<ITriplet<double>> itemArray, int index)
    {
        return new Triplet<double>(
            itemArray[index].Item2,
            itemArray[index + 1].Item2,
            itemArray[index + 2].Item2
        );
    }

    
    public static Quad<double> GetTupleYQuad(this IReadOnlyList<ITriplet<double>> itemArray, int index)
    {
        return new Quad<double>(
            itemArray[index].Item2,
            itemArray[index + 1].Item2,
            itemArray[index + 2].Item2,
            itemArray[index + 3].Item2
        );
    }

    
    public static Quint<double> GetTupleYQuint(this IReadOnlyList<ITriplet<double>> itemArray, int index)
    {
        return new Quint<double>(
            itemArray[index].Item2,
            itemArray[index + 1].Item2,
            itemArray[index + 2].Item2,
            itemArray[index + 3].Item2,
            itemArray[index + 4].Item2
        );
    }

    
    public static Hexad<double> GetTupleYHexad(this IReadOnlyList<ITriplet<double>> itemArray, int index)
    {
        return new Hexad<double>(
            itemArray[index].Item2,
            itemArray[index + 1].Item2,
            itemArray[index + 2].Item2,
            itemArray[index + 3].Item2,
            itemArray[index + 4].Item2,
            itemArray[index + 5].Item2
        );
    }


    
    public static Pair<double> GetTupleZPair(this IReadOnlyList<ITriplet<double>> itemArray, int index)
    {
        return new Pair<double>(
            itemArray[index].Item3,
            itemArray[index + 1].Item3
        );
    }

    
    public static Triplet<double> GetTupleZTriplet(this IReadOnlyList<ITriplet<double>> itemArray, int index)
    {
        return new Triplet<double>(
            itemArray[index].Item3,
            itemArray[index + 1].Item3,
            itemArray[index + 2].Item3
        );
    }

    
    public static Quad<double> GetTupleZQuad(this IReadOnlyList<ITriplet<double>> itemArray, int index)
    {
        return new Quad<double>(
            itemArray[index].Item3,
            itemArray[index + 1].Item3,
            itemArray[index + 2].Item3,
            itemArray[index + 3].Item3
        );
    }

    
    public static Quint<double> GetTupleZQuint(this IReadOnlyList<ITriplet<double>> itemArray, int index)
    {
        return new Quint<double>(
            itemArray[index].Item3,
            itemArray[index + 1].Item3,
            itemArray[index + 2].Item3,
            itemArray[index + 3].Item3,
            itemArray[index + 4].Item3
        );
    }

    
    public static Hexad<double> GetTupleZHexad(this IReadOnlyList<ITriplet<double>> itemArray, int index)
    {
        return new Hexad<double>(
            itemArray[index].Item3,
            itemArray[index + 1].Item3,
            itemArray[index + 2].Item3,
            itemArray[index + 3].Item3,
            itemArray[index + 4].Item3,
            itemArray[index + 5].Item3
        );
    }


    // TODO: Make more of these
    
    public static LinFloat64Vector3D CeilingToLinVector3D(this ITriplet<double> vector)
    {
        return vector.ToLinVector3D(Math.Ceiling);
    }

    
    public static LinFloat64Vector3D FloorToLinVector3D(this ITriplet<double> vector)
    {
        return vector.ToLinVector3D(Math.Floor);
    }

    
    public static LinFloat64Vector3D AbsToLinVector3D(this ITriplet<double> vector)
    {
        return vector.ToLinVector3D(Math.Abs);
    }

}