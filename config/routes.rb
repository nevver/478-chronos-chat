Rails.application.routes.draw do
  #devise_for :users
  get  '' => 'home#index'
  post 'authenticate' => 'authentication#authenticate_user'
  post 'registration' => 'authentication#register_user'
  get  '/conversations' => 'conversations#home'
  get  '/conversations/create' => 'conversations#home'
  get  '/conversations/index' => 'conversations#index'
  post '/conversations/create' => 'conversations#create'
  get  '/messages' => 'messages#home'
  get  '/messages/create' => 'messages#home'
  get  '/messages/index' => 'messages#index'
  post '/messages/create' => 'messages#create'
end
