using System;
using System.Collections.Generic;
using System.ComponentModel;
using ViewGenerator;

namespace Model
{
    [Serializable]
    [Description("Сотрудник")]
    public class Employee : IComparable<Employee>
    {
        public Guid IdEmployee { get; set; } = Guid.NewGuid();

        [Description("Фамилия"), DataNotEmpty]
        public string Surname { get; set; }

        [Description("Имя"), DataNotEmpty]
        public string FirstName { get; set; }

        [Description("Отчество"), DataNotEmpty]
        public string LastName { get; set; }

        [Description("Должность"), DataLookup("IdAppointment", "Appointments")]
        public Guid IdAppointment { get; set; }

        [Description("Системное имя"), DataNotEmpty]
        public string Login { get; set; }

        [Description("Пароль"), DataNotEmpty, DataPassword]
        public string Password { get; set; }

        [Description("Кабинет"), DataNotEmpty]
        public string Cabinet { get; set; }

        [Description("Телефон")]
        public string PhoneNumber { get; set; }

        [Description("E-mail")]
        public string Email { get; set; }

        [Description("Подразделение"), DataLookup("IdPartition", "Partitions")]
        public Guid IdPartition { get; set; }

        [Description("Администрирование\n системы")]
        public bool AS { get; set; }
        //public bool AD { get; set; }
        //public bool RK { get; set; }
        //public bool KIZ { get; set; }
        //public bool CR { get; set; }
        //public bool SRF { get; set; }

        public int CompareTo(Employee other)
        {
            return string.Compare(this.ToString(), other.ToString());
        }

        public override string ToString()
        {
            return $"{Surname} {FirstName} {LastName}";
        }
    }

    [Serializable]
    [Description("Список сотрудников")]
    public class Employees : List<Employee>
    {
        public new void Add(Employee item)
        {
            if (base.Exists(x => x.ToString() == item.ToString()))
                throw new Exception($"Сотрудник \"{item}\" уже существует!");
            base.Add(item);
            base.Sort();
        }

        public new void Remove(Employee item)
        {
            base.Remove(item);
        }

    }


}
