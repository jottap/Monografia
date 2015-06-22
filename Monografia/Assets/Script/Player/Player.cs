using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
		public float velocidade;
		private int vida = 1000;	
		public Image hudVida;	
		public Image hudMagia;
		public GameObject pauseMenu;
		public Text vidaHud;

		public Animator animator;	

		public Text playerName;

		void Start ()
		{
				velocidade /= 50;
				vida = 1000;
				if (networkView.isMine) {
						SetPlayerName (PlayerInfo.Nome);
				}
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (!this.networkView.isMine) {
						return;
				}
		
				if (Input.GetKeyDown (KeyCode.Escape)) {
						Debug.Log ("XXX---[" + pauseMenu.activeSelf + "]--- ");
						pauseMenu.SetActive (pauseMenu.activeSelf == true ? false : true);
				}
				if (pauseMenu.activeSelf == false) {
						Comandos ();
				}
		}

		void Comandos ()
		{
				var h = Input.GetAxis ("Horizontal");
				var v = Input.GetAxis ("Vertical");
				var direcao = new Vector2 (h, v);
				
				transform.Translate (direcao * velocidade);
		
				networkView.RPC ("animatorValue", RPCMode.AllBuffered, "speed_X", h);
				networkView.RPC ("animatorValue", RPCMode.AllBuffered, "speed_Y", v);

		}

		[RPC]
		void animatorValue (string parametro, float value)
		{
				animator.SetFloat (parametro, value);
		}


		void OnNetworkLoadedLevel ()
		{
				if (networkView.isMine) {
						SetPlayerName (PlayerInfo.Nome);
				}
		}

		[RPC]
		private void SetPlayerName (string nome)
		{
				playerName.text = nome;
				Debug.Log ("(( " + PlayerInfo.Nome + " )) -- " + nome);
				if (networkView.isMine) {
						networkView.RPC ("SetPlayerName", RPCMode.OthersBuffered, nome);
				}
		}

		void OnSerializeNetworkView (BitStream stream, NetworkMessageInfo info)
		{
				Vector3 syncPosition = Vector3.zero;
				if (stream.isWriting) {
						syncPosition = this.transform.position;
						stream.Serialize (ref syncPosition);
				} else {
						stream.Serialize (ref syncPosition);
						this.transform.position = syncPosition;
				}
		}

		public void SetDamage (int damage)
		{
			
				Debug.Log ("[[" + playerName.text + "]]" + "XXXXXXX");	
				
				float amount = ((vida -= damage) * 100) / 1000; 
				Debug.Log ("222[" + PlayerInfo.Nome + "]VIDA[" + vida + "]");
				SetWidth (amount);
				if (vida <= 0) {
						Network.Destroy (this.gameObject);
						pauseMenu.SetActive (true);
				}
		
		}
	
		public void SetWidth (float amount)
		{
				Debug.Log ("amount[" + amount / 100 + "]");
				hudVida.fillAmount = amount / 100;
		}
	
		void OnDisconnectedFromServer ()
		{	
				Destroy (this.gameObject);
				Application.LoadLevel ("menu");
		}

}
