namespace GraphicsComposerLib.Rendering.Visuals.Space2D
{
    public abstract class GrVisualElement2D :
        IGrVisualElement2D
    {
        public string Name { get; }
    
        public GrVisualElementStyle2D Style { get; }
            = new GrVisualElementStyle2D();


        protected GrVisualElement2D(string name)
        {
            Name = name;
        }


        public abstract bool IsValid();
    }
}