require 'rails_helper'

RSpec.describe "products/new", type: :view do
  before(:each) do
    assign(:product, Product.new(
      :name => "MyString",
      :length => 1.5,
      :width => 1.5,
      :height => 1.5,
      :weight => 1.5
    ))
  end

  it "renders new product form" do
    render

    assert_select "form[action=?][method=?]", products_path, "post" do

      assert_select "input#product_name[name=?]", "product[name]"

      assert_select "input#product_length[name=?]", "product[length]"

      assert_select "input#product_width[name=?]", "product[width]"

      assert_select "input#product_height[name=?]", "product[height]"

      assert_select "input#product_weight[name=?]", "product[weight]"
    end
  end
end
