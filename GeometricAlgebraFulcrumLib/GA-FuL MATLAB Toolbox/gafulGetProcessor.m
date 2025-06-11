function processor = gafulGetProcessor(negativeCount, zeroCount)
    processor = GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors.XGaFloat64Processor.Create(negativeCount, zeroCount);
end