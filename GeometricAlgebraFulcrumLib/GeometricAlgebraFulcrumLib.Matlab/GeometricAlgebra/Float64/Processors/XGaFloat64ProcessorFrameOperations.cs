using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Frames;
using GeometricAlgebraFulcrumLib.Matlab.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors
{
    public partial class XGaFloat64Processor
    {
        
        public XGaFloat64BasisVectorFrame CreateBasisVectorFrame(int vSpaceDimensions)
        {
            return XGaFloat64BasisVectorFrame.Create(
                vSpaceDimensions.GetRange(VectorTerm)
            );
        }

        
        public XGaFloat64VectorFrame CreateFreeFrameOfBasis(int vSpaceDimensions)
        {
            var vectorsList =
                vSpaceDimensions
                    .GetRange()
                    .Select(index => VectorTerm(index));

            return XGaFloat64VectorFrame.Create(
                XGaFloat64VectorFrameSpecs.CreateUnitBasisSpecs(),
                vectorsList
            );
        }

        
        public XGaFloat64VectorFrame CreateFreeFrameOfScaledBasis(int vSpaceDimensions, double scalingFactor)
        {
            var vectorsList =
                vSpaceDimensions
                    .GetRange()
                    .Select(index => VectorTerm(index, scalingFactor));

            return XGaFloat64VectorFrame.Create(
                XGaFloat64VectorFrameSpecs.CreateScaledBasisSpecs(),
                vectorsList
            );
        }

        
        public XGaFloat64VectorFrame CreateFreeFrameOfSimplex(int vSpaceDimensions, double scalingFactor)
        {
            var vSpaceDimensions1 = vSpaceDimensions + 1;

            // Create ones-vector
            var onesVector = VectorSymmetricUnit(vSpaceDimensions1);
            var basisVector = VectorTerm(vSpaceDimensions);

            // Find a rotor that rotates the ones vector into the last basis vector
            var rotor = onesVector.CreatePureRotor(
                basisVector,
                true
            );

            // Create a subspace from the dual of the ones vector
            var hyperSubspace =
                onesVector.Dual(vSpaceDimensions + 1).ToSubspace();

            // Project each basis vectors on the subspace, and then rotate
            // the projected vectors and discard the last component of each vector
            var vectorsList =
                vSpaceDimensions1
                    .GetRange()
                    .Select(index =>
                        rotor.OmMap(
                            hyperSubspace.Project(
                                VectorTerm(index, scalingFactor)
                            )
                        ).GetVectorPart((int i) => i < vSpaceDimensions)
                    );

            return XGaFloat64VectorFrame.Create(
                XGaFloat64VectorFrameSpecs.CreateSimplexSpecs(),
                vectorsList
            );
        }

        
        public XGaFloat64VectorFrameFixed CreateBasisVectorFrameFixed(int vSpaceDimensions)
        {
            return CreateFreeFrameOfBasis(vSpaceDimensions)
                .CreateFixedFrame();
        }

        
        public XGaFloat64VectorFrameFixed CreateFixedFrameOfScaledBasis(int vSpaceDimensions, double scalingFactor)
        {
            return CreateFreeFrameOfScaledBasis(vSpaceDimensions, scalingFactor)
                .CreateFixedFrame();
        }

        
        public XGaFloat64VectorFrameFixed CreateFixedFrameOfSimplex(int vSpaceDimensions, double scalingFactor)
        {
            return CreateFreeFrameOfSimplex(vSpaceDimensions, scalingFactor).CreateFixedFrame();
        }

    }
}
