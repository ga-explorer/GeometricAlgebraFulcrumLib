using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Directives;

/// <summary>
/// http://www.povray.org/documentation/3.7.0/r3_3.html#r3_3_2_2
/// </summary>
public sealed class GrPovRayDeclareDirective<T> : 
    GrPovRayDirective 
    where T : IGrPovRayRValue
{
    public bool IsLocal { get; }

    public string Identifier { get; }

    public T Value { get; }

    
    internal GrPovRayDeclareDirective(string identifier, T value, bool isLocal = false)
    {
        Identifier = identifier;
        Value = value;
        IsLocal = isLocal;
    }

    public void Deconstruct(out string identifier, out T value)
    {
        identifier = Identifier;
        value = Value;
    }


    public override string GetPovRayCode()
    {
        var composer = new LinearTextComposer();

        composer
            .Append(IsLocal ? "#local " : "#declare ")
            .Append(Identifier)
            .Append(" = ")
            .Append(Value.GetPovRayCode())
            .Append(";");

        return composer.ToString();
    }
}