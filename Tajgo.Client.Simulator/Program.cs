using System;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Common.Exceptions;
using Newtonsoft.Json;

namespace Tajgo.Client.Simulator
{
    /// <summary>
    /// Class Program.
    /// </summary>
    class Program
    {
        /// <summary>
        /// The registry manager
        /// </summary>
        internal static RegistryManager registryManager;
        
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();

            Console.CancelKeyPress += (s, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };
            registryManager = RegistryManager.CreateFromConnectionString(Settings.IotHubOwnerConnectionString);
            MainAsync(args, cts.Token).Wait(cts.Token);
        }

        /// <summary>
        /// main as an asynchronous operation.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="token">The token.</param>
        /// <returns>Task.</returns>
        static async Task MainAsync(string[] args, CancellationToken token)
        {
            Console.WriteLine("Starting simulator....");
            Console.WriteLine("Checking for device registration and checking status.");
            //check, if device is registered
            // TEST to work with devices
            //EB94AA15-2F5C-4B10-AD10-799BA9C40C12
            //4AB5198A-F6AC-41E1-BEDA-8FFCFCC62BFB
            //2645265D-5668-4456-A0B7-DD3FA869A6A2
            //602D7362-8B03-45CB-83FA-B1B99BECC0AA
            var device1 = await AddDeviceAsync("EB94AA15-2F5C-4B10-AD10-799BA9C40C12");
            var device2 = await AddDeviceAsync("4AB5198A-F6AC-41E1-BEDA-8FFCFCC62BFB");
            var device3 = await AddDeviceAsync("2645265D-5668-4456-A0B7-DD3FA869A6A2");
            var device4 = await AddDeviceAsync("602D7362-8B03-45CB-83FA-B1B99BECC0AA");

            int counter = 0;
            while (true)
            {
                //execute endless loop to get data to IoT hub as a simulator
                if (counter % 5 == 0)
                {
                    //send device 1
                    await SendDeviceToCloudMessagesAsync(device1.Id, 
                        device1.Authentication.SymmetricKey.PrimaryKey);
                }
                else if (counter % 3 == 0)
                {
                    //send device 2 
                    await SendDeviceToCloudMessagesAsync(device2.Id, 
                        device2.Authentication.SymmetricKey.PrimaryKey);
                }
                else if (counter % 7 == 0)
                {
                    //send device 3
                    await SendDeviceToCloudMessagesAsync(device3.Id, 
                        device3.Authentication.SymmetricKey.PrimaryKey);

                }
                else
                {
                    //send device 4
                    await SendDeviceToCloudMessagesAsync(device4.Id, 
                        device4.Authentication.SymmetricKey.PrimaryKey);

                }
                counter++;
            }
        }

        /// <summary>
        /// add device as an asynchronous operation.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns>Task&lt;Device&gt;.</returns>
        private static async Task<Device> AddDeviceAsync(string deviceId)
        {
            Device device;
            try
            {
                device = await registryManager.AddDeviceAsync(new Device(deviceId));
            }
            catch (DeviceAlreadyExistsException)
            {
                device = await registryManager.GetDeviceAsync(deviceId);
            }
            Console.WriteLine("Generated device key: {0}",
                device.Authentication.SymmetricKey.PrimaryKey);
            return device;
        }

        private static async Task SendDeviceToCloudMessagesAsync(string deviceId, string deviceKey)
        {
            var rand = new Random();
            var type = new Random();
            var deviceClient = DeviceClient.Create(Settings.IotHubHostName,
                new DeviceAuthenticationWithRegistrySymmetricKey(deviceId, deviceKey));

            int counter = 0;
            while (counter < 100000)
            {
                var telemetryDataPoint = new
                {
                    deviceId,
                    value = rand.NextDouble().ToString(CultureInfo.CurrentCulture),
                    typeId = type.Next(1, 5)
                };
                var messageString = JsonConvert.SerializeObject(telemetryDataPoint);
                var message = new Microsoft.Azure.Devices.Client.Message
                    (Encoding.ASCII.GetBytes(messageString));

                await deviceClient.SendEventAsync(message);
                Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);
                counter++;
            }
            Task.Delay(1000).Wait();
        }
    }
}
