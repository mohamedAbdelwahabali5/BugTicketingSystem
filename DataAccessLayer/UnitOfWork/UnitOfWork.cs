
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccessLayer.Context;
using DataAccessLayer.Repositories;
using SchoolApp.DAL.Repositories;
namespace SchoolApp.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BTSDbContext _context;

        public IUserBugRepository UserBugRepository { get; }
        public IUserRepository UserRepository { get; }
        public IBugRepository BugRepository { get; }
        public IProjectRepository ProjectRepository { get; }
        public IAttachmentRepository AttachmentRepository { get; }

        public UnitOfWork(
            IUserBugRepository userBugRepo,
            IUserRepository userRepo,
            IBugRepository bugRepo,
            IProjectRepository projectRepo,
            IAttachmentRepository attachmentRepo,

            BTSDbContext context)
        {
            UserBugRepository = userBugRepo;
            UserRepository = userRepo;
            BugRepository = bugRepo;
            ProjectRepository = projectRepo;
            AttachmentRepository = attachmentRepo;

            _context = context;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
