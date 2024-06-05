namespace GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.CodeComposer.Constants;

/// <summary>
/// https://en.wikibooks.org/wiki/LaTeX/Package_Reference
/// </summary>
public static class LaTeXPackageNames
{
    /// <summary>
    /// It contains the advanced math extensions for LaTeX. The complete documentation should
    /// be in your LaTeX distribution; the file is called amsdoc, and can be dvi or pdf. For more
    /// information, see the chapter about Mathematics. Successed by mathtools package described below.
    /// </summary>
    public static string AmsMath { get; } = "amsmath";

    /// <summary>
    /// It adds new symbols in to be used in math mode.
    /// </summary>
    public static string AmsSymbols { get; } = "amssymb";

    /// <summary>
    /// It introduces the proof environment and the \theoremstyle command.
    /// </summary>
    public static string AmsTheorems { get; } = "amsthm";

    /// <summary>
    /// It extends the possibility of LaTeX to handle tables, fixing some bugs and adding new features.
    /// Using it, you can create very complicated and customized tables. 
    /// </summary>
    public static string Array { get; } = "array";

    /// <summary>
    /// It provides the internationalization of LaTeX. It has to be loaded in any document,
    /// and you have to give as an option the main language you are going to use in the document. 
    /// </summary>
    public static string Babel { get; } = "babel";

    /// <summary>
    /// Advanced bibliography handling. It is the package to use for writing a thesis.
    /// </summary>
    public static string BibLaTeX { get; } = "biblatex";

    /// <summary>
    /// Allows use of bold greek letters in math mode using the \bm{...} command. This supersedes
    /// the amsbsy package.
    /// </summary>
    public static string BoldGreekMath { get; } = "bm";

    /// <summary>
    /// provides ex­tra com­mands as well as be­hind-the-scenes op­ti­mi­sa­tion for producing tables.
    /// Guide­lines are given as to what con­sti­tutes a good ta­ble in the package documentation.
    /// </summary>
    public static string BookTables { get; } = "booktabs";

    /// <summary>
    /// It introduces the boxedminipage environment, that works exactly like minipage but adds a
    /// frame around it.
    /// </summary>
    public static string BoxedMiniPage { get; } = "boxedminipage";

    /// <summary>
    /// Allows customization of appearance and placement of captions for figures, tables, etc.
    /// </summary>
    public static string Caption { get; } = "caption";

    /// <summary>
    /// Provides commands for striking out mathematical expressions. The syntax is \cancel{x}
    /// or \cancelto{0}{x}
    /// </summary>
    public static string Cancel { get; } = "cancel";

    /// <summary>
    /// Part of a bundle to typeset chemistry easily and consistent.
    /// </summary>
    public static string ChemistryMacros { get; } = "chemmacros";

    /// <summary>
    /// To easily change the margins of pages. The syntax is:
    /// \changepage{textheight}{textwidth}{evensidemargin}{oddsidemargin}{columnsep}{topmargin}
    /// {headheight}{headsep}{footskip}
    /// All the arguments can be both positive and negative numbers; they will be added
    /// (keeping the sign) to the relative variable.
    /// </summary>
    public static string ChangePage { get; } = "changepage";

    /// <summary>
    /// En­hances LaTeX's cross-ref­er­enc­ing fea­tures, al­low­ing the for­mat of ref­er­ences to be
    /// de­ter­mined au­to­mat­i­cally ac­cord­ing to the type of ref­er­ence.
    /// </summary>
    public static string CleveeReferences { get; } = "cleveref";

    /// <summary>
    /// The package defines a new "D" column format in tab­u­lar en­vi­ron­ments for aligning the numbers
    /// in columns on the decimal point.
    /// </summary>
    public static string DecimalAlignColumn { get; } = "dcolumn";

    /// <summary>
    /// Adds support for arbitrarily-deep nested lists (useful for outlines).
    /// </summary>
    public static string EnumItem { get; } = "enumitem";

    /// <summary>
    /// Provides and option to convert EPS images to PDF and include them with \includegraphics{}.
    /// </summary>
    public static string EpsToPdf { get; } = "epstopdf";

    /// <summary>
    /// Adds additional integral symbols, for integrals over squares, clockwise integrals over sets,
    /// etc.
    /// </summary>
    public static string EsIntegrals { get; } = "esint";

