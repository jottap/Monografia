using UnityEngine;
using System.Collections;

public class Magia : MonoBehaviour
{
		//[HideInInspector]
		public Player myPlayer;
		public int power = 100;

		void OnTriggerEnter2D (Collider2D other)
		{
				Debug.Log ("ENTROU");
				if (other.GetComponent<Player> ()) {
						var playerDamaged = other.GetComponent<Player> ();
						
						Debug.Log ("ACERTOU");
						if (this.networkView.isMine && !playerDamaged.networkView.isMine) {
								networkView.RPC ("SetDamage", playerDamaged.networkView.owner, playerDamaged.networkView.viewID);
								//	playerDamaged.SetDamage (power);
								Debug.Log ("Damage [[" + playerDamaged.playerName.text + "]]");
								this.networkView.RPC ("DestroyMe", RPCMode.AllBuffered); 
								
						}
				} else {
						Debug.Log ("Obj");
						this.networkView.RPC ("DestroyMe", RPCMode.AllBuffered); 
				}

		}

		[RPC]
		public void DestroyMe ()
		{
				Debug.Log ("DestroyMe");
				Destroy (this.gameObject);
		}

		[RPC]
		public void SetDamage (NetworkViewID viewIdAux)
		{
				Player[] players = FindObjectsOfType (typeof(Player)) as Player[];
				foreach (var item in players) {
						if (item.networkView.viewID == viewIdAux) {
								Player playerAux = item.GetComponent<Player> ();
								playerAux.SetDamage (power);
						}
				}
		}
}
