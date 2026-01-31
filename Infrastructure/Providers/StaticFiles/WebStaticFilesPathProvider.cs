using Application.Interfaces.Infrastructure.Providers.StaticFiles;
using Microsoft.AspNetCore.Hosting;

namespace Infrastructure.Providers.StaticFiles
{
    public class WebStaticFilesPathProvider : IStaticFilesPathProvider
    {
        public string WebRootPath { get; }

        public WebStaticFilesPathProvider(IWebHostEnvironment env)
        {
            WebRootPath = env.WebRootPath;
        }
    }
}
