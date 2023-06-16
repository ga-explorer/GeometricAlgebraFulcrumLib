using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Differential.Functions
{
    public abstract class DifferentialCompositeFunction :
        DifferentialFunction
    {
        public override int TreeDepth 
            => ArgumentCount == 0 
                ? 1 
                : 1 + Arguments.Max(a => a.TreeDepth);

        public override bool IsBasic 
            => false;

        public override bool IsComposite 
            => true;
    
        public override bool IsConstant 
            => Arguments.All(f => f.IsConstant);

        public override bool HasArguments 
            => ArgumentCount > 0;

        public override bool CanBeSimplified { get; }

        public abstract DifferentialFunction MapArguments(Func<DifferentialFunction, DifferentialFunction> functionMapping);

        public abstract DifferentialFunction MapArguments(Func<int, DifferentialFunction, DifferentialFunction> functionMapping);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected DifferentialCompositeFunction(bool canBeSimplified)
        {
            CanBeSimplified = canBeSimplified;
        }
    }
}