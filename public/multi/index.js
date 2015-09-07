/**
 * Created by dovy on 7/26/2015.
 */




function setupNewBroadcastButtonClickHandler() {

    this.disabled = true;

    var role = select.value;

    window.connection = new RTCMultiConnection();

    // dont-override-session allows you force RTCMultiConnection
    // to not override default session of participants;
    // by default, session is always overridden and set to the session coming from initiator!
    connection.dontOverrideSession = true;
    connection.session = {
        audio: true,
        video: true,
        oneway: role == 'Anonymous Viewer'
    };
    connection.onstream = function (e) {
        videos.appendChild(e.mediaElement);

        if (connection.isInitiator) {
            if (e.type == 'remote') {
                var radio_remote = makeRadioButton("yesbutton", "no", "remote 1");
                videos.appendChild(radio_remote);
            }
            if (e.type != 'remote') {
                var radio_local = makeRadioButton("yesbutton", "yes", "local 1");
                videos.appendChild(radio_local);
            }
        }

        if (e.type == 'remote') {
            // because "viewer" joined room as "oneway:true"
            // initiator will NEVER share participants
            // to manually ask for participants;
            // call "askToShareParticipants" method.
            connection.askToShareParticipants();
        }
        // if you're moderator
        // if stream-type is 'remote'
        // if target user is broadcaster!
        if (connection.isInitiator && e.type == 'remote' && !e.session.oneway) {
            // call "shareParticipants" to manually share participants with all connected users!
            connection.shareParticipants({
                dontShareWith: e.userid
            });
        }
    };
    connection.onNewSession = function (session) {
        if (role == 'Spectator') {
            session.join({
                oneway: true
            });
        }

        if (role == 'Broadcaster') {
            session.join();
        }
    };
    if (role == 'Producer')
        connection.open({
            sessionid: connection.channel,
            captureUserMediaOnDemand: false
        });
    else
        connection.connect(connection.channel);

}
//http://stackoverflow.com/questions/23430455/in-html-with-javascript-create-new-radio-button-and-its-text
function makeRadioButton(name, value, text) {
    var label = document.createElement("label");
    var radio = document.createElement("input");
    radio.type = "radio";
    radio.name = name;
    radio.value = value;
    radio.onclick = function () {
        //a person who delivers a live commentary on an event or performance.
        // if session isn't active, simply skip!
        //if (connection.stats.numberOfConnectedUsers <= 0) return;
        if (connection.isInitiator) {
            //connection.selectDevices(connection.value);
            switchCameras()
            alert("Commentator switch to this camera")
        }
    };
    label.appendChild(radio);
    label.appendChild(document.createTextNode(text));
    return label;
}
//http://stackoverflow.com/questions/14610945/how-to-choose-input-video-device-for-webrtc
function switchCameras() {
    // todo(fix): currently API provides maximum two selections
    //connection.selectDevices(audioInputDevices.value);
    //connection.selectDevices(videoOutputDevices.value);
    //connection.selectDevices(audioOutputDevices.value);


    // below code switches between cameras
    // removes old streams
    // negotiates new streams
    //if (connection.stats.numberOfConnectedUsers <= 0) return; // if session isn't active, simply skip!

    // remove all local video streams
    connection.removeStream({
        video: true,
        local: true
    }, true);
    // when renegotiating streams,
    // it is preferred to always set OfferToReceive Audio/Video
    // to make sure renegotiation NEVER fails!
    connection.sdpConstraints.mandatory = {
        OfferToReceiveAudio: true,
        OfferToReceiveVideo: true
    };

    // add new camera to existing peer connections
    connection.addStream({
        audio: true,
        video: true
    });
};

function setStatus(status){
    document.getElementById('status').innerHTML = status;
}

function toggleSettings(){
    toggleElement(document.getElementById('settings'));
}

function toggleVideo(){
    toggleElement(document.getElementById('local-video'));
}

function updateStatus(){
    if (Object.size(peerConnections) == 0)
        setStatus('Waiting for incoming connections...');
    else
        setStatus('Currently active consumers: '+Object.size(peerConnections));
}

function detectRTC(){
    //works only with chrom
    var connect = new RTCMultiConnection();
    connect.DetectRTC.load(function() {
        connect.enumerateDevices(function(devices) {
            // iterate over devices-array
            devices.forEach(function(device) {
                // skip audio devices
                //if (device.kind.indexOf('audio') != -1) return;

                var button = document.createElement('button');
                button.id = device.deviceId;
                button.innerHTML = device.label || (device.kind + ': ' + device.deviceId)
                button.onclick = function() {
                    this.disabled = true;

                    connect.selectDevices(this.id);
                    connect.dontCaptureUserMedia = false;
                    connect.captureUserMedia(function(stream) {
                        connect.attachStreams.push(stream);
                        connect.dontCaptureUserMedia = true;

                        if (document.getElementById('broadcast-all-cameras').disabled == true) {
                            document.getElementById('broadcast-all-cameras').disabled = false;
                            document.getElementById('broadcast-all-cameras').style.background = '-webkit-gradient(linear, 0% 0%, 0% 100%, color-stop(0.05, rgb(143, 231, 253)), to(rgb(255, 255, 255)))';
                        }
                    });
                };
                buttonsContainer.appendChild(button);
            });
        });
    });
}
document.onkeypress = function (event){
    switch (String.fromCharCode(event.charCode)){
        case 's':
            toggleSettings();
            break;
        case 'v':
            toggleVideo();
            break;
        case 'l':
            toggleElement(document.getElementById('log'));
            break;
        case 'd':
            detectRTC();
            break;
        default:
            break;
    }
}
