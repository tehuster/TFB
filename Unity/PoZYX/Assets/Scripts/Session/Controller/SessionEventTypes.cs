public class SessionEventTypes {
    private const string PREFIX = "SESSION_";
    /// <summary>
    /// Arguments: 1. (String) Session Name. 2. (String) Session Disability. 3. (String) Session Scenario.
    /// </summary>
    public const string START = PREFIX + "START";

    /// <summary>
    /// Arguments: -
    /// </summary>
    public const string STOP = PREFIX + "STOP";

    /// <summary>
    /// Arguments: 1. (String) Comment.
    /// </summary>
    public const string ADD_COMMENT = PREFIX + "ADD_COMMENT";

    /// <summary>
    /// Arguments: 1. (Int) New min intensity. 
    /// </summary>
    public const string UPDATE_MIN_INTENSITY = PREFIX + "UPDATE_MIN_INTENSITY"; 

    /// <summary>
    /// Arguments: 1. (Int) New max intensity. 
    /// </summary>
    public const string UPDATE_MAX_INTENSITY = PREFIX + "UPDATE_MAX_INTENSITY";

    /// <summary>
    /// Arguments: 1. (Int) New max distance. 
    /// </summary>
    public const string UPDATE_MAX_DISTANCE = PREFIX + "UPDATE_MAX_DISTANCE";
}