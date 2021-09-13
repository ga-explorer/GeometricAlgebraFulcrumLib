using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra.Signatures;
using NUnit.Framework;

namespace GeometricAlgebraFulcrumLib.UnitTests.Algebra
{
    [TestFixture]
    public sealed class ProcessorsTests
    {
        private static uint VSpaceDimension = 6;

        private readonly GeometricAlgebraSignatureConformal _processor1 
            = (GeometricAlgebraSignatureConformal) GeometricAlgebraSignatureFactory.CreateConformal(VSpaceDimension);


    }
}
