using System;
using RestSharp;

namespace iam_sample_csharp
{
	public class Program
	{
	    public static void Main()
		{
			// Get a token from Auth0 that will be used to call IAM
			var fetcher = new Auth0TokenFetcher("{YOUR CLIENT ID HERE}", "{YOUR CLIENT SECRET HERE}");
			var iamToken = fetcher.GetToken();

			// Make a request to IAM for permissions
			var sub = "adfs|jshu@cimpress.com"; // Most commonly the sub field of the JWT that your app accepts
			var resourceType = "merchants";
			var merchant = "vistaprint";

			var client = new RestClient("https://development.api.cimpress.io/iam/v0");
			// var client = new RestClient("https://api.cimpress.io/auth/iam/v0"); // Production. Note the extra /auth path segment

			// USER PERMISSIONS FOR ALL MERCHANTS
			var request = new RestRequest("user-permissions/{sub}/{resourceType}", Method.GET);
			request.AddHeader("Authorization", "Bearer " + iamToken);
			request.AddUrlSegment("sub", sub);
			request.AddUrlSegment("resourceType", resourceType);

			Console.WriteLine("Calling " + client.BaseUrl + "/" + request.Resource);
			IRestResponse response = client.Execute(request);
			Console.WriteLine(response.Content);

			// USER PERMISSIONS FOR A SINGLE MERCHANT
			request = new RestRequest("user-permissions/{sub}/{resourceType}/{resourceIdentifier}", Method.GET);
			request.AddHeader("Authorization", "Bearer " + iamToken);
			request.AddUrlSegment("sub", sub);
			request.AddUrlSegment("resourceType", resourceType);
			request.AddUrlSegment("resourceIdentifier", merchant);

			Console.WriteLine("Calling " + client.BaseUrl + "/" + request.Resource);
			response = client.Execute(request);
			Console.WriteLine(response.Content);
		}
	}
}
