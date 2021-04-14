namespace OpenCue.Service
{
    public class Config
    {
        // The name of the profiles directory which define all profiles.
        // This name correspond to a subdirectory in the GameSdkEffects directory
        public string ProfilesDirectoryName { get; set; } = "profiles";
        // This profile is used to automatically detect if iCUE was restarted or crashed.
        // The profile should be empty (have no effects), because it will be active all the time
        public string AutoSyncProfileName { get; set; } = "";
        // The interval in seconds between automatic sync with iCUE
        public int AutoSyncInterval { get; set; } = 10;
        // The name of the profile activated when starting the 
        public string StartProfileName { get; set; } = "";
    }
}
