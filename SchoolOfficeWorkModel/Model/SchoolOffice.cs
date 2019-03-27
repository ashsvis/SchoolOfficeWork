using System;

namespace Model
{
    [Serializable]
    public class SchoolOffice
    {
        public Partitions Partitions { get; set; } = new Partitions();
        public Appointments Appointments { get; set; } = new Appointments();
        public Employees Employees { get; set; } = new Employees();
    }
}
