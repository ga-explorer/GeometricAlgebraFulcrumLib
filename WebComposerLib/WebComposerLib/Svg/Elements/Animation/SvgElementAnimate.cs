using WebComposerLib.Svg.Attributes;
using WebComposerLib.Svg.Elements.Categories;
using WebComposerLib.Svg.Values;

namespace WebComposerLib.Svg.Elements.Animation
{
    /// <summary>
    /// The 'animate' SVG element is used to animate an attribute or property of an element over time.
    /// It's normally inserted inside the element or referenced by the href attribute of the target element.
    /// http://docs.w3cub.com/svg/element/animate/
    /// https://www.w3.org/TR/smil-animation/
    /// </summary>
    public sealed class SvgElementAnimate : SvgElement, ISvgAnimationElement
    {
        public static SvgElementAnimate Create()
        {
            return new SvgElementAnimate();
        }

        public static SvgElementAnimate Create(string id)
        {
            return new SvgElementAnimate() { Id = id };
        }


        public override string ElementName => "animate";


        //public SvgEavString<SvgElementAnimate> Id
        //{
        //    get
        //    {
        //        var attrInfo = SvgAttributes.Id;

        //        ISvgAttributeValue attrValue;
        //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
        //            return attrValue as SvgEavString<SvgElementAnimate>;

        //        var attrValue1 = new SvgEavString<SvgElementAnimate>(this, attrInfo);
        //        AttributesTable.Add(attrInfo.Id, attrValue1);

        //        return attrValue1;
        //    }
        //}

        public SvgEavString<SvgElementAnimate> XmlBase
        {
            get
            {
                var attrInfo = SvgAttributeUtils.XmlBase;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavString<SvgElementAnimate>;

                var attrValue1 = new SvgEavString<SvgElementAnimate>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementAnimate> XmlLanguage
        {
            get
            {
                var attrInfo = SvgAttributeUtils.XmlLanguage;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavString<SvgElementAnimate>;

                var attrValue1 = new SvgEavString<SvgElementAnimate>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavStruct<bool, SvgElementAnimate> ExternalResourcesRequired
        {
            get
            {
                var attrInfo = SvgAttributeUtils.ExternalResourcesRequired;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavStruct<bool, SvgElementAnimate>;

                var attrValue1 = new SvgEavStruct<bool, SvgElementAnimate>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavAttribute<SvgElementAnimate> Attribute
        {
            get
            {
                var attrInfo = SvgAttributeUtils.AttributeName;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavAttribute<SvgElementAnimate>;

                var attrValue1 = new SvgEavAttribute<SvgElementAnimate>(this);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEav<SvgValueAttributeType, SvgElementAnimate> AttributeType
        {
            get
            {
                var attrInfo = SvgAttributeUtils.AttributeType;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEav<SvgValueAttributeType, SvgElementAnimate>;

                var attrValue1 = new SvgEav<SvgValueAttributeType, SvgElementAnimate>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementAnimate> From
        {
            get
            {
                var attrInfo = SvgAttributeUtils.From;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavString<SvgElementAnimate>;

                var attrValue1 = new SvgEavString<SvgElementAnimate>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementAnimate> To
        {
            get
            {
                var attrInfo = SvgAttributeUtils.To;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavString<SvgElementAnimate>;

                var attrValue1 = new SvgEavString<SvgElementAnimate>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementAnimate> By
        {
            get
            {
                var attrInfo = SvgAttributeUtils.By;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavString<SvgElementAnimate>;

                var attrValue1 = new SvgEavString<SvgElementAnimate>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementAnimate> Values
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Values;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavString<SvgElementAnimate>;

                var attrValue1 = new SvgEavString<SvgElementAnimate>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavClock<SvgElementAnimate> Duration
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Duration;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavClock<SvgElementAnimate>;

                var attrValue1 = new SvgEavClock<SvgElementAnimate>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavClock<SvgElementAnimate> RepeatDuration
        {
            get
            {
                var attrInfo = SvgAttributeUtils.RepeatDuration;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavClock<SvgElementAnimate>;

                var attrValue1 = new SvgEavClock<SvgElementAnimate>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavClock<SvgElementAnimate> MinDuration
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Min;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavClock<SvgElementAnimate>;

                var attrValue1 = new SvgEavClock<SvgElementAnimate>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavClock<SvgElementAnimate> MaxDuration
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Max;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavClock<SvgElementAnimate>;

                var attrValue1 = new SvgEavClock<SvgElementAnimate>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavNumber<SvgElementAnimate> RepeatCount
        {
            get
            {
                var attrInfo = SvgAttributeUtils.RepeatCount;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavNumber<SvgElementAnimate>;

                var attrValue1 = new SvgEavNumber<SvgElementAnimate>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEav<SvgValueAnimationFill, SvgElementAnimate> Fill
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Fill;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEav<SvgValueAnimationFill, SvgElementAnimate>;

                var attrValue1 = new SvgEav<SvgValueAnimationFill, SvgElementAnimate>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEav<SvgValueAnimationAdditive, SvgElementAnimate> Additive
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Additive;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEav<SvgValueAnimationAdditive, SvgElementAnimate>;

                var attrValue1 = new SvgEav<SvgValueAnimationAdditive, SvgElementAnimate>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEav<SvgValueAnimationAccumulate, SvgElementAnimate> Accumulate
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Accumulate;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEav<SvgValueAnimationAccumulate, SvgElementAnimate>;

                var attrValue1 = new SvgEav<SvgValueAnimationAccumulate, SvgElementAnimate>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEav<SvgValueAnimationRestart, SvgElementAnimate> Restart
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Restart;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEav<SvgValueAnimationRestart, SvgElementAnimate>;

                var attrValue1 = new SvgEav<SvgValueAnimationRestart, SvgElementAnimate>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEav<SvgValueAnimationCalcMode, SvgElementAnimate> CalcMode
        {
            get
            {
                var attrInfo = SvgAttributeUtils.CalcMode;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEav<SvgValueAnimationCalcMode, SvgElementAnimate>;

                var attrValue1 = new SvgEav<SvgValueAnimationCalcMode, SvgElementAnimate>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEav<SvgValueLengthsList, SvgElementAnimate> KeyTimes
        {
            get
            {
                var attrInfo = SvgAttributeUtils.KeyTimes;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEav<SvgValueLengthsList, SvgElementAnimate>;

                var attrValue1 = new SvgEav<SvgValueLengthsList, SvgElementAnimate>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        //TODO: Implement the begin and end attributes http://docs.w3cub.com/svg/attribute/begin/
        //TODO: Implement the keySplines attribute http://docs.w3cub.com/svg/attribute/keysplines/http://docs.w3cub.com/svg/attribute/keysplines/

        private SvgElementAnimate()
        {
        }
    }
}