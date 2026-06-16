namespace AbstractFactory.Patterns.AbstractFactory;

// ────────────────────────────────────────────────────────────────
// Abstract Factory Interface
//
// This is the core of the Abstract Factory pattern.
// It declares methods for creating each widget type in a family.
// Each concrete factory will create a matching set of widgets
// that all belong to the same theme.
// ────────────────────────────────────────────────────────────────
public interface IUiWidgetFactory
{
    IButton CreateButton();
    ITextBox CreateTextBox();
    ILabel CreateLabel();
}

// ────────────────────────────────────────────────────────────────
// Light Theme Factory
//
// Creates Light-themed widgets. All widgets from this factory
// share the same visual style (light backgrounds, dark text).
// ────────────────────────────────────────────────────────────────
public class LightThemeFactory : IUiWidgetFactory
{
    public IButton CreateButton() => new LightButton();
    public ITextBox CreateTextBox() => new LightTextBox();
    public ILabel CreateLabel() => new LightLabel();
}

// ────────────────────────────────────────────────────────────────
// Dark Theme Factory
//
// Creates Dark-themed widgets. All widgets from this factory
// share the same visual style (dark backgrounds, light text).
// ────────────────────────────────────────────────────────────────
public class DarkThemeFactory : IUiWidgetFactory
{
    public IButton CreateButton() => new DarkButton();
    public ITextBox CreateTextBox() => new DarkTextBox();
    public ILabel CreateLabel() => new DarkLabel();
}

// ────────────────────────────────────────────────────────────────
// High Contrast Theme Factory
//
// Creates HighContrast-themed widgets. All widgets from this
// factory share the same visual style (high contrast colors
// for accessibility).
// ────────────────────────────────────────────────────────────────
public class HighContrastThemeFactory : IUiWidgetFactory
{
    public IButton CreateButton() => new HighContrastButton();
    public ITextBox CreateTextBox() => new HighContrastTextBox();
    public ILabel CreateLabel() => new HighContrastLabel();
}
