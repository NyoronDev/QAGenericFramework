using BoDi;

namespace CrossLayer.Containers
{
    public interface IAppContainer
    {
        void RegisterWebBrowserPages(IObjectContainer objectContainer);

        void RegisterAPIs(IObjectContainer objectContainer);

        void RegisterPerformance(IObjectContainer objectContainer);

        void RegisterDatabaseRepositories(IObjectContainer objectContainer);
    }
}