using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektTAI
{
    public class Methods<T>
    {
        async static public Task Deleter(string url, int i)
        {
            if (i == -1) return;
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage res;
                try
                {
                    res = await client.DeleteAsync(url + "/" + i);
                    if (res.IsSuccessStatusCode)
                        return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public static DictList GetDictionary(string type, ComboBox cb)
        {
            cb.Items.Clear();
            string url = "";
            if (type == "model")
                url = "http://localhost:5297/api/Main/GetModels";
            if (type == "producer")
                url = "http://localhost:5297/api/Main/GetProducents";
            if (type == "type")
                url = "http://localhost:5297/api/Main/GetTypes";

            using (WebClient client = new WebClient())
            {
                try
                {
                    DictList dc = new DictList();
                    IDictionaries[]? emp = null;
                    string text = Encoding.UTF8.GetString(client.DownloadData(url));
                    if (type == "model")
                        emp = JsonConvert.DeserializeObject<Models[]>(text);
                    else if (type == "producer")
                        emp = JsonConvert.DeserializeObject<Producent[]>(text);
                    else if (type == "type")
                        emp = JsonConvert.DeserializeObject<Type[]>(text);
                    if (emp == null)
                        throw new NoNullAllowedException();
                    foreach (IDictionaries x in emp)
                    {
                        cb.Items.Add(x);
                    }
                    dc.dc = emp.ToList();
                    return dc;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                    return null;
                }
            }
        }

        async public static Task AddOrModify(string url, T input, bool modify)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage res;
                try
                {
                    res = modify ?
                        await client.PutAsJsonAsync(url, input) :
                        await client.PostAsJsonAsync(url, input);

                    if (res.IsSuccessStatusCode)
                        return;
                    //MessageBox.Show(await res.Content.ReadAsStringAsync());
                    else
                        MessageBox.Show("Nieprawidłowe wywołanie" + await res.Content.ReadAsStringAsync());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

    }
}
