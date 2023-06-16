using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.SpaceND;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.SpaceND;

public interface ILinFloat64UnilinearMap :
    ILinearElement
{
    bool SwapsHandedness { get; }

    bool IsIdentity();

    bool IsReflection();

    bool IsNearIdentity(double epsilon = 1e-12d);

    bool IsNearReflection(double epsilon = 1e-12d);

    ILinFloat64UnilinearMap GetInverseMap();

    Float64Vector MapBasisVector(int index);

    Float64Vector MapVector(Float64Vector vector);

    IEnumerable<KeyValuePair<int, Float64Vector>> GetMappedBasisVectors(int vSpaceDimensions);

    double[,] ToArray(int rowCount, int colCount);

    Matrix<double> ToMatrix(int rowCount, int colCount);

    LinFloat64UnilinearMap ToUnilinearMap(int vSpaceDimensions);
}