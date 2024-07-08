using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace VoxelBusters.EasyMLKit
{
    public partial class Barcode
    {
        public class DrivingLicense
        {
            public string AddressCity
            {
                get;
                private set;
            }

            public string AddressState
            {
                get;
                private set;
            }

            public string AddressStreet
            {
                get;
                private set;
            }

            public string AddressZip
            {
                get;
                private set;
            }


            public DateTime BirthDay
            {
                get;
                private set;
            }

            public string DocumentType
            {
                get;
                private set;
            }

            public DateTime ExpiresAt
            {
                get;
                private set;
            }

            public string FirstName
            {
                get;
                private set;
            }

            public string MiddleName
            {
                get;
                private set;
            }

            public string LastName
            {
                get;
                private set;
            }

            public string Gender
            {
                get;
                private set;
            }

            public DateTime IssuedOn
            {
                get;
                private set;
            }

            public string IssuingCountry
            {
                get;
                private set;
            }

            public string LicenseNumber
            {
                get;
                private set;
            }

            public DrivingLicense(string documentType, long birthdayTimestamp, long expiresAtTimestamp, long issuedOnTimestamp, string issuingCountry, string licenseNumber, string firstName, string middleName, string lastName, string gender, string addressCity, string addressState, string addressStreet, string addressZip)
            {
                DocumentType = documentType;
                BirthDay = new DateTime(birthdayTimestamp);
                ExpiresAt = new DateTime(expiresAtTimestamp);
                IssuedOn = new DateTime(issuedOnTimestamp);
                IssuingCountry = issuingCountry;
                LicenseNumber = licenseNumber;
                FirstName = firstName;
                MiddleName = middleName;
                LastName = lastName;
                Gender = gender;
                AddressCity = addressCity;
                AddressStreet = addressStreet;
                AddressState = addressState;
                AddressZip = addressZip;
            }
        }
    }
}