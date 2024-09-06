using Firebase.Auth;
using Firebase.Storage;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GestorBarberia.Application.Services
{

    public static class FirebaseServices
    {
        private static readonly string ApiKey = "AIzaSyCTW7PypDBUz50_yQ7maxZlAn1CVPobi90";
        private static readonly string AuthEmail = "dauringonzales7@gmail.com";
        private static readonly string AuthPassword = "Daurin16";
        private static readonly string BucketUrl = "gestorbarberia.appspot.com";

        private static readonly string AuthDomain = "gestorbarberia.firebaseapp.com";

        static FirebaseServices()
        {
            // Initialize Firebase Auth
            FirebaseAuthProvider authProvider = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
            var auth = authProvider.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword).Result;
        }

        public static string UploadImage(string filePath, string fileName, string folderName)
        {
            string url = string.Empty;

            try
            {
                var task = UploadImageAsync(filePath, fileName, folderName);
                task.Wait(); // Ensure the async task completes
                url = task.Result;
            }
            catch (Exception ex)
            {
                // Handle exceptions if needed
                Console.WriteLine($"Error uploading image: {ex.Message}");
            }

            return url;
        }

        private static async Task<string> UploadImageAsync(string filePath, string fileName, string folderName)
        {
            var stream = File.Open(filePath, FileMode.Open);
            var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
            var authResult = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);

            var storage = new FirebaseStorage(BucketUrl, new FirebaseStorageOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(authResult.FirebaseToken)
            });

            var task = storage
                .Child(folderName)
                .Child(fileName)
                .PutAsync(stream);

            string url = await task;
            return url;
        }
    }

}
