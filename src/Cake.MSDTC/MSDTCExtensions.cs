using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.MSDTC
{
    [CakeAliasCategory("Web")]
    public static class MSDTCExtensions
    {
        [CakeMethodAlias]
        public static void StartMSDTC(this ICakeContext context, int timeout = 10)
        {
            MSDTCHelper
               .Using(context)
               .Start(timeout);
        }

        [CakeMethodAlias]
        public static void StopMSDTC(this ICakeContext context, int timeout = 10)
        {
            MSDTCHelper
               .Using(context)
               .Stop(timeout);
        }

        [CakeMethodAlias]
        public static void PauseMSDTC(this ICakeContext context, int timeout = 10)
        {
            MSDTCHelper
               .Using(context)
               .Pause(timeout);
        }

        [CakeMethodAlias]
        public static void ContinueMSDTC(this ICakeContext context, int timeout = 10)
        {
            MSDTCHelper
               .Using(context)
               .Continue(timeout);
        }
    }
}
