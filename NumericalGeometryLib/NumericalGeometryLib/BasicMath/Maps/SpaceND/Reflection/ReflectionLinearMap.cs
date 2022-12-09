using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using MathNet.Numerics.LinearAlgebra;
using NumericalGeometryLib.BasicMath.Tuples.Mutable;

namespace NumericalGeometryLib.BasicMath.Maps.SpaceND.Reflection;

public abstract class ReflectionLinearMap :
    ILinearMap
{
    public abstract int Dimensions { get; }

    public abstract bool SwapsHandedness { get; }

    public abstract bool IsValid();

    public abstract bool IsIdentity();

    public abstract bool IsNearIdentity(double epsilon = 1e-12d);

    public abstract Float64Tuple MapVectorBasis(int basisIndex);

    public abstract Float64Tuple MapVector(Float64Tuple vector);

    public abstract ReflectionLinearMap GetReflectionLinearMapInverse();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual Matrix<double> GetMatrix()
    {
        var columnList =
            Dimensions
                .GetRange()
                .Select(i => MapVectorBasis(i).ScalarArray);

        return Matrix<double>
            .Build
            .DenseOfColumnArrays(columnList);
    }

    public virtual double[,] GetArray()
    {
        var array = new double[Dimensions, Dimensions];

        for (var j = 0; j < Dimensions; j++)
        {
            var columnVector = MapVectorBasis(j).ScalarArray;

            for (var i = 0; i < Dimensions; i++) 
                array[i, j] = columnVector[i];
        }

        return array;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinearMap GetLinearMapInverse()
    {
        return GetReflectionLinearMapInverse();
    }

    public abstract HyperPlaneNormalReflectionSequence ToHyperPlaneReflectionSequence();
}