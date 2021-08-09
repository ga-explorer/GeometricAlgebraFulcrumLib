using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using NUnit.Framework;

namespace GeometricAlgebraFulcrumLib.UnitTests.Algebra
{
    [TestFixture]
    public sealed class ProcessorsTests
    {
        private static uint VSpaceDimension = 6;

        private readonly GaSignatureConformal _processor1 
            = (GaSignatureConformal) GaSignatureFactory.CreateConformal(VSpaceDimension);


    }
}
