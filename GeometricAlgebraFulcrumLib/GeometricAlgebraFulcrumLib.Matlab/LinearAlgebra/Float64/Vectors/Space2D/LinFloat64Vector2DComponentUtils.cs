using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space2D;

public static class LinFloat64Vector2DComponentUtils
{
    /// <summary>
    /// The value of the smallest component in this vector
    /// </summary>
    
    public static double GetMinComponent(this IPair<double> vector)
    {
        return vector.Item1 <= vector.Item2
            ? vector.Item1
            : vector.Item2;
    }

    /// <summary>
    /// The value of the largest component in this vector
    /// </summary>
    
    public static double GetMaxComponent(this IPair<double> vector)
    {
        return vector.Item1 >= vector.Item2
            ? vector.Item1
            : vector.Item2;
    }

    /// <summary>
    /// The index of the smallest component of this vector
    /// </summary>
    
    public static int GetMinComponentIndex(this IPair<double> vector)
    {
        return vector.Item1 <= vector.Item2
            ? 0 : 1;
    }

    /// <summary>
    /// The index of the largest component of this vector
    /// </summary>
    
    public static int GetMaxComponentIndex(this IPair<double> vector)
    {
        return vector.Item1 >= vector.Item2
            ? 0 : 1;
    }

    /// <summary>
    /// The index of the largest absolute component in this vector
    /// </summary>
    
    public static int GetMinAbsComponentIndex(this IPair<double> vector)
    {
        return Math.Abs(vector.Item1) <= Math.Abs(vector.Item2)
            ? 0 : 1;
    }

    /// <summary>
    /// The index of the largest absolute component in this vector
    /// </summary>
    
    public static int GetMaxAbsComponentIndex(this IPair<double> vector)
    {
        return Math.Abs(vector.Item1) >= Math.Abs(vector.Item2)
            ? 0 : 1;
    }

    /// <summary>
    /// Returns a new vector containing component-wise minimum values of the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    
    public static LinFloat64Vector2D Min(this IPair<double> v1, IPair<double> v2)
    {
        return LinFloat64Vector2D.Create(
            v1.Item1 < v2.Item1 ? v1.Item1 : v2.Item1,
            v1.Item2 < v2.Item2 ? v1.Item2 : v2.Item2
        );
    }

    /// <summary>
    /// Returns a new vector containing component-wise maximum values of the given vectors
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    
    public static LinFloat64Vector2D Max(this IPair<double> v1, IPair<double> v2)
    {
        return LinFloat64Vector2D.Create(
            v1.Item1 > v2.Item1 ? v1.Item1 : v2.Item1,
            v1.Item2 > v2.Item2 ? v1.Item2 : v2.Item2
        );
    }

    /// <summary>
    /// Returns a new vector containing component-wise ceiling values of the given vector
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    
    public static LinFloat64Vector2D Ceiling(this IPair<double> vector)
    {
        return LinFloat64Vector2D.Create(
            Math.Ceiling(vector.Item1),
            Math.Ceiling(vector.Item2)
        );
    }

    /// <summary>
    /// Returns a new vector containing component-wise floor values of the given vector
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    
    public static LinFloat64Vector2D Floor(this IPair<double> vector)
    {
        return LinFloat64Vector2D.Create(
            Math.Floor(vector.Item1),
            Math.Floor(vector.Item2)
        );
    }

    /// <summary>
    /// Returns a new vector containing component-wise absolute values of the given vector
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    
    public static LinFloat64Vector2D Abs(this IPair<double> vector)
    {
        return LinFloat64Vector2D.Create(
            Math.Abs(vector.Item1),
            Math.Abs(vector.Item2)
        );
    }

    /// <summary>
    /// Returns a permuted version of the components of this vector
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="xIndex"></param>
    /// <param name="yIndex"></param>
    /// <returns></returns>
    
