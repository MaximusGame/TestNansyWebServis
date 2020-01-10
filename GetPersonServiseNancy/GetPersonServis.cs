using Abstractions;
using Nancy;
using System;
using System.Dynamic;

namespace GetPersonServiseNancy
{
    public class GetPersonServis : NancyModule
    {

        public GetPersonServis()
        {
            Get("/api/v1/persons/{id}", args => 
            {
                try
                {
                    LogConteiner.mylog.GetRequest((string)args.id);
                    dynamic model = new ExpandoObject();
                    return Response.AsJson((object)GetPerson(args.id, model));
                }
                catch
                {
                    return 500;
                }
            });
        }

        public dynamic GetPerson(dynamic id, dynamic obj)
        {
            IPersonRepository repo = new PersonsManager();
            Person person = repo.Find((Guid)id);
            obj.Name = person.Name;
            obj.BirthDay = person.BirthDay;
            return obj;
        }
    }


  
}
