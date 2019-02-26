using UnityEngine;
using Feature.Networking;

public class POZYX_Parser : MonoBehaviour {
	[SerializeField] private NetworkingDataModel networkData;

	//public StringVariable UDP_Message;
	public POZYXVariable PoZYX;

	private void Update() {
		if (networkData.dataString.Length != 0) {
			string[] words = networkData.dataString.Split(',');
			float newX = float.Parse(words[0]);
			float newY = float.Parse(words[1]);
			//float newZ = float.Parse(words[2]);

			PoZYX.x = newX / 100;
			PoZYX.z = newY / 100;
			PoZYX.yaw = float.Parse(words[2]);

			//PoZYX.y = 2;// / 100;
			//PoZYX.roll = float.Parse(words[4]);
			//PoZYX.pitch = float.Parse(words[5]);
		}
	}
}
