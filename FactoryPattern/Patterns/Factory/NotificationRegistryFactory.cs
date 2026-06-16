namespace FactoryPattern.Patterns.Factory;

// ────────────────────────────────────────────────────────────────
// Registration-Based Factory
//
// WHAT: Instead of a hardcoded switch statement, this factory
//       stores a dictionary of "recipes" (delegates) for creating
//       objects. You can add new recipes at any time by calling
//       Register().
//
// WHY:  To add a new product type, you just call Register().
//       No switch statements to edit, no existing code to change.
//       This makes it great for plugin systems or when you want
//       to add types without modifying the factory itself.
//
// HOW IT WORKS:
//   - Register("slack", () => new SlackNotification())
//     stores a recipe under the key "slack".
//   - Create("slack") looks up the recipe and runs it to get
//     a new SlackNotification object.
// ────────────────────────────────────────────────────────────────
public class NotificationRegistryFactory
{
    // Internal dictionary that stores the creation recipes.
    // StringComparer.OrdinalIgnoreCase means keys are case-insensitive
    // (so "Slack" and "slack" are treated the same).
    private readonly Dictionary<string, Func<INotification>> _registry = new(StringComparer.OrdinalIgnoreCase);

    // Adds a new creation recipe to the registry.
    // "key" is the name you'll use later to create objects.
    // "createNotification" is a function that returns a new INotification.
    public void Register(string key, Func<INotification> createNotification)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException("Key required.", nameof(key));
        _registry[key] = createNotification ?? throw new ArgumentNullException(nameof(createNotification));
    }

    // Looks up a recipe by key and runs it to create a new object.
    // If the key is not found, it throws an error.
    public INotification Create(string key) =>
        _registry.TryGetValue(key, out var createNotification)
            ? createNotification()
            : throw new InvalidOperationException($"No factory registered for '{key}'.");
}
