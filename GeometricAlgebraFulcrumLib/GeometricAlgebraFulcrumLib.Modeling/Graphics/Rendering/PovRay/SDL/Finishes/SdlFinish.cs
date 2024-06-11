namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.SDL.Finishes;

public class SdlFinish : ISdlFinish
{
    public string FinishIdentifier { get; set; }

    public List<ISdlFinishItem> FinishItems { get; private set; }

    public bool ConserveEnergy { get; set; }


    public SdlFinish()
    {
        FinishItems = new List<ISdlFinishItem>();
    }
}