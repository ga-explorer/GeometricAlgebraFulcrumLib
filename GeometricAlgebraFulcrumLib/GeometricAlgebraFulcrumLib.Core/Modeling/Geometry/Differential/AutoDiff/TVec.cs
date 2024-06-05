namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Differential.AutoDiff;

/// <summary>
/// A column vector made of terms.
/// </summary>
#if (!NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2 && !NETSTANDARD1_3 && !NETSTANDARD1_4 && !NETSTANDARD1_5 && !NETSTANDARD1_6)
[Serializable]
#endif
public class Vec
{
    private readonly Term[] _terms;

    /// <summary>
    /// Constructs a new instance of the <see cref="Vec"/> class given vector components.
    /// </summary>
    /// <param name="terms">The vector component terms</param>
    public Vec(IEnumerable<Term> terms)
    {
        Guard.NotNullOrEmpty(terms, nameof(terms));
        Guard.ItemsNotNull(terms, nameof(terms));

        _terms = terms.ToArray();
    }

    /// <summary>
    /// Constructs a new instance of the <see cref="Vec"/> class given vector components.
    /// </summary>
    /// <param name="terms">The vector component terms</param>
    public Vec(params Term[] terms)
        : this(terms as IEnumerable<Term>)
    {
        Guard.NotNullOrEmpty(terms, nameof(terms));
        Guard.ItemsNotNull(terms, nameof(terms));
    }

    /// <summary>
    /// Constructs a new instance of the <see cref="Vec"/> class using another vector's components.
    /// </summary>
    /// <param name="first">A vector containing the first vector components to use.</param>
    /// <param name="rest">More vector components to add in addition to the components in <paramref name="first"/></param>
    public Vec(Vec first, params Term[] rest)
        : this(first._terms.Concat(rest ?? Enumerable.Empty<Term>()))
    {
        Guard.ItemsNotNull(rest, nameof(rest));
    }

    private Vec(IReadOnlyList<Term> left, IReadOnlyList<Term> right, Func<Term, Term, Term> elemOp)
    {
        var n = left.Count;
        _terms = new Term[n];
        for (var i = 0; i < n; ++i)
            _terms[i] = elemOp(left[i], right[i]);
    }

    private Vec(IReadOnlyList<Term> input, Func<Term, Term> elemOp)
    {
        _terms = new Term[input.Count];
        for (var i = 0; i < input.Count; ++i)
            _terms[i] = elemOp(input[i]);
    }

    /// <summary>
    /// Gets a vector component given its zero-based index.
    /// </summary>
    /// <param name="index">The vector's component index.</param>
    /// <returns>The vector component.</returns>
    public Term this[int index]
    {
        get
        {
            Guard.InRange(index, nameof(index), 0, Dimension);
            return _terms[index];
        }
    }

    /// <summary>
    /// Gets a term representing the squared norm of this vector.
    /// </summary>
    public Term NormSquared
    {
        get
        {
            var powers = _terms.Select(x => TermBuilder.Power(x, 2));
            return TermBuilder.Sum(powers);
        }
    }

    /// <summary>
    /// Gets the dimensions of this vector
    /// </summary>
    public int Dimension
    {
        get
        {
            return _terms.Length;
        }
    }

    /// <summary>
    /// Gets the first vector component
    /// </summary>
    public Term X
    {
        get
        {
            return this[0];
        }
    }

    /// <summary>
    /// Gets the second vector component.
    /// </summary>
    public Term Y
    {
        get
        {
            Guard.InRange(Dimension, nameof(Dimension), 2, int.MaxValue);
            return this[1];
        }
    }

    /// <summary>
    /// Gets the third vector component
    /// </summary>
    public Term Z
    {
        get
        {
            Guard.InRange(Dimension, nameof(Dimension), 3, int.MaxValue);
            return this[2];
        }
    }

    /// <summary>
    /// Gets an array of all vector components.
    /// </summary>
    /// <returns>An array of all vector components. Users are free to modify this array. It doesn't point to any
    /// internal structures.</returns>
    public Term[] GetTerms()
    {
        return (Term[])_terms.Clone();
    }

