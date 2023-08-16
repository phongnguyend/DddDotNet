using System.Runtime.InteropServices;

namespace DddDotNet.Infrastructure.Storages.WindowsNetworkShare;

[StructLayout(LayoutKind.Sequential)]
public class Win32NetResource
{
    public Win32ResourceScope Scope { get; set; }
    public Win32ResourceType ResourceType { get; set; }
    public ResourceDisplaytype DisplayType { get; set; }
    public int Usage { get; set; }
    public string LocalName { get; set; }
    public string RemoteName { get; set; }
    public string Comment { get; set; }
    public string Provider { get; set; }
}

public enum Win32ResourceScope : int
{
    Connected = 1,
    GlobalNetwork = 2,
    Remembered = 3,
    Recent = 4,
    Context = 5,
}

public enum Win32ResourceType : int
{
    Any = 0,
    Disk = 1,
    Print = 2,
    Reserved = 8,
}

public enum ResourceDisplaytype : int
{
    Generic = 0x0,
    Domain = 0x01,
    Server = 0x02,
    Share = 0x03,
    File = 0x04,
    Group = 0x05,
    Network = 0x06,
    Root = 0x07,
    Shareadmin = 0x08,
    Directory = 0x09,
    Tree = 0x0a,
    Ndscontainer = 0x0b,
}
