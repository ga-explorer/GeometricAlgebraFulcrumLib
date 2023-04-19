using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps;

public interface ILinFloat64UnilinearMap :
    IGeometricElement
{
    int VSpaceDimensions { get; }

    bool SwapsHandedness { get; }
    
    bool IsIdentity();

    bool IsNearIdentity(double epsilon = 1e-12d);
    
    ILinFloat64UnilinearMap GetInverseMap();
 
    LinFloat64Vector MapBasisVector(int index);

    LinFloat64Vector MapVector(LinFloat64Vector vector);

    IEnumerable<KeyValuePair<int, LinFloat64Vector>> GetMappedBasisVectors(int vSpaceDimensions);

    double[,] ToArray(int rowCount, int colCount);
    
    Matrix<double> ToMatrix(int rowCount, int colCount);

    LinFloat64UnilinearMap ToUnilinearMap(int vSpaceDimensions);
}