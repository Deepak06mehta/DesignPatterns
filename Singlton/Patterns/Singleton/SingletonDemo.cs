namespace Singlton.Patterns.Singleton;

/// <summary>
/// Runs a small console demonstration for each singleton implementation.
/// </summary>
public static class SingletonDemo
{
    /// <summary>
    /// Creates or retrieves each singleton and proves repeated access returns the same object.
    /// </summary>
    public static void Run()
    {
        Console.WriteLine("=== Singleton Patterns Demo ===\n");

        // EagerSingleton is already initialized when the type is first used.
        var e = EagerSingleton.Instance;

        // ReferenceEquals proves both variables point to the exact same object in memory.
        Console.WriteLine($"1) EagerSingleton ReferenceEquals: {ReferenceEquals(e, EagerSingleton.Instance)}");
        e.Execute("Verified.");

        Console.WriteLine("\n2) LazySingleton (using Lazy<T>)");

        // LazySingleton is created here because Instance accesses Lazy<T>.Value.
        var l = LazySingleton.Instance;
        Console.WriteLine($"   ReferenceEquals: {ReferenceEquals(l, LazySingleton.Instance)}");
        l.Execute("Verified.");

        Console.WriteLine("\n3) ManualLazySingleton (using Double-Checked Locking)");

        // ManualLazySingleton is created here using explicit null checks and locking.
        var m = ManualLazySingleton.Instance;
        Console.WriteLine($"   ReferenceEquals: {ReferenceEquals(m, ManualLazySingleton.Instance)}");
        m.Execute("Verified.");

        DemonstrateRaceCondition();
        DemonstrateDeadlock();
    }

    /// <summary>
    /// Shows what happens when lazy singleton creation is not protected by a lock or Lazy&lt;T&gt;.
    /// </summary>
    private static void DemonstrateRaceCondition()
    {
        Console.WriteLine("\n4) Race condition demo (unsafe lazy singleton)");

        UnsafeLazySingleton.ResetForDemo();

        // Many threads access Instance at the same time. Because the getter has no lock,
        // several threads can pass the null check and construct separate objects.
        Parallel.For(0, 40, _ =>
        {
            GC.KeepAlive(UnsafeLazySingleton.Instance);
        });

        Console.WriteLine($"   Objects created: {UnsafeLazySingleton.CreatedCount}");
        Console.WriteLine("   Expected for a correct singleton: 1");
    }

    /// <summary>
    /// Shows a deadlock caused by two threads taking the same locks in opposite order.
    /// </summary>
    private static void DemonstrateDeadlock()
    {
        Console.WriteLine("\n5) Deadlock demo (inconsistent lock ordering)");

        var singleton = DeadlockProneSingleton.Instance;
        using var configurationLockTaken = new ManualResetEventSlim();
        using var stateLockTaken = new ManualResetEventSlim();

        // Task 1 takes _configurationLock first, then waits for _stateLock.
        var task1 = Task.Run(() =>
            singleton.LockConfigurationThenState(configurationLockTaken, stateLockTaken));

        // Task 2 takes _stateLock first, then waits for _configurationLock.
        var task2 = Task.Run(() =>
            singleton.LockStateThenConfiguration(stateLockTaken, configurationLockTaken));

        // Wait briefly instead of blocking forever. If both tasks are still running,
        // the demo has successfully reproduced the deadlock.
        var completed = Task.WaitAll([task1, task2], TimeSpan.FromMilliseconds(500));

        Console.WriteLine(completed
            ? "   No deadlock detected in this run."
            : "   Deadlock detected: both tasks are waiting for each other's lock.");
        Console.WriteLine("   Fix: always acquire shared locks in one consistent order, or use one lock.");
    }
}
