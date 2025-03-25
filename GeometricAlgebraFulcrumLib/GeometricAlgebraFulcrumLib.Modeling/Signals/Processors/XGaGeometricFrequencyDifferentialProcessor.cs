using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Modeling.Signals.Interpolators;

namespace GeometricAlgebraFulcrumLib.Modeling.Signals.Processors;

public sealed class XGaGeometricFrequencyDifferentialProcessor :
    XGaGeometricFrequencyProcessor
{
    public Func<XGaVector<Float64SampledTimeSignal>, VectorDifferentialInterpolator> InterpolatorFactory { get; }


    public XGaGeometricFrequencyDifferentialProcessor(int vSpaceDimensions, Func<XGaVector<Float64SampledTimeSignal>, VectorDifferentialInterpolator> interpolatorFactory) 
        : base(vSpaceDimensions)
    {
        InterpolatorFactory = interpolatorFactory;
    }


    protected override void ComputeVectorSignalTimeDerivatives()
    {
        var vectorInterpolator = InterpolatorFactory(VectorSignal);
        
        VectorSignalInterpolated = vectorInterpolator.GetVectors();

        var vDtArray = new XGaVector<Float64SampledTimeSignal>[VSpaceDimensions];

        if (AssumeVectorSignalIsDerivative)
        {
            // This is to use one-less derivative which gives more numerically
            // stable results

            vDtArray[0] = vectorInterpolator.GetVectors();

            for (var degree = 1; degree < VSpaceDimensions; degree++)
                vDtArray[degree] = vectorInterpolator.GetVectorsDt(degree);
        }
        else
        {
            for (var degree = 0; degree < VSpaceDimensions; degree++)
                vDtArray[degree] = vectorInterpolator.GetVectorsDt(degree + 1);
        }

        VectorSignalTimeDerivatives = vDtArray;
    }


    public void ProcessVectorSignal(XGaVector<Float64SampledTimeSignal> vectorSignal)
    {
        ClearData();

        VectorSignal = vectorSignal;
        SamplingSpecs = vectorSignal.GetSamplingSpecs();

        ComputeVectorSignalTimeDerivatives();

        ComputeArcLengthTimeDerivatives();

        ComputeVectorSignalArcLengthDerivatives();

        ComputeArcLengthFrames();

        ComputeCurvatures();

        //ComputeArcLengthFrameDerivatives();

        ValidateData();
    }
}