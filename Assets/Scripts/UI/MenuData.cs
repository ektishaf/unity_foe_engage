using System.Collections.Generic;

public class SlotData
{
    public string pilotName;
    public string machineName;
}

public static class MenuData
{
    public static List<SlotData> Slots = new List<SlotData>
    {
        new SlotData { pilotName = "Pilot A", machineName = "Wanzor" },
        new SlotData { pilotName = "Pilot B", machineName = "Zenith" },
        new SlotData { pilotName = "---", machineName = "---" },
        new SlotData { pilotName = "---", machineName = "---" }
    };
}
