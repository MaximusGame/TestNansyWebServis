using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions
{
    public abstract class PersonBase
    {

    }

    public class Person : PersonBase
    {
        public Guid Id;
        /// <summary>
        /// Person Name
        /// </summary>
        public string Name;

        /// <summary>
        /// Person Date Birth day
        /// </summary>
        public string BirthDay;

        public Person(Guid id, string name, string birth_day)
        {
            // if name = null
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            //if birth day = null
            if (string.IsNullOrEmpty(birth_day))
            {
                throw new ArgumentNullException(nameof(birth_day));
            }
            Id = id;
            Name = name;
            BirthDay = birth_day;
        }
    }

    public abstract class CreateBase
    {
        abstract public PersonBase Create();
    }

    public class PersonCreater : CreateBase
    {
        private readonly Guid Id;
        private readonly string Name;
        private readonly string BirthDay;
        public PersonCreater(Guid id, string name, string birthday)
        {
            Id = id;
            Name = name;
            BirthDay = birthday;
        }
        public override PersonBase Create()
        {
            DateTime date = DateTime.Now;
            DateTime BirthDay_date = Convert.ToDateTime(BirthDay);
            if (((date.Year - BirthDay_date.Year) > 120) || (string.IsNullOrEmpty(Name)))
            {
                return null;
            }
            else
            {
                return new Person(Id, Name, BirthDay);
            }
        }
    }
}
