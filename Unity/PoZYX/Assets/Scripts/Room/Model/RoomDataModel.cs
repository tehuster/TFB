using System;
using UnityEngine;

namespace Feature.Room {
	/// <summary>
	/// RoomDataModel.cs
	/// <summary>
	/// Author: Thomas Jonckheere
	/// 
	/// TODO: huidige objects saven en vector2 van de struct.
	/// </summary>
	[CreateAssetMenu(fileName = "New Scenario Data Model", menuName = "FTB/Scenario/New Scenario Data Model")]
	public class RoomDataModel : ScriptableObject {
		public Anchor[] anchors;
        public Anchor[] targets;

		[Serializable]
		public struct Anchor {
			public float x;
			public float y;
		}
	}
}