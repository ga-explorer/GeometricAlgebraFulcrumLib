using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Atmospheric
{
    public class GrPovRayAtmosphericBackground :
        IGrPovRayStatement
    {
        public static GrPovRayAtmosphericBackground Create(GrPovRayColorValue color)
        {
            return new GrPovRayAtmosphericBackground(color);
        }


        public GrPovRayColorValue Color { get; }


        private GrPovRayAtmosphericBackground(GrPovRayColorValue color)
        {
            Color = color;
        }


        public bool IsEmptyCodeElement()
        {
            return false;
        }

        public string GetPovRayCode()
        {
            return $"background {{ {Color.GetPovRayCode()} }}";
        }

        public override string ToString()
        {
            return GetPovRayCode();
        }
    }
}
