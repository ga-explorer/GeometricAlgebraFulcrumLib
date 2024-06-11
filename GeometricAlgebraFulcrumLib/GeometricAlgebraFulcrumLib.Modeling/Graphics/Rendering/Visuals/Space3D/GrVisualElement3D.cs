using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D;

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

            _visibility = Float64Utils.Clamp(value, 0d, 1d);
        }
    }
        

    protected GrVisualElement3D(string name)
    {
        Name = name;
    }


    public abstract bool IsValid();
}