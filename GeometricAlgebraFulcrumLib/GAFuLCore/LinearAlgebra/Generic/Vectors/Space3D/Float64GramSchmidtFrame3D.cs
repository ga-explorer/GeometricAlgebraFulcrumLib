//using System.Runtime.CompilerServices;
//using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars;
//using MathNet.Numerics.LinearAlgebra;

//namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;

//public class LinGramSchmidtFrame3D<T>
//{
//    public static LinGramSchmidtFrame3D<T> Create(LinVector3D<T> v1, LinVector3D<T> v2, LinVector3D<T> v3)
//    {
//        var array = new Scalar<T>[,]
//        {
//            { v1.X, v2.X, v3.X },
//            { v1.Y, v2.Y, v3.Y },
//            { v1.Z, v2.Z, v3.Z }
//        };

//        var matrix = Matrix<Scalar<T>>.Build.DenseOfArray(array);

//        var qr = matrix.QR();

//        var n1 = qr.R[0, 0];
//        var n2 = qr.R[1, 1];
//        var n3 = qr.R[2, 2];

//        var e1 = n1 >= 0
//            ? LinVector3D<T>.Create(qr.Q[0, 0], qr.Q[1, 0], qr.Q[2, 0])
//            : LinVector3D<T>.Create(-qr.Q[0, 0], -qr.Q[1, 0], -qr.Q[2, 0]);

//        var e2 = n2 >= 0
//            ? LinVector3D<T>.Create(qr.Q[0, 1], qr.Q[1, 1], qr.Q[2, 1])
//            : LinVector3D<T>.Create(-qr.Q[0, 1], -qr.Q[1, 1], -qr.Q[2, 1]);

//        var e3 = n3 >= 0
//            ? LinVector3D<T>.Create(qr.Q[0, 2], qr.Q[1, 2], qr.Q[2, 2])
//            : LinVector3D<T>.Create(-qr.Q[0, 2], -qr.Q[1, 2], -qr.Q[2, 2]);

//        return new LinGramSchmidtFrame3D<T>(
//            n1.Abs(), n2.Abs(), n3.Abs(),
//            e1, e2, e3
//        );
//    }


//    public IScalarProcessor<T> ScalarProcessor 
//        => UnitDirection1.ScalarProcessor;

//    public Scalar<T> Direction1Norm { get; private set; }

//    public Scalar<T> Direction2Norm { get; private set; }

//    public Scalar<T> Direction3Norm { get; private set; }

//    public LinVector3D<T> UnitDirection1 { get; }

//    public LinVector3D<T> UnitDirection2 { get; }

//    public LinVector3D<T> UnitDirection3 { get; }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    private LinGramSchmidtFrame3D(Scalar<T> direction1Norm, Scalar<T> direction2Norm, Scalar<T> direction3Norm, LinVector3D<T> unitDirection1, LinVector3D<T> unitDirection2, LinVector3D<T> unitDirection3)
//    {
//        Direction1Norm = direction1Norm;
//        Direction2Norm = direction2Norm;
//        Direction3Norm = direction3Norm;

//        UnitDirection1 = unitDirection1;
//        UnitDirection2 = unitDirection2;
//        UnitDirection3 = unitDirection3;
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public LinGramSchmidtFrame3D<T> CleanNorms()
//    {
//        if (Direction1Norm.IsNearZero())
//            Direction1Norm = ScalarProcessor.Zero;

//        if (Direction2Norm.IsNearZero())
//            Direction2Norm = ScalarProcessor.Zero;

//        if (Direction3Norm.IsNearZero())
//            Direction3Norm = ScalarProcessor.Zero;

//        return this;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public LinVector3D<T> GetDirection1()
//    {
//        return Direction1Norm * UnitDirection1;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public LinVector3D<T> GetDirection2()
//    {
//        return Direction2Norm * UnitDirection2;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public LinVector3D<T> GetDirection3()
//    {
//        return Direction3Norm * UnitDirection3;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Scalar<T> GetCurvature12()
//    {
//        return Direction2Norm / Direction1Norm;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Scalar<T> GetCurvature23()
//    {
//        return Direction3Norm / Direction2Norm;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public LinBivector3D<T> GetDarbouxBlade12()
//    {
//        return GetCurvature12() * UnitDirection1.Op(UnitDirection2);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public LinBivector3D<T> GetDarbouxBlade23()
//    {
//        return GetCurvature23() * UnitDirection2.Op(UnitDirection3);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public LinBivector3D<T> GetDarbouxBivector()
//    {
//        return GetDarbouxBlade12() + GetDarbouxBlade23();
//    }
//}