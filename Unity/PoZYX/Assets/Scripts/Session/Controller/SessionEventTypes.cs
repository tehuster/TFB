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
}