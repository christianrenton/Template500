
// usage: log('inside coolFunc', this, arguments);
// paulirish.com/2009/log-a-lightweight-wrapper-for-consolelog/
window.log = function () {
	log.history = log.history || [];   // store logs to an array for reference
	log.history.push(arguments);
	if (this.console) console.log(Array.prototype.slice.call(arguments));
};



// place any jQuery/helper plugins in here, instead of separate, slower script files.

function setLocalStorage(c_name, value) {
	var exdays = 30;
	// if localStorage is present, use that
	if (('localStorage' in window) && window.localStorage !== null) {

		localStorage[c_name] = value;

	} else {

		var exdate = new Date();
		exdate.setDate(exdate.getDate() + exdays);
		var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
		document.cookie = c_name + "=" + c_value;
	}
}

function getLocalStorage(c_name) {
	// if localStorage is present, use that
	if (('localStorage' in window) && window.localStorage !== null) {

		return localStorage[c_name];

	} else {

		var i, x, y, ARRcookies = document.cookie.split(";");
		for (i = 0; i < ARRcookies.length; i++) {
			x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
			y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
			x = x.replace(/^\s+|\s+$/g, "");
			if (x == c_name) {
				return unescape(y);
			}
		}
	}
}