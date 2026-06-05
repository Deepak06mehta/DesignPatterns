namespace Singlton.Patterns.Singleton;

/// <summary>
/// Intentionally unsafe singleton used only to demonstrate a race condition.
/// Do not copy this implementation into production code.
/// </summary>
public sealed class UnsafeLazySingleton
{
    private static UnsafeLazySingleton? _instance;
    private static int _createdCount;

    private UnsafeLazySingleton()
    {
        // Count how many objects were actually constructed.
        Interlocked.Increment(ref _createdCount);
    }

    public static int CreatedCount => _createdCount;

    public static UnsafeLazySingleton Instance
    {
        get
        {
            if (_instance == null)
            {
                // Artificial delay makes it easier for multiple threads to observe null together.
                Thread.Sleep(25);
                _instance = new UnsafeLazySingleton();
            }

            return _instance;
        }
    }

    public static void ResetForDemo()
    {
        _instance = null;
        _createdCount = 0;
    }
}

/// <summary>
/// Intentionally bad singleton used only to demonstrate how inconsistent lock ordering deadlocks.
/// </summary>
public sealed class DeadlockProneSingleton
{
    private readonly object _configurationLock = new();
    private readonly object _stateLock = new();

    private DeadlockProneSingleton() { }

    public static DeadlockProneSingleton Instance { get; } = new();

    public void LockConfigurationThenState(ManualResetEventSlim configurationLockTaken, ManualResetEventSlim statePathReady)
    {
        lock (_configurationLock)
        {
            configurationLockTaken.Set();
            statePathReady.Wait();

            // This waits forever when another thread already holds _stateLock and wants _configurationLock.
            lock (_stateLock)
            {
                Console.WriteLine("This line is not expected during the deadlock demo.");
            }
        }
    }

    public void LockStateThenConfiguration(ManualResetEventSlim stateLockTaken, ManualResetEventSlim configurationPathReady)
    {
        lock (_stateLock)
        {
            stateLockTaken.Set();
            configurationPathReady.Wait();

            // This waits forever when another thread already holds _configurationLock and wants _stateLock.
            lock (_configurationLock)
            {
                Console.WriteLine("This line is not expected during the deadlock demo.");
            }
        }
    }
}
