using DataStructuresLib.Basic;
using SixLabors.ImageSharp.Formats;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Images
{
    public abstract class GrVisualImage3D :
        GrVisualElement3D, 
        IGrVisualImage3D
    {
        protected GrVisualImage3D(string name) 
            : base(name)
        {
        }


        public abstract Pair<int> GetSize();

        public abstract Image GetImage();
        
        public void SaveImage(string filePath)
        {
            var image = GetImage();

            image.Save(filePath);
        }

        public void SaveImage(string filePath, IImageEncoder format)
        {
            var image = GetImage();

            image.Save(filePath, format);
        }
    }
}
