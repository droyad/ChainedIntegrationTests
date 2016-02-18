using System;
using FluentAssertions;
using Nancy;
using Nancy.Testing;
using Newtonsoft.Json;
using NUnit.Framework;

namespace IntegrationTests
{
    public static class TestHelpers
    {
        public static T AsJson<T>(this BrowserResponse response)
        {
            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception($"Expected status code to be OK, but got {response.StatusCode}. Body:\r\n{response.Body.AsString()}");

            return JsonConvert.DeserializeObject<T>(response.Body.AsString());
        }

        public static TResponse Put<TResponse>(this Browser browser, string url, object data)
        {
            return browser.Put(url, c =>
            {
                c.Header("content-type", "application/json");
                c.Body(JsonConvert.SerializeObject(data));
            })
            .AsJson<TResponse>();
        }

        public static BrowserResponse Post(this Browser browser, string url, object data)
        {
            return browser.Post(url, c =>
            {
                c.Header("content-type", "application/json");
                c.Body(JsonConvert.SerializeObject(data));
            });
        }

        public static TResponse Post<TResponse>(this Browser browser, string url, object data)
        {
            return browser.Post(url, c =>
            {
                c.Header("content-type", "application/json");
                c.Body(JsonConvert.SerializeObject(data));
            })
            .AsJson<TResponse>();
        }

        public static T Given<T>() where T : BddTest, new()
        {
            var test = new T();
            try
            {
                test.Execute();
            }
            catch (Exception)
            {
                throw new InconclusiveException($"Dependent test {typeof(T).Name} failed");
            }
            return test;
        }
    }
}