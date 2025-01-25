//using System.Runtime.CompilerServices;
//using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
//using GeometricAlgebraFulcrumLib.Algebra.BasicMath.Tuples.Mutable;
//using MathNet.Numerics.LinearAlgebra;

//namespace GeometricAlgebraFulcrumLib.Algebra.BasicMath.Maps.SpaceND.Reflection
//{
//    public abstract class ReflectionLinearMap :
//        ILinearMap
//    {
//        public abstract int VSpaceDimensions { get; }

//        public abstract bool SwapsHandedness { get; }

//        public abstract bool IsValid();

//        public abstract bool IsIdentity();

//        public abstract bool IsNearIdentity(double zeroEpsilon = 1e-12d);

//        public abstract Float64Tuple MapBasisVector(int basisIndex);

//        public abstract Float64Tuple MapVector(Float64Tuple vector);

//        public abstract ReflectionLinearMap GetReflectionLinearMapInverse();


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

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public ILinearMap GetInverseMap()
//        {
//            return GetReflectionLinearMapInverse();
//        }

//        public abstract HyperPlaneNormalReflectionSequence ToHyperPlaneReflectionSequence();
//    }
//}