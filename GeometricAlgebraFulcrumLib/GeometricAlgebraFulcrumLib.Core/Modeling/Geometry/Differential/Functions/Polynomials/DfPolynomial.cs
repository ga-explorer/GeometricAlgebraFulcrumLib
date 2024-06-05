using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Differential.Functions.Polynomials;

public abstract class DfPolynomial :
    DifferentialCustomFunction
{
    public int Degree
        => Math.Max(ScalarCoefficients.LastIndex, 0);

    public LinFloat64Vector ScalarCoefficients { get; }

    public bool IsZero
        => ScalarCoefficients.IsZero;

    public override bool IsConstant
        => ScalarCoefficients.IsZero ||
           ScalarCoefficients.Count == 1 && ScalarCoefficients.FirstIndex == 0;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected DfPolynomial(LinFloat64Vector scalarCoefficients)
        : base(false)
    {
        ScalarCoefficients = scalarCoefficients;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Tuple<bool, DifferentialFunction> TrySimplify()
    {
        return new Tuple<bool, DifferentialFunction>(false, this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override DifferentialFunction Simplify()
    {
        return this;
    }
}