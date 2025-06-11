using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.Space3D.Reflection;

public abstract class LinFloat64ReflectionBase3D :
    ILinFloat64UnilinearMap3D
{
    public int VSpaceDimensions
        => 3;

    public abstract bool SwapsHandedness { get; }

    public abstract bool IsValid();

    public abstract bool IsIdentity();

    public abstract bool IsNearIdentity(double zeroEpsilon = 1e-12d);

    public abstract LinFloat64Vector3D MapBasisVector(int basisIndex);

    public abstract LinFloat64Vector3D MapVector(ILinFloat64Vector3D vector);

    public abstract LinFloat64ReflectionBase3D GetReflectionLinearMapInverse();


    
    public ILinFloat64UnilinearMap3D GetInverseMap()
    {
        return GetReflectionLinearMapInverse();
    }

    public abstract LinFloat64HyperPlaneNormalReflectionSequence3D ToHyperPlaneReflectionSequence();
}