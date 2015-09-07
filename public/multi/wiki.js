/**
 * Created by dovy on 7/26/2015.
 */
// audioInputDevices  == <select>
// audioOutputDevices == <select>
// videoOutputDevices == <select>

// https://github.com/muaz-khan/RTCMultiConnection/wiki/Channels-and-Sessions
//https://github.com/muaz-khan/RTCMultiConnection/wiki/Switch-between-cameras

connection.enumerateDevices(function(devices) {
    devices.forEach(appendOption);

    function appendOption(device) {
        var option = document.createElement('option');
        option.innerHTML = device.label || (device.kind + ': ' + device.deviceId);
        option.value = device.deviceId;

        if (device.kind == 'audioinput' || device.kind == 'audio') {
            audioInputDevices.appendChild(option);
        } else if (device.kind == 'audiooutput') {
            audioOutputDevices.appendChild(option);
        } else videoOutputDevices.appendChild(option);
    }
});

document.getElementById('btn-switch-cameras').onclick = function() {
    // todo(fix): currently API provides maximum two selections
    connection.selectDevices(audioInputDevices.value);
    connection.selectDevices(videoOutputDevices.value);
    // connection.selectDevices(audioOutputDevices.value);

    // below code switches between cameras
    // removes old streams
    // negotiates new streams
    if (connection.stats.numberOfConnectedUsers <= 0) return; // if session isn't active, simply skip!

    // remove all local video streams
    connection.removeStream({
        video: true,
        local: true
    }, true);

    // add new camera to existing peer connections
    connection.addStream({
        audio: true,
        video: true
    });
};


function afterEach(setTimeoutInteval, numberOfTimes, callback, startedTimes) {
    startedTimes = (startedTimes || 0) + 1;
    if (startedTimes >= numberOfTimes) return;

    setTimeout(function () {
        callback();
        afterEach(setTimeoutInteval, numberOfTimes, callback, startedTimes);
    }, setTimeoutInteval);
}
connection.onunmute = function (event) {
    // event.isAudio == audio-only-stream
    // event.audio == has audio tracks

    if (event.isAudio || event.session.audio) {
        // set volume=0
        event.mediaElement.volume = 0;

        // steadily increase volume
        afterEach(200, 5, function () {
            event.mediaElement.volume += .20;
        });
    }
};