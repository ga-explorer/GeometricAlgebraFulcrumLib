using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Geometry.Graphics.Space2D;
using GeometricAlgebraFulcrumLib.Geometry.Graphics.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class GraphicsFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EuclideanVector2D ToEuclideanVector2D(this IFloat64Tuple2D v)
        {
            return v is EuclideanVector2D ev
                ? ev
                : new EuclideanVector2D(v.X, v.Y);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EuclideanVector2D ToEuclideanVector2D(this VectorStorage<double> vectorStorage)
        {
            var x = vectorStorage.GetTermScalarByIndex(0);
            var y = vectorStorage.GetTermScalarByIndex(1);

            return new EuclideanVector2D(x, y);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EuclideanVector3D ToEuclideanVector3D(this IFloat64Tuple3D v)
        {
            return v is EuclideanVector3D ev
                ? ev
                : new EuclideanVector3D(v.X, v.Y, v.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EuclideanVector3D ToEuclideanVector3D(this VectorStorage<double> vectorStorage)
        {
            var x = vectorStorage.GetTermScalarByIndex(0);
            var y = vectorStorage.GetTermScalarByIndex(1);
            var z = vectorStorage.GetTermScalarByIndex(2);

            return new EuclideanVector3D(x, y, z);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EuclideanPoint2D ToEuclideanPoint2D(this IFloat64Tuple2D v)
        {
            return v is EuclideanPoint2D ev
                ? ev
                : new EuclideanPoint2D(v.X, v.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EuclideanPoint2D ToEuclideanPoint2D(this VectorStorage<double> vectorStorage)
        {
            var x = vectorStorage.GetTermScalarByIndex(0);
            var y = vectorStorage.GetTermScalarByIndex(1);

            return new EuclideanPoint2D(x, y);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EuclideanPoint3D ToEuclideanPoint3D(this IFloat64Tuple3D v)
        {
            return v is EuclideanPoint3D ev
                ? ev
                : new EuclideanPoint3D(v.X, v.Y, v.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EuclideanPoint3D ToEuclideanPoint3D(this VectorStorage<double> vectorStorage)
        {
            var x = vectorStorage.GetTermScalarByIndex(0);
            var y = vectorStorage.GetTermScalarByIndex(1);
            var z = vectorStorage.GetTermScalarByIndex(2);

            return new EuclideanPoint3D(x, y, z);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Vector ToVector(this IFloat64Tuple2D v)
        {
            return GraphicsUtils.GeometricProcessor.CreateVector(v.X, v.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Vector ToVector(this IFloat64Tuple3D v)
        {
            return GraphicsUtils.GeometricProcessor.CreateVector(v.X, v.Y, v.Z);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Vector ToVector(this IFloat64Tuple4D v)
        {
            return GraphicsUtils.GeometricProcessor.CreateVector(v.X, v.Y, v.Z, v.W);
        }
    }
}