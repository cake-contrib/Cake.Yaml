using System;
using System.Collections.Generic;
using Cake.Core.IO;
using YamlDotNet.Serialization.NamingConventions;
using Xunit;

namespace Cake.Yaml.Tests
{
    public class YamlTests : IDisposable
    {
        FakeCakeContext context;

        const string SERIALIZED_YAML_DATA = "Name: Testing\nItems:\n- One\n- Two\n- Three\nKeysAndValues:\n  Key: Value\n  AnotherKey: AnotherValue\n  Such: Wow\nNested:\n  Id: 0\n  Value: 7\nMultiples:\n- Id: 1\n  Value: 14\n- Id: 2\n  Value: 29\n- Id: 3\n  Value: 58\n";
        const string SERIALIZED_YAML_DATA_CAMEL_CASE = "name: Testing\nitems:\n- One\n- Two\n- Three\nkeysAndValues:\n  Key: Value\n  AnotherKey: AnotherValue\n  Such: Wow\nnested:\n  id: 0\n  value: 7\nmultiples:\n- id: 1\n  value: 14\n- id: 2\n  value: 29\n- id: 3\n  value: 58\n";

        string SERIALIZED_YAML = "";
        string SERIALIZED_YAML_CAMEL_CASE = "";

        const string SERIALIZED_YAML_MERGE_DATA = "default: &default\n Item1: item1\n Item2: item2\n\nmerged:\n <<: *default\n Item3: test item3\n\nnodefault:\n Item1: nodef-item1\n Item2: nodef-item2\n Item3: nodef-item3\n";

        public YamlTests ()
        {
            SERIALIZED_YAML = SERIALIZED_YAML_DATA.Replace ("\n", Environment.NewLine);
            SERIALIZED_YAML_CAMEL_CASE = SERIALIZED_YAML_DATA_CAMEL_CASE.Replace("\n", Environment.NewLine);

            context = new FakeCakeContext ();           
        }

        public void Dispose ()
        {
            context.DumpLogs ();
        }

        [Fact]
        public void SerializeToString ()
        {
            var obj = new TestObject ();

            var yaml = context.CakeContext.SerializeYaml (obj);

            Assert.NotEmpty (yaml);
            Assert.Equal (SERIALIZED_YAML, yaml);
        }

        [Fact]
        public void SerializeToStringUsingNamingConvention()
        {
            var obj = new TestObject();

            var settings = new SerializeYamlSettings
            {
                NamingConvention = CamelCaseNamingConvention.Instance,
            };

            var yaml = context.CakeContext.SerializeYaml(obj, settings);

            Assert.NotEmpty(yaml);
            Assert.Equal(SERIALIZED_YAML_CAMEL_CASE, yaml);
        }

        [Fact]
        public void DeserializeWithMergeToFile()
        {

        }

        [Fact]
        public void SerializeToFile ()
        {
            var obj = new TestObject ();

            var file = new FilePath ("./serialized.yaml");

            context.CakeContext.SerializeYamlToFile (file, obj);

            var yaml = System.IO.File.ReadAllText (file.MakeAbsolute (context.CakeContext.Environment).FullPath);

            Assert.NotEmpty (yaml);
            Assert.Equal (SERIALIZED_YAML, yaml);
        }

        [Fact]
        public void SerializeToFileUsingNamingConvention()
        {
            var obj = new TestObject();

            var file = new FilePath("./serialized_camelcase.yaml");

            var settings = new SerializeYamlSettings
            {
                NamingConvention = CamelCaseNamingConvention.Instance,
            };

            context.CakeContext.SerializeYamlToFile(file, obj, settings);

            var yaml = System.IO.File.ReadAllText(file.MakeAbsolute(context.CakeContext.Environment).FullPath);

            Assert.NotEmpty(yaml);
            Assert.Equal(SERIALIZED_YAML_CAMEL_CASE, yaml);
        }

        [Fact]
        public void DeserializeFromFile ()
        {
            var file = new FilePath ("test.yaml");

            var testObject = context.CakeContext.DeserializeYamlFromFile<TestObject> (file);

            Assert.NotNull (testObject);
            Assert.Equal ("Testing", testObject.Name);
        }

        [Fact]
        public void DeserializeFromFileUsingNamingConvention()
        {
            var file = new FilePath("test_camelcase.yaml");

            var settings = new DeserializeYamlSettings
            {
                NamingConvention = CamelCaseNamingConvention.Instance,
            };

            var testObject = context.CakeContext.DeserializeYamlFromFile<TestObject>(file, settings);

            Assert.NotNull(testObject);
            Assert.Equal("Testing", testObject.Name);
        }

        [Fact]
        public void DeserializeMergeFromString()
        {
            var testObjectDictionary = context.CakeContext.DeserializeYaml<Dictionary<string, TestMergeObject>>(SERIALIZED_YAML_MERGE_DATA);
            AssertDictionaryMatchesTestMergeYaml(testObjectDictionary);
        }

        [Fact]
        public void DeserializeMergeFromFile()
        {
            var file = new FilePath("testmerge.yaml");

            var testObjectDictionary = context.CakeContext.DeserializeYamlFromFile<Dictionary<string, TestMergeObject>>(file);

            AssertDictionaryMatchesTestMergeYaml(testObjectDictionary);
        }
        private void AssertDictionaryMatchesTestMergeYaml(Dictionary<string, TestMergeObject> testDictionary)
        {
            Assert.Equal(3, testDictionary.Count);
            Assert.Equal("item1", testDictionary["default"].Item1);
            Assert.Equal("item2", testDictionary["default"].Item2);

            // item1 and item 2 are inherited from &default
            Assert.Equal("item1", testDictionary["merged"].Item1);
            Assert.Equal("item2", testDictionary["merged"].Item2);
            Assert.Equal("test item3", testDictionary["merged"].Item3);

            Assert.Equal("nodef-item1", testDictionary["nodefault"].Item1);
            Assert.Equal("nodef-item2", testDictionary["nodefault"].Item2);
            Assert.Equal("nodef-item3", testDictionary["nodefault"].Item3);
        }

        [Fact]
        public void DeserializeFromString ()
        {
            var testObject = context.CakeContext.DeserializeYaml<TestObject> (SERIALIZED_YAML);

            Assert.NotNull (testObject);
            Assert.Equal ("Testing", testObject.Name);
        }

        [Fact]
        public void DeserializeFromStringUsingNamingConvention()
        {
            var settings = new DeserializeYamlSettings
            {
                NamingConvention = CamelCaseNamingConvention.Instance,
            };

            var testObject = context.CakeContext.DeserializeYaml<TestObject>(SERIALIZED_YAML_CAMEL_CASE, settings);

            Assert.NotNull(testObject);
            Assert.Equal("Testing", testObject.Name);
        }
    }
}

