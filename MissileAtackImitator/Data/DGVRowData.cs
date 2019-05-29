namespace MissileAtackImitatorNS.Data
{
    class DGVRowData
    {
        public DGVRowData(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }

        public object Value { get; set; }
    }
}
