using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.EasyMLKit
{
    public partial class Barcode
    {
        public BarcodeFormat Format
        {
            get;
            private set;
        }

        public BarcodeValueType ValueType
        {
            get;
            private set;
        }

        public string RawValue
        {
            get;
            private set;
        }

        public byte[] RawBytes
        {
            get;
            private set;
        }

        public string DisplayValue
        {
            get;
            private set;
        }

        public Rect BoundingBox
        {
            get;
            private set;
        }

        private object m_value;
        

        public Barcode(BarcodeFormat format, BarcodeValueType valueType, object value, string rawValue, byte[] rawBytes, string displayValue, Rect boundingBox)
        {
            Format = format;
            ValueType = valueType;
            m_value = value;
            RawValue = rawValue;
            RawBytes = rawBytes;
            DisplayValue = displayValue;
            BoundingBox = boundingBox;
        }

        public T GetValue<T>()
        {
            if (typeof(T) == m_value.GetType())
                return (T)m_value;
            else
                return default;
        }

    }
}