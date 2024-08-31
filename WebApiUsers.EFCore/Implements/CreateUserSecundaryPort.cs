using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiUsers.Application.Ports.Secundary;
using WebApiUsers.Domain.Dtos;
using WebApiUsers.Domain.Models;
using WebApiUsers.EFCore.DataContext;

namespace WebApiUsers.EFCore.Implements
{
    public class CreateUserSecundaryPort : IUserInformationSecundaryPort
    {
        readonly WebApiContext Context;

        public CreateUserSecundaryPort(WebApiContext context)
        {
            Context = context;
        }

        public async Task<int> CreateUser(User user)
        {
            await Context.AddAsync(user);
            return Context.SaveChanges();
        }

        public async Task<(List<User>, int)> GetUsers(SearchDto search)
        {

            var resultado = Context.Users
                .Where(e => (string.IsNullOrEmpty(search.FirstName) || e.FirstName.Contains(search.FirstName))
                     && (string.IsNullOrEmpty(search.FirstLastName) || e.FirstLastName.Contains(search.FirstLastName)))
                     .ToListAsync();

            var PageCount = (search.PageNumber > 0 && search.PageSize > 0)
    ? (int)Math.Ceiling((double)resultado.Result.Count / search.PageSize) : 1;


            if (search.PageNumber > 0 && search.PageSize > 0)
            {
                return (resultado.Result.Skip((search.PageNumber - 1) * search.PageSize).Take(search.PageSize).ToList(), PageCount);
            }

            return (resultado.Result.ToList(), PageCount);

        }

        public async Task<User> GetUserById(int id)
        {
            var result = await Context.Users.FindAsync(id);
            return result;
        }

        public async Task<User> UpdateUser(User user)
        {
            Context.Users.Update(user);
            Context.SaveChanges();
            return user;
        }

        public async Task DeleteUser(int id)
        {
            var user = await Context.Users.FindAsync(id);
            Context.Users.Remove(user);
            await Context.SaveChangesAsync();
        }
    }
}
