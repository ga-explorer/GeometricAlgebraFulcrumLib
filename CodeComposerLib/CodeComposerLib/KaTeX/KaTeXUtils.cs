namespace CodeComposerLib.KaTeX
{
    public static class KaTeXUtils
    {
        

        //public static Bitmap GenerateScreenshot(string url)
        //{
        //    // Load the webpage into a WebBrowser control
        //    var wb = new WebBrowser
        //    {
        //        ScrollBarsEnabled = false, 
        //        ScriptErrorsSuppressed = true
        //    };
        //    wb.Navigate(url);
        //    //wb.DocumentText = "";
            

        //    // waits for the page to be completely loaded
        //    while (wb.ReadyState != WebBrowserReadyState.Complete) ;

        //    // Take Screenshot of the web pages full width + some padding
        //    wb.Width = wb.Document.Body.ScrollRectangle.Height;
        //    // Take Screenshot of the web pages full height
        //    wb.Height = wb.Document.Body.ScrollRectangle.Height;

        //    // Get a Bitmap representation of the webpage as it's rendered in the WebBrowser control
        //    var bitmap = new Bitmap(wb.Width, wb.Height);
        //    wb.DrawToBitmap(bitmap, new System.Drawing.Rectangle(0, 0, wb.Width, wb.Height));
        //    wb.Dispose();

        //    return bitmap;
        //}

        //public static void TestKaTeX()
        //{
        //    var url = Path.Combine(Directory.GetCurrentDirectory(), "sample.html");
        //    var image = GenerateScreenshot(url);

        //    image.Save("sample.png");
        //}
    }
}
