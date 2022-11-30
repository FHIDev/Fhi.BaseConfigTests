# Usage of BaseConfigTests

This is a base class for use when testing configurations, or when your tests needs to load configurations.
Your test class should derive from the baseclass.
By default it knows about the `appsettings.json`, the other sub-appsettings, like `appsettings.development.json` or whatever you prefer to call them, you need to set in your derived class.
The baseclass will load the settings as specified, and you can extract the section you need using a built-in utility method.

It depends on NUnit, which is used underneath.

## Usage

If you want to test your development settings, and combine them with the parent appsettings.json, the code will be something like the below:

```csharp

using Fhi.TestUtilities;
using NUnit.Framework;

public class MyTest : BaseConfigTests
{
    public MyTest() : base("appsettings.development.json")
    {
    }

    [Test]
    public void Test()
    {
        var section = GetConfiguration<MySection>(nameof(MySection));
        Assert.That(section, Is.Not.Null);
        Assert.That(section.MyProperty, Is.EqualTo("MyValue"));
    }
}

```

If you however want to test just your appsettings file, which is the default, you can do just:

```csharp

using Fhi.TestUtilities;
using NUnit.Framework;

public class MyTest : BaseConfigTests
{
    [Test]
    public void Test()
    {
        var section = GetConfiguration<MySection>(nameof(MySection));
        Assert.That(section, Is.Not.Null);
        Assert.That(section.MyProperty, Is.EqualTo("MyValue"));
    }
}

```

And if you want to test just a specific appsettings file without mixing in the base appsettings.json, then:

```csharp

using Fhi.TestUtilities;
using NUnit.Framework;

public class MyTest : BaseConfigTests
{
    public MyTest() 
    {
       AppSettings = "appsettings.development.json";
    }

    [Test]
    public void Test()
    {
        var section = GetConfiguration<MySection>(nameof(MySection));
        Assert.That(section, Is.Not.Null);
        Assert.That(section.MyProperty, Is.EqualTo("MyValue"));
    }
}

```

## Details

The baseclass will run a OneTimeSetup.  Anything you do in the constructor will be done before the OneTimeSetup of the baseclass is run. The OneTimeSetup loads the specified appsettings files.

If some of the specified appsettings don't exist at the binary output location, then the baseclass will assert with an error message about what file is not found and its full path.

