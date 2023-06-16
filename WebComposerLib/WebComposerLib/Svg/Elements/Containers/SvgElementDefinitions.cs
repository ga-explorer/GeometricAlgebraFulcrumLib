using WebComposerLib.Svg.Attributes;
using WebComposerLib.Svg.Elements.Categories;
using WebComposerLib.Svg.Transforms;

namespace WebComposerLib.Svg.Elements.Containers
{
    /// <summary>
    /// SVG allows graphical objects to be defined for later reuse. It is recommended that, wherever
    /// possible, referenced elements be defined inside of a 'defs' element. Objects created inside
    /// a 'defs' element are not rendered immediately; instead, think of them as templates or macros
    /// created for future use.
    /// Defining these elements inside of a 'defs' element promotes understandability of the SVG
    /// content and thus promotes accessibility. You can use a 'use' element to render those elements
    /// wherever you want in the viewport.
    /// You can also use 'defs' to create gradients for later use; see the example provided for the x1
    /// attribute for an example.
    /// http://docs.w3cub.com/svg/element/defs/
    /// </summary>
    public sealed class SvgElementDefinitions : SvgElement, ISvgContainerElement
    {
        public static SvgElementDefinitions Create()
        {
            return new SvgElementDefinitions();
        }

        public static SvgElementDefinitions Create(string id)
        {
            return new SvgElementDefinitions() { Id = id };
        }


        public override string ElementName => "defs";


        //public SvgEavString<SvgElementDefinitions> Id
        //{
        //    get
        //    {
        //        var attrInfo = SvgAttributes.Id;

        //        ISvgAttributeValue attrValue;
        //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
        //            return attrValue as SvgEavString<SvgElementDefinitions>;

        //        var attrValue1 = new SvgEavString<SvgElementDefinitions>(this, attrInfo);
        //        AttributesTable.Add(attrInfo.Id, attrValue1);

        //        return attrValue1;
        //    }
        //}

        public SvgEavString<SvgElementDefinitions> XmlBase
        {
            get
            {
                var attrInfo = SvgAttributeUtils.XmlBase;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavString<SvgElementDefinitions>;

                var attrValue1 = new SvgEavString<SvgElementDefinitions>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementDefinitions> XmlLanguage
        {
            get
            {
                var attrInfo = SvgAttributeUtils.XmlLanguage;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavString<SvgElementDefinitions>;

                var attrValue1 = new SvgEavString<SvgElementDefinitions>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavStruct<bool, SvgElementDefinitions> ExternalResourcesRequired
        {
            get
            {
                var attrInfo = SvgAttributeUtils.ExternalResourcesRequired;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavStruct<bool, SvgElementDefinitions>;

                var attrValue1 = new SvgEavStruct<bool, SvgElementDefinitions>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementDefinitions> Class
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Class;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavString<SvgElementDefinitions>;

                var attrValue1 = new SvgEavString<SvgElementDefinitions>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        //public SvgEavStyle<SvgElementDefinitions> Style
        //{
        //    get
        //    {
        //        var attrInfo = SvgAttributes.Style;

        //        ISvgAttributeValue attrValue;
        //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
        //            return attrValue as SvgEavStyle<SvgElementDefinitions>;

        //        var attrValue1 = new SvgEavStyle<SvgElementDefinitions>(this);
        //        AttributesTable.Add(attrInfo.Id, attrValue1);

        //        return attrValue1;
        //    }
        //}

        public SvgEav<SvgTransform, SvgElementDefinitions> Transform
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Transform;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEav<SvgTransform, SvgElementDefinitions>;

                var attrValue1 = new SvgEav<SvgTransform, SvgElementDefinitions>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }


        private SvgElementDefinitions()
        {
        }
    }
}