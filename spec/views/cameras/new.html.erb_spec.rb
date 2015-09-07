require 'rails_helper'

RSpec.describe "cameras/new", type: :view do
  before(:each) do
    assign(:camera, Camera.new(
      :name => "MyString",
      :url => "MyString",
      :caption => "MyString",
      :pw => "MyString",
      :primary => false
    ))
  end

  it "renders new camera form" do
    render

    assert_select "form[action=?][method=?]", cameras_path, "post" do

      assert_select "input#camera_name[name=?]", "camera[name]"

      assert_select "input#camera_url[name=?]", "camera[url]"

      assert_select "input#camera_caption[name=?]", "camera[caption]"

      assert_select "input#camera_pw[name=?]", "camera[pw]"

      assert_select "input#camera_primary[name=?]", "camera[primary]"
    end
  end
end
