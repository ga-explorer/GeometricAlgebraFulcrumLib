﻿namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.AutoDiff;

/// <summary>
/// Base class for all automatically-differentiable terms.
/// </summary>
#if (!NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2 && !NETSTANDARD1_3 && !NETSTANDARD1_4 && !NETSTANDARD1_5 && !NETSTANDARD1_6)
[Serializable]
#endif
public abstract class Term
{
    /// <summary>
    /// Accepts a term visitor
    /// </summary>
    /// <param name="visitor">The term visitor to accept</param>
    public abstract void Accept(ITermVisitor visitor);

    /// <summary>
    /// Accepts a term visitor with a generic result
    /// </summary>
    /// <typeparam name="TResult">The type of the result from the visitor's function</typeparam>
    /// <param name="visitor">The visitor to accept</param>
    /// <returns>The result from the visitor's visit function.</returns>
    public abstract TResult Accept<TResult>(ITermVisitor<TResult> visitor);

    /// <summary>
    /// Converts a floating point constant to a constant term.
    /// </summary>
    /// <param name="value">The floating point constant</param>
    /// <returns>The resulting term.</returns>
    public static implicit operator Term(double value)
    {
        return TermBuilder.Constant(value);
    }

    /// <summary>
    /// Constructs a sum of the two given terms.
    /// </summary>
    /// <param name="left">First term in the sum</param>
    /// <param name="right">Second term in the sum</param>
    /// <returns>A term representing the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static Term operator +(Term left, Term right)
    {
        return TermBuilder.Sum(left, right);
    }

    /// <summary>
    /// Constructs a product term of the two given terms.
    /// </summary>
    /// <param name="left">The first term in the product</param>
    /// <param name="right">The second term in the product</param>
    /// <returns>A term representing the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static Term operator *(Term left, Term right)
    {
        return TermBuilder.Product(left, right);
    }

    /// <summary>
    /// Constructs a fraction term of the two given terms.
    /// </summary>
    /// <param name="numerator">The numerator of the fraction. That is, the "top" part.</param>
    /// <param name="denominator">The denominator of the fraction. That is, the "bottom" part.</param>
    /// <returns>A term representing the fraction <paramref name="numerator"/> over <paramref name="denominator"/>.</returns>
    public static Term operator /(Term numerator, Term denominator)
    {
        return TermBuilder.Product(numerator, TermBuilder.Power(denominator, -1));
    }

    /// <summary>
    /// Constructs a difference of the two given terms.
    /// </summary>
    /// <param name="left">The first term in the difference</param>
    /// <param name="right">The second term in the difference.</param>
    /// <returns>A term representing <paramref name="left"/> - <paramref name="right"/>.</returns>
    public static Term operator -(Term left, Term right)
    {
        return left + -1 * right;
    }

    /// <summary>
    /// Constructs a negated term
    /// </summary>
    /// <param name="term">The term to negate</param>
    /// <returns>A term representing <c>-term</c>.</returns>
    public static Term operator -(Term term)
    {
        return -1 * term;
    }
}