using CustomerApplication.GUI.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net;

namespace CustomerApplication.GUI.Core.Datahandler
{
    /// <summary>The data class</summary>
    public class Data
    {

        /// <summary>Gets the user asynchronous.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUri">The request URI.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Invalid header value: " + header
        /// or
        /// Invalid header value: " + header</exception>
        public static async Task<T> GetUserAsync<T>(Uri requestUri)
        {
            HttpClient httpClient = new HttpClient();
            var headers = httpClient.DefaultRequestHeaders;

            string header = "ie";
            if (!headers.UserAgent.TryParseAdd(header))
            {
                throw new Exception("Invalid header value: " + header);
            }

            header = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
            if (!headers.UserAgent.TryParseAdd(header))
            {
                throw new Exception("Invalid header value: " + header);
            }

            HttpResponseMessage httpResponse = new HttpResponseMessage();
            string httpResponseBody = "";

            try
            {

                //Send the GET request
                httpResponse = await httpClient.GetAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();


                var serializeOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };
                var testUser = System.Text.Json.JsonSerializer.Deserialize<T>(httpResponseBody, serializeOptions);
                return testUser;
            }
            catch (Exception ex)
            {

                httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
                return default;
            }
        }


        /// <summary>Registers the user.</summary>
        /// <param name="url">The URL.</param>
        /// <param name="UserObject">The user object.</param>
        /// <returns></returns>
        public static async Task<bool> RegisterUser(Uri url, StringContent UserObject)
        {
            try
            {
                // Construct the HttpClient and Uri. This endpoint is for test purposes only.
                HttpClient httpClient = new HttpClient();


                // Post the JSON and wait for a response.
                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(
                    url, UserObject);

                // Make sure the post succeeded, and write out the response.
                httpResponseMessage.EnsureSuccessStatusCode();
                var httpResponseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                Debug.WriteLine(httpResponseBody);
                return true;

            }
            catch (HttpRequestException ex)
            {
                // Write out any exceptions.
                Debug.WriteLine(ex);
                Console.WriteLine(ex);
                return false;
            }
            catch (WebException ex)
            {

                Debug.WriteLine(ex);
                return false;
            }
        }

        /// <summary>Gets the count.</summary>
        /// <param name="requestUri">The request URI.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Invalid header value: " + header
        /// or
        /// Invalid header value: " + header</exception>
        public static async Task<int> GetCount(Uri requestUri)
        {
            HttpClient httpClient = new HttpClient();
            var headers = httpClient.DefaultRequestHeaders;

            string header = "ie";
            if (!headers.UserAgent.TryParseAdd(header))
            {
                throw new Exception("Invalid header value: " + header);
            }

            header = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
            if (!headers.UserAgent.TryParseAdd(header))
            {
                throw new Exception("Invalid header value: " + header);
            }



            HttpResponseMessage httpResponse = new HttpResponseMessage();
            string httpResponseBody = "";

            try
            {

                //Send the GET request
                httpResponse = await httpClient.GetAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                return Convert.ToInt32(httpResponseBody);

            }
            catch (Exception ex)
            {
                httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
                return 0;
            }
        }


        /// <summary>Updates the user.</summary>
        /// <param name="url">The URL.</param>
        /// <param name="UserObject">The user object.</param>
        public static async Task UpdateUser(Uri url, StringContent UserObject)
        {
            try
            {
                // Construct the HttpClient and Uri. This endpoint is for test purposes only.
                HttpClient httpClient = new HttpClient();


                // Post the JSON and wait for a response.
                HttpResponseMessage httpResponseMessage = await httpClient.PutAsync(
                    url, UserObject);

                // Make sure the post succeeded, and write out the response.
                httpResponseMessage.EnsureSuccessStatusCode();
                var httpResponseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                Debug.WriteLine(httpResponseBody);

            }
            catch (HttpRequestException ex)
            {
                // Write out any exceptions.
                Debug.WriteLine(ex);
                Console.WriteLine(ex);
            }
        }

