using System.CommandLine;
using System.Net.Sockets;

RootCommand Root = new RootCommand(description: "This is an Tool to help at troubleshooting Network");

Command PortTest = new Command(name: "-P", description: "Test if an Port is reachable");
PortTest.AddAlias("--PortTest");

//-P --IP 172.67.74.183 --Port 80

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
    bool Worked = true;
    try
    { 
        await Client.ConnectAsync(PortTestIPVar, PortTestPortVar,);
    }
    catch (Exception Error)
    {
        Worked = false;
    }

    if (Worked)
    {
        await Console.Out.WriteLineAsync($"Port {PortTestIPVar}:{PortTestPort} is reachable");
    }
    else
    {
        await Console.Out.WriteLineAsync($"Port {PortTestIPVar}:{PortTestPort} is not reachable");

    }
    await Console.Out.WriteLineAsync();

}, PortTestIP, PortTestPort);

Root.AddCommand(PortTest);


await Root.InvokeAsync(args);