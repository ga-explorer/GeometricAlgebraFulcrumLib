using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Extensions;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.SpaceND.Scaling;

/// <summary>
/// This class represents the most basic kind of linear operations:
/// Scaling by a factor in a given direction.
/// </summary>
public abstract class LinFloat64DirectionalScalingLinearMap :
    ILinFloat64DirectionalScalingLinearMap
{
    public abstract int VSpaceDimensions { get; }

    public bool SwapsHandedness
        => ScalingFactor < 0;

    public abstract double ScalingFactor { get; }

    public abstract LinFloat64Vector ScalingVector { get; }


    public abstract bool IsValid();

    
    public bool IsIdentity()
    {
        return ScalingFactor.IsOne();
    }

    public bool IsReflection()
    {
        throw new NotImplementedException();
    }

    
    public bool IsNearIdentity(double zeroEpsilon = 1E-12)
    {
        return ScalingFactor.IsNearOne(zeroEpsilon);
    }

    public bool IsNearReflection(double zeroEpsilon = 1E-12)
    {
        throw new NotImplementedException();
    }

    public abstract LinFloat64Vector MapBasisVector(int basisIndex);

    public abstract LinFloat64Vector MapVector(LinFloat64Vector vector);

    
    public IEnumerable<KeyValuePair<int, LinFloat64Vector>> GetMappedBasisVectors(int vSpaceDimensions)
    {
        return vSpaceDimensions
            .GetRange(i =>
                new KeyValuePair<int, LinFloat64Vector>(
                    i,
                    MapBasisVector(i)
                )
            ).Where(p => !p.Value.IsZero);
    }


    
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

    
    public virtual LinFloat64UnilinearMap ToUnilinearMap(int vSpaceDimensions)
    {
        return vSpaceDimensions.CreateLinUnilinearMap(MapBasisVector);
    }

    public virtual double[,] ToArray(int rowCount, int colCount)
    {
        var array = new double[rowCount, colCount];

        for (var j = 0; j < colCount; j++)
        {
            var columnVector = MapBasisVector(j);

            foreach (var (i, scalar) in columnVector.ToTuples())
            {
                if (i >= rowCount)
                    throw new InvalidOperationException();

                array[i, j] = scalar;
            }
        }

        return array;
    }

    public abstract ILinFloat64DirectionalScalingLinearMap GetDirectionalScalingInverse();

    public abstract LinFloat64VectorDirectionalScaling ToVectorDirectionalScaling();

    
    public ILinFloat64UnilinearMap GetInverseMap()
    {
        return GetDirectionalScalingInverse();
    }
}