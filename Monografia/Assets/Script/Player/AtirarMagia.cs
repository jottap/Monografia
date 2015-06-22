using UnityEngine;
using System.Collections;

public class AtirarMagia : MonoBehaviour
{

		public Transform posicaoPersonagem;
		public GameObject magia;
		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (!this.networkView.isMine) {
						return;
				}

				/*
				if (Input.GetMouseButtonDown (0)) {
						Debug.Log ("TESTE CLICK");
						Vector3 mouseInScreenPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
						mouseInScreenPos.z = 0;
						Debug.Log ("TESTE PEGOU POS CLICK" + "MOUSE IN SCREEN : " + mouseInScreenPos);
						Vector3 myCharacterInScreenPos = Camera.main.WorldToScreenPoint (posicaoPersonagem.transform.position);
						Debug.Log ("TESTE PEGOU POS PLAYER");
						Debug.Log ("Input.mouse : " + Input.mousePosition);
						Debug.Log ("MyChar : " + myCharacterInScreenPos);
						var target = mouseInScreenPos - myCharacterInScreenPos;
						Debug.Log ("Target : " + target);
						var direcao = target.magnitude;
						Debug.Log ("Target magnetude : " + target.magnitude);
						
						miraMagia.transform.LookAt (mouseInScreenPos);
								
						Quaternion rotacao = miraMagia.transform.rotation;
						//rotacao.z = miraMagia.transform.rotation.z;
						GameObject auxMagia = Instantiate (magia, posicaoPersonagem.transform.position, rotacao) as GameObject;
						Physics2D.IgnoreLayerCollision (8, 9);			
						//auxMagia.rigidbody2D.velocity = new Vector2 (direcao, 0);
						//auxMagia.rigidbody2D.velocity = auxMagia.transform.right * 20;		

						//auxMagia.rigidbody2D.velocity = direcao;
						//rigidbody.velocity = direcao;
				}
		 */
				
				if (Input.GetMouseButtonDown (0)) {
						if (this.gameObject.GetComponent<Player> ().pauseMenu.activeSelf == true) {
								Debug.Log ("]]]] " + this.gameObject.GetComponent<Player> ().pauseMenu.activeSelf + " [[[[");
								return;
						}
						//Debug.Log(this.transform.forward);
						var mouseToWorldSpace = Camera.main.ScreenToWorldPoint (Input.mousePosition);
						mouseToWorldSpace.z = 0;

						var direcao = (mouseToWorldSpace - transform.position).normalized;

						GameObject auxMagia = Network.Instantiate (magia, posicaoPersonagem.transform.position, Quaternion.identity, 0) as GameObject;
						//auxMagia.myPlayer = this.GetComponent<Player>();
						auxMagia.rigidbody2D.velocity = direcao * 10;
				}
		}


}