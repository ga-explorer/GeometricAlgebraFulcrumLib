using GraphicsComposerLib.Rendering.GraphViz.Dot.Color;
using GraphicsComposerLib.Rendering.GraphViz.Dot.Image;
using GraphicsComposerLib.Rendering.GraphViz.Dot.Label;
using GraphicsComposerLib.Rendering.GraphViz.Dot.Label.Text;
using GraphicsComposerLib.Rendering.GraphViz.Dot.Value;
using GraphicsComposerLib.Rendering.LaTeX.ImageComposers;
using TextComposerLib;
using TextComposerLib.Text;

namespace GraphicsComposerLib.Rendering.GraphViz.Dot
{
    /// <summary>
    /// This class represents a top-level main graph in the dot AST. The user can extend
    /// this class by inheritance to include additional features in the graph like
    /// certain styles of nodes, common structures, sub graphs, etc.
    /// See  http://www.graphviz.org/content/dot-language 
    /// and http://www.graphviz.org/content/attrs for more details
    /// </summary>
    public class DotGraph : IDotGraph
    {
        /// <summary>
        /// Create a new undirected graph with no name
        /// </summary>
        /// <returns></returns>
        public static DotGraph Undirected()
        {
            return new DotGraph(DotGraphType.Undirected, "");
        }

        /// <summary>
        /// Create a new directed graph with no name
        /// </summary>
        /// <returns></returns>
        public static DotGraph Directed()
        {
            return new DotGraph(DotGraphType.Directed, "");
        }

        /// <summary>
        /// Create a new strictly directed graph with no name
        /// </summary>
        /// <returns></returns>
        public static DotGraph StrictDirected()
        {
            return new DotGraph(DotGraphType.StrictDirected, "");
        }

        /// <summary>
        /// Create a new undirected graph
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static DotGraph Undirected(string name)
        {
            return new DotGraph(DotGraphType.Undirected, name);
        }

        /// <summary>
        /// Create a new directed graph with no name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static DotGraph Directed(string name)
        {
            return new DotGraph(DotGraphType.Directed, name);
        }

        /// <summary>
        /// Create a new strictly directed graph with no name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static DotGraph StrictDirected(string name)
        {
            return new DotGraph(DotGraphType.StrictDirected, name);
        }



        /// <summary>
        /// This is used as a cache for node statements only. Any node defined inside an edge will 
        /// not be present here
        /// </summary>
        internal readonly Dictionary<string, DotNode> NodesCache 
            = new Dictionary<string, DotNode>();

        internal readonly List<IDotStatement> StatementsList 
            = new List<IDotStatement>();

        internal Dictionary<string, IDotGraphImage> ImagesCache
            = new Dictionary<string, IDotGraphImage>();


        /// <summary>
        /// The type of this graph
        /// </summary>
        public DotGraphType GraphType { get; }

        /// <summary>
        /// The name of this graph
        /// </summary>
        public string GraphName { get; private set; }


        public IEnumerable<IDotStatement> Statements 
            => StatementsList;

        public IEnumerable<DotNode> NodeStatements
        {
            get
            {
                return
                    StatementsList
                    .Select(s => s as DotNode)
                    .Where(s => ReferenceEquals(s, null) == false && s.IsNode);
            }
        }

        public DotNode FirstNodeStatement
        {
            get
            {
                return
                    StatementsList
                    .Select(s => s as DotNode)
                    .FirstOrDefault(s => ReferenceEquals(s, null) == false && s.IsNode);
            }
        }

        public DotNode LastNodeStatement
        {
            get
            {
                return
                    StatementsList
                    .Select(s => s as DotNode)
                    .LastOrDefault(s => ReferenceEquals(s, null) == false && s.IsNode);
            }
        }

        public IEnumerable<DotNode> NodeDefaultsStatements
        {
            get
            {
                return
                    StatementsList
                    .Select(s => s as DotNode)
                    .Where(s => ReferenceEquals(s, null) == false && s.IsNodeDefaults);
            }
        }

