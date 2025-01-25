namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

public class GrPovRayFlagValue :
    GrPovRayValue<bool>
{
    public static GrPovRayFlagValue True { get; } 
        = new GrPovRayFlagValue(true);
    

    public static implicit operator GrPovRayFlagValue(bool value)
    {
        if (!value) 
            throw new ArgumentException();

        return True;
    }


    public bool IsTrue 
        => Value;
    
    public bool IsFalse 
        => !Value;

    
    private GrPovRayFlagValue(bool value)
        : base(value)
    {
    }


    public override string GetPovRayCode()
    {
        return string.Empty;
    }
}