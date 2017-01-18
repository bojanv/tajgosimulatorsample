namespace Tajgo.Client.Simulator
{
    /// <summary>
    /// Class Settings.
    /// </summary>
    public static class Settings
    {
        /// <summary>
        /// The iot hub owner connection string
        /// </summary>
        public const string IotHubOwnerConnectionString = "HostName=tajgoiot.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=dQ6ZbMHh7KYI0inu1ES13AJXShVZVxZ4A7B4CCV7/D0=";
        /// <summary>
        /// The iot hub D2C endpoint
        /// </summary>
        public const string IotHubD2CEndpoint = "messages/events";
        /// <summary>
        /// The iot hub host name
        /// </summary>
        public const string IotHubHostName = "tajgoiot.azure-devices.net";
    }
}