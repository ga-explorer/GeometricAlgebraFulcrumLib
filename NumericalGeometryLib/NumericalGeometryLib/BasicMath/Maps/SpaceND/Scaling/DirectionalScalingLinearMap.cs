using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using MathNet.Numerics.LinearAlgebra;
using NumericalGeometryLib.BasicMath.Tuples.Mutable;

namespace NumericalGeometryLib.BasicMath.Maps.SpaceND.Scaling;

/// <summary>
/// This class represents the most basic kind of linear operations:
/// Scaling by a factor in a given direction.
/// </summary>
public abstract class DirectionalScalingLinearMap :
    IDirectionalScalingLinearMap
{
    public abstract int Dimensions { get; }

    public bool SwapsHandedness 
        => ScalingFactor < 0;

    public abstract double ScalingFactor { get; }

    public abstract Float64Tuple ScalingVector { get; }

    
    public abstract bool IsValid();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsIdentity()
    {
        return ScalingFactor.IsExactOne();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearIdentity(double epsilon = 1E-12)
    {
        return ScalingFactor.IsNearOne(epsilon);
    }

    public abstract Float64Tuple MapVectorBasis(int basisIndex);

    public abstract Float64Tuple MapVector(Float64Tuple vector);
    

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

    public abstract IDirectionalScalingLinearMap GetDirectionalScalingInverse();

    public abstract VectorDirectionalScaling ToVectorDirectionalScaling();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinearMap GetLinearMapInverse()
    {
        return GetDirectionalScalingInverse();
    }
}