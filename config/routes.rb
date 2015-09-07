Receta::Application.routes.draw do

  get 'fox2/cast'


  get 'fox/cast'

  #It broadcasts media in one-way direction over number of peers.
  #All peers can view/watch/listen the broadcast, anonymously.
  #"Anonymously" means viewers will NEVER be prompted to allow webcam or mic or screen.
  #Assume like a radio station inside the browser.
  get 'cnn/index'

  get 'fox3' , :to => redirect('broadcasting/index')


=begin
  <h2>Real-life scenarios?</h2>
  <ol>
   <li>5 CEOs can setup a presentation with many employees!</li>
   <li>5 Teachers can teach many students in the same room!</li>
   <li>Two ore more doctors can inspect two or more patients while LIVE teaching many students!</li>
  </ol>
    <h2>How to use it?</h2>
  <ol>
  <li>1st Tab: There MUST always be a room-moderator.
  Select "Room Moderator" and click "continue".</li>
  <li>2nd Tab: Select "Anonymous Viewer" if you want to anonymously watch/listen all videos.</li>
  <li>3rd Tab: Select "Broadcaster" if you want to setup two-way or multi-directional
  video chat with all other broadcasters.</li>
  </ol>                                                                                                                                                                           <li>3rd Tab: Select "Broadcaster" if you want to setup two-way or multi-directional video chat with all other broadcasters.</li>
=end
  get 'fox4' , :to => redirect('multi/index')

  get 'gopro/index'

  get 'svg1' , :to => redirect('svg_1/broadcaster')
  get 'svg2' , :to => redirect('svg_1/spectator')

  get 'svg3' , :to => redirect('canvas-designer/index')

  get 'cast/index'

  get 'cast/show'

  get 'image/index'

  get 'image/show'

  resources :cameras
  root 'home#index'
  resources :products
  resources :recipes, only: [:index, :show, :create, :update, :destroy]

  get '/foo', :to => redirect('samples/src/content/devices/multi/index')

  get '/foo1', :to => redirect('comhall-webrtc/producer/index')

  get '/foo2', :to => redirect('comhall-webrtc/consumer/index')

  # http://localhost:3000/comhall-webrtc/producer/index
  # http://localhost:3000/samples/src/content/devices/multi/index
end
