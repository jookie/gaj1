require 'rails_helper'

RSpec.describe "cameras/index", type: :view do
  before(:each) do
    assign(:cameras, [
      Camera.create!(
        :name => "Name",
        :url => "Url",
        :caption => "Caption",
        :pw => "Pw",
        :primary => false
      ),
      Camera.create!(
        :name => "Name",
        :url => "Url",
        :caption => "Caption",
        :pw => "Pw",
        :primary => false
      )
    ])
  end

  it "renders a list of cameras" do
    render
    assert_select "tr>td", :text => "Name".to_s, :count => 2
    assert_select "tr>td", :text => "Url".to_s, :count => 2
    assert_select "tr>td", :text => "Caption".to_s, :count => 2
    assert_select "tr>td", :text => "Pw".to_s, :count => 2
    assert_select "tr>td", :text => false.to_s, :count => 2
  end
end
