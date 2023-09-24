using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Images;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D
{
    public sealed class GrVisualTexture3D :
        GrVisualElement3D
    {
        public GrVisualImage3D Composer { get; set; }


        public GrVisualTexture3D(string name) 
            : base(name)
        {
        }


        public override bool IsValid()
        {
            return true;
        }
    }
}