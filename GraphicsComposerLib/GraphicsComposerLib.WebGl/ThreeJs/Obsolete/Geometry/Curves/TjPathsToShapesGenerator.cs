using System;

namespace GraphicsComposerLib.WebGl.ThreeJs.Obsolete.Geometry.Curves
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