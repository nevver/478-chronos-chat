class MessagesController < ApplicationController
	before_action :authenticate_request!
	before_action do
	   @conversation = Conversation.find(params[:conversation_id])
	   is_user_in_conversation
	end

	def home
    render json: {'logged_in' => true}
  	end


	def index
		@messages = @conversation.messages.order(:created_at)
		render json: @messages
	end

	def create
 		@message = @conversation.messages.new(user_id: current_user.id, body: params[:body], conversation_id: params[:conversation_id], user_email: current_user.email, body2: params[:body2], nc: params[:nc], nc2: params[:nc2], tag: params[:tag], tag2: params[:tag2], key: params[:key], key2: params[:key2])
 		if @message.save

 			render json: {
 				status: 'Message Sent'
 			}, status: :ok

 		else

 			render json: {
 				status: 'Message Failed to Send',
 			}, status: :bad_request

 		end
 	end


	private
 	def is_user_in_conversation
 		if not (current_user.id == @conversation.sender_id or current_user.id == @conversation.recipient_id)
 			render json: {
  			error: "Invalid user!",
  			status: :bad_request
			}, status: :bad_request
		end
	end



end
