strComputer = "."

Function SetHostName(hostnameString)
	strComputer = hostnameString
	End Function



Function GetWMIServices()
	Set GetWMIServices = GetObject("winmgmts:" _
	    & "{impersonationLevel=impersonate}!\\" & strComputer & "\root\cimv2")
End Function

Function WMIDateStringToDate(dtmWMIDate)
    If Not IsNull(dtmWMIDate) Then
    WMIDateStringToDate = CDate(Mid(dtmWMIDate, 5, 2) & "/" & _
         Mid(dtmWMIDate, 7, 2) & "/" & Left(dtmWMIDate, 4) & _
         " " & Mid (dtmWMIDate, 9, 2) & ":" & _
         Mid(dtmWMIDate, 11, 2) & ":" & Mid(dtmWMIDate, _
         13, 2))
    End If
End Function

Function DisplayOutputHeader(strHeader)
	document.all.headOutput.innerText = strHeader
End Function

Function DisplayOutput(strOutput)
	document.all.divOutput.innerHTML = strOutput
End Function

Function GetTableHeader()
	str = "<TABLE class='tblOutput'>"
	str = str & "<THEAD><TR><TH width=30%>Property</TH><TH>Value</TH></TR></THEAD>"
	str = str & "<TBODY>" & vbCRLF
	GetTableHeader = str
End Function

Function GetTableFooter()
	str = "</TBODY>" & vbCRLF & "</TABLE>" & vbCRLF
	GetTableFooter = str
End Function

Function GetRow(PropName, PropValue)
	str = "<TR>"
	str = str & "<TD class='PropName'>" & PropName & "</TD>"
	str = str & "<TD>" & PropValue & "</TD>"
	str = str & "</TR>" & vbCRLF
	GetRow = str
End Function

Function ShowTimeZoneInfo()
	On Error Resume Next
	DisplayOutputHeader("Time zone - Win32_TimeZone")
	str = ""
	Set objWMIService = GetWMIServices()
	Set colItems = objWMIService.ExecQuery("Select * from Win32_TimeZone")
	For Each objItem in colItems
	    str = str & GetTableHeader()
	    str = str & GetRow("Bias", objItem.Bias)
	    str = str & GetRow("Caption", objItem.Caption)
	    str = str & GetRow("Daylight Bias", objItem.DaylightBias)
	    str = str & GetRow("Daylight Day", objItem.DaylightDay)
	    str = str & GetRow("Daylight Day Of Week", objItem.DaylightDayOfWeek)
	    str = str & GetRow("Daylight Hour", objItem.DaylightHour)
	    str = str & GetRow("Daylight Millisecond", objItem.DaylightMillisecond)
	    str = str & GetRow("Daylight Minute", objItem.DaylightMinute)
	    str = str & GetRow("Daylight Month", objItem.DaylightMonth)
	    str = str & GetRow("Daylight Name", objItem.DaylightName)
	    str = str & GetRow("Daylight Second", objItem.DaylightSecond)
	    str = str & GetRow("Daylight Year", objItem.DaylightYear)
	    str = str & GetRow("Description", objItem.Description)
	    str = str & GetRow("Setting ID", objItem.SettingID)
	    str = str & GetRow("Standard Bias", objItem.StandardBias)
	    str = str & GetRow("Standard Day", objItem.StandardDay)
	    str = str & GetRow("Standard Day Of Week", objItem.StandardDayOfWeek)
	    str = str & GetRow("Standard Hour", objItem.StandardHour)
	    str = str & GetRow("Standard Millisecond", objItem.StandardMillisecond)
	    str = str & GetRow("Standard Minute", objItem.StandardMinute)
	    str = str & GetRow("Standard Month", objItem.StandardMonth)
	    str = str & GetRow("Standard Name", objItem.StandardName)
	    str = str & GetRow("Standard Second", objItem.StandardSecond)
	    str = str & GetRow("Standard Year", objItem.StandardYear)
		str = str & GetTableFooter()
	Next
	DisplayOutput(str)
End Function

Function ShowBaseboardInfo()
	On Error Resume Next
	DisplayOutputHeader("Baseboard - Win32_BaseBoard")
	str = ""
	Set objWMIService = GetWMIServices()
	Set colItems = objWMIService.ExecQuery("Select * from Win32_BaseBoard")
	For Each objItem in colItems
	    str = str & GetTableHeader()
	    For Each strOption in objItem.ConfigOptions
	        str = str & GetRow("Configuration Option", strOption)
	    Next
	    str = str & GetRow("Depth", objItem.Depth)
	    str = str & GetRow("Description", objItem.Description)
	    str = str & GetRow("Height", objItem.Height)
	    str = str & GetRow("Hosting Board", objItem.HostingBoard)
	    str = str & GetRow("Hot Swappable", objItem.HotSwappable)
	    str = str & GetRow("Manufacturer", objItem.Manufacturer)
	    str = str & GetRow("Model", objItem.Model)
	    str = str & GetRow("Name", objItem.Name)
	    str = str & GetRow("Other Identifying Information", objItem.OtherIdentifyingInfo)
	    str = str & GetRow("Part Number", objItem.PartNumber)
	    str = str & GetRow("Powered On", objItem.PoweredOn)
	    str = str & GetRow("Product", objItem.Product)
	    str = str & GetRow("Removable", objItem.Removable)
	    str = str & GetRow("Replaceable", objItem.Replaceable)
	    str = str & GetRow("Requirements Description", objItem.RequirementsDescription)
	    str = str & GetRow("Requires DaughterBoard", objItem.RequiresDaughterBoard)
	    str = str & GetRow("Serial Number", objItem.SerialNumber)
	    str = str & GetRow("SKU", objItem.SKU)
	    str = str & GetRow("Slot Layout", objItem.SlotLayout)
	    str = str & GetRow("Special Requirements", objItem.SpecialRequirements)
	    str = str & GetRow("Tag", objItem.Tag)
	    str = str & GetRow("Version", objItem.Version)
	    str = str & GetRow("Weight", objItem.Weight)
	    str = str & GetRow("Width", objItem.Width)
	    str = str & GetTableFooter()
	Next
	DisplayOutput(str)
End Function

Function ShowComputerBusInfo()
	On Error Resume Next
	DisplayOutputHeader("Computer bus - Win32_Bus")
	str = ""
	Set objWMIService = GetWMIServices()
	Set colItems = objWMIService.ExecQuery("Select * from Win32_Bus")
	For Each objItem in colItems
	    str = str & GetTableHeader()
	    str = str & GetRow("Bus Number", objItem.BusNum)
	    str = str & GetRow("Bus Type", objItem.BusType)
	    str = str & GetRow("Description", objItem.Description)
	    str = str & GetRow("Device ID", objItem.DeviceID)
	    str = str & GetRow("Name", objItem.Name)
	    str = str & GetRow("PNP Device ID", objItem.PNPDeviceID)
	    str = str & GetTableFooter()
	Next
	DisplayOutput(str)
