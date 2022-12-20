namespace LiWiMus.Core.Shared.Interfaces;

// ReSharper disable once InconsistentNaming
public static class IResource
{
    // ReSharper disable once InconsistentNaming
    public interface WithOwner<T>
    {
        public T Owner { get; set; }
    }

    // ReSharper disable once InconsistentNaming
    public interface WithMultipleOwners<T>
    {
        public List<T> Owners { get; set; }
    }
}