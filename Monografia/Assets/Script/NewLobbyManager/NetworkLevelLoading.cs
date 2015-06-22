using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NetworkView))]
public class NetworkLevelLoading : MonoBehaviour
{
		public static NetworkLevelLoading Instance { get; set; }

		public string[] supportedNetworkLevels = new string[] { "mylevel" };
		public string disconnectedLevel = "loader";
		public int lastLevelPrefix = 0;

		void Awake ()
		{
				// allow only one instance of this Class
				if (Instance != null && Instance != this) {
						Destroy (this.gameObject);
						return;
				}

				Instance = this;
				// Network level loading is done in a separate channel.
				DontDestroyOnLoad (this.gameObject);

				networkView.group = 1;
				//Application.LoadLevel(disconnectedLevel);
		}
	
		[RPC]
		void LoadLevel (string level, int levelPrefix)
		{
				StartCoroutine (LoadLevel_Routine (level, levelPrefix));
		}

		private IEnumerator LoadLevel_Routine (string level, int levelPrefix)
		{
				lastLevelPrefix = levelPrefix;
		
				// There is no reason to send any more data over the network on the default channel,
				// because we are about to load the level, thus all those objects will get deleted anyway
				Network.SetSendingEnabled (0, false);    
		
				// We need to stop receiving because first the level must be loaded first.
				// Once the level is loaded, rpc's and other state update attached to objects in the level are allowed to fire
				Network.isMessageQueueRunning = false;
		
				// All network views loaded from a level will get a prefix into their NetworkViewID.
				// This will prevent old updates from clients leaking into a newly created scene.
				Network.SetLevelPrefix (levelPrefix);
				Application.LoadLevel (level);
				yield return null;
				yield return null;

				// Allow receiving data again
				Network.isMessageQueueRunning = true;
				// Now the level has been loaded and we can start sending out data to clients
				Network.SetSendingEnabled (0, true);
		
		
				foreach (var go in (GameObject[])FindObjectsOfType(typeof(GameObject))) {
						go.SendMessage ("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);
				}

				//yield return null;
		}

}