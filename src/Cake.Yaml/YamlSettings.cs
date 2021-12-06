using YamlDotNet.Serialization;

namespace Cake.Yaml
{
    /// <summary>
    /// Base class for settings for YAML serialization and deserialization using <see cref="YamlAliases"/>.
    /// </summary>
    public abstract class YamlSettings
    {
        /// <summary>
        /// The <see cref="INamingConvention"/> that will be used by the (de)serializer.
        /// </summary>
        public INamingConvention NamingConvention { get; set; }
    }
}
