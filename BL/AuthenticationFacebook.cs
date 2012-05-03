using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BL
{
    public static class AuthenticationFacebook
    {

        public static string Code;
        private static string StaticUrlOauth = "https://www.facebook.com/dialog/oauth?client_id={{CODIGO_API}}&redirect_uri={{URL_DESTINO}}";
        private static string StaticOauthAcessToke = "https://graph.facebook.com/oauth/access_token?client_id={{CODIGO_API}}&redirect_uri={{URL_DESTINO}}&client_secret={{CODIGO_API_SECRETO}}&code={{CODIGO_RETORNO_USUARIO}}";
        private static string StaticMeAcessToken = "https://graph.facebook.com/me?access_token={{TOKEN_RETORNO}}";

        public static string UrlOauth()
        {
            return StaticUrlOauth.Replace("{{CODIGO_API}}", ConfigurationManager.AppSettings["AppId"]).Replace("{{URL_DESTINO}}", ConfigurationManager.AppSettings["UrlDestino"]);
        }

        public static string ReturnJson()
        {
            try
            {
                var dados = ExecuteAuthentication();
                var json = JsonConvert.SerializeObject(dados);
                return json;
            }catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public static InfoPerson ReturnObject()
        {
            InfoPerson ip;
            try
            {
                var dados = ExecuteAuthentication();
                JObject o = JObject.Parse(dados);
                ip = new InfoPerson()
                {
                    id = (string)o["id"],
                    name = (string)o["name"],
                    first_name = (string)o["first_name"],
                    last_name = (string)o["last_name"],
                    msg = "OK"
                };
            }
            catch (Exception ex)
            {
                ip = new InfoPerson()
                {
                    msg = ex.Message
                };
            }

            return ip;
        }

        private static string ExecuteAuthentication()
        {

            WebRequest request;
            WebResponse response;

            //Monta url OauthAcessToke
            var OauthAcessToke = StaticOauthAcessToke;
            OauthAcessToke = OauthAcessToke.Replace("{{CODIGO_API}}", ConfigurationManager.AppSettings["AppId"]);
            OauthAcessToke = OauthAcessToke.Replace("{{URL_DESTINO}}", ConfigurationManager.AppSettings["UrlDestino"]);
            OauthAcessToke = OauthAcessToke.Replace("{{CODIGO_API_SECRETO}}",ConfigurationManager.AppSettings["AppSecret"]);
            OauthAcessToke = OauthAcessToke.Replace("{{CODIGO_RETORNO_USUARIO}}", Code);

            request = WebRequest.Create(OauthAcessToke);
            response = request.GetResponse();
            var retOauthAcessToke = new StreamReader(response.GetResponseStream()).ReadToEnd().Split('&')[0].Replace("access_token=","");

            //Monta url MeAcessToken
            var MeAcessToken = StaticMeAcessToken;
            MeAcessToken = MeAcessToken.Replace("{{TOKEN_RETORNO}}", retOauthAcessToke);

            request = WebRequest.Create(MeAcessToken);
            response = request.GetResponse();
            var dadosUsuario = new StreamReader(response.GetResponseStream()).ReadToEnd().ToString();
            //var dadosSerializer = new JavaScriptSerializer().Serialize(dadosUsuario);
            return dadosUsuario;
        }
    }

    public struct InfoPerson
    {
        public string id { get; set; }
        public string name { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string msg { get; set; }
    }
}
