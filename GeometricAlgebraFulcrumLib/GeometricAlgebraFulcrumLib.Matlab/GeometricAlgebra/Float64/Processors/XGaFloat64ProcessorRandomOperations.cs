using System;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors
{
    public partial class XGaFloat64Processor
    {
        
        public XGaFloat64RandomComposer CreateXGaRandomComposer(int vSpaceDimensions)
        {
            return new XGaFloat64RandomComposer(this, vSpaceDimensions);
        }

        
        public XGaFloat64RandomComposer CreateXGaRandomComposer(int vSpaceDimensions, int seed)
        {
            return new XGaFloat64RandomComposer(this, vSpaceDimensions, seed);
        }

        
        public XGaFloat64RandomComposer CreateXGaRandomComposer(int vSpaceDimensions, Random randomGenerator)
        {
            return new XGaFloat64RandomComposer(this, vSpaceDimensions, randomGenerator);
        }



    }
}
