namespace GeometricAlgebraFulcrumLib.MathBase.Differential.Functions.Constants
{
    public sealed class DfConstantValuePlus :
        DfConstantValue
    {
        public IReadOnlyList<DfConstantValue> Factors { get; }

        public override bool IsZero { get; }

        public override bool IsOne { get; }

        public override double Float64Value 
            => Factors.Aggregate(
                1d, 
                (d, value) => d * value.Float64Value
            );


        private DfConstantValuePlus(IReadOnlyList<DfConstantValue> factors)
        {
            Factors = factors;
        }


        public override DfConstantValue Simplify()
        {
            throw new NotImplementedException();
        }
    }
}