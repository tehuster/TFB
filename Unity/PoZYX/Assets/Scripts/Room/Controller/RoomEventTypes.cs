namespace Feature.Room {
	public class RoomEventTypes {
		private const string PREFIX = "ROOM_";

		/// <summary>
		/// Arguments: -
		/// </summary>
		public const string LOAD_ROOM = "LOAD_ROOM";

		/// <summary>
		/// Arguments: 1.(Transform) Anchors Parent.
		/// </summary>
		public const string LOADED_NEW_ROOM = "LOADED_NEW_ROOM";
	}
}