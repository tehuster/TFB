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

        /// <summary>
        /// Arguments: 1. (String) Room asset ids;
        /// </summary>
        public const string SET_ROOM_NAMES = "SET_ROOM_NAMES";

        /// <summary>
        /// Arguments: 1. (String) Room asset path;
        /// </summary>
        public const string UPDATE_CURRENT_ROOM = "UPDATE_CURRENT_ROOM";

        /// <summary>
        /// Arguments: -
        /// </summary>
        public const string DELETE_TARGETS = "DELETE_TARGETS";

        /// <summary>
        /// Arguments: 1. (Transform) New target;
        /// </summary>
        public const string NEW_TARGET = "NEW_TARGET";

        /// <summary>
        /// Arguments: 1. (Transform) Current Closest target;
        /// </summary>
        public const string NEW_CLOSEST_TARGET = "NEW_CLOSEST_TARGET";
	}
}