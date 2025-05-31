#!/bin/bash

declare -A function_apps
function_apps["./src/ABC.FoodDelivery.AdminService"]=7001
function_apps["./src/ABC.FoodDelivery.CustomerService"]=7041
function_apps["./src/ABC.FoodDelivery.DeliveryService"]=7051
function_apps["./src/ABC.FoodDelivery.IdentityService"]=7061
function_apps["./src/ABC.FoodDelivery.OrderService"]=7071
function_apps["./src/ABC.FoodDelivery.RestaurantService"]=7081

for folder in "${!function_apps[@]}"
do
  port=${function_apps[$folder]}
  (cd "$folder" && func start --port $port &)
done

wait