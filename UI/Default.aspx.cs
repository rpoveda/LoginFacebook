using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;

namespace UI
{
    public partial class Default : System.Web.UI.Page
    {

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            if (Session["dadosJson"] != null)
            {
                ltlRetorno.Text = (string)Session["dadosJson"];
            }
            else if (Session["dadosObject"] != null)
            {
                var obj = (InfoPerson)Session["dadosObject"];
                var montaString = "";
                montaString += "Id: " + obj.id;
                montaString += "<br/> Name: " + obj.name;
                montaString += "<br/> First Name: " + obj.first_name;
                montaString += "<br/> Last Name: " + obj.last_name;
                montaString += "<br/> Msg: " + obj.msg;
                ltlRetorno.Text = montaString;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if(Request.QueryString["code"] != null)
                {
                    AuthenticationFacebook.Code = Request.QueryString["code"];
                    if(Session["tipoDado"].ToString() == "Json".ToUpper())
                    {
                        Session["dadosJson"] = AuthenticationFacebook.ReturnJson();
                    }
                    else if (Session["tipoDado"].ToString() == "Objeto".ToUpper())
                    {
                        Session["dadosObject"] = AuthenticationFacebook.ReturnObject();
                    }
                    Response.Redirect("Default.aspx");
                }
            }
        }

        protected void hlLoginFacebook_OnClick(object sender, EventArgs e)
        {
            var url = AuthenticationFacebook.UrlOauth();
            Session["tipoDado"] = rbTipoRetorno.SelectedValue.ToUpper();
            Response.Redirect(url);
        }
    }
}