class AuthenticationController < ApplicationController
before_action :authenticate_request!

  def authenticate_user
    user = User.find_for_database_authentication(email: params[:email])
    if user.valid_password?(params[:password])
      render json: payload(user)
    else
      render json: {error: 'Invalid Username/Password'}, status: :unauthorized
    end
  end

  def register_user
    user = User.create(email: params[:email], password: params[:password], password_confirmation: params[:password_confirmation])
    if not (user == nil or user.id == nil)
      render json: {status: 'Success'}
    else
      render json: {error: 'Invalid information'}, status: :unauthorized
    end
  end
  
  def home
    render json: {'logged_in' => true}
  end

  private
  def payload(user)
    return nil unless user and user.id
    {
      auth_token: JsonWebToken.encode({user_id: user.id}),
      user: {id: user.id, email: user.email}
    }
  end
end