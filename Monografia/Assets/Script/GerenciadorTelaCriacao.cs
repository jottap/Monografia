using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GerenciadorTelaCriacao : MonoBehaviour
{
		private int index = 0;
		public Image avatarSelecionado;	
		public InputField inputName;


		public void anterior ()
		{
				index --;
				index = index < 0 ? (ListaAvatar.Instance.listaSprite .Count - 1) : index;
				avatarSelecionado.sprite = ListaAvatar.Instance.listaSprite [index];
		}
		public void proximo ()
		{
				index ++;
				index = index % ListaAvatar.Instance.listaSprite.Count;
				avatarSelecionado.sprite = ListaAvatar.Instance.listaSprite [index];
		}

		public void create ()
		{
				PlayerInfo.Nome = inputName.text;
				PlayerInfo.SpriteEscolhido = avatarSelecionado.sprite;
				Application.LoadLevel ("menu");
		}

}
