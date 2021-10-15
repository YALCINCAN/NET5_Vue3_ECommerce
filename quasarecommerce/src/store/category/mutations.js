export function setCategories(state, categories) {
    state.categories = categories;
  }
  
  export function addCategory(state, category) {
    state.categories.push(category);
  }
  
  export function updateCategory(state, category) {
    let index = state.categories.findIndex((c) => c.id == category.id);
    if (index > -1) {
      state.categories.splice(index, 1, category);
    }
  }
  
  export function removeCategory(state, id) {
    let index = state.categories.findIndex((c) => c.id == id);
    if (index > -1) {
      state.categories.splice(index, 1);
    }
  }
  