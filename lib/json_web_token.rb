class JsonWebToken
  def self.encode(payload)
  	payload[:exp] = (5).minutes.from_now.to_i #expire in 5 minutes
    JWT.encode(payload, Rails.application.secrets.secret_key_base)
  end

  def self.decode(token)
    return HashWithIndifferentAccess.new(JWT.decode(token, Rails.application.secrets.secret_key_base)[0])
  rescue
    nil
  end
end