End Function

Function ShowDesktopMonitorInfo()
	On Error Resume Next
	DisplayOutputHeader("Desktop monitor - Win32_DesktopMonitor")
	str = ""
	Set objWMIService = GetWMIServices()
	Set colItems = objWMIService.ExecQuery("Select * from Win32_DesktopMonitor")
	For Each objItem in colItems
	    str = str & GetTableHeader()
	    str = str & GetRow("Availability", objItem.Availability)
	    str = str & GetRow("Bandwidth", objItem.Bandwidth)
	    str = str & GetRow("Description", objItem.Description)
	    str = str & GetRow("Device ID", objItem.DeviceID)
	    str = str & GetRow("Display Type", objItem.DisplayType)
	    str = str & GetRow("Is Locked", objItem.IsLocked)
	    str = str & GetRow("Monitor Manufacturer", objItem.MonitorManufacturer)
	    str = str & GetRow("Monitor Type", objItem.MonitorType)
	    str = str & GetRow("Name", objItem.Name)
	    str = str & GetRow("Pixels Per X Logical Inch", objItem.PixelsPerXLogicalInch)
	    str = str & GetRow("Pixels Per Y Logical Inch", objItem.PixelsPerYLogicalInch)
	    str = str & GetRow("PNP Device ID", objItem.PNPDeviceID)
	    str = str & GetRow("Screen Height", objItem.ScreenHeight)
	    str = str & GetRow("Screen Width", objItem.ScreenWidth)
	    str = str & GetTableFooter()
	Next
	DisplayOutput(str)
End Function

Function ShowDeviceMemoryAddressInfo()
	On Error Resume Next
	DisplayOutputHeader("Device memory address - Win32_DeviceMemoryAddress")
	str = ""
	Set objWMIService = GetWMIServices()
	Set colItems = objWMIService.ExecQuery("Select * from Win32_DeviceMemoryAddress")
	For Each objItem in colItems
	    str = str & GetTableHeader()
	    str = str & GetRow("Name", objItem.Name)
	    str = str & GetRow("Starting Address", objItem.StartingAddress)
	    str = str & GetRow("Ending Address", objItem.EndingAddress)
	    str = str & GetTableFooter()
	Next
	DisplayOutput(str)
End Function

Function ShowDMAChannelInfo()
	On Error Resume Next
	DisplayOutputHeader("DMA channel - Win32_DMAChannel")
	str = ""
	Set objWMIService = GetWMIServices()
	Set colItems = objWMIService.ExecQuery("Select * from Win32_DMAChannel")
	For Each objItem in colItems
	    str = str & GetTableHeader()
	    str = str & GetRow("Address Size", objItem.AddressSize)
	    str = str & GetRow("Availability", objItem.Availability)
	    str = str & GetRow("Byte Mode", objItem.ByteMode)
	    str = str & GetRow("Channel Timing", objItem.ChannelTiming)
	    str = str & GetRow("DMA Channel", objItem.DMAChannel)
	    str = str & GetRow("Maximum Transfer Size", objItem.MaxTransferSize)
	    str = str & GetRow("Name", objItem.Name)
	    str = str & GetRow("Type C Timing", objItem.TypeCTiming)
	    str = str & GetRow("Word Mode", objItem.WordMode)
	    str = str & GetTableFooter()
	Next
	DisplayOutput(str)
End Function

Function ShowIRQSettingsInfo()
	On Error Resume Next
	DisplayOutputHeader("IRQ settings - Win32_IRQResource")
	str = ""
	Set objWMIService = GetWMIServices()
	Set colItems = objWMIService.ExecQuery("Select * from Win32_IRQResource")
	For Each objItem in colItems
	    str = str & GetTableHeader()
		str = str & GetRow("Availability", objItem.Availability)
	    str = str & GetRow("Hardware", objItem.Hardware)
	    str = str & GetRow("IRQ Number", objItem.IRQNumber)
	    str = str & GetRow("Name", objItem.Name)
	    str = str & GetRow("Trigger Level", objItem.TriggerLevel)
	    str = str & GetRow("Trigger Type", objItem.TriggerType)
	    str = str & GetTableFooter()
	Next
	DisplayOutput(str)
End Function

Function ShowKeyboardInfo()
	On Error Resume Next
	DisplayOutputHeader("Keyboard - Win32_Keyboard")
	str = ""
	Set objWMIService = GetWMIServices()
	Set colItems = objWMIService.ExecQuery("Select * from Win32_Keyboard")
	For Each objItem in colItems
	    str = str & GetTableHeader()
	    str = str & GetRow("Caption", objItem.Caption)
	    str = str & GetRow("Description", objItem.Description)
	    str = str & GetRow("Device ID", objItem.DeviceID)
	    str = str & GetRow("Is Locked", objItem.IsLocked)
	    str = str & GetRow("Layout", objItem.Layout)
	    str = str & GetRow("Name", objItem.Name)
	    str = str & GetRow("Number of Function Keys", objItem.NumberOfFunctionKeys)
	    str = str & GetRow("Password", objItem.Password)
	    str = str & GetRow("PNP Device ID", objItem.PNPDeviceID)
	    str = str & GetTableFooter()
	Next
	DisplayOutput(str)
End Function

Function ShowMemoryDevicesInfo()
	On Error Resume Next
	DisplayOutputHeader("Memory devices - Win32_MemoryDevice")
	str = ""
	Set objWMIService = GetWMIServices()
	Set colItems = objWMIService.ExecQuery("Select * from Win32_MemoryDevice")
	For Each objItem in colItems
	    str = str & GetTableHeader()
	    str = str & GetRow("Device ID", objItem.DeviceID)
	    str = str & GetRow("Starting Address", objItem.StartingAddress)
	    str = str & GetRow("Ending Address", objItem.EndingAddress)
	    str = str & GetTableFooter()
	Next
	DisplayOutput(str)
End Function

Function ShowOnboardDevicesInfo()
	On Error Resume Next
	DisplayOutputHeader("Onboard devices - Win32_OnBoardDevice")
	str = ""
	Set objWMIService = GetWMIServices()
	Set colItems = objWMIService.ExecQuery("Select * from Win32_OnBoardDevice")
	For Each objItem in colItems
	    str = str & GetTableHeader()
	    str = str & GetRow("Description", objItem.Description)
	    str = str & GetRow("Device Type", objItem.DeviceType)
	    str = str & GetRow("Model", objItem.Model)
	    str = str & GetRow("Name", objItem.Name)
	    str = str & GetRow("Tag", objItem.Tag)
	    str = str & GetRow("Version", objItem.Version)
	    str = str & GetTableFooter()
	Next
	DisplayOutput(str)
