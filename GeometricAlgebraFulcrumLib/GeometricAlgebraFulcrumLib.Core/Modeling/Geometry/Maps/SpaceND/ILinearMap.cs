//using GeometricAlgebraFulcrumLib.Algebra.BasicMath.Tuples.Mutable;
//using MathNet.Numerics.LinearAlgebra;

//namespace GeometricAlgebraFulcrumLib.Algebra.BasicMath.Maps.SpaceND
//{
//    public interface ILinearMap :
//        IGeometricElement
//    {
//        int VSpaceDimensions { get; }

//        bool SwapsHandedness { get; }
    
//        bool IsIdentity();

//        bool IsNearIdentity(double epsilon = 1e-12d);

//        Float64Tuple MapBasisVector(int basisIndex);

//        Float64Tuple MapVector(Float64Tuple vector);
    
//        Matrix<double> ToMatrix();

//        double[,] ToArray();

//        ILinearMap GetInverseMap();
//    }
//}