using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentScheduler;
using Topshelf;
using Topshelf.Logging;

namespace LinxuServiceDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(service => {
                service.Service<BizService>(biz => {
                    biz.ConstructUsing(() => new BizService()); // 服务业务类
                    biz.WhenStarted(b => b.Start()).WhenStopped(b => b.Stop());
                });
                service.UseLinuxIfAvailable();// linux
                service.UseLog4Net("log4net.config", true);
                service.RunAsLocalSystem().SetDescription("my first linux service");
                service.SetDisplayName("MyFirstService");
                service.SetServiceName("firstlinuxservice");
            });

        }
    }

    public class BizService
    {
        LogWriter log = null;

        public BizService()
        {
            log = HostLogger.Get<BizService>();
        }

        public void Start()
        {
            log.Debug("MyFirstService is starting.");

            Dictionary<string, List<KeyValuePair<string, string>>> featureTagDict = new Dictionary<string, List<KeyValuePair<string, string>>>() { {"leo1",new List<KeyValuePair<string,string>> () {
                    new KeyValuePair<string,string> ("leo11", "haha"),
                    new KeyValuePair<string,string> ("leo12", "haha2"),
                }
            }, {"leo2",new List<KeyValuePair<string,string>> () {
                    new KeyValuePair<string,string> ("leo21", "hehe"),
                    new KeyValuePair<string,string> ("leo22", "hehe2"),
                }
            },

        };

            JobManager.AddJob(() => {
                log.DebugFormat("myfirstservice is running {0}", DateTime.Now.ToString("yyyyMMddHHmmss"));

            },
                (s) => s.ToRunEvery(1).Seconds());
        }

        public void Stop()
        {
            log.Debug("MyFirstService is stoping.");
        }
    }
}
