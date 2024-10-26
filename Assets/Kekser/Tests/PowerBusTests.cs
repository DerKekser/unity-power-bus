using System.Collections;
using Kekser.PowerBus;
using UnityEngine.Assertions;
using UnityEngine.TestTools;

namespace Kekser.Tests
{
    public class PowerBusTests
    {
        public class Event1
        {
            public string StringValue { get; set; }
        }
        
        public class Event2
        {
            public int StringValue { get; set; }
        }
        
        [UnityTest]
        public IEnumerator TestBusCreation()
        {
            yield return null;
            var bus = new Bus<Event1>();
            Assert.IsNotNull(bus);
        }
        
        [UnityTest]
        public IEnumerator TestBusValue()
        {
            yield return null;
            var bus = new Bus<Event1>();
            bus.Value = new Event1 { StringValue = "Hello" };
            Assert.AreEqual("Hello", bus.Value.StringValue);
        }
        
        [UnityTest]
        public IEnumerator TestBusOnChange()
        {
            yield return null;
            var bus = new Bus<Event1>();
            var value = new Event1 { StringValue = "Hello" };
            bus.Value = value;
            Event1 receivedValue = null;
            bus.OnChange += v => receivedValue = v;
            bus.Value = new Event1 { StringValue = "World" };
            Assert.AreEqual("World", receivedValue.StringValue);
        }
        
        [UnityTest]
        public IEnumerator TestBusOnChangeInline()
        {
            yield return null;
            var bus = new Bus<Event1>((v) => Assert.AreEqual("Hello", v.StringValue));
            bus.Value = new Event1 { StringValue = "Hello" };
        }
        
        [UnityTest]
        public IEnumerator TestBusImplicitConversion()
        {
            yield return null;
            var bus = new Bus<Event1>();
            bus.Value = new Event1 { StringValue = "Hello" };
            Event1 value = bus;
            Assert.AreEqual("Hello", value.StringValue);
        }
        
        [UnityTest]
        public IEnumerator TestBusManager()
        {
            yield return null;
            var manager = new BusManager();
            var bus = new Bus<Event1>(manager: manager);
            bus.Value = new Event1 { StringValue = "Hello" };
            Assert.AreEqual("Hello", bus.Value.StringValue);
        }
        
        [UnityTest]
        public IEnumerator TestBusManagerGlobalInstance()
        {
            yield return null;
            var bus = new Bus<Event1>();
            bus.Value = new Event1 { StringValue = "Hello" };
            Assert.AreEqual("Hello", bus.Value.StringValue);
        }
        
        [UnityTest]
        public IEnumerator TestMultipleBuses()
        {
            yield return null;
            var bus1 = new Bus<Event1>();
            var bus2 = new Bus<Event1>();
            bus1.Value = new Event1 { StringValue = "Hello" };
            Assert.AreEqual("Hello", bus1.Value.StringValue);
            Assert.AreEqual("Hello", bus2.Value.StringValue);
        }
        
        [UnityTest]
        public IEnumerator TestMultipleBusesDifferentValues()
        {
            yield return null;
            var bus1 = new Bus<Event1>();
            var bus2 = new Bus<Event1>();
            bus1.Value = new Event1 { StringValue = "Hello" };
            bus2.Value = new Event1 { StringValue = "World" };
            Assert.AreEqual("World", bus1.Value.StringValue);
            Assert.AreEqual("World", bus2.Value.StringValue);
        }
        
        [UnityTest]
        public IEnumerator TestMultipleBusesOnChange()
        {
            yield return null;
            var bus1 = new Bus<Event1>();
            var bus2 = new Bus<Event1>();
            bus1.Value = new Event1 { StringValue = "Hello" };
            bus2.Value = new Event1 { StringValue = "World" };
            Event1 receivedValue1 = null;
            Event1 receivedValue2 = null;
            bus1.OnChange += v => receivedValue1 = v;
            bus2.OnChange += v => receivedValue2 = v;
            bus1.Value = new Event1 { StringValue = "Changed" };
            Assert.AreEqual("Changed", receivedValue1.StringValue);
            Assert.AreEqual("Changed", receivedValue2.StringValue);
        }
        
        [UnityTest]
        public IEnumerator TestDifferentEvents()
        {
            yield return null;
            var bus = new Bus<Event1>();
            var bus2 = new Bus<Event2>();
            bus.Value = new Event1 { StringValue = "Hello" };
            bus2.Value = new Event2 { StringValue = 42 };
            Assert.AreEqual("Hello", bus.Value.StringValue);
            Assert.AreEqual(42, bus2.Value.StringValue);
        }
        
        [UnityTest]
        public IEnumerator TestDifferentEventsOnChange()
        {
            yield return null;
            var bus = new Bus<Event1>();
            var bus2 = new Bus<Event2>();
            bus.Value = new Event1 { StringValue = "Hello" };
            bus2.Value = new Event2 { StringValue = 42 };
            Event1 receivedValue1 = null;
            Event2 receivedValue2 = null;
            bus.OnChange += v => receivedValue1 = v;
            bus2.OnChange += v => receivedValue2 = v;
            bus.Value = new Event1 { StringValue = "Changed" };
            bus2.Value = new Event2 { StringValue = 43 };
            Assert.AreEqual("Changed", receivedValue1.StringValue);
            Assert.AreEqual(43, receivedValue2.StringValue);
        }
        
        [UnityTest]
        public IEnumerator TestDifferentBusManagers()
        {
            yield return null;
            var manager1 = new BusManager();
            var manager2 = new BusManager();
            var bus1 = new Bus<Event1>(manager: manager1);
            var bus2 = new Bus<Event1>(manager: manager2);
            bus1.Value = new Event1 { StringValue = "Hello" };
            bus2.Value = new Event1 { StringValue = "World" };
            Assert.AreEqual("Hello", bus1.Value.StringValue);
            Assert.AreEqual("World", bus2.Value.StringValue);
        }
        
        [UnityTest]
        public IEnumerator TestDifferentBusManagersOnChange()
        {
            yield return null;
            var manager1 = new BusManager();
            var manager2 = new BusManager();
            var bus1 = new Bus<Event1>(manager: manager1);
            var bus2 = new Bus<Event1>(manager: manager2);
            bus1.Value = new Event1 { StringValue = "Hello" };
            bus2.Value = new Event1 { StringValue = "World" };
            Event1 receivedValue1 = null;
            Event1 receivedValue2 = null;
            bus1.OnChange += v => receivedValue1 = v;
            bus2.OnChange += v => receivedValue2 = v;
            bus1.Value = new Event1 { StringValue = "Changed" };
            bus2.Value = new Event1 { StringValue = "Changed" };
            Assert.AreEqual("Changed", receivedValue1.StringValue);
            Assert.AreEqual("Changed", receivedValue2.StringValue);
        }
        
        [UnityTest]
        public IEnumerator TestBusInitialValue()
        {
            yield return null;
            var manager = new BusManager();
            var bus = new Bus<Event1>(new Event1 { StringValue = "Hello" }, manager);
            Assert.AreEqual("Hello", bus.Value.StringValue);
        }
        
        [UnityTest]
        public IEnumerator TestMultipleBusesInitialValue()
        {
            yield return null;
            var manager = new BusManager();
            var bus1 = new Bus<Event1>(new Event1 { StringValue = "Hello" }, manager);
            var bus2 = new Bus<Event1>(new Event1 { StringValue = "World" }, manager);
            Assert.AreEqual("Hello", bus1.Value.StringValue);
            Assert.AreEqual("Hello", bus2.Value.StringValue);
        }
    }
}