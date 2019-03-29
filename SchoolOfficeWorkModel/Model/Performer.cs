using System;
using System.Collections.Generic;
using System.ComponentModel;
using ViewGenerator;

namespace Model
{
    [Serializable]
    [Description("Исполнитель")]
    public class Performer : IComparable<Performer>
    {
        public Guid IdPerformer { get; set; }

        [Description("Исполнитель"), DataNotEmpty]
        public string NamePerformer { get; set; }

        [Description("Должность"), DataLookup("IdAppointment", "Appointments")]
        public Guid IdAppointment { get; set; }

        public int CompareTo(Performer other)
        {
            return string.Compare(this.ToString(), other.ToString());
        }

        public override string ToString()
        {
            return NamePerformer;
        }
    }

    [Serializable]
    [Description("Список исполнителей")]
    public class Performers : List<Performer>
    {
        public new void Add(Performer item)
        {
            if (base.Exists(x => x.NamePerformer == item.NamePerformer))
                throw new Exception($"Исполнитель \"{item.NamePerformer}\" уже существует!");
            base.Add(item);
            base.Sort();
        }

        public new void Remove(Performer item)
        {
            base.Remove(item);
        }

    }
}
