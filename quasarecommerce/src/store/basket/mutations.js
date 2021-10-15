export function setBasket(state, basket) {
  state.basket =basket;
}

export function removeFromBasket(state, productid) {
  let index = state.basket.basketItems.findIndex((c) => c.productId == productid);
  if (index > -1) {
    state.basket.basketItems.splice(index, 1);
  }
}

export function clearBasket(state) {
  state.basket.basketItems=[];
}

