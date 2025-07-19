using System;
using System.Text;
using GeometricAlgebraFulcrumLib.Utilities.Code.HTMLold.Elements;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.HTMLold.Attributes;

/// <summary>
/// http://docs.w3cub.com/svg/content_type/#Clock-value
/// http://www.w3.org/TR/smil-animation
/// </summary>
/// <typeparam name="TParentElement"></typeparam>
public sealed class HtmlEavClock<TParentElement>
    : HtmlElementAttributeValue<TParentElement> where TParentElement : HtmlElement
{
    private double _hoursValue;
    public double HoursValue
    {
        get { return _hoursValue; }
        set
        {
            _hoursValue = value;
            IsValueComputed = true;
        }
    }

    private double _minutesValue;
    public double MinutesValue
    {
        get { return _minutesValue; }
        set
        {
            _minutesValue = value;
            IsValueComputed = true;
        }
    }

    private double _secondsValue;
    public double SecondsValue
    {
        get { return _secondsValue; }
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
                    .Append((_secondsValue * 1000).ToHtmlNumberText())
                    .Append("ms")
                    .ToString();

            if (IsValueSecondsCount)
                return s
                    .Append(_secondsValue.ToHtmlNumberText())
                    .Append("s")
                    .ToString();

            if (IsValueMinutesCount)
                return s
                    .Append(_minutesValue.ToHtmlNumberText())
                    .Append("min")
                    .ToString();

            if (IsValueHoursCount)
                return s
                    .Append(_hoursValue.ToHtmlNumberText())
                    .Append("h")
                    .ToString();

            if (IsValueFullClock)
            {
                if (!IsHoursValueZero)
                    return s
                        .Append(_hoursValue.ToHtmlNumberText())
                        .Append(":")
                        .Append(_minutesValue.ToHtmlNumberText())
                        .Append(":")
                        .Append(_secondsValue.ToHtmlNumberText())
                        .ToString();

                if (!IsMinutesValueZero)
                    return s
                        .Append(_minutesValue.ToHtmlNumberText())
                        .Append(":")
                        .Append(_secondsValue.ToHtmlNumberText())
                        .ToString();

                return s
                    .Append(_secondsValue.ToHtmlNumberText())
                    .ToString();

            }

            return string.Empty;
        }
    }


    internal HtmlEavClock(TParentElement parentElement, HtmlAttributeInfo attributeInfo)
        : base(parentElement, attributeInfo)
    {
    }


    public override IHtmlAttributeValue CreateCopy()
    {
        throw new NotImplementedException();
    }

    public override IHtmlAttributeValue UpdateFrom(IHtmlAttributeValue sourceAttributeValue)
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