using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Curves
{
    public sealed class GrBabylonJsHermiteSpline :
        GrBabylonJsCurve3Base
    {
        protected override string ConstructorName 
            => "BABYLON.Curve3.CreateHermiteSpline";

        public GrBabylonJsVector3Value Point1 { get; set; }

        public GrBabylonJsVector3Value Tangent1 { get; set; }

        public GrBabylonJsVector3Value Point2 { get; set; }

        public GrBabylonJsVector3Value Tangent2 { get; set; }

        public GrBabylonJsVector3Value PointNumber { get; set; }


        protected override IEnumerable<string> GetConstructorArguments()
        {
            yield return Point1.GetCode();
            yield return Tangent1.GetCode();
            yield return Point2.GetCode();
            yield return Tangent2.GetCode();
            yield return PointNumber.GetCode();
        }


        public GrBabylonJsHermiteSpline(string constName) 
            : base(constName)
        {
        }
    }
}