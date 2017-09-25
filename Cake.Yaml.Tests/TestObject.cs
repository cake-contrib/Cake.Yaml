using System;
using System.Collections.Generic;

namespace Cake.Yaml.Tests
{
    public class TestObject
    {
        public TestObject ()
        {
            Name = "Testing";
            Items = new List<string> { "One", "Two", "Three" };
            KeysAndValues = new Dictionary<string, string> {
                { "Key", "Value" },
                { "AnotherKey", "AnotherValue" },
                { "Such", "Wow" }               
            };
            Nested = new NestedTestObject {
                Id = 0,
                Value = 7
            };
            Multiples = new List<NestedTestObject> {
                new NestedTestObject {
                    Id = 1,
                    Value = 14
                },
                new NestedTestObject {
                    Id = 2,
                    Value = 29
                },
                new NestedTestObject {
                    Id = 3,
                    Value = 58
                }
            };
        }

        public string Name { get;set; }

        public List<string> Items { get; set; }

        public Dictionary<string, string> KeysAndValues { get;set; }

        public NestedTestObject Nested { get; set; }

        public List<NestedTestObject> Multiples { get;set; }
    }

    public class NestedTestObject
    {
        public NestedTestObject ()
        {
        }

        public int Id { get;set; }
        public int Value { get;set; }
    }
}

