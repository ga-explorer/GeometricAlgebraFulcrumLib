//using System.Runtime.CompilerServices;
//using DataStructuresLib.BitManipulation;
//using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
//using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Mutable;
//using MathNet.Numerics.LinearAlgebra;

//namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps.SpaceND.Scaling
//{
//    /// <summary>
//    /// This class represents the most basic kind of linear operations:
//    /// Scaling by a factor in a given direction.
//    /// </summary>
//    public abstract class DirectionalScalingLinearMap :
//        IDirectionalScalingLinearMap
//    {
//        public abstract int VSpaceDimensions { get; }

//        public bool SwapsHandedness 
//            => ScalingFactor < 0;

//        public abstract double ScalingFactor { get; }

//        public abstract Float64Tuple ScalingVector { get; }

    
//        public abstract bool IsValid();
    
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public bool IsIdentity()
//        {
//            return ScalingFactor.IsOne();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public bool IsNearIdentity(double epsilon = 1E-12)
//        {
//            return ScalingFactor.IsNearOne(epsilon);
//        }

//        public abstract Float64Tuple MapBasisVector(int basisIndex);

//        public abstract Float64Tuple MapVector(Float64Tuple vector);
    

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public virtual Matrix<double> ToMatrix()
//        {
//            var columnList =
//                VSpaceDimensions
//                    .GetRange()
//                    .Select(i => MapBasisVector(i).ScalarArray);

//            return Matrix<double>
//                .Build
//                .DenseOfColumnArrays(columnList);
//        }

//        public virtual double[,] ToArray()
//        {
//            var array = new double[VSpaceDimensions, VSpaceDimensions];

//            for (var j = 0; j < VSpaceDimensions; j++)
//            {
//                var columnVector = MapBasisVector(j).ScalarArray;

//                for (var i = 0; i < VSpaceDimensions; i++) 
//                    array[i, j] = columnVector[i];
//            }

//            return array;
//        }

//        public abstract IDirectionalScalingLinearMap GetDirectionalScalingInverse();

//        public abstract VectorDirectionalScaling ToVectorDirectionalScaling();

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public ILinearMap GetInverseMap()
//        {
//            return GetDirectionalScalingInverse();
//        }
//    }
//}