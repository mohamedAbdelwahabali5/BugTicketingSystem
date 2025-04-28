using SchoolApp.DAL.Repositories.Genaric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;
namespace DataAccessLayer.Repositories
{
    public interface IAttachmentRepository : IGenaricRepository<Attachment>
    {
        //Get Attachments for Bug: Retrieve all attachments for a bug.
        IEnumerable<Attachment> GetAttachmentsForBug(int bugId);


    }
}
