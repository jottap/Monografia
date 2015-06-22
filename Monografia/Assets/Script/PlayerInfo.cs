using UnityEngine;
using System.Collections;

public static class PlayerInfo
{
	#region valores
		public static Sprite SpriteEscolhido {
				get;
				set;
		}
		public static string Nome {
				get;
				set;
		}
		public static string IP {
				get;
				set;
		}
		public static NetworkViewID viewID {
				get;
				set;
		}
	#endregion
}

