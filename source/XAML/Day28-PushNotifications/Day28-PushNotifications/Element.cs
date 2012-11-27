using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Day28_PushNotifications
{
    class Element
    {
        public int Id { get; set; }
        public double AtomicWeight { get; set; }
        public int AtomicNumber { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string State { get; set; }

        [DataMember(Name = "channel")]
        public string Channel { get; set; }
    }
}
