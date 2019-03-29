using System;
using System.Collections.Generic;
using System.ComponentModel;
using ViewGenerator;

namespace Model
{
    [Serializable]
    [Description("Подписчик")]
    public class Subscriber : IComparable<Subscriber>
    {
        public Guid IdSubscriber { get; set; }

        [Description("Подписчик"), DataNotEmpty]
        public string NameSubscriber { get; set; }

        [Description("Должность"), DataLookup("IdAppointment", "Appointments")]
        public Guid IdAppointment { get; set; }

        public int CompareTo(Subscriber other)
        {
            return string.Compare(this.ToString(), other.ToString());
        }

        public override string ToString()
        {
            return NameSubscriber;
        }
    }

    [Serializable]
    [Description("Список подписчиков")]
    public class Subscribers : List<Subscriber>
    {
        public new void Add(Subscriber item)
        {
            if (base.Exists(x => x.NameSubscriber == item.NameSubscriber))
                throw new Exception($"Подписчик \"{item.NameSubscriber}\" уже существует!");
            base.Add(item);
            base.Sort();
        }

        public new void Remove(Subscriber item)
        {
            base.Remove(item);
        }

    }
}
