namespace Feature.Networking {
	public class NetworkingEventTypes {
		private const string PREFIX = "NETWORKING_";

		/// <summary>
		/// Arguments: -
		/// </summary>
		public const string SEND_DATA = "SEND_DATA";

        /// <summary>
        /// Arguments: (Bool)Motors State
        /// </summary>
        public const string TOGGLE_MOTORS = "TOGGLE_MOTORS";
	}
}