using GeometricAlgebraFulcrumLib.MathBase.Parametric.Curves;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Curves
{
    public sealed class GrVisualParametricCurve3D :
        GrVisualCurve3D
    {
        public IParametricCurve3D Curve { get; }

        public IReadOnlyList<double> ParameterValues { get; }

        public IReadOnlyList<double> FrameParameterValues { get; }

        public bool ShowCurve { get; set; }
            = true;

        public bool ShowFrames { get; set; }
            = false;

        public double FrameSize { get; set; } 
            = 1;
    

        public GrVisualParametricCurve3D(string name, IParametricCurve3D curve, IReadOnlyList<double> parameterValues, IReadOnlyList<double> frameParameterValues)
            : base(name)
        {
            Curve = curve;
            ParameterValues = parameterValues;
            FrameParameterValues = frameParameterValues;
        }


        //public double GetLength()
        //{
        //    var length = 0d;
        //    var point1 = PositionList[0];

        //    for (var i = 1; i < PositionList.Count; i++)
        //    {
        //        var point2 = PositionList[i];

        //        length += point1.GetDistanceToPoint(point2);

        //        point1 = point2;
        //    }

        //    return length;
        //}
    }
}