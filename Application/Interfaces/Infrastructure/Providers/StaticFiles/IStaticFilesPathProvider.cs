namespace Application.Interfaces.Infrastructure.Providers.StaticFiles
{
    public interface IStaticFilesPathProvider
    {
        string WebRootPath { get; }
    }
}
