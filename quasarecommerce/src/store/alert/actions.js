export function setMessage(context, message) {
  context.commit("setMessage", message);
}

export function setErrors(context, errors) {
  context.commit("setErrors", errors);
}

export function clearAlert(context) {
  context.commit("clearAlert");
}
