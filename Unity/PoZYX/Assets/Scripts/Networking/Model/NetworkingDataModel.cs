using UnityEngine;

namespace Feature.Networking {
	/// <summary>
	/// NetworkingDataModel.cs
	/// <summary>
	/// Author: Thomas Jonckheere
	/// </summary>
	[CreateAssetMenu(fileName = "Networking Data Model", menuName = "FTB/Networking/New Networking Data Model")]
	public class NetworkingDataModel : ScriptableObject {
		[Header("IP Address")]
		public string IP;

		[Header("Receiving Data Settings")]
		public int receivePort;

		[Header("Sending Data Settings")]
		public int sendPort;

		[Header("POZYX Data String")]
		public string dataString;
	}
}