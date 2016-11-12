using System;
using System.Collections.Generic;
using System.Text;

namespace Macro.Common.Media
{
    public interface IBurnMediaData
    {
        /// <summary>
        /// 设置要刻录的文件的信息
        /// </summary>
        string Path
        {
            get;
            set;
        }

        /// <summary>
        /// 设置此刻录文件的类型
        /// </summary>
        MediaType Type
        {
            get;
            set;
        }
    }
}