        public DotNode FirstNodeDefaultsStatement
        {
            get
            {
                return
                    StatementsList
                    .Select(s => s as DotNode)
                    .FirstOrDefault(s => ReferenceEquals(s, null) == false && s.IsNodeDefaults);
            }
        }

        public DotNode LastNodeDefaultsStatement
        {
            get
            {
                return
                    StatementsList
                    .Select(s => s as DotNode)
                    .LastOrDefault(s => ReferenceEquals(s, null) == false && s.IsNodeDefaults);
            }
        }

        public IEnumerable<DotEdge> EdgeStatements
        {
            get
            {
                return
                    StatementsList
                    .Select(s => s as DotEdge)
                    .Where(s => ReferenceEquals(s, null) == false && s.IsEdge);
            }
        }

        public DotEdge FirstEdgeStatement
        {
            get
            {
                return
                    StatementsList
                    .Select(s => s as DotEdge)
                    .FirstOrDefault(s => ReferenceEquals(s, null) == false && s.IsEdge);
            }
        }

        public DotEdge LastEdgeStatement
        {
            get
            {
                return
                    StatementsList
                    .Select(s => s as DotEdge)
                    .LastOrDefault(s => ReferenceEquals(s, null) == false && s.IsEdge);
            }
        }

        public IEnumerable<DotEdge> EdgeDefaultsStatements
        {
            get
            {
                return
                    StatementsList
                    .Select(s => s as DotEdge)
                    .Where(s => ReferenceEquals(s, null) == false && s.IsEdgeDefaults);
            }
        }

        public DotEdge FirstEdgeDefaultsStatement
        {
            get
            {
                return
                    StatementsList
                    .Select(s => s as DotEdge)
                    .FirstOrDefault(s => ReferenceEquals(s, null) == false && s.IsEdgeDefaults);
            }
        }

        public DotEdge LastEdgeDefaultsStatement
        {
            get
            {
                return
                    StatementsList
                    .Select(s => s as DotEdge)
                    .LastOrDefault(s => ReferenceEquals(s, null) == false && s.IsEdgeDefaults);
            }
        }

        public IEnumerable<DotSubGraph> SubGraphStatements
        {
            get
            {
                return
                    StatementsList
                    .Select(s => s as DotSubGraph)
                    .Where(s => ReferenceEquals(s, null) == false);
            }
        }

        public DotSubGraph FirstSubGraphStatement
        {
            get
            {
                return
                    StatementsList
                    .Select(s => s as DotSubGraph)
                    .FirstOrDefault(s => ReferenceEquals(s, null) == false);
            }
        }

        public DotSubGraph LastSubGraphStatement
        {
            get
            {
                return
                    StatementsList
                    .Select(s => s as DotSubGraph)
                    .LastOrDefault(s => ReferenceEquals(s, null) == false);
            }
        }

        public IEnumerable<DotSubGraph> ClusterStatements
        {
            get
            {
                return
                    StatementsList
                    .Select(s => s as DotSubGraph)
                    .Where(s => ReferenceEquals(s, null) == false && s.IsCluster);
            }
        }

        public DotSubGraph FirstClusterStatement
        {
            get
            {
                return
                    StatementsList
                    .Select(s => s as DotSubGraph)
                    .FirstOrDefault(s => ReferenceEquals(s, null) == false && s.IsCluster);
            }
        }

        public DotSubGraph LastClusterStatement
        {
            get
            {
                return
                    StatementsList
                    .Select(s => s as DotSubGraph)
                    .LastOrDefault(s => ReferenceEquals(s, null) == false && s.IsCluster);
            }
        }

        public IEnumerable<DotSubGraph> NonClusterStatements
        {
            get
            {
                return
                    StatementsList
                    .Select(s => s as DotSubGraph)
                    .Where(s => ReferenceEquals(s, null) == false && s.IsCluster == false);
            }
        }

