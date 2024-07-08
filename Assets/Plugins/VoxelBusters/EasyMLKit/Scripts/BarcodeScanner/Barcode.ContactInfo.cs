using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace VoxelBusters.EasyMLKit
{
    public partial class Barcode
    {
        public class ContactInfo
        {
            public List<Barcode.Address> Addresses
            {
                get;
                private set;
            }

            public List<Barcode.Email> Emails
            {
                get;
                private set;
            }

            public Barcode.PersonName Name
            {
                get;
                private set;
            }

            public string Organization
            {
                get;
                private set;
            }

            public List<Barcode.Phone> Phones
            {
                get;
                private set;
            }

            public string Title
            {
                get;
                private set;
            }

            public List<string> Urls
            {
                get;
                private set;
            }

            public ContactInfo(List<Barcode.Address> addresses, List<Barcode.Email> emails, Barcode.PersonName name, string organization, List<Barcode.Phone> phones, string title, List<string> urls)
            {
                Addresses       = addresses;
                Emails          = emails;
                Name            = name;
                Organization    = organization;
                Phones          = phones;
                Title           = title;
                Urls            = urls;
            }
        }
    }
}