    /// <summary>
    /// Other mathematical symbols.
    /// </summary>
    public static string EulerCal { get; } = "eucal";

    /// <summary>
    /// To change header and footer of any page of the document. It is described in the Page Layout
    /// section.
    /// </summary>
    public static string FancyHeaders { get; } = "fancyhdr";

    /// <summary>
    /// Im­proves the in­ter­face for defin­ing float­ing ob­jects such as fig­ures and ta­bles, introduces
    /// new floating objects types (boxed, ruled, plaintop) and provides an ability to define custom
    /// ones.
    /// </summary>
    public static string Float { get; } = "float";

    /// <summary>
    /// To choose the font encoding of the output text. You might need it if you are writing documents
    /// in a language other than English.
    /// </summary>
    public static string FontEncoding { get; } = "fontenc";

    /// <summary>
    /// Pro­vides generic com­mands \de­gree, \cel­sius, \pert­hou­sand, \mi­cro and \ohm which work both in
    /// text and maths mode.
    /// </summary>
    public static string GenericSymbols { get; } = "gensymb";

    /// <summary>
    /// For easy management of document margins and the document page size.
    /// </summary>
    public static string Geometry { get; } = "geometry";

    /// <summary>
    /// For creation of glossaries and list of acronyms.
    /// </summary>
    public static string Glossaries { get; } = "glossaries";

    /// <summary>
    /// Allows you to insert graphic files within a document.
    /// </summary>
    public static string Graphicx { get; } = "graphicx";

    /// <summary>
    /// Improves the file name pro­cess­ing of graphic/graphicx pack­ages to sup­port a larger range of
    /// file names (spaces, multiple dots, etc.).
    /// </summary>
    public static string Grffile { get; } = "grffile";

    /// <summary>
    /// It gives LaTeX the possibility to manage links within the document or to any URL when you
    /// compile in PDF.
    /// </summary>
    public static string HyperReference { get; } = "hyperref";

    /// <summary>
    /// Once loaded, the beginning of any chapter/section is indented by the usual paragraph indentation.
    /// </summary>
    public static string IndentFirst { get; } = "indentfirst";

    /// <summary>
    /// To choose the encoding of the input text. You might need it if you are writing documents in a
    /// language other than English.
    /// </summary>
    public static string InputEncoding { get; } = "inputenc";

    /// <summary>
    /// Other mathematical symbols.
    /// </summary>
    public static string LatexSymols { get; } = "latexsym";

    /// <summary>
    /// To insert programming code within the document. Many languages are supported and the output
    /// can be customized.
    /// </summary>
    public static string CodeListings { get; } = "listings";

    /// <summary>
    /// Al­lows you to write ta­bles that con­tinue to the next page. You can also define a header and a
    /// footer which will be shown on every page the table occupies, for example cont. from last page.
    /// </summary>
    public static string LongTable { get; } = "longtable";

    /// <summary>
    /// Sets the default font of the entire document (including math formulae) to Times New Roman,
    /// which is a more familiar font, and useful in saving space when fighting against page limits.
    /// </summary>
    public static string MathPtmx { get; } = "mathptmx";

    /// <summary>
    /// Other mathematical symbols.
    /// </summary>
    public static string MathRalphSmithFormalScript { get; } = "mathrsfs";

    /// <summary>
    /// Successor of amsmath, some additional functionality, some bugs fixed.
    /// </summary>
    public static string MathTools { get; } = "mathtools";

    /// <summary>
    /// allows you to easily type chemical species and equations. It automatically formats chemical
    /// species so you don't have to use subscript commands. It also Allows you to draw chemical
    /// formulas.
    /// </summary>
    public static string MhChemical { get; } = "mhchem";

    /// <summary>
    /// It provides an improvement to LaTeX's default ty­po­graphic ex­ten­sions, improvements in such
    /// areas as char­ac­ter pro­tru­sion and font ex­pan­sion, in­ter­word spac­ing and ad­di­tional kern­ing,
    /// and hy­phen­at­able letter-spacing
    /// </summary>
    public static string MicroType { get; } = "microtype";

    /// <summary>
    /// provides the mul­ti­cols environment which typesets text into multiple columns.
    /// </summary>
    public static string MultiColumn { get; } = "multicol";

