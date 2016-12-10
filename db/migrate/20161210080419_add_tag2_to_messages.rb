class AddTag2ToMessages < ActiveRecord::Migration[5.0]
  def change
    add_column :messages, :tag2, :text
  end
end