End Function

Function ShowParallelPortInfo()
	On Error Resume Next
	DisplayOutputHeader("Parallel ports - Win32_ParallelPort")
	str = ""
	Set objWMIService = GetWMIServices()
	Set colItems = objWMIService.ExecQuery("Select * from Win32_ParallelPort", , 48)
	For Each objItem in colItems
	    str = str & GetTableHeader()
	    str = str & GetRow("Availability", objItem.Availability)
	    For Each strCapability in objItem.Capabilities
	        str = str & GetRow("Capability", strCapability)
	    Next
	    str = str & GetRow("Description", objItem.Description)
	    str = str & GetRow("Device ID", objItem.DeviceID)
	    str = str & GetRow("Name", objItem.Name)
	    str = str & GetRow("OS Auto Discovered", objItem.OSAutoDiscovered)
	    str = str & GetRow("PNP Device ID", objItem.PNPDeviceID)
	    str = str & GetRow("Protocol Supported", objItem.ProtocolSupported)
	    str = str & GetTableFooter()
	Next
	DisplayOutput(str)
End Function

Function ShowPhysicalMemoryInfo()
	On Error Resume Next
	DisplayOutputHeader("Physical memory - Win32_PhysicalMemoryArray")
	str = ""
	Set objWMIService = GetWMIServices()
	Set colItems = objWMIService.ExecQuery("Select * from Win32_PhysicalMemoryArray")
	For Each objItem in colItems
	    str = str & GetTableHeader()
		str = str & GetRow("Description", objItem.Description)
	    str = str & GetRow("Maximum Capacity", objItem.MaxCapacity)
	    str = str & GetRow("Memory Devices", objItem.MemoryDevices)
	    str = str & GetRow("Memory Error Correction", objItem.MemoryErrorCorrection)
	    str = str & GetTableFooter()
	Next
	DisplayOutput(str)
End Function

Function ShowPnPDevicesInfo()
	On Error Resume Next
	DisplayOutputHeader("Plug and play devices - Win32_PnPEntity")
	str = ""
	Set objWMIService = GetWMIServices()
	Set colItems = objWMIService.ExecQuery("Select * from Win32_PnPEntity")
	For Each objItem in colItems
	    str = str & GetTableHeader()
		str = str & GetRow("Class GUID", objItem.ClassGuid)
	    str = str & GetRow("Description", objItem.Description)
	    str = str & GetRow("Device ID", objItem.DeviceID)
	    str = str & GetRow("Manufacturer", objItem.Manufacturer)
	    str = str & GetRow("Name", objItem.Name)
	    str = str & GetRow("PNP Device ID", objItem.PNPDeviceID)
	    str = str & GetRow("Service", objItem.Service)
	    str = str & GetTableFooter()
	Next
	DisplayOutput(str)
End Function

Function ShowPnpSignedDriversInfo()
	On Error Resume Next
	DisplayOutputHeader("Plug and play signed drivers - Win32_PnPSignedDriver")
	str = ""
	Set objWMIService = GetWMIServices()
	Set colItems = objWMIService.ExecQuery("Select * from Win32_PnPSignedDriver")
	For Each objItem in colItems
	    str = str & GetTableHeader()
	    str = str & GetRow("Class Guid", objItem.ClassGuid)
	    str = str & GetRow("Compatibility ID", objItem.CompatID)
	    str = str & GetRow("Description", objItem.Description)
	    str = str & GetRow("Device Class", objItem.DeviceClass)
	    str = str & GetRow("Device ID", objItem.DeviceID)
	    str = str & GetRow("Device Name", objItem.DeviceName)
	    dtmWMIDate = objItem.DriverDate
	    strReturn = WMIDateStringToDate(dtmWMIDate)
	    str = str & GetRow("Driver Date", strReturn)
	    str = str & GetRow("Driver Provider Name", objItem.DriverProviderName)
	    str = str & GetRow("Driver Version", objItem.DriverVersion)
	    str = str & GetRow("HardWare ID", objItem.HardWareID)
	    str = str & GetRow("Inf Name", objItem.InfName)
	    str = str & GetRow("Is Signed", objItem.IsSigned)
	    str = str & GetRow("Manufacturer", objItem.Manufacturer)
	    str = str & GetRow("PDO", objItem.PDO)
	    str = str & GetRow("Signer", objItem.Signer)
	    str = str & GetTableFooter()
	Next
	DisplayOutput(str)
End Function

Function ShowPointingDevicesInfo()
	On Error Resume Next
	DisplayOutputHeader("Pointing devices - Win32_PointingDevice")
	str = ""
	Set objWMIService = GetWMIServices()
	Set colItems = objWMIService.ExecQuery("Select * from Win32_PointingDevice")
	For Each objItem in colItems
	    str = str & GetTableHeader()
	    str = str & GetRow("Description", objItem.Description)
	    str = str & GetRow("Device ID", objItem.DeviceID)
	    str = str & GetRow("Device Interface", objItem.DeviceInterface)
	    str = str & GetRow("Double Speed Threshold", objItem.DoubleSpeedThreshold)
	    str = str & GetRow("Handedness", objItem.Handedness)
	    str = str & GetRow("Hardware Type", objItem.HardwareType)
	    str = str & GetRow("INF File Name", objItem.InfFileName)
	    str = str & GetRow("INF Section", objItem.InfSection)
	    str = str & GetRow("Manufacturer", objItem.Manufacturer)
	    str = str & GetRow("Name", objItem.Name)
	    str = str & GetRow("Number Of Buttons", objItem.NumberOfButtons)
	    str = str & GetRow("PNP Device ID", objItem.PNPDeviceID)
	    str = str & GetRow("Pointing Type", objItem.PointingType)
	    str = str & GetRow("Quad Speed Threshold", objItem.QuadSpeedThreshold)
	    str = str & GetRow("Resolution", objItem.Resolution)
	    str = str & GetRow("Sample Rate", objItem.SampleRate)
	    str = str & GetRow("Synch", objItem.Synch)
	    str = str & GetTableFooter()
	Next
	DisplayOutput(str)
End Function

