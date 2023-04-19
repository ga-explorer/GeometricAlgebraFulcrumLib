using System.Collections;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Basic;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Surfaces;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Groups
{
    public sealed class GrVisualFrame3D :
        GrVisualElement3D,
        IGrVisualElementList3D
    {
        public IFloat64Tuple3D Origin { get; set; } 
            = Float64Tuple3D.Zero;

        public IFloat64Tuple3D Direction1 { get; set; } 
            = Float64Tuple3D.E1;

        public IFloat64Tuple3D Direction2 { get; set; } 
            = Float64Tuple3D.E2;

        public IFloat64Tuple3D Direction3 { get; set; } 
            = Float64Tuple3D.E3;

        public GrVisualFrameStyle3D Style { get; set; } 

        //public GrVisualImage3D? OriginTextImage { get; set; }

        //public GrVisualImage3D? Direction1TextImage { get; set; }

        //public GrVisualImage3D? Direction2TextImage { get; set; }

        //public GrVisualImage3D? Direction3TextImage { get; set; }

        public int Count 
            => 4;

        public IGrVisualElement3D this[int index]
        {
            get
            {
                return index switch
                {
                    0 => GetVisualOrigin(),
                    1 => GetVisualVector1(),
                    2 => GetVisualVector2(),
                    3 => GetVisualVector3(),
                    _ => throw new IndexOutOfRangeException()
                };
            }
        }


        public GrVisualFrame3D(string name) 
            : base(name)
        {
        }


        public GrVisualPoint3D GetVisualOrigin()
        {
            return new GrVisualPoint3D($"{Name}Origin", Origin)
            {
                Style = new GrVisualSurfaceThickStyle3D(
                    Style.OriginMaterial, 
                    Style.OriginThickness
                )
            };
        }

        public GrVisualVector3D GetVisualVector1()
        {
            return new GrVisualVector3D($"{Name}Vector1", Origin, Direction1)
            {
                Style = new GrVisualVectorStyle3D(
                    Style.DirectionMaterial1,
                    Style.DirectionThickness
                )
            };
        }

        public GrVisualVector3D GetVisualVector2()
        {
            return new GrVisualVector3D($"{Name}Vector2", Origin, Direction2)
            {
                Style = new GrVisualVectorStyle3D(
                    Style.DirectionMaterial2,
                    Style.DirectionThickness
                )
            };
        }

        public GrVisualVector3D GetVisualVector3()
        {
            return new GrVisualVector3D($"{Name}Vector3", Origin, Direction3)
            {
                Style = new GrVisualVectorStyle3D(
                    Style.DirectionMaterial3,
                    Style.DirectionThickness
                )
            };
        }

        public IEnumerator<IGrVisualElement3D> GetEnumerator()
        {
            yield return GetVisualOrigin();
            yield return GetVisualVector1();
            yield return GetVisualVector2();
            yield return GetVisualVector3();

            //if (OriginTextImage is not null) yield return OriginTextImage;
            //if (Direction1TextImage is not null) yield return Direction1TextImage;
            //if (Direction2TextImage is not null) yield return Direction2TextImage;
            //if (Direction3TextImage is not null) yield return Direction3TextImage;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}