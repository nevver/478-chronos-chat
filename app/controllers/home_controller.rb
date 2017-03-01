class HomeController < ApplicationController

  def index
    render :text => "Chronos Messenger API", status: :ok
  end

end
