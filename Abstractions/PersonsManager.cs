using Dapper;
using System;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace Abstractions
{
    public class PersonsManager : IPersonRepository
    {
        public Person Find(Guid id)
        {
            IDbConnection _dataSource;
            GetPersonByIdQuery getPersonByIdQuery = new GetPersonByIdQuery(_dataSource = new SQLiteConnection(SQLiteDataAccess.LoadConectionString()));
            return getPersonByIdQuery.Execute(id);
        }

        public void Insert(Person item)
        {
            IDbConnection _dataSource;
            AddPersonToDB addPersonToDB = new AddPersonToDB(_dataSource = new SQLiteConnection(SQLiteDataAccess.LoadConectionString()));
            addPersonToDB.Execute(item);
        }
    }

    public class GetPersonByIdQuery
    {
        private readonly IDbConnection _dataSource;

        public GetPersonByIdQuery(IDbConnection dataSource)
        {
            _dataSource = dataSource;
        }

        public Person Execute(Guid id)
        {
            string PersonGuid = id.ToString();
            var r = _dataSource.Query<PersonModel>("SELECT * FROM Persons WHERE PersonGuid = @PersonGuid", new { PersonGuid }).SingleOrDefault();
            return new Person(new Guid(r.PersonGuid), r.Name, r.BirthDay);          
        }
    }

    public class AddPersonToDB
    {
        private readonly IDbConnection _dataSource;

        public AddPersonToDB(IDbConnection dataSource)
        {
            _dataSource = dataSource;
        }

        public void Execute(Person person)
        {
            _dataSource.Execute("INSERT INTO Persons (PersonGuid, Name, BirthDay) values (@PersonGuid, @Name, @BirthDay)", 
                new
                {
                    PersonGuid = person.Id.ToString(),
                    Name = person.Name,
                    BirthDay = person.BirthDay
                });
        }
    }

}
