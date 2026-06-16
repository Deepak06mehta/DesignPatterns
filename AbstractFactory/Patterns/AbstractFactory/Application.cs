namespace AbstractFactory.Patterns.AbstractFactory;

// ────────────────────────────────────────────────────────────────
// The Client — Application
//
// This class represents a desktop application that needs a set of
// UI widgets. It receives a factory and uses it to create a
// complete, matching set of widgets.
//
// The key point: Application does NOT know which theme it is using.
// It just calls factory.CreateButton(), factory.CreateTextBox(),
// and factory.CreateLabel(). The factory decides whether those
// are Light, Dark, or HighContrast widgets.
// ────────────────────────────────────────────────────────────────
public class Application
{
    private readonly IButton _button;
    private readonly ITextBox _textBox;
    private readonly ILabel _label;

    // The factory is "injected" through the constructor.
    // Whatever factory is passed in determines the theme.
    public Application(IUiWidgetFactory factory)
    {
        Console.WriteLine($"  [Application] Creating UI with {factory.GetType().Name}...");
        _button = factory.CreateButton();
        _textBox = factory.CreateTextBox();
        _label = factory.CreateLabel();
    }

    // Simulates rendering the entire UI.
    public void RenderUI()
    {
        Console.WriteLine("  [Application] Rendering UI...");
        _label.Render();
        _textBox.Render();
        _button.Render();
    }

    // Simulates the user interacting with the UI.
    public void SimulateUserInteraction()
    {
        Console.WriteLine("  [Application] Simulating user interaction...");
        _label.Display("Welcome to the app!");
        _textBox.Input("Hello, World!");
        _button.Click();
    }
}
