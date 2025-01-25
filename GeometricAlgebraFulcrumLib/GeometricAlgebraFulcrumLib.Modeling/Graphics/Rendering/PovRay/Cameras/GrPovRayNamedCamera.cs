//using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

//namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Cameras;

///// <summary>
///// http://www.povray.org/documentation/3.7.0/r3_4.html#r3_4_2_5
///// </summary>
//public class GrPovRayNamedCamera : 
//    GrPovRayCamera
//{
//    public override string CameraType { get; }


//    internal GrPovRayNamedCamera(string cameraType)
//    {
//        CameraType = cameraType;
//    }
    

//    public override string GetPovRayCode()
//    {
//        var composer = new LinearTextComposer();

//        composer
//            .AppendLine("camera {")
//            .IncreaseIndentation()
//            .AppendLine(CameraType)
//            .AppendAtNewLine(Transforms.GetPovRayCode())
//            .DecreaseIndentation()
//            .AppendAtNewLine("}");

//        return composer.ToString();
//    }
//}