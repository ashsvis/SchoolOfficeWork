using System;
using System.Collections;
using System.Collections.Generic;

namespace Model
{
    [Serializable]
    public class SchoolOffice
    {
        public Partitions Partitions { get; set; }
        public Appointments Appointments { get; set; }
        public Employees Employees { get; set; }
        public KindDocuments KindDocuments { get; set; }
        public Performers Performers { get; set; }
        public Subscribers Subscribers { get; set; }

        public SchoolOffice()
        {
            RegistryTables();
        }

        public void RegistryTables()
        {
            if (Partitions == null) Partitions = new Partitions();
            RegistryTable("Список подразделений", new Partition(), Partitions);

            if (Appointments == null) Appointments = new Appointments();
            RegistryTable("Перечень должностей", new Appointment(), Appointments);

            if (Employees == null) Employees = new Employees();
            RegistryTable("Список сотрудников", new Employee(), Employees);

            if (KindDocuments == null) KindDocuments = new KindDocuments();
            RegistryTable("Виды документов", new KindDocument(), KindDocuments);

            if (Performers == null) Performers = new Performers();
            RegistryTable("Список исполнителей", new Performer(), Performers);

            if (Subscribers == null) Subscribers = new Subscribers();
            RegistryTable("Список подписчиков", new Subscriber(), Subscribers);
        }

        public TableInfo GetTableInfo(string name)
        {
            return (TableInfo)Tables[name];
        }

        public string[] GetTableNames()
        {
            var list = new List<string>();
            foreach (var item in Tables.Keys)
                list.Add(item.ToString());
            list.Sort();
            return list.ToArray();
        }

        public Hashtable Tables { get; private set; } = new Hashtable();

        public void RegistryTable(string name, object item, object table)
        {
            if (Tables.ContainsKey(name)) return;
            Tables[name] = new TableInfo
            {
                TableName = name,
                Table = table,
                Item = item
            };
        }
    }

    [Serializable]
    public class TableInfo
    {
        public string TableName { get; set; }
        public object Table { get; set; }
        public object Item { get; set; }
    } 

}
