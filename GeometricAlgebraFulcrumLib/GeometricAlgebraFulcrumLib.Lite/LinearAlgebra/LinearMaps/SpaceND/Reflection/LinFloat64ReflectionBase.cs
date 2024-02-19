using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.SpaceND.Reflection;

public abstract class LinFloat64ReflectionBase :
    ILinFloat64UnilinearMap
{
    public abstract int VSpaceDimensions { get; }

    public abstract bool SwapsHandedness { get; }

    public abstract bool IsValid();

    public abstract bool IsIdentity();
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsReflection()
    {
        return true;
    }

    public abstract bool IsNearIdentity(double epsilon = 1e-12d);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearReflection(double epsilon = 1E-12)
    {
        return true;
    }

    public abstract Float64Vector MapBasisVector(int basisIndex);

    public abstract Float64Vector MapVector(Float64Vector vector);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<int, Float64Vector>> GetMappedBasisVectors(int vSpaceDimensions)
    {
        return vSpaceDimensions
            .GetRange()
            .Select(i => new KeyValuePair<int, Float64Vector>(i, MapBasisVector(i)))
            .Where(pair => !pair.Value.IsZero);
    }

    public abstract LinFloat64ReflectionBase GetReflectionLinearMapInverse();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual Matrix<double> ToMatrix(int rowCount, int colCount)
    {
        var columnList =
            colCount
                .GetRange()
                .Select(i => MapBasisVector(i).ToArray(rowCount));

        return Matrix<double>
            .Build
            .DenseOfColumnArrays(columnList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64UnilinearMap ToUnilinearMap(int vSpaceDimensions)
    {
        return vSpaceDimensions.CreateLinUnilinearMap(MapBasisVector);
    }

    public virtual double[,] ToArray(int rowCount, int colCount)
    {
        var array = new double[VSpaceDimensions, VSpaceDimensions];

        for (var j = 0; j < VSpaceDimensions; j++)
        {
            var columnVector = MapBasisVector(j);

            if (columnVector.IsZero)
                continue;

            foreach (var (i, s) in columnVector)
                array[i, j] = s;
        }

        return array;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinFloat64UnilinearMap GetInverseMap()
    {
        return GetReflectionLinearMapInverse();
    }

    public abstract LinFloat64HyperPlaneNormalReflectionSequence ToHyperPlaneReflectionSequence();
}