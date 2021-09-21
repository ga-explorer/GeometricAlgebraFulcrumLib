using System.Text;

namespace GraphicsComposerLib.POVRay.SDL.Values
{
    public sealed class SdlVector2D : SdlValue, ISdlVectorValue
    {
        public ISdlScalarValue X { get; set; }

        public ISdlScalarValue Y { get; set; }

        public override string Value => new StringBuilder()
            .Append(X.ScalarOrDefault())
            .Append(',')
            .Append(Y.ScalarOrDefault())
            .ToString();


        public SdlVector2D(ISdlScalarValue x, ISdlScalarValue y)
        {
            X = x;
            Y = y;
        }


        public override string ToString()
        {
            return TaggedValue;
        }
    }
}
