class MessagesController < ApplicationController
	before_action :authenticate_request!
	before_action do
	   @conversation = Conversation.find(params[:conversation_id])
	   check_user
	end

	def home
    render json: {'logged_in' => true}
  	end


	def index
		@messages = @conversation.messages.order(:created_at)
		render json: @messages
	end

	def create
 		@message = @conversation.messages.new(user_id: current_user.id, body: params[:body], conversation_id: params[:conversation_id], user_email: current_user.email)
 		if @message.save

 			render json: {
 				status: 'Message Sent'
 			}, status: 200

 		else

 			render json: {
 				status: 'Message Failed to Send',
 			}, status: 401

 		end
 	end

	private

 	def check_user
 		if not (current_user.id == @conversation.sender_id or current_user.id == @conversation.recipient_id)
 			render json: {
  			error: "Invalid user!",
  			status: 401
			}, status: 401
		end
	end



end
