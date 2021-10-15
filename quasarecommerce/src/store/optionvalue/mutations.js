export function setOptionValues(state, optionvalues) {
    state.optionvalues = optionvalues;
  }
  
  export function addOptionValue(state, optionvalue) {
    state.optionvalues.push(optionvalue);
  }
  
  export function updateOptionValue(state, optionvalue) {
    let index = state.optionvalues.findIndex((c) => c.id == optionvalue.id);
    if (index > -1) {
      state.optionvalues.splice(index, 1, optionvalue);
    }
  }
  export function removeOptionValue(state, id) {
    let index = state.optionvalues.findIndex((c) => c.id == id);
    if (index > -1) {
      state.optionvalues.splice(index, 1);
    }
  }
  