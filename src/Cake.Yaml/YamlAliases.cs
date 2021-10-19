using System;
using System.IO;
using System.Text;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Annotations;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace Cake.Yaml
{
    /// <summary>
    /// <para>YAML related cake aliases.</para>
    /// <para>
    ///  In order to use aliases from this addin, you will need to also reference YamlDotNet as an addin.
    ///  Here is what including Cake.Yaml in your script should look like:
    /// <code>
    /// #addin nuget:?package=Cake.Yaml&amp;version=x.y.z
    /// #addin nuget:?package=YamlDotNet&amp;version=11.2.1
    /// </code>
    /// </para>
    /// </summary>
    [CakeAliasCategory("Yaml")]
    [CakeNamespaceImport("Cake.Yaml")]
    [CakeNamespaceImport("YamlDotNet.Serialization.NamingConventions")]
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
        public static T DeserializeYamlFromFile<T>(this ICakeContext context, FilePath filename)
        {
            return DeserializeYamlFromFile<T>(context, filename, DeserializeYamlSettings.Default);
        }

        /// <summary>
        /// Deserializes the YAML from a file.
        /// </summary>
        /// <returns>The Deserialized Object.</returns>
        /// <param name="context">The context.</param>
        /// <param name="filename">The YAML filename.</param>
        /// <param name="settings">The <see cref="DeserializeYamlSettings"/> that will be used to build the deserializer.</param>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        [CakeMethodAlias]
        public static T DeserializeYamlFromFile<T>(this ICakeContext context, FilePath filename, DeserializeYamlSettings settings)
        {
            T result = default(T);

            var d = BuildDeserializer(settings);

            using (var tr = File.OpenText(filename.MakeAbsolute(context.Environment).FullPath))
            {
                var reader = new MergingParser(new Parser(tr));
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
        public static T DeserializeYaml<T>(this ICakeContext context, string yaml)
        {
            return DeserializeYaml<T>(context, yaml, DeserializeYamlSettings.Default);
        }

        /// <summary>
        /// Deserializes the YAML from a string.
        /// </summary>
        /// <returns>The Deserialized Object.</returns>
        /// <param name="context">The context.</param>
        /// <param name="yaml">The YAML string.</param>
        /// <param name="settings">The <see cref="DeserializeYamlSettings"/> that will be used to build the deserializer.</param>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        [CakeMethodAlias]
        public static T DeserializeYaml<T>(this ICakeContext context, string yaml, DeserializeYamlSettings settings)
        {
            T result = default(T);

            var d = BuildDeserializer(settings);
            using (var tr = new StringReader(yaml))
            {
                var reader = new MergingParser(new Parser(tr));
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
        public static void SerializeYamlToFile<T>(this ICakeContext context, FilePath filename, T instance)
        {
            SerializeYamlToFile(context, filename, instance, SerializeYamlSettings.Default);
        }

        /// <summary>
        /// Serializes an object to a YAML file.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="filename">The filename to serialize to.</param>
        /// <param name="instance">The object to serialize.</param>
        /// <param name="settings">The <see cref="SerializeYamlSettings"/> that will be used to build the serializer.</param>
        /// <typeparam name="T">The type of object to serialize.</typeparam>
        [CakeMethodAlias]
        public static void SerializeYamlToFile<T>(this ICakeContext context, FilePath filename, T instance, SerializeYamlSettings settings)
        {
            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            var s = BuildSerializer(settings);

            using (var tw = new StreamWriter(File.Open(filename.MakeAbsolute(context.Environment).FullPath, FileMode.Create)))
            {
                s.Serialize(tw, instance);
            }
        }

        /// <summary>
        /// Serializes an object to a YAML string.
        /// </summary>
        /// <returns>The YAML string.</returns>
        /// <param name="context">The context.</param>
        /// <param name="instance">The object to serialize.</param>
        /// <typeparam name="T">The type of object to serialize.</typeparam>
        [CakeMethodAlias]
        public static string SerializeYaml<T>(this ICakeContext context, T instance)
        {
            return SerializeYaml(context, instance, SerializeYamlSettings.Default);
        }

        /// <summary>
        /// Serializes an object to a YAML string.
        /// </summary>
        /// <returns>The YAML string.</returns>
        /// <param name="context">The context.</param>
        /// <param name="instance">The object to serialize.</param>
        /// <param name="settings">The <see cref="SerializeYamlSettings"/> that will be used to build the serializer.</param>
        /// <typeparam name="T">The type of object to serialize.</typeparam>
        [CakeMethodAlias]
        public static string SerializeYaml<T>(this ICakeContext context, T instance, SerializeYamlSettings settings)
        {
            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            var s = BuildSerializer(settings);

            var sb = new StringBuilder();
            using (var tw = new StringWriter(sb))
            {
                s.Serialize(tw, instance);
            }

            return sb.ToString();
        }

        private static ISerializer BuildSerializer(SerializeYamlSettings settings)
        {
            var serializerBuilder = new SerializerBuilder();

            if (!(settings.NamingConvention is null))
            {
                serializerBuilder = serializerBuilder.WithNamingConvention(settings.NamingConvention);
            }

            var serializer = serializerBuilder.Build();
            return serializer;
        }

        private static IDeserializer BuildDeserializer(DeserializeYamlSettings settings)
        {
            var deserializerBuilder = new DeserializerBuilder();

            if (!(settings.NamingConvention is null))
            {
                deserializerBuilder = deserializerBuilder.WithNamingConvention(settings.NamingConvention);
            }

            var deserializer = deserializerBuilder.Build();
            return deserializer;
        }
    }
}
