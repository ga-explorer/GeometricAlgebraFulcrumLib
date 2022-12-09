using MathNet.Numerics.LinearAlgebra;
using NumericalGeometryLib.BasicMath.Tuples.Mutable;

namespace NumericalGeometryLib.BasicMath.Maps.SpaceND;

public interface ILinearMap :
    IGeometricElement
{
    int Dimensions { get; }

    bool SwapsHandedness { get; }
    
    Float64Tuple MapVectorBasis(int basisIndex);

    Float64Tuple MapVector(Float64Tuple vector);
    
    Matrix<double> GetMatrix();

    double[,] GetArray();

    ILinearMap GetLinearMapInverse();

    bool IsIdentity();

    bool IsNearIdentity(double epsilon = 1e-12d);
}