json.array!(@cameras) do |camera|
  json.extract! camera, :id, :name, :url, :caption, :pw, :primary
  json.url camera_url(camera, format: :json)
end
