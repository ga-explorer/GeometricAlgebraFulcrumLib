namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.BIH.SpaceND;

public sealed class PointND
{
    public readonly double[] Coords;
    public int Dim => Coords.Length;
    public PointND(int dim) { Coords = new double[dim]; }
    public PointND(double[] coords) { Coords = (double[])coords.Clone(); }
}