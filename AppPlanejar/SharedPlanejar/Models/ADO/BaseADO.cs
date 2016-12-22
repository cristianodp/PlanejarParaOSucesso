using Models.DB;
using SharedPlanejar.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using SharedPlanejar.Utils;
using SharedPlanejar.Models.DB;

namespace SharedPlanejar.Models.ADO
{
    public abstract class BaseADO<T>
    {

        public ValuesWhere where;
        private List<T> list { get; set; }
        protected DataBase dataBase;

        public BaseADO() {
            dataBase = new DataBase();
            where = new ValuesWhere();
        }

        public void ClearWhere() {
            where.clear();
        }

        public List<T> consultar()
        {
            return consultar(where);
        }

        public abstract List<T> consultar(ValuesWhere where);

        public abstract void apagar(T item);

        public abstract long update(T item);

        public abstract long insert(T item);


    }
}
