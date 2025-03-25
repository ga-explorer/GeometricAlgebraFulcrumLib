using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Composers;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Applications.Graphics
{
    public sealed class GrPovRayBasisBladesVisualizer :
        GrPovRaySceneSequenceComposer
    {
        public GrPovRayBasisBladesVisualizer(string workingFolder, Float64SamplingSpecs samplingSpecs) 
            : base(workingFolder, samplingSpecs)
        {
            AxesOrigin = -5 * LinFloat64Vector3D.E2;
        }


        protected override void AddTemporalValues()
        {
            Console.Write("Generating temporal values .. ");

            var thetaLimit = 90d.DegreesToRadians() - 5e-5d;
            TemporalScalars.SetScalar(
                "theta", 
                Float64ScalarSignal
                    .FiniteCos()
                    .Repeat(
                        1,
                        0, 
                        1,
                        -thetaLimit, 
                        thetaLimit
                    )
            );

            TemporalScalars.SetScalar(
                "sourceVector.color.alpha",
                Float64ScalarSignal
                    .FiniteSmoothRectangle()
                    .Repeat(10, 0, 1, 0, 1)
            );
        
            TemporalScalars.SetScalar(
                "targetVector.color.alpha",
                Float64ScalarSignal
                    .FiniteSmoothRectangle()
                    .Repeat(10, 0, 1, 0, 1)
            );

            Console.WriteLine("done.");
            Console.WriteLine();
        }
        
        protected override void ComposeScene(int frameIndex)
        {
            if (ShowGrid)
                ActiveSceneComposer.AddGrid(
                    "defaultZx",
                    -5 * LinFloat64Vector3D.E2, 
                    LinFloat64Quaternion.XyToZx, 
                    GridUnitCount,
                    1,
                    1
                );

            if (ShowAxes)
                ActiveSceneComposer.AddAxes(
                    "defaultAxes",
                    -5 * LinFloat64Vector3D.E2,
                    LinFloat64Quaternion.Identity,
                    1, 
                    1
                );



            //if (DrawRotorTrace) AddRotorTrace();

            //AddInputVectors(frameIndex);
            //AddRotors(frameIndex);
            //AddProjectedVectors(frameIndex);
            //AddIntersections(frameIndex);
        }
    }
}
