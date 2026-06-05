namespace Singlton.Patterns.Singleton;

/// <summary>
/// Eagerly initialized singleton. Thread-safe by CLR static initializer guarantees.
/// </summary>
public sealed class EagerSingleton
{
    // The single instance is created as soon as this type is first accessed.
    // CLR static initialization makes this thread-safe without a manual lock.
    private static readonly EagerSingleton _instance = new();

    // Private constructor prevents callers from creating additional instances with "new".
    private EagerSingleton() { }

    // Public access point used by the rest of the application to get the one shared instance.
    public static EagerSingleton Instance => _instance;

    // Example behavior on the singleton instance.
    public void Execute(string message) => Console.WriteLine($"[EagerSingleton] {message}");
}
