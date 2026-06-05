# Singleton Flow Diagrams

This document explains the flow for every singleton scenario demonstrated in this folder.

## Demo Entry Flow

`Program.cs` calls `SingletonDemo.Run()`. The demo then runs five scenarios in order.

```text
Program.cs
   |
   v
SingletonDemo.Run()
   |
   +--> 1. EagerSingleton
   +--> 2. LazySingleton
   +--> 3. ManualLazySingleton
   +--> 4. UnsafeLazySingleton race condition demo
   +--> 5. DeadlockProneSingleton deadlock demo
```

## 1. Eager Singleton

File: `EagerSingleton.cs`

The eager singleton creates its instance when the type is first accessed. The CLR handles static initialization safely, so no manual lock is needed.

```text
Application asks for EagerSingleton.Instance
   |
   v
CLR loads EagerSingleton type
   |
   v
static readonly _instance = new EagerSingleton()
   |
   v
Private constructor runs once
   |
   v
Instance property returns _instance
   |
   v
Later calls return the same _instance
```

Key point: object creation happens early, before the application knows whether the instance is actually needed.

## 2. Lazy Singleton Using Lazy<T>

File: `LazySingleton.cs`

The lazy singleton delays object creation until `Instance` is requested for the first time.

```text
Application asks for LazySingleton.Instance
   |
   v
Instance reads _lazy.Value
   |
   +--> First call?
   |       |
   |       v
   |   Lazy<T> runs factory method
   |       |
   |       v
   |   new LazySingleton()
   |       |
   |       v
   |   Lazy<T> stores the created object
   |
   +--> Later call?
           |
           v
       Lazy<T> returns stored object
```

Key point: `Lazy<T>` gives lazy creation and thread safety without custom locking code.

## 3. Manual Lazy Singleton With Double-Checked Locking

File: `ManualLazySingleton.cs`

This version manually delays object creation and uses a lock to prevent multiple threads from creating more than one instance.

```text
Thread asks for ManualLazySingleton.Instance
   |
   v
Check 1: is _instance null?
   |
   +--> No
   |       |
   |       v
   |   Return existing _instance
   |
   +--> Yes
           |
           v
       Enter lock(_lock)
           |
           v
       Check 2: is _instance still null?
           |
           +--> No
           |       |
           |       v
           |   Another thread already created it
           |       |
           |       v
           |   Return existing _instance
           |
           +--> Yes
                   |
                   v
               Create new ManualLazySingleton()
                   |
                   v
               Store in _instance
                   |
                   v
               Return _instance
```

Key point: the first check avoids locking after creation. The second check prevents duplicate creation if another thread entered the lock first.

## 4. Race Condition Demo

Files: `UnsafeSingletonExamples.cs`, `SingletonDemo.cs`

`UnsafeLazySingleton` intentionally does not use a lock. The demo starts many threads at the same time with `Parallel.For`.

```text
Parallel.For starts many workers
   |
   +--> Thread A asks for Instance
   |       |
   |       v
   |   Sees _instance == null
   |       |
   |       v
   |   Sleeps for demo delay
   |
   +--> Thread B asks for Instance at the same time
   |       |
   |       v
   |   Also sees _instance == null
   |       |
   |       v
   |   Sleeps for demo delay
   |
   +--> Thread C asks for Instance at the same time
           |
           v
       Also sees _instance == null
```

After the delay, multiple threads create separate objects.

```text
Thread A creates UnsafeLazySingleton #1
Thread B creates UnsafeLazySingleton #2
Thread C creates UnsafeLazySingleton #3
   |
   v
_createdCount becomes greater than 1
   |
   v
Demo prints: Objects created: N
```

Expected singleton behavior is exactly one object. If the demo prints more than `1`, it has reproduced the race condition.

## 5. Deadlock Demo

Files: `UnsafeSingletonExamples.cs`, `SingletonDemo.cs`

`DeadlockProneSingleton` intentionally has two locks:

```text
_configurationLock
_stateLock
```

The deadlock happens because two tasks acquire those locks in opposite order.

```text
Task 1                                      Task 2
------                                      ------
lock(_configurationLock)                    lock(_stateLock)
   |                                           |
   v                                           v
wait until Task 2 has _stateLock             wait until Task 1 has _configurationLock
   |                                           |
   v                                           v
try lock(_stateLock)                         try lock(_configurationLock)
   |                                           |
   v                                           v
blocked because Task 2 holds it              blocked because Task 1 holds it
```

The final state is a circular wait.

```text
Task 1 owns _configurationLock and waits for _stateLock
Task 2 owns _stateLock and waits for _configurationLock
   |
   v
Neither task can continue
   |
   v
Deadlock
```

The demo uses `Task.WaitAll(..., TimeSpan.FromMilliseconds(500))` so the console application does not freeze forever.

```text
Start both tasks
   |
   v
Wait up to 500 ms
   |
   +--> Both tasks finish
   |       |
   |       v
   |   No deadlock detected
   |
   +--> Timeout expires
           |
           v
       Deadlock detected
```

## Fixing The Deadlock Pattern

Use one consistent lock order everywhere.

```text
Correct order used by all code paths:

lock(_configurationLock)
   |
   v
lock(_stateLock)
   |
   v
perform work
```

Do not allow another code path to reverse that order.

```text
Bad reversed order:

lock(_stateLock)
   |
   v
lock(_configurationLock)
```

## Scenario Summary

| Scenario | Thread-safe | Lazy creation | Problem demonstrated |
| --- | --- | --- | --- |
| `EagerSingleton` | Yes | No | None |
| `LazySingleton` | Yes | Yes | None |
| `ManualLazySingleton` | Yes | Yes | Shows explicit locking |
| `UnsafeLazySingleton` | No | Yes | Race condition |
| `DeadlockProneSingleton` | No | Not the focus | Deadlock from inconsistent lock order |
