using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Frames;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic.LinearMaps;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic.Matrices;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Mathematica.Applications.GaPoT
{
    public static class GaPoTUtils
    {
        public static LinUnilinearMap<T> CreateClarkeRotationMap<T>(this IScalarProcessor<T> processor, int vectorsCount)
        {
            return processor.CreateLinUnilinearMap(
                processor
                .CreateClarkeRotationArray(vectorsCount)
                .ColumnsToLinVectors(processor)
            );
        }

        
        /// <summary>
        /// See the paper "Generalized Clarke Components for Poly-phase Networks", 1969
        /// </summary>
        /// <param name="processor"></param>
        /// <param name="vectorsCount"></param>
        /// <returns></returns>
        public static XGaVectorFrame<T> CreateClarkeRotationFrame<T>(this XGaProcessor<T> processor, int vectorsCount)
        {
            return XGaVectorFrameSpecs
                .CreateUnitBasisSpecs()
                .CreateVectorFrame(
                    processor
                        .ScalarProcessor
                        .CreateClarkeRotationArray(vectorsCount)
                        .ColumnsToXGaVectors(processor)
                );
        }
        
        public static XGaFloat64LinearMapOutermorphism CreateClarkeRotationMap(this XGaFloat64Processor processor, int vectorsCount)
        {
            var clarkeMapArray =
                Float64ArrayUtils.CreateClarkeRotationArray(vectorsCount);

            var basisVectorImagesDictionary = 
                new Dictionary<int, Float64Vector>();

            for (var i = 0; i < vectorsCount; i++)
                basisVectorImagesDictionary.Add(
                    i, 
                    clarkeMapArray.ColumnToLinVector(i)
                );

            return basisVectorImagesDictionary
                .ToLinUnilinearMap()
                .ToOutermorphism(processor);
        }

        public static XGaLinearMapOutermorphism<T> CreateClarkeRotationMap<T>(this XGaProcessor<T> processor, int vectorsCount)
        {
            var scalarProcessor = processor.ScalarProcessor;

            var clarkeMapArray =
                scalarProcessor.CreateClarkeRotationArray(vectorsCount);

            var basisVectorImagesDictionary = 
                new Dictionary<int, LinVector<T>>();

            for (var i = 0; i < vectorsCount; i++)
                basisVectorImagesDictionary.Add(
                    i, 
                    clarkeMapArray.ColumnToLinVector(scalarProcessor, i)
                );

            return scalarProcessor.CreateLinUnilinearMap(
                basisVectorImagesDictionary
            ).ToOutermorphism(processor);
        }
        
        public static XGaPureRotor<T> CreateSimpleKirchhoffRotor<T>(this XGaProcessor<T> processor, int vSpaceDimensions)
        {
            var v1 = 
                processor.CreateSymmetricUnitVector(vSpaceDimensions);

            var v2 = processor.CreateVector(
                vSpaceDimensions - 1
            );

            return v2.CreatePureRotor(v1);
        }
    }
}
