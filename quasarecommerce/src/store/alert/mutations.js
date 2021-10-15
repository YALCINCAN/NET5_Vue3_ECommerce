export function setMessage(state, message) {
  if (message) {
    state.message = message;
    state.show = true;
  }
}

export function setErrors(state, errors) {
  if (errors) {
    state.errors = errors;
    state.show = true;
  }
}

export function clearAlert(state) {
  state.message = null;
  state.errors = [];
  state.show = false;
}
