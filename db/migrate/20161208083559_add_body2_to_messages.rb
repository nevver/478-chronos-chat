class AddBody2ToMessages < ActiveRecord::Migration[5.0]
  def change
    add_column :messages, :body2, :text, :null => true
  end
end
