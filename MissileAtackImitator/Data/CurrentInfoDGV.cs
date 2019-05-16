using System.Collections.Generic;

namespace MissileAtackImitatorNS.Data
{
    class CurrentInfoDGV : List<CurrentInfoDGVRow>
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
    }
}
