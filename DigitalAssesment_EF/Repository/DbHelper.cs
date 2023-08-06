using System;
using DigitalAssesment_EF.Models;

namespace DigitalAssesment_EF.Repository
{
	public class DbHelper
	{
		private UserDbContext context;
        public DbHelper(UserDbContext con)
		{
			context = con;
		}

        public List<UserModel> GetUsers()
        {
            List<UserModel> response = new List<UserModel>();
            var dataList = context.Users.ToList();
            dataList.ForEach(row => response.Add(new UserModel()
            {
                id = row.id,
                username = row.username,
                password = row.password
            }));
            return response;
        }
        public void saveUser(UserModel user)
        {
            UserClass newUser = new UserClass();
            newUser.id = user.id;
            newUser.username = user.username;
            newUser.password = user.password;
            context.Users.Add(newUser);
            context.SaveChanges();
        }
        public UserModel checkUser(string username)
        {
            var data = context.Users.Where(d => d.username.Equals(username)).FirstOrDefault();
            UserModel userModel = new UserModel();

                userModel.id = data.id;
                userModel.username = data.username;
                userModel.password = data.password;

            return userModel;
        }
        public bool isValidUser(string username ) 
        {
            try
            {
                var data = context.Users.Where(d => d.username.Equals(username)).FirstOrDefault();
                if(data != null)
                {
                    return true;
                }
            }catch(Exception ex)
            {
                return false;
            }
            return false;
        }
        public UserModel AuthenticateUSer(string username,string password)
        {
            var data = context.Users.Where(d => d.username.Equals(username) && d.password.Equals(password)).FirstOrDefault();
            UserModel userModel = new UserModel();

            userModel.id = data.id;
            userModel.username = data.username;
            userModel.password = data.password;

            return userModel;
        }
        public UserModel getUserById(Guid id)
        {
            var data = context.Users.Where(d => d.id.Equals(id)).FirstOrDefault();
            UserModel userModel = new UserModel();
            userModel.id = data.id;
            userModel.username = data.username;
            userModel.password = data.password;

            return userModel;
        }
    }

	
}

