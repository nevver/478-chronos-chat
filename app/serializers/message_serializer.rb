class MessageSerializer < ActiveModel::Serializer
  ActiveModel::Serializer.config.adapter = :json
  attributes :user_id, :user_email, :conversation_id, :body, :nc, :tag, :key, :body2, :nc2, :tag2, :key2, :created_at
end
