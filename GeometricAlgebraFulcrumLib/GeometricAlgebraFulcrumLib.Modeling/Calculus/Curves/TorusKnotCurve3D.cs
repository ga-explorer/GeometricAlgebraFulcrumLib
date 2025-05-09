using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Calculus.Curves;

/// <summary>
/// See https://en.wikipedia.org/wiki/Torus
/// and https://en.wikipedia.org/wiki/Torus_knot
/// </summary>
public class TorusKnotCurve3D :
    Float64DifferentialPath3D
{
    public int PValue { get; }

    public int QValue { get; }

    public double TubeRadius { get; }

    public double RingRadius { get; }


    protected TorusKnotCurve3D(ITriplet<DifferentialFunction> components) 
        : base(Float64ScalarRange.ZeroToTwoPi, true, components)
    {
    }

    protected TorusKnotCurve3D(ITriplet<DifferentialFunction> components, DifferentialFunction tangentNorm) 
        : base(Float64ScalarRange.ZeroToTwoPi, true, components, tangentNorm)
    {
    }
}