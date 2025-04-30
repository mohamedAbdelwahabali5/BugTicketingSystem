using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;


namespace DataAccessLayer.Repositories
{
    public class AttachmentRepository : GeneraicRepository<Attachment>, IAttachmentRepository
    {
        private readonly BTSDbContext _context;
        public AttachmentRepository(BTSDbContext context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<Attachment> GetAttachmentsForBug(int bugId)
        {
            return _context.Attachments.Where(a => a.BugId == bugId);
            
        }
    }
}
