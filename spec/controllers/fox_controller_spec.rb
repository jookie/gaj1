require 'rails_helper'

RSpec.describe FoxController, type: :controller do

  describe "GET #cast" do
    it "returns http success" do
      get :cast
      expect(response).to have_http_status(:success)
    end
  end

end
