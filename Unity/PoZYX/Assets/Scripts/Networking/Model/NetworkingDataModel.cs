using UnityEngine;

namespace Feature.Networking {
	/// <summary>
	/// NetworkingDataModel.cs
	/// <summary>
	/// Author: Thomas Jonckheere
	/// </summary>
	[CreateAssetMenu(fileName = "Networking Data Model", menuName = "FTB/Networking/New Networking Data Model")]
	public class NetworkingDataModel : ScriptableObject {
		[Header("Receiving Data Settings")]
		public string receiveIP;
		public int receivePort;

		[Header("Sending Data Settings")]
		public string sendIP;
		public int sendPort;
		[Space]
		public string dataString;
	}
}