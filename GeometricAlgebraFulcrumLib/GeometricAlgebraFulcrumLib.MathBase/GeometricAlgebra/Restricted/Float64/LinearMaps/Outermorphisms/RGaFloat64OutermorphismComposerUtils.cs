using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.LinearMaps.Outermorphisms
{
    public static class RGaFloat64OutermorphismComposerUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64LinearMapOutermorphism CreateOutermorphism(this RGaFloat64Processor metric, LinFloat64UnilinearMap linearMap)
        {
            return new RGaFloat64LinearMapOutermorphism(metric, linearMap);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64LinearMapOutermorphism ToOutermorphism(this LinFloat64UnilinearMap linearMap, RGaFloat64Processor metric)
        {
            return new RGaFloat64LinearMapOutermorphism(metric, linearMap);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64DiagonalOutermorphism CreateDiagonalAutomorphism(this RGaFloat64Vector diagonalVector)
        {
            return new RGaFloat64DiagonalOutermorphism(diagonalVector);
        }
        
        
    }
}