using dapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dapper.DAL.Interfaces
{
    public interface IPersonDAL
    {
        List<Person> GetAllPerson();
        bool NewPerson(Person person);
        bool EditPerson(Person person);
        bool Delete(long id);
    }
}
