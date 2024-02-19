using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL.Values;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL.Objects.ISP;

public sealed class SdlPolySurfaceSparseCoef
{
    public int XPower { get; }

    public int YPower { get; }

    public int ZPower { get; }

    public ISdlScalarValue Coef { get; set; }


    internal SdlPolySurfaceSparseCoef(int xPower, int yPower, int zPower)
    {
        XPower = xPower;
        YPower = yPower;
        ZPower = zPower;
    }
}

public class SdlPolySurfaceSparse : SdlObject, ISdlIspObject
{
    protected readonly List<SdlPolySurfaceSparseCoef> SparseCoefsList = 
        new List<SdlPolySurfaceSparseCoef>();

    public int Order { get; }

    public IEnumerable<SdlPolySurfaceSparseCoef> Coefs => SparseCoefsList;

    public int SparseCoefsCount => SparseCoefsList.Count;

    public int MaxCoefsCount => (Order + 1) * (Order + 2) * (Order + 3) / 6;

    public ISdlScalarValue this[int xPower, int yPower, int zPower]
    {
        get
        {
            var i = SparseCoefsList.FindIndex(
                c => c.XPower == xPower && c.YPower == yPower && c.ZPower == zPower
            );

            return i >= 0 ? SparseCoefsList[i].Coef : SdlScalarLiteral.Zero;
        }
        set
        {
            RemoveCoef(xPower, yPower, zPower);

            SparseCoefsList.Add(
                new SdlPolySurfaceSparseCoef(xPower, yPower, zPower) { Coef = value }
            );
        }
    }


    public SdlPolySurfaceSparse(int order)
    {
        Order = order;
    }


    public SdlPolySurfaceSparse ClearCoefs()
    {
        SparseCoefsList.Clear();
        return this;
    }

    public SdlPolySurfaceSparse RemoveCoef(int xPower, int yPower, int zPower)
    {
        var i = SparseCoefsList.FindIndex(c => c.XPower == xPower && c.YPower == yPower && c.ZPower == zPower);

        if (i >= 0) SparseCoefsList.RemoveAt(i);

        return this;
    }
}