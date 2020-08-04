using System;
using System.Globalization;
using System.Reflection.Metadata;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Server.Circuits;

namespace Blue.Data
{
    public class Tank
    {
        float left;
        float scale=1.0f;

        public string Left { get { return ((int)left).ToString()+"px";} }
        public string Scale { get { return scale.ToString("F2", CultureInfo.InvariantCulture); } }

        public Tank(int l){left=l;}

        public void Update()
        {
            if (scale > 0.0f) 
                if (left < 1000) left++;
                else scale = -1.0f;
            else 
                if(left > 10) left --;
            else scale = 1.0f;
        }

        public async Task UpdateAsync()
        {
            if (scale > 0.0f)
                if (left < 1000) left++;
                else scale = -1.0f;
            else
                if (left > 10) left--;
            else scale = 1.0f;
            await Task.CompletedTask;
        }
    }

    public class TankServiceThread : IDisposable
    {
        public Tank[] tank = new Tank[15];
        public int Timeout {get;set;} = 8;

        Thread thread = null;
        volatile bool STOP = false;

        public TankServiceThread()
        {
            for (int n = 0; n < 15; n++) tank[n] = new Data.Tank(-1280 + new Random().Next(32) + 128 * n);
            thread = new Thread(new ThreadStart(TankThread));
            thread.Start();
        }

        public void TankThread()
        {
            while (!STOP)
            {
                for (int n = 0; n < 15; n++) tank[n].Update();
                Thread.Sleep(Timeout);
            }
        }

        public async Task Update(int value)
        {
            if (Notify != null)
            {
                await Notify.Invoke(value);
            }
        }

        public event Func<int, Task> Notify;

        public void Dispose()
        {
            STOP = true;
            thread?.Join();
        }
    }

    public class TankService : IDisposable
    {
        readonly CancellationTokenSource cts = new CancellationTokenSource();

        public Tank[] tank = new Tank[15];
        public int Timeout { get; set; } = 8;

        public TankService()
        {
            for (int n = 0; n < 15; n++) tank[n] = new Data.Tank(-1280 + new Random().Next(32) + 128 * n);
            var token = cts.Token;
            Notify += TankUpdate;
            Task.Run(() => Update(token), token);
        }

        public async Task TankUpdate()
        {
            for (int n = 0; n < 15; n++) await tank[n].UpdateAsync();
        }

        private async void Update(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            while (true)
            {
                if (token.IsCancellationRequested) token.ThrowIfCancellationRequested();
                await Notify?.Invoke();
                await Task.Delay(Timeout);
            }
        }

        public event Func<Task> Notify;
        //public event Action Notify;

        public void Dispose()
        {
            Notify-=TankUpdate;
            cts.Cancel();

        }
    }
}
