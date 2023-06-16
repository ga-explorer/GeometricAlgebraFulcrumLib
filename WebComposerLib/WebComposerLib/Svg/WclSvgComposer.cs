using TextComposerLib.Text.Linear;
using SvgElement = WebComposerLib.Svg.Elements.SvgElement;

namespace WebComposerLib.Svg
{
    public sealed class WclSvgComposer
    {
        private readonly LinearTextComposer _textComposer 
            = new LinearTextComposer();


        public WclSvgComposer Clear()
        {
            _textComposer.Clear();

            return this;
        }


        public WclSvgComposer AppendTag(SvgElement element)
        {
            _textComposer
                .AppendAtNewLine(element.TagText);

            return this;
        }

        public WclSvgComposer AppendTagBegin(SvgElement element)
        {
            _textComposer
                .AppendLineAtNewLine(element.BeginTagText)
                .IncreaseIndentation();

            return this;
        }

        public WclSvgComposer AppendTagBeginEnd(SvgElement element)
        {
            _textComposer
                .AppendLineAtNewLine(element.BeginEndTagText);

            return this;
        }

        public WclSvgComposer AppendTagEnd(SvgElement element)
        {
            _textComposer
                .DecreaseIndentation()
                .AppendLineAtNewLine(element.EndTagText);

            return this;
        }

        public WclSvgComposer AppendTagBegin(string elementName)
        {
            _textComposer
                .AppendAtNewLine("<")
                .Append(elementName)
                .AppendLine(">")
                .IncreaseIndentation();

            return this;
        }

        public WclSvgComposer AppendTagBeginEnd(string elementName)
        {
            _textComposer
                .AppendAtNewLine("<")
                .Append(elementName)
                .AppendLine("/>");

            return this;
        }

        public WclSvgComposer AppendTagEnd(string elementName)
        {
            _textComposer
                .DecreaseIndentation()
                .AppendAtNewLine("</")
                .Append(elementName)
                .AppendLine(">");

            return this;
        }


        public WclSvgComposer AppendSvgFileHeader()
        {
            _textComposer
                .AppendLine("<?xml version=\"1.0\"?>")
                .AppendLine("<!DOCTYPE svg PUBLIC \"-//W3C//DTD SVG 1.1//EN\" \"http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd\">")
                .AppendLine();

            return this;
        }


        //public Bitmap RenderToBitmap(int width, int height)
        //{
        //    var svgDocument = SvgDocument.FromSvg<SvgDocument>(
        //        ToString()
        //    );

        //    return svgDocument.Draw(width, height);
        //}

        //public SvgComposer SaveToPngFile(string pngFilePath, int width, int height)
        //{
        //    var svgDocument = SvgDocument.FromSvg<SvgDocument>(
        //        ToString()
        //    );

        //    using (var bitmap = svgDocument.Draw(width, height))
        //        bitmap.Save(pngFilePath, ImageFormat.Png);

        //    return this;
        //}

        public WclSvgComposer RenderToPngFile(string filePath)
        {
            //You could use inkScape for this:
            //http://harriyott.com/2008/05/converting-svg-images-to-png-in-c

            /*
             public void ProcessRequest(HttpContext context)
            {
             context.Response.ContentType = "image/png";
             
             String svgXml = GetSvgImageXml(context);
             string svgFileName = GetSvgFileName(context);
             using (StreamWriter writer = new StreamWriter(svgFileName, false))
             {
              writer.Write(svgXml);
             }

             string pngFileName = GetPngFileName(context);

             string inkscapeArgs = 
              "-f " + svgFileName + " -e \"" +
              context.Server.MapPath(PngRelativeDirectory) + "\\" +
              pngFileName + "\"";

             Process inkscape = Process.Start(
               new ProcessStartInfo(
                "C:\\program files\\inkscape\\inkscape.exe",
                inkscapeArgs));
             inkscape.WaitForExit(3000);
             context.Server.Transfer(PngRelativeDirectory + pngFileName);
            }
            */

            //Or this code:
            //https://stackoverflow.com/questions/8414324/convert-svg-to-image-programatically
            /*
             string svgFileName = HttpContext.Current.Server.MapPath("sample.svg");
            string PngRelativeDirectory = "C:\\";
            string pngName = "svgpieresult.png";
            string pngFileName = HttpContext.Current.Server.MapPath(pngName);


            // ignored assume sample.svg is in the web app directory
            //using (StreamWriter writer = new StreamWriter(svgFileName, false))
            //{
            //   writer.Write(svgXml);
            //}
            //

            string inkscapeArgs = string.Format(@"-f ""{0}"" -e ""{1}""", svgFileName, pngFileName);

            Process inkscape = Process.Start(
                new ProcessStartInfo("C:\\Program Files\\inkscape\\inkscape.exe", inkscapeArgs));

            inkscape.WaitForExit(3000);
            //Context.RewritePath(pngName);
            this.Response.Redirect(pngName);
            */

            //Or this code using a web browser control:
            //https://www.danbarbulescu.com/svg-png-batch-converter-c-net-2012/
            /*
            public Bitmap webpage_to_image(string url) 
            {
                 
            // Based on Chris Pietschmann's code
            // URL: http://pietschsoft.com/post/2008/07/C-Generate-WebPage-Thumbmail-Screenshot-Image
            // License: CC BY 3.0 US
                 
                // Load the webpage into a WebBrowser control
                WebBrowser wb = new WebBrowser(); 
                wb.ScrollBarsEnabled = false; 
                wb.ScriptErrorsSuppressed = true;
                wb.Navigate(url);  
                while (wb.ReadyState != WebBrowserReadyState.Complete)
                { System.Windows.Forms.Application.DoEvents(); }  
             
                wb.Width = 1920;
                wb.Height = 980;
                wb.ScrollBarsEnabled = false;
             
                Bitmap bitmap = new Bitmap(wb.Width, wb.Height); 
                wb.DrawToBitmap(bitmap, new Rectangle(0, 0, wb.Width, wb.Height));
                wb.Dispose();  
                return bitmap;
            }
             
             
            private void button1_Click(object sender, EventArgs e)
            {
                String path_input = "D:\\svg\\";
                String path_output = "D:\\png\\";
                String[] files = Directory.GetFiles(path_input);
             
                for (int i = 0; i < files.Count(); i++)
                {
                    GC.Collect();
                    string url = files[i];
                    Bitmap thumbnail = webpage_to_image(url);
                    thumbnail.Save(path_output + Path.GetFileNameWithoutExtension(files[i]) + ".png", System.Drawing.Imaging.ImageFormat.Png);
                }
             
            }
            */

            return this;
        }

        public override string ToString()
        {
            return _textComposer.ToString();
        }
    }
}
