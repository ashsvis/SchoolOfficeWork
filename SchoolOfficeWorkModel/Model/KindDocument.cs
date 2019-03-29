using System;
using System.Collections.Generic;
using System.ComponentModel;
using ViewGenerator;

namespace Model
{
    [Serializable]
    [Description("Вид документа")]
    public class KindDocument : IComparable<KindDocument>
    {
        public Guid IdKindDocument { get; set; } = Guid.NewGuid();

        [Description("Наименование документа"), DataNotEmpty]
        public string NameKind { get; set; }

        [Description("Тип документа"), DataNotEmpty]
        public string TypeKind { get; set; }

        [Description("Шаблон документа")]
        public byte[] Template { get; set; } = new byte[] { };

        public int CompareTo(KindDocument other)
        {
            return string.Compare(this.ToString(), other.ToString());
        }

        public override string ToString()
        {
            return $"{NameKind} ({TypeKind})";
        }
    }

    [Serializable]
    [Description("Виды документов")]
    public class KindDocuments : List<KindDocument>
    {
        public new void Add(KindDocument item)
        {
            if (base.Exists(x => x.ToString() == item.ToString()))
                throw new Exception($"Вид документа \"{item}\" уже существует!");
            base.Add(item);
            base.Sort();
        }

        public new void Remove(KindDocument item)
        {
            base.Remove(item);
        }

    }
}
