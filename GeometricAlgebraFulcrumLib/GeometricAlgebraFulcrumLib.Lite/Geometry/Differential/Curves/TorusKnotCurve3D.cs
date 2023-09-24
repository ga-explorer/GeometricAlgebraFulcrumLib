using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.Functions;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.Curves;

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