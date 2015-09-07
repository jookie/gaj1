chrome.app.runtime.onLaunched.addListener(function() {
	var window = chrome.app.window.create('index.html', {
	},function(createdWindow){
		createdWindow.maximize();
	});
});