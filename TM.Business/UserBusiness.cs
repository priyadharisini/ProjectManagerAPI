using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Business.Request;
using TM.Data;
using TM.Entities;

namespace TM.Business
{
    public interface IUserBusiness
    {
        void Save(UserRequest user);
        IEnumerable<UserRequest> GetAll();
        UserRequest GetById(int id);
        void Delete(int id);
    }

    public class UserBusiness : IUserBusiness
    {
        readonly IRepository<User> _userRepository;

        public UserBusiness(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public void Delete(int id)
        {
            _userRepository.Delete(id);
        }

        public IEnumerable<UserRequest> GetAll()
        {
            var users = new List<UserRequest>();
            var entities = _userRepository.GetAll().OrderByDescending(x => x.UserId);

            entities.ToList().ForEach(u => users.Add(ToUserViewModel(u)));

            return users;
        }

        public void Save(UserRequest user)
        {
            var entity = ToUserEntity(user);
            if (user.UserId == 0)
            {
                _userRepository.Insert(entity);
            }
            else
            {
                _userRepository.Update(entity);
            }
        }

        public UserRequest GetById(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null) return new UserRequest();
            return ToUserViewModel(user);
        }

        private UserRequest ToUserViewModel(User user)
        {
            return new UserRequest
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmployeeId = user.EmployeeId,
                ProjectId = user.ProjectId.GetValueOrDefault(),
                TaskId = user.TaskId.GetValueOrDefault()
            };
        }

        private User ToUserEntity(UserRequest userRequest)
        {
            return new User
            {
                UserId = userRequest.UserId,
                FirstName = userRequest.FirstName,
                LastName = userRequest.LastName,
                EmployeeId = userRequest.EmployeeId
            };
        }


    }
}
