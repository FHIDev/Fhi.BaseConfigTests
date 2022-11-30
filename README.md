# Usage of BaseConfigTests

This is a base class for use when testing configurations, or when your tests needs to load configurations.
Your test class should derive from the baseclass.
By default it knows about the `appsettings.json`, the other sub-appsettings, like `appsettings.development.json` or whatever you prefer to call them, you need to set in your derived class.
The baseclass will load the settings as specified, and you can extract the section you need using a built-in utility method.

It depends on NUnit, which is used underneath.

## Usage

```csharp

public class MyTest : BaseConfigTests
{
    public MyTest() : base("appsettings.development.json")
    {
        AppSettings
        
    }

    [Test]
    public void Test()
    {
        var section = GetSection<MySection>("MySection");
        Assert.That(section, Is.Not.Null);
        Assert.That(section.MyProperty, Is.EqualTo("MyValue"));
    }
}

```

```