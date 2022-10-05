namespace GraphicsComposerLib.Rendering.LaTeX.CodeComposer.Constants
{
    /// <summary>
    /// https://en.wikibooks.org/wiki/LaTeX/Document_Structure#Document_classes
    /// 
    /// </summary>
    public static class LaTeXDocumentClassNames
    {
        /// <summary>
        /// For articles in scientific journals, presentations, short reports, program documentation,
        /// invitations, ... etc.
        /// </summary>
        public static string Article { get; } = "article";

        /// <summary>
        /// For articles with the IEEE Transactions format.
        /// </summary>
        public static string IeeetTransactions { get; } = "IEEEtran";

        /// <summary>
        /// A class for proceedings based on the article class.
        /// </summary>
        public static string Proceedings { get; } = "proc";

        /// <summary>
        /// For longer reports containing several chapters, small books, thesis, ... etc.
        /// </summary>
        public static string Report { get; } = "report";

        /// <summary>
        /// For real books.
        /// </summary>
        public static string Book { get; } = "book";

        /// <summary>
        /// For slides. The class uses big sans serif letters.
        /// </summary>
        public static string Slides { get; } = "slides";

        /// <summary>
        /// For changing sensibly the output of the document. It is based on the book class,
        /// but you can create any kind of document with it.
        /// </summary>
        public static string Memoir { get; } = "memoir";

        /// <summary>
        /// For writing letters.
        /// </summary>
        public static string Letter { get; } = "letter";

        /// <summary>
        /// For writing presentations
        /// </summary>
        public static string Beamer { get; } = "beamer";
    }
}
