class CreateCameras < ActiveRecord::Migration
  def change
    create_table :cameras do |t|
      t.string :name
      t.string :url
      t.string :caption
      t.string :pw
      t.boolean :primary

      t.timestamps null: false
    end
  end
end
