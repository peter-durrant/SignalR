# SignalR Client Server Concepts

This code uses SignalR 2.2.0. See the [SignalR documentation](http://www.asp.net/signalr) for up-to-date information.

## Server

## Hubs

### Startup

The WebApp.Start method assumes a Startup class is available to map the hubs so <Startup> is not required.

```c#
_server = WebApp.Start(_uri.AbsoluteUri);
```

It can be supplied explicitly meaning that it could be named differently.

```c#
_server = WebApp.Start<Startup>(_uri.AbsoluteUri);
```

If the server is disposed, the resolver is disposed too. Therefore a new resolver needs to be created on startup and assigned to the GlobalHost.DependencyResolver

```c#
public void Configuration(IAppBuilder app)
{
    GlobalHost.DependencyResolver = new DefaultDependencyResolver();
    var config = new HubConfiguration { Resolver = GlobalHost.DependencyResolver };
    app.MapSignalR(config);
}
```

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

By providing application context to the hub, the hub can access application-wide state in order to determine the correct behaviour (if required).

### Authentication

Authentication is handled server-side. When establishing a connection, it is beneficial to complete authentication/handshake activities when establishing the connection. This enables the client connection to be closed during negotiation if it does not authenticate successfully. Without this step, the server will be required to send additional messaging to the client to instruct the client to close the connection. Currently, the SignalR library does not provide functionality for servers to disconnect clients.

If the server needs to disconnect the client and provide a readon, then an implementator can move authentication to a dedicated message and await a response from the server confirming successful negotiation.

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

## Client

The HDD.SignalR.Client project includes:
* Synchronous client - Client (implements IClient)
* Asynchronous client - AsyncClient (implenents IAsyncClient)

### Synchronous

A synchronous client will block it's thread until an action completes. Networking introduces latency, so it is recommended to use an asynchronous client if blocking will prevent an application responding, such as when using a UI thread.

On attempting to establish a connection, the thread will block on the Wait() call.

```c#
public bool Connect()
{
    _connection.Start().Wait();
    return _connection.State == ConnectionState.Connected;
}
```

The Connect function returns the state of the connection once the connection attempt has completed.

### Asynchronous

The use of Wait() is dropped in favour of await.

```c#
public async Task<bool> Connect()
{
    await _connection.Start();
    return _connection != null && _connection.State == ConnectionState.Connected;
}
```

The Connect function returns the state of the connection once the connection attempt has completed using the Task template.

The calling code will asynchronously await the connection attempt to complete.

```c#
var client = new Client.AsyncClient(uri);
var connected = await client.Connect();
```

# Testing

The testing project HDD.SignalR.Test contains unit tests for the application parts. Unit test are named COMPONENT\_ACTIVITY\_EXPECTEDRESULT.

For example, the unit test Client\_ConnectMultipleClientsToServer\_ConnectionsSucceed exercises the Client component, attempts to connect multiple clients to the server, and expects all of the connections to succeed.

## NSubstitute

[NSusbstitute](http://nsubstitute.github.io/) is used for various reasons such as to mock objects from their interface.