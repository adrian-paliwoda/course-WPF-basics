using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Evernote.Authentication.Model;
using Evernote.Model;
using Newtonsoft.Json;

namespace Evernote.Authentication
{
    public class FirebaseAuthentication
    {
        private const string ApiKey = "AIzaSyDm5-AlRzhqCOS1rIVBAb2GP-T8zXp1XSs";

        private const string FirebaseSingUpUrl =
            $"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={ApiKey}";

        private const string FirebaseSingInUrl =
            $"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={ApiKey}";

        public static async Task<(bool, string)> Register(User user)
        {
            using (HttpClient client = new())
            {
                var body = new
                {
                    email = user.UserName,
                    password = user.Password,
                    returnSecureToken = true
                };

                var bodyJson = JsonConvert.SerializeObject(body);

                var data = new StringContent(bodyJson, Encoding.UTF8, "application/json");

                var respond = await client.PostAsync(FirebaseSingUpUrl, data);

                if (respond.IsSuccessStatusCode)
                {
                    var result = await respond.Content.ReadAsStringAsync();
                    var registerResult = JsonConvert.DeserializeObject<RegisterResult>(result);

                    if (registerResult != null)
                    {
                        return (true, registerResult.LocalId);
                    }

                    return (false, string.Empty);
                }

                var resultError = await respond.Content.ReadAsStringAsync();
                var registerError = JsonConvert.DeserializeObject<RegisterError>(resultError);

                return (true, registerError?.Error.Message ?? string.Empty);
            }
        }

        public static async Task<(bool, string)> Login(User user)
        {
            using (HttpClient client = new())
            {
                var body = new
                {
                    email = user.UserName,
                    password = user.Password,
                    returnSecureToken = true
                };

                var bodyJson = JsonConvert.SerializeObject(body);

                var data = new StringContent(bodyJson, Encoding.UTF8, "application/json");

                var respond = await client.PostAsync(FirebaseSingInUrl, data);

                if (respond.IsSuccessStatusCode)
                {
                    var result = await respond.Content.ReadAsStringAsync();
                    var registerResult = JsonConvert.DeserializeObject<RegisterResult>(result);

                    if (registerResult != null)
                    {
                        return (true, registerResult.LocalId);
                    }

                    return (false, string.Empty);
                }

                var resultError = await respond.Content.ReadAsStringAsync();
                var registerError = JsonConvert.DeserializeObject<RegisterError>(resultError);

                return (false, registerError?.Error.Message ?? string.Empty);
            }
        }
    }
}