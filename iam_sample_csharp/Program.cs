using System;
using RestSharp;

namespace iam_sample_csharp
{
	public class Program
	{
	    public static void Main()
		{
			// Get a token from Auth0 that will be used to call COAM
			var fetcher = new Auth0TokenFetcher("{YOUR CLIENT ID HERE}", "{YOUR CLIENT SECRET HERE}");
			var accessToken = fetcher.GetToken();

			// Make a request to COAM for permissions
			var principal = "adfs|jdaviscooke@cimpress.com"; // Most commonly the principal field of the JWT that your app accepts
			var resourceType = "merchants";
			var merchant = "vistaprint";

			var client = new RestClient("https://api.cimpress.io/auth/access-management/v1");

			// USER PERMISSIONS FOR ALL MERCHANTS
			var request = new RestRequest("principals/{principal}/permissions/{resourceType}", Method.GET);
			request.AddHeader("Authorization", "Bearer " + accessToken);
			request.AddUrlSegment("principal", principal);
			request.AddUrlSegment("resourceType", resourceType);

			Console.WriteLine("Calling " + client.BaseUrl + "/" + request.Resource);
			IRestResponse response = client.Execute(request);
			Console.WriteLine(response.Content);

			// USER PERMISSIONS FOR A SINGLE MERCHANT
			request = new RestRequest("peincipals/{principal}/permissions/{resourceType}/{resourceIdentifier}", Method.GET);
			request.AddHeader("Authorization", "Bearer " + accessToken);
			request.AddUrlSegment("principal", principal);
			request.AddUrlSegment("resourceType", resourceType);
			request.AddUrlSegment("resourceIdentifier", merchant);

			Console.WriteLine("Calling " + client.BaseUrl + "/" + request.Resource);
			response = client.Execute(request);
			Console.WriteLine(response.Content);
		}
	}
}
