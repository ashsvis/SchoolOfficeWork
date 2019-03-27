using System;
using System.Collections.Generic;
using System.ComponentModel;
using ViewGenerator;

namespace Model
{
    [Serializable]
    [Description("Должность")]
    public class Appointment : IComparable<Appointment>
    {
        public Guid IdAppointment { get; set; } = Guid.NewGuid();

        [Description("Должность"), DataNotEmpty]
        public string NameAppointment { get; set; }

        public int CompareTo(Appointment other)
        {
            return string.Compare(this.ToString(), other.ToString());
        }

        public override string ToString()
        {
            return NameAppointment;
        }
    }

    [Serializable]
    [Description("Список должностей")]
    public class Appointments : List<Appointment>
    {
        public new void Add(Appointment item)
        {
            if (base.Exists(x => x.NameAppointment == item.NameAppointment))
                throw new Exception($"Должность \"{item.NameAppointment}\" уже существует!");
            base.Add(item);
            base.Sort();
        }

        public new void Remove(Appointment item)
        {
            base.Remove(item);
        }

    }
}
