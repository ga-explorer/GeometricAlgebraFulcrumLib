using System.Collections;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Collections;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic2D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Textures;

public sealed class GraphicsTextureCoordinatesGrid
    : IPeriodicSequence2D<ILinFloat64Vector2D>
{
    public IReadOnlyList<double> TextureURange { get; }

    public IReadOnlyList<double> TextureVRange { get; }

    public int Count1 
        => TextureURange.Count;

    public int Count2 
        => TextureVRange.Count;

    public int Count 
        => TextureURange.Count * TextureVRange.Count;

    public ILinFloat64Vector2D this[int index]
    {
        get
        {
            var (index1, index2) = this.GetItemIndexPair(index);

            return LinFloat64Vector2D.Create((Float64Scalar)TextureURange[index1],
                (Float64Scalar)TextureVRange[index2]);
        }
    }

    public ILinFloat64Vector2D this[int index1, int index2] 
        => LinFloat64Vector2D.Create((Float64Scalar)TextureURange[index1],
            (Float64Scalar)TextureVRange[index2]);

    public bool IsBasic 
        => true;

    public bool IsOperator 
        => false;


    public GraphicsTextureCoordinatesGrid(IReadOnlyList<double> textureURange, IReadOnlyList<double> textureVRange)
    {
        TextureURange = textureURange ?? throw new ArgumentNullException(nameof(textureURange));
        TextureVRange = textureVRange ?? throw new ArgumentNullException(nameof(textureVRange));
    }


    public PSeqSlice1D<ILinFloat64Vector2D> GetSliceAt(int dimension, int index)
    {
        return new PSeqSlice1D<ILinFloat64Vector2D>(this, dimension, index);
    }

    public IEnumerator<ILinFloat64Vector2D> GetEnumerator()
    {
        foreach (var value1 in TextureURange)
        foreach (var value2 in TextureVRange)
            yield return LinFloat64Vector2D.Create((Float64Scalar)value1, (Float64Scalar)value2);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}