        public DotSubGraph FirstNonClusterStatement
        {
            get
            {
                return
                    StatementsList
                    .Select(s => s as DotSubGraph)
                    .FirstOrDefault(s => ReferenceEquals(s, null) == false && s.IsCluster == false);
            }
        }

        public DotSubGraph LastNonClusterStatement
        {
            get
            {
                return
                    StatementsList
                    .Select(s => s as DotSubGraph)
                    .LastOrDefault(s => ReferenceEquals(s, null) == false && s.IsCluster == false);
            }
        }

        public IEnumerable<DotSubGraphDefaults> SubGraphDefaultsStatements
        {
            get
            {
                return
                    StatementsList
                    .Select(s => s as DotSubGraphDefaults)
                    .Where(s => ReferenceEquals(s, null) == false);
            }
        }

        public DotSubGraphDefaults FirstSubGraphDefaultsStatement
        {
            get
            {
                return
                    StatementsList
                    .Select(s => s as DotSubGraphDefaults)
                    .FirstOrDefault(s => ReferenceEquals(s, null) == false);
            }
        }

        public DotSubGraphDefaults LastSubGraphDefaultsStatement
        {
            get
            {
                return
                    StatementsList
                    .Select(s => s as DotSubGraphDefaults)
                    .LastOrDefault(s => ReferenceEquals(s, null) == false);
            }
        }

        public IEnumerable<DotFixedCode> FixedCodeStatements
        {
            get
            {
                return
                    StatementsList
                    .Select(s => s as DotFixedCode)
                    .Where(s => ReferenceEquals(s, null) == false);
            }
        }

        public DotFixedCode FirstFixedCodeStatement
        {
            get
            {
                return
                    StatementsList
                    .Select(s => s as DotFixedCode)
                    .FirstOrDefault(s => ReferenceEquals(s, null) == false);
            }
        }

        public DotFixedCode LastFixedCodeStatement
        {
            get
            {
                return
                    StatementsList
                    .Select(s => s as DotFixedCode)
                    .LastOrDefault(s => ReferenceEquals(s, null) == false);
            }
        }


        public IDotGraph ParentGraph => null;

        public DotGraph ParentAsMainGraph => null;

        public DotSubGraph ParentAsSubGraph => null;

        public DotGraph MainGraph => this;

        public bool IsSubGraph => false;

        public bool IsMainGraph => true;

        public bool IsCluster => false;

        public bool IsNonClusterSubGraph => false;


        internal DotGraph SetAttribute(string attrName, float attrValue)
        {
            StatementsList.Add(new DotGraphAttribute(this, attrName, attrValue.ToDotDouble()));

            return this;
        }

        internal DotGraph SetAttribute(string attrName, string attrValue)
        {
            StatementsList.Add(new DotGraphAttribute(this, attrName, attrValue));

            return this;
        }

        internal DotNode FindOrDefineNode(string nodeName)
        {
            if (NodesCache.TryGetValue(nodeName, out var node))
                return node;

            node = new DotNode(this, nodeName);

            NodesCache.Add(nodeName, node);

            StatementsList.Add(node);

            return node;
        }

        public DotGraphLaTeXImage SetLaTeXImage(string imageId, string latexMath)
        {
            DotGraphLaTeXImage latexImage;

            if (ImagesCache.TryGetValue(imageId, out var image))
            {
                latexImage = image as DotGraphLaTeXImage;

                if (!ReferenceEquals(latexImage, null))
                {
                    latexImage.LaTeXCode = latexMath;

                    return latexImage;
                }

                latexImage = new DotGraphLaTeXImage(imageId)
                {
                    LaTeXCode = latexMath
                };

                ImagesCache[imageId] = latexImage;

                return latexImage;
            }

            latexImage = new DotGraphLaTeXImage(imageId)
            {
                LaTeXCode = latexMath
            };

            ImagesCache.Add(imageId, latexImage);

            return latexImage;
        }


