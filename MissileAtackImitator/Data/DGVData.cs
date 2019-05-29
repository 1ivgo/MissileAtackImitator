namespace MissileAtackImitatorNS.Data
{
    using System.ComponentModel;

    internal class DGVData : BindingList<DGVRowData>
    {
        public DGVRowData this [string name]
        {
            get
            {
                foreach (var row in this)
                {
                    if (row.Name == name)
                    {
                        return row;
                    }
                }

                return null;
            }
        }

        public void Add(string name, object value)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new System.ArgumentNullException(nameof(name));
            }

            if (value == null)
            {
                throw new System.ArgumentNullException(nameof(value));
            }

            if (this[name] != null)
            {
                throw new System.Exception("Попытка добавить в таблицу уже существующую строку");
            }

            Add(new DGVRowData(name, value));
        }

        public void Set(string name, object value)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new System.ArgumentNullException(nameof(name));
            }

            if (value == null)
            {
                throw new System.ArgumentNullException(nameof(value));
            }

            int index = IsContain(name);
            if (index != -1)
            {
                this[index].Value = value;
            }
            else
            {
                Add(name, value);
            }

            ResetBindings();
        }

        public int IsContain(string name)
        {
            foreach (var data in this)
            {
                if (data.Name == name)
                {
                    return IndexOf(data);
                }
            }

            return -1;
        }
    }
}
