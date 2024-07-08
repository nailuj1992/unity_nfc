using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace VoxelBusters.EasyMLKit
{
    public partial class Barcode
    {
        public class Email
        {
            public enum EmailType
            {
                Unknown,
                Home,
                Work
            }

            public EmailType Type
            {
                get;
                private set;
            }

            public string Address
            {
                get;
                private set;
            }

            public string Subject
            {
                get;
                private set;
            }

            public string Body
            {
                get;
                private set;
            }


            public Email(EmailType type, string address, string subject, string body)
            {
                Type    = type;
                Address = address;
                Subject = subject;
                Body    = body;
            }
        }
    }
}