using App.Common.Enums;
using App.Web.Data.Entity;
using App.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;

        public SeedDb(
            DataContext dataContext,
            IUserHelper userHelper)
        {
            _dataContext = dataContext;
            _userHelper = userHelper;
        }
        public async Task SeedAsync()
        {
            await _dataContext.Database.EnsureCreatedAsync();
            await CheckRolesAsync();
            await CheckUserAsync("1010", "Usuario", "Administrador", "admin@yopmail.com", "350 634 2747", "Calle Luna Calle Sol", UserType.Admin);
            UserEntity user1 = await CheckUserAsync("3030", "Usuario", "De Registro", "register@yopmail.com", "350 634 2747", "Calle Luna Calle Sol", UserType.Register);
            UserEntity user2 = await CheckUserAsync("5050", "Camila", "Cifuentes", "camila@yopmail.com", "350 634 2747", "Calle Luna Calle Sol", UserType.Register);
            UserEntity user3 = await CheckUserAsync("6060", "Sandra", "Usuga", "sandra@yopmail.com", "350 634 2747", "Calle Luna Calle Sol", UserType.Cashier);
            UserEntity user4 = await CheckUserAsync("7070", "Lisa", "Marin", "luisa@yopmail.com", "350 634 2747", "Calle Luna Calle Sol", UserType.Cashier);
            
            await CheckItemTypeAsync();
        }

     

        private async Task<UserEntity> CheckUserAsync(
            string document,
            string firstName,
            string lastName,
            string email,
            string phone,
            string address,
            UserType userType)
        {
            var user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new UserEntity
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    UserType = userType
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
            }

            return user;
        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.Register.ToString());
            await _userHelper.CheckRoleAsync(UserType.Cashier.ToString());
        }  
        private async Task CheckItemTypeAsync()
        {
            List<ItemTypeEntity> types = new List<ItemTypeEntity> {
                new ItemTypeEntity { Name = "Accesory" },
                new ItemTypeEntity { Name = "Smartphone" },
                new ItemTypeEntity { Name = "Table" }
                };
           _dataContext.ItemTypes.AddRange(types);
            await _dataContext.SaveChangesAsync();
        }
    }
}