Function ShowPortConnectorInfo()
	On Error Resume Next
	DisplayOutputHeader("Port connector - Win32_PortConnector")
	str = ""
	Set objWMIService = GetWMIServices()
	Set colItems = objWMIService.ExecQuery("Select * from Win32_PortConnector")
	For Each objItem in colItems
	    str = str & GetTableHeader()
	    str = str & GetRow("Connector Pinout", objItem.ConnectorPinout)
	    For Each strConnectorType in objItem.ConnectorType
	        str = str & GetRow("Connector Type", strConnectorType)
	    Next
	    str = str & GetRow("Description", objItem.Description)
	    str = str & GetRow("External Reference Designator", objItem.ExternalReferenceDesignator)
	    str = str & GetRow("Internal Reference Designator", objItem.InternalReferenceDesignator)
	    str = str & GetRow("Name", objItem.Name)
	    str = str & GetRow("Port Type", objItem.PortType)
	    str = str & GetRow("Serial Number", objItem.SerialNumber)
	    str = str & GetRow("Tag", objItem.Tag)
	    str = str & GetRow("Version", objItem.Version)
	    str = str & GetTableFooter()
	Next
	DisplayOutput(str)
End Function

Function ShowVideoResolutionsInfo()
	On Error Resume Next
	DisplayOutputHeader("Possible video resolutions - CIM_VideoControllerResolution")
	str = ""
	Set objWMIService = GetWMIServices()
	Set colItems = objWMIService.ExecQuery("Select * from CIM_VideoControllerResolution")
	For Each objItem in colItems
	    str = str & GetTableHeader()
	    str = str & GetRow("Setting ID", objItem.SettingID)
	    str = str & GetRow("Horizontal Resolution", objItem.HorizontalResolution)
	    str = str & GetRow("Vertical Resolution", objItem.VerticalResolution)
	    str = str & GetRow("Number Of Colors", objItem.NumberOfColors)
	    str = str & GetRow("Refresh Rate", objItem.RefreshRate)
	    str = str & GetRow("Scan Mode", objItem.ScanMode)
	    str = str & GetTableFooter()
	Next
	DisplayOutput(str)
End Function

Function ShowProcessorInfo()
	On Error Resume Next
	DisplayOutputHeader("Processor - Win32_Processor")
	str = ""
	Set objWMIService = GetWMIServices()
	Set colItems = objWMIService.ExecQuery("Select * from Win32_Processor")
	For Each objItem in colItems
	    str = str & GetTableHeader()
	    str = str & GetRow("Address Width", objItem.AddressWidth)
	    str = str & GetRow("Architecture", objItem.Architecture)
	    str = str & GetRow("Availability", objItem.Availability)
	    str = str & GetRow("CPU Status", objItem.CpuStatus)
	    str = str & GetRow("Current Clock Speed", objItem.CurrentClockSpeed)
	    str = str & GetRow("Data Width", objItem.DataWidth)
	    str = str & GetRow("Description", objItem.Description)
	    str = str & GetRow("Device ID", objItem.DeviceID)
	    str = str & GetRow("Ext Clock", objItem.ExtClock)
	    str = str & GetRow("Family", objItem.Family)
	    str = str & GetRow("L2 Cache Size", objItem.L2CacheSize)
	    str = str & GetRow("L2 Cache Speed", objItem.L2CacheSpeed)
	    str = str & GetRow("Level", objItem.Level)
	    str = str & GetRow("Load Percentage", objItem.LoadPercentage)
	    str = str & GetRow("Manufacturer", objItem.Manufacturer)
	    str = str & GetRow("Maximum Clock Speed", objItem.MaxClockSpeed)
	    str = str & GetRow("Name", objItem.Name)
	    str = str & GetRow("PNP Device ID", objItem.PNPDeviceID)
	    str = str & GetRow("Processor Id", objItem.ProcessorId)
	    str = str & GetRow("Processor Type", objItem.ProcessorType)
	    str = str & GetRow("Revision", objItem.Revision)
	    str = str & GetRow("Role", objItem.Role)
	    str = str & GetRow("Socket Designation", objItem.SocketDesignation)
	    str = str & GetRow("Status Information", objItem.StatusInfo)
	    str = str & GetRow("Stepping", objItem.Stepping)
	    str = str & GetRow("Unique Id", objItem.UniqueId)
	    str = str & GetRow("Upgrade Method", objItem.UpgradeMethod)
	    str = str & GetRow("Version", objItem.Version)
	    str = str & GetRow("Voltage Caps", objItem.VoltageCaps)
	    str = str & GetTableFooter()
	Next
	DisplayOutput(str)
End Function

Function ShowPhysicalMemoryConfigurations()
	On Error Resume Next
	DisplayOutputHeader("Physical memory configurations - Win32_PhysicalMemory")
	str = ""
	Set objWMIService = GetWMIServices()
	Set colItems = objWMIService.ExecQuery("Select * from Win32_PhysicalMemory", , 48)
	For Each objItem in colItems
	    str = str & GetTableHeader()
	    str = str & GetRow("Bank Label", objItem.BankLabel)
	    str = str & GetRow("Capacity", objItem.Capacity)
	    str = str & GetRow("Data Width", objItem.DataWidth)
	    str = str & GetRow("Description", objItem.Description)
	    str = str & GetRow("Device Locator", objItem.DeviceLocator)
	    str = str & GetRow("Form Factor", objItem.FormFactor)
	    str = str & GetRow("Hot Swappable", objItem.HotSwappable)
	    str = str & GetRow("Manufacturer", objItem.Manufacturer)
	    str = str & GetRow("Memory Type", objItem.MemoryType)
	    str = str & GetRow("Name", objItem.Name)
	    str = str & GetRow("Part Number", objItem.PartNumber)
	    str = str & GetRow("Position In Row", objItem.PositionInRow)
	    str = str & GetRow("Speed", objItem.Speed)
	    str = str & GetRow("Tag", objItem.Tag)
	    str = str & GetRow("Type Detail", objItem.TypeDetail)
	    str = str & GetTableFooter()
	Next
	DisplayOutput(str)
End Function

