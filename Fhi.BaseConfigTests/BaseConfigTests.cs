using NUnit.Framework;

namespace Fhi.BaseConfigTests;
using System.IO;

using Microsoft.Extensions.Configuration;


public abstract class BaseConfigTests
{
    protected IConfigurationRoot Config { get; private set; } = null!;

    /// <summary>
    /// Use to set primary appsettings file, default is appsettings.json
    /// </summary>
    protected string AppSettings { get; init; } = "appsettings.json";

    /// <summary>
    /// Use to set the secondary sub appsettings, like appsettings.test.json, default is empty
    /// </summary>
    protected string AppSettingsSub { get; init; } = "";

    protected BaseConfigTests()
    {
        
    }

    protected BaseConfigTests(string appSettingsSub)
    {
        AppSettingsSub = appSettingsSub;
    }


    [OneTimeSetUp]
    public void OneTimeInit()
    {
        Config = GetIConfigurationRoot(TestContext.CurrentContext.TestDirectory);
    }

    protected T GetConfiguration<T>(string sectionName) where T : class
    {
        return Config.GetSection(sectionName).Get<T>();
    }

    protected IConfigurationRoot GetIConfigurationRoot(string outputPath)
    {
        VerifyPath(AppSettings);
        var builder = new ConfigurationBuilder()
            .SetBasePath(outputPath)
            .AddJsonFile(AppSettings, optional: false);
        if (!string.IsNullOrEmpty(AppSettingsSub))
        {
            VerifyPath(AppSettingsSub);
            builder.AddJsonFile(AppSettingsSub, optional: false);
        }
        return builder.Build();

        void VerifyPath(string appsettings)
        {
            var path = Path.Combine(outputPath, appsettings);
            Assert.That(File.Exists(path), $"Finner ikke {path}");
        }
    }
}
