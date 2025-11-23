using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND;

public sealed class LinFloat64MatrixLinearMap :
    ILinFloat64UnilinearMap
{
    private readonly Matrix<double> _mapArray;

    public int VSpaceDimensions
        => _mapArray.RowCount;

    public double Determinant { get; }

    public bool SwapsHandedness
        => Determinant < 0;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64MatrixLinearMap(Matrix<double> mapArray)
    {
        _mapArray = mapArray;
        Determinant = _mapArray.Determinant();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return _mapArray.Enumerate().All(s => s.IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector MapBasisVector(int basisIndex)
    {
        return _mapArray.Column(basisIndex).ToLinFloat64Vector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector MapVector(LinFloat64Vector vector)
    {
        return _mapArray.Multiply(vector);
    }

    public IEnumerable<KeyValuePair<int, LinFloat64Vector>> GetMappedBasisVectors(int vSpaceDimensions)
    {
        throw new NotImplementedException();
    }

    public Matrix<double> ToArray(int rowCount, int colCount)
    {
        throw new NotImplementedException();
    }

    public Matrix<double> ToMatrix(int rowCount, int colCount)
    {
        throw new NotImplementedException();
    }

    public LinFloat64UnilinearMap ToUnilinearMap(int vSpaceDimensions)
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[,] ToArray2D()
    {
        return _mapArray.ToArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Matrix<double> ToMatrix()
    {
        return _mapArray.Clone();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsReflection()
    {
        return _mapArray.IsOrthogonal(0);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearReflection(double tolerance = Float64Utils.DefaultTolerance)
    {
        return _mapArray.IsOrthogonal(tolerance);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinFloat64UnilinearMap GetInverseMap()
    {
        return new LinFloat64MatrixLinearMap(
            _mapArray.Inverse()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsIdentity()
    {
        return _mapArray.IsNearIdentity(0);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearIdentity(double tolerance = Float64Utils.DefaultTolerance)
    {
        return _mapArray.IsNearIdentity(tolerance);
    }
}