export function setBrands(state, brands) {
    state.brands = brands;
  }
  
  export function addBrand(state, brand) {
    state.brands.push(brand);
  }
  
  export function removeBrand(state, id) {
    let index = state.brands.findIndex((c) => c.id == id);
    if (index > -1) {
      state.brands.splice(index, 1);
    }
  }
  