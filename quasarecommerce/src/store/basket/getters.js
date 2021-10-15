export function getBasket (state) {
   return state.basket;
}

export function getBasketItemCount(state){
   return state.basket.basketItems.length
}
