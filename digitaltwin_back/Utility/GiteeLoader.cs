using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

class GiteeUploader
{
    private static readonly string GITEE_API_URL = "https://gitee.com/api/v5/repos/{owner}/{repo}/contents/{path}";

    private string owner;
    private string repo;
    private string accessToken;

    public GiteeUploader(string owner, string repo, string accessToken)
    {
        this.owner = owner;
        this.repo = repo;
        this.accessToken = accessToken;
    }

    public string UploadFile(string path, string filename, string content)
    {
        try
        {
            string url = GITEE_API_URL
                .Replace("{owner}", owner)
                .Replace("{repo}", repo)
                .Replace("{path}", path + "/" + filename);

            HttpWebRequest request = WebRequest.CreateHttp(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add("Authorization", "Bearer " + accessToken);

            string message = "Upload by " + Environment.CurrentDirectory;

            byte[] contentBytes = Encoding.UTF8.GetBytes(content);
            string encodedContent = Convert.ToBase64String(contentBytes);

            string postData = string.Format(
                "{{\"content\":\"{0}\",\"message\":\"{1}\"}}",
                encodedContent,
                message
            );

            using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(postData);
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                string result = reader.ReadToEnd();
                return result;
            }
        }
        catch (WebException ex)
        {
            using (StreamReader reader = new StreamReader(ex.Response.GetResponseStream()))
            {
                string result = reader.ReadToEnd();
                Console.WriteLine(result);
                return result;
            }
        }
    }
}

