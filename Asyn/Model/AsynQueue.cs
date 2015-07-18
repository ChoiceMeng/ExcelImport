using System.Collections.Generic;
using Asyn.Asyn.Model;
using ExcelImport.Asyn.Model;

/// <summary>
/// 异步操作队列
/// </summary>
public class AsynQueue
{
    /// <summary>
    /// 异步类型
    /// </summary>
    private AsynType mAsynType = AsynType.AT_NoOrder;

    /// <summary>
    /// 按队列执行
    /// </summary>
    protected List<AsynOperator> mQueueOperators = new List<AsynOperator>();

    /// <summary>
    /// 锁
    /// </summary>
    protected object mQueueOperatorLock = new object();

    /// <summary>
    /// 是否在执行队列
    /// </summary>
    protected bool bExeQueueOperator = false;

    /// <summary>
    /// 执行结束的队列
    /// </summary>
    protected List<AsynOperator> mDoneOperators = new List<AsynOperator>();

    /// <summary>
    /// 锁
    /// </summary>
    protected object mDoneOperatorLock = new object();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="mType"></param>
    public AsynQueue(AsynType mType)
    {
        this.mAsynType = mType;
    }

    /// <summary>
    /// 操作的数量
    /// </summary>
    public int OperatorCount
    {
        get
        {
            lock (mQueueOperatorLock)
            {
                return this.mQueueOperators.Count;
            }
        }
    }

    /// <summary>
    /// 添加进入操作还未执行队列
    /// </summary>
    /// <param name="mOperator"></param>
    public void AddOpeator(AsynOperator mOperator)
    {
        lock (mQueueOperatorLock)
        {
            this.mQueueOperators.Add(mOperator);
        }
    }

    /// <summary>
    /// 返回一个队列
    /// </summary>
    /// <returns></returns>
    public AsynOperator PopOperator()
    {
        lock (mQueueOperatorLock)
        {
            if (this.mQueueOperators.Count <= 0)
                return null;

            AsynOperator mOperator = this.mQueueOperators[0];
            this.mQueueOperators.RemoveAt(0);

            return mOperator;
        }
    }

    /// <summary>
    /// 是否正在操作
    /// </summary>
    public bool ExeQueueOperator
    {
        get { return this.bExeQueueOperator; }
        set { bExeQueueOperator = value; }
    }

    /// <summary>
    /// 操作的数量
    /// </summary>
    public int DoneOperatorCount
    {
        get
        {
            lock (mDoneOperatorLock)
            {
                return this.mDoneOperators.Count;
            }
        }
    }

    /// <summary>
    /// 添加进入操作已经执行完成的队列
    /// </summary>
    /// <param name="mOperator"></param>
    public void AddDoneOpeator(AsynOperator mOperator)
    {
        lock (mDoneOperatorLock)
        {
            this.mDoneOperators.Add(mOperator);
        }
    }

    /// <summary>
    /// 返回一个队列
    /// </summary>
    /// <returns></returns>
    public AsynOperator PopDoneOperator()
    {
        lock (mDoneOperatorLock)
        {
            if (this.mDoneOperators.Count <= 0)
                return null;

            AsynOperator mOperator = this.mDoneOperators[0];
            this.mDoneOperators.RemoveAt(0);

            return mOperator;
        }
    }

    /// <summary>
    /// 异步类型
    /// </summary>
    public AsynType AsynType
    {
        get { return this.mAsynType; }
    }
}
