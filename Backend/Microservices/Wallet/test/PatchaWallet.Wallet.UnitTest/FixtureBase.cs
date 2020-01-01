using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PatchaWallet.Wallet.UnitTest
{
    public class FixtureBase
    {
        protected T LoadJson<T>(string name)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            var file = path + "\\Mock\\" + name + ".json";
            using (StreamReader r = new StreamReader(file))
            {
                string json = r.ReadToEnd();
                T item = JsonConvert.DeserializeObject<T>(json);

                return item;
            }
        }
    }
}
