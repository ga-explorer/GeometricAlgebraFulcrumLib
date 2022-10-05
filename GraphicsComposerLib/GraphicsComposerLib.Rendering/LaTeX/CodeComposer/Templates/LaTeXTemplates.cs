namespace GraphicsComposerLib.Rendering.LaTeX.CodeComposer.Templates
{
    public static class LaTeXTemplates
    {
        public static string MathArticle { get; }
            = @"\documentclass[english]{article}
\usepackage[T1]{fontenc}
\usepackage[latin9]{inputenc}
\usepackage{babel}
\usepackage{amsmath}
\begin{document}
#contents#
\end{document}";

        public static string MathPreview1 { get; }
            = @"\documentclass[preview,border=2pt]{standalone}
\usepackage{amsmath}
\usepackage{amsfonts}
\begin{document}
#contents#
\end{document}";

        public static string MathPreview2 { get; }
            = @"\documentclass{article}
\usepackage[active]{preview}
\usepackage{amsmath}
\usepackage{amsfonts}
\begin{document}
\begin{preview}
\[
\pi = \sqrt{12}\sum^\infty_{k=0} \frac{ (-3)^{-k} }{ 2k+1 }
\]
\end{preview}
\end{document}";

        public static string Dot2TeX { get; }
            = @"\begin{tikzpicture}[>=latex',scale=0.5]
    % set node style
    \begin{dot2tex}[dot,tikz,codeonly,styleonly,options=-s -tmath]
        #contents#
    \end{dot2tex}.
\end{tikzpicture}";
    }
}
