using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace task1

{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                throw new ArgumentNullException("URL has to be a first paratmeter");
            }

            bool result = Uri.TryCreate(args[0], UriKind.Absolute, out Uri uriResult)
           && (uriResult.Scheme == Uri.UriSchemeHttps || uriResult.Scheme == Uri.UriSchemeHttp);

            if(!result)
            {
                throw new ArgumentException("Soemhting is wrong with this URL");            
            }


            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(uriResult);

            if(response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                //   Console.WriteLine(content);

                Regex regex = new Regex("[a-z]+[a-z0-9]*@[a-z0-]+\\.[a-z]+", RegexOptions.IgnoreCase);

                MatchCollection matches = regex.Matches(content);

                foreach( object i in matches)
                {

                    Console.WriteLine(i);
                }
            }else
            {
                Console.WriteLine("Error during the GET request");
            }
        }


   
    }
}
