
//C:\AngularConsumer\public\broadcasting\index.js

function on_load_page() {

}

var config = {
    openSocket: function(config) {
        // https://github.com/muaz-khan/WebRTC-Experiment/blob/master/Signaling.md
        // This method "openSocket" can be defined in HTML page
        // to use any signaling gateway either XHR-Long-Polling or SIP/XMPP or WebSockets/Socket.io
        // or WebSync/SignalR or existing implementations like signalmaster/peerserver or sockjs etc.

        var channel = config.channel || location.href.replace( /\/|:|#|%|\.|\[|\]/g , '');
        var socket = new Firebase('https://webrtc.firebaseIO.com/' + channel);
        socket.channel = channel;
        socket.on("child_added", function(data) {
            config.onmessage && config.onmessage(data.val());
        });
        socket.send = function(data) {
            this.push(data);
        };
        config.onopen && setTimeout(config.onopen, 1);
        socket.onDisconnect().remove();
        return socket;
    },
    onRemoteStream: function(htmlElement) {
        htmlElement.setAttribute('controls', true);
        videosContainer.insertBefore(htmlElement, videosContainer.firstChild);
        htmlElement.play();
        rotateInCircle(htmlElement);
    },
    onRoomFound: function(room) {
        var alreadyExist = document.querySelector('button[data-broadcaster="' + room.broadcaster + '"]');
        if (alreadyExist) return;

        if (typeof roomsList === 'undefined') roomsList = document.body;

        var tr = document.createElement('tr');
        tr.innerHTML = '<td><strong>' + room.roomName + '</strong> is broadcasting his media!</td>' +
            '<td><button class="join">Join</button></td>';
        roomsList.insertBefore(tr, roomsList.firstChild);

        var joinRoomButton = tr.querySelector('.join');
        joinRoomButton.setAttribute('data-broadcaster', room.broadcaster);
        joinRoomButton.setAttribute('data-roomToken'  , room.broadcaster);
        joinRoomButton.onclick = function() {
            this.disabled = true;

            var broadcaster = this.getAttribute('data-broadcaster');
            var roomToken = this.getAttribute('data-roomToken');
            broadcastUI.joinRoom({
                roomToken: roomToken,
                joinUser: broadcaster
            });
            hideUnnecessaryStuff();
        };
    },
    onNewParticipant: function(numberOfViewers) {
        document.title = 'Viewers: ' + numberOfViewers;
    }
};

function setupNewBroadcastButtonClickHandler() {
    document.getElementById('broadcast-name').disabled = true;
    document.getElementById('setup-new-broadcast').disabled = true;

    captureUserMedia(function() {
        var shared = 'video';
        if (window.option == 'Only Audio') {
            shared = 'audio';
        }
        if (window.option == 'Screen') {
            shared = 'screen';
        }

        broadcastUI.createRoom({
            roomName: (document.getElementById('broadcast-name') || { }).value || 'Anonymous',
            isAudio: shared === 'audio'
        });
    });
    hideUnnecessaryStuff();
}

function captureUserMedia(callback) {
    var constraints = null;
    window.option = broadcastingOption ? broadcastingOption.value : '';
    if (option === 'Only Audio') {
        constraints = {
            audio: true,
            video: false
        };

        if(DetectRTC.hasMicrophone !== true) {
            alert('DetectRTC library is unable to find microphone; maybe you denied microphone access once and it is still denied or maybe microphone device is not attached to your system or another app is using same microphone.');
        }
    }
    if (option === 'Screen') {
        var video_constraints = {
            mandatory: {
                chromeMediaSource: 'screen'
            },
            optional: []
        };
        constraints = {
            audio: false,
            video: video_constraints
        };

        if(DetectRTC.isScreenCapturingSupported !== true) {
            alert('DetectRTC library is unable to find screen capturing support. You MUST run chrome with command line flag "chrome --enable-usermedia-screen-capturing"');
        }
    }

    if (option != 'Only Audio' && option != 'Screen' && DetectRTC.hasWebcam !== true) {
        alert('DetectRTC library is unable to find webcam; maybe you denied webcam access once and it is still denied or maybe webcam device is not attached to your system or another app is using same webcam.');
    }

    var htmlElement = document.createElement(option === 'Only Audio' ? 'audio' : 'video');
    htmlElement.setAttribute('autoplay', true);
    htmlElement.setAttribute('controls', true);
    videosContainer.insertBefore(htmlElement, videosContainer.firstChild);

    var mediaConfig = {
        video: htmlElement,
        onsuccess: function(stream) {
            config.attachStream = stream;
            callback && callback();

            htmlElement.setAttribute('muted', true);
            rotateInCircle(htmlElement);
        },
        onerror: function() {
            if (option === 'Only Audio') alert('unable to get access to your microphone');
            else if (option === 'Screen') {
                if (location.protocol === 'http:') alert('Please test this WebRTC experiment on HTTPS.');
                else alert('Screen capturing is either denied or not supported. Are you enabled flag: "Enable screen capture support in getUserMedia"?');
            } else alert('unable to get access to your webcam');
        }
    };
    if (constraints) mediaConfig.constraints = constraints;
    getUserMedia(mediaConfig);
}

function hideUnnecessaryStuff() {
    var visibleElements = document.getElementsByClassName('visible'),
        length = visibleElements.length;
    for (var i = 0; i < length; i++) {
        visibleElements[i].style.display = 'none';
    }
}

function rotateInCircle(video) {
    video.style[navigator.mozGetUserMedia ? 'transform' : '-webkit-transform'] = 'rotate(0deg)';
    setTimeout(function() {
        video.style[navigator.mozGetUserMedia ? 'transform' : '-webkit-transform'] = 'rotate(360deg)';
    }, 1000);
}
