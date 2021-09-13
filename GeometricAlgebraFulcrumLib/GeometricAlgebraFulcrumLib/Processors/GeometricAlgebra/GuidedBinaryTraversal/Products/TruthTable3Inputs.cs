using System;

namespace GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra.GuidedBinaryTraversal.Products
{
    public sealed class TruthTable3Inputs
    {
        public int OutputsPattern { get; private set; }


        public bool[] GetOutputs()
        {
            return new[]
            {
                (OutputsPattern & 1) != 0,
                (OutputsPattern & 2) != 0,
                (OutputsPattern & 4) != 0,
                (OutputsPattern & 8) != 0,
                (OutputsPattern & 16) != 0,
                (OutputsPattern & 32) != 0,
                (OutputsPattern & 64) != 0,
                (OutputsPattern & 128) != 0
            };
        }

        public TruthTable3Inputs SetOutputs(params bool[] outputsArray)
        {
            OutputsPattern =
                (outputsArray[0] ? 1 : 0) |
                (outputsArray[1] ? 2 : 0) |
                (outputsArray[2] ? 4 : 0) |
                (outputsArray[3] ? 8 : 0) |
                (outputsArray[4] ? 16 : 0) |
                (outputsArray[5] ? 32 : 0) |
                (outputsArray[6] ? 64 : 0) |
                (outputsArray[7] ? 128 : 0);

            return this;
        }

        public TruthTable3Inputs SetOutputs(int outputsPattern)
        {
            OutputsPattern = outputsPattern & 255;

            return this;
        }

        public TruthTable3Inputs SetOutputs(Func<bool, bool, bool, bool> outputsFunc)
        {
            OutputsPattern =
                (outputsFunc(false, false, false) ? 1 : 0) |
                (outputsFunc(true, false, false) ? 2 : 0) |
                (outputsFunc(false, true, false) ? 4 : 0) |
                (outputsFunc(true, true, false) ? 8 : 0) |
                (outputsFunc(false, false, true) ? 16 : 0) |
                (outputsFunc(true, false, true) ? 32 : 0) |
                (outputsFunc(false, true, true) ? 64 : 0) |
                (outputsFunc(true, true, true) ? 128 : 0);

            return this;
        }

        public TruthTable3Inputs SetTrue(bool input1, bool input2)
        {
            var i1 = input1 ? 1 : 0;
            var i2 = input2 ? 2 : 0;

            OutputsPattern |= 1 << (i1 | i2);

            return this;
        }

        public TruthTable3Inputs SetFalse(bool input1, bool input2)
        {
            var i1 = input1 ? 1 : 0;
            var i2 = input2 ? 2 : 0;

            OutputsPattern &= ~(1 << (i1 | i2));

            return this;
        }

        public TruthTable3Inputs Invert(bool input1, bool input2)
        {
            var i1 = input1 ? 1 : 0;
            var i2 = input2 ? 2 : 0;

            OutputsPattern ^= 1 << (i1 | i2);

            return this;
        }
    }
}