    /// <summary>
    /// Constructs a sum of two term vectors.
    /// </summary>
    /// <param name="left">The first vector in the sum</param>
    /// <param name="right">The second vector in the sum</param>
    /// <returns>A vector representing the sum of <paramref name="left"/> and <paramref name="right"/></returns>
    public static Vec operator +(Vec left, Vec right)
    {
        Guard.NotNull(left, nameof(left));
        Guard.NotNull(right, nameof(right));
        Guard.MustHold(left.Dimension == right.Dimension, "left and right must be of the same dimension");
        return new Vec(left._terms, right._terms, (x, y) => x + y);
    }

    /// <summary>
    /// Constructs a difference of two term vectors,
    /// </summary>
    /// <param name="left">The first vector in the difference</param>
    /// <param name="right">The second vector in the difference.</param>
    /// <returns>A vector representing the difference of <paramref name="left"/> and <paramref name="right"/></returns>
    public static Vec operator -(Vec left, Vec right)
    {
        Guard.NotNull(left, nameof(left));
        Guard.NotNull(right, nameof(right));
        Guard.MustHold(left.Dimension == right.Dimension, "left and right must be of the same dimension");
        return new Vec(left._terms, right._terms, (x, y) => x - y);
    }

    /// <summary>
    /// Inverts a vector
    /// </summary>
    /// <param name="vector">The vector to invert</param>
    /// <returns>A vector repsesenting the inverse of <paramref name="vector"/></returns>
    public static Vec operator -(Vec vector)
    {
        Guard.NotNull(vector, nameof(vector));
        return vector * -1;
    }

    /// <summary>
    /// Multiplies a vector by a scalar
    /// </summary>
    /// <param name="vector">The vector</param>
    /// <param name="scalar">The scalar</param>
    /// <returns>A product of the vector <paramref name="vector"/> and the scalar <paramref name="scalar"/>.</returns>
    public static Vec operator *(Vec vector, Term scalar)
    {
        Guard.NotNull(vector, nameof(vector));
        Guard.NotNull(scalar, nameof(scalar));
        return new Vec(vector._terms, x => scalar * x);
    }

    /// <summary>
    /// Multiplies a vector by a scalar
    /// </summary>
    /// <param name="vector">The vector</param>
    /// <param name="scalar">The scalar</param>
    /// <returns>A product of the vector <paramref name="vector"/> and the scalar <paramref name="scalar"/>.</returns>
    public static Vec operator *(Term scalar, Vec vector)
    {
        Guard.NotNull(vector, nameof(vector));
        Guard.NotNull(scalar, nameof(scalar));
        return vector * scalar;
    }

    /// <summary>
    /// Constructs a term representing the inner product of two vectors.
    /// </summary>
    /// <param name="left">The first vector of the inner product</param>
    /// <param name="right">The second vector of the inner product</param>
    /// <returns>A term representing the inner product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static Term operator *(Vec left, Vec right)
    {
        return InnerProduct(left, right);
    }

    /// <summary>
    /// Constructs a term representing the inner product of two vectors.
    /// </summary>
    /// <param name="left">The first vector of the inner product</param>
    /// <param name="right">The second vector of the inner product</param>
    /// <returns>A term representing the inner product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static Term InnerProduct(Vec left, Vec right)
    {
        Guard.NotNull(left, nameof(left));
        Guard.NotNull(right, nameof(right));
        Guard.MustHold(left.Dimension == right.Dimension, "left and right must be of the same dimension");

        var products = from i in Enumerable.Range(0, left.Dimension)
            select left._terms[i] * right._terms[i];

        return TermBuilder.Sum(products);
    }

    /// <summary>
    /// Constructs a 3D cross-product vector given two 3D vectors.
    /// </summary>
    /// <param name="left">The left cross-product term</param>
    /// <param name="right">The right cross product term</param>
    /// <returns>A vector representing the cross product of <paramref name="left"/> and <paramref name="right"/></returns>
    public static Vec CrossProduct(Vec left, Vec right)
    {
        Guard.NotNull(left, nameof(left));
        Guard.NotNull(right, nameof(right));
        Guard.MustHold(left.Dimension == 3 && right.Dimension == 3,
            "vectors must be three dimensional");

        return new Vec(
            left.Y * right.Z - left.Z * right.Y,
            left.Z * right.X - left.X * right.Z,
            left.X * right.Y - left.Y * right.X
        );
    }
}