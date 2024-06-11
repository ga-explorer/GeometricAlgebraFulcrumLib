using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Calculus.Curves;

/// <summary>
/// See https://en.wikipedia.org/wiki/Torus
/// and https://en.wikipedia.org/wiki/Torus_knot
/// </summary>
public class TorusKnotCurve3D :
    DifferentialCurve3D
{
    public int PValue { get; }

    public int QValue { get; }

    public double TubeRadius { get; }

    public double RingRadius { get; }


    protected TorusKnotCurve3D(ITriplet<DifferentialFunction> components) 
        : base(components)
    {
    }

    protected TorusKnotCurve3D(ITriplet<DifferentialFunction> components, DifferentialFunction tangentNorm) 
        : base(components, tangentNorm)
    {
    }
}