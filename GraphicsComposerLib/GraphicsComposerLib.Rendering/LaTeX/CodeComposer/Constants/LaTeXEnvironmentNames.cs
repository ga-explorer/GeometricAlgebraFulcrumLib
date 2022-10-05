namespace GraphicsComposerLib.Rendering.LaTeX.CodeComposer.Constants
{
    /// <summary>
    /// http://latex.wikia.com/wiki/List_of_LaTeX_environments
    /// https://www.sharelatex.com/learn/Environments
    /// </summary>
    public static class LaTeXEnvironmentNames
    {
        public static string Document { get; } = "document";

        /// <summary>
        /// Text inside a float environment is "floated" according to its placement, an optional parameter.
        /// The standard report and article classes use the default placement [tbp]. The float environments
        /// are figure and table. 
        /// </summary>
        public static string Figure { get; } = "figure";

        /// <summary>
        /// Text inside a float environment is "floated" according to its placement, an optional parameter.
        /// The standard report and article classes use the default placement [tbp]. The float environments
        /// are figure and table. 
        /// </summary>
        public static string Table { get; } = "table";


        //The list environments are description, enumerate, itemize, and the generic list, which is
        //seldom used in text, but often in macros. Within a list environment item commands are used
        //to identify the items in the list. 

        public static string List { get; } = "list";

        /// <summary>
        /// The description environment produces a labeled list. 
        /// </summary>
        public static string Description { get; } = "description";

        /// <summary>
        /// The enumerate environment produces a numbered list. At least one item is needed.
        /// Enumerate lists can be nested inside other enumerate lists, up to four levels deep. 
        /// </summary>
        public static string Enumerate { get; } = "enumerate";

        /// <summary>
        /// The itemize environment creates an unnumbered, or "bulleted" list. 
        /// </summary>
        public static string Itemize { get; } = "itemize";

        /// <summary>
        /// The math environment is used within paragraph mode to place mathematical symbols inline with
        /// the paragraph's text. The following are all equivalent:
        /// \( … \) 
        /// $ … $ 
        /// \begin{math} ... \end{math}
        /// In a wiki, the <math> ... </math> tags create a math environment.
        /// </summary>
        public static string Math { get; } = "math";

        /// <summary>
        /// The following are equivalent:
        /// \[ … \] 
        /// $$ … $$ 
        /// \begin{displaymath}…\end{displaymath}
        /// Note: The AMS Short Math Guide recommends against using $$ ... $$ to enter displaymath mode,
        /// because it is not documented in the TeX manual, and because it interferes with the proper
        /// operation of the fleqn option.
        /// </summary>
        public static string DisplayMath { get; } = "displaymath";

        /// <summary>
        /// The split environment is used to write multiple lines that are aligned using the ampersand
        /// (&amp;) character. 
        /// </summary>
        public static string Split { get; } = "split";

        /// <summary>
        /// The array environment is used to make a table of information, with column alignment (left,
        /// center, or right) and optional vertical lines separating the columns. 
        /// </summary>
        public static string Array { get; } = "array";

        /// <summary>
        /// An equation number is placed on every line unless that line has a \nonumber command.
        /// The command \lefteqn is used for splitting long formulas across lines. It typesets its
        /// argument in display style flush left in a box of zero width. 
        /// </summary>
        public static string EqationArray { get; } = "eqnarray";

        /// <summary>
        /// The equation environment centers the equation, and places an equation number in the right
        /// margin. 
        /// </summary>
        public static string Equation { get; } = "equation";

        /// <summary>
        /// The equation environment centers the equation with no number. 
        /// </summary>
        public static string UnnumberedEquation { get; } = "equation*";

        /// <summary>
        /// The subequations environment affixes letters a, b, c... to the end of the equation number
        /// within the environment. 
        /// </summary>
        public static string SubEquations { get; } = "subequations";

        /// <summary>
        /// The multiline environment lets you split equations over multiple lines, aligning the beginning
        /// of the equation with the left margin, and the end of it with the right margin. Equations are
        /// numbered. 
        /// </summary>
        public static string Multlline { get; } = "multiline";

        /// <summary>
        /// The gather environment gathers and centers equations; each equation is numbered; not available
        /// in wiki. 
        /// </summary>
        public static string Geother { get; } = "gather";

        /// <summary>
        /// The align environment lets you use the ampersand (&amp;) to align multiple equations; each
        /// equation is numbered; not available in wiki. 
        /// </summary>
        public static string Align { get; } = "align";

        /// <summary>
        /// The alignat environment can be used to align equations, and explicitly specify the number of
        /// "equation" columns. An equation column has two parts, separated by the equals-sign.
        /// Essentially, this is an array with alternating right-aligned and left-aligned columns.
        /// The required parameter of alignat is the maximum number of ampersands in a row plus 1, and
        /// then divided by 2. One use of alignat is to explicitly specify the amount of horizontal space
        /// between columns by including the required spacing in the first row. 
        /// </summary>
        public static string AlignAt { get; } = "alignat";

        /// <summary>
        /// The flalign environment lets you use the ampersand (&amp;) to align multiple equations.
        /// The leftmost and rightmost columns are aligned with the margins. 
        /// </summary>
        public static string ForceLeftAlign { get; } = "flalign";

        /// <summary>
        /// The theorem environment produces "Therem x" in bold face, followed by your theorem text.
        /// </summary>
        public static string Theorem { get; } = "theorem";

        // The matrix environments are matrix, pmatrix, bmatrix, Bmatrix, vmatrix, Vmatrix, and
        // smallmatrix. Each provides a table for expressions, aligned in rows and columns. The main
        // difference between the various types of matrix is the kind of delimeters that surround them.
        // Each row of a matrix ends with two backslashes(\\). Each column ends with an ampersand(&). 
        // http://latex.wikia.com/wiki/Matrix_environments

        /// <summary>
        /// Plain matrix 
        /// </summary>
        public static string Matrix { get; } = "matrix";

        /// <summary>
        /// bracketed matrix; typically represents the matrix itself
        /// </summary>
        public static string BracketedMatrix { get; } = "bmatrix";

        /// <summary>
        /// Braced matrix 
        /// </summary>
        public static string BracedMatrix  { get; } = "Bmatrix";

        /// <summary>
        /// Parenthesized matrix 
        /// </summary>
        public static string ParenthesizedMatrix { get; } = "pmatrix";

        /// <summary>
        /// vertical bar matrix; typically represents the determinant
        /// </summary>
        public static string VerticalBarMatrix { get; } = "vmatrix";

        /// <summary>
        /// Double-vertical bar matrix 
        /// </summary>
        public static string DoubleVerticalBarMatrix { get; } = "Vmatrix";

        /// <summary>
        /// small matrix; can be used inline
        /// </summary>
        public static string SmallMatrix { get; } = "smallmatrix";

        /// <summary>
        /// The cases environment is used to left-brace a group of equations or a piecewise-defined
        /// function.
        /// </summary>
        public static string Cases { get; } = "cases";

        // The paragraph environments are center, flushleft, flushright, minipage, quotation, quote,
        // verbatim, and verse. Within a paragraph environment, each line must be terminated with a double
        // backslash, \\. Related commands are centering, raggedright, raggedleft, parbox, footnote,
        // footnotetext, and verb

        /// <summary>
        /// The center environment allows you to create a paragraph consisting of lines that are
        /// centered within the left and right margins on the current page. 
        /// </summary>
        public static string Center { get; } = "center";

        /// <summary>
        /// The flushleft environment allows you to create a paragraph consisting of lines that are
        /// flushed left, to the left-hand margin. 
        /// </summary>
        public static string FlushLeft { get; } = "flushleft";

        /// <summary>
        /// The flushright environment allows you to create a paragraph consisting of lines that are
        /// flushed right, to the right-hand margin. 
        /// </summary>
        public static string FlushRight { get; } = "flushright";

        /// <summary>
        /// The minipage environment is similar to the \parbox command. It is not advisable to nest
        /// minipage environments, because it messes up footnote positioning. 
        /// </summary>
        public static string MiniPage { get; } = "minipage";

        /// <summary>
        /// The margins of the quotation environment are indented on the left and the right.
        /// The text is justified at both margins and there is paragraph indentation. 
        /// </summary>
        public static string Quotation { get; } = "quotation";

        /// <summary>
        /// The margins of the quotation environment are indented on the left and the right.
        /// The text is justified at both margins without paragraph indentation.
        /// </summary>
        public static string Quote { get; } = "quote";

        /// <summary>
        /// Uses typewriter (\tt) style, similar to the \verb command. 
        /// </summary>
        public static string Verbatim { get; } = "verbatim";

        /// <summary>
        /// The verse environment is designed for poetry. The margins are indented on the left and
        /// the right. End each lines with \\, and use a blank line to separate the stanzas. 
        /// </summary>
        public static string Verse { get; } = "verse";

        /// <summary>
        /// 
        /// </summary>
        public static string Picture { get; } = "picture";

        /// <summary>
        /// In the tabbing environment:
        ///     Tab stops are set with \=.
        ///     \> advances to the next tab stop.
        ///     \+ ends a line, and begins the next line one tab stop to the right.
        ///     \- ends a line, and begins the next line one tab stop to the left.
        /// </summary>
        public static string Tabbing { get; } = "tabbing";

        /// <summary>
        /// The tabular environment uses ampersands (&amp;) to separate columns. 
        /// </summary>
        public static string Tabular { get; } = "tabular";

        /// <summary>
        /// The thebibliography environment is used to print a bibliography. Related commands are bibitem,
        /// cite, nocite
        /// </summary>
        public static string TheBibliography { get; } = "thebibliography";

        /// <summary>
        /// The titlepage environment is used to print a title page. It has no printed page number or page
        /// heading. The following page is numbered page 1. A related commands is maketitle. 
        /// </summary>
        public static string TitlePage { get; } = "titlepage";
    }
}
