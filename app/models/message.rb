class Message < ActiveRecord::Base
 belongs_to :conversation
 belongs_to :user
 validates_presence_of :body, :conversation_id, :user_id

 	def mark_read(user)
 		if self.conversation.sender == user
 			self.read_by_1 = false
 		else
 			self.read_by_2 = false
 		end
 		self.save
 	end

end
