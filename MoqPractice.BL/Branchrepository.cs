using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoqPractice.BL
{
    public class BranchRepository
    {
        public virtual string[] GetAllBranches()
        {
            throw new NotImplementedException();
        }

        public virtual string[] GetBranchesForCompany(int companyId, int branchType)
        {
            throw new NotImplementedException();
        }

        public virtual string[] GetBranchPage(int pageNumber, int pageSize, out int count)
        {
            throw new NotImplementedException();
        }
    }
}
