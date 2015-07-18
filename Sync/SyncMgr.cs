using LoginLoginSync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Login.Sync
{
    /// <summary>
    /// 同步队列管理
    /// </summary>
    class SyncMgr
    {
        /// <summary>
        /// 同步队列
        /// </summary>
        protected static SyncMgr sInstance = new SyncMgr();

        /// <summary>
        /// 同步队列
        /// </summary>
        protected Queue<ISyncModel> m_aSyncQueue = new Queue<ISyncModel>();

        /// <summary>
        /// 锁文件
        /// </summary>
        protected object m_aLock = new object();

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <returns></returns>
        public static SyncMgr getInstance()
        {
            return sInstance;
        }

        public void Init() 
        {
        }

        /// <summary>
        /// 心跳函数
        /// </summary>
        public void HeartBeat()
        {
            // 处理队列
            HandleSync();
        }

        /// <summary>
        /// 添加到同步队列
        /// </summary>
        /// <param name="model"></param>
        public void PushSync(ISyncModel model)
        {
            lock (m_aLock)
            {
                if (model == null)
                    return;

                m_aSyncQueue.Enqueue(model);
            }
        }

        /// <summary>
        /// 获得一个同步队列
        /// </summary>
        /// <returns></returns>
        protected ISyncModel PopSync()
        {
            lock (m_aLock)
            {
                if (m_aSyncQueue.Count == 0)
                    return null;

                ISyncModel model = m_aSyncQueue.Dequeue();
                return model;
            }
        }

        /// <summary>
        /// 获得数量
        /// </summary>
        /// <returns></returns>
        protected int GetSyncCount()
        {
            lock (m_aLock)
            {
                return m_aSyncQueue.Count;
            }
        }

        /// <summary>
        /// 处理同步队列
        /// </summary>
        protected void HandleSync()
        {
            int nCount = GetSyncCount();
            for (int nIndex = 0; nIndex < nCount; ++nIndex)
            {
                ISyncModel model = PopSync();
                if (model == null)
                    continue;

                try
                {
                    // 处理这个Handle
                    model.Handle();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}
