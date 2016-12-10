class AddKey2ToMessages < ActiveRecord::Migration[5.0]
  def change
    add_column :messages, :key2, :text
  end
end
