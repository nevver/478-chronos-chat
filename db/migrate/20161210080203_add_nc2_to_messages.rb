class AddNc2ToMessages < ActiveRecord::Migration[5.0]
  def change
    add_column :messages, :nc2, :text
  end
end
