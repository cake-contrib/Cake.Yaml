# Cake.Yaml

A set of aliases for [cakebuild.net](https://cakebuild.net) to help with YAML Serialization.

![AppVeyor](https://ci.appveyor.com/api/projects/status/3ttdasxutw5r8v7d/branch/master?svg=true)
[![License](https://img.shields.io/:license-mit-blue.svg)](./LICENSE)

You can easily reference Cake.Yaml directly in your build script via a Cake addin:

```csharp
#addin nuget:?package=Cake.Yaml&version=4.0.0
#addin nuget:?package=YamlDotNet&version=12.3.1
```

NOTE: It's very important at this point in time to specify the `YamlDotNet` package *and* the version `12.3.1` for it.

## Aliases

Please visit the Cake Documentation for a list of available aliases:

[https://cakebuild.net/dsl/yaml](https://cakebuild.net/dsl/yaml)

## Naming conventions

Since version 5.0.0, Cake.Yaml supports naming conventions implemented in YamlDotNet. E.g.:

```csharp
var settings = new SerializeYamlSettings
{
    NamingConvention = CamelCaseNamingConvention.Instance,
};

var serialized = SerializeYaml(someObject, settings);
```

YamlDotNet [includes](https://github.com/aaubry/YamlDotNet/tree/master/YamlDotNet/Serialization/NamingConventions) a number of naming conventions out of the box:

| Naming convention class       | Description                                                                                                         |
| ----------------------------- | ------------------------------------------------------------------------------------------------------------------- |
| `CamelCaseNamingConvention`   | Convert the string with underscores (`this_is_a_test`) or hyphens (`this-is-a-test`) to camel case (`thisIsATest`)  |
| `HyphenatedNamingConvention`  | Convert the string from camelcase (`thisIsATest`) to a hyphenated (`this-is-a-test`) string                         |
| `LowerCaseNamingConvention`   | Convert the string with underscores (`this_is_a_test`) or hyphens (`this-is-a-test`) to lower case (`thisisatest`)  |
| `NullNamingConvention`        | Performs no naming conversion                                                                                       |
| `PascalCaseNamingConvention`  | Convert the string with underscores (`this_is_a_test`) or hyphens (`this-is-a-test`) to pascal case (`ThisIsATest`) |
| `UnderscoredNamingConvention` | Convert the string from camelcase (`thisIsATest`) to a underscored (`this_is_a_test`) string                        |

You can implement your own naming convention by creating a class that implements the `INamingConvention` interface.

## Discussion

For questions and to discuss ideas & feature requests, use the [GitHub discussions on the Cake GitHub repository](https://github.com/cake-build/cake/discussions), under the [Extension Q&A](https://github.com/cake-build/cake/discussions/categories/extension-q-a) category.

[![Join in the discussion on the Cake repository](https://img.shields.io/badge/GitHub-Discussions-green?logo=github)](https://github.com/cake-build/cake/discussions)
