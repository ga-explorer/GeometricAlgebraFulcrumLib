using GeometricAlgebraFulcrumLib.MathBase.Geometry.Differential.Functions;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Differential.Curves
{
    public class DifferentialCurve
    {
        public IReadOnlyList<DifferentialFunction> ScalarFunctions { get; }


        public int Dimensions 
            => ScalarFunctions.Count;

        public DifferentialFunction this[int index] 
            => ScalarFunctions[index];


        public DifferentialCurve(IReadOnlyList<DifferentialFunction> scalarFunctions)
        {
            ScalarFunctions = scalarFunctions;
        }



    }
}
