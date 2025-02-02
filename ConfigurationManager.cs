using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Abbacus.ConfigurationManager
{
    public class ConfigurationManager
    {
        private readonly IConfiguration _configuration;
        private readonly Dictionary<string, object> _cache = new Dictionary<string, object>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationManager"/> class.
        /// </summary>
        public ConfigurationManager()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

            _configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }

        /// <summary>
        /// Gets a configuration value as a string.
        /// </summary>
        /// <param name="key">The configuration key.</param>
        /// <returns>The configuration value as a string.</returns>
        public string GetSetting(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key), "Configuration key cannot be null or empty.");

            if (_cache.TryGetValue(key, out var cachedValue))
            {
                return cachedValue as string;
            }

            var value = _configuration[key];
            _cache[key] = value;
            return value;
        }

        /// <summary>
        /// Gets a configuration value as a strongly-typed object.
        /// </summary>
        /// <typeparam name="T">The type of the configuration value.</typeparam>
        /// <param name="key">The configuration key.</param>
        /// <returns>The configuration value as type T.</returns>
        public T GetSetting<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key), "Configuration key cannot be null or empty.");

            if (_cache.TryGetValue(key, out var cachedValue))
            {
                return (T)cachedValue;
            }

            var value = _configuration.GetValue<T>(key);
            _cache[key] = value;
            return value;
        }

        /// <summary>
        /// Validates that all required settings are present.
        /// </summary>
        /// <param name="requiredSettings">A list of required configuration keys.</param>
        /// <exception cref="InvalidOperationException">Thrown if a required setting is missing.</exception>
        public void ValidateSettings(params string[] requiredSettings)
        {
            if (requiredSettings == null || requiredSettings.Length == 0)
                throw new ArgumentNullException(nameof(requiredSettings), "At least one required setting must be provided.");

            foreach (var key in requiredSettings)
            {
                if (string.IsNullOrEmpty(GetSetting(key)))
                {
                    throw new InvalidOperationException($"Required setting '{key}' is missing in configuration.");
                }
            }
        }
    }
}