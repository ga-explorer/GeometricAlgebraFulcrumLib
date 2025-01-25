using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Textures;

public abstract class GrPovRayTexture :
    IGrPovRayTexture
{
    public string BaseElementIdentifier { get; }

    public Float64AffineMap3D AffineMap { get; } 
        = Float64AffineMap3D.Create();

    public IFloat64AffineMap3D Transform 
        => AffineMap;

    //public GrPovRayTransformList Transforms { get; } 
    //    = new GrPovRayTransformList();


    protected GrPovRayTexture(string identifier)
    {
        BaseElementIdentifier = identifier;
    }
    

    public abstract bool IsEmptyCodeElement();
    
    public abstract string GetPovRayCode(bool isInterior);

    public string GetPovRayCode()
    {
        return GetPovRayCode(false);
    }

    public override string ToString()
    {
        return GetPovRayCode();
    }
}