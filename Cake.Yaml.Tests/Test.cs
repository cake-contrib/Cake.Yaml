using NUnit.Framework;
using System;
using Cake.Core.IO;

namespace Cake.Yaml.Tests
{
    [TestFixture ()]
    public class YamlTests
    {
        FakeCakeContext context;

        const string SERIALIZED_YAML = "Name: Testing\nItems:\n- One\n- Two\n- Three\nKeysAndValues:\n  Key: Value\n  AnotherKey: AnotherValue\n  Such: Wow\nNested:\n  Value: 7.3\nMultiples:\n- Id: 1\n  Value: 14.6\n- Id: 2\n  Value: 29.2\n- Id: 3\n  Value: 58.4\n";

        [SetUp]
        public void Setup ()
        {
            context = new FakeCakeContext ();           
        }

        [TearDown]
        public void Teardown ()
        {
            context.DumpLogs ();
        }

        [Test]
        public void SerializeToString ()
        {
            var obj = new TestObject ();

            var yaml = context.CakeContext.SerializeYaml (obj);

            Assert.IsNotEmpty (yaml);
            Assert.AreEqual (SERIALIZED_YAML, yaml);
        }

        [Test]
        public void SerializeToFile ()
        {
            var obj = new TestObject ();

            var file = new FilePath ("./serialized.yaml");

            context.CakeContext.SerializeYamlToFile (file, obj);

            var yaml = System.IO.File.ReadAllText (file.MakeAbsolute (context.CakeContext.Environment).FullPath);

            Assert.IsNotEmpty (yaml);
            Assert.AreEqual (SERIALIZED_YAML, yaml);
        }

        [Test]
        public void DeserializeFromFile ()
        {
            var file = new FilePath ("test.yaml");

            var testObject = context.CakeContext.DeserializeYamlFromFile<TestObject> (file);

            Assert.IsNotNull (testObject);
            Assert.AreEqual ("Testing", testObject.Name);
        }

        [Test]
        public void DeserializeFromString ()
        {
            var testObject = context.CakeContext.DeserializeYaml<TestObject> (SERIALIZED_YAML);

            Assert.IsNotNull (testObject);
            Assert.AreEqual ("Testing", testObject.Name);
        }
    }
}

