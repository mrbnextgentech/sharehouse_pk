using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Html2pdf;
using iText.Layout;
using iText.Layout.Element;
using System.IO;
using iText.Kernel.Events;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Gharshare.Models;
using Document = iText.Layout.Document;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace Gharshare.CommonHelper
{
    public class Pager
    {
        public int TotalItems { get; private set; }
        public int? ItemsinCurrentPage { get; set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int FromRecord { get; set; }
        public int ToRecord { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; private set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string SortOrder { get; set; }
        public string? SearchPhrase { get; set; } = string.Empty;
        public string? Type { get; set; } = string.Empty;
        public bool HasSearchPhrase { get; set; } = false; 
        public int SortOrderNum { get; set; }
        public string FunctionName { get; set; }
        public string Uid { get; set; }
        public string Id { get; set; }

        public Pager()
        {
        }
        public static int GetPageSize()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Json", optional: false);
            IConfiguration configuration = builder.Build();
            int pageSize = Convert.ToInt32(configuration.GetValue<string>("PageSize:Size"));
            return pageSize;
        }
        public Pager(int totalItems, int Page, string controllerName,
            string actionName, int pageSize = 10, string? searchPhrase = "", string? type = "",
           Int32? itemsinCurrentPage = 0,string? functionName = "", int sortOrderNum = 1,
           string? id = "", string? uid = "")
        {
            try
            {
                int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
                int currentPage = Page;

                int startPage = currentPage - 5;
                int endPage = currentPage + 4;

                if (startPage <= 0)
                {
                    endPage = endPage - (startPage - 1);
                    startPage = 1;
                }

                if (endPage > totalPages)
                {
                    endPage = totalPages;
                    if (endPage > 10)
                    {
                        startPage = endPage - 9;
                    }
                }

                TotalItems = totalItems;
                CurrentPage = currentPage;
                PageSize = pageSize;
                TotalPages = totalPages;
                StartPage = startPage;
                EndPage = endPage;
                ControllerName = controllerName;
                ActionName = actionName;
                SearchPhrase = searchPhrase;
                ItemsinCurrentPage = itemsinCurrentPage;
                Type = type;
                FromRecord = ((CurrentPage - 1) * PageSize) + 1;
                ToRecord = ((CurrentPage - 1) * PageSize) + (int)ItemsinCurrentPage; //PageSize-1;
                FunctionName = functionName;
                SortOrderNum = sortOrderNum;
                Uid = uid;
                Id = id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
    class PageNumberEventHelper : IEventHandler
    {
        private PdfCanvas canvas;
        private int pageNumber = 1;
        public void HandleEvent(Event currentEvent)
        {
            try
            {
                PdfDocumentEvent pdfEvent = (PdfDocumentEvent)currentEvent;
                PdfDocument pdfDoc = pdfEvent.GetDocument();
                PdfPage page = pdfEvent.GetPage();
                canvas = new PdfCanvas(page);
                canvas.BeginText();
                canvas.SetFontAndSize(PdfFontFactory.CreateFont(StandardFonts.HELVETICA), 10);
                canvas.MoveText(290, 780);
                canvas.ShowText("Page " + pageNumber);
                canvas.EndText();
                canvas.Release();
                pageNumber++;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
    public class Utility
    {
        private IHostingEnvironment _hostingEnvironment;
        public Utility(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public static string RemoveStreetHouse(string inputAddress)
        {
            // Remove ", Pakistan" at the end if it exists
            inputAddress = inputAddress.TrimEnd(new[] { ',', ' ' });
            if (inputAddress.EndsWith(", Pakistan", StringComparison.OrdinalIgnoreCase))
            {
                inputAddress = inputAddress.Substring(0, inputAddress.Length - 9).
                    TrimEnd(new[] { ',', ' ' });
            }

            // Define a list of patterns to remove
            string[] patternsToRemove = { "st", "Street No.", "Street No", "Street#", "street No", "street no", "Stree#","Str#", "Street #", "street no", "st#", "St#", "ST#", "House no", "House No", "House No.", "house No.", "House No.", "house no.", "house No", "house no.", "house#", "House#", "h#", "hno", "house", "street", "House", "Street" };

            foreach (string pattern in patternsToRemove)
            {
                // Remove variations of patterns with numbers
                inputAddress = Regex.Replace(inputAddress, $@"\b{pattern}(\s?#?| no)?\d*\s?,\s?", "", RegexOptions.IgnoreCase);
            }

            // Remove multiple spaces and commas
            inputAddress = Regex.Replace(inputAddress, @"\s+", " ").Trim();

            return inputAddress;
        }

        public string RemoveCountryName(string inputAddress)
        {
            // Remove ",Pakistan" at the end if it exists
            inputAddress = inputAddress.TrimEnd(new[] { ',', ' ' }) + ",";
            if (inputAddress.EndsWith("Pakistan,", StringComparison.OrdinalIgnoreCase))
            {
                inputAddress = inputAddress.Substring(0, inputAddress.Length - 9);
            }
            return inputAddress.TrimEnd(new[] { ',', ' ' });
        }
        public static string shortText(string text, Int32 totalCharacters)
        {
            if (text != null && text != String.Empty)
            {
                if (text.Length > totalCharacters)
                {
                    return text.Substring(0, totalCharacters) + "...";
                }
                else
                {
                    return text;
                }
            }
            else
                return "";
        }
        public static string shortusername(string text, Int32 totalCharacters)
        {
            int firstSpaceIndex = text.IndexOf(' '); // Find the index of the first space
            string name = "";

            if (firstSpaceIndex >= 0)
            {
                name = text.Substring(0, firstSpaceIndex);
                return name;

            }

            if (name.Length > totalCharacters)
            {
                return name.Substring(0, totalCharacters);
            }
            else
            {
                return text;
            }
        }
        public static string ConvertToShortDateTime(DateTime? date)
        {
            if (date.HasValue)
            {
                DateTime currentDate = DateTime.Today;

                if (date.Value.Date == currentDate)
                {
                    return "Today " + date.Value.ToString("hh:mm tt");
                }
                else
                {
                    return date.Value.ToString("dd/MM/yyyy hh:mm tt");
                }
            }
            else
            {
                return string.Empty;
            }
        }
        public static string GetOnlineDate(DateTime? date)
        {
            if (date.HasValue)
            {
                if (date.Value.Day.Equals(DateTime.Today.Day))
                    return "Today";
                else
                    return date.Value.ToString("dd/MM/yy");
            }
            else
            {
                return string.Empty;
            }
        }
        public static string ConvertToShortDateFormat(DateTime? date)
        {
            if (date.HasValue)
            {
                return date.Value.ToString("dd/MM/yyyy");
            }
            else
            {
                return string.Empty;
            }
        }
        public static DateTime ConvertToStandardFormat(DateTime? date)
        {
            if (date.HasValue)

            {
                //DateTime.ParseExact(TextBox2.Text.Trim(), "dd/MM/yyyy", null).ToString("MM/dd/yyyy");

                return DateTime.ParseExact(date.Value.ToString("MM/dd/yyyy"), "MM/dd/yyyy", CultureInfo.InvariantCulture);

                // return date.Value.ToString("MM/dd/yyyy");
            }
            else
            {
                return (DateTime)date;
            }
        }


        public string SendOtpbySendPKGateway(string toNumber, string messageText, string from)
        {
            string apiBaseUrl = getSetting("sendpk:baseUrl");
            string apiKey = getSetting("sendpk:apikey");
            string sender = from;
            string mobile = toNumber;
            string message = Uri.EscapeDataString(messageText);

            try
            {
                string uri = $"{apiBaseUrl}?api_key={apiKey}&sender={sender}&mobile={mobile}&message={message}";

                HttpWebRequest request = WebRequest.CreateHttp(uri);
                request.Method = "GET";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    return reader.ReadToEnd().Trim();
                }
            }
            catch (WebException ex)
            {
                if (ex.Response is HttpWebResponse httpWebResponse)
                {
                    switch (httpWebResponse.StatusCode)
                    {
                        case HttpStatusCode.NotFound:
                            return "404: URL not found";
                        case HttpStatusCode.BadRequest:
                            return "400: Bad Request";
                        default:
                            return httpWebResponse.StatusCode.ToString();
                    }
                }
                else
                {
                    return "Error: " + ex.Message;
                }
            }
        }

        public static string getSetting(string settingName)
        {
            string configSetting = string.Empty;
            try
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Json", optional: false);
                IConfiguration configuration = builder.Build();
                configSetting = configuration.GetValue<string>(settingName);
            }
            catch (Exception ex)
            {
                throw;
            }
            return configSetting;
        }
        public static string ConvertToCustomDateFormat(DateTime? date)
        {
            if (date.HasValue)
            {
                return date.Value.ToString("MMMM d, yyyy");
            }
            else
            {
                return string.Empty;
            }
        }
        public static string Format(decimal? value)
        {
            if (value.HasValue)
            {
                return value?.ToString("#,##0");
            }
            else
            {
                return string.Empty;
            }
        }

        public static string GetEnumDisplayName(Enum value)
        {
            if (value != null)
            {
                var displayAttribute = value
                    .GetType()
                    .GetMember(value.ToString())
                    .FirstOrDefault()
                    ?.GetCustomAttribute<DisplayAttribute>();

                return displayAttribute != null ? displayAttribute.Name : value.ToString();
            }
            else
                return null;
        }

        public byte[] GeneratePdfByiText7(string html, string filePath)
        {
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    // Create a new PDF writer
                    var pdfWriter = new PdfWriter(memoryStream);
                    // Create a new PDF document
                    var pdfDocument = new iText.Kernel.Pdf.PdfDocument(pdfWriter);

                    // Create a new ConversionProperties object
                    var properties = new ConverterProperties();
                    // Configure the conversion properties
                    properties.SetBaseUri("https://localhost:7163");
                    // Convert the HTML to PDF
                    HtmlConverter.ConvertToPdf(html, pdfDocument, properties);
                    // Create a new layout document
                    var document = new Document(pdfDocument);
                    // Add page numbering
                    pdfDocument.AddEventHandler(PdfDocumentEvent.END_PAGE, new PageNumberEventHelper());
                    // Close the PDF document
                    pdfDocument.Close();
                    byte[] pdfBytes = memoryStream.ToArray();
                    //using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    using (FileStream fs = File.Create(filePath))
                    {
                        fs.Write(pdfBytes, 0, pdfBytes.Length);
                    }
                    // Return the PDF as a byte array
                    return memoryStream.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<string> RenderViewToStringAsync(
    string viewName, object model,
    ControllerContext controllerContext,
    bool isPartial = false)
        {
            try
            {
                var actionContext = controllerContext as ActionContext;
                var serviceProvider = controllerContext.HttpContext.RequestServices;
                var razorViewEngine = serviceProvider.GetService(typeof(IRazorViewEngine)) as IRazorViewEngine;
                var tempDataProvider = serviceProvider.GetService(typeof(ITempDataProvider)) as ITempDataProvider;

                using (var sw = new StringWriter())
                {
                    var viewResult = razorViewEngine.FindView(actionContext, viewName, !isPartial);

                    if (viewResult?.View == null)
                        throw new ArgumentException($"{viewName} does not match any available view");

                    var viewDictionary =
                        new ViewDataDictionary(new EmptyModelMetadataProvider(),
                            new ModelStateDictionary())
                        { Model = model };

                    var viewContext = new ViewContext(
                        actionContext,
                        viewResult.View,
                        viewDictionary,
                        new TempDataDictionary(actionContext.HttpContext, tempDataProvider),
                        sw,
                        new HtmlHelperOptions()
                    );

                    await viewResult.View.RenderAsync(viewContext);
                    return sw.ToString();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public string GetRootPath()
        {
            try
            {
                string rootPath = _hostingEnvironment.WebRootPath;
                return rootPath;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public byte[] ReadStream(System.IO.Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }
        public string GenerateFileName(string FileName, string folderpath)
        {
            try
            {
                string folder = string.Empty;
                folder = Path.Combine(_hostingEnvironment.WebRootPath, folderpath);
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
                string uniqueFileName = Guid.NewGuid().ToString() + "-" + FileName;//"wwwroot/"+
                string filePath = folderpath + @"/" + uniqueFileName;
                return uniqueFileName;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public string ResizeAndSaveImage(IFormFile file, string filefolder, int targetSizeInBytes, int width, int height)
        {
            string _filePath = string.Empty;
            string fileNametoreturn = string.Empty;
            try
            {
                if (file != null)
                {
                    string _uploadDir = Path.Combine(_hostingEnvironment.WebRootPath, filefolder);
                    if (!Directory.Exists(_uploadDir))
                    {
                        Directory.CreateDirectory(_uploadDir);
                    }
                    string _fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
                    _fileName = _fileName.Replace(" ", "_");
                    fileNametoreturn = "/" + filefolder + "/" + _fileName.Trim();
                    _filePath = Path.Combine(_uploadDir, _fileName.Trim());

                    var imageStream = file.OpenReadStream();
                    using (var image = new Bitmap(imageStream))
                    {
                        // Calculate the new dimensions based on the original image's aspect ratio
                        int newWidth, newHeight;

                        if (image.Width > image.Height)
                        {
                            // Landscape orientation
                            newWidth = width;
                            newHeight = (int)((float)image.Height / image.Width * newWidth);
                        }
                        else
                        {
                            // Portrait orientation or square
                            newHeight = height;
                            newWidth = (int)((float)image.Width / image.Height * newHeight);
                        }

                        using (var thumbnail = new Bitmap(newWidth, newHeight))
                        {
                            using (var graphic = Graphics.FromImage(thumbnail))
                            {
                                graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                graphic.CompositingQuality = CompositingQuality.HighQuality;
                                graphic.SmoothingMode = SmoothingMode.HighQuality;
                                graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;

                                graphic.Clear(System.Drawing.Color.White);

                                // Draw the image on the thumbnail
                                graphic.DrawImage(image, 0, 0, newWidth, newHeight);

                                // Create a memory stream to store the compressed image
                                using (var compressedStream = new MemoryStream())
                                {
                                    var thumbnailQuality = 90; // Initial quality setting
                                    var encoderInfo = GetEncoderInfo(ImageFormat.Jpeg);
                                    var encoder = ImageCodecInfo.GetImageEncoders().First(c => c.FormatID == encoderInfo.FormatID);
                                    var encoderParameters = new EncoderParameters(1);

                                    // Save the thumbnail as a JPEG image with the initial quality setting
                                    encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)thumbnailQuality);
                                    thumbnail.Save(compressedStream, encoder, encoderParameters);

                                    // Loop to adjust quality until the image size is within targetSizeInBytes
                                    while (compressedStream.Length > targetSizeInBytes)
                                    {
                                        // Decrease the quality and re-save the image
                                        thumbnailQuality -= 10;

                                        if (thumbnailQuality < 10)
                                        {
                                            break; // Exit the loop if quality goes below 10
                                        }

                                        encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)thumbnailQuality);
                                        // Reset the memory stream position and clear it
                                        compressedStream.Seek(0, SeekOrigin.Begin);
                                        compressedStream.SetLength(0);

                                        // Save the compressed image again
                                        thumbnail.Save(compressedStream, encoder, encoderParameters);
                                    }

                                    // Save the final compressed image to the file
                                    using (var fs = new FileStream(_filePath, FileMode.Create))
                                    {
                                        compressedStream.Seek(0, SeekOrigin.Begin);
                                        compressedStream.CopyTo(fs);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return fileNametoreturn;
        }

        private ImageCodecInfo GetEncoderInfo(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            return codecs.FirstOrDefault(codec => codec.FormatID == format.Guid);
        }
        public string SaveFile(IFormFile file, string filefolder)
        {
            string _filePath = string.Empty;
            try
            {
                if (file != null)
                {
                    string _uploadDir = Path.Combine(_hostingEnvironment.WebRootPath, filefolder);
                    if (!Directory.Exists(_uploadDir))
                    {
                        Directory.CreateDirectory(_uploadDir);
                    }
                    string _fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
                    _fileName = _fileName.Replace(" ", "_");
                    _filePath = Path.Combine(_uploadDir, _fileName.Trim());
                    using (FileStream fs = new FileStream(_filePath, FileMode.Create))
                    {
                        file.CopyTo(fs);
                    }
                    _filePath = @"/" + filefolder + @"/" + _fileName;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return _filePath;
        }

        public void DeletFile(string oldFile)
        {
            try
            {
                if (oldFile != null)
                {
                    string _oldFilePath = _hostingEnvironment.WebRootPath.Trim() + oldFile.Trim();
                    if (File.Exists(_oldFilePath))
                    {
                        File.Delete(_oldFilePath);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }


        public string SaveFile(IFormFile file, string filefolder, long targetSizeBytes)
        {
            string _filePath = string.Empty;

            try
            {
                if (file != null)
                {
                    string _uploadDir = Path.Combine(_hostingEnvironment.WebRootPath, filefolder);
                    if (!Directory.Exists(_uploadDir))
                    {
                        Directory.CreateDirectory(_uploadDir);
                    }
                    string _fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
                    _fileName = _fileName.Replace(" ", "_");
                    _filePath = Path.Combine(_uploadDir, _fileName.Trim());

                    using (var inputStream = file.OpenReadStream())
                    using (var outputStream = new MemoryStream())
                    using (var image = SixLabors.ImageSharp.Image.Load(inputStream))
                    {
                        CompressImage(image, outputStream, targetSizeBytes);

                        // Save the compressed image to the file system
                        File.WriteAllBytes(_filePath, outputStream.ToArray());
                    }

                    _filePath = @"/" + filefolder + @"/" + _fileName;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                throw;
            }

            return _filePath;
        }

        private void CompressImage(SixLabors.ImageSharp.Image image, Stream outputStream, long targetSizeBytes)
        {
            long quality = 100; // Initial quality setting

            // Create a copy of the image to work with
            using (var clonedImage = image.Clone(x => x.Resize(image.Width, image.Height)))
            {
                while (quality > 10)
                {
                    // Re-encode the cloned image with the specified quality
                    using (var encodedStream = new MemoryStream())
                    {
                        // Adjust the quality property of the JpegEncoder
                        var jpegEncoder = new JpegEncoder()
                        {
                            Quality = (int)quality
                        };

                        clonedImage.Save(encodedStream, jpegEncoder);

                        // Reset the encodedStream position and write it to the outputStream
                        encodedStream.Seek(0, SeekOrigin.Begin);
                        encodedStream.CopyTo(outputStream);

                        if (outputStream.Length <= targetSizeBytes)
                        {
                            // The encoded image is within the target size, so exit the loop
                            break;
                        }

                        // Reduce the quality for the next iteration
                        quality -= 10;

                        // Clear the encodedStream to release resources
                        encodedStream.SetLength(0);
                    }
                }
            }
        }
    }
    public class TwilioService
    {
        private readonly TwilioOptions _options;

        public TwilioService(IOptions<TwilioOptions> options)
        {
            _options = options.Value;
        }
        public async Task SendSmsAsync(string toPhoneNumber, string message)
        {
            try
            {
                TwilioClient.Init(_options.AccountSid, _options.AuthToken);

                var smsMessage = await MessageResource.CreateAsync(
                    body: message,
                    from: new Twilio.Types.PhoneNumber(_options.FromPhoneNumber),
                    to: new Twilio.Types.PhoneNumber(toPhoneNumber)
                );
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

    public class TwilioOptions
    {
        public string AccountSid { get; set; }
        public string AuthToken { get; set; }
        public string FromPhoneNumber { get; set; }
    }
}
