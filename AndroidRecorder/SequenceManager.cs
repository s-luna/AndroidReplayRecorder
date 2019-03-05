using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace AndroidRecorder
{
    public class SequenceManager
    {
        private int m_timeLimit
        {
            get
            {
                return ApplicationConfig.Instance.GetRecordingTime();
            }
        }
        const int MILLISEC_MAGNIFICATION = 1000;

        private static SequenceManager ins;
        public static SequenceManager Instance {
            get {
                if (ins == null) {
                    ins = new SequenceManager();
                }
                return ins;
            }
        }

        public enum SequenceStatus {
            None,
            Idle,
            Recording,
        }

        public SequenceManager()
        {
            Initialization();
        }

        private SequenceStatus m_status;
        private Queue<RecordingManager> m_recordeQueue;
        private CancellationTokenSource m_cancellationTokenSource;

        private void Initialization()
        {
            m_status = SequenceStatus.Idle;
            m_recordeQueue = new Queue<RecordingManager>();
        }

        public SequenceStatus GetSequenceStatus() { return m_status; }

        public void Start()
        {
            DataManager.Instance.Initialize();
            Initialization();
            SetSequenceStatus(SequenceStatus.Recording);
            StartRecording();
        }

        public void Stop()
        {
            // キャッシュ全部消すやつ
            // loop終了処理
            StopRecording();
            SetSequenceStatus(SequenceStatus.Idle);
            while (m_recordeQueue.Count > 0) {
                RecordingManager recording = m_recordeQueue.Dequeue();
                recording.ExitRecording();
            }
            DataManager.Instance.Clean();
            // あとデータ全消し依頼を投げる
        }

        public void Export()
        {
            StopRecording();
            m_recordeQueue.Last().Interruption();
            Start();
            // データ移す指示するやつ？
            // それはRecordingManagerが終了待ちに発行するやつでは
        }

        private void SetSequenceStatus (SequenceStatus status)
        {
            m_status = status;
        }

        private void SetCancellatioToken() {
            m_cancellationTokenSource = new CancellationTokenSource();
        }

        private void StartRecording ()
        {
            RecordingManager recording = new RecordingManager();
            recording.StartRecording();
            m_recordeQueue.Enqueue(recording);
            SetCancellatioToken();
            Task.Run(() => AsyncRecordingWait(m_cancellationTokenSource.Token));
        }

        private void ExitCurrentRecording() {
            m_recordeQueue.Dequeue().ExitRecording();
        }

        private void StopRecording()
        {
            SetSequenceStatus(SequenceStatus.Idle);
            m_cancellationTokenSource.Cancel();
        }

        private async Task AsyncRecordingWait(CancellationToken token)
        {
            await Task.Delay(m_timeLimit * MILLISEC_MAGNIFICATION);
            if (token.IsCancellationRequested) {
                return;
            } else
            {
                NextLoop();
            }
        }

        private void NextLoop()
        {
            StartRecording();
            ExitCurrentRecording();
        }
    }
}
