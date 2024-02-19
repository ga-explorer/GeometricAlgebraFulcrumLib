using System.Text;
using WebComposerLib.Svg.Elements;
using WebComposerLib.Svg.Values;

namespace WebComposerLib.Svg.Attributes;

public sealed class SvgEavPreserveAspectRatio<TParentElement>
    : SvgElementAttributeValue<TParentElement> where TParentElement : SvgElement
{
    private SvgConstants.Alignment _xAlignment = SvgConstants.Alignment.Mid;
    public SvgConstants.Alignment XAlignment
    {
        get => _xAlignment;
        set
        {
            _xAlignment = value;
            IsValueComputed = true;
        }
    }

    private SvgConstants.Alignment _yAlignment = SvgConstants.Alignment.Mid;
    public SvgConstants.Alignment YAlignment
    {
        get => _yAlignment;
        set
        {
            _yAlignment = value;
            IsValueComputed = true;
        }
    }

    private bool _slice;
    public bool Slice
    {
        get => _slice;
        set
        {
            _slice = value;
            IsValueComputed = true;
        }
    }

    public bool Meet
    {
        get => !_slice;
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

            if (_xAlignment == SvgConstants.Alignment.Min)
                s.Append("Min");
            else if (_xAlignment == SvgConstants.Alignment.Mid)
                s.Append("Mid");
            else
                s.Append("Max");

            s.Append("Y");

            if (_xAlignment == SvgConstants.Alignment.Min)
                s.Append("Min");
            else if (_xAlignment == SvgConstants.Alignment.Mid)
                s.Append("Mid");
            else
                s.Append("Max");

            s.Append(Meet ? " meet" : " slice");

            return s.ToString();
        }
    }


    public SvgEavPreserveAspectRatio(TParentElement parentElement) 
        : base(parentElement, SvgAttributeUtils.PreserveAspectRatio)
    {
    }


    public override ISvgAttributeValue CreateCopy()
    {
        throw new NotImplementedException();
    }

    public override ISvgAttributeValue UpdateFrom(ISvgAttributeValue sourceAttributeValue)
    {
        throw new NotImplementedException();
    }

    public TParentElement SetTo(SvgConstants.Alignment xAlignment, SvgConstants.Alignment yAlignment, bool meet)
    {
        XAlignment = xAlignment;
        YAlignment = yAlignment;
        Meet = meet;

        return ParentElement;
    }

    public TParentElement SetToMeet(SvgConstants.Alignment xAlignment, SvgConstants.Alignment yAlignment)
    {
        XAlignment = xAlignment;
        YAlignment = yAlignment;
        Meet = true;

        return ParentElement;
    }

    public TParentElement SetToSlice(SvgConstants.Alignment xAlignment, SvgConstants.Alignment yAlignment)
    {
        XAlignment = xAlignment;
        YAlignment = yAlignment;
        Slice = true;

        return ParentElement;
    }
}