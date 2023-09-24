using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs;

public sealed class GrBabylonJsFreeCode :
    GrBabylonJsObject
{
    private static int CodeBlockId { get; set; }

    private static string GetCodeBlockName()
    {
        return $"codeBlock{CodeBlockId++}";
    }


    public GrBabylonJsCodeValue Code { get; }

    protected override string ConstructorName 
        => string.Empty;

    public override GrBabylonJsObjectOptions? ObjectOptions 
        => null;

    public override GrBabylonJsObjectProperties? ObjectProperties 
        => null;


    public GrBabylonJsFreeCode(string code) 
        : base(GetCodeBlockName())
    {
        Code = code;
    }

    protected override IEnumerable<string> GetConstructorArguments()
    {
        throw new NotImplementedException();
    }

    public override string GetCode()
    {
        return Code.GetCode();
    }
}