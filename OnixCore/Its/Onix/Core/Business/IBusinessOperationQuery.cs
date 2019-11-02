using System.Collections.Generic;
using Its.Onix.Core.Commons.Table;

namespace Its.Onix.Core.Business
{
	public interface IBusinessOperationQuery<T> : IBusinessOperation where T : class
	{
        IEnumerable<T> Apply(T dat, CTable param);
    }
}
