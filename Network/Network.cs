using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Network
{
    public class Network
    {
        public static string GuessOwnIP()
        {

            string currentIP = "";

            NetworkInterface[] ni = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface n in ni)
            {
                if (/*(n.Name == nameNetworkInterface) &&*/
                   (n.NetworkInterfaceType == NetworkInterfaceType.Ethernet) &&
                   (n.OperationalStatus == OperationalStatus.Up))
                {
                    foreach (UnicastIPAddressInformation uai in n.GetIPProperties().UnicastAddresses)
                    {
                        if (uai.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {

                            currentIP = uai.Address.ToString();
                        }
                    }
                }
            }
            return currentIP;

        }


    }
}
