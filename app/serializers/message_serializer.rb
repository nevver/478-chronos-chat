class MessageSerializer < ActiveModel::Serializer
  ActiveModel::Serializer.config.adapter = :json
  attributes :conversation_id, :user_id, :body, :created_at
end
