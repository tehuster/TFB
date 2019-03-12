using UnityEngine;

namespace Feature.Session {
    /// <summary>
    /// SessionDataModel.cs
    /// <summary>
    /// Author: Thomas Jonckheere
    /// </summary>
    [CreateAssetMenu(fileName = "Session Data Model", menuName = "FTB/Session/New Session Data Model")]
    public class SessionDataModel : ScriptableObject {
        [Header("Session Data")]
        public string Name;
        public string Date;
        public string Disability;
    }
}