        /// <summary>Logins the user.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="employeeUri">The employee URI.</param>
        /// <param name="UserObject">The user object.</param>
        /// <returns></returns>
        public static async Task<T> LoginUser<T>(Uri employeeUri, StringContent UserObject)
        {
            try
            {
                // Construct the HttpClient and Uri. This endpoint is for test purposes only.
                HttpClient httpClient = new HttpClient();


                // Post the JSON and wait for a response.
                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(
                     employeeUri, UserObject);

                var serialize = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };

                // Make sure the post succeeded, and write out the response.
                httpResponseMessage.EnsureSuccessStatusCode();
                var httpResponseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                var oneUser = System.Text.Json.JsonSerializer.Deserialize<T>(httpResponseBody, serialize);
                return oneUser;

            }

            catch (HttpRequestException)
            {
                // Write out any exceptions.
                return default;
            }
        }

        /// <summary>Gets the employees asynchronous.</summary>
        /// <returns></returns>
        public static async Task<Employee[]> GetEmployeesAsync()
        {
            HttpClient httpClient = new HttpClient();
            Uri studentUri = new Uri("http://localhost:5000/Users");


            HttpResponseMessage result = await httpClient.GetAsync(studentUri);
            string json = await result.Content.ReadAsStringAsync();
            Employee[] employees = JsonConvert.DeserializeObject<Employee[]>(json);

            return employees;

        }

        /// <summary>Gets the companies asynchronous.</summary>
        /// <returns></returns>
        public static async Task<Company[]> GetCompaniesAsync()
        {
            HttpClient httpClient = new HttpClient();
            Uri studentUri = new Uri("http://localhost:5000/api/Companies");


            HttpResponseMessage result = await httpClient.GetAsync(studentUri);
            string json = await result.Content.ReadAsStringAsync();
            Company[] companies = JsonConvert.DeserializeObject<Company[]>(json);

            return companies;

        }


        /// <summary>Gets the employees companies asynchronous.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public static async Task<IList<T>> GetEmployeesCompaniesAsync<T>(int id)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                Uri studentUri = new Uri("http://localhost:5000/api/Companies/RelatedEmployees/" + id);


                HttpResponseMessage result = await httpClient.GetAsync(studentUri);

                var serializeOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };

                string json = await result.Content.ReadAsStringAsync();
                var studentArray = System.Text.Json.JsonSerializer.Deserialize<IList<T>>(json, serializeOptions);

                return studentArray;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>Gets the inventories companies asynchronous.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public static async Task<IList<T>> GetInventoriesCompaniesAsync<T>(int id)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                Uri studentUri = new Uri("http://localhost:5000/api/Companies/RelatedInventories/" + id);


                HttpResponseMessage result = await httpClient.GetAsync(studentUri);

                var serializeOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };

                string json = await result.Content.ReadAsStringAsync();
                var studentArray = System.Text.Json.JsonSerializer.Deserialize<IList<T>>(json, serializeOptions);

                return studentArray;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>Deletes the inventory.</summary>
        /// <param name="uri">The URI.</param>
        /// <exception cref="Exception">Invalid header value: " + header
        /// or
        /// Invalid header value: " + header</exception>
        public static async Task DeleteInventory(Uri uri)
        {
            try
            {
                // Construct the HttpClient and Uri. This endpoint is for test purposes only.
                HttpClient httpClientPost = new HttpClient();
                var headers = httpClientPost.DefaultRequestHeaders;



                string header = "ie";
                if (!headers.UserAgent.TryParseAdd(header))
                {
                    throw new Exception("Invalid header value: " + header);
                }

                header = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
                if (!headers.UserAgent.TryParseAdd(header))
                {
                    throw new Exception("Invalid header value: " + header);
                }
                // Post the JSON and wait for a response.
                HttpResponseMessage httpResponseMessage = await httpClientPost.DeleteAsync(
                    uri
                    );

                // Make sure the put succeeded, and write out the response.
                httpResponseMessage.EnsureSuccessStatusCode();
                var httpResponseBodyPost = await httpResponseMessage.Content.ReadAsStringAsync();
                Debug.WriteLine(httpResponseBodyPost);
            }
            catch (Exception ex)
            {
                // Write out any exceptions.
                Debug.WriteLine(ex);
            }
        }


    }
}

