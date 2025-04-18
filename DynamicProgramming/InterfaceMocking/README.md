# Interface Mocking Exercise
This is a code walkthrough of how you would code something like .SetupGet().Return() from the Moq framework.
Main resource is Microsoft Copilot.

In the Moq framework, SetUpGet() is used to set up expectations for property getters on mocked itnerfaces or classes. 
It allows you to specify what a property's getter should return without relying on an actual implementation. This 
is incredibly useful for isolating units of code during testing. 

Here's how you might user it with Moq:
```csharp
var mock = new Mock<IMyInterface>();
mock.SetupGet(x => x.MyProperty).Returns("ExpectedValue");

// When you access the property, it returns "ExpectedValue"
string value = mock.Object.MyProperty;
// value == "ExpectedValue"
```

But how would you implement similar functionality without Moq? (In the pursuit of extending our knownledge)

## Creating Custom Mockign Behavior

To mimic SetupGet().Returns, you can create a dynamic proxy that intercepts property getter calls 
and returns predefined values. This involves:
1. Defining the Interface: Start with the interface you want to mock. 
```csharp
public interface IMyInterface
{
	string MyProperty { get; }
}
```
2. Using Dynamic Proxy Generation: Utilize a library like Castle DynamicProxy to create dynamic implementations of your interfaces.
```
Install-Package Castle.Core
```
3. Create an Interceptor: Implement an interceptor that overrides the property getter.



4. 

