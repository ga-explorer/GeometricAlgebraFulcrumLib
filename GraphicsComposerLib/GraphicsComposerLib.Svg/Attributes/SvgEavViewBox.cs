using System.Text;
using GraphicsComposerLib.Svg.Elements;

namespace GraphicsComposerLib.Svg.Attributes
{
    public sealed class SvgEavViewBox<TParentElement> 
        : SvgElementAttributeValue<TParentElement> where TParentElement : SvgElement
    {
        private double _minX;
        public double MinX
        {
            get { return _minX; }
            set
            {
                _minX = value;
                IsValueComputed = true;
            }
        }

        private double _minY;
        public double MinY
        {
            get { return _minY; }
            set
            {
                _minY = value;
                IsValueComputed = true;
            }
        }

        private double _width;
        public double Width
        {
            get { return _width; }
            set
            {
                _width = value;
                IsValueComputed = true;
            }
        }

        private double _height;
        public double Height
        {
            get { return _height; }
            set
            {
                _height = value;
                IsValueComputed = true;
            }
        }

        protected override string ValueComputedText 
            => new StringBuilder()
                .Append(MinX.ToSvgNumberText())
                .Append(", ")
                .Append(MinY.ToSvgNumberText())
                .Append(", ")
                .Append(Width.ToSvgNumberText())
                .Append(", ")
                .Append(Height.ToSvgNumberText())
                .ToString();


        internal SvgEavViewBox(TParentElement parentElement)
            : base(parentElement, SvgAttributeUtils.ViewBox)
        {
        }


        public override ISvgAttributeValue CreateCopy()
        {
            throw new System.NotImplementedException();
        }

        public override ISvgAttributeValue UpdateFrom(ISvgAttributeValue sourceAttributeValue)
        {
            throw new System.NotImplementedException();
        }

        public TParentElement SetMinCorner(double minX, double minY)
        {
            MinX = minX;
            MinY = minY;

            return ParentElement;
        }

        public TParentElement SetSize(double width, double height)
        {
            Width = width;
            Height = height;

            return ParentElement;
        }

        public TParentElement SetTo(double minX, double minY, double width, double height)
        {
            MinX = minX;
            MinY = minY;
            Width = width;
            Height = height;

            return ParentElement;
        }
    }
}