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
    public interface IProjectBusiness
    {
        void Save(ProjectRequest user);
        IEnumerable<ProjectRequest> GetAll();
        string FormatName(User manager);
        void Delete(int id);
    }
    public class ProjectBusiness  : IProjectBusiness
    {

        readonly IRepository<Project> _projectRepository;
        readonly IRepository<User> _userRepository;
        readonly IRepository<ProjectTask> _taskRepository;
        public ProjectBusiness(IRepository<Project> projectRepository,
            IRepository<User> userRepository,
            IRepository<ProjectTask> taskRepository)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _taskRepository = taskRepository;
        }

        public void Delete(int id)
        {
            _projectRepository.Delete(id);
        }

        public IEnumerable<ProjectRequest> GetAll()
        {
            var projects = new List<ProjectRequest>();
            var entities = _projectRepository.GetAll();

            entities.ToList().ForEach(u => projects.Add(ToProjectViewModel(u)));

            return projects;
        }

        public void Save(ProjectRequest model)
        {
            var entity = _projectRepository.GetById(model.ProjectId);
            if (entity != null)
            {
                entity.Title = model.ProjectName;
                entity.StartDate = model.StartDate;
                entity.EndDate = model.EndDate;
                entity.Priority = model.Priority;
                _projectRepository.Update(entity);
            }
            else
            {
                entity = ToProjectEntity(model);
                _projectRepository.Insert(entity);
            }

            model.ProjectId = entity.ProjectId;
            UpdateUser(model);
        }

        private void UpdateUser(ProjectRequest model)
        {
            if (model.ProjectId == 0) return;

            var user = _userRepository.GetById(model.UserId);
            if (user != null)
            {
                user.ProjectId = model.ProjectId;
                _userRepository.Update(user);
            }
        }

        private ProjectRequest ToProjectViewModel(Project project)
        {
            var tasks = _taskRepository.GetAll().Where(p => p.ProjectId == project.ProjectId);
            var taskCount = tasks.Count();

            var isAllTasksCompleted = tasks
                .All(s => !string.IsNullOrEmpty(s.Status) &&
                string.Equals("Yes", s.Status, StringComparison.InvariantCultureIgnoreCase));

            var manager = _userRepository.GetAll().FirstOrDefault(p => p.ProjectId == project.ProjectId);
            var managerName = "";
            var managerId = 0;

            if (manager != null)
            {
                managerName = FormatName(manager);
                managerId = manager.UserId;
            }

            return new ProjectRequest
            {
                ProjectId = project.ProjectId,
                ProjectName = project.Title,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Priority = project.Priority,
                Manager = managerName,
                UserId = managerId,
                Completed = isAllTasksCompleted ? "Yes" : "No",
                NumberOfTasks = taskCount
            };
        }

        public string FormatName(User manager)
        {
            if (manager == null) return string.Empty;
            return string.Format("{0} {1}", manager.FirstName, manager.LastName);

        }
        private Project ToProjectEntity(ProjectRequest model)
        {
            return new Project
            {
                ProjectId = model.ProjectId,
                Title = model.ProjectName,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Priority = model.Priority
            };
        }

    }
}