        #region Attribute Accessors
        public DotGraph SetDamping(float value)
        {
            return SetAttribute("Damping", value);
        }

        public DotGraph SetK(float value)
        {
            return SetAttribute("K", value);
        }

        public DotGraph SetUrl(DotHtmlString value)
        {
            return SetAttribute("URL", value.QuotedValue);
        }

        public DotGraph SetBackground(string value)
        {
            return SetAttribute("_background", value.ValueToQuotedLiteral());
        }

        public DotGraph SetBoundingBox(float x1, float y1, float x2, float y2)
        {
            return SetAttribute("bb", DotUtils.ToDotRect(x1, y1, x2, y2));
        }

        public DotGraph SetBackgroundColor(DotColor value)
        {
            return SetAttribute("bgcolor", value.QuotedValue);
        }

        public DotGraph SetBackgroundColor(DotColor c1, DotColor c2)
        {
            return SetAttribute("bgcolor", (c1.Value + ":" + c2.Value).DoubleQuote());
        }

        public DotGraph SetCenter(bool value)
        {
            return SetAttribute("center", value.ToString());
        }

        public DotGraph SetCharSet(string value)
        {
            return SetAttribute("charset", value.ValueToQuotedLiteral());
        }

        public DotGraph SetClusterRank(DotClusterMode value)
        {
            return SetAttribute("clusterrank", value.QuotedValue);
        }

        public DotGraph SetColorScheme(DotColorScheme value)
        {
            return SetAttribute("colorscheme", value.QuotedValue);
        }

        public DotGraph SetComment(string value)
        {
            return SetAttribute("comment", value.ValueToQuotedLiteral());
        }

        public DotGraph SetCompound(bool value)
        {
            return SetAttribute("compound", value.ToString());
        }

        public DotGraph SetConcentrate(bool value)
        {
            return SetAttribute("concentrate", value.ToString());
        }

        public DotGraph SetDefaultDist(double value)
        {
            return SetAttribute("defaultdist", value.ToDotDouble());
        }

        public DotGraph SetLayoutDimensions(int value)
        {
            return SetAttribute("dim", value.ToString());
        }

        public DotGraph SetRenderingDimensions(int value)
        {
            return SetAttribute("dimen", value.ToString());
        }

        public DotGraph SetDirEdgeConstraints(bool value)
        {
            return SetAttribute("diredgeconstraints", value.ToString());
        }

        public DotGraph SetDpi(double value)
        {
            return SetAttribute("dpi", value.ToDotDouble());
        }

        public DotGraph SetEpsilon(double value)
        {
            return SetAttribute("epsilon", value.ToDotDouble());
        }

        public DotGraph SetEdgeSeparationMargins(double value)
        {
            return SetAttribute("esep", value.ToDotDouble());
        }

        public DotGraph SetEdgeSeparationMargins(double x, double y)
        {
            return SetAttribute("esep", DotUtils.ToDotPoint(x, y));
        }

        public DotGraph SetEdgeSeparationMarginsDelta(double value)
        {
            return SetAttribute("esep", value.ToDotSignedDouble());
        }

        public DotGraph SetEdgeSeparationMarginsDelta(double x, double y)
        {
            return SetAttribute("esep", DotUtils.ToDotSignedPoint(x, y));
        }

        public DotGraph SetFontColor(DotColor value)
        {
            return SetAttribute("fontcolor", value.QuotedValue);
        }

        public DotGraph SetFontName(string value)
        {
            return SetAttribute("fontname", value.ValueToQuotedLiteral());
        }

        public DotGraph SetFontNames(string value)
        {
            return SetAttribute("fontnames", value.ValueToQuotedLiteral());
        }

        public DotGraph SetFontPath(string value)
        {
            return SetAttribute("fontpath", value.ValueToQuotedLiteral());
        }

        public DotGraph SetFontSize(float value)
        {
            return SetAttribute("fontsize", value.ToDotDouble().DoubleQuote());
        }

