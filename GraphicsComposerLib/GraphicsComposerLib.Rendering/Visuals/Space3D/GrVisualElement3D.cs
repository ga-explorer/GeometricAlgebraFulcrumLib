namespace GraphicsComposerLib.Rendering.Visuals.Space3D
{
    public abstract class GrVisualElement3D :
        IGrVisualElement3D
    {
        public string Name { get; }

        //public GrVisualImage3D? TextImage { get; set; }


        protected GrVisualElement3D(string name)
        {
            Name = name;
        }
    }
}