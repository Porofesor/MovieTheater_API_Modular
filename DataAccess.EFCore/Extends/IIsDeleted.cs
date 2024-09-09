using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EFCore.Extends
{
    public interface IIsDeleted
    {
        bool IsDeleted { get; set; }
    }
}
