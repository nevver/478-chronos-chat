class AddKeyToMessages < ActiveRecord::Migration[5.0]
  def change
    add_column :messages, :key, :text
  end
end
