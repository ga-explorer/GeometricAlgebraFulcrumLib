using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND;

public interface ILinFloat64UnilinearMap :
    IFloat64LinearAlgebraElement
{
    bool SwapsHandedness { get; }

    bool IsIdentity();

    bool IsReflection();

    bool IsNearIdentity(double epsilon = 1e-12d);

    bool IsNearReflection(double epsilon = 1e-12d);

    ILinFloat64UnilinearMap GetInverseMap();

    LinFloat64Vector MapBasisVector(int index);

    LinFloat64Vector MapVector(LinFloat64Vector vector);

    IEnumerable<KeyValuePair<int, LinFloat64Vector>> GetMappedBasisVectors(int vSpaceDimensions);

    double[,] ToArray(int rowCount, int colCount);

    Matrix<double> ToMatrix(int rowCount, int colCount);

    LinFloat64UnilinearMap ToUnilinearMap(int vSpaceDimensions);
}