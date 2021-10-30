using System;

namespace GraphicsComposerLib.WebGl.ThreeJs.Obsolete.Constants
{
    public static class TjBufferAttributeUsageConstants
    {
        public enum GeometryUsage
        {
            StaticDrawUsage,
            DynamicDrawUsage,
            StreamDrawUsage,
            StaticReadUsage,
            DynamicReadUsage,
            StreamReadUsage,
            StaticCopyUsage,
            DynamicCopyUsage,
            StreamCopyUsage
        }

        
        public static string GetName(this GeometryUsage value)
        {
            return value switch
            {
                GeometryUsage.StaticDrawUsage => "StaticDrawUsage",
                GeometryUsage.DynamicDrawUsage => "DynamicDrawUsage",
                GeometryUsage.StreamDrawUsage => "StreamDrawUsage",
                GeometryUsage.StaticReadUsage => "StaticReadUsage",
                GeometryUsage.DynamicReadUsage => "DynamicReadUsage",
                GeometryUsage.StreamReadUsage => "StreamReadUsage",
                GeometryUsage.StaticCopyUsage => "StaticCopyUsage",
                GeometryUsage.DynamicCopyUsage => "DynamicCopyUsage",
                GeometryUsage.StreamCopyUsage => "StreamCopyUsage",
                _ => throw new ArgumentOutOfRangeException(nameof(value))
            };
        }
    }
}