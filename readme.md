# SignalR Client Server Concepts

## Server

## Hubs

### Methods

#### Server to Client Messaging

Hubs can define methods that attached clients can call. If a hub only needs to send messages to clients, then the hub does not need to implement any functionality. The client(s) can be called from the server.

```c#
public void SendMessage(string message)
{
    GlobalHost.ConnectionManager.GetHubContext<MessageHub>().Clients.All.SendMessage(message);
}
```

The SendMessage function will not be defined on MessageHub.

#### Client to Server Messaging

The hub must implement methods that clients call.

### Overriding Hub Names

Hub names are derived from the class name

```c#
public class MyHubName : Hub
```
To reference this hub name using Javascript use

```javascript
$.connection.myHubName
```

Override the hub name

```c#
[HubName("ThisHubNameIsUsed")]
public class MyHubName : Hub
```

To reference this hub name using Javascript use

```javascript
$.connection.ThisHubNameIsUsed
```

### Application Context - Server

The lifetime of hubs is managed by SignalR. The initialisation of hubs is not handled explictly in your own code.

In order for hubs to interact with the host application, the context of the application can be passed to the hubs as a static property.

```c#
class MessageHub : Hub
{
    private static IApplicationContext _context;

    public static void SetContext(IApplicationContext context)
    {
        _context = context;
    }
}
```

### Authentication

Authentication is handled server-side. When establishing a connection, it is beneficial to complete authentication/handshake activities when establishing the connection. This enables the client connection to be closed during negotiation if it does not authenticate successfully. Without this step, the server will be required to send additional messaging to the client to instruct the client to close the connection. Currently, the SignalR library does not provide functionality for servers to disconnect clients.

#### Authentication Attribute

Create a custom authorization attribute (implementing AuthorizeAttribute). Override AuthorizeHubConnection to return true if authorised, otherwise return false. If the function returns false, the connection will be terminated.

```c#
[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
class AuthorizeConnection : AuthorizeAttribute
{
    public override bool AuthorizeHubConnection(HubDescriptor hubDescriptor, IRequest request)
    {
    	var authorized = false;
    	
    	// Can the connection be authorised?
    	// Add logic to determine whether authorized should be set to true
    	
    	return authorized;
    }
}
```

Decorate the hub(s) with the AuthorizeConnection attribute to enable authorisation using this mechanism.

```c#
[AuthorizeConnection]
public class MessageHub : Hub
{
```

By providing application context to the hub, the hub can access application-wide state in order to determine the correct behaviour (if required).

# Testing

The testing project HDD.SignalR.Test contains unit tests for the application parts. Unit test are named COMPONENT\_ACTIVITY\_EXPECTEDRESULT.

## NSubstitute

[NSusbstitute](http://nsubstitute.github.io/) is used for various reasons such as to mock objects from their interface.