﻿namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.AutoDiff;

/// <summary>
/// A visitor for the terms that has a result from its computations
/// </summary>
/// <typeparam name="TResult">The type of the computation results</typeparam>
public interface ITermVisitor<out TResult>
{
    /// <summary>
    /// Computes a value for a constant term.
    /// </summary>
    /// <param name="constant">The input term.</param>
    /// <returns>The result of the computation.</returns>
    TResult Visit(Constant constant);

    /// <summary>
    /// Computes a value for a zero term.
    /// </summary>
    /// <param name="zero">The input term.</param>
    /// <returns>The result of the computation.</returns>
    TResult Visit(Zero zero);

    /// <summary>
    /// Computes a value for a power term.
    /// </summary>
    /// <param name="power">The input term.</param>
    /// <returns>The result of the computation.</returns>
    TResult Visit(ConstPower power);

    /// <summary>
    /// Computes a value for a power term.
    /// </summary>
    /// <param name="power">The input term.</param>
    /// <returns>The result of the computation.</returns>
    TResult Visit(TermPower power);

    /// <summary>
    /// Computes a value for a product term.
    /// </summary>
    /// <param name="product">The input term.</param>
    /// <returns>The result of the computation.</returns>
    TResult Visit(Product product);

    /// <summary>
    /// Computes a value for a sum term.
    /// </summary>
    /// <param name="sum">The input term.</param>
    /// <returns>The result of the computation.</returns>
    TResult Visit(Sum sum);

    /// <summary>
    /// Computes a value for a variable term.
    /// </summary>
    /// <param name="variable">The input term.</param>
    /// <returns>The result of the computation.</returns>
    TResult Visit(Variable variable);

    /// <summary>
    /// Computes a value for a logarithm term.
    /// </summary>
    /// <param name="log">The input term.</param>
    /// <returns>The result of the computation.</returns>
    TResult Visit(Log log);

    /// <summary>
    /// Computes a value for an exponential function term.
    /// </summary>
    /// <param name="exp">The input term.</param>
    /// <returns>The result of the computation.</returns>
    TResult Visit(Exp exp);

    /// <summary>
    /// Computes a value for an unary function
    /// </summary>
    /// <param name="func">The unary function</param>
    /// <returns>The result of the computation</returns>
    TResult Visit(UnaryFunc func);

    /// <summary>
    /// Computes a value for a binary function
    /// </summary>
    /// <param name="func">The binary function</param>
    /// <returns>The result of the computation</returns>
    TResult Visit(BinaryFunc func);

    /// <summary>
    /// Computes a value for a n-ary function
    /// </summary>
    /// <param name="func">The n-ary function</param>
    /// <returns>The result of the computation</returns>
    TResult Visit(NaryFunc func);
}