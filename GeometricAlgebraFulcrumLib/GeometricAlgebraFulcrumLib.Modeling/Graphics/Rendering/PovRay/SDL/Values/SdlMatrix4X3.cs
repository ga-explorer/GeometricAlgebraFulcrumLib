using System.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.SDL.Values;

public sealed class SdlMatrix4X3 : SdlValue
{
    public int Rows => 4;

    public int Columns => 3;


    public SdlScalarLiteral[,] Items { get; }

    public override string Value
    {
        get
        {
            var s = new StringBuilder();

            for (var r = 0; r < Rows; r++)
            for (var c = 0; c < Columns; c++)
                s.Append(Items[r, c].ScalarOrDefault()).Append(',');

            s.Length -= 1;

            return s.ToString();
        }
    }


    internal SdlMatrix4X3()
    {
        Items = new SdlScalarLiteral[4, 3];
    }


    public override string ToString()
    {
        return TaggedValue;
    }
}