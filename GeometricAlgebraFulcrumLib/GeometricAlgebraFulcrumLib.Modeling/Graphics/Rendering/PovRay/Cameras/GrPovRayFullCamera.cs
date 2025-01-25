using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Cameras;

public abstract class GrPovRayFullCamera : 
    GrPovRayCamera
{
    public GrPovRayColorValue? FlashLightColor { get; set; }
        = GrPovRayColorValue.Rgb(0.9, 0.9, 1);

    public abstract GrPovRayFullCameraProperties Properties { get; protected set; }


    public Float64Scalar RightLength 
        => Properties.Right.Norm(); 
    
    public Float64Scalar UpLength 
        => Properties.Up.Norm(); 


    protected GrPovRayFullCamera(string baseCameraName) 
        : base(baseCameraName)
    {
    }


    public override bool IsEmptyCodeElement()
    {
        return Properties.IsNullOrEmpty() &&
               Transform.IsNearIdentity();
    }

    public override string GetPovRayCode()
    {
        var composer = new LinearTextComposer();

        composer
            .AppendLine("camera {")
            .IncreaseIndentation();

        if (!BaseCameraName.IsNullOrEmpty())
            composer.AppendAtNewLine(BaseCameraName);

        composer
            .AppendLine(CameraType)
            .AppendAtNewLine(Properties.GetPovRayCode())
            .AppendAtNewLine(Transform.GetPovRayMatrixCode())
            .DecreaseIndentation()
            .AppendAtNewLine("}");

        return composer.ToString();
    }

}