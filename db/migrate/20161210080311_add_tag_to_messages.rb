class AddTagToMessages < ActiveRecord::Migration[5.0]
  def change
    add_column :messages, :tag, :text
  end
end
