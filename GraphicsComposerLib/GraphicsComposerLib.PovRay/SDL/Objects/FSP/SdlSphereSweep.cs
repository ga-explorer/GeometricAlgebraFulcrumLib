using System.Collections.Generic;
using GraphicsComposerLib.PovRay.SDL.Values;

namespace GraphicsComposerLib.PovRay.SDL.Objects.FSP
{
    public enum SdlSphereSweepKind
    {
        LinearSpline = 0, BSpline = 1, CubicSpline = 2
    }

    public sealed class SdlSphereSweepItem
    {
        public ISdlVectorValue Center { get; set; }

        public ISdlScalarValue Radius { get; set; }
    }

    public class SdlSphereSweep : SdlObject, ISdlFspObject
    {
        private static readonly string[] KindNames = new[]
        {
            "linear_spline", "b_spline", "cubic_spline"
        };


        public List<SdlSphereSweepItem> Spheres { get; private set; }

        public ISdlScalarValue Tolerance { get; set; }

        public SdlSphereSweepKind InterpolationKind { get; set; }

        public string InterpolationKindName => KindNames[(int)InterpolationKind];


        public SdlSphereSweep()
        {
            Spheres = new List<SdlSphereSweepItem>();
        }
    }
}
