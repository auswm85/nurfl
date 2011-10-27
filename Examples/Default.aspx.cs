using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nurfl.Core;

namespace Examples
{
    public partial class Default : System.Web.UI.Page
    {
        private string webServiceUrl = System.Configuration.ConfigurationManager.AppSettings["teraWurflUrl"];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //contructor with current request
                NurflRequest nurfl = new NurflRequest(Request);

                //use some default search params if you fancy
                string[] defaultParams = new string[]
                {
                    "is_wireless_device", 
                    "is_tablet", 
                    "model_name",
                    "brand_name"
                };

                //make a request for the device
                Device device = nurfl.Get(webServiceUrl, defaultParams);

                string modelName = device.GetCapability("model_name").ToString().Trim();
                string brandName = device.GetCapability("brand_name").ToString().Trim();

                if (device.IsMobileDevice())
                    Context.Response.Write("<b>wireless device: " + brandName + "-" + modelName +"</b>");

                if (device.IsTablet())
                    Context.Response.Write("<b>tablet device: " + brandName + "-" + modelName +"</b>");
            }
        }
    }
}