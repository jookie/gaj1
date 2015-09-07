#### One-Way sharing/broadcasting / http://codepen.io/ToeJamson/details/XJBJva

Created March 02, 2015
PubNub
#http://www.pubnub.com/blog/broadcasting-one-many-angularjs-gamecast-app/

the game cast will involve a broadcaster updating and sending game data to spectators.
We’ll need three files: broadcaster.html, spectator.html, and styles.css.
In PubNub API terminology, the broadcaster publishes game data to a channel,
and spectators subscribe to that channel.
The spectators will be able to view the scores of the home and away team,
the inning, the current number of balls, strikes, and outs, and the most recent play.

The broadcaster edits the state of the game data and publishes the entire state of the game to channel.
The spectators can view the game data by subscribing to that channel.

Game Board SVG

An SVG for the game board was generated with InkScape. Each ball, strike, and out is a circle.
Bases are rotated rectangles. It is NOT necessary to generate this code by hand. The code below is for reference.

Each of the SVG elements is given an id and ng-class.

The ng-class is a map of the type, shape, and state.

For example, for <path> with id="b1", the ng-class is {ball:true,circle:true,ball_on:state.balls[0]}.

This means its classes are ball and circle. If a ball is called, ball_on style is applied to the first ball in the balls array.

To reiterate, in the code below a “rect” tag represents the base. The “path” tag is a circle

This is just one of the many things you can do with a broadcast pub/sub design.

Pretty much anytime you want to stream or send data from one publisher to several subscribers,

broadcast is what you’re looking for. Use cases include live-blogging, statistics,

streaming stock quotes, and triggering device actions.