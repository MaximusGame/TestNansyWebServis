using Abstractions;
using Nancy;
using Nancy.Extensions;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;

namespace CreatePersonServiseNancy
{
    public class CreatePerson : NancyModule
    {
        public CreatePerson()
        {
            Post("/api/v1/persons/", args =>
            {
                try
                {
                    string text = Context.Request.Body.AsString();
                    LogConteiner.mylog.PostRequest(text);
                    PersonModel dto = this.Bind<PersonModel>();
                    return Root(dto);
                }
                catch
                {
                    return 500;
                }
            });
        }

        private Response Root(PersonModel dto)
        {
            //PersonModel dto = this.Bind<PersonModel>();

            if (dto == null)
            {
                return HttpStatusCode.BadRequest;
            }
            else
            {
               Guid g = Guid.NewGuid();
               CreateBase creat = new PersonCreater(g, dto.Name, dto.BirthDay);
               PersonBase pers = creat.Create();
               if (pers == null)
               {
                    return HttpStatusCode.UnprocessableEntity;
               }
               else
               {
  
                    AddPerson((Person)pers);
                    var values = new Dictionary<string, string>
                    {
                       { "Location", "http://localhost:1234/api/v1/persons/" },
                       { "id", g.ToString()}
                    };
                    var response = new Response { StatusCode = HttpStatusCode.Created, Headers = values };
                    return response;
               }
            }
                       
        }

        public void AddPerson(Person person)
        {
            IPersonRepository repo = new PersonsManager();
            repo.Insert(person);
        }

    }
}
