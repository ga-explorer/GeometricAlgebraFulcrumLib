namespace GraphicsComposerLib.Rendering.ThreeJs.Obsolete.Constants
{
    public static class TjAnimationConstants
    {
        public enum LoopModes
        {
            LoopOnce,
            LoopRepeat,
            LoopPingPong
        }

        public enum InterpolationModes
        {
            InterpolateDiscrete,
            InterpolateLinear,
            InterpolateSmooth
        }

        public enum EndingModes
        {
            ZeroCurvatureEnding,
            ZeroSlopeEnding,
            WrapAroundEnding
        }


        public static string GetName(this LoopModes value)
        {
            return value switch
            {
                LoopModes.LoopOnce => "THREE.LoopOnce",
                LoopModes.LoopRepeat => "THREE.LoopRepeat",
                LoopModes.LoopPingPong => "THREE.LoopPingPong",
                _ => throw new ArgumentOutOfRangeException(nameof(value))
            };
        }
        
        public static string GetName(this InterpolationModes value)
        {
            return value switch
            {
                InterpolationModes.InterpolateDiscrete => "THREE.InterpolateDiscrete",
                InterpolationModes.InterpolateLinear => "THREE.InterpolateLinear",
                InterpolationModes.InterpolateSmooth => "THREE.InterpolateSmooth",
                _ => throw new ArgumentOutOfRangeException(nameof(value))
            };
        }
        
        public static string GetName(this EndingModes value)
        {
            return value switch
            {
                EndingModes.ZeroCurvatureEnding => "THREE.ZeroCurvatureEnding",
                EndingModes.ZeroSlopeEnding => "THREE.ZeroSlopeEnding",
                EndingModes.WrapAroundEnding => "THREE.WrapAroundEnding",
                _ => throw new ArgumentOutOfRangeException(nameof(value))
            };
        }
    }
}