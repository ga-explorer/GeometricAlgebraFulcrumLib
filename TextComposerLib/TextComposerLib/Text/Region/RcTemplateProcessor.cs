namespace TextComposerLib.Text.Region
{
    /// <summary>
    /// This class can be used to operate on a region template to change any of its contents
    /// </summary>
    public abstract class RcTemplateProcessor
    {
        /// <summary>
        /// Implement this method to make any desired changes in the given fixed region
        /// </summary>
        /// <param name="region"></param>
        protected abstract void Visit(RcFixedRegion region);

        /// <summary>
        /// Implement this method to make any desired changes in the given slot region
        /// </summary>
        /// <param name="region"></param>
        protected abstract void Visit(RcSlotRegion region);

        /// <summary>
        /// Implement this method to make any desired changes in the given fixed tag
        /// </summary>
        /// <param name="tag"></param>
        protected abstract void Visit(RcFixedTag tag);

        /// <summary>
        /// Implement this method to make any desired changes in the given slot tag
        /// </summary>
        /// <param name="tag"></param>
        protected abstract void Visit(RcSlotTag tag);

        /// <summary>
        /// Visit all regions and tags in the given template making changes to the template
        /// that include, but not limited to, generating text based on tag strings inside slot tags
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public virtual RcTemplate TransformTemplate(RcTemplate template)
        {
            foreach (var region in template.Regions)
            {
                var slotRegion = region as RcSlotRegion;
                if (slotRegion != null)
                {
                    //Visit a slot region
                    Visit(slotRegion);

                    foreach (var tag in slotRegion.Tags)
                    {
                        var slotTag = tag as RcSlotTag;
                        if (slotTag != null)
                            //Visit a slot tag
                            Visit(slotTag);

                        else
                            //Visit a fixed text tag
                            Visit((RcFixedTag) tag);
                    }
                }
                else
                    //Visit a fixed-text region
                    Visit((RcFixedRegion) region);
            }

            return template;
        }
    }
}
