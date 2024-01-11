using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdvertAppTests.Fixtures;

public abstract class TestBedFixture : IDisposable, IAsyncDisposable
{
    private readonly IServiceCollection _services;
    private IServiceProvider? _serviceProvider;
    private bool _disposedValue;
    private bool _disposedAsync;
    private bool _servicesAdded;
    private readonly IConfiguration _configuration;

    protected TestBedFixture()
    {
        _services = new ServiceCollection();
        _servicesAdded = false;
        _configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false)
            .AddJsonFile("appsettings.Development.json", true)
            .AddEnvironmentVariables()
            .Build();
    }

    public IServiceProvider GetServiceProvider()
    {
        if (_serviceProvider != default)
        {
            return _serviceProvider;
        }
        if (!_servicesAdded)
        {
            AddServices(_services, _configuration);
            _servicesAdded = true;
        }
        _services.AddLogging();
        return _serviceProvider = _services.BuildServiceProvider();
    }

    public T? GetService<T>()
        => GetServiceProvider().GetService<T>();

    protected virtual void AddServices(IServiceCollection services, IConfiguration configuration)
        => services.AddTransient(sp => configuration);

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                if (_serviceProvider is not null)
                {
                    ((ServiceProvider)_serviceProvider).Dispose();
                }
                _services.Clear();
            }

            _disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        if (!_disposedAsync)
        {
            await DisposeAsyncCore();
            GC.SuppressFinalize(this);
            _disposedAsync = true;
        }
    }

    protected abstract ValueTask DisposeAsyncCore();
}