        public DotGraph SetForceLabels(bool value)
        {
            return SetAttribute("forcelabels", value.ToString());
        }

        public DotGraph SetGradientAngle(int value)
        {
            return SetAttribute("gradientangle", value.DoubleQuote());
        }

        public DotGraph SetHRef(DotHtmlString value)
        {
            return SetAttribute("href", value.QuotedValue);
        }

        public DotGraph SetId(DotHtmlString value)
        {
            return SetAttribute("id", value.QuotedValue);
        }

        public DotGraph SetImagePath(string value)
        {
            return SetAttribute("imagepath", value.ValueToQuotedLiteral());
        }

        public DotGraph SetInputScale(double value)
        {
            return SetAttribute("inputscale", value.ToDotDouble());
        }

        public DotGraph SetTeXLabel(string value)
        {
            return SetAttribute("texlbl", value.ValueToQuotedLiteral());
        }

        public DotGraph SetLabel(string value)
        {
            return SetAttribute("label", value.ValueToQuotedLiteral());
        }

        public DotGraph SetLabel(IDotHtmlLabel value)
        {
            return SetAttribute("label", value.TaggedValue);
        }

        public DotGraph SetLabelScheme(int value)
        {
            return SetAttribute("label_scheme", value.ToString());
        }

        public DotGraph SetLabelJustification(DotJustification value)
        {
            return SetAttribute("labeljust", value.Value);
        }

        public DotGraph SetLabelLocation(DotVerticalLocation value)
        {
            return SetAttribute("labelloc", value.QuotedValue);
        }

        public DotGraph SetLandscape(bool value)
        {
            return SetAttribute("landscape", value.ToString());
        }

        public DotGraph SetLayerListSep(string value)
        {
            return SetAttribute("layerlistsep", value.ValueToQuotedLiteral());
        }

        public DotGraph SetLayers(string value)
        {
            return SetAttribute("layers", value.ValueToQuotedLiteral());
        }

        public DotGraph SetLayerSelect(string value)
        {
            return SetAttribute("layerselect", value.ValueToQuotedLiteral());
        }

        public DotGraph SetLayerSep(string value)
        {
            return SetAttribute("layersep", value.ValueToQuotedLiteral());
        }

        public DotGraph SetLayout(string value)
        {
            return SetAttribute("layout", value.ValueToQuotedLiteral());
        }

        public DotGraph SetLevels(int value)
        {
            return SetAttribute("levels", value.ToString());
        }

        public DotGraph SetLevelsGeop(double value)
        {
            return SetAttribute("levelsgap", value.ToDotDouble());
        }

        public DotGraph SetLabelHeight(double value)
        {
            return SetAttribute("lheight", value.ToDotDouble());
        }

        public DotGraph SetLabelPosition(float x, float y)
        {
            return SetAttribute("lp", DotUtils.ToDotPoint(x, y));
        }

        public DotGraph SetLabelPosition(double x, double y)
        {
            return SetAttribute("lp", DotUtils.ToDotPoint(x, y));
        }

        public DotGraph SetLabelWidth(double value)
        {
            return SetAttribute("lwidth", value.ToDotDouble());
        }

        public DotGraph SetMargin(double value)
        {
            return SetAttribute("margin", value.ToDotDouble());
        }

        public DotGraph SetMargin(float x, float y)
        {
            return SetAttribute("margin", DotUtils.ToDotPoint(x, y));
        }

        public DotGraph SetMargin(double x, double y)
        {
            return SetAttribute("margin", DotUtils.ToDotPoint(x, y));
        }

        public DotGraph SetMaxIter(int value)
        {
            return SetAttribute("maxiter", value.ToString());
        }

        public DotGraph SetMcLimit(double value)
        {
            return SetAttribute("mclimit", value.ToDotDouble());
        }

        public DotGraph SetMinDistance(double value)
        {
            return SetAttribute("mindist", value.ToDotDouble());
        }

