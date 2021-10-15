export function setOptions(state, options) {
    state.options = options;
  }
  
  export function addOption(state, option) {
    state.options.push(option);
  }
  
  export function updateOption(state, option) {
    let index = state.options.findIndex((c) => c.id == option.id);
    if (index > -1) {
      state.options.splice(index, 1, option);
    }
  }
  export function removeOption(state, id) {
    let index = state.options.findIndex((c) => c.id == id);
    if (index > -1) {
      state.options.splice(index, 1);
    }
  }
  