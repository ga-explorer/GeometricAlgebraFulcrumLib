using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.Space3D;

public interface ILinFloat64UnilinearMap3D :
    ILinearElement
{
    bool SwapsHandedness { get; }
    
    bool IsIdentity();

    bool IsNearIdentity(double epsilon = 1e-12d);
    
    ILinFloat64UnilinearMap3D GetInverseMap();
 
    Float64Vector3D MapBasisVector(int index);

    Float64Vector3D MapVector(IFloat64Vector3D vector);
}