using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dajiangspider
{
    public class FlySafe
    {
        private ForbiddenArea _forbiddenAreasData;
        public ForbiddenArea ForbiddenAreasData
        {
            get { return _forbiddenAreasData; }
            set { _forbiddenAreasData = value; }
        }

        #region singleton
        private static readonly object lockObject = new object();

        /// <summary>
        /// 线程锁
        /// </summary>
        private volatile static FlySafe _instance = null;

        public static FlySafe CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockObject)
                {
                    if (_instance == null)
                    {
                        _instance = new FlySafe();
                    }
                }
            }

            return _instance;
        }

        public FlySafe()
        {
            _forbiddenAreasData = new ForbiddenArea();
            //Initialize();
        }
        #endregion

        private void Initialize()
        {
            //_forbiddenAreasData
        }
    }
}
