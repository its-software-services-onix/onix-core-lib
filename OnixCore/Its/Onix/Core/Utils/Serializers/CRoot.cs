using System;
using System.Collections;

using Its.Onix.Core.Commons.Table;

namespace Its.Onix.Core.Utils.Serializers
{
	public class CRoot
    {
		private CTable param = null;
		private CTable data = null;

		public CRoot(CTable prm, CTable dta)
		{
            param = prm;
            data = dta;
		}
		
        public CTable Param
        {
            get
            {
                return (param);
            }

            set
            {
                param = value;
            }
        }

        public CTable Data
        {
            get
            {
                return (data);
            }

            set
            {
                data = value;
            }
        }
    }
}
