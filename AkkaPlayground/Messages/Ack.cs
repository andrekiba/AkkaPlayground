﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaPlayground.Messages
{
    public class Ack
    {
        public static Ack Instance { get; } = new Ack();
    }
}
