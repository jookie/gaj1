json.array!(@products) do |product|
  json.extract! product, :id, :name, :length, :width, :height, :weight
  json.url product_url(product, format: :json)
end
