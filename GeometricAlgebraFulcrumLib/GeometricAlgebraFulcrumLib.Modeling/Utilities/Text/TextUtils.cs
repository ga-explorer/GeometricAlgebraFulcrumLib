using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Modeling.Geometry;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;

namespace GeometricAlgebraFulcrumLib.Modeling.Utilities.Text
{
    public static class TextUtils
    {
        public static string VGaToLaTeX(XGaFloat64Multivector mv)
        {
            var vSpaceDimensions = Math.Max(mv.VSpaceDimensions, 4);

            var specs = GaFloat64GeometricSpaceBasisSpecs.CreateVGa(vSpaceDimensions);

            return specs.ToLaTeX(mv);
        }

        public static string CGaToLaTeX(XGaFloat64Multivector mv)
        {
            var vSpaceDimensions = Math.Max(mv.VSpaceDimensions, 4);

            var specs = GaFloat64GeometricSpaceBasisSpecs.CreateCGa(vSpaceDimensions);

            return specs.ToLaTeX(mv);
        }
        
        public static string CGaToLaTeX(CGaFloat64Blade mv)
        {
            return mv.BasisSpecs.ToLaTeX(mv.InternalKVector);
        }

        public static string CGaOrthoToLaTeX(XGaFloat64Multivector mv)
        {
            var vSpaceDimensions = Math.Max(mv.VSpaceDimensions, 4);

            var specs = GaFloat64GeometricSpaceBasisSpecs.CreateCGaOrthogonal(vSpaceDimensions);

            return specs.ToLaTeX(mv);
        }
        
        public static string CGaOrthoToLaTeX(CGaFloat64Blade mv)
        {
            var vSpaceDimensions = mv.BasisSpecs.VSpaceDimensions;
            
            var specs = GaFloat64GeometricSpaceBasisSpecs.CreateCGaOrthogonal(vSpaceDimensions);

            return specs.ToLaTeX(mv.InternalKVector);
        }


        public static string VGaToText(XGaFloat64Multivector mv)
        {
            var vSpaceDimensions = Math.Max(mv.VSpaceDimensions, 2);

            var specs = GaFloat64GeometricSpaceBasisSpecs.CreateVGa(vSpaceDimensions);

            return specs.ToText(mv);
        }

        public static string CGaToText(XGaFloat64Multivector mv)
        {
            var vSpaceDimensions = Math.Max(mv.VSpaceDimensions, 4);

            var specs = GaFloat64GeometricSpaceBasisSpecs.CreateCGa(vSpaceDimensions);

            return specs.ToText(mv);
        }
        
        public static string CGaToText(CGaFloat64Blade mv)
        {
            return mv.BasisSpecs.ToText(mv.InternalKVector);
        }

        public static string CGaOrthoToText(XGaFloat64Multivector mv)
        {
            var vSpaceDimensions = Math.Max(mv.VSpaceDimensions, 4);

            var specs = GaFloat64GeometricSpaceBasisSpecs.CreateCGaOrthogonal(vSpaceDimensions);

            return specs.ToText(mv);
        }
        
        public static string CGaOrthoToText(CGaFloat64Blade mv)
        {
            var vSpaceDimensions = mv.BasisSpecs.VSpaceDimensions;
            
            var specs = GaFloat64GeometricSpaceBasisSpecs.CreateCGaOrthogonal(vSpaceDimensions);

            return specs.ToText(mv.InternalKVector);
        }

    }
}
