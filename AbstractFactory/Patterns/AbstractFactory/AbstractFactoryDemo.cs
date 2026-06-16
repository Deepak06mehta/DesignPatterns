namespace AbstractFactory.Patterns.AbstractFactory;

// ────────────────────────────────────────────────────────────────
// AbstractFactoryDemo — Runs the Abstract Factory example
//
// This demonstrates how the Abstract Factory pattern lets us
// switch between entire families of products (themes) with
// minimal code change. Each theme factory creates a complete,
// matching set of widgets.
// ────────────────────────────────────────────────────────────────
public static class AbstractFactoryDemo
{
    public static void Run()
    {
        Console.WriteLine("=== Abstract Factory Pattern Demo ===\n");
        Console.WriteLine("The Abstract Factory creates FAMILIES of related products.\n");
        Console.WriteLine("Here, each \"theme\" factory creates a matching set of\nUI widgets (Button, TextBox, Label) that all look consistent.\n");

        // Use Light theme
        Console.WriteLine("─".PadRight(60, '─'));
        Console.WriteLine("THEME 1: Light Theme");
        Console.WriteLine("─".PadRight(60, '─'));
        var lightApp = new Application(new LightThemeFactory());
        lightApp.RenderUI();
        lightApp.SimulateUserInteraction();

        Console.WriteLine();

        // Use Dark theme
        Console.WriteLine("─".PadRight(60, '─'));
        Console.WriteLine("THEME 2: Dark Theme");
        Console.WriteLine("─".PadRight(60, '─'));
        var darkApp = new Application(new DarkThemeFactory());
        darkApp.RenderUI();
        darkApp.SimulateUserInteraction();

        Console.WriteLine();

        // Use High Contrast theme
        Console.WriteLine("─".PadRight(60, '─'));
        Console.WriteLine("THEME 3: High Contrast Theme");
        Console.WriteLine("─".PadRight(60, '─'));
        var highContrastApp = new Application(new HighContrastThemeFactory());
        highContrastApp.RenderUI();
        highContrastApp.SimulateUserInteraction();

        Console.WriteLine("\n" + "─".PadRight(60, '─'));
        Console.WriteLine("KEY INSIGHT");
        Console.WriteLine("─".PadRight(60, '─'));
        Console.WriteLine("The Application class never mentions LightButton, DarkButton,");
        Console.WriteLine("or any concrete widget. It only works with IButton, ITextBox,");
        Console.WriteLine("and ILabel. The factory decides which specific widgets to create.");
        Console.WriteLine("This guarantees that all widgets in the app share the same theme.");
    }
}
