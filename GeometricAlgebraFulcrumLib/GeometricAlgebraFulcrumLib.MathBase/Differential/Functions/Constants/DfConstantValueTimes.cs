namespace GeometricAlgebraFulcrumLib.MathBase.Differential.Functions.Constants
{
    public sealed class DfConstantValueTimes :
        DfConstantValue
    {
        public IReadOnlyList<DfConstantValue> Factors { get; }

        public override bool IsZero 
            => Factors.Any(c => c.IsZero);

        public override bool IsOne 
            => Factors.Count == 0 || Factors.All(c => c.IsOne);

        public override double Float64Value 
            => Factors.Aggregate(
                1d, 
                (d, value) => d * value.Float64Value
            );


        private DfConstantValueTimes(IReadOnlyList<DfConstantValue> factors)
        {
            Factors = factors;
        }


        public override DfConstantValue Simplify()
        {
            throw new NotImplementedException();
        }
    }
}