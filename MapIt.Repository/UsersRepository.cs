using System;
using System.Linq;
using MapIt.Data;

namespace MapIt.Repository
{
    public class UsersRepository : Repository<User>
    {
        public IQueryable<User> Search(long? userId, int? sex, int? country, int? active, DateTime? cDateFrom, DateTime? cDateTo, string keyword)
        {
            return base.Find(u =>
                    (userId.HasValue && userId.Value > 0 ? u.Id == userId.Value : true) &&
                    (sex.HasValue && sex.Value > 0 ? u.Sex == sex.Value : true) &&
                    (country.HasValue && country.Value > 0 ? u.CountryId == country.Value : true) &&
                    (active.HasValue ? (u.IsActive == (active.Value == 1 ? true : false)) : true) &&
                    ((cDateFrom.HasValue && cDateTo.HasValue) ? (u.AddedOn >= cDateFrom.Value && u.AddedOn < cDateTo.Value) : true) &&
                    ((!string.IsNullOrEmpty(keyword) ? u.FirstName.Replace(" ", string.Empty).ToLower().Contains(keyword.Replace(" ", string.Empty).ToLower()) : true) ||
                    (!string.IsNullOrEmpty(keyword) ? u.LastName.Replace(" ", string.Empty).ToLower().Contains(keyword.Replace(" ", string.Empty).ToLower()) : true) ||
                    (!string.IsNullOrEmpty(keyword) ? u.Phone.Replace(" ", string.Empty).ToLower().Contains(keyword.Replace(" ", string.Empty).ToLower()) : true) ||
                    (!string.IsNullOrEmpty(keyword) ? u.Email.Replace(" ", string.Empty).ToLower().Contains(keyword.Replace(" ", string.Empty).ToLower()) : true) ||
                    (!string.IsNullOrEmpty(keyword) ? u.UserName.Replace(" ", string.Empty).ToLower().Contains(keyword.Replace(" ", string.Empty).ToLower()) : true)));
        }

        public bool HasPendingMessages()
        {
            return base.Find(u => u.TechMessages.Any(m => !m.IsRead)).Count() > 0;
        }

        public User GetByPhone(string phone, long userId = 0)
        {
            if (string.IsNullOrEmpty(phone) && phone.Trim() != string.Empty)
                return null;

            return base.First(u => u.Phone == phone && u.Id != userId);
        }

        public User GetByEmail(string email, long userId = 0)
        {
            if (string.IsNullOrEmpty(email) && email.Trim() != string.Empty)
                return null;

            return base.First(u => u.Email == email && u.Id != userId);
        }

        public User GetByUserName(string username, long userId = 0)
        {
            if (string.IsNullOrEmpty(username) && username.Trim() != string.Empty)
                return null;

            return base.First(u => u.UserName == username && u.Id != userId);
        }

        public User Login(string userName, string password)
        {
            var userObj = base.First(u => u.UserName.Trim().ToLower() == userName.Trim().ToLower());
            if (userObj != null && userObj.Password == password)
            {
                return userObj;
            }

            return null;
        }
    }
}
