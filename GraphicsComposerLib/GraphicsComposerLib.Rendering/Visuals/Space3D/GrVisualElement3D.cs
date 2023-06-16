using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D
{
    public abstract class GrVisualElement3D :
        IGrVisualElement3D
    {
        public string Name { get; }

        
        private double _visibility = 1d;
        public double Visibility
        {
            get => _visibility;
            set
            {
                if (!value.IsValid())
                    throw new ArgumentException(nameof(value));

                _visibility = value.Clamp(0d, 1d);
            }
        }
        

        protected GrVisualElement3D(string name)
        {
            Name = name;
        }


        public abstract bool IsValid();
    }
}