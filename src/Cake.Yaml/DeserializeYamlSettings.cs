namespace Cake.Yaml
{
    /// <summary>
    /// Contains settings for YAML deserialization using <see cref="YamlAliases"/>.
    /// </summary>
    public sealed class DeserializeYamlSettings : YamlSettings
    {
        /// <summary>
        /// The default <see cref="DeserializeYamlSettings"/>.
        /// </summary>
        public static DeserializeYamlSettings Default { get; set; } = new DeserializeYamlSettings();
    }
}
