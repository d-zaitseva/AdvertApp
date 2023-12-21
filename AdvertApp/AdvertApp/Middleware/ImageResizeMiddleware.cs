using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.Processing;
using System.Reflection;
using System.Text;

namespace AdvertApp.Middleware;

public class ImageResizeMiddleware
{
    struct ResizeParams
    {
        public bool hasParams;
        public int w;
        public int h;

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"w: {w}, ");
            sb.Append($"h: {h}, ");

            return sb.ToString();
        }
    }

    struct ContentParams
    {
        public int size;
        public string format;
        public byte[] data;
    }

    private readonly RequestDelegate _next;
    private readonly ILogger<ImageResizeMiddleware> _logger;
    private readonly IConfiguration _configuration;
    private static readonly string[] suffixes = new string[] {
            ".png",
            ".jpg",
            ".jpeg"
        };

    public async Task Invoke(HttpContext context)
    {
        var path = context.Request.Path;

        if (context.Request.Query.Count == 0 || !IsImagePath(path))
        {
            await _next.Invoke(context);
            return;
        }

        var resizeParams = GetResizeParams(path, context.Request.Query);
        if (!resizeParams.hasParams || (resizeParams.w == 0 && resizeParams.h == 0))
        {
            await _next.Invoke(context);
            return;
        }

        _logger.LogInformation($"Resizing {path.Value} with params {resizeParams}");

        var imagePath = Path.Combine(
            _configuration["StoredFilesPath"],
            path.Value.Replace('/', Path.DirectorySeparatorChar).TrimStart(Path.DirectorySeparatorChar));

        var contentParams = GetContentData(imagePath, resizeParams);

        context.Response.ContentType = contentParams.format == ".png" ? "image/png" : "image/jpeg";
        context.Response.ContentLength = contentParams.size;
        await context.Response.Body.WriteAsync(contentParams.data, 0, contentParams.size);
    }

    public ImageResizeMiddleware(RequestDelegate next, ILogger<ImageResizeMiddleware> logger, IConfiguration configuration)
    {
        _next = next;
        _logger = logger;
        _configuration = configuration;
    }

    private bool IsImagePath(PathString path)
    {
        if (path == null || !path.HasValue)
            return false;

        return suffixes.Any(x => x.EndsWith(x, StringComparison.OrdinalIgnoreCase));
    }

    private ResizeParams GetResizeParams(PathString path, IQueryCollection query)
    {
        ResizeParams resizeParams = new ResizeParams();

        resizeParams.hasParams =
            resizeParams.GetType().GetTypeInfo()
            .GetFields().Where(f => f.Name != "hasParams")
            .Any(f => query.ContainsKey(f.Name));

        if (!resizeParams.hasParams)
            return resizeParams;

        int w = 0;
        if (query.ContainsKey("w"))
            int.TryParse(query["w"], out w);
        resizeParams.w = w;

        int h = 0;
        if (query.ContainsKey("h"))
            int.TryParse(query["h"], out h);
        resizeParams.h = h;

        return resizeParams;
    }

    private ContentParams GetContentData(string imagePath, ResizeParams resizeParams)
    {
        byte[] arr;

        using (Image image = Image.Load(imagePath))
        {
            image.Mutate(x => x.Resize(resizeParams.w, resizeParams.h));

            using (MemoryStream memoryStream = new MemoryStream())
            {
                var encoder = image.DetectEncoder(imagePath);
                image.Save(memoryStream, encoder);
                arr = memoryStream.ToArray();
            }
        }

        return new ContentParams()
        {
            size = arr.Length,
            format = Path.GetExtension(imagePath),
            data = arr
        };
    }
}