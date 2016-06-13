#if ENABLE_UNET

namespace UnityEngine.Networking
{
	[AddComponentMenu("Network/NetworkManagerHUD")]
	[RequireComponent(typeof(NetworkManager))]
	[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
	public class sb_NetworkManagerInterface : MonoBehaviour {
		public NetworkManager manager;

		void Awake()
		{
			manager = GetComponent<NetworkManager>();
		}

		public void ButtonPress() {
			manager.StartClient();
		}
	}
}

#endif //ENABLE_UNET