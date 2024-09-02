namespace SmartLibrary.Common.Interfaces;

public interface IRequireInitializeAsync
{
    Task InitializeAsync();
}

public interface IRequireInitialize
{
    void Initialize();
}
