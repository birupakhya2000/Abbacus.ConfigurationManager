# ConfigurationManager

A .NET Core library for managing configuration settings across multiple environments with support for JSON files, environment variables, and secure secret management.

## Features

- **Multiple Configuration Sources**: Load configurations from JSON files, environment variables, and more.
- **Environment-Specific Configuration**: Automatically load environment-specific configurations (e.g., `appsettings.Development.json`, `appsettings.Production.json`).
- **Secret Management**: Securely manage sensitive data like API keys and connection strings.
- **Validation**: Validate configuration settings at startup to ensure all required settings are present and valid.
- **Caching**: Cache configuration settings to improve performance.

## Installation

You can install the package via NuGet:

```bash
dotnet add package Abbacus.ConfigurationManager
```

Or by using the NuGet Package Manager in Visual Studio.

## Usage

### Basic Usage

1. **Add Configuration Files**:
   - Add `appsettings.json` and environment-specific files like `appsettings.Development.json` to your project.

   Example `appsettings.json`:
   ```json
   {
     "AppSettings": {
       "Setting1": "Value1",
       "Setting2": "Value2"
     }
   }
   ```

2. **Initialize ConfigurationManager**:
   ```csharp
   using ConfigurationManager;

   var configManager = new ConfigurationManager();
   ```

3. **Retrieve Configuration Settings**:
   ```csharp
   var setting1 = configManager.GetSetting("AppSettings:Setting1");
   var setting2 = configManager.GetSetting<int>("AppSettings:Setting2");
   ```

4. **Validate Configuration Settings**:
   ```csharp
   configManager.ValidateSettings();
   ```

### Advanced Usage

#### Environment-Specific Configuration

The library automatically loads environment-specific configurations based on the `ASPNETCORE_ENVIRONMENT` environment variable.

Example `appsettings.Development.json`:
```json
{
  "AppSettings": {
    "Setting1": "DevelopmentValue1"
  }
}
```

#### Secret Management

You can integrate with Azure Key Vault or other secret management tools to securely retrieve sensitive data.

Example:
```csharp
var secretValue = configManager.GetSetting("KeyVault:SecretName");
```

#### Caching

The library caches configuration settings to improve performance. You can clear the cache if needed.

Example:
```csharp
configManager.ClearCache();
```

## Contributing

We welcome contributions! Please read our [Contributing Guidelines](CONTRIBUTING.md) for more information on how to get started.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Support

If you encounter any issues or have any questions, please file an issue on our [GitHub Issues](https://github.com/birupakhya2000/ConfigurationManager/issues) page.

## Acknowledgments

- Thanks to the .NET Core team for providing a robust framework.
- Inspiration from various open-source configuration management libraries.

## Changelog

### 1.0.0
- Initial release with support for JSON files, environment variables, and basic validation.

### 1.0.1
- Added support for Azure Key Vault integration.
- Improved caching mechanism.

### 1.0.2
- Improve some minor bugs.
- Improved caching mechanism.