Function ShowSerialPortConfigurations()
	On Error Resume Next
	DisplayOutputHeader("Serial port configuration - Win32_SerialPortConfiguration")
	str = ""
	Set objWMIService = GetWMIServices()
	Set colItems = objWMIService.ExecQuery("Select * from Win32_SerialPortConfiguration")
	For Each objItem in colItems
	    str = str & GetTableHeader()
	    str = str & GetRow("Abort Read Write On Error", objItem.AbortReadWriteOnError)
	    str = str & GetRow("Baud Rate", objItem.BaudRate)
	    str = str & GetRow("Binary Mode Enabled", objItem.BinaryModeEnabled)
	    str = str & GetRow("Bits Per Byte", objItem.BitsPerByte)
	    str = str & GetRow("Continue XMit On XOff", objItem.ContinueXMitOnXOff)
	    str = str & GetRow("CTS Outflow Control", objItem.CTSOutflowControl)
	    str = str & GetRow("Discard NULL Bytes", objItem.DiscardNULLBytes)
	    str = str & GetRow("DSR Outflow Control", objItem.DSROutflowControl)
	    str = str & GetRow("DSR Sensitivity", objItem.DSRSensitivity)
	    str = str & GetRow("DTR Flow Control Type", objItem.DTRFlowControlType)
	    str = str & GetRow("EOF Character", objItem.EOFCharacter)
	    str = str & GetRow("Error Replace Character", objItem.ErrorReplaceCharacter)
	    str = str & GetRow("Error Replacement Enabled", objItem.ErrorReplacementEnabled)
	    str = str & GetRow("Event Character", objItem.EventCharacter)
	    str = str & GetRow("Is Busy", objItem.IsBusy)
	    str = str & GetRow("Name", objItem.Name)
	    str = str & GetRow("Parity", objItem.Parity)
	    str = str & GetRow("Parity Check Enabled", objItem.ParityCheckEnabled)
	    str = str & GetRow("RTS Flow Control Type", objItem.RTSFlowControlType)
	    str = str & GetRow("Setting ID", objItem.SettingID)
	    str = str & GetRow("Stop Bits", objItem.StopBits)
	    str = str & GetRow("XOff Character", objItem.XOffCharacter)
	    str = str & GetRow("XOff XMit Threshold", objItem.XOffXMitThreshold)
	    str = str & GetRow("XOn Character", objItem.XOnCharacter)
	    str = str & GetRow("XOn XMit Threshold", objItem.XOnXMitThreshold)
	    str = str & GetRow("XOn XOff InFlow Control", objItem.XOnXOffInFlowControl)
	    str = str & GetRow("XOn XOff OutFlow Control", objItem.XOnXOffOutFlowControl)
	    str = str & GetTableFooter()
	Next
	DisplayOutput(str)
End Function

Function ShowSerialPortInfo()
	On Error Resume Next
	DisplayOutputHeader("Serial ports - Win32_SerialPort")
	str = ""
	Set objWMIService = GetWMIServices()
	Set colItems = objWMIService.ExecQuery("Select * from Win32_SerialPort", , 48)
	For Each objItem in colItems
	    str = str & GetTableHeader()
	    str = str & GetRow("Binary", objItem.Binary)
	    str = str & GetRow("Description", objItem.Description)
	    str = str & GetRow("Device ID", objItem.DeviceID)
	    str = str & GetRow("Maximum Baud Rate", objItem.MaxBaudRate)
	    str = str & GetRow("Maximum Input Buffer Size", objItem.MaximumInputBufferSize)
	    str = str & GetRow("Maximum Output Buffer Size", objItem.MaximumOutputBufferSize)
	    str = str & GetRow("Name", objItem.Name)
	    str = str & GetRow("OS Auto Discovered", objItem.OSAutoDiscovered)
	    str = str & GetRow("PNP Device ID", objItem.PNPDeviceID)
	    str = str & GetRow("Provider Type", objItem.ProviderType)
	    str = str & GetRow("Settable Baud Rate", objItem.SettableBaudRate)
	    str = str & GetRow("Settable Data Bits", objItem.SettableDataBits)
	    str = str & GetRow("Settable Flow Control", objItem.SettableFlowControl)
	    str = str & GetRow("Settable Parity", objItem.SettableParity)
	    str = str & GetRow("Settable Parity Check", objItem.SettableParityCheck)
	    str = str & GetRow("Settable RLSD", objItem.SettableRLSD)
	    str = str & GetRow("Settable Stop Bits", objItem.SettableStopBits)
	    str = str & GetRow("Supports 16-Bit Mode", objItem.Supports16BitMode)
	    str = str & GetRow("Supports DTRDSR", objItem.SupportsDTRDSR)
	    str = str & GetRow("Supports Elapsed Timeouts", objItem.SupportsElapsedTimeouts)
	    str = str & GetRow("Supports Int Timeouts", objItem.SupportsIntTimeouts)
	    str = str & GetRow("Supports Parity Check", objItem.SupportsParityCheck)
	    str = str & GetRow("Supports RLSD", objItem.SupportsRLSD)
	    str = str & GetRow("Supports RTSCTS", objItem.SupportsRTSCTS)
	    str = str & GetRow("Supports Special Characters", objItem.SupportsSpecialCharacters)
	    str = str & GetRow("Supports XOn XOff", objItem.SupportsXOnXOff)
	    str = str & GetRow("Supports XOn XOff Setting", objItem.SupportsXOnXOffSet)
	    str = str & GetTableFooter()
	Next
	DisplayOutput(str)
End Function

Function ShowSoundCardInfo()
	On Error Resume Next
	DisplayOutputHeader("Sound card - Win32_SoundDevice")
	str = ""
	Set objWMIService = GetWMIServices()
	Set colItems = objWMIService.ExecQuery("Select * from Win32_SoundDevice")
	For Each objItem in colItems
	    str = str & GetTableHeader()
	    str = str & GetRow("Description", objItem.Description)
	    str = str & GetRow("Device ID", objItem.DeviceID)
	    str = str & GetRow("DMA Buffer Size", objItem.DMABufferSize)
	    str = str & GetRow("Manufacturer", objItem.Manufacturer)
	    str = str & GetRow("MPU 401 Address", objItem.MPU401Address)
	    str = str & GetRow("Name", objItem.Name)
	    str = str & GetRow("PNP Device ID", objItem.PNPDeviceID)
	    str = str & GetRow("Product Name", objItem.ProductName)
	    str = str & GetRow("Status Information", objItem.StatusInfo)
	    str = str & GetTableFooter()
	Next
	DisplayOutput(str)
End Function

Function ShowSystemSlotInfo()
	On Error Resume Next
	DisplayOutputHeader("System slot - Win32_SystemSlot")
	str = ""
	Set objWMIService = GetWMIServices()
	Set colItems = objWMIService.ExecQuery("Select * from Win32_SystemSlot",,48)
	For Each objItem in colItems
	    str = str & GetTableHeader()
	    For Each strConnectorPinout in objItem.ConnectorPinout
	        str = str & GetRow("Connector Pinout", strConnectorPinout)
	    Next
	    str = str & GetRow("Connector Type", objItem.ConnectorType)
	    str = str & GetRow("Current Usage", objItem.CurrentUsage)
	    str = str & GetRow("Description", objItem.Description)
	    str = str & GetRow("Height Allowed", objItem.HeightAllowed)
	    str = str & GetRow("Length Allowed", objItem.LengthAllowed)
	    str = str & GetRow("Manufacturer", objItem.Manufacturer)
	    str = str & GetRow("Maximum Data Width", objItem.MaxDataWidth)
	    str = str & GetRow("Model", objItem.Model)
	    str = str & GetRow("Name", objItem.Name)
	    str = str & GetRow("Number", objItem.Number)
	    str = str & GetRow("PME Signal", objItem.PMESignal)
	    str = str & GetRow("Shared", objItem.Shared)
	    str = str & GetRow("Slot Designation", objItem.SlotDesignation)
	    str = str & GetRow("Supports Hot Plug", objItem.SupportsHotPlug)
	    str = str & GetRow("Tag", objItem.Tag)
	    str = str & GetRow("Thermal Rating", objItem.ThermalRating)
	    For Each strVccVoltageSupport in objItem.VccMixedVoltageSupport
	        str = str & GetRow("VCC Mixed Voltage Support", strVccVoltageSupport)
	    Next
	    str = str & GetRow("Version", objItem.Version)
	    For Each strVppVoltageSupport in objItem.VppMixedVoltageSupport
	        str = str & GetRow("VPP Mixed Voltage Support", strVppVoltageSupport)
	    Next
	    str = str & GetTableFooter()
	Next
	DisplayOutput(str)
