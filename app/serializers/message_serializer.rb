class MessageSerializer < ActiveModel::Serializer
  ActiveModel::Serializer.config.adapter = :json
  attributes :user_id, :user_email, :conversation_id, :body, :created_at
end
