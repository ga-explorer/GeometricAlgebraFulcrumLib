using System;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;

namespace GeometricAlgebraFulcrumLib.Utilities.Composers
{
    public sealed class Float64TextComposer
        : TextComposerBase<double>
    {
        public static Float64TextComposer DefaultComposer { get; }
            = new Float64TextComposer();


        public int RoundingDecimals { get; set; }
            = 15;


        private Float64TextComposer() 
            : base(Float64ScalarProcessor.DefaultProcessor)
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string GetScalarText(double scalar)
        {
            return Math.Round(scalar, RoundingDecimals).ToString("G");
        }
    }
}