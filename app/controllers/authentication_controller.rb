class AuthenticationController < ApplicationController
before_action :configure_permitted_parameters, if: :devise_controller?

  def authenticate_user
    user = User.find_for_database_authentication(email: params[:email])
    if user.valid_password?(params[:password])
      render json: payload(user)
    else
      render json: {error: 'Invalid Username/Password'}, status: :bad_request
    end
  end

  def register_user
    user = User.create(email: params[:email], password: params[:password], first_name: params[:first_name], last_name: params[:last_name])
    if not (user == nil or user.id == nil)
      render json: {status: 'Success'}
    else
      render json: {error: 'Invalid information'}, status: :bad_request
    end
  end

  def home
    render json: {'logged_in' => false}
  end

  private
  def payload(user)
    return nil unless user and user.id
    {
      auth_token: JsonWebToken.encode({user_id: user.id}),
      user: {id: user.id, email: user.email}
    }
  end

  protected
  def configure_permitted_parameters
  devise_parameter_sanitizer.permit(:sign_up, keys: [:first_name, :last_name])
  end

end
