using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace SerializeExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            var alon = new Person
            {
                Name = new FullName("Alon", "Fliess"),
                Age = 43,
                Address = new Address
                {
                    Street = "Azmaut 10",
                    City = "Binyamina",
                    ZipCode = "30500",
                    PhoneNumber = "+972-546160636"
                }
            };

            var liat = new Person
            {
                Name = new FullName("Liat", "Fliess"),
                Age = 40,
                Address = alon.Address,
                Spouse = alon
            };
            alon.Spouse = liat;

            using (var buffer = new MemoryStream())
            {
                Serializer.Serialize(liat, buffer);
                buffer.Seek(0, SeekOrigin.Begin);
                var newLiat = Serializer.Deserialize<Person>(buffer);

                Trace.Assert(liat == newLiat, "Fail, object are not logicaly the same");
                Trace.Assert(newLiat != null && newLiat.Spouse != null && newLiat.Spouse.Address != null && newLiat.Address != null && ReferenceEquals(newLiat.Address, newLiat.Spouse.Address), "Address is not the same object");
                Trace.Assert(newLiat != null && newLiat.Spouse != null && newLiat.Spouse.Spouse != null && ReferenceEquals(liat.Spouse.Spouse, liat), "The new Liat and new Alon don't refer each other");
            }
        }
    }
}


