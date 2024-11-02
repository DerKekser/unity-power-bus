# Unity - Power Bus

[Power Bus](https://github.com/DerKekser/unity-power-bus) is a flexible event bus system designed to streamline your Unity game development. It allows for easy communication between different parts of your game without tight coupling.

## Contents
- [Simple Example](#simple-example)
- [Event Handling](#event-handling)
- [Bus Creation](#bus-creation)
  - [Inline Event Handling](#inline-event-handling)
  - [Initial Value](#initial-value)
  - [Bus Manager](#bus-manager)
- [Global Trigger](#global-trigger)
- [Game Object Bus](#game-object-bus)
  - [Game Object Bus Global](#game-object-bus-global)
  - [Game Object Bus Local](#game-object-bus-local)
- [Dispose](#dispose)
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
bus.Trigger(new StringEvent { Value = "Hello World" });
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
bus.Trigger(new StringEvent { Value = "Hello World" });
```

#### Inline Event Handling

You can handle events inline by passing a lambda function or a method to the constructor.

```csharp
var bus = new Bus<StringEvent>(e => Debug.Log(e.Value));
bus.Trigger(new StringEvent { Value = "Hello World" });

// or

void HandleEvent(StringEvent e)
{
    Debug.Log(e.Value);
}

var bus = new Bus<StringEvent>(HandleEvent);
bus.Trigger(new StringEvent { Value = "Hello World" });
```

#### Initial Value

You can set the initial value of the bus by passing it to the constructor.

> Note: The initial value will be only set once when the bus is created.

```csharp
var bus = new Bus<StringEvent>(new StringEvent { Value = "Hello World" });
```

#### Bus Manager

You can manage multiple buses using the `BusManager` class.

```csharp
var manager = new BusManager();
var bus = new Bus<StringEvent>(manager: manager);
bus.Trigger(new StringEvent { Value = "Hello World" });
```

### Global Trigger

You can trigger an event globally by calling the `Trigger` method on the static bus class.

```csharp
Bus.Trigger<StringEvent>(new StringEvent { Value = "Hello World" });
```

### Game Object Bus

You can access the bus from a game object.

#### Game Object Bus Global

You can access a bus from any game object.

```csharp
var bus = gameObject.Bus<StringEvent>();
bus.Trigger(new StringEvent { Value = "Hello World" });
```

#### Game Object Bus Local

You can access a bus from a game object that only triggers events locally.

```csharp
var bus = gameObject.LocalBus<StringEvent>();
bus.Trigger(new StringEvent { Value = "Hello World" });
```

### Dispose

You can dispose of the bus object by calling the `Dispose` method.

> Note: You should always dispose of the bus when you no longer need it.

```csharp
bus.Dispose();
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