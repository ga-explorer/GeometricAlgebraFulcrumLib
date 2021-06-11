using System.Text;
using CodeComposerLib.HTMLold.Elements;
using CodeComposerLib.HTMLold.Values;

namespace CodeComposerLib.HTMLold.Attributes
{
    public sealed class HtmlEavPreserveAspectRatio<TParentElement>
        : HtmlElementAttributeValue<TParentElement> where TParentElement : HtmlElement
    {
        private HtmlConstants.Alignment _xAlignment = HtmlConstants.Alignment.Mid;
        public HtmlConstants.Alignment XAlignment
        {
            get { return _xAlignment; }
            set
            {
                _xAlignment = value;
                IsValueComputed = true;
            }
        }

        private HtmlConstants.Alignment _yAlignment = HtmlConstants.Alignment.Mid;
        public HtmlConstants.Alignment YAlignment
        {
            get { return _yAlignment; }
            set
            {
                _yAlignment = value;
                IsValueComputed = true;
            }
        }

        private bool _slice;
        public bool Slice
        {
            get { return _slice; }
            set
            {
                _slice = value;
                IsValueComputed = true;
            }
        }

        public bool Meet
        {
            get { return !_slice; }
            set
            {
                _slice = !value;
                IsValueComputed = true;
            }
        }

        protected override string ValueComputedText
        {
            get
            {
                var s = new StringBuilder();

                s.Append("x");

                if (_xAlignment == HtmlConstants.Alignment.Min)
                    s.Append("Min");
                else if (_xAlignment == HtmlConstants.Alignment.Mid)
                    s.Append("Mid");
                else
                    s.Append("Max");

                s.Append("Y");

                if (_xAlignment == HtmlConstants.Alignment.Min)
                    s.Append("Min");
                else if (_xAlignment == HtmlConstants.Alignment.Mid)
                    s.Append("Mid");
                else
                    s.Append("Max");

                s.Append(Meet ? " meet" : " slice");

                return s.ToString();
            }
        }


        public HtmlEavPreserveAspectRatio(TParentElement parentElement) 
            : base(parentElement, HtmlAttributeUtils.PreserveAspectRatio)
        {
        }


        public override IHtmlAttributeValue CreateCopy()
        {
            throw new System.NotImplementedException();
        }

        public override IHtmlAttributeValue UpdateFrom(IHtmlAttributeValue sourceAttributeValue)
        {
            throw new System.NotImplementedException();
        }

        public TParentElement SetTo(HtmlConstants.Alignment xAlignment, HtmlConstants.Alignment yAlignment, bool meet)
        {
            XAlignment = xAlignment;
            YAlignment = yAlignment;
            Meet = meet;

            return ParentElement;
        }

        public TParentElement SetToMeet(HtmlConstants.Alignment xAlignment, HtmlConstants.Alignment yAlignment)
        {
            XAlignment = xAlignment;
            YAlignment = yAlignment;
            Meet = true;

            return ParentElement;
        }

        public TParentElement SetToSlice(HtmlConstants.Alignment xAlignment, HtmlConstants.Alignment yAlignment)
        {
            XAlignment = xAlignment;
            YAlignment = yAlignment;
            Slice = true;

            return ParentElement;
        }
    }
}