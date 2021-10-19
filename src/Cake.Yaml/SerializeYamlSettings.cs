namespace Cake.Yaml
{
    /// <summary>
    /// Contains settings for YAML serialization using <see cref="YamlAliases"/>.
    /// </summary>
    public sealed class SerializeYamlSettings : YamlSettings
    {
        /// <summary>
        /// The default <see cref="SerializeYamlSettings"/>.
        /// </summary>
        public static SerializeYamlSettings Default { get; set; } = new SerializeYamlSettings();
    }
}
