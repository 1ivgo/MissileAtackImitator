﻿namespace MissileAtackImitatorNS.Data
{
    class CurrentInfoDGVRow
    {
        public CurrentInfoDGVRow(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }

        public object Value { get; set; }
    }
}
