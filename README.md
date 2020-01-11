# UdpClientServer

[![Build Status](https://github.com/davewalker5/UdpClientServer/workflows/.NET%20Core%20CI%20Build/badge.svg)](https://github.com/davewalker5/UdpClientServer/actions)
[![GitHub issues](https://img.shields.io/github/issues/davewalker5/UdpClientServer)](https://github.com/davewalker5/UdpClientServer/issues)
[![Releases](https://img.shields.io/github/v/release/davewalker5/UdpClientServer.svg?include_prereleases)](https://github.com/davewalker5/UdpClientServer/releases)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://github.com/davewalker5/UdpClientServer/blob/master/LICENSE)
[![Language](https://img.shields.io/badge/language-c%23-blue.svg)](https://github.com/davewalker5/UdpClientServer/)
[![GitHub code size in bytes](https://img.shields.io/github/languages/code-size/davewalker5/UdpClientServer)](https://github.com/davewalker5/UdpClientServer/)

UdpClientServer is a demonstration of UDP connectivity using the .NET UdpCient. It demonstrates the following:

| Mode | Description |
| --- | --- |
| client | Receives input from the user and sends it to the server |
| server | Receives and acknowledges data from the client |
| sender | Broadcasts data at regular intervals |
| listener | Listens for and reports data broadcast by tbe sender |

It also demonstrates client/listener and server/sender combinations.

## Getting Started

### Pre-Requisites

You will need .NET Core 3.0 and the .NET CLI installed. Instructions for downloading and installing are on the .NET website:

[https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)

### Building the Project

To build the project using the .NET CLI, open a terminal window, change to the "src" folder of your working copy and run the following:

```
dotnet build UdpClientServer.sln
```

## Usage

### Running a Client/Server Pair

Open two terminal windows. In the first, change to the folder containing the compiled output and enter the following:

```
UdpClientServer server
```

In the second, change to the folder containing the compiled output and enter the following:

```
UdpClientServer client
```

You will be prompted for some data. Enter some text to send it to the server and you will see it echoed to the terminal along with the response from the server on receipt of that  data:

```
Data or [ENTER] to quit : hello
3786 : Sent 5 bytes : hello
3786 : Read 2 bytes : ok
Data or [ENTER] to quit : goodbye
3786 : Sent 7 bytes : goodbye
3786 : Read 2 bytes : ok
Data or [ENTER] to quit : 
```

As each line of text is received, the server will respond by echoing the text sending an "ok" response back to the client, that you can see echoed in the:

```
3690 : Read 5 bytes : hello
3690 : Read 7 bytes : goodbye
```

When you're finished, enter "stop" in the client  to stop both the client and server.

### Running a Sender/Listener Pair

Open two terminal windows. In the first, change to the folder containing the compiled output and enter the following:

```
UdpClientServer sender
```

The sender will immediately start broadcasting data:

```
4000 : Sent 19 bytes : 11/01/2020 04:30:09
4000 : Sent 19 bytes : 11/01/2020 04:30:11
4000 : Sent 19 bytes : 11/01/2020 04:30:12
4000 : Sent 19 bytes : 11/01/2020 04:30:13
4000 : Sent 19 bytes : 11/01/2020 04:30:14
```

In the second, change to the folder containing the compiled output and enter the following:

```
UdpClientServer listener
```

The listener will immediately start listening for and echoing data from the sender:

```
4073 : Sent 19 bytes : 11/01/2020 04:30:59
4073 : Sent 19 bytes : 11/01/2020 04:31:00
4073 : Sent 19 bytes : 11/01/2020 04:31:01
4073 : Sent 19 bytes : 11/01/2020 04:31:02
4073 : Sent 19 bytes : 11/01/2020 04:31:03
```

When you're finished, close the two windows or CTRL-C out of the running applications in each one.

### Running Combined Server-Sender and Client-Listener

Open two terminal windows. In the first, change to the folder containing the compiled output and enter the following:

```
UdpClientServer server-sender
```

This starts a sender on a background thread and a server, listening for and responding to input, on the foreground thread.

In the second, change to the folder containing the compiled output and enter the following:

```
UdpClientServer client-listener
```

This starts a listener on a background thread and a client, receiving input and sending it to the server, on the foreground thread. 

## Authors

- **Dave Walker** - *Initial work* - [LinkedIn](https://www.linkedin.com/in/davewalker5/)

## Feedback

To file issues or suggestions, please use the [Issues](https://github.com/davewalker5/TelloCommander/issues) page for this project on GitHub.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
