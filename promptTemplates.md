-- Create a handler class.
using getValues.cs as a template. Write a GetRestaurants handler class that injects the the RestaurantRepository and uses the interface to get all the restaurants. Return it in a response object named Response that has a list of RestaurantDetails. There are no parameters on the request object.


-- create the restaurant controller test
using the Given_a_valid_request_to_getvalues.cs file as a template create an test for the RestaurantController that uses a valid request to the GetRestaurants handler and returns two restaurants.



-- prompt used to create the plan for the AddressesController
using Given_a_valid_request_to_getrestaurants.cs as an example create a plan to implement the GET method on the AddressController using the GetAddresses handler. Be sure to create an integration test that tests the request and response from the controller endpoint.