﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Entity
{
    public class AbyssDeploy
    {
        public Character character { get; set; }
        public Weapon weapon { get; set; }
        public List<Stigmata> stigmataList { get; set; }
    }
}