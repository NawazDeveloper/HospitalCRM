﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.DtoModel
{
    public class AddressModel
    {
        public long ID { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}
