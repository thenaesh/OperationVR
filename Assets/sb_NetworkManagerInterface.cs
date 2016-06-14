#if ENABLE_UNET
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using System.Collections;

namespace UnityEngine.Networking
{
	[AddComponentMenu("Network/NetworkManagerHUD")]
	[RequireComponent(typeof(NetworkManager))]
	[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
	public class sb_NetworkManagerInterface : MonoBehaviour {
		private NetworkManager manager;

		public Text inputfield;
		public Text outText;

		void Awake()
		{
			manager = GetComponent<NetworkManager>();
		}

		void Update()
		{
			outText.text = "My IP Address: " +  LocalIPAddress();
		}

		public void ConnectButtonPress() {
			Debug.Log (inputfield.text);
			manager.networkAddress = inputfield.text;
			manager.StartClient();
		}


		public void HostButtonPress() {
			manager.StartHost();
		}
	
	
		public string LocalIPAddress()
		{
			IPHostEntry host;
			string localIP = "";
			host = Dns.GetHostEntry(Dns.GetHostName());
			foreach (IPAddress ip in host.AddressList)
			{
				if (ip.AddressFamily == AddressFamily.InterNetwork)
				{
					localIP = ip.ToString();
					break;
				}
			}
			return localIP;
		}
	}
		
}

#endif //ENABLE_UNET