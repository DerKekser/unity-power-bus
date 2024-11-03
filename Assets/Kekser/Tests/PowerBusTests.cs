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
            var bus = new Bus<Event1>();
            Assert.IsNotNull(bus);
            bus.Dispose();
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator TestBusOn()
        {
            var bus = new Bus<Event1>();
            bus.Trigger(new Event1 { StringValue = "Hello" });
            Event1 receivedValue = null;
            bus.On += (v) => receivedValue = v;
            bus.Trigger(new Event1 { StringValue = "World" });
            Assert.AreEqual("World", receivedValue.StringValue);
            bus.Dispose();
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator TestBusOnInline()
        {
            var bus = new Bus<Event1>((v) => Assert.AreEqual("Hello", v.StringValue));
            bus.Trigger(new Event1 { StringValue = "Hello" });
            bus.Dispose();
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator TestBusManager()
        {
            var manager = new BusManager();
            var bus = new Bus<Event1>(manager: manager);
            bus.On += (v) => Assert.AreEqual("Hello", v.StringValue);
            bus.Trigger(new Event1 { StringValue = "Hello" });
            bus.Dispose();
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator TestBusManagerGlobalInstance()
        {
            var bus = new Bus<Event1>();
            bus.On += v => Assert.AreEqual("Hello", v.StringValue);
            bus.Trigger(new Event1 { StringValue = "Hello" });
            bus.Dispose();
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator TestMultipleBuses()
        {
            var bus1 = new Bus<Event1>();
            var bus2 = new Bus<Event1>();
            bus1.On += (v) => Assert.AreEqual("Hello", v.StringValue);
            bus2.On += (v) => Assert.AreEqual("Hello", v.StringValue);
            bus1.Trigger(new Event1 { StringValue = "Hello" });
            bus1.Dispose();
            bus2.Dispose();
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator TestMultipleBusesOn()
        {
            var bus1 = new Bus<Event1>();
            var bus2 = new Bus<Event1>();
            Event1 receivedValue1 = null;
            Event1 receivedValue2 = null;
            bus1.On += (v) => receivedValue1 = v;
            bus2.On += (v) => receivedValue2 = v;
            bus1.Trigger(new Event1 { StringValue = "Hello" });
            bus2.Trigger(new Event1 { StringValue = "World" });
            Assert.AreEqual("World", receivedValue1.StringValue);
            Assert.AreEqual("World", receivedValue2.StringValue);
            bus1.Dispose();
            bus2.Dispose();
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator TestDifferentEvents()
        {
            var bus1 = new Bus<Event1>();
            var bus2 = new Bus<Event2>();
            bus1.On += (v) => Assert.AreEqual("Hello", v.StringValue);
            bus2.On += (v) => Assert.AreEqual(42, v.StringValue);
            bus1.Trigger(new Event1 { StringValue = "Hello" });
            bus2.Trigger(new Event2 { StringValue = 42 });
            bus1.Dispose();
            bus2.Dispose();
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator TestDifferentEventsOn()
        {
            var bus1 = new Bus<Event1>();
            var bus2 = new Bus<Event2>();
            Event1 receivedValue1 = null;
            Event2 receivedValue2 = null;
            bus1.On += (v) => receivedValue1 = v;
            bus2.On += (v) => receivedValue2 = v;
            bus1.Trigger(new Event1 { StringValue = "Hello" });
            bus2.Trigger(new Event2 { StringValue = 42 });
            bus1.Trigger(new Event1 { StringValue = "World" });
            bus2.Trigger(new Event2 { StringValue = 43 });
            Assert.AreEqual("World", receivedValue1.StringValue);
            Assert.AreEqual(43, receivedValue2.StringValue);
            bus1.Dispose();
            bus2.Dispose();
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator TestDifferentBusManagers()
        {
            var manager1 = new BusManager();
            var manager2 = new BusManager();
            var bus1 = new Bus<Event1>(manager: manager1);
            var bus2 = new Bus<Event1>(manager: manager2);
            bus1.On += (v) => Assert.AreEqual("Hello", v.StringValue);
            bus2.On += (v) => Assert.AreEqual("World", v.StringValue);
            bus1.Trigger(new Event1 { StringValue = "Hello" });
            bus2.Trigger(new Event1 { StringValue = "World" });
            bus1.Dispose();
            bus2.Dispose();
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator TestDifferentBusManagersOn()
        {
            var manager1 = new BusManager();
            var manager2 = new BusManager();
            var bus1 = new Bus<Event1>(manager: manager1);
            var bus2 = new Bus<Event1>(manager: manager2);
            Event1 receivedValue1 = null;
            Event1 receivedValue2 = null;
            bus1.On += (v) => receivedValue1 = v;
            bus2.On += (v) => receivedValue2 = v;
            bus1.Trigger(new Event1 { StringValue = "Hello" });
            bus2.Trigger(new Event1 { StringValue = "World" });
            bus1.Trigger(new Event1 { StringValue = "Changed" });
            bus2.Trigger(new Event1 { StringValue = "Changed" });
            Assert.AreEqual("Changed", receivedValue1.StringValue);
            Assert.AreEqual("Changed", receivedValue2.StringValue);
            bus1.Dispose();
            bus2.Dispose();
            yield return null;
        }
    }
}