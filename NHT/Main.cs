using System.CommandLine;
using System.Net.Sockets;

RootCommand Root = new RootCommand(description: "This is an Tool to help at troubleshooting Network");

Command PortTest = new Command(name: "-P", description: "Test if an Port is reachable");
PortTest.AddAlias("--PortTest");

Option<string> PortTestIP = new Option<string>(name: "--IP", description: "The IP to test")
{
    IsRequired = true
};
Option<int> PortTestPort = new Option<int>(name: "--Port", description: "The Port to test")
{
    IsRequired = true
};

PortTest.AddOption(PortTestIP);
PortTest.AddOption(PortTestPort);

PortTest.SetHandler(async (PortTestIPVar, PortTestPortVar) =>
{
    TcpClient Client = new TcpClient();
    try
    {
        await Client.ConnectAsync(PortTestIPVar, PortTestPortVar);
    }
    catch (Exception Error)
    {

    }
}, PortTestIP, PortTestPort);

Root.AddCommand(PortTest);


await Root.InvokeAsync(args);