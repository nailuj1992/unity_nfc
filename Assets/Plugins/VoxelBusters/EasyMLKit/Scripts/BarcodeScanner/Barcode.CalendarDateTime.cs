using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace VoxelBusters.EasyMLKit
{
    public partial class Barcode
    {
        public class CalendarDateTime
        {
            public int Day
            {
                get;
                private set;
            }

            public int Hours
            {
                get;
                private set;
            }

            public int Minutes
            {
                get;
                private set;
            }

            public int Month
            {
                get;
                private set;
            }

            public int Seconds
            {
                get;
                private set;
            }

            public int Year
            {
                get;
                private set;
            }

            public bool IsUtc
            {
                get;
                private set;
            }

            public CalendarDateTime(DateTime dateTime, bool isUtc = true)
            {
                Calendar calendar = CultureInfo.InvariantCulture.Calendar;
                Day = calendar.GetDayOfYear(dateTime);
                Hours = calendar.GetHour(dateTime);
                Minutes = calendar.GetMinute(dateTime);
                Month = calendar.GetMonth(dateTime);
                Seconds = calendar.GetSecond(dateTime);
                Year = calendar.GetYear(dateTime);
                IsUtc = isUtc;
            }
        }
    }
}