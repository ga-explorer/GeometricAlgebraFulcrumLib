using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Curves;
using GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsPath;
using GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsPath.Space3D;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Styles;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Curves
{
    public sealed class GrVisualParametricCurve3D :
        GrVisualCurve3D
    {
        public static GrVisualParametricCurve3D Create(string name, GrVisualCurveStyle3D style, IParametricCurve3D curve, IReadOnlyList<double> parameterValues, IReadOnlyList<double> frameParameterValues)
        {
            return new GrVisualParametricCurve3D(
                name,
                style, 
                curve, 
                parameterValues,
                frameParameterValues
            );
        }


        public IParametricCurve3D Curve { get; }

        public IReadOnlyList<double> ParameterValues { get; }

        public IReadOnlyList<double> FrameParameterValues { get; }

        public bool ShowCurve { get; set; }
            = true;

        public bool ShowFrames { get; set; }
            = false;

        public double FrameSize { get; set; } 
            = 1;

        public override double ArcLength 
            => throw new NotImplementedException();


        private GrVisualParametricCurve3D(string name, GrVisualCurveStyle3D style, IParametricCurve3D curve, IReadOnlyList<double> parameterValues, IReadOnlyList<double> frameParameterValues)
            : base(name, style)
        {
            Curve = curve;
            ParameterValues = parameterValues;
            FrameParameterValues = frameParameterValues;
        }


        public override IPointsPath3D GetPointsPath(int count)
        {
            return new ParametricPointsPath3D(
                ParameterValues,
                Curve.GetPoint
            );
        }

        public override bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}