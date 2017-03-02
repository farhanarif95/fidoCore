using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fidoBackend.Services
{
    public class BaseService
    {
        public static MobileServiceClient MobileService =
  new MobileServiceClient(
      "https://fidocore.azurewebsites.net"
  );
    }
}
