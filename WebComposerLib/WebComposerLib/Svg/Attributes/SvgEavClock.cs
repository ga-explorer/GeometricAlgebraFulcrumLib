using System.Text;
using WebComposerLib.Html.Media;
using WebComposerLib.Svg.Elements;

namespace WebComposerLib.Svg.Attributes;

/// <summary>
/// http://docs.w3cub.com/svg/content_type/#Clock-value
/// http://www.w3.org/TR/smil-animation
/// </summary>
/// <typeparam name="TParentElement"></typeparam>
public sealed class SvgEavClock<TParentElement>
    : SvgElementAttributeValue<TParentElement> where TParentElement : SvgElement
{
    private double _hoursValue;
    public double HoursValue
    {
        get => _hoursValue;
        set
        {
            _hoursValue = value;
            IsValueComputed = true;
        }
    }

    private double _minutesValue;
    public double MinutesValue
    {
        get => _minutesValue;
        set
        {
            _minutesValue = value;
            IsValueComputed = true;
        }
    }

    private double _secondsValue;
    public double SecondsValue
    {
        get => _secondsValue;
        set
        {
            _secondsValue = value;
            IsValueComputed = true;
        }
    }

    public bool IsHoursValueZero 
        => Math.Abs(_hoursValue) <= 0;

    public bool IsMinutesValueZero 
        => Math.Abs(_minutesValue) <= 0;

    public bool IsSecondsValueZero
        => Math.Abs(_secondsValue) <= 0;

    public bool IsHoursValueWhole
        => Math.Abs(_hoursValue % 1) <= 0;

    public bool IsMinutesValueWhole
        => Math.Abs(_minutesValue % 1) <= 0;

    public bool IsSecondsValueWhole
        => Math.Abs(_secondsValue % 1) <= 0;

    public bool IsValueFullClock
        => IsHoursValueWhole && HoursValue >= 0 &&
           IsMinutesValueWhole && MinutesValue >= 0 && MinutesValue < 60 &&
           SecondsValue < 60;

    public bool IsValueHoursCount
        => !IsHoursValueZero && IsMinutesValueZero && IsSecondsValueZero;

    public bool IsValueMinutesCount
        => IsHoursValueZero && !IsMinutesValueZero && IsSecondsValueZero;

    public bool IsValueSecondsCount
        => IsHoursValueZero && IsMinutesValueZero && !IsSecondsValueZero;

    public bool IsValueMilliSecondsCount
        => IsHoursValueZero && IsMinutesValueZero && !IsSecondsValueZero && _secondsValue < 1;

    public bool IsValueTimeCount
        => IsValueHoursCount || IsValueMinutesCount || IsValueSecondsCount;


    protected override string ValueComputedText
    {
        get
        {
            var s = new StringBuilder(128);

            if (IsValueMilliSecondsCount)
                return s
                    .Append((_secondsValue * 1000).ToSvgNumberText())
                    .Append("ms")
                    .ToString();

            if (IsValueSecondsCount)
                return s
                    .Append(_secondsValue.ToSvgNumberText())
                    .Append("s")
                    .ToString();

            if (IsValueMinutesCount)
                return s
                    .Append(_minutesValue.ToSvgNumberText())
                    .Append("min")
                    .ToString();

            if (IsValueHoursCount)
                return s
                    .Append(_hoursValue.ToSvgNumberText())
                    .Append("h")
                    .ToString();

            if (IsValueFullClock)
            {
                if (!IsHoursValueZero)
                    return s
                        .Append(_hoursValue.ToSvgNumberText())
                        .Append(":")
                        .Append(_minutesValue.ToSvgNumberText())
                        .Append(":")
                        .Append(_secondsValue.ToSvgNumberText())
                        .ToString();

                if (!IsMinutesValueZero)
                    return s
                        .Append(_minutesValue.ToSvgNumberText())
                        .Append(":")
                        .Append(_secondsValue.ToSvgNumberText())
                        .ToString();

                return s
                    .Append(_secondsValue.ToSvgNumberText())
                    .ToString();

            }

            return string.Empty;
        }
    }


    internal SvgEavClock(TParentElement parentElement, SvgAttributeInfo attributeInfo)
        : base(parentElement, attributeInfo)
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

    public TParentElement SetToHours(double hoursValue)
    {
        HoursValue = hoursValue;
        MinutesValue = 0;
        SecondsValue = 0;

        return ParentElement;
    }

    public TParentElement SetToMinutes(double minutesValue)
    {
        HoursValue = 0;
        MinutesValue = minutesValue;
        SecondsValue = 0;

        return ParentElement;
    }

    public TParentElement SetToSeconds(double secondsValue)
    {
        HoursValue = 0;
        MinutesValue = 0;
        SecondsValue = secondsValue;

        return ParentElement;
    }

    public TParentElement SetToMilliSeconds(int milliSecondsValue)
    {
        HoursValue = 0;
        MinutesValue = 0;
        SecondsValue = ((double)milliSecondsValue) / 1000;

        return ParentElement;
    }

    public TParentElement SetToClock(double secondsValue)
    {
        SecondsValue = secondsValue;

        return ParentElement;
    }

    public TParentElement SetToClock(int minutesValue, double secondsValue)
    {
        HoursValue = 0;
        MinutesValue = minutesValue;
        SecondsValue = secondsValue;

        return ParentElement;
    }

    public TParentElement SetToClock(int hoursValue, int minutesValue, double secondsValue)
    {
        HoursValue = hoursValue;
        MinutesValue = minutesValue;
        SecondsValue = secondsValue;

        return ParentElement;
    }

    //public TParentElement SetToClock(int hoursValue, int minutesValue, int secondsValue, int milliSecondsValue)
    //{
    //    HoursValue = hoursValue;
    //    MinutesValue = minutesValue;
    //    SecondsValue = secondsValue + ((double)milliSecondsValue) / 1000;

    //    return ParentElement;
    //}
}