namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL.Objects
{
    public enum SdlCsgOperation
    {
        Union = 0, SplitUnion = 1, Merge = 2, Intersection = 3, Difference = 4
    }

    public sealed class SdlCsgObject : SdlObject, ISdlCsgObject
    {
        private static readonly string[] CsgOpNames = new[]
        {
            "union", "split_union", "merge", "intersection", "difference"
        };


        public SdlCsgOperation CsgOperation { get; set; }

        public string CsgOperationName => CsgOpNames[(int)CsgOperation];

        public List<ISdlSolidObject> ChildObjects { get; }

        public ISdlSolidObject FirstChildObject
        {
            get => ChildObjects.FirstOrDefault();
            set
            {
                if (ChildObjects.Count == 0)
                    ChildObjects.Add(value);
                else
                    ChildObjects[0] = value;
            }
        }


        public SdlCsgObject()
        {
            ChildObjects = new List<ISdlSolidObject>();
        }
    }
}
