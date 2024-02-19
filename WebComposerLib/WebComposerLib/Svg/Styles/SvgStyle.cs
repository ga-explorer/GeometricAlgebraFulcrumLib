﻿using TextComposerLib.Text;
using TextComposerLib.Text.Linear;
using WebComposerLib.Svg.Attributes;
using WebComposerLib.Svg.Elements;
using WebComposerLib.Svg.Styles.Properties;
using WebComposerLib.Svg.Values;

namespace WebComposerLib.Svg.Styles;

public sealed class SvgStyle : 
    SvgElementAttributeValue<SvgElement>, 
    ISvgStyle
{
    public static SvgStyle Create()
    {
        return new SvgStyle();
    }

    public static SvgStyle Create(SvgElement parentElement)
    {
        return new SvgStyle(parentElement);
    }


    private readonly Dictionary<int, SvgStylePropertyValue> _propertiesTable
        = new Dictionary<int, SvgStylePropertyValue>();


    public IEnumerable<SvgAttributeInfo> PropertyInfos
        => SvgAttributeUtils.AttributeInfos.Where(a => a.IsCssAttribute);

    public IEnumerable<SvgAttributeInfo> ActivePropertyInfos
        => ActivePropertyValues.Select(p => p.AttributeInfo);

    public IEnumerable<SvgStylePropertyValue> ActivePropertyValues
        => _propertiesTable
            .Values
            .Where(v => !v.IsNullOrEmpty());

    public string ActivePropertyValuesText
        => _propertiesTable.Count == 0
            ? string.Empty
            : ActivePropertyValues.Concatenate(" ");

    public bool IsSubStyle => false;

    public SvgStyle BaseStyle => this;

    protected override string ValueComputedText
        => ActivePropertyValuesText;


    #region Font selection properties https://www.w3.org/TR/SVG11/text.html#FontPropertiesUsedBySVG
    public SvgSpvString Font
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.Font;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpvString;

            var attrValue1 = new SvgSpvString(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpvString FontFamily
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.FontFamily;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpvString;

            var attrValue1 = new SvgSpvString(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpvFontSize FontSize
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.FontSize;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpvFontSize;

            var attrValue1 = new SvgSpvFontSize(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpvNumber FontSizeAdjust
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.FontSizeAdjust;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpvNumber;

            var attrValue1 = new SvgSpvNumber(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpv<SvgValueFontStretch> FontStretch
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.FontStretch;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpv<SvgValueFontStretch>;

            var attrValue1 = new SvgSpv<SvgValueFontStretch>(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpv<SvgValueFontStyle> FontStyle
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.FontStyle;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpv<SvgValueFontStyle>;

            var attrValue1 = new SvgSpv<SvgValueFontStyle>(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpv<SvgValueFontVariant> FontVariant
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.FontVariant;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpv<SvgValueFontVariant>;

            var attrValue1 = new SvgSpv<SvgValueFontVariant>(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpv<SvgValueFontWeight> FontWeight
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.FontWeight;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpv<SvgValueFontWeight>;

            var attrValue1 = new SvgSpv<SvgValueFontWeight>(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }
    #endregion

    #region Text properties https://www.w3.org/TR/SVG11/text.html
    public SvgSpv<SvgValueTextDirection> Direction
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.Direction;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpv<SvgValueTextDirection>;

            var attrValue1 = new SvgSpv<SvgValueTextDirection>(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpvLength LetterSpacing
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.LetterSpacing;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpvLength;

            var attrValue1 = new SvgSpvLength(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpv<SvgValueTextDecoration> TextDecoration
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.TextDecoration;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpv<SvgValueTextDecoration>;

            var attrValue1 = new SvgSpv<SvgValueTextDecoration>(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpv<SvgValueUnicodeBidi> UnicodeBidi
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.UnicodeBidi;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpv<SvgValueUnicodeBidi>;

            var attrValue1 = new SvgSpv<SvgValueUnicodeBidi>(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpvLength WordSpacing
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.WordSpacing;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpvLength;

            var attrValue1 = new SvgSpvLength(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpvString AlignmentBaseline
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.AlignmentBaseline;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpvString;

            var attrValue1 = new SvgSpvString(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpvString BaselineShift
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.BaselineShift;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpvString;

            var attrValue1 = new SvgSpvString(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpvString DominantBaseline
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.DominantBaseline;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpvString;

            var attrValue1 = new SvgSpvString(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpvString GlyphOrientationHorizontal
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.GlyphOrientationHorizontal;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpvString;

            var attrValue1 = new SvgSpvString(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpvString GlyphOrientationVertical
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.GlyphOrientationVertical;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpvString;

            var attrValue1 = new SvgSpvString(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpvString Kerning
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.Kerning;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpvString;

            var attrValue1 = new SvgSpvString(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpvString TextAnchor
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.TextAnchor;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpvString;

            var attrValue1 = new SvgSpvString(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpvString WritingMode
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.WritingMode;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpvString;

            var attrValue1 = new SvgSpvString(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }
    #endregion

    #region Other properties for visual media
    public SvgSpvString Clip
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.Clip;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpvString;

            var attrValue1 = new SvgSpvString(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpvString Color
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.Color;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpvString;

            var attrValue1 = new SvgSpvString(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpvString Cursor
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.Cursor;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpvString;

            var attrValue1 = new SvgSpvString(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpvString Display
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.Display;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpvString;

            var attrValue1 = new SvgSpvString(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpvString Overflow
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.Overflow;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpvString;

            var attrValue1 = new SvgSpvString(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpvString Visibility
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.Visibility;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpvString;

            var attrValue1 = new SvgSpvString(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }
    #endregion

    #region Clipping, Masking and Compositing properties https://www.w3.org/TR/SVG11/masking.html
    public SvgSpvString ClipPath
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.ClipPath;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpvString;

            var attrValue1 = new SvgSpvString(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpvString ClipRule
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.ClipRule;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpvString;

            var attrValue1 = new SvgSpvString(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpvString Mask
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.Mask;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpvString;

            var attrValue1 = new SvgSpvString(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpvString Opacity
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.Opacity;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpvString;

            var attrValue1 = new SvgSpvString(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }
    #endregion

    #region Filter Effects properties https://www.w3.org/TR/SVG11/filters.html
    public SvgSpvString EnableBackground
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.EnableBackground;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpvString;

            var attrValue1 = new SvgSpvString(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpvString Filter
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.Filter;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpvString;

            var attrValue1 = new SvgSpvString(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpvString FloodColor
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.FloodColor;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpvString;

            var attrValue1 = new SvgSpvString(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpvString FloodOpacity
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.FloodOpacity;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpvString;

            var attrValue1 = new SvgSpvString(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpvString LightingColor
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.LightingColor;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpvString;

            var attrValue1 = new SvgSpvString(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }
    #endregion

    #region Gradient properties https://www.w3.org/TR/SVG11/pservers.html#Gradients
    public SvgSpvColor StopColor
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.StopColor;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpvColor;

            var attrValue1 = new SvgSpvColor(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpvNumber StopOpacity
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.StopOpacity;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpvNumber;

            var attrValue1 = new SvgSpvNumber(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }
    #endregion

    #region Interactivity properties https://www.w3.org/TR/SVG11/interact.html
    public SvgSpvString PointerEvents
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.PointerEvents;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpvString;

            var attrValue1 = new SvgSpvString(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }
    #endregion

    #region Color and Painting properties https://www.w3.org/TR/SVG11/color.html https://www.w3.org/TR/SVG11/painting.html
    public SvgSpv<SvgValueColorInterpolation> ColorInterpolation
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.ColorInterpolation;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpv<SvgValueColorInterpolation>;

            var attrValue1 = new SvgSpv<SvgValueColorInterpolation>(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpv<SvgValueColorInterpolationFilters> ColorInterpolationFilters
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.ColorInterpolationFilters;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpv<SvgValueColorInterpolationFilters>;

            var attrValue1 = new SvgSpv<SvgValueColorInterpolationFilters>(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpvString ColorProfile
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.ColorProfile;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpvString;

            var attrValue1 = new SvgSpvString(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpv<SvgValueColorRendering> ColorRendering
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.ColorRendering;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpv<SvgValueColorRendering>;

            var attrValue1 = new SvgSpv<SvgValueColorRendering>(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpvPaint Fill
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.Fill;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpvPaint;

            var attrValue1 = new SvgSpvPaint(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpvNumber FillOpacity
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.FillOpacity;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpvNumber;

            var attrValue1 = new SvgSpvNumber(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpv<SvgValueFillRule> FillRule
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.FillRule;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpv<SvgValueFillRule>;

            var attrValue1 = new SvgSpv<SvgValueFillRule>(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpv<SvgValueImageRendering> ImageRendering
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.ImageRendering;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpv<SvgValueImageRendering>;

            var attrValue1 = new SvgSpv<SvgValueImageRendering>(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpvMarker Marker
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.Marker;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpvMarker;

            var attrValue1 = new SvgSpvMarker(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpvMarker MarkerEnd
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.MarkerEnd;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpvMarker;

            var attrValue1 = new SvgSpvMarker(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpvMarker MarkerMid
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.MarkerMid;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpvMarker;

            var attrValue1 = new SvgSpvMarker(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpvMarker MarkerStart
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.MarkerStart;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpvMarker;

            var attrValue1 = new SvgSpvMarker(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpv<SvgValueShapeRendering> ShapeRendering
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.ShapeRendering;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpv<SvgValueShapeRendering>;

            var attrValue1 = new SvgSpv<SvgValueShapeRendering>(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpvPaint Stroke
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.Stroke;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpvPaint;

            var attrValue1 = new SvgSpvPaint(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpvString StrokeDashArray
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.StrokeDashArray;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpvString;

            var attrValue1 = new SvgSpvString(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpvLength StrokeDashOffset
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.StrokeDashOffset;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpvLength;

            var attrValue1 = new SvgSpvLength(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpv<SvgValueStrokeLineCap> StrokeLineCap
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.StrokeLineCap;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpv<SvgValueStrokeLineCap>;

            var attrValue1 = new SvgSpv<SvgValueStrokeLineCap>(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpv<SvgValueStrokeLineJoin> StrokeLineJoin
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.StrokeLineJoin;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpv<SvgValueStrokeLineJoin>;

            var attrValue1 = new SvgSpv<SvgValueStrokeLineJoin>(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpvNumber StrokeMiterLimit
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.StrokeMiterLimit;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpvNumber;

            var attrValue1 = new SvgSpvNumber(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpvNumber StrokeOpacity
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.StrokeOpacity;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpvNumber;

            var attrValue1 = new SvgSpvNumber(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpvLength StrokeWidth
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.StrokeWidth;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpvLength;

            var attrValue1 = new SvgSpvLength(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgSpv<SvgValueTextRendering> TextRendering
    {
        get
        {
            IsValueComputed = true;
            var attrInfo = SvgAttributeUtils.TextRendering;

            if (_propertiesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgSpv<SvgValueTextRendering>;

            var attrValue1 = new SvgSpv<SvgValueTextRendering>(this, attrInfo);
            _propertiesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }
    #endregion


    private SvgStyle()
        : base(null, SvgAttributeUtils.Style)
    {
    }

    private SvgStyle(SvgElement parentElement)
        : base(parentElement, SvgAttributeUtils.Style)
    {
    }


    public SvgStyle ClearProperties()
    {
        _propertiesTable.Clear();

        IsValueComputed = true;

        return this;
    }

    public SvgStyle ClearProperty(SvgAttributeInfo propertyInfo)
    {
        _propertiesTable.Remove(propertyInfo.Id);

        IsValueComputed = true;

        return this;
    }

    public SvgStyle ClearProperties(params SvgAttributeInfo[] propertyInfosList)
    {
        foreach (var attributeInfo in propertyInfosList)
            _propertiesTable.Remove(attributeInfo.Id);

        IsValueComputed = true;

        return this;
    }

    public SvgStyle ClearProperties(IEnumerable<SvgAttributeInfo> propertyInfosList)
    {
        foreach (var attributeInfo in propertyInfosList)
            _propertiesTable.Remove(attributeInfo.Id);

        IsValueComputed = true;

        return this;
    }

    public SvgStyle ClearDefaultProperties()
    {
        var attrIDs =
            _propertiesTable
                .Where(pair => pair.Value.IsNullOrDefault())
                .Select(pair => pair.Key);

        foreach (var attrId in attrIDs)
            _propertiesTable.Remove(attrId);

        IsValueComputed = true;

        return this;
    }


    public string GetInlineValueText()
    {
        return _propertiesTable.Count == 0 
            ? string.Empty 
            : ActivePropertyValues.Concatenate(" ");
    }

    public string GetActivePropertyValuesText(string selectorName)
    {
        var composer = new LinearTextComposer();

        composer
            .Append(string.IsNullOrEmpty(selectorName) ? "*" : selectorName)
            .AppendLine(" {")
            .IncreaseIndentation();

        foreach (var pair in _propertiesTable)
            composer.AppendAtNewLine(pair.Value.ToString());

        composer
            .DecreaseIndentation()
            .AppendAtNewLine("}");

        return composer.ToString();
    }

    public bool ContainsProperty(SvgAttributeInfo propertyInfo)
    {
        return _propertiesTable.TryGetValue(propertyInfo.Id, out var propertyValue) &&
               !ReferenceEquals(propertyValue, null) &&
               !string.IsNullOrEmpty(propertyValue.ValueText);
    }

    public bool TryGetPropertyValue(SvgAttributeInfo propertyInfo, out SvgStylePropertyValue propertyValue)
    {
        if (
            !_propertiesTable.TryGetValue(propertyInfo.Id, out var result) || 
            ReferenceEquals(result, null) ||
            string.IsNullOrEmpty(result.ValueText)
        )
        {
            propertyValue = null;
            return false;
        }

        propertyValue = result;
        return true;
    }

    public SvgStyle UpdateFromActiveProperties(ISvgStyle sourceStyle)
    {
        return UpdateFromProperties(sourceStyle.BaseStyle, sourceStyle.ActivePropertyInfos);
    }

    public SvgStyle UpdateFromProperties(SvgStyle sourceStyle, IEnumerable<SvgAttributeInfo> propertyInfosList)
    {
        foreach (var propertyInfo in propertyInfosList)
        {
            if (!sourceStyle.TryGetPropertyValue(propertyInfo, out var propertyValue))
            {
                _propertiesTable.Remove(propertyInfo.Id);
                continue;
            }

            if (_propertiesTable.ContainsKey(propertyInfo.Id))
                _propertiesTable[propertyInfo.Id].UpdateFrom(propertyValue);
            else
                _propertiesTable.Add(propertyInfo.Id, propertyValue.CreateCopy());
        }

        IsValueComputed = true;

        return this;
    }

    public override ISvgAttributeValue CreateCopy()
    {
        throw new NotImplementedException();
    }

    public override ISvgAttributeValue UpdateFrom(ISvgAttributeValue sourceAttributeValue)
    {
        throw new NotImplementedException();
    }
}