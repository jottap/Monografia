using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GerenciadorGame : MonoBehaviour
{
		public GameObject painel;
		public GameObject Mage;
		public List<GameObject> listaRespawns;
		public Text ip_text;
		public Image hudVida;
		public Image hudMagia;
		public GameObject pauseMenu;

		// Use this for initialization
		void Start ()
		{
				if (Network.isClient || Network.isServer) {
						IniciarGame ();
				}

		}

		// Update is called once per frame
		void Update ()
		{
	
		}

		public void FecharTelaPause ()
		{
				painel.SetActive (false);
		}

		public void IniciarGame ()
		{
				ip_text.text = PlayerInfo.IP;
				GameObject aux = listaRespawns [0];
				Vector3 auxV = new Vector3 ();
				auxV = aux.transform.position;
				Debug.Log ("[[ " + auxV + "]]");

				var mago = Network.Instantiate (Mage, listaRespawns [0].transform.position, Quaternion.identity, 0) as GameObject;
				if (mago.networkView.isMine) {
						mago.GetComponent<Player> ().hudVida = hudVida;
						mago.GetComponent<Player> ().hudMagia = hudMagia;
						mago.GetComponent<Player> ().pauseMenu = pauseMenu;
						Camera.main.GetComponent<CameraFollow> ().playerTransform = mago.transform;
						PlayerInfo.viewID = mago.networkView.viewID;
				}
				
		}

		public void Respawn ()
		{
				Player[] players = FindObjectsOfType (typeof(Player)) as Player[];
				foreach (var item in players) {
						if (item.networkView.viewID == PlayerInfo.viewID) {
								return;
						}
				}

				var mago = Network.Instantiate (Mage, listaRespawns [0].transform.position, Quaternion.identity, 0) as GameObject;
				if (mago.networkView.isMine) {
						mago.GetComponent<Player> ().hudVida = hudVida;
						mago.GetComponent<Player> ().SetWidth (100);
						mago.GetComponent<Player> ().hudMagia = hudMagia;
						mago.GetComponent<Player> ().pauseMenu = pauseMenu;
						Camera.main.GetComponent<CameraFollow> ().playerTransform = mago.transform;
						PlayerInfo.viewID = mago.networkView.viewID;
				}
		}

}
