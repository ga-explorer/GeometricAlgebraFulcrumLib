using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space4D;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.Space4D.Reflection;

public abstract class LinFloat64ReflectionBase4D :
    ILinFloat64UnilinearMap4D
{
    public int VSpaceDimensions
        => 3;

    public abstract bool SwapsHandedness { get; }

    public abstract bool IsValid();

    public abstract bool IsIdentity();

    public abstract bool IsNearIdentity(double zeroEpsilon = 1e-12d);

    public abstract LinFloat64Vector4D MapBasisVector(int basisIndex);

    public abstract LinFloat64Vector4D MapVector(ILinFloat64Vector4D vector);

    public abstract LinFloat64ReflectionBase4D GetReflectionLinearMapInverse();


    
    public ILinFloat64UnilinearMap4D GetInverseMap()
    {
        return GetReflectionLinearMapInverse();
    }

    public abstract LinFloat64HyperPlaneNormalReflectionSequence4D ToHyperPlaneReflectionSequence();
}