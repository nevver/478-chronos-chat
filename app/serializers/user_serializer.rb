class UserSerializer < ActiveModel::Serializer
  ActiveModel::Serializer.config.adapter = :json
  attributes :email, :first_name, :last_name
end
