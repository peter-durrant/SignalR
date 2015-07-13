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

When establishing a connection, it is beneficial to complete authentication/handshake activities when establishing the connection. This enables the client connection to close. Without this step, the server will be required to send additional messaging to the client to instruct the client to close the connection. Currently, the SignalR library does not provide functionality for servers to disconnect clients.

#### Authentication Attribute
