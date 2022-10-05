using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Geometry.Conformal;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class GaFuLConformalGeometryUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetDistance<T>(this ConformalIpnsVectorBase<T> ipnsVector, GaVector<T> positionVector)
        {
            var processor = ipnsVector.ConformalProcessor;

            var distance = processor.Sp(
                ipnsVector.GetUnitWeightVectorStorage(),
                processor.CreateIpnsPoint(positionVector).VectorStorage
            );

            return ipnsVector.HasZeroWeight() 
                ? distance 
                : processor.Times(-2, distance);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetDistance<T>(this ConformalIpnsVectorBase<T> ipnsVector1, ConformalIpnsVectorBase<T> ipnsVector2)
        {
            var processor = ipnsVector1.ConformalProcessor;

            var distance = processor.Sp(
                ipnsVector1.GetUnitWeightVectorStorage(),
                ipnsVector2.GetUnitWeightVectorStorage()
            );

            return ipnsVector1.HasZeroWeight() || ipnsVector2.HasZeroWeight() 
                ? distance 
                : processor.Times(-2, distance);
        }
    }
}