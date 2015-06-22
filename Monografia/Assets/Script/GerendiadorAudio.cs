using UnityEngine;
using System.Collections;

public class GerendiadorAudio : MonoBehaviour
{
		public AudioClip clickMenu;

		public void Click ()
		{
				//audio
				AudioSource.PlayClipAtPoint (clickMenu, transform.position);
		}

		public void PlayAudio (AudioClip audio)
		{
				AudioSource.PlayClipAtPoint (audio, transform.position);
		}
}