    public static LinFloat64Vector2D Permute(this IPair<double> vector, int xIndex, int yIndex)
    {
        return LinFloat64Vector2D.Create(
            vector.GetComponent(xIndex),
            vector.GetComponent(yIndex)
        );
    }

    /// <summary>
    /// Returns a permuted version of the components of this vector. The given indices are always
    /// converted to a valid range using modulus operation
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="xIndex"></param>
    /// <param name="yIndex"></param>
    /// <returns></returns>
    
    public static LinFloat64Vector2D SafePermute(this IPair<double> vector, int xIndex, int yIndex)
    {
        return LinFloat64Vector2D.Create(
            vector.GetComponent(xIndex.Mod(2)),
            vector.GetComponent(yIndex.Mod(2))
        );
    }

    
    public static double GetComponent(this IPair<double> vector, int index)
    {
        return index switch
        {
            0 => vector.Item1,
            1 => vector.Item2,
            _ => 0d
        };
    }

    
    public static IEnumerable<double> GetComponents(this IPair<double> vector)
    {
        yield return vector.Item1;
        yield return vector.Item2;
    }

    
    public static IEnumerable<double> GetComponents(this IEnumerable<IPair<double>> vectorsList)
    {
        return vectorsList.SelectMany(t => t.GetComponents());
    }

    
    public static Pair<double> GetTupleXPair(this IReadOnlyList<IPair<double>> itemArray, int index)
    {
        return new Pair<double>(
            itemArray[index].Item1,
            itemArray[index + 1].Item1
        );
    }

    
    public static Triplet<double> GetTupleXTriplet(this IReadOnlyList<IPair<double>> itemArray, int index)
    {
        return new Triplet<double>(
            itemArray[index].Item1,
            itemArray[index + 1].Item1,
            itemArray[index + 2].Item1
        );
    }

    
    public static Quad<double> GetTupleXQuad(this IReadOnlyList<IPair<double>> itemArray, int index)
    {
        return new Quad<double>(
            itemArray[index].Item1,
            itemArray[index + 1].Item1,
            itemArray[index + 2].Item1,
            itemArray[index + 3].Item1
        );
    }

    
    public static Quint<double> GetTupleXQuint(this IReadOnlyList<IPair<double>> itemArray, int index)
    {
        return new Quint<double>(
            itemArray[index].Item1,
            itemArray[index + 1].Item1,
            itemArray[index + 2].Item1,
            itemArray[index + 3].Item1,
            itemArray[index + 4].Item1
        );
    }

    
    public static Hexad<double> GetTupleXHexad(this IReadOnlyList<IPair<double>> itemArray, int index)
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


    
    public static Pair<double> GetTupleYPair(this IReadOnlyList<IPair<double>> itemArray, int index)
    {
        return new Pair<double>(
            itemArray[index].Item2,
            itemArray[index + 1].Item2
        );
    }

    
    public static Triplet<double> GetTupleYTriplet(this IReadOnlyList<IPair<double>> itemArray, int index)
    {
        return new Triplet<double>(
            itemArray[index].Item2,
            itemArray[index + 1].Item2,
            itemArray[index + 2].Item2
        );
    }

    
    public static Quad<double> GetTupleYQuad(this IReadOnlyList<IPair<double>> itemArray, int index)
    {
        return new Quad<double>(
            itemArray[index].Item2,
            itemArray[index + 1].Item2,
            itemArray[index + 2].Item2,
            itemArray[index + 3].Item2
        );
    }

    
    public static Quint<double> GetTupleYQuint(this IReadOnlyList<IPair<double>> itemArray, int index)
    {
        return new Quint<double>(
            itemArray[index].Item2,
            itemArray[index + 1].Item2,
            itemArray[index + 2].Item2,
            itemArray[index + 3].Item2,
            itemArray[index + 4].Item2
        );
    }

    
    public static Hexad<double> GetTupleYHexad(this IReadOnlyList<IPair<double>> itemArray, int index)
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
}