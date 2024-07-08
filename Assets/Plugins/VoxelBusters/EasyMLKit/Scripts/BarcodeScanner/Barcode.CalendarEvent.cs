using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace VoxelBusters.EasyMLKit
{
    public partial class Barcode
    {
        public class CalendarEvent
        {
            public string Description
            {
                get;
                private set;
            }

            public DateTime End
            {
                get;
                private set;
            }

            public string Organizer
            {
                get;
                private set;
            }

            public DateTime Start
            {
                get;
                private set;
            }

            public string Location
            {
                get;
                private set;
            }

            public string Status
            {
                get;
                private set;
            }

            public string Summary
            {
                get;
                private set;
            }

            public CalendarEvent(long startTimestamp, long endTimestamp, string description, string summary, string location, string organizer, string status)
            {
                Start = new DateTime(startTimestamp);
                End = new DateTime(endTimestamp);
                Description = description;
                Summary = summary;
                Organizer = organizer;
                Status = status;
                Location = location;
            }
        }
    }
}