using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NewLobbyManager : MonoBehaviour
{
		public InputField inputFieldIP;
		public InputField inputFieldPORT;

	
		public InputField inputFieldIPConnect;
		public InputField inputFieldPORTConnect;


		public void QuitGame ()
		{
				Application.Quit ();
		}

		public void CreateServer ()
		{
				int listenPort;
				if (!int.TryParse (inputFieldPORT.text, out listenPort)) {
						listenPort = 25000;
				}
				var error = Network.InitializeServer (100, listenPort, false);
				//var error = Network.InitializeServer(100, 25000, !Network.HavePublicAddress());
				Debug.Log ("NetworkConnectionError: " + error);

				if (Network.peerType != NetworkPeerType.Disconnected) {
						if (Network.peerType == NetworkPeerType.Server) {
								Debug.Log ("Server On. IP: " + Network.player.ipAddress);
								LevelLoading ();
						} else {
								Debug.Log ("Tentando Inicializar um Server, mas esse PEER ja eh um Client Conectado.");
						}
				}
		}

		public void ConnectToServer ()
		{
				var ip = string.IsNullOrEmpty (inputFieldIPConnect.text) ? "127.0.0.1" : inputFieldIPConnect.text;
				int remotePort;
				if (!int.TryParse (inputFieldPORTConnect.text, out remotePort)) {
						remotePort = 25000;
				}

				Debug.Log (string.Format ("IP: {0} - Porta: {1}", ip, remotePort));

				var error = Network.Connect (ip, remotePort);

				//var error = Network.Connect("127.0.0.1", 25000);
				Debug.Log ("NetworkConnectionError: " + error);
		}

		void OnServerInitialized ()
		{
				Debug.Log ("OnServerInitialized");
		}

		void OnFailedToConnect ()
		{
				Debug.Log ("Client FAILED TO CONNECT.");
		}

		void OnConnectedToServer ()
		{
				if (Network.peerType == NetworkPeerType.Client) {
						Debug.Log ("Client On.");
						LevelLoading ();
				} else {
						Debug.Log ("Tentando Conectar a um Server, mas esse PEER ja eh um Server.");
				}
		}

		private void LevelLoading ()
		{
				PlayerInfo.IP = Network.player.ipAddress;
				if (Network.peerType == NetworkPeerType.Server) {
						Network.RemoveRPCsInGroup (0);
						NetworkLevelLoading.Instance.networkView.RPC ("LoadLevel", RPCMode.AllBuffered, "game", NetworkLevelLoading.Instance.lastLevelPrefix + 1);
				}
		}
}
