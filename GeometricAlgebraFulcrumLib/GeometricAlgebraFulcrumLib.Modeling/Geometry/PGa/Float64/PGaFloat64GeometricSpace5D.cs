//using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
//using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.PGa.Blades;

//namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.PGa;

///// <summary>
///// 5D Conformal Geometric Algebra for 3D Euclidean Space
///// </summary>
//public sealed class PGaFloat64GeometricSpace5D :
//    PGaFloat64GeometricSpace
//{
//    public static PGaFloat64GeometricSpace5D Instance { get; }
//        = new PGaFloat64GeometricSpace5D();


//    public PGaFloat64Blade E3 { get; }

//    public PGaFloat64Blade E13 { get; }

//    public PGaFloat64Blade E23 { get; }

//    public PGaFloat64Visualizer Visualizer { get; }


//    private PGaFloat64GeometricSpace5D()
//        : base(5)
//    {
//        E3 = new PGaFloat64Blade(this, ConformalProcessor.VectorTerm(4));

//        E13 = new PGaFloat64Blade(this, ConformalProcessor.BivectorTerm(2, 4));
//        E23 = new PGaFloat64Blade(this, ConformalProcessor.BivectorTerm(3, 4));

//        Visualizer = new PGaFloat64Visualizer(this);
//    }
//}