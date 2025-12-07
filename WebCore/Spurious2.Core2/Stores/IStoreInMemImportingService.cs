namespace Spurious2.Core2.Stores;

public interface IStoreInMemImportingService
{
#pragma warning disable CA1002 // Do not expose generic lists
    public List<Store> ImportWithData();
#pragma warning restore CA1002 // Do not expose generic lists
}
