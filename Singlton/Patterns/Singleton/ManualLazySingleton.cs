namespace Singlton.Patterns.Singleton;

/// <summary>
/// Manually implemented lazy singleton using the Double-Checked Locking pattern.
/// </summary>
public sealed class ManualLazySingleton
{
    // Nullable because no instance exists until the first call to Instance.
    private static ManualLazySingleton? _instance;

    // Shared lock object used to protect instance creation across multiple threads.
    private static readonly object _lock = new();

    // Private constructor prevents callers from bypassing the singleton by using "new".
    private ManualLazySingleton()
    {
        Console.WriteLine("  [ManualLazySingleton] Instance created manually.");
    }

    /// <summary>
    /// Returns the single ManualLazySingleton instance, creating it only on first use.
    /// </summary>
    public static ManualLazySingleton Instance
    {
        get
        {
            // First check avoids locking after the instance has already been created.
            if (_instance == null)
            {
                // Lock ensures only one thread can create the instance at a time.
                lock (_lock)
                {
                    // Second check prevents duplicate creation if another thread won the lock first.
                    if (_instance == null)
                    {
                        _instance = new ManualLazySingleton();
                    }
                }
            }
            return _instance;
        }
    }

    // Example behavior on the singleton instance.
    public void Execute(string message) => Console.WriteLine($"  [ManualLazySingleton] {message}");
}
