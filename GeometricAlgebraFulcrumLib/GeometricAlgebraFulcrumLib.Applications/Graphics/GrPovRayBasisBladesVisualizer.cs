using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Composers;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Grids;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars;
using GeometricAlgebraFulcrumLib.Utilities.Web.Images;
using SixLabors.ImageSharp;

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


        protected override void InitializeTemporalValues()
        {
            Console.Write("Generating temporal values .. ");

            var thetaLimit = 90d.DegreesToRadians() - 5e-5d;
            TemporalScalarSet.SetScalar(
                "theta", 
                TemporalFloat64Scalar
                    .FullCos()
                    .Repeat(
                        1,
                        0, 
                        1,
                        -thetaLimit, 
                        thetaLimit
                    )
            );

            TemporalScalarSet.SetScalar(
                "sourceVector.color.alpha",
                TemporalFloat64Scalar
                    .SmoothRectangle()
                    .Repeat(10, 0, 1, 0, 1)
            );
        
            TemporalScalarSet.SetScalar(
                "targetVector.color.alpha",
                TemporalFloat64Scalar
                    .SmoothRectangle()
                    .Repeat(10, 0, 1, 0, 1)
            );

            Console.WriteLine("done.");
            Console.WriteLine();
        }
        
        protected override void InitializeTextureSet()
        {
            
        }

        protected override void AddGrid()
        {
            // Add ground coordinates grid
            ActiveSceneComposer.GridMaterialKind =
                GrPovRayGridMaterialKind.TexturedMaterial;
            
            var imageComposer = new GrVisualGridImageComposer()
            {
                BaseSquareColor = Color.LightYellow,
                BaseLineColor = Color.BurlyWood,
                MidLineColor = Color.SandyBrown,
                BorderLineColor = Color.SaddleBrown,
                BaseSquareCount = 4,
                BaseSquareSize = 64,
                BaseLineWidth = 2,
                MidLineWidth = 4,
                BorderLineWidth = 3
            };

            imageComposer.SetGridColorsOpacity(1);

            ActiveSceneComposer.AddSquareGrid(
                GrVisualSquareGrid3D.DefaultZx(
                    LinFloat64Vector3D.Zero, 
                    GridUnitCount,
                    1,
                    1
                )
            );
        }

        protected override void ComposeFrame(int frameIndex)
        {
            base.ComposeFrame(frameIndex);

            //if (DrawRotorTrace) AddRotorTrace();

            //AddInputVectors(frameIndex);
            //AddRotors(frameIndex);
            //AddProjectedVectors(frameIndex);
            //AddIntersections(frameIndex);
        }
    }
}
