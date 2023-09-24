using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space4D;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.Space4D;

public interface ILinFloat64UnilinearMap4D :
    ILinearElement
{
    bool SwapsHandedness { get; }
    
    bool IsIdentity();

    bool IsNearIdentity(double epsilon = 1e-12d);
    
    ILinFloat64UnilinearMap4D GetInverseMap();
 
    Float64Vector4D MapBasisVector(int index);

    Float64Vector4D MapVector(IFloat64Vector4D vector);
}