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
		userNum = @conversation.one_or_2?(current_user)
		if(userNum == 1)
			@messages = @conversation.messages.where(read_by_1: false).order(:created_at)
		else
			@messages = @conversation.messages.where(read_by_2: false).order(:created_at)
		end

		render json: @messages
		@messages.each do |message|
			message.mark_read(current_user)
		end
	end

	def create
 		@message = @conversation.messages.new(user_id: current_user.id, body: params[:body], conversation_id: params[:conversation_id], user_email: current_user.email)
 		userNum = @conversation.one_or_2?(current_user)
		if(userNum == 1)
			@message.read_by_1 = false
		else
			@message.read_by_2 = false
		end
 		if @message.save

 			render json: {
 				status: 'Message Sent'
 			}, status: 200

 		else

 			render json: {
 				status: 'Error',
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
