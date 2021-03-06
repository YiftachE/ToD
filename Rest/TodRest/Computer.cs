﻿using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace TodREST
{
    public class Computer
    {
        public string ComputerId
        {
            get;
            set;
        }

        [ElasticProperty(Type = FieldType.GeoPoint)]
        public GeoLocation Location
        {
            get;
            set;
        }
    }
}