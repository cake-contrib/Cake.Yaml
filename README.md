# Cake.Yaml
A set of aliases for http://cakebuild.net to help with YAML Serialization.

![AppVeyor](https://ci.appveyor.com/api/projects/status/github/redth/Cake.Yaml)

You can easily reference Cake.Yaml directly in your build script via a cake addin:

```csharp
#addin nuget:?package=Cake.Yaml
#addin nuget:?package=YamlDotNet&version=5.2.1
```

NOTE: It's very important at this point in time to specify the `YamlDotNet` package *and* the version _5.2.1_ for it.

## Aliases

Please visit the Cake Documentation for a list of available aliases:

[http://cakebuild.net/dsl/yaml](http://cakebuild.net/dsl/yaml)

## Apache License 2.0
Apache Cake.Yaml Copyright 2015. The Apache Software Foundation This product includes software developed at The Apache Software Foundation (http://www.apache.org/).