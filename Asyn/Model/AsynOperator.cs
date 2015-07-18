using ExcelImport.Asyn.Model;
using System;

namespace Asyn.Asyn.Model
{
    /// <summary>
    /// 异步操作的基类
    /// </summary>
    public abstract class AsynOperator
    {
        /// <summary>
        /// 操作状态
        /// </summary>
        public enum OperatorState
        {
            OperatorState_Start,                    // 开始执行
            OperatorState_Execute,                  // 正在执行
            OperatorState_Finish,                   // 执行结束
            OperatorState_UnKnown,                  // 未知
        }

        /// <summary>
        /// 执行状态
        /// </summary>
        protected OperatorState mState = OperatorState.OperatorState_UnKnown;

        /// <summary>
        /// 是否是队列的
        /// </summary>
        protected bool bIsQueue = true;

        /// <summary>
        /// 是否在队列中
        /// </summary>
        public bool InQueue
        {
            get { return bIsQueue; }
            set { bIsQueue = value; }
        }

        /// <summary>
        /// 异步类型
        /// </summary>
        protected AsynType mAsynType = AsynType.AT_NoOrder;

        /// <summary>
        /// 异步类型
        /// </summary>
        public AsynType AsynType
        {
            get { return mAsynType; }
            set { mAsynType = value; }
        }

        /// <summary>
        /// 开始执行
        /// </summary>
        public void Start()
        {
            mState = OperatorState.OperatorState_Start;
            this.OnStart();
        }

        /// <summary>
        /// 开始时调用(主线程)
        /// </summary>
        protected abstract void OnStart();

        /// <summary>
        /// 开始执行
        /// </summary>
        public void Execute()
        {
            mState = OperatorState.OperatorState_Execute;
            try
            {
                // 执行操作
                this.OnExecute();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 执行的时候
        /// </summary>
        protected abstract void OnExecute();

        /// <summary>
        /// 结束
        /// </summary>
        public void Finish()
        {
            // 结束
            mState = OperatorState.OperatorState_Finish;
            this.OnFinish();
        }

        /// <summary>
        /// 结束时调用
        /// </summary>
        protected abstract void OnFinish();
    }
}
