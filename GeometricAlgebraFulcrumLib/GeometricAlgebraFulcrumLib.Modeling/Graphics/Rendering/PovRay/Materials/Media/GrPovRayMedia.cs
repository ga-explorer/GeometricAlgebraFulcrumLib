using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Lists;
using GeometricAlgebraFulcrumLib.Utilities.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Media
{
    public class GrPovRayMedia :
        IGrPovRayMedia
    {
        public string BaseElementIdentifier { get; }
        
        public GrPovRayMediaProperties Properties { get; private set; }
            = new GrPovRayMediaProperties();

        public GrPovRayTransformList TransformList { get; }
            = new GrPovRayTransformList();


        public GrPovRayMedia()
        {
            BaseElementIdentifier = string.Empty;
        }

        public GrPovRayMedia(string finishIdentifier)
        {
            BaseElementIdentifier = finishIdentifier;
        }


        public GrPovRayMedia SetProperties(GrPovRayMediaProperties properties)
        {
            Properties = new GrPovRayMediaProperties(properties);

            return this;
        }

        public bool IsEmptyCodeElement()
        {
            return BaseElementIdentifier.IsNullOrEmpty() &&
                   Properties.IsNullOrEmpty() &&
                   TransformList.IsNullOrEmpty();
        }

        public string GetPovRayCode()
        {
            var composer = new LinearTextComposer();

            composer
                .AppendLine("media {")
                .IncreaseIndentation();

            if (!string.IsNullOrEmpty(BaseElementIdentifier))
                composer.AppendAtNewLine(BaseElementIdentifier);

            composer.AppendAtNewLine(Properties.GetPovRayCode());

            if (!TransformList.IsEmptyCodeElement())
                composer.AppendAtNewLine(TransformList.GetPovRayCode());

            composer
                .DecreaseIndentation()
                .AppendAtNewLine("}");

            return composer.ToString();
        }
    }
}
