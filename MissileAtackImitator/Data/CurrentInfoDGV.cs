using System.ComponentModel;

namespace MissileAtackImitatorNS.Data
{
    class CurrentInfoDGV : BindingList<CurrentInfoDGVRow>
    {
        public CurrentInfoDGVRow this [string Name]
        {
            get
            {

                foreach (var row in this)
                {
                    if (row.Name == Name)
                        return row;
                }

                return null;
            }
        }

        public void Add(string name, object value)
        {
            if (this[name] != null)
                throw new System.Exception("Попытка добавить в таблицу уже существующую строку");

            Add(new CurrentInfoDGVRow(name, value));
        }
    }
}