End Function

Function ShowVideoControllerInfo()
	On Error Resume Next
	DisplayOutputHeader("Video controller - Win32_VideoController")
	str = ""
	Set objWMIService = GetWMIServices()
	Set colItems = objWMIService.ExecQuery("Select * from Win32_VideoController")
	For Each objItem in colItems
	    str = str & GetTableHeader()
	    For Each strCapability in objItem.AcceleratorCapabilities
	        str = str & GetRow("Accelerator Capability", strCapability)
	    Next
	    str = str & GetRow("Adapter Compatibility", objItem.AdapterCompatibility)
	    str = str & GetRow("Adapter DAC Type", objItem.AdapterDACType)
	    str = str & GetRow("Adapter RAM", objItem.AdapterRAM)
	    str = str & GetRow("Availability", objItem.Availability)
	    str = str & GetRow("Color Table Entries", objItem.ColorTableEntries)
	    str = str & GetRow("Current Bits Per Pixel", objItem.CurrentBitsPerPixel)
	    str = str & GetRow("Current Horizontal Resolution", objItem.CurrentHorizontalResolution)
	    str = str & GetRow("Current Number Of Colors", objItem.CurrentNumberOfColors)
	    str = str & GetRow("Current Number Of Columns", objItem.CurrentNumberOfColumns)
	    str = str & GetRow("Current Number Of Rows", objItem.CurrentNumberOfRows)
	    str = str & GetRow("Current Refresh Rate", objItem.CurrentRefreshRate)
	    str = str & GetRow("Current Scan Mode", objItem.CurrentScanMode)
	    str = str & GetRow("Current Vertical Resolution", objItem.CurrentVerticalResolution)
	    str = str & GetRow("Description", objItem.Description)
	    str = str & GetRow("Device ID", objItem.DeviceID)
	    str = str & GetRow("Device Specific Pens", objItem.DeviceSpecificPens)
	    str = str & GetRow("Dither Type", objItem.DitherType)
	    str = str & GetRow("Driver Date", WMIDateStringToDate(objItem.DriverDate))
	    str = str & GetRow("Driver Version", objItem.DriverVersion)
	    str = str & GetRow("ICM Intent", objItem.ICMIntent)
	    str = str & GetRow("ICM Method", objItem.ICMMethod)
	    str = str & GetRow("INF Filename", objItem.InfFilename)
	    str = str & GetRow("INF Section", objItem.InfSection)
	    str = str & GetRow("Installed Display Drivers", objItem.InstalledDisplayDrivers)
	    str = str & GetRow("Maximum Memory Supported", objItem.MaxMemorySupported)
	    str = str & GetRow("Maximum Number Controlled", objItem.MaxNumberControlled)
	    str = str & GetRow("Maximum Refresh Rate", objItem.MaxRefreshRate)
	    str = str & GetRow("Minimum Refresh Rate", objItem.MinRefreshRate)
	    str = str & GetRow("Monochrome", objItem.Monochrome)
	    str = str & GetRow("Name", objItem.Name)
	    str = str & GetRow("Number of Color Planes", objItem.NumberOfColorPlanes)
	    str = str & GetRow("Number of Video Pages", objItem.NumberOfVideoPages)
	    str = str & GetRow("PNP Device ID", objItem.PNPDeviceID)
	    str = str & GetRow("Reserved System Palette Entries", objItem.ReservedSystemPaletteEntries)
	    str = str & GetRow("Specification Version", objItem.SpecificationVersion)
	    str = str & GetRow("System Palette Entries", objItem.SystemPaletteEntries)
	    str = str & GetRow("Video Architecture", objItem.VideoArchitecture)
	    str = str & GetRow("Video Memory Type", objItem.VideoMemoryType)
	    str = str & GetRow("Video Mode", objItem.VideoMode)
	    str = str & GetRow("Video Mode Description", objItem.VideoModeDescription)
	    str = str & GetRow("Video Processor", objItem.VideoProcessor)
	    str = str & GetTableFooter()
	Next
	DisplayOutput(str)
End Function

Function ShowBatteryInfo()
	On Error Resume Next
	DisplayOutputHeader("Battery - Win32_Battery")
	str = ""
	Set objWMIService = GetWMIServices()
	Set colItems = objWMIService.ExecQuery("Select * from Win32_Battery")
	For Each objItem in colItems
	    str = str & GetTableHeader()
	    str = str & GetRow("Availability", objItem.Availability)
	    str = str & GetRow("Battery Status", objItem.BatteryStatus)
	    str = str & GetRow("Chemistry", objItem.Chemistry)
	    str = str & GetRow("Description", objItem.Description)
	    str = str & GetRow("Design Voltage", objItem.DesignVoltage)
	    str = str & GetRow("Device ID", objItem.DeviceID)
	    str = str & GetRow("Estimated Run Time", objItem.EstimatedRunTime)
	    str = str & GetRow("Name", objItem.Name)
	    For Each objElement In objItem.PowerManagementCapabilities
	        str = str & GetRow("Power Management Capabilities", objElement)
	    Next
	    str = str & GetRow("Power Management Supported", objItem.PowerManagementSupported)
	    str = str & GetTableFooter()
	Next
	DisplayOutput(str)
End Function

