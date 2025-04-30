
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IUnitOfWork
    {
        public IUserBugRepository UserBugRepository { get; }
        public IUserRepository UserRepository { get; }
        public IBugRepository BugRepository { get; }
        public IProjectRepository ProjectRepository { get; }
        public IAttachmentRepository AttachmentRepository { get; }

        public IUserBugRepository User_BugRepository { get; }

        Task<int> SaveChangesAsync();
    }
}
