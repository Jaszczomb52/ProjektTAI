using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektTAI
{
    public class Methods
    {
        async static public void Deleter(string url, int i)
        {
            if (i == -1) return;
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage res;
                try
                {
                    res = await client.DeleteAsync(url + "/" + i);
                    if (res.IsSuccessStatusCode)
                        MessageBox.Show(await res.Content.ReadAsStringAsync());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}