Function ShowBIOSInfo()
	On Error Resume Next
	DisplayOutputHeader("BIOS - Win32_BIOS")
	str = ""
	Set objWMIService = GetObject("winmgmts:{impersonationLevel=impersonate}!\\" & strComputer & "\root\cimv2")
	Set colBIOS = objWMIService.ExecQuery("Select * from Win32_BIOS")
	For each objBIOS in colBIOS
	    str = str & GetTableHeader()
	    str = str & GetRow("Build Number", objBIOS.BuildNumber)
	    str = str & GetRow("Current Language", objBIOS.CurrentLanguage)
	    str = str & GetRow("Installable Languages", objBIOS.InstallableLanguages)
	    str = str & GetRow("Manufacturer", objBIOS.Manufacturer)
	    str = str & GetRow("Name", objBIOS.Name)
	    str = str & GetRow("Primary BIOS", objBIOS.PrimaryBIOS)
	    str = str & GetRow("Release Date", objBIOS.ReleaseDate)
	    str = str & GetRow("Serial Number", objBIOS.SerialNumber)
	    str = str & GetRow("SMBIOS Version", objBIOS.SMBIOSBIOSVersion)
	    str = str & GetRow("SMBIOS Major Version", objBIOS.SMBIOSMajorVersion)
	    str = str & GetRow("SMBIOS Minor Version", objBIOS.SMBIOSMinorVersion)
	    str = str & GetRow("SMBIOS Present", objBIOS.SMBIOSPresent)
	    str = str & GetRow("Status", objBIOS.Status)
	    str = str & GetRow("Version", objBIOS.Version)
	    For i = 0 to Ubound(objBIOS.BiosCharacteristics)
	        str = str & GetRow("BIOS Characteristics", objBIOS.BiosCharacteristics(i))
	    Next
	    str = str & GetTableFooter()
	Next
	DisplayOutput(str)
End Function

Function ShowCacheMemoryInfo()
	On Error Resume Next
	DisplayOutputHeader("Cache memory - Win32_CacheMemory")
	str = ""
	Set objWMIService = GetWMIServices()
	Set colItems = objWMIService.ExecQuery("Select * from Win32_CacheMemory")
	For Each objItem in colItems
	    str = str & GetTableHeader()
	    str = str & GetRow("Access", objItem.Access)
	    For Each objElement In objItem.AdditionalErrorData
	        str = str & GetRow("Additional Error Data", objElement)
	    Next
	    str = str & GetRow("Associativity", objItem.Associativity)
	    str = str & GetRow("Availability", objItem.Availability)
	    str = str & GetRow("Block Size", objItem.BlockSize)
	    str = str & GetRow("Cache Speed", objItem.CacheSpeed)
	    str = str & GetRow("Cache Type", objItem.CacheType)
	    For Each objElement In objItem.CurrentSRAM
	        str = str & GetRow("Current SRAM", objElement)
	    Next
	    str = str & GetRow("Description", objItem.Description)
	    str = str & GetRow("Device ID", objItem.DeviceID)
	    str = str & GetRow("Error Correct Type", objItem.ErrorCorrectType)
	    str = str & GetRow("Installed Size", objItem.InstalledSize)
	    str = str & GetRow("Level", objItem.Level)
	    str = str & GetRow("Location", objItem.Location)
	    str = str & GetRow("Maximum Cache Size", objItem.MaxCacheSize)
	    str = str & GetRow("Name", objItem.Name)
	    str = str & GetRow("Number Of Blocks", objItem.NumberOfBlocks)
	    str = str & GetRow("Status Information", objItem.StatusInfo)
	    For Each objElement In objItem.SupportedSRAM
	        str = str & GetRow("Supported SRAM", objElement)
	    Next
	    str = str & GetRow("WritePolicy", objItem.WritePolicy)
	    str = str & GetTableFooter()
	Next
	DisplayOutput(str)
End Function

Function ShowFanInfo()
	On Error Resume Next
	DisplayOutputHeader("Fan - Win32_Fan")
	str = ""
	Set objWMIService = GetWMIServices()
	Set colItems = objWMIService.ExecQuery("Select * from Win32_Fan")
	For Each objItem in colItems
	    str = str & GetTableHeader()
	    str = str & GetRow("Active Cooling", objItem.ActiveCooling)
	    str = str & GetRow("Availability", objItem.Availability)
	    str = str & GetRow("Device ID", objItem.DeviceID)
	    str = str & GetRow("Name", objItem.Name)
	    str = str & GetRow("Status Information", objItem.StatusInfo)
	    str = str & GetTableFooter()
	Next
	DisplayOutput(str)
End Function

Function ShowModemInfo()
	On Error Resume Next
	DisplayOutputHeader("Modem - Win32_POTSModem")
	str = ""
	Set objWMIService = GetWMIServices()
	Set colItems = objWMIService.ExecQuery("Select * from Win32_POTSModem")
	For Each objItem in colItems
	    str = str & GetTableHeader()
	    str = str & GetRow("Attached To", objItem.AttachedTo)
	    str = str & GetRow("Blind Off", objItem.BlindOff)
	    str = str & GetRow("Blind On", objItem.BlindOn)
	    str = str & GetRow("Compression Off", objItem.CompressionOff)
	    str = str & GetRow("Compression On", objItem.CompressionOn)
	    str = str & GetRow("Configuration Manager Error Code", objItem.ConfigManagerErrorCode)
	    str = str & GetRow("Configuration Manager User Configuration", objItem.ConfigManagerUserConfig)
	    str = str & GetRow("Configuration Dialog", objItem.ConfigurationDialog)
	    str = str & GetRow("Country Selected", objItem.CountrySelected)
	    For Each objElement In objItem.DCB
	        str = str & GetRow("DCB", objElement)
	    Next
	    For Each objElement In objItem.Default
	        str = str & GetRow("Default", objElement)
	    Next
	    str = str & GetRow("Device ID", objItem.DeviceID)
	    str = str & GetRow("Device Type", objItem.DeviceType)
	    str = str & GetRow("Driver Date", objItem.DriverDate)
	    str = str & GetRow("Error Control Forced", objItem.ErrorControlForced)
	    str = str & GetRow("Error Control Off", objItem.ErrorControlOff)
	    str = str & GetRow("Error Control On", objItem.ErrorControlOn)
	    str = str & GetRow("Flow Control Hard", objItem.FlowControlHard)
	    str = str & GetRow("Flow Control Off", objItem.FlowControlOff)
	    str = str & GetRow("Flow Control Soft", objItem.FlowControlSoft)
	    str = str & GetRow("Inactivity Scale", objItem.InactivityScale)
	    str = str & GetRow("Inactivity Timeout", objItem.InactivityTimeout)
	    str = str & GetRow("Index", objItem.Index)
	    str = str & GetRow("Maximum Baud Rate To SerialPort", objItem.MaxBaudRateToSerialPort)
	    str = str & GetRow("Model", objItem.Model)
	    str = str & GetRow("Modem Inf Path", objItem.ModemInfPath)
	    str = str & GetRow("Modem Inf Section", objItem.ModemInfSection)
	    str = str & GetRow("Modulation Bell", objItem.ModulationBell)
	    str = str & GetRow("Modulation CCITT", objItem.ModulationCCITT)
	    str = str & GetRow("Name", objItem.Name)
	    str = str & GetRow("PNP Device ID", objItem.PNPDeviceID)
	    str = str & GetRow("Port SubClass", objItem.PortSubClass)
	    str = str & GetRow("Prefix", objItem.Prefix)
	    For Each objElement In objItem.Properties
	        str = str & GetRow("Properties", objElement)
	    Next
	    str = str & GetRow("Provider Name", objItem.ProviderName)
	    str = str & GetRow("Pulse", objItem.Pulse)
	    str = str & GetRow("Reset", objItem.Reset)
	    str = str & GetRow("Responses Key Name", objItem.ResponsesKeyName)
	    str = str & GetRow("Speaker Mode Dial", objItem.SpeakerModeDial)
	    str = str & GetRow("Speaker Mode Off", objItem.SpeakerModeOff)
	    str = str & GetRow("Speaker Mode On", objItem.SpeakerModeOn)
	    str = str & GetRow("Speaker Mode Setup", objItem.SpeakerModeSetup)
	    str = str & GetRow("Speaker Volume High", objItem.SpeakerVolumeHigh)
	    str = str & GetRow("Speaker Volume Info", objItem.SpeakerVolumeInfo)
	    str = str & GetRow("Speaker Volume Low", objItem.SpeakerVolumeLow)
	    str = str & GetRow("Speaker Volume Med", objItem.SpeakerVolumeMed)
	    str = str & GetRow("Status Info", objItem.StatusInfo)
	    str = str & GetRow("Terminator", objItem.Terminator)
	    str = str & GetRow("Tone", objItem.Tone)
	    str = str & GetTableFooter()
	Next
	DisplayOutput(str)
