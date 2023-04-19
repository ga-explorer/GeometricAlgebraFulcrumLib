using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Curves
{
    public sealed class GrBabylonJsCubicBezier :
        GrBabylonJsCurve3Base
    {
        protected override string ConstructorName 
            => "BABYLON.Curve3.CreateCubicBezier";

        public GrBabylonJsVector3Value Point1 { get; set; }

        public GrBabylonJsVector3Value Point2 { get; set; }

        public GrBabylonJsVector3Value Point3 { get; set; }

        public GrBabylonJsVector3Value Point4 { get; set; }

        public GrBabylonJsVector3Value PointNumber { get; set; }
    

        public GrBabylonJsCubicBezier(string constName) 
            : base(constName)
        {
        }


        protected override IEnumerable<string> GetConstructorArguments()
        {
            yield return Point1.GetCode();
            yield return Point2.GetCode();
            yield return Point3.GetCode();
            yield return Point4.GetCode();
            yield return PointNumber.GetCode();
        }
    }
}