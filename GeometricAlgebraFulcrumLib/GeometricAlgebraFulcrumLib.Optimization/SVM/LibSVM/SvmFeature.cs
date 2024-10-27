using System.Runtime.InteropServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Optimization.SVM.LibSVM
{
    public sealed record SvmFeature
    {
        public static IReadOnlyList<SvmFeature> CreateFromStructArrayPtr(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                return [];

            var features = new List<SvmFeature>();
            var iPtrFeatures = ptr;

            while (true)
            {
                var feature = (LibSvmInterop.FeatureStruct)Marshal.PtrToStructure(iPtrFeatures, typeof(LibSvmInterop.FeatureStruct))!;
                iPtrFeatures = IntPtr.Add(iPtrFeatures, Marshal.SizeOf(typeof(LibSvmInterop.FeatureStruct)));

                if (feature.index <= 0)
                    break;

                features.Add(new SvmFeature(feature.index - 1, feature.value));
            }

            return features.ToArray();
        }


        internal int IndexPlus1 
            => Index + 1;

        public int Index { get; }

        public double Value { get; }


        public SvmFeature(int index, double value)
        {
            if (index < 0)
                throw new IndexOutOfRangeException();

            if (value.IsNaNOrInfinite())
                throw new ArgumentOutOfRangeException(nameof(value));

            Index = index;
            Value = value;
        }

        public void Deconstruct(out int index, out double value)
        {
            index = Index;
            value = Value;
        }

        public bool Equals(SvmFeature? x)
        {
            if (x == null) return false;

            return Index.Equals(x.Index) && Value.Equals(x.Value);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Index, Value);
        }

        public SvmFeature GetCopy()
        {
            return this;
        }
    }
}
