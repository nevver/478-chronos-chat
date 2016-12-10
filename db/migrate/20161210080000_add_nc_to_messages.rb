class AddNcToMessages < ActiveRecord::Migration[5.0]
  def change
    add_column :messages, :nc, :text
  end
end
