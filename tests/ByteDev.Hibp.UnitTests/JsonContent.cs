﻿using System.Net.Http;
using System.Text;

namespace ByteDev.Hibp.UnitTests
{
    public class JsonContent : StringContent
    {
        public JsonContent(string content)
            : this(content, Encoding.UTF8)
        {
        }

        public JsonContent(string content, Encoding encoding)
            : base(content, encoding, "application/json")
        {
        }
    }
}