    /// <summary>
    /// Gives additional citation options and styles. Often used for journal submission.
    /// </summary>
    public static string NatBib { get; } = "natbib";

    /// <summary>
    /// This package simplifies the insertion of external multi-page PDF or PS documents.
    /// </summary>
    public static string PdfPages { get; } = "pdfpages";

    /// <summary>
    /// It lets you rotate any kind of object. It is particularly useful for rotating tables.
    /// </summary>
    public static string Rotating { get; } = "rotating";

    /// <summary>
    /// Lets you change line spacing, e.g. provides the \doublespacing command for making double
    /// spaced documents.
    /// </summary>
    public static string SetSpace { get; } = "setspace";

    /// <summary>
    /// A useful package related to referencing. If you wish to reference an image or formula,
    /// you have to give it a name using \label{...} and then you can recall it using \ref{...}.
    /// When you compile the document these will be replaced only with numbers, and you can't know
    /// which label you had used unless you take a look at the source. If you have loaded the showkeys
    /// package, you will see the label just next or above the relevant number in the compiled version.
    /// This way you can easily keep track of the labels you add or use, simply looking at the preview
    /// (both dvi or pdf). Just before the final version, remove it.
    /// </summary>
    public static string ShowKeys { get; } = "showkeys";

    /// <summary>
    /// It prints out all index entries in the left margin of the text. This is quite useful for
    /// proofreading a document and verifying the index.
    /// </summary>
    public static string ShoWordIndex { get; } = "showidx";

    /// <summary>
    /// The "root" and "child" document can be compiled at the same time without making changes to
    /// the "child" document.
    /// </summary>
    public static string SubFiles { get; } = "subfiles";

    /// <summary>
    /// It allows to define multiple floats (figures, tables) within one environment giving individual
    /// captions and labels in the form 1a, 1b.
    /// </summary>
    public static string SubCaption { get; } = "subcaption";

    /// <summary>
    /// If you add the following code in your preamble:
    ///     \usepackage{syntonly}
    ///     \syntaxonly
    /// LaTeX skims through your document only checking for proper syntax and usage of the commands,
    /// but doesn’t produce any (DVI or PDF) output. As LaTeX runs faster in this mode you may save
    /// yourself valuable time. If you want to get the output, you can simply comment out the second
    /// line.
    /// </summary>
    public static string SyntaxOnly { get; } = "syntonly";

    /// <summary>
    /// Provides extra symbols, e.g. arrows like \textrightarrow, various currencies (\texteuro,...),
    /// things like \textcelsius and many others.
    /// </summary>
    public static string TextCompanion { get; } = "textcomp";

    /// <summary>
    /// You can change the style of newly defined theorems.
    /// </summary>
    public static string Theorem { get; } = "theorem";

    /// <summary>
    /// Lets you insert notes of stuff to do with the syntax \todo{Add details.}
    /// </summary>
    public static string ToDoNotes { get; } = "todonotes";

    /// <summary>
    /// Helps you typeset of SI-units correctly. For example \SI{12}{\mega\hertz}. Automatically
    /// handles the correct spacing between the number and the unit. Note that even non-SI-units
    /// are set, like dB, rad, ...
    /// </summary>
    public static string SiUnitx { get; } = "siunitx";

    /// <summary>
    /// It allows to underline text (either with straight or wavy line).
    /// </summary>
    public static string Underlinem { get; } = "ulem";

    /// <summary>
    /// It defines the \url{...} command. URLs often contain special character such as '_' and '&',
    /// in order to write them you should escape them inserting a backslash, but if you write them as
    /// an argument of \url{...}, you don't need to escape any special character and it will take care
    /// of proper formatting for you. If you are using hyperref, you don't need to load url because it
    /// already provides the \url{...} command.
    /// </summary>
    public static string Url { get; } = "url";

    /// <summary>
    /// It improves the verbatim environment, fixing some bugs. Moreover, it provides the comment
    /// environment, that lets you add multiple-line comments or easily comment out big parts of the
    /// code.
    /// </summary>
    public static string Verbatim { get; } = "verbatim";

    /// <summary>
    /// It adds support for colored text.
    /// </summary>
    public static string XColor { get; } = "xcolor";

    /// <summary>
    /// It is used to create trees, graphs, (commutative) diagrams, and similar things.
    /// </summary>
    public static string XyPic { get; } = "xypic";
}