# Abstract Factory Pattern

This project demonstrates the **Abstract Factory** pattern using a UI theme system. You can switch between **Light**, **Dark**, and **High Contrast** themes, and every widget in the application automatically matches the chosen theme.

Run it with:

```bash
dotnet run --project AbstractFactory/AbstractFactory.csproj
```

---

## What is Abstract Factory?

The Abstract Factory pattern lets you create **families of related objects** without specifying their concrete classes.

**Real-world analogy:** Think of IKEA furniture sets. A "Kallax factory" gives you a Kallax shelf + Kallax drawers that fit together perfectly. A "Billy factory" gives you a Billy shelf + Billy doors. You never mix parts from different sets because they won't fit.

In this project:
- **LightThemeFactory** creates LightButton + LightTextBox + LightLabel (all light-themed)
- **DarkThemeFactory** creates DarkButton + DarkTextBox + DarkLabel (all dark-themed)
- **HighContrastThemeFactory** creates HighContrastButton + HighContrastTextBox + HighContrastLabel (all high-contrast)

---

## Overall Project Flow

```mermaid
flowchart TD
    Start([Program starts]) --> Demo[AbstractFactoryDemo.Run]
    Demo --> T1[Light Theme]
    Demo --> T2[Dark Theme]
    Demo --> T3[High Contrast Theme]

    T1 --> LApp["new Application(LightThemeFactory)"]
    LApp --> LRender["RenderUI()"]
    LApp --> LInteract["SimulateUserInteraction()"]

    T2 --> DApp["new Application(DarkThemeFactory)"]
    DApp --> DRender["RenderUI()"]
    DApp --> DInteract["SimulateUserInteraction()"]

    T3 --> HApp["new Application(HighContrastThemeFactory)"]
    HApp --> HRender["RenderUI()"]
    HApp --> HInteract["SimulateUserInteraction()"]

    LApp & DApp & HApp --> Insight([Key Insight shown])
```

---

## Complete UML Class Diagram

```mermaid
classDiagram
    class IUiWidgetFactory {
        <<interface>>
        +CreateButton() IButton
        +CreateTextBox() ITextBox
        +CreateLabel() ILabel
    }

    class IButton {
        <<interface>>
        +Render()
        +Click()
    }

    class ITextBox {
        <<interface>>
        +Render()
        +Input(text)
    }

    class ILabel {
        <<interface>>
        +Render()
        +Display(text)
    }

    class LightThemeFactory {
        +CreateButton() IButton
        +CreateTextBox() ITextBox
        +CreateLabel() ILabel
    }

    class DarkThemeFactory {
        +CreateButton() IButton
        +CreateTextBox() ITextBox
        +CreateLabel() ILabel
    }

    class HighContrastThemeFactory {
        +CreateButton() IButton
        +CreateTextBox() ITextBox
        +CreateLabel() ILabel
    }

    class LightButton {
        +Render()
        +Click()
    }

    class DarkButton {
        +Render()
        +Click()
    }

    class HighContrastButton {
        +Render()
        +Click()
    }

    class LightTextBox {
        +Render()
        +Input(text)
    }

    class DarkTextBox {
        +Render()
        +Input(text)
    }

    class HighContrastTextBox {
        +Render()
        +Input(text)
    }

    class LightLabel {
        +Render()
        +Display(text)
    }

    class DarkLabel {
        +Render()
        +Display(text)
    }

    class HighContrastLabel {
        +Render()
        +Display(text)
    }

    class Application {
        -IButton _button
        -ITextBox _textBox
        -ILabel _label
        +Application(factory)
        +RenderUI()
        +SimulateUserInteraction()
    }

    IUiWidgetFactory <|.. LightThemeFactory : implements
    IUiWidgetFactory <|.. DarkThemeFactory : implements
    IUiWidgetFactory <|.. HighContrastThemeFactory : implements

    IButton <|.. LightButton : implements
    IButton <|.. DarkButton : implements
    IButton <|.. HighContrastButton : implements

    ITextBox <|.. LightTextBox : implements
    ITextBox <|.. DarkTextBox : implements
    ITextBox <|.. HighContrastTextBox : implements

    ILabel <|.. LightLabel : implements
    ILabel <|.. DarkLabel : implements
    ILabel <|.. HighContrastLabel : implements

    LightThemeFactory ..> LightButton : creates
    LightThemeFactory ..> LightTextBox : creates
    LightThemeFactory ..> LightLabel : creates

    DarkThemeFactory ..> DarkButton : creates
    DarkThemeFactory ..> DarkTextBox : creates
    DarkThemeFactory ..> DarkLabel : creates

    HighContrastThemeFactory ..> HighContrastButton : creates
    HighContrastThemeFactory ..> HighContrastTextBox : creates
    HighContrastThemeFactory ..> HighContrastLabel : creates

    Application --> IUiWidgetFactory : uses
    Application --> IButton : uses
    Application --> ITextBox : uses
    Application --> ILabel : uses
```

