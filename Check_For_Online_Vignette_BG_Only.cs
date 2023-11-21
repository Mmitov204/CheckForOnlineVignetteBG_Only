using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Console.Write("Enter your vehicle registration number: ");
        string registrationNumber = Console.ReadLine();

        try
        {
            bool hasVignette = await CheckVignette(registrationNumber);

            if (hasVignette)
            {
                Console.WriteLine("You have a valid vignette.");
            }
            else
            {
                Console.WriteLine("You do not have a valid vignette.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static async Task<bool> CheckVignette(string registrationNumber)
    {
        // API
        string apiUrl = "https://check.bgtoll.bg/";
        string apiKey = "your_api_key";

        using (HttpClient client = new HttpClient())
        {
           
            HttpResponseMessage response = await client.GetAsync($"{apiUrl}?registrationNumber={registrationNumber}&apiKey={apiKey}");

            response.EnsureSuccessStatusCode(); // Throw an exception if the request is not successful.

         
            string responseBody = await response.Content.ReadAsStringAsync();

            // For simplicity, let's assume the presence of the word "valid" in the response indicates a valid vignette.
            return responseBody.ToLower().Contains("valid");
        }
    }
}
