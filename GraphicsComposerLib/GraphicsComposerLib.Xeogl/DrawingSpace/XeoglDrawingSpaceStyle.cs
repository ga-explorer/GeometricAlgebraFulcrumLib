﻿using GraphicsComposerLib.Xeogl.Materials;

namespace GraphicsComposerLib.Xeogl.DrawingSpace
{
    public sealed class XeoglDrawingSpaceStyle
    {
        public XeoglDrawingSpace ParentDrawingSpace { get; }

        public XeoglMaterial PointMaterial { get; set; }

        public XeoglMaterial LineMaterial { get; set; }

        public XeoglMaterial SurfaceMaterial { get; set; }


        internal XeoglDrawingSpaceStyle(XeoglDrawingSpace parentDrawingSpace)
        {
            ParentDrawingSpace = parentDrawingSpace;
        }
    }
}