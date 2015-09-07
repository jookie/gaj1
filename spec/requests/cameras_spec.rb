require 'rails_helper'

RSpec.describe "Cameras", type: :request do
  describe "GET /cameras" do
    it "works! (now write some real specs)" do
      get cameras_path
      expect(response).to have_http_status(200)
    end
  end
end
