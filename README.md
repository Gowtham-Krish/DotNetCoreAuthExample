# DotNetCoreAuthExample
This solution has project which implements Authentication & Authorization using Asp Dot Net core identity and JWT access token.
Weather App:
  MVC UI pages which allows to login to the app, when logged in based on the user role one can see today's weather or weather forecast for five days. The weather details are random and it doesnt uses any 3rd party weather APIs
  User can also register themselves using EmailId and decide a role for them (either Admin or Common).
  
Weather Server:
  Web Api which exposes two end points.
  TodaysWeather which gives the weather for today. It needs the user to be of role Admin.
  Forecast which gives the weather forecast for five days. It needs the user to be of role Common.
  
Api Integration Tests:
  Added integration tests using Xunit. 
  Generate JWT token for a test user with Common and Admin roles and test for the API result.
  
  
