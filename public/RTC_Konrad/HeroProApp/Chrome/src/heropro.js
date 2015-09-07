function getXhr(){
				var xhr = null;
				if(window.XMLHttpRequest) // Firefox and others
					xhr = new XMLHttpRequest();
				else if(window.ActiveXObject){ // Internet Explorer
					try {
						xhr = new ActiveXObject("Msxml2.XMLHTTP");
					} catch (e) {
						xhr = new ActiveXObject("Microsoft.XMLHTTP");
					}
				}
				else { // XMLHttpRequest no supported
					alert("Votre navigateur ne supporte pas les objets XMLHTTPRequest...");
					xhr = false;
				}
				return xhr
			}
 //Something strange to get the f*cking chrome app to work:
 function requestListener() {
  document.querySelector("#content").innerHTML = this.responseText;
};

onload = function() {
  var request = new XMLHttpRequest();
  request.onload = requestListener;
  request.open("GET", "content.txt", true);
  request.send();
};
			/**
			* Command
			*/
			function command(device, app, command){
				var xhr = getXhr()
 
				xhr.onreadystatechange = function(){
 
					if(xhr.readyState == 4 && xhr.status == 200){
						alert(xhr.responseText);
					}
				}
				xhr.open('GET','http://10.5.5.9:80/'+device+'/'+app+'?t='+document.getElementById('wifiPassword').value+'&p='+command);
				
				xhr.send(null);
			}