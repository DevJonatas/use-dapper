using dapper.DAL.Interfaces;
using dapper.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace dapper.DAL
{
    public class PersonDAL : IPersonDAL
    {
        private readonly IConfiguration _configuration;
        public string ConnectionString { get; private set; }

        public PersonDAL(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("Connection");
        }

        public bool Delete(long id)
        {
            using (var con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();
                    var query = $"DELETE Person WHERE Id = @Id";
                    con.Execute(query, new { Id = id});
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    con.Close();
                }

                return true;
            }
        }

        public bool EditPerson(Person person)
        {
            using (var con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();
                    var query = $"UPDATE Person SET FullName = @Fullname, CellPhone = @CellPhone, Email = @Email WHERE Id = @Id";
                    con.Execute(query, new
                    {
                        FullName = person.FullName,
                        CellPhone = person.CellPhone,
                        Email = person.Email,
                        Id = person.Id
                    });
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    con.Close();
                }

                return true;
            }
        }

        public List<Person> GetAllPerson()
        {
            var person = new List<Person>();
            using (var con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();
                    var query = "select Id, Fullname, Gender, SocialSecurityNumber, CellPhone, Email from Person";

                    person = con.Query<Person>(query).ToList();
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    con.Close();
                }

                return person;
            }
        }

        public bool NewPerson(Person person)
        {
            using (var con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();
                    var query = "INSERT INTO Person (FullName, Gender, SocialSecurityNumber, CellPhone, Email) VALUES (@Fullname, @Gender, @SocialSecurityNumber, @CellPhone, @Email)";
                    con.Execute(query, person);
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    con.Close();
                }
                return true;
            }
        }
    }
}
