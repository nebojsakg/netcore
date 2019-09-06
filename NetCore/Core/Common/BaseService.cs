using Domain;

namespace Core.Common
{
    public abstract class BaseService
    {
        protected DatabaseContext ctx;

        protected BaseService(DatabaseContext ctx)
        {
            this.ctx = ctx;
        }
    }
}
