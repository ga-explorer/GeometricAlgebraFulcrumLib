using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Decoding;

public abstract class CGaBladeDecoderBase<T>
{
    public CGaBlade<T> Blade { get; }

    public CGaGeometricSpace<T> GeometricSpace 
        => Blade.GeometricSpace;


    protected CGaBladeDecoderBase(CGaBlade<T> blade)
    {
        Blade = blade;
    }
}