---

## How One Theme Flows (Light Theme Example)

```mermaid
flowchart LR
    Demo["AbstractFactoryDemo.Run()"] -->|"new LightThemeFactory()"| Factory[LightThemeFactory]
    Demo -->|"passes factory"| App["new Application(factory)"]

    App --> CreateButton["factory.CreateButton()"]
    App --> CreateTextBox["factory.CreateTextBox()"]
    App --> CreateLabel["factory.CreateLabel()"]

    CreateButton --> LB[LightButton]
    CreateTextBox --> LT[LightTextBox]
    CreateLabel --> LL[LightLabel]

    LB & LT & LL --> Render["Application.RenderUI()"]
    Render --> R1["LightLabel.Render()"]
    Render --> R2["LightTextBox.Render()"]
    Render --> R3["LightButton.Render()"]

    R1 & R2 & R3 --> Interact["Application.SimulateUserInteraction()"]
    Interact --> I1["LightLabel.Display()"]
    Interact --> I2["LightTextBox.Input()"]
    Interact --> I3["LightButton.Click()"]
```

The same flow applies for **Dark Theme** and **High Contrast Theme** — the only difference is which factory is passed to `Application`.

---

## Key Concepts

| Concept | How it's shown here |
|---|---|
| **Abstract Factory Interface** | `IUiWidgetFactory` — declares `CreateButton()`, `CreateTextBox()`, `CreateLabel()` |
| **Product Interfaces** | `IButton`, `ITextBox`, `ILabel` — each widget type has its own contract |
| **Concrete Factories** | `LightThemeFactory`, `DarkThemeFactory`, `HighContrastThemeFactory` — each creates a matching family |
| **Concrete Products** | `LightButton`, `DarkTextBox`, `HighContrastLabel`, etc. — actual widget implementations |
| **Client** | `Application` — receives a factory, creates widgets, never knows concrete types |
| **Family Consistency** | Light factory always creates light widgets; dark factory always creates dark widgets |

---

## Why This Pattern Matters

- **You can swap entire families** by changing one line: `new Application(new DarkThemeFactory())` instead of `new Application(new LightThemeFactory())`
- **Products are guaranteed to be compatible** — you'll never accidentally pair a dark button with a light label
- **The client is decoupled** from concrete classes — it only depends on interfaces
- **Adding a new theme** means creating a new factory + new widget classes — no existing code is modified

---

## Files in this project

| File | What it contains |
|---|---|
| `Program.cs` | Application entry point |
| `Patterns/AbstractFactory/Products.cs` | `IButton`, `ITextBox`, `ILabel` interfaces + all concrete widget implementations |
| `Patterns/AbstractFactory/WidgetFactory.cs` | `IUiWidgetFactory` interface + `LightThemeFactory`, `DarkThemeFactory`, `HighContrastThemeFactory` |
| `Patterns/AbstractFactory/Application.cs` | The client — uses the factory to create and interact with widgets |
| `Patterns/AbstractFactory/AbstractFactoryDemo.cs` | Runs the demo for all three themes |
