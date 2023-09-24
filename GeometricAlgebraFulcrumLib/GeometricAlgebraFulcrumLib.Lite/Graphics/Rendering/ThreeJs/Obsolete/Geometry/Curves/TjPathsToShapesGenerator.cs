namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.ThreeJs.Obsolete.Geometry.Curves
{
    public class TjPathsToShapesGenerator :
        TjComponentSimple
    {
        public override string JavaScriptClassName 
            => "ShapePath";


        protected override string GetConstructorArgumentsText()
        {
            return string.Empty;
        }

        protected override string GetSetMethodArgumentsText()
        {
            throw new InvalidOperationException();
        }
    }
}