using System;
using System.Collections.Generic;
using System.ComponentModel;
using ViewGenerator;

namespace Model
{
    [Serializable]
    [Description("Подразделение")]
    public class Partition: IComparable<Partition>
    {
        public Guid IdPartition { get; set; } = Guid.NewGuid();
        [Description("Название подразделения")]
        [DataNotEmpty]
        public string NamePartition { get; set; }

        public int CompareTo(Partition other)
        {
            return string.Compare(this.NamePartition, other.NamePartition);
        }

        public override string ToString()
        {
            return NamePartition;
        }
    }

    [Serializable]
    [Description("Список подразделений")]
    public class Partitions : List<Partition>
    {
        public new void Add(Partition item)
        {
            if (base.Exists(x => x.NamePartition == item.NamePartition))
                throw new Exception($"Подразделение \"{item.NamePartition}\" уже существует!");
            base.Add(item);
            base.Sort();
        }

        public new void Remove(Partition item)
        {
            base.Remove(item);
        }
    }
}
