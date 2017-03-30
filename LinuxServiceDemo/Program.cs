using System.Threading;
using Mono.Unix;
using Mono.Unix.Native;
using Topshelf;
using Topshelf.Logging;

namespace LinxuServiceDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(service =>
            {
                service.Service<MyService>(biz =>
                {
                    biz.ConstructUsing(() => new MyService());
                    biz.WhenStarted((b,hc) => b.Start(hc));
                    biz.WhenStopped(b => b.Stop());
                });
                service.UseLinuxIfAvailable();
                service.UseLog4Net("log4net.config", true);
                service.RunAsLocalSystem().SetDescription("my first linux service");
                service.SetDisplayName("MyFirstService");
                service.SetServiceName("firstlinuxservice");
            });

        }
    }

    public class MyService
    {
        LogWriter log = null;
        public MyService()
        {
            log = HostLogger.Get<MyService>();
        }

        public bool Start(HostControl hc)
        {
            log.Debug("MyFirstService is starting.");
            new Thread(TerminateHandler).Start(hc);
            return true;
        }

        void TerminateHandler(object obj)
        {
            HostControl hc = obj as HostControl;

            log.Debug("Initializing Handler for SIGINT");
            UnixSignal signal = new UnixSignal(Signum.SIGINT);
            if(signal.WaitOne())
            {
                log.Debug("Control-C Pressed!");
            }
            log.Debug("handler Terminated");

            hc.Stop();
        }

        public void Stop()
        {
            log.Debug("MyFirstService is stoping.");
        }
    }
}
