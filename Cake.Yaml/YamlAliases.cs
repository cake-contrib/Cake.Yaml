using System;
using Cake.Core.Annotations;
using Cake.Core.IO;
using Cake.Core;
using System.IO;
using System.Text;
using YamlDotNet.Core;

namespace Cake.Yaml
{
    /// <summary>
    /// YAML related cake aliases.
    /// </summary>
    [CakeAliasCategory ("Yaml")]
    public static class YamlAliases
    {
        /// <summary>
        /// Deserializes the YAML from a file.
        /// </summary>
        /// <returns>The Deserialized Object.</returns>
        /// <param name="context">The context.</param>
        /// <param name="filename">The YAML filename.</param>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        [CakeMethodAlias]
        //[CakeNamespaceImport("YamlDotNet")]
        public static T DeserializeYamlFromFile<T>(this ICakeContext context, FilePath filename)
        {
            T result = default(T);

            var d = new YamlDotNet.Serialization.Deserializer();

            using (var tr = File.OpenText(filename.MakeAbsolute(context.Environment).FullPath))
            {
                var reader = new EventReader(new MergingParser(new Parser(tr)));
                result = d.Deserialize<T>(reader);
            }

            return result;
        }

        /// <summary>
        /// Deserializes the YAML from a string.
        /// </summary>
        /// <returns>The Deserialized Object.</returns>
        /// <param name="context">The context.</param>
        /// <param name="yaml">The YAML string.</param>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        [CakeMethodAlias]
        //[CakeNamespaceImport("YamlDotNet")]
        public static T DeserializeYaml<T> (this ICakeContext context, string yaml)
        {
            T result = default(T);

            var d = new YamlDotNet.Serialization.Deserializer ();
            using (var tr = new StringReader(yaml))
            {
                var reader = new EventReader(new MergingParser(new Parser(tr)));
                result = d.Deserialize<T>(reader);
            }
            return result;
        }

        /// <summary>
        /// Serializes an object to a YAML file.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="filename">The filename to serialize to.</param>
        /// <param name="instance">The object to serialize.</param>
        /// <typeparam name="T">The type of object to serialize.</typeparam>
        [CakeMethodAlias]
        //[CakeNamespaceImport("YamlDotNet")]
        public static void SerializeYamlToFile<T> (this ICakeContext context, FilePath filename, T instance)
        {
            var s = new YamlDotNet.Serialization.Serializer ();

            using (var tw = new StreamWriter (filename.MakeAbsolute (context.Environment).FullPath))
                s.Serialize (tw, instance);
        }

        /// <summary>
        /// Serializes an object to a YAML string.
        /// </summary>
        /// <returns>The YAML string.</returns>
        /// <param name="context">The context.</param>
        /// <param name="instance">The object to serialize.</param>
        /// <typeparam name="T">The type of object to serialize.</typeparam>
        [CakeMethodAlias]
        //[CakeNamespaceImport("YamlDotNet")]
        public static string SerializeYaml<T> (this ICakeContext context, T instance)
        {
            var s = new YamlDotNet.Serialization.Serializer ();

            var sb = new StringBuilder ();
            using (var tw = new StringWriter (sb))
                s.Serialize (tw, instance);

            return sb.ToString ();
        }
    }
}

