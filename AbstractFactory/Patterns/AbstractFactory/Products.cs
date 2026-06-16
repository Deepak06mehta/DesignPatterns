namespace AbstractFactory.Patterns.AbstractFactory;

// ────────────────────────────────────────────────────────────────
// Product Interfaces
//
// These define the contract for each type of UI widget.
// Every theme (Light, Dark, HighContrast) will provide its own
// versions of these widgets.
// ────────────────────────────────────────────────────────────────

// A clickable button widget.
public interface IButton
{
    void Render();
    void Click();
}

// A text input box widget.
public interface ITextBox
{
    void Render();
    void Input(string text);
}

// A display label widget.
public interface ILabel
{
    void Render();
    void Display(string text);
}

// ────────────────────────────────────────────────────────────────
// Light Theme Widgets
// ────────────────────────────────────────────────────────────────
public class LightButton : IButton
{
    public void Render() => Console.WriteLine("  [LightButton] Rendered with white background, black text.");
    public void Click() => Console.WriteLine("  [LightButton] Clicked! Button glows blue.");
}

public class LightTextBox : ITextBox
{
    public void Render() => Console.WriteLine("  [LightTextBox] Rendered with white background, gray border.");
    public void Input(string text) => Console.WriteLine($"  [LightTextBox] User typed: \"{text}\"");
}

public class LightLabel : ILabel
{
    public void Render() => Console.WriteLine("  [LightLabel] Rendered with white background, dark gray text.");
    public void Display(string text) => Console.WriteLine($"  [LightLabel] Showing: \"{text}\"");
}

// ────────────────────────────────────────────────────────────────
// Dark Theme Widgets
// ────────────────────────────────────────────────────────────────
public class DarkButton : IButton
{
    public void Render() => Console.WriteLine("  [DarkButton] Rendered with black background, white text.");
    public void Click() => Console.WriteLine("  [DarkButton] Clicked! Button glows purple.");
}

public class DarkTextBox : ITextBox
{
    public void Render() => Console.WriteLine("  [DarkTextBox] Rendered with dark gray background, light border.");
    public void Input(string text) => Console.WriteLine($"  [DarkTextBox] User typed: \"{text}\"");
}

public class DarkLabel : ILabel
{
    public void Render() => Console.WriteLine("  [DarkLabel] Rendered with black background, light gray text.");
    public void Display(string text) => Console.WriteLine($"  [DarkLabel] Showing: \"{text}\"");
}

// ────────────────────────────────────────────────────────────────
// High Contrast Theme Widgets
// ────────────────────────────────────────────────────────────────
public class HighContrastButton : IButton
{
    public void Render() => Console.WriteLine("  [HighContrastButton] Rendered with black background, yellow text.");
    public void Click() => Console.WriteLine("  [HighContrastButton] Clicked! Button border turns red.");
}

public class HighContrastTextBox : ITextBox
{
    public void Render() => Console.WriteLine("  [HighContrastTextBox] Rendered with black background, yellow border.");
    public void Input(string text) => Console.WriteLine($"  [HighContrastTextBox] User typed: \"{text}\"");
}

public class HighContrastLabel : ILabel
{
    public void Render() => Console.WriteLine("  [HighContrastLabel] Rendered with black background, yellow text.");
    public void Display(string text) => Console.WriteLine($"  [HighContrastLabel] Showing: \"{text}\"");
}
