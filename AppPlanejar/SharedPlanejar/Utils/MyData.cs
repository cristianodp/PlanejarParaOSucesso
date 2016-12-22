using System;
using System.Collections.Generic;
using System.Text;

namespace Utils
{
    public class MyData
    {
        public String keyId;
        public String keyValues;

        public MyData(String keyId, String keyValues)
        {
            this.keyId = keyId;
            this.keyValues = keyValues;
        }

        public String getValue()
        {
            return keyValues;
        }

        public int getKeyInt()
        {
            return Convert.ToInt32(keyId);
        }

        public String getKey()
        {
            return keyId;
        }
    }
}
