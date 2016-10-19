using RestSharp;

namespace iam_sample_csharp
{
	public class Auth0TokenFetcher
	{
		private string clientId;
		private string clientSecret;

		public Auth0TokenFetcher(string clientId, string clientSecret)
		{
			this.clientId = clientId;
			this.clientSecret = clientSecret;
		}

		public string GetToken()
		{
			var client = new RestClient("https://cimpress-dev.auth0.com");

			var request = new RestRequest("oauth/token", Method.POST);
			request.AddParameter("client_id", this.clientId); 
			request.AddParameter("client_secret", this.clientSecret);
			request.AddParameter("audience", "https://development.api.cimpress.io/");
			request.AddParameter("grant_type", "client_credentials");

			IRestResponse response = client.Execute(request);
			dynamic json = Newtonsoft.Json.Linq.JObject.Parse(response.Content);
			return json["access_token"];
		}
	}
}