        public DotGraph SetMode(DotLayoutOptMode value)
        {
            return SetAttribute("mode", value.QuotedValue);
        }

        public DotGraph SetModel(DotDistanceMatrixModel value)
        {
            return SetAttribute("model", value.QuotedValue);
        }

        public DotGraph SetMosek(bool value)
        {
            return SetAttribute("mosek", value.ToString());
        }

        public DotGraph SetNodeSep(double value)
        {
            return SetAttribute("nodesep", value.ToDotDouble());
        }

        public DotGraph SetNoJustify(bool value)
        {
            return SetAttribute("nojustify", value.ToString());
        }

        public DotGraph SetNormalize(double value)
        {
            return SetAttribute("normalize", value.ToDotDouble());
        }

        public DotGraph SetNormalize(bool value)
        {
            return SetAttribute("normalize", value.ToString());
        }

        public DotGraph SetNoTranslate(bool value)
        {
            return SetAttribute("notranslate", value.ToString());
        }

        public DotGraph SetNsLimit(double value)
        {
            return SetAttribute("nslimit", value.ToDotDouble());
        }

        public DotGraph SetOrdering(DotOrdering value)
        {
            return SetAttribute("ordering", value.QuotedValue);
        }

        public DotGraph SetOrientation(string value)
        {
            return SetAttribute("orientation", value.ValueToQuotedLiteral());
        }

        public DotGraph SetOutputOrder(DotOutputOrder value)
        {
            return SetAttribute("outputorder", value.QuotedValue);
        }

        public DotGraph SetOverlap(DotOverlap value)
        {
            return SetAttribute("overlap", value.QuotedValue);
        }

        public DotGraph SetOverlap(DotOverlap value, int prefix)
        {
            return SetAttribute("overlap", prefix + ":" + value.QuotedValue);
        }

        public DotGraph SetOverlap(int prefix)
        {
            return SetAttribute("overlap", prefix + ":");
        }

        public DotGraph SetOverlapPrism(int prefix, int suffix)
        {
            return SetAttribute("overlap", prefix + ":prism" + suffix);
        }

        public DotGraph SetOverlapPrism(int suffix)
        {
            return SetAttribute("overlap", "prism" + suffix);
        }

        public DotGraph SetOverlapScaling(double value)
        {
            return SetAttribute("overlap_scaling", value.ToDotDouble());
        }

        public DotGraph SetOverlapShrink(bool value)
        {
            return SetAttribute("overlap_shrink", value.ToString());
        }

        public DotGraph SetPack(bool value)
        {
            return SetAttribute("pack", value.ToString());
        }

        public DotGraph SetPack(int value)
        {
            return SetAttribute("pack", value.ToString());
        }

        public DotGraph SetPackMode(DotPackMode value)
        {
            return SetAttribute("packmode", value.QuotedValue);
        }

        public DotGraph SetPackModeArray(string flags)
        {
            var flagsText =
                string.IsNullOrEmpty(flags)
                ? string.Empty
                : "_" + flags;

            return SetAttribute("packmode", "array" + flagsText);
        }

        public DotGraph SetPad(double value)
        {
            return SetAttribute("pad", value.ToDotDouble());
        }

        public DotGraph SetPad(float x, float y)
        {
            return SetAttribute("pad", DotUtils.ToDotPoint(x, y));
        }

        public DotGraph SetPad(double x, double y)
        {
            return SetAttribute("pad", DotUtils.ToDotPoint(x, y));
        }

        public DotGraph SetPage(double value)
        {
            return SetAttribute("page", value.ToDotDouble());
        }

        public DotGraph SetPage(float x, float y)
        {
            return SetAttribute("page", DotUtils.ToDotPoint(x, y));
        }

        public DotGraph SetPage(double x, double y)
        {
            return SetAttribute("page", DotUtils.ToDotPoint(x, y));
        }

