using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


    public class UpdateService : IDisposable
    {
        readonly CancellationTokenSource cts = new CancellationTokenSource();

        private int Fps=1;
        public int Timeout { get { return Fps>0?1000/Fps:1000;} set { _=value!=0? Fps = 1000/value : 1000; } }

        public UpdateService()
        {
            var token = cts.Token;
            Task.Run(() => Update(token), token);
        }

        //public event Func<string, int, Action> Notify; 
        public event Action Notify;

        public void Dispose()
        {
            cts.Cancel();
        }

        private async void Update(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            while (true)
            {
                if (token.IsCancellationRequested) token.ThrowIfCancellationRequested();
                Notify?.Invoke();
                await Task.Delay(Timeout);
            }
        }
    }

    public class UpdateServiceThread : IDisposable
    {
        Thread thread = null;
        volatile bool STOP = false;
        readonly int timeout = 1000;

        public UpdateServiceThread()
        {

            thread = new Thread(new ThreadStart(UpdateThread));
            thread.Start();
        }

        public void UpdateThread()
        {
            while (!STOP)
            {
                if (Notify != null)
                {
                    Notify.Invoke(null, 0);
                }
                Thread.Sleep(timeout);
            }
        }

        public event Func<string, int, Action> Notify;

        public void Dispose()
        {
            STOP = true;
            thread?.Join();
        }
    }

