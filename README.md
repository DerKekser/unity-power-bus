# Unity - Power Bus

[Power Bus](https://github.com/DerKekser/unity-power-bus) is a flexible event bus system designed to streamline your Unity game development. It allows for easy communication between different parts of your game without tight coupling.

## Contents
- [Simple Example](#simple-example)
- [Event Handling](#event-handling)
- [Bus Creation](#bus-creation)
- [Initial Value](#initial-value)
- [Bus Manager](#bus-manager)
- [Install](#install)
  - [Install via Unity Package](#install-via-unity-package)
  - [Install via git URL](#install-via-git-url)
- [License](#license)

### Simple Example

You can define an event bus by creating a new instance of the `Bus` class.

```csharp
using Kekser.PowerBus;

public class StringEvent
{
    public string Value { get; set; }
}

var bus = new Bus<StringEvent>();
bus.OnChange += e => Debug.Log(e.Value);
bus.Value = new StringEvent { Value = "Hello World" };
```

### Event Handling

You can handle events by subscribing to the `OnChange` event of the bus.

```csharp
void HandleEvent(StringEvent e)
{
    Debug.Log(e.Value);
}

bus.OnChange += HandleEvent;
```

### Bus Creation

You can create a bus by instantiating the `Bus` class.
  
```csharp
var bus = new Bus<StringEvent>();
bus.Value = new StringEvent { Value = "Hello World" };
```

### Initial Value

You can set the initial value of the bus by passing it to the constructor.

> Note: The initial value will be only set once when the bus is created.

```csharp
var bus = new Bus<StringEvent>(new StringEvent { Value = "Hello World" });
```

### Bus Manager

You can manage multiple buses using the `BusManager` class.

```csharp
var manager = new BusManager();
var bus = new Bus<StringEvent>(manager: manager);
bus.Value = new StringEvent { Value = "Hello World" };
```

### Install

#### Install via Unity Package

Download the latest [release](https://github.com/DerKekser/unity-power-bus/releases) and import the package into your Unity project.
#### Install via git URL

You can add this package to your project by adding this git URL in the Package Manager:
```
https://github.com/DerKekser/unity-power-bus.git?path=Assets/Kekser/PowerBus
```
![Package Manager](/Assets/Kekser/Screenshots/package_manager.png)
### License

This library is under the MIT License.