End Function

Function ShowPCMCIAInfo()
	On Error Resume Next
	DisplayOutputHeader("PCMCIA controller - Win32_PCMCIAController")
	str = ""
	Set objWMIService = GetWMIServices()
	Set colItems = objWMIService.ExecQuery("Select * from Win32_PCMCIAController")
	For Each objItem in colItems
	    str = str & GetTableHeader()
	    str = str & GetRow("Configuration Manager Error Code", _
	        objItem.ConfigManagerErrorCode)
	    str = str & GetRow("Configuration Manager User Configuration", _
	        objItem.ConfigManagerUserConfig)
	    str = str & GetRow("Device ID", objItem.DeviceID)
	    str = str & GetRow("Manufacturer", objItem.Manufacturer)
	    str = str & GetRow("Name", objItem.Name)
	    str = str & GetRow("PNP Device ID", objItem.PNPDeviceID)
	    str = str & GetRow("Protocol Supported", objItem.ProtocolSupported)
	    str = str & GetTableFooter()
	Next
	DisplayOutput(str)
End Function

Function ShowSystemInformation()
	On Error Resume Next
	DisplayOutputHeader("System information")
	str = ""
	
	Set objWMIService = GetWMIServices()
	Set colSettings = objWMIService.ExecQuery("Select * from Win32_OperatingSystem")
	str = str & "<H2>Operating systems</H2>" & vbCRLF
	For Each objOperatingSystem in colSettings
	    str = str & GetTableHeader()
	    str = str & GetRow("OS Name", objOperatingSystem.Name)
	    str = str & GetRow("Version", objOperatingSystem.Version)
	    str = str & GetRow("Service Pack", _
	        objOperatingSystem.ServicePackMajorVersion & "." & objOperatingSystem.ServicePackMinorVersion)
	    str = str & GetRow("OS Manufacturer", objOperatingSystem.Manufacturer)
	    str = str & GetRow("Windows Directory", objOperatingSystem.WindowsDirectory)
	    str = str & GetRow("Locale", objOperatingSystem.Locale)
	    str = str & GetRow("Available Physical Memory", objOperatingSystem.FreePhysicalMemory)
	    str = str & GetRow("Total Virtual Memory", objOperatingSystem.TotalVirtualMemorySize)
	    str = str & GetRow("Available Virtual Memory", objOperatingSystem.FreeVirtualMemory)
	    str = str & GetRow("OS Name", objOperatingSystem.SizeStoredInPagingFiles)
	    str = str & GetTableFooter()
	Next
	
	Set colSettings = objWMIService.ExecQuery("Select * from Win32_ComputerSystem")
	str = str & "<H2>Computer systems</H2>" & vbCRLF
	For Each objComputer in colSettings
	    str = str & GetTableHeader()
	    str = str & GetRow("System Name", objComputer.Name)
	    str = str & GetRow("System Manufacturer", objComputer.Manufacturer)
	    str = str & GetRow("System Model", objComputer.Model)
	    str = str & GetRow("Time Zone", objComputer.CurrentTimeZone)
	    str = str & GetRow("Total Physical Memory", objComputer.TotalPhysicalMemory)
	    str = str & GetTableFooter()
	Next
	
	Set colSettings = objWMIService.ExecQuery("Select * from Win32_Processor")
	str = str & "<H2>Processors</H2>" & vbCRLF
	For Each objProcessor in colSettings
	    str = str & GetTableHeader()
	    str = str & GetRow("System Type", objProcessor.Architecture)
	    str = str & GetRow("Processor", objProcessor.Description)
	    str = str & GetTableFooter()
	Next
	
	Set colSettings = objWMIService.ExecQuery("Select * from Win32_BIOS")
	str = str & "<H2>BIOS</H2>" & vbCRLF
	For Each objBIOS in colSettings
	    str = str & GetTableHeader()
	    str = str & GetRow("BIOS Version", objBIOS.Version)
	    str = str & GetTableFooter()
	Next
	
	Set colItems = objWMIService.ExecQuery("Select * from Win32_DisplayConfiguration")
	str = str & "<H2>Display configuration</H2>" & vbCRLF
	For Each objItem in colItems
	    str = str & GetTableHeader()
	    str = str & GetRow("Bits Per Pel", objItem.BitsPerPel)
	    str = str & GetRow("Device Name", objItem.DeviceName)
	    str = str & GetRow("Display Flags", objItem.DisplayFlags)
	    str = str & GetRow("Display Frequency", objItem.DisplayFrequency)
	    str = str & GetRow("Driver Version", objItem.DriverVersion)
	    str = str & GetRow("Log Pixels", objItem.LogPixels)
	    str = str & GetRow("Pels Height", objItem.PelsHeight)
	    str = str & GetRow("Pels Width", objItem.PelsWidth)
	    str = str & GetRow("Setting ID", objItem.SettingID)
	    str = str & GetRow("Specification Version", objItem.SpecificationVersion)
	    str = str & GetTableFooter()
	Next

	DisplayOutput(str)
End Function
