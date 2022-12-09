using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using MathNet.Numerics.LinearAlgebra;
using NumericalGeometryLib.BasicMath.Tuples.Mutable;

namespace NumericalGeometryLib.BasicMath.Maps.SpaceND.Reflection
{
    public sealed class VectorReflection :
        ILinearMap
    {
        public Float64Tuple ReflectionVector { get; }

        public int Dimensions { get; }

        public bool SwapsHandedness 
            => true;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorReflection(int dimensions, Float64Tuple vector)
        {
            Debug.Assert(vector.GetVectorNormSquared().IsNearOne());

            Dimensions = dimensions;
            ReflectionVector = vector;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return ReflectionVector.IsValid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsIdentity()
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearIdentity(double epsilon = 1E-12)
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple MapVectorBasis(int basisIndex)
        {
            Debug.Assert(basisIndex >= 0 && basisIndex < Dimensions);

            var y = new double[Dimensions];

            var u = ReflectionVector.ScalarArray;
            var s = 2d * u[basisIndex];

            for (var i = 0; i < Dimensions; i++)
                y[i] = s * u[i];

            y[basisIndex] -= 1d;

            return Float64Tuple.Create(y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple MapVector(Float64Tuple vector)
        {
            var y = new double[Dimensions];

            var x = vector.ScalarArray;
            var u = ReflectionVector.ScalarArray;
            var s = 2d * x.VectorDot(u);

            for (var i = 0; i < Dimensions; i++)
                y[i] = s * u[i] - x[i];

            return Float64Tuple.Create(y);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix<double> GetMatrix()
        {
            var columnList =
                Dimensions
                    .GetRange()
                    .Select(i => MapVectorBasis(i).ScalarArray);

            return Matrix<double>
                .Build
                .DenseOfColumnArrays(columnList);
        }

        public double[,] GetArray()
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
        public VectorReflection GetVectorReflectionInverse()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinearMap GetLinearMapInverse()
        {
            return GetVectorReflectionInverse();
        }
    }
}
