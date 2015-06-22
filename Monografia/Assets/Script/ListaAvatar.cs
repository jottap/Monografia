using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ListaAvatar : MonoBehaviour
{

		public static ListaAvatar Instance {
				get;
				set;
		}
		
		public List<Sprite> listaSprite;

		void Awake ()
		{

				if (Instance != null && Instance != this) {
						Destroy (this.gameObject);
						return;
				}
		
				Instance = this;

		}

}
