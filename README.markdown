Nurfl
================================

Nurfl is a set of simple drop in objects you can use to access your [TeraWurfl](http://dbapi.scientiamobile.com/wiki/index.php/Main_Page "Tera Wurfl") device database.
You obviously must have TeraWurfl installed and the webservice endpoint available.

To retrieve a device you'll need to work with the NurflRequest Object.

    var nurfl = new NurflRequest(Request);
	var device = nurfl.Get(webserviceurl);