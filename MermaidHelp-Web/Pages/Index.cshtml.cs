using FineUICore;
using MermaidHelp.Code;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR.Protocol;
using Newtonsoft.Json.Linq;
using ShimSkiaSharp;
using SkiaSharp;
using Svg.Skia;
using System.Threading.Tasks;

namespace MermaidHelp.Pages
{
    public partial class IndexModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected async Task txtInput_Enter(object sender, EventArgs e)
        {
            var inpput = txtInput.Text;
            var output = GetMessage(inpput);
            //RegisterStartupScript($"F.ui.messagePanel.setText('{inpput}');");
            RegisterStartupScript($"F.ui.codePanel.setText('{output}');");
        }

        protected async Task btnRefresh_Click(object sender, EventArgs e)
        {
            SetSession(sessionkey, new MermaidMask());
            RegisterStartupScript($"F.ui.messagePanel.clear();");
        }

        private const string sessionkey = "Mermaid";

        private string GetMessage(string input)
        {
            /*
             * �û���ĶԻ������û�о�new MermaidMask()
             * �ύ �ý�� �ٷŻػ���
             */
            MermaidMask mask = GetSession<MermaidMask>(sessionkey);
            if (mask == null) { mask = new MermaidMask(); }
            mask.adduser(input);

            var msg = mask.GetResult().Result;

            mask.addassistant(msg);

            SetSession(sessionkey, mask);
            // �� Windows ���Ļ��з� \r\n �滻Ϊ \n
            string normalizedInput = msg.Replace("\r\n", "\n");

            // �����з� \n �滻Ϊ <br> ��ǩ
            string output = normalizedInput.Replace("\n", "\\n");

            return output;
        }

        public IActionResult OnPostExportToExcel(string svgContent)
        {
            if (string.IsNullOrWhiteSpace(svgContent))
            {
                return BadRequest("SVG content is required.");
            }

            var imageBytes = ConvertSvgToPng(svgContent);

            if (imageBytes == null)
            {
                return StatusCode(500, "Error converting SVG to PNG.");
            }

            return File(imageBytes, "image/png", "image.png");
        }

        private byte[] ConvertSvgToPng(string svgContent)
        {
            //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            //var encoding = Encoding.GetEncoding("GB2312");
            //var bytes = encoding.GetBytes(svgContent);

            //using (var svgstream = new MemoryStream(bytes))
            using (var svgstream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(svgContent)))
            using (var svg = new SKSvg())
            {
                svg.Load(svgstream);

                var bitmap = new SKBitmap((int)svg.Picture.CullRect.Width, (int)svg.Picture.CullRect.Height);
                using (var canvas = new SkiaSharp.SKCanvas(bitmap))
                {
                    canvas.Clear(SKColors.White);
                    // ����ϵͳ��������
                    var typeface = SkiaSharp.SKTypeface.FromFamilyName("SimHei");

                    var paint = new SkiaSharp.SKPaint
                    {
                        Typeface = typeface,
                        TextSize = 24,
                        IsAntialias = true,
                        Color = SKColors.Black,
                        TextEncoding = SkiaSharp.SKTextEncoding.Utf8
                    };
                    var fontPath = Path.Combine(FineUICore.PageContext.WebRootPath, "res", "SIMHEI.TTF");
                    paint.Typeface = SKFontManager.Default.CreateTypeface(fontPath);//����
                    canvas.DrawPicture(svg.Picture, paint);
                }

                using (var image = SkiaSharp.SKImage.FromBitmap(bitmap))
                using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
                using (var stream = new MemoryStream())
                {
                    data.SaveTo(stream);
                    return stream.ToArray();
                }
            }
        }
    }
}
