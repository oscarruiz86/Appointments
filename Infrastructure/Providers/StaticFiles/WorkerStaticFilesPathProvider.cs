using Application.Interfaces.Infrastructure.Providers.StaticFiles;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Providers.StaticFiles
{
    public class WorkerStaticFilesPathProvider : IStaticFilesPathProvider
    {
        public string WebRootPath { get; }

        public WorkerStaticFilesPathProvider(IHostEnvironment env)
        {
            WebRootPath = Path.Combine(env.ContentRootPath, "wwwroot");
        }
    }
}
