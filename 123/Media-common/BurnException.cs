using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Macro.Common.Media
{
    /// <summary>
    /// 异常类
    /// </summary>
    public class BurnException : COMException
    { 

        /// <summary>
        /// 刻录错误信息
        /// </summary>
        public string ErrorMessage
        {
            get {

                string errorCodeMessage = ImapiReturnValues.GetName(this.ErrorCode);
                string error = string.Format("Message:{0} \t 具体错误:{1}", base.Message, errorCodeMessage);

                return error;
            }
        }
    }

   

}
