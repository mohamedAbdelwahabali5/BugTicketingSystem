
using DataAccessLayer.Repositories;
using SchoolApp.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IUserBugRepository UserBugRepository { get; }
        public IUserRepository UserRepository { get; }
        public IBugRepository BugRepository { get; }
        public IProjectRepository ProjectRepository { get; }
        public IAttachmentRepository AttachmentRepository { get; }

        Task<int> SaveChangesAsync();
    }
}
