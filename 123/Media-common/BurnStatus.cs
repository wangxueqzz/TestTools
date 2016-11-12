
using Macro.Common.Media.IMAPI2;

namespace Macro.Common.Media
{
    public class BurnStatus
    {

        /// <summary>
        /// 刻录机ID
        /// </summary>
        public string UniqueRecorderId;

        /// <summary>
        /// 状态信息
        /// </summary>
        public string StatusMessage;

        /// <summary>
        /// 当前工作类型
        /// </summary>
        public BURN_MEDIA_TASK Task;

        // IDiscFormat2DataEventArgs Interface
        /// <summary>
        /// Elapsed time in seconds
        /// </summary>
        public long ElapsedTime;

        /// <summary>
        ///  Remaining time in seconds
        /// </summary>
        public long RemainingTime;

        /// <summary>
        ///  total estimated time in seconds
        /// </summary>
        public long TotalTime;


        // IWriteEngine2EventArgs Interface
        /// <summary>
        /// 当前写的操作
        /// </summary>
        public IMAPI_FORMAT2_DATA_WRITE_ACTION CurrentAction;

        /// <summary>
        ///  the starting lba of the current operation
        /// </summary>
        public long StartLba;

        /// <summary>
        ///  the total sectors to write in the current operation
        /// </summary>
        public long SectorCount;

        /// <summary>
        /// the last read lba address
        /// </summary>
        public long LastReadLba;

        /// <summary>
        /// the last written lba address
        /// </summary>
        public long LastWrittenLba;

        /// <summary>
        ///  total size of the system buffer
        /// </summary>
        public long TotalSystemBuffer;

        /// <summary>
        /// size of used system buffer
        /// </summary>
        public long UsedSystemBuffer;

        /// <summary>
        ///  size of the free system buffer
        /// </summary>
        public long FreeSystemBuffer;
    }
}
