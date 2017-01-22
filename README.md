# TajGo Azure IoT Hub Simulator Sample

This is a sample on how to send / receive data with Azure IoT Hub and then kicking off Azure Stream Analytics Service to perform data management on the received data.

It consist out of 2 projects:

- **Tajgo.Client.Simulator** - it purpose is to send simulated data to the Azure IoT Hub
- **Tajgo.Client.Receiver** - it purpose is to get data from Azure IoT Hub to check, if sending data was a success

After data is received, [Azure Stream Analytics](https://azure.microsoft.com/en-us/services/stream-analytics/ "Azure Stream Analytics") kicks in and perform data management. 

    {
    "SystemPressure":0,
    "SplittingCilinderPressure":0
    "SawPressure":0,
    "OilTemperature":0,
    "LogLength":0,
    "LogDiameter":0,
    "TotalQuantity":0,
    "CurrentQuantity":0,
    "CurrentLogProduction":0,
    "MaksimumPullForce":0,
    "ShaftTurns":0,
    "TotalWorkingHours":0,
    "IntervalWorkingHours":0,
    "CorrectionFactor":0
    }


Query in Azure Stream Analytics get's the data from Azure IoT Hub and then saves data to  [Azure Table Storage](https://docs.microsoft.com/en-us/azure/storage/storage-dotnet-how-to-use-tables "Azure Table Storage") for raw log processing. Other data is saved into [Sql Azure](https://azure.microsoft.com/en-us/services/sql-database/?b=16.50 "Azure SQL"). Other services are added to for additional data management possibilities (like PowerBI for creating rich visualizations, etc.).


The result is then entries in the database with following structure. Data is then be pulled out of database for data analysis and preview on website (other database tables ommited).

![Database](https://gt0xdw.bn1301.livefilestore.com/y3mtDOx-EWJbrM881IQ2GOsrW_JlDANEqTWJRt_cRgPhKIzsboAQMNjcvf_vXWdHZXU5Y6cKu2s235vdaIUcJj6MPgpvxq6gcCxZO6rM080NY6X1Fc77kqyXVNNQ16JSqq_nGpp2695C8cIh4IWh5aRebbNfFWNqzkX-NCzPs_BtSk?width=594&height=609&cropmode=none)

You can download SQL script for creating the upper structure [here](https://1drv.ms/u/s!At3HFvGXo562q4skM9Si7SrISHFwtw "Download script").

## Sample instructions for code to work ##

In order to use the sample, you will need to create Azure IoT Hub. You can create it via [Azure Portal](https://portal.azure.com "Azure Portal"). Follow [this tutorial](https://docs.microsoft.com/en-us/azure/iot-hub/iot-hub-csharp-csharp-getstarted "Tutorial on how to create Azure IoT Hub") to create IoT Hub.

After you created service, you need to change settings in **Settings.cs** file.

![](https://rtsm9w.bn1301.livefilestore.com/y3mF-RyCP92DzFqVj6deOARc-usp_OZUZhmtfRU3NQ-AGAw9CFkcleuJRmxeFNjNSHLq5BJpcdbZdTRqdj77AS80ouhe3AzbGn-KQkYse61kxwDqPVMIFsxHMxXfhHI-FAihhAzSLNbpwQoujeDvU2C_3nDrGzsT7pBJxIEuawinrE?width=600&height=508&cropmode=none)

IoT Hub Host Name is the name of your IoT hub. Click on the **Overview** information in your Azure IoT Hub and copy the name. Replace the **[IotHubhostName]**.

![](https://y9r9eq.bn1301.livefilestore.com/y3mo_X5YsR4M8nWmh8JNHJZQ_1vJV9pirPhfoyYcSA3yOH_R5uc9nw7uu3bgZRE_2j9JC0Efyk5VAheVgRl5-UKq5H9GVhTFidlfi8CtSB0GWSNBqDg6iBY2BEryGdZD_1seM0ZfGN9F5ZhTljSCSp8w3dRIDAX8DrClyN4P4c_4dY?width=474&height=233&cropmode=none)

Now you only need to enter the connection string. It can be obtained in the **Settings** blade. Click on the **Settings** option, choose **Shared access policies**, select **policy** (in our case **iothubowner**) and copy** Connection String** (**primary key**).

![](https://zkplta.bn1301.livefilestore.com/y3m68goLGMXVc73DtssIW6xIfS0wk2FZD0bywo99Q1z8-RwKWxMf7KduUetzOd3yuUHWjY8LbP05AdipIrL9eUi8oMssOdkTBdcszJbTLoLffZygJEBiEeqrJ4-Y-fEsAjNMf_KSt62NbNdHxr6EsZ6w23-2w6cAo6crcv8XStzM54?width=1780&height=558&cropmode=none)

If you have any questions, feel free to contact me.
