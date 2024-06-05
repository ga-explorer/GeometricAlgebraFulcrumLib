using GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Attributes;
using GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Elements.Categories;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Elements.Containers;

/// <summary>
/// The 'symbol' element is used to define graphical template objects which can be instantiated
/// by a 'use' element. The use of symbol elements for graphics that are used multiple times in
/// the same document adds structure and semantics. Documents that are rich in structure may be
/// rendered graphically, as speech, or as Braille, and thus promote accessibility. Note that a
/// symbol element itself is not rendered. Only instances of a symbol element (i.e., a reference
/// to a symbol by a 'use' element) are rendered.
/// http://docs.w3cub.com/svg/element/symbol/
/// </summary>
public sealed class SvgElementSymbol : SvgElement, ISvgContainerElement
{
    public static SvgElementSymbol Create()
    {
        return new SvgElementSymbol();
    }

    public static SvgElementSymbol Create(string id)
    {
        return new SvgElementSymbol() { Id = id };
    }


    public override string ElementName => "symbol";


    //public SvgEavString<SvgElementSymbol> Id
    //{
    //    get
    //    {
    //        var attrInfo = SvgAttributes.Id;

    //        ISvgAttributeValue attrValue;
    //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
    //            return attrValue as SvgEavString<SvgElementSymbol>;

    //        var attrValue1 = new SvgEavString<SvgElementSymbol>(this, attrInfo);
    //        AttributesTable.Add(attrInfo.Id, attrValue1);

    //        return attrValue1;
    //    }
    //}

    public SvgEavString<SvgElementSymbol> XmlBase
    {
        get
        {
            var attrInfo = SvgAttributeUtils.XmlBase;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEavString<SvgElementSymbol>;

            var attrValue1 = new SvgEavString<SvgElementSymbol>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgEavString<SvgElementSymbol> XmlLanguage
    {
        get
        {
            var attrInfo = SvgAttributeUtils.XmlLanguage;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEavString<SvgElementSymbol>;

            var attrValue1 = new SvgEavString<SvgElementSymbol>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgEavStruct<bool, SvgElementSymbol> ExternalResourcesRequired
    {
        get
        {
            var attrInfo = SvgAttributeUtils.ExternalResourcesRequired;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEavStruct<bool, SvgElementSymbol>;

            var attrValue1 = new SvgEavStruct<bool, SvgElementSymbol>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgEavString<SvgElementSymbol> Class
    {
        get
        {
            var attrInfo = SvgAttributeUtils.Class;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEavString<SvgElementSymbol>;

            var attrValue1 = new SvgEavString<SvgElementSymbol>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    //public SvgEavStyle<SvgElementSymbol> Style
    //{
    //    get
    //    {
    //        var attrInfo = SvgAttributes.Style;

    //        ISvgAttributeValue attrValue;
    //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
    //            return attrValue as SvgEavStyle<SvgElementSymbol>;

    //        var attrValue1 = new SvgEavStyle<SvgElementSymbol>(this);
    //        AttributesTable.Add(attrInfo.Id, attrValue1);

    //        return attrValue1;
    //    }
    //}

    public SvgEavViewBox<SvgElementSymbol> ViewBox
    {
        get
        {
            var attrInfo = SvgAttributeUtils.ViewBox;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEavViewBox<SvgElementSymbol>;

            var attrValue1 = new SvgEavViewBox<SvgElementSymbol>(this);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgEavPreserveAspectRatio<SvgElementSymbol> PreserveAspectRatio
    {
        get
        {
            var attrInfo = SvgAttributeUtils.PreserveAspectRatio;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEavPreserveAspectRatio<SvgElementSymbol>;

            var attrValue1 = new SvgEavPreserveAspectRatio<SvgElementSymbol>(this);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }


    private SvgElementSymbol()
    {
    }
}