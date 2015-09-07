class ProductsController < ApplicationController
  skip_before_filter  :verify_authenticity_token
  before_action :set_product, only: [:show, :update, :destroy]

  # GET /products
  # GET /products.json
  def index
    @products = Product.all
  end

  # GET /products/1
  # GET /products/1.json
  def show

  end

   # POST /products
  # POST /products.json
  def create
    @product = Product.new(product_params)
    @product.save
    render 'show', status: 201
  end

  # PATCH/PUT /products/1
  # PATCH/PUT /products/1.json
  def update
    @product.update(product_params)
    head :no_content
  end

  # DELETE /products/1
  # DELETE /products/1.json
  def destroy
    @product.destroy
    head :no_content
  end

  private
    # Use callbacks to share common setup or constraints between actions.
    def set_product
      @product = Product.find(params[:id])
    end

    # Never trust parameters from the scary internet, only allow the white list through.
    def product_params
      params.require(:product).permit(:name, :length, :width, :height, :weight)
    end
end
