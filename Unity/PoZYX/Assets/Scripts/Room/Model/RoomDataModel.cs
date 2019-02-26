using System;
using UnityEngine;

namespace Feature.Room {
	/// <summary>
	/// RoomDataModel.cs
	/// <summary>
	/// Author: Thomas Jonckheere
	/// </summary>
	[CreateAssetMenu(fileName = "Room Data Model", menuName = "FTB/Room/New Room Data Model")]
	public class RoomDataModel : ScriptableObject {
		public Anchor[] anchors;

		[Serializable]
		public struct Anchor {
			public float x;
			public float y;
		}
	}
}