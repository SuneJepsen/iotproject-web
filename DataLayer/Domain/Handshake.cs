﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Domain
{
    public class Handshake
    {
        public long? Accelometer { get; set; }
        public long? Promixitmity { get; set; }

        public DateTime? CreatedDate{ get; set; }
    }
}
