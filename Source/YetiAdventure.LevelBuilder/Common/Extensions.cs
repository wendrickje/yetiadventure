using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetiAdventure.LevelBuilder.Common
{
    public static class Extensions
    {
        public static TService GetService<TService>(this IServiceProvider container) where TService : class
        {
            return container.GetService(typeof(TService)) as TService;
        }
    }
}
