Nurfl
================================

Nurfl is a set of simple drop in objects you can use to access your [TeraWurfl](http://dbapi.scientiamobile.com/wiki/index.php/Main_Page "Tera Wurfl") device database.
You obviously must have TeraWurfl installed and the webservice endpoint available.

You can find a full listing of capabilities provided by the Tera Wurfl api [here.](http://wurfl.sourceforge.net/help_doc.php "Tera Wurfl Capabilities")

To retrieve a device you'll need to work with the NurflRequest Object.
    
	//instantiate with current request object
    var nurfl = new NurflRequest(Request);
	
	//instantiate with user agent from request 
	var nurfl = new NurflRequest(Request.UserAgent);
	
	//or from string
	var nurfl = new NurflRequest("Mozilla/5.0 (iPhone; U; CPU iPhone OS 4_0 like Mac OS X; en-us) AppleWebKit/532.9 (KHTML, like Gecko) Version/4.0.5 Mobile/8A293 Safari/6531.22.7");
	
	//retrieve the device
	var device = nurfl.Get(webserviceurl);
	
	//retrieve specific capabilities
	var device = nurfl.Get(webserviceurl, "is_wireless_device", "brand_name", "model_name", "is_tablet");
	
	//do fun stuff
	device.IsMobileDevice();
	device.IsTablet();
	
	string brand = device.GetCapability("brand_name");
	string model = device.GetCapability("model_name");