using System.Net;

namespace LogFilter.Helpers;

internal static class IPAddressExtensions
{
    public static bool IsInSameSubnet(this IPAddress address, IPAddress otherAddress, int? subnet)
    {
        if (!subnet.HasValue)
        {
            subnet = address.GetAddressBytes().Length * 8;
        }

        var network = address.GetNetworkAddress(subnet.Value);
        var otherNetwork = otherAddress.GetNetworkAddress(subnet.Value);

        return network.Equals(otherNetwork);
    }

    private static IPAddress GetNetworkAddress(this IPAddress address, int subnet)
    {
        var ipBytes = address.GetAddressBytes();
        var subnetBytes = new byte[ipBytes.Length];

        for (int i = 0; i < subnet / 8; i++)
        {
            subnetBytes[i] = 255;
        }

        if (subnet % 8 > 0)
        {
            subnetBytes[subnet / 8] = (byte)(256 - (1 << (8 - subnet % 8)));
        }

        byte[] networkBytes = new byte[ipBytes.Length];

        for (int i = 0; i < ipBytes.Length; i++)
        {
            networkBytes[i] = (byte)(ipBytes[i] & subnetBytes[i]);
        }

        return new IPAddress(networkBytes);
    }
}