        public DotGraph SetPageDir(DotPageDirection value)
        {
            return SetAttribute("pagedir", value.QuotedValue);
        }

        public DotGraph SetQuadtree(DotQuadTreeType value)
        {
            return SetAttribute("quadtree", value.QuotedValue);
        }

        public DotGraph SetQuantum(double value)
        {
            return SetAttribute("quantum", value.ToDotDouble());
        }

        public DotGraph SetRankDir(DotRankDirection value)
        {
            return SetAttribute("rankdir", value.QuotedValue);
        }

        public DotGraph SetRankSep(double value, bool equallyFlag)
        {
            var finalValue = value.ToDotDouble() + (equallyFlag ? " equally" : string.Empty);

            return SetAttribute("ranksep", finalValue.ValueToQuotedLiteral());
        }

        public DotGraph SetRankSep(params double[] valueList)
        {
            var finalValue = valueList.Select(v => v.ToDotDouble()).Concatenate(":");

            return SetAttribute("ranksep", finalValue.DoubleQuote());
        }

        public DotGraph SetRatio(double value)
        {
            return SetAttribute("ratio", value.ToDotDouble());
        }

        public DotGraph SetRatio(DotAspectRatio value)
        {
            return SetAttribute("ratio", value.QuotedValue);
        }

        public DotGraph SetReMinCross(bool value)
        {
            return SetAttribute("remincross", value.ToString());
        }

        public DotGraph SetRepulsiveForce(double value)
        {
            return SetAttribute("repulsiveforce", value.ToDotDouble());
        }

        public DotGraph SetResolution(double value)
        {
            return SetAttribute("resolution", value.ToDotDouble());
        }

        public DotGraph SetRoot(string value)
        {
            return SetAttribute("root", value.ValueToQuotedLiteral());
        }

        public DotGraph SetRoot(bool value)
        {
            return SetAttribute("root", value.ToString());
        }

        public DotGraph SetRotate(int value)
        {
            return SetAttribute("rotate", value.ToString());
        }

        public DotGraph SetRotation(double value)
        {
            return SetAttribute("rotation", value.ToDotDouble());
        }

        public DotGraph SetScale(double value)
        {
            return SetAttribute("scale", value.ToDotDouble());
        }

        public DotGraph SetScale(float x, float y)
        {
            return SetAttribute("scale", DotUtils.ToDotPoint(x, y));
        }

        public DotGraph SetScale(double x, double y)
        {
            return SetAttribute("scale", DotUtils.ToDotPoint(x, y));
        }

        public DotGraph SetSearchSize(int value)
        {
            return SetAttribute("searchsize", value.ToString());
        }

        public DotGraph SetNodeMargin(double value)
        {
            return SetAttribute("sep", value.ToDotDouble());
        }

        public DotGraph SetNodeMarginDelta(double value)
        {
            return SetAttribute("sep", value.ToDotSignedDouble());
        }

        public DotGraph SetNodeMargin(float x, float y)
        {
            return SetAttribute("sep", DotUtils.ToDotPoint(x, y));
        }

        public DotGraph SetNodeMargin(double x, double y)
        {
            return SetAttribute("sep", DotUtils.ToDotPoint(x, y));
        }

        public DotGraph SetNodeMarginDelta(float x, float y)
        {
            return SetAttribute("sep", DotUtils.ToDotSignedPoint(x, y));
        }

        public DotGraph SetNodeMarginDelta(double x, double y)
        {
            return SetAttribute("sep", DotUtils.ToDotSignedPoint(x, y));
        }

        public DotGraph SetShowBoxes(int value)
        {
            return SetAttribute("showboxes", value.ToString());
        }

        public DotGraph SetSize(double value)
        {
            var finalValue = value.ToDotDouble();

            return SetAttribute("size", finalValue.DoubleQuote());
        }

        public DotGraph SetSize(double value, bool isDesiredSize)
        {
            var finalValue = value.ToDotDouble() + (isDesiredSize ? "!" : string.Empty);

            return SetAttribute("size", finalValue.DoubleQuote());
        }

