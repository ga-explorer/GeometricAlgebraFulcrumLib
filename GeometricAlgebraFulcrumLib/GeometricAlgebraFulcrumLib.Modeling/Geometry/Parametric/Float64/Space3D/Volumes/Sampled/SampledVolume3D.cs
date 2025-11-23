// File: SampledVolume3D.cs

using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Volumes.Sampled
{
    public sealed class SampledVolume3D
    {
        #region Nested Types

        public readonly struct CornerPosition3D : IEquatable<CornerPosition3D>
        {
            public int Level { get; }
            public int SegmentU { get; }
            public int SegmentV { get; }
            public int SegmentW { get; }

            internal CornerPosition3D(int level, int sU, int sV, int sW)
            {
                if (level < 0)
                    throw new ArgumentOutOfRangeException(nameof(level));

                if (sU < 0 || sV < 0 || sW < 0)
                    throw new ArgumentOutOfRangeException("segment counts");

                if (sU == 0 && sV == 0 && sW == 0)
                {
                    Level = 0;
                    SegmentU = SegmentV = SegmentW = 0;
                    return;
                }

                var tzU = CountTrailingZeroBits((uint)sU);
                var tzV = CountTrailingZeroBits((uint)sV);
                var tzW = CountTrailingZeroBits((uint)sW);

                var tz = Math.Min(Math.Min(tzU, tzV), tzW);
                if (tz > level) tz = level;

                Level = level - tz;
                SegmentU = sU >> tz;
                SegmentV = sV >> tz;
                SegmentW = sW >> tz;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static int CountTrailingZeroBits(uint x)
            {
                if (x == 0) return 32;
                var tz = 0;
                while ((x & 1) == 0) { tz++; x >>= 1; }
                return tz;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public double GetInterpolationValueU() => SegmentU / (double)(1L << Level);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public double GetInterpolationValueV() => SegmentV / (double)(1L << Level);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public double GetInterpolationValueW() => SegmentW / (double)(1L << Level);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public (double u, double v, double w) GetParameterValues(double minU, double maxU, double minV, double maxV, double minW, double maxW)
            {
                var a = GetInterpolationValueU();
                var b = GetInterpolationValueV();
                var c = GetInterpolationValueW();
                var u = minU + a * (maxU - minU);
                var v = minV + b * (maxV - minV);
                var w = minW + c * (maxW - minW);
                return (u, v, w);
            }

            public override string ToString() =>
                $"(L={Level}, SU={SegmentU}, SV={SegmentV}, SW={SegmentW})";

            public bool Equals(CornerPosition3D other) =>
                Level == other.Level && SegmentU == other.SegmentU && SegmentV == other.SegmentV && SegmentW == other.SegmentW;

            public override bool Equals(object? obj) => obj is CornerPosition3D c && Equals(c);
            public override int GetHashCode() => HashCode.Combine(Level, SegmentU, SegmentV, SegmentW);
        }

        public sealed class Corner
        {
            public AdaptiveVolumeSampler3D Sampler { get; }
            public int Index { get; }
            public VolumeFrame Frame { get; }
            public CornerPosition3D Position { get; }

            internal Corner(AdaptiveVolumeSampler3D sampler, int index, VolumeFrame frame, CornerPosition3D pos)
            {
                Sampler = sampler ?? throw new ArgumentNullException(nameof(sampler));
                Index = index;
                Frame = frame;
                Position = pos;
            }

            public override string ToString() => $"Corner[{Index}] (u={Frame.ParamU:F4}, v={Frame.ParamV:F4}, w={Frame.ParamW:F4})";
        }

        public sealed class LeafDescriptor
        {
            public int Level { get; }
            public int CellU { get; }
            public int CellV { get; }
            public int CellW { get; }

            public (CornerPosition3D, CornerPosition3D, CornerPosition3D, CornerPosition3D, CornerPosition3D, CornerPosition3D, CornerPosition3D, CornerPosition3D) C000to111 { get; }

            public CornerPosition3D C000 => C000to111.Item1;
            public CornerPosition3D C001 => C000to111.Item2;
            public CornerPosition3D C010 => C000to111.Item3;
            public CornerPosition3D C011 => C000to111.Item4;
            public CornerPosition3D C100 => C000to111.Item5;
            public CornerPosition3D C101 => C000to111.Item6;
            public CornerPosition3D C110 => C000to111.Item7;
            public CornerPosition3D C111 => C000to111.Item8;

            public LeafDescriptor(int level, int u, int v, int w,
                CornerPosition3D c000, CornerPosition3D c001, CornerPosition3D c010, CornerPosition3D c011,
                CornerPosition3D c100, CornerPosition3D c101, CornerPosition3D c110, CornerPosition3D c111)
            {
                Level = level; CellU = u; CellV = v; CellW = w;
                C000to111 = (c000, c001, c010, c011, c100, c101, c110, c111);
            }
        }

        public sealed class Sample
        {
            public LeafNode Leaf { get; }
            public double ParamU { get; }
            public double ParamV { get; }
            public double ParamW { get; }

            private readonly double _s000, _s001, _s010, _s011, _s100, _s101, _s110, _s111;

            internal Sample(LeafNode leaf, double u, double v, double w)
            {
                Leaf = leaf;
                ParamU = u; ParamV = v; ParamW = w;

                var u0 = (ParamU - leaf.MinParamU) / (leaf.MaxParamU - leaf.MinParamU);
                var v0 = (ParamV - leaf.MinParamV) / (leaf.MaxParamV - leaf.MinParamV);
                var w0 = (ParamW - leaf.MinParamW) / (leaf.MaxParamW - leaf.MinParamW);

                Debug.Assert(u0 >= 0 && u0 <= 1 && v0 >= 0 && v0 <= 1 && w0 >= 0 && w0 <= 1);

                var iu = u0; var iv = v0; var iw = w0;
                _s000 = (1 - iu) * (1 - iv) * (1 - iw);
                _s001 = (1 - iu) * (1 - iv) * iw;
                _s010 = (1 - iu) * iv * (1 - iw);
                _s011 = (1 - iu) * iv * iw;
                _s100 = iu * (1 - iv) * (1 - iw);
                _s101 = iu * (1 - iv) * iw;
                _s110 = iu * iv * (1 - iw);
                _s111 = iu * iv * iw;
            }

            private LinFloat64Vector3D InterpPoint()
            {
                return _s000 * Leaf.Frame000.Point + _s001 * Leaf.Frame001.Point + _s010 * Leaf.Frame010.Point +
                       _s011 * Leaf.Frame011.Point + _s100 * Leaf.Frame100.Point + _s101 * Leaf.Frame101.Point +
                       _s110 * Leaf.Frame110.Point + _s111 * Leaf.Frame111.Point;
            }

            private LinFloat64Vector3D InterpGradient()
            {
                var g = _s000 * Leaf.Frame000.Gradient + _s001 * Leaf.Frame001.Gradient + _s010 * Leaf.Frame010.Gradient +
                        _s011 * Leaf.Frame011.Gradient + _s100 * Leaf.Frame100.Gradient + _s101 * Leaf.Frame101.Gradient +
                        _s110 * Leaf.Frame110.Gradient + _s111 * Leaf.Frame111.Gradient;
                return g;
            }

            public LinFloat64Vector3D GetPoint() => InterpPoint();
            public LinFloat64Vector3D GetGradient() => InterpGradient().VectorDivideByENorm();
        }

        public sealed class LeafNode
        {
            public SampledVolume3D Sampler { get; }
            public int DescriptorIndex { get; }
            public int CornerIdx000 { get; }
            public int CornerIdx001 { get; }
            public int CornerIdx010 { get; }
            public int CornerIdx011 { get; }
            public int CornerIdx100 { get; }
            public int CornerIdx101 { get; }
            public int CornerIdx110 { get; }
            public int CornerIdx111 { get; }

            internal LeafNode(SampledVolume3D sampler, int descriptorIndex,
                int c000, int c001, int c010, int c011, int c100, int c101, int c110, int c111)
            {
                Sampler = sampler;
                DescriptorIndex = descriptorIndex;
                CornerIdx000 = c000; CornerIdx001 = c001; CornerIdx010 = c010; CornerIdx011 = c011;
                CornerIdx100 = c100; CornerIdx101 = c101; CornerIdx110 = c110; CornerIdx111 = c111;
            }

            public VolumeFrame Frame000 => Sampler.GetFrameFromCorner(CornerIdx000);
            public VolumeFrame Frame001 => Sampler.GetFrameFromCorner(CornerIdx001);
            public VolumeFrame Frame010 => Sampler.GetFrameFromCorner(CornerIdx010);
            public VolumeFrame Frame011 => Sampler.GetFrameFromCorner(CornerIdx011);
            public VolumeFrame Frame100 => Sampler.GetFrameFromCorner(CornerIdx100);
            public VolumeFrame Frame101 => Sampler.GetFrameFromCorner(CornerIdx101);
            public VolumeFrame Frame110 => Sampler.GetFrameFromCorner(CornerIdx110);
            public VolumeFrame Frame111 => Sampler.GetFrameFromCorner(CornerIdx111);

            public double MinParamU => Math.Min(Math.Min(Frame000.ParamU, Frame001.ParamU), Math.Min(Frame010.ParamU, Frame011.ParamU));
            public double MaxParamU => Math.Max(Math.Max(Frame100.ParamU, Frame101.ParamU), Math.Max(Frame110.ParamU, Frame111.ParamU));

            public double MinParamV => Math.Min(Math.Min(Frame000.ParamV, Frame001.ParamV), Math.Min(Frame010.ParamV, Frame011.ParamV));
            public double MaxParamV => Math.Max(Math.Max(Frame100.ParamV, Frame101.ParamV), Math.Max(Frame110.ParamV, Frame111.ParamV));

            public double MinParamW => Math.Min(Math.Min(Frame000.ParamW, Frame001.ParamW), Math.Min(Frame010.ParamW, Frame011.ParamW));
            public double MaxParamW => Math.Max(Math.Max(Frame100.ParamW, Frame101.ParamW), Math.Max(Frame110.ParamW, Frame111.ParamW));

            public bool Contains(double u, double v, double w)
            {
                const double tol = 1e-12;
                return u >= MinParamU - tol && u <= MaxParamU + tol &&
                       v >= MinParamV - tol && v <= MaxParamV + tol &&
                       w >= MinParamW - tol && w <= MaxParamW + tol;
            }
        }

        #endregion

        private readonly AdaptiveVolumeSampler3D _sampler;
        private readonly List<LeafNode> _leafNodes = new List<LeafNode>();

        public SampledVolume3D(AdaptiveVolumeSampler3D sampler)
        {
            _sampler = sampler ?? throw new ArgumentNullException(nameof(sampler));
        }

        public IReadOnlyList<LeafDescriptor> LeafDescriptors => _sampler.LeafDescriptors;

        public int LeafCount => _leafNodes.Count;

        public SampledVolume3D Generate()
        {
            BuildLeafNodes();
            return this;
        }

        private void BuildLeafNodes()
        {
            _leafNodes.Clear();
            _leafNodes.Capacity = _sampler.LeafDescriptors.Count;
            for (var i = 0; i < _sampler.LeafDescriptors.Count; i++)
            {
                var desc = _sampler.LeafDescriptors[i];

                // For each of the 8 corners, ask sampler for the corner index corresponding to that single position.
                // We construct a canonical 8-element key where each element is the same corner position
                var key000 = (desc.C000, desc.C000, desc.C000, desc.C000, desc.C000, desc.C000, desc.C000, desc.C000);
                var key001 = (desc.C001, desc.C001, desc.C001, desc.C001, desc.C001, desc.C001, desc.C001, desc.C001);
                var key010 = (desc.C010, desc.C010, desc.C010, desc.C010, desc.C010, desc.C010, desc.C010, desc.C010);
                var key011 = (desc.C011, desc.C011, desc.C011, desc.C011, desc.C011, desc.C011, desc.C011, desc.C011);
                var key100 = (desc.C100, desc.C100, desc.C100, desc.C100, desc.C100, desc.C100, desc.C100, desc.C100);
                var key101 = (desc.C101, desc.C101, desc.C101, desc.C101, desc.C101, desc.C101, desc.C101, desc.C101);
                var key110 = (desc.C110, desc.C110, desc.C110, desc.C110, desc.C110, desc.C110, desc.C110, desc.C110);
                var key111 = (desc.C111, desc.C111, desc.C111, desc.C111, desc.C111, desc.C111, desc.C111, desc.C111);

                var c000 = _sampler.GetOrAddCornerIndex(key000);
                var c001 = _sampler.GetOrAddCornerIndex(key001);
                var c010 = _sampler.GetOrAddCornerIndex(key010);
                var c011 = _sampler.GetOrAddCornerIndex(key011);
                var c100 = _sampler.GetOrAddCornerIndex(key100);
                var c101 = _sampler.GetOrAddCornerIndex(key101);
                var c110 = _sampler.GetOrAddCornerIndex(key110);
                var c111 = _sampler.GetOrAddCornerIndex(key111);

                var node = new LeafNode(this, i, c000, c001, c010, c011, c100, c101, c110, c111);
                _leafNodes.Add(node);
            }
        }

        internal VolumeFrame GetFrameFromCorner(int cornerIndex)
        {
            return _sampler.CornerList[cornerIndex].Frame;
        }

        public LeafNode? FindLeafNodeForParameters(double u, double v, double w)
        {
            foreach (var ln in _leafNodes)
                if (ln.Contains(u, v, w)) return ln;
            return null;
        }

        public Sample GetSample(double u, double v, double w)
        {
            var leaf = FindLeafNodeForParameters(u, v, w);
            if (leaf == null) throw new ArgumentOutOfRangeException($"Parameters outside sampled domain: u={u},v={v},w={w}");
            return new Sample(leaf, u, v, w);
        }
    }
}
