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

        public virtual int CompanyCount
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual string Connection
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        internal virtual bool Ping()
        {
            throw new NotImplementedException();
        }

        protected virtual string GetRepositoryType()
        {
            throw new NotImplementedException();
        }

        public string RepositoryType
        {
            get
            {
                return GetRepositoryType();
            }
        }

        public virtual void AddBranches(string[] branchCodes)
        {
            foreach (string branchCode in branchCodes) CreateBranch(branchCode);
        }

        internal virtual bool CreateBranch(string branchCode)
        {
            throw new NotImplementedException();
        }
    }
}
