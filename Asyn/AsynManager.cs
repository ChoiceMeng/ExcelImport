using Asyn.Asyn.Model;
using ExcelImport.Asyn.Model;
using System;
using System.Collections.Generic;
using System.Threading;

/// <summary>
/// 异步管理
/// </summary>
public class AsynManager
{
    /// <summary>
    /// 异步管理实例
    /// </summary>
    protected static AsynManager sAsynManager = new AsynManager();

    /// <summary>
    /// 异步操作队列
    /// </summary>
    protected List<AsynQueue> mAsynQueue = new List<AsynQueue>();

    /// <summary>
    /// 不是队列的类型
    /// </summary>
    protected List<AsynType> mNoOrderQueue = new List<AsynType>();

    /// <summary>
    /// 获得实例
    /// </summary>
    /// <returns></returns>
    public static AsynManager getInstance()
    {
        return sAsynManager;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        // 初始化所有队列
        for (AsynType mType = AsynType.AT_NoOrder; mType < AsynType.AT_Max; ++mType)
        {
            this.mAsynQueue.Add(new AsynQueue(mType));
        }

        this.mNoOrderQueue.Add(AsynType.AT_NoOrder);
    }

    /// <summary>
    /// 心跳
    /// </summary>
    public void HeartBeat()
    {
        // 先处理已经结束的
        HandleDoneOperator();

        // 处理不是队列的消息
        HandleOperator();
    }

    /// <summary>
    /// 结束
    /// </summary>
    public void Stop()
    {

    }

    /// <summary>
    /// 添加操作
    /// </summary>
    /// <param name="mOperator"></param>
    /// <param name="mAsynType"></param>
    public void AddOperator(AsynOperator mOperator, AsynType mAsynType = AsynType.AT_NoOrder)
    {
        mOperator.AsynType = mAsynType;
        if (mOperator.AsynType < AsynType.AT_NoOrder || mOperator.AsynType >= AsynType.AT_Max)
            return;

        this.mAsynQueue[(int)mOperator.AsynType].AddOpeator(mOperator);
    }

    /// <summary>
    /// 异步操作
    /// </summary>
    protected void HandleDoneOperator()
    {
        foreach (AsynQueue mQueue in this.mAsynQueue)
        {
            int nCount = mQueue.DoneOperatorCount;
            for (int nIndex = 0; nIndex < nCount; ++nIndex)
            {
                AsynOperator mOperator = mQueue.PopDoneOperator();
                if (mOperator == null)
                    continue;

                // 结束
                try
                {
                    // 执行结束操作
                    mOperator.Finish();
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            mQueue.ExeQueueOperator = false;
        }
    }

    /// <summary>
    /// 是否是不是队列的操作类型
    /// </summary>
    /// <param name="mType"></param>
    /// <returns></returns>
    protected bool IsNoOrderQueue(AsynType mType)
    {
        return this.mNoOrderQueue.Contains(mType);
    }

    /// <summary>
    /// 执行操作
    /// </summary>
    protected void HandleOperator()
    {
        foreach (AsynQueue mQueue in this.mAsynQueue)
        {
            int nCount = mQueue.OperatorCount;
            for (int nIndex = 0; nIndex < nCount; ++nIndex)
            {
                // 只有没有队形的异步才能一次执行多个
                if (mQueue.ExeQueueOperator)
                {
                    if (!IsNoOrderQueue(mQueue.AsynType))
                        break;
                }

                AsynOperator mOperator = mQueue.PopOperator();
                if (mOperator == null)
                    continue;

                mQueue.ExeQueueOperator = true;

                // 结束
                try
                {
                    ExecuteOperator(mOperator);
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }

    /// <summary>
    /// 执行操作
    /// </summary>
    /// <param name="mOperator"></param>
    protected void ExecuteOperator(AsynOperator mOperator)
    {
        // 执行开始操作
        mOperator.Start();

        // 扔到任务队列中
        ThreadPool.QueueUserWorkItem(new WaitCallback(DoOperator), mOperator);
    }

    /// <summary>
    /// 执行操作
    /// </summary>
    /// <param name="mObj"></param>
    protected void DoOperator(object mObj)
    {
        if (!(mObj is AsynOperator))
            return;

        AsynOperator mOperator = (AsynOperator)mObj;
        if (mOperator == null)
            return;

        // 开始执行
        mOperator.Execute();

        // 添加到已经完成的队列中
        this.mAsynQueue[(int)mOperator.AsynType].AddDoneOpeator(mOperator);
    }
}
