namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Groups
{
    public class GrVisualFrameStyle3D
    {
        public IGrVisualElementMaterial3D OriginMaterial { get; set; }

        public IGrVisualElementMaterial3D DirectionMaterial1 { get; set; }

        public IGrVisualElementMaterial3D DirectionMaterial2 { get; set; }

        public IGrVisualElementMaterial3D DirectionMaterial3 { get; set; }

        public double OriginThickness { get; set; } = 0.075;

        public double DirectionThickness { get; set; } = 0.05;
    }
}