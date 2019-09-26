using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Venom.Data.Cache;
using Venom.Data.Models;
using Venom.Data.Rest;

namespace Venom.Utility
{
    public class LocalStorage
    {
        private readonly ILogger _logger;

        #region Local Datas

        public string LocalAccount { get; set; } = "";
        public string LocalServer { get; set; } = "";
        #endregion

        public LocalStorage(
            ILogger<LocalStorage> logger )
        {
            _logger = logger;
        }
    }
}
