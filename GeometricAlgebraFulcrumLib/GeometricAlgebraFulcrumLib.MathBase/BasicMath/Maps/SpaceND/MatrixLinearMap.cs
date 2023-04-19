//using System.Runtime.CompilerServices;
//using DataStructuresLib.Extensions;
//using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Arrays.Float64;
//using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Matrices;
//using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
//using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Mutable;
//using MathNet.Numerics.LinearAlgebra;

//namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps.SpaceND
//{
//    public sealed class MatrixLinearOperator :
//        ILinearMap
//    {
//        private readonly double[,] _mapArray;

//        public int VSpaceDimensions 
//            => _mapArray.GetLength(0);

//        public double Determinant { get; }

//        public bool SwapsHandedness 
//            => Determinant < 0;


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        private MatrixLinearOperator(double[,] mapArray)
//        {
//            _mapArray = mapArray;
//            Determinant = _mapArray.ToMatrix().Determinant();
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public bool IsValid()
//        {
//            return _mapArray.GetItems().All(s => s.IsValid());
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public Float64Tuple MapBasisVector(int basisIndex)
//        {
//            return Float64Tuple.Create(
//                _mapArray.ColumnToArray1D(basisIndex)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public Float64Tuple MapVector(Float64Tuple vector)
//        {
//            return Float64Tuple.Create(
//                _mapArray.MatrixProduct(vector.ScalarArray)
//            );
//        }
    
//        public double[,] ToArray()
//        {
//            var array = new double[VSpaceDimensions, VSpaceDimensions];

//            for (var j = 0; j < VSpaceDimensions; j++)
//            for (var i = 0; i < VSpaceDimensions; i++) 
//                array[i, j] = _mapArray[i, j];

//            return array;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public Matrix<double> ToMatrix()
//        {
//            return _mapArray.ToMatrix();
//        }
    
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public ILinearMap GetInverseMap()
//        {
//            return new MatrixLinearOperator(
//                _mapArray.ToMatrix().Inverse().ToArray()
//            );
//        }

//        public bool IsIdentity()
//        {
//            for (var i = 0; i < VSpaceDimensions; i++)
//            {
//                for (var j = 0; j < i; j++)
//                    if (!_mapArray[i, j].IsZero())
//                        return false;

//                if (!_mapArray[i, i].IsOne())
//                    return false;

//                for (var j = i + 1; j < VSpaceDimensions; j++)
//                    if (!_mapArray[i, j].IsZero())
//                        return false;
//            }

//            return true;
//        }

//        public bool IsNearIdentity(double epsilon = 1E-12)
//        {
//            for (var i = 0; i < VSpaceDimensions; i++)
//            {
//                for (var j = 0; j < i; j++)
//                    if (!_mapArray[i, j].IsNearZero(epsilon))
//                        return false;

//                if (!_mapArray[i, i].IsNearOne(epsilon))
//                    return false;

//                for (var j = i + 1; j < VSpaceDimensions; j++)
//                    if (!_mapArray[i, j].IsNearZero(epsilon))
//                        return false;
//            }

//            return true;
//        }
//    }
//}