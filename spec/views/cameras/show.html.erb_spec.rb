require 'rails_helper'

RSpec.describe "cameras/show", type: :view do
  before(:each) do
    @camera = assign(:camera, Camera.create!(
      :name => "Name",
      :url => "Url",
      :caption => "Caption",
      :pw => "Pw",
      :primary => false
    ))
  end

  it "renders attributes in <p>" do
    render
    expect(rendered).to match(/Name/)
    expect(rendered).to match(/Url/)
    expect(rendered).to match(/Caption/)
    expect(rendered).to match(/Pw/)
    expect(rendered).to match(/false/)
  end
end
