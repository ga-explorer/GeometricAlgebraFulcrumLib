using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.LinearMaps.Outermorphisms
{
    public static class XGaFloat64OutermorphismComposerUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64LinearMapOutermorphism CreateOutermorphism(this XGaFloat64Processor metric, LinFloat64UnilinearMap linearMap)
        {
            return new XGaFloat64LinearMapOutermorphism(metric, linearMap);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64LinearMapOutermorphism ToOutermorphism(this LinFloat64UnilinearMap linearMap, XGaFloat64Processor metric)
        {
            return new XGaFloat64LinearMapOutermorphism(metric, linearMap);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64DiagonalOutermorphism CreateDiagonalAutomorphism(this XGaFloat64Vector diagonalVector)
        {
            return new XGaFloat64DiagonalOutermorphism(diagonalVector);
        }
        
        
    }
}