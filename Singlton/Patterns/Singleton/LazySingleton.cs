namespace Singlton.Patterns.Singleton;

/// <summary>
/// Lazily initialized singleton using <see cref="Lazy{T}"/>.
/// </summary>
public sealed class LazySingleton
{
    // Lazy<T> delays object creation until Value is requested for the first time.
    // By default, Lazy<T> is thread-safe, so only one LazySingleton is created.
    private static readonly Lazy<LazySingleton> _lazy = new(() =>
    {
        Console.WriteLine("  [LazySingleton] Instance created.");
        return new LazySingleton();
    });

    // Private constructor keeps all instance creation inside this class.
    private LazySingleton() { }

    // Accessing Value runs the factory above on first use, then reuses the same object.
    public static LazySingleton Instance => _lazy.Value;

    // Example behavior on the singleton instance.
    public void Execute(string message) => Console.WriteLine($"  [LazySingleton] {message}");
}
