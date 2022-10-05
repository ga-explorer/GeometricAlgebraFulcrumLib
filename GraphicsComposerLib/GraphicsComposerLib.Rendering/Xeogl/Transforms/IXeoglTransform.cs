namespace GraphicsComposerLib.Rendering.Xeogl.Transforms
{
    public interface IXeoglTransform
    {
        bool ContainsMatrix { get; }

        bool ContainsQuaternion { get; }

        bool ContainsRotate { get; }

        bool ContainsScale { get; }

        bool ContainsTranslate { get; }


        string GetMatrixText();

        string GetQuaternionText();

        string GetRotateText();

        string GetScaleText();

        string GetTranslateText();
    }
}