        public DotGraph SetSize(double x, double y)
        {
            var finalValue = DotUtils.ToDotPoint(x, y);

            return SetAttribute("size", finalValue.DoubleQuote());
        }

        public DotGraph SetSize(double x, double y, bool isDesiredSize)
        {
            var finalValue = DotUtils.ToDotPoint(x, y) + (isDesiredSize ? "!" : string.Empty);

            return SetAttribute("size", finalValue.DoubleQuote());
        }

        public DotGraph SetSmoothing(DotSmoothType value)
        {
            return SetAttribute("smoothing", value.QuotedValue);
        }

        public DotGraph SetSortVertical(int value)
        {
            return SetAttribute("sortv", value.ToString());
        }

        public DotGraph SetSplines(DotSplines value)
        {
            return SetAttribute("splines", value.Value);
        }

        public DotGraph SetStartRegular()
        {
            return SetAttribute("start", "regular".DoubleQuote());
        }

        public DotGraph SetStartSelf()
        {
            return SetAttribute("start", "self".DoubleQuote());
        }

        public DotGraph SetStartRandom()
        {
            return SetAttribute("start", "random".DoubleQuote());
        }

        public DotGraph SetStartRandom(int seed)
        {
            return SetAttribute("start", ("random" + seed).DoubleQuote());
        }

        public DotGraph SetStyleRadial()
        {
            return SetAttribute("style", "radial");
        }

        public DotGraph SetStyleSheet(string value)
        {
            return SetAttribute("stylesheet", value.ValueToQuotedLiteral());
        }

        public DotGraph SetTarget(DotHtmlString value)
        {
            return SetAttribute("target", value.QuotedValue);
        }

        public DotGraph SetTrueColor(bool value)
        {
            return SetAttribute("truecolor", value.ToString());
        }

        public DotGraph SetViewPort(double value)
        {
            return SetAttribute("viewport", value.ToDotDouble());
        }

        public DotGraph SetVoronoiMargin(double value)
        {
            return SetAttribute("voro_margin", value.ToDotDouble());
        }

        public DotGraph SetXDotVersion(string value)
        {
            return SetAttribute("xdotversion", value.ValueToQuotedLiteral());
        }
        #endregion


        protected DotGraph(DotGraphType graphType, string graphName)
        {
            GraphType = graphType;
            GraphName = graphName;
        }


        /// <summary>
        /// Convert the definition of this graph into GraphViz dot code
        /// </summary>
        /// <returns></returns>
        public string GenerateDotCode()
        {
            var context = new DotCodeGenContext(GraphType);

            context.GenerateDotCode(this);

            return context.ToString();
        }

        public void SaveDotCode(string filePath)
        {
            File.WriteAllText(filePath, GenerateDotCode());

            var folderPath = Path.GetDirectoryName(filePath) ?? "";

            var imagesArray = 
                ImagesCache
                    .Values
                    .Select(i => i as DotGraphLaTeXImage)
                    .Where(i => !ReferenceEquals(i, null))
                    .ToArray();

            var inputsArray = 
                imagesArray.Select(
                    i => Tuple.Create(i.ImageId, i.LaTeXCode)
                ).ToArray();

            var latexRenderer = new GrLaTeXImageComposer()
            {
                Resolution = 150
            };

            latexRenderer.RenderToPngFiles(inputsArray);

            //foreach (var image in ImagesCache.Values.Where(i => i.IsGenerated))
            //{
            //    //var img = (DotGraphLaTeXImage) image;

            //    //latexRenderer.RenderMathToPdfFile(
            //    //    image.ImageId,
            //    //    img.LaTeXMath
            //    //);

            //    var imageData = image.GetImage();
            //    var imagePath = Path.Combine(folderPath, image.ImageFileName);

            //    imageData.Save(imagePath, ImageFormat.Png);
            //}